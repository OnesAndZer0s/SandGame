using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing a set of other named tags. Order is not guaranteed. </summary>
  public sealed class CompoundTag : NBTTag, ICollection<NBTTag>, ICollection
  {
    /// <summary> Type of this tag (Compound). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.Compound; }
    }

    readonly Dictionary<string, NBTTag> tags = new Dictionary<string, NBTTag>();


    /// <summary> Creates an empty unnamed Byte tag. </summary>
    public CompoundTag() { }


    /// <summary> Creates an empty Byte tag with the given name. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public CompoundTag(string tagName)
    {
      name = tagName;
    }


    /// <summary> Creates an unnamed Byte tag, containing the given tags. </summary>
    /// <param name="tags"> Collection of tags to assign to this tag's Value. May not be null </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tags"/> is <c>null</c>, or one of the tags is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If some of the given tags were not named, or two tags with the same name were given. </exception>
    public CompoundTag(IEnumerable<NBTTag> tags)
        : this(null, tags) { }


    /// <summary> Creates an Byte tag with the given name, containing the given tags. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="tags"> Collection of tags to assign to this tag's Value. May not be null </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tags"/> is <c>null</c>, or one of the tags is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If some of the given tags were not named, or two tags with the same name were given. </exception>
    public CompoundTag(string? tagName, IEnumerable<NBTTag> tags)
    {
      if (tags == null) throw new ArgumentNullException(nameof(tags));
      name = tagName;
      foreach (NBTTag tag in tags)
      {
        Add(tag);
      }
    }


    /// <summary> Creates a deep copy of given Compound. </summary>
    /// <param name="other"> An existing Compound to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    public CompoundTag(CompoundTag other)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      name = other.name;
      foreach (NBTTag tag in other.tags.Values)
      {
        Add((NBTTag)tag.Clone());
      }
    }


    /// <summary> Gets or sets the tag with the specified name. May return <c>null</c>. </summary>
    /// <returns> The tag with the specified key. Null if tag with the given name was not found. </returns>
    /// <param name="tagName"> The name of the tag to get or set. Must match tag's actual name. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tagName"/> is <c>null</c>; or if trying to assign null value. </exception>
    /// <exception cref="ArgumentException"> <paramref name="tagName"/> does not match the given tag's actual name;
    /// or given tag already has a Parent. </exception>
    public override NBTTag this[string tagName]
    {

      get { return Get<NBTTag>(tagName)!; }
      set
      {
        if (tagName == null)
        {
          throw new ArgumentNullException(nameof(tagName));
        }
        else if (value == null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        else if (value.Name != tagName)
        {
          throw new ArgumentException("Given tag name must match tag's actual name.");
        }
        else if (value.Parent != null)
        {
          throw new ArgumentException("A tag may only be added to one compound/list at a time.");
        }
        else if (value == this)
        {
          throw new ArgumentException("Cannot add tag to itself");
        }
        tags[tagName] = value;
        value.Parent = this;
      }
    }


    /// <summary> Gets the tag with the specified name. May return <c>null</c>. </summary>
    /// <param name="tagName"> The name of the tag to get. </param>
    /// <typeparam name="T"> Type to cast the result to. Must derive from NBTTag. </typeparam>
    /// <returns> The tag with the specified key. Null if tag with the given name was not found. </returns>
    /// <exception cref="ArgumentNullException"> <paramref name="tagName"/> is <c>null</c>. </exception>
    /// <exception cref="InvalidCastException"> If tag could not be cast to the desired tag. </exception>

    public T? Get<T>(string tagName) where T : NBTTag
    {
      if (tagName == null) throw new ArgumentNullException(nameof(tagName));
      NBTTag? result;
      if (tags.TryGetValue(tagName, out result))
      {
        return (T)result;
      }
      return null;
    }


    /// <summary> Gets the tag with the specified name. May return <c>null</c>. </summary>
    /// <param name="tagName"> The name of the tag to get. </param>
    /// <returns> The tag with the specified key. Null if tag with the given name was not found. </returns>
    /// <exception cref="ArgumentNullException"> <paramref name="tagName"/> is <c>null</c>. </exception>
    /// <exception cref="InvalidCastException"> If tag could not be cast to the desired tag. </exception>

    public NBTTag? Get(string tagName)
    {
      if (tagName == null) throw new ArgumentNullException(nameof(tagName));
      NBTTag? result;
      if (tags.TryGetValue(tagName, out result))
      {
        return result;
      }
      return null;
    }


    /// <summary> Gets the tag with the specified name. </summary>
    /// <param name="tagName"> The name of the tag to get. </param>
    /// <param name="result"> When this method returns, contains the tag associated with the specified name, if the tag is found;
    /// otherwise, null. This parameter is passed uninitialized. </param>
    /// <typeparam name="T"> Type to cast the result to. Must derive from NBTTag. </typeparam>
    /// <returns> true if the Compound contains a tag with the specified name; otherwise, false. </returns>
    /// <exception cref="ArgumentNullException"> <paramref name="tagName"/> is <c>null</c>. </exception>
    /// <exception cref="InvalidCastException"> If tag could not be cast to the desired tag. </exception>
    public bool TryGet<T>(string tagName, out T? result) where T : NBTTag
    {
      if (tagName == null) throw new ArgumentNullException(nameof(tagName));
      NBTTag? tempResult;
      if (tags.TryGetValue(tagName, out tempResult))
      {
        result = (T)tempResult;
        return true;
      }
      else
      {
        result = null;
        return false;
      }
    }

    /// <summary> Gets the tag with the specified name. </summary>
    /// <param name="tagName"> The name of the tag to get. </param>
    /// <param name="result"> When this method returns, contains the tag associated with the specified name, if the tag is found;
    /// otherwise, null. This parameter is passed uninitialized. </param>
    /// <returns> true if the Compound contains a tag with the specified name; otherwise, false. </returns>
    /// <exception cref="ArgumentNullException"> <paramref name="tagName"/> is <c>null</c>. </exception>
    /// <exception cref="InvalidCastException"> If tag could not be cast to the desired tag. </exception>
    public bool TryGet(string tagName, out NBTTag? result)
    {
      if (tagName == null) throw new ArgumentNullException(nameof(tagName));
      NBTTag? tempResult;
      if (tags.TryGetValue(tagName, out tempResult))
      {
        result = tempResult;
        return true;
      }
      else
      {
        result = null;
        return false;
      }
    }


    /// <summary> Adds all tags from the specified collection to this Compound. </summary>
    /// <param name="newTags"> The collection whose elements should be added to this Compound. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="newTags"/> is <c>null</c>, or one of the tags in newTags is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If one of the given tags was unnamed,
    /// or if a tag with the given name already exists in this Compound. </exception>
    public void AddRange(IEnumerable<NBTTag> newTags)
    {
      if (newTags == null) throw new ArgumentNullException(nameof(newTags));
      foreach (NBTTag tag in newTags)
      {
        Add(tag);
      }
    }


    /// <summary> Determines whether this Compound contains a tag with a specific name. </summary>
    /// <param name="tagName"> NBTTag name to search for. May not be <c>null</c>. </param>
    /// <returns> true if a tag with given name was found; otherwise, false. </returns>
    /// <exception cref="ArgumentNullException"> <paramref name="tagName"/> is <c>null</c>. </exception>
    public bool Contains(string tagName)
    {
      if (tagName == null) throw new ArgumentNullException(nameof(tagName));
      return tags.ContainsKey(tagName);
    }


    /// <summary> Removes the tag with the specified name from this Compound. </summary>
    /// <param name="tagName"> The name of the tag to remove. </param>
    /// <returns> true if the tag is successfully found and removed; otherwise, false.
    /// This method returns false if name is not found in the Compound. </returns>
    /// <exception cref="ArgumentNullException"> <paramref name="tagName"/> is <c>null</c>. </exception>
    public bool Remove(string tagName)
    {
      if (tagName == null) throw new ArgumentNullException(nameof(tagName));
      NBTTag? tag;
      if (!tags.TryGetValue(tagName, out tag))
      {
        return false;
      }
      tags.Remove(tagName);
      tag.Parent = null;
      return true;
    }


    internal void RenameTag(string oldName, string newName)
    {
      Debug.Assert(oldName != null);
      Debug.Assert(newName != null);
      Debug.Assert(newName != oldName);
      if (tags.TryGetValue(newName, out _))
      {
        throw new ArgumentException("Cannot rename: a tag with the name already exists in this compound.");
      }
      if (!tags.TryGetValue(oldName, out NBTTag? tag))
      {
        throw new ArgumentException("Cannot rename: no tag found to rename.");
      }
      tags.Remove(oldName);
      tags.Add(newName, tag);
    }


    /// <summary> Gets a collection containing all tag names in this Compound. </summary>

    public IEnumerable<string> Names
    {
      get { return tags.Keys; }
    }

    /// <summary> Gets a collection containing all tags in this Compound. </summary>

    public IEnumerable<NBTTag> Tags
    {
      get { return tags.Values; }
    }


    #region Reading / Writing

    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      if (Parent != null && readStream.Selector != null && !readStream.Selector(this))
      {
        SkipTag(readStream);
        return false;
      }

      while (true)
      {
        NBTTagType nextTag = readStream.ReadTagType();
        NBTTag newTag;
        switch (nextTag)
        {
          case NBTTagType.End:
            return true;

          case NBTTagType.Byte:
            newTag = new ByteTag();
            break;

          case NBTTagType.Short:
            newTag = new ShortTag();
            break;

          case NBTTagType.Int:
            newTag = new IntTag();
            break;

          case NBTTagType.Long:
            newTag = new LongTag();
            break;

          case NBTTagType.Float:
            newTag = new FloatTag();
            break;

          case NBTTagType.Double:
            newTag = new DoubleTag();
            break;

          case NBTTagType.ByteArray:
            newTag = new ByteArrayTag();
            break;

          case NBTTagType.String:
            newTag = new StringTag();
            break;

          case NBTTagType.List:
            newTag = new ListTag();
            break;

          case NBTTagType.Compound:
            newTag = new CompoundTag();
            break;

          case NBTTagType.IntArray:
            newTag = new IntArrayTag();
            break;

          case NBTTagType.LongArray:
            newTag = new LongArrayTag();
            break;

          default:
            throw new FormatException("Unsupported tag type found in NBT_Compound: " + nextTag);
        }
        newTag.Parent = this;
        newTag.Name = readStream.ReadString();
        if (newTag.ReadTag(readStream))
        {
          // ReSharper disable AssignNullToNotNullAttribute
          // newTag.Name is never null
          tags.Add(newTag.Name, newTag);
          // ReSharper restore AssignNullToNotNullAttribute
        }
      }
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      while (true)
      {
        NBTTagType nextTag = readStream.ReadTagType();
        NBTTag newTag;
        switch (nextTag)
        {
          case NBTTagType.End:
            return;

          case NBTTagType.Byte:
            newTag = new ByteTag();
            break;

          case NBTTagType.Short:
            newTag = new ShortTag();
            break;

          case NBTTagType.Int:
            newTag = new IntTag();
            break;

          case NBTTagType.Long:
            newTag = new LongTag();
            break;

          case NBTTagType.Float:
            newTag = new FloatTag();
            break;

          case NBTTagType.Double:
            newTag = new DoubleTag();
            break;

          case NBTTagType.ByteArray:
            newTag = new ByteArrayTag();
            break;

          case NBTTagType.String:
            newTag = new StringTag();
            break;

          case NBTTagType.List:
            newTag = new ListTag();
            break;

          case NBTTagType.Compound:
            newTag = new CompoundTag();
            break;

          case NBTTagType.IntArray:
            newTag = new IntArrayTag();
            break;

          case NBTTagType.LongArray:
            newTag = new LongArrayTag();
            break;

          default:
            throw new FormatException("Unsupported tag type found in NBT_Compound: " + nextTag);
        }
        readStream.SkipString();
        newTag.SkipTag(readStream);
      }
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.Compound);
      if (Name == null) throw new FormatException("Name is null");
      writeStream.Write(Name);
      WriteData(writeStream);
    }


    internal override void WriteData(NBTBinaryWriter writeStream)
    {
      foreach (NBTTag tag in tags.Values)
      {
        tag.WriteTag(writeStream);
      }
      writeStream.Write(NBTTagType.End);
    }

    #endregion


    #region Implementation of IEnumerable<NBTTag>

    /// <summary> Returns an enumerator that iterates through all tags in this Compound. </summary>
    /// <returns> An IEnumerator&gt;NBTTag&lt; that can be used to iterate through the collection. </returns>
    public IEnumerator<NBTTag> GetEnumerator()
    {
      return tags.Values.GetEnumerator();
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return tags.Values.GetEnumerator();
    }

    #endregion


    #region Implementation of ICollection<NBTTag>

    /// <summary> Adds a tag to this Compound. </summary>
    /// <param name="newTag"> The object to add to this Compound. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="newTag"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If the given tag is unnamed;
    /// or if a tag with the given name already exists in this Compound. </exception>
    public void Add(NBTTag newTag)
    {
      if (newTag == null)
      {
        throw new ArgumentNullException(nameof(newTag));
      }
      else if (newTag == this)
      {
        throw new ArgumentException("Cannot add tag to self");
      }
      else if (newTag.Name == null)
      {
        throw new ArgumentException("Only named tags are allowed in compound tags.");
      }
      else if (newTag.Parent != null)
      {
        throw new ArgumentException("A tag may only be added to one compound/list at a time.");
      }
      tags.Add(newTag.Name, newTag);
      newTag.Parent = this;
    }


    /// <summary> Removes all tags from this Compound. </summary>
    public void Clear()
    {
      foreach (NBTTag tag in tags.Values)
      {
        tag.Parent = null;
      }
      tags.Clear();
    }


    /// <summary> Determines whether this Compound contains a specific NBTTag.
    /// Looks for exact object matches, not name matches. </summary>
    /// <returns> true if tag is found; otherwise, false. </returns>
    /// <param name="tag"> The object to locate in this Compound. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tag"/> is <c>null</c>. </exception>
    public bool Contains(NBTTag tag)
    {
      if (tag == null) throw new ArgumentNullException(nameof(tag));
      return tags.ContainsValue(tag);
    }


    /// <summary> Copies the tags of the Compound to an array, starting at a particular array index. </summary>
    /// <param name="array"> The one-dimensional array that is the destination of the tag copied from Compound.
    /// The array must have zero-based indexing. </param>
    /// <param name="arrayIndex"> The zero-based index in array at which copying begins. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="array"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentOutOfRangeException"> arrayIndex is less than 0. </exception>
    /// <exception cref="ArgumentException"> Given array is multidimensional; arrayIndex is equal to or greater than the length of array;
    /// the number of tags in this Compound is greater than the available space from arrayIndex to the end of the destination array;
    /// or type NBTTag cannot be cast automatically to the type of the destination array. </exception>
    public void CopyTo(NBTTag[] array, int arrayIndex)
    {
      tags.Values.CopyTo(array, arrayIndex);
    }


    /// <summary> Removes the first occurrence of a specific NBTTag from the Compound.
    /// Looks for exact object matches, not name matches. </summary>
    /// <returns> true if tag was successfully removed from the Compound; otherwise, false.
    /// This method also returns false if tag is not found. </returns>
    /// <param name="tag"> The tag to remove from the Compound. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tag"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If the given tag is unnamed </exception>
    public bool Remove(NBTTag tag)
    {
      if (tag == null) throw new ArgumentNullException(nameof(tag));
      if (tag.Name == null) throw new ArgumentException("Trying to remove an unnamed tag.");
      NBTTag? maybeItem;
      if (tags.TryGetValue(tag.Name, out maybeItem))
      {
        if (maybeItem == tag && tags.Remove(tag.Name))
        {
          tag.Parent = null;
          return true;
        }
      }
      return false;
    }


    /// <summary> Gets the number of tags contained in the Compound. </summary>
    /// <returns> The number of tags contained in the Compound. </returns>
    public int Count
    {
      get { return tags.Count; }
    }

    bool ICollection<NBTTag>.IsReadOnly
    {
      get { return false; }
    }

    #endregion


    #region Implementation of ICollection

    void ICollection.CopyTo(Array array, int index)
    {
      CopyTo((NBTTag[])array, index);
    }


    object ICollection.SyncRoot
    {
      get { return (tags as ICollection).SyncRoot; }
    }

    bool ICollection.IsSynchronized
    {
      get { return false; }
    }

    #endregion


    /// <inheritdoc />
    public override object Clone()
    {
      return new CompoundTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_Compound");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.AppendFormat(": {0} entries {{", tags.Count);

      if (Count > 0)
      {
        sb.Append('\n');
        foreach (NBTTag tag in tags.Values)
        {
          tag.PrettyPrint(sb, indentString, indentLevel + 1);
          sb.Append('\n');
        }
        for (int i = 0; i < indentLevel; i++)
        {
          sb.Append(indentString);
        }
      }
      sb.Append('}');
    }

    public bool IsEmpty()
    {
      return tags.Count == 0;
    }

  }
}