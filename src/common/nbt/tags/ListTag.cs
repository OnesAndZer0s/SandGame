using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing a list of unnamed tags, all of the same kind. </summary>
  public sealed class ListTag : NBTTag, IList<NBTTag>, IList
  {
    /// <summary> Type of this tag (List). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.List; }
    }


    readonly List<NBTTag> tags = new List<NBTTag>();

    /// <summary> Gets or sets the tag type of this list. All tags in this NBTTag must be of the same type. </summary>
    /// <exception cref="ArgumentException"> If the given NBTTagType does not match the type of existing list items (for non-empty lists). </exception>
    /// <exception cref="ArgumentOutOfRangeException"> If the given NBTTagType is a recognized tag type. </exception>
    public NBTTagType ListType
    {
      get { return listType; }
      set
      {
        if (value == NBTTagType.End)
        {
          // Empty lists may have type "End", see: https://github.com/fragmer/Sandbox.Common.NBT/issues/12
          if (tags.Count > 0)
          {
            throw new ArgumentException("Only empty list tags may have NBTTagType of End.");
          }
        }
        else if (value < NBTTagType.Byte || (value > NBTTagType.LongArray && value != NBTTagType.Unknown))
        {
          throw new ArgumentOutOfRangeException(nameof(value));
        }
        if (tags.Count > 0)
        {
          NBTTagType actualType = tags[0].NBTTagType;
          // We can safely assume that ALL tags have the same NBTTagType as the first tag.
          if (actualType != value)
          {
            string msg = String.Format("Given NBTTagType ({0}) does not match actual element type ({1})",
                                       value, actualType);
            throw new ArgumentException(msg);
          }
        }
        listType = value;
      }
    }

    NBTTagType listType;


    /// <summary> Creates an unnamed List with empty contents and undefined ListType. </summary>
    public ListTag()
        : this(null, null, NBTTagType.Unknown) { }


    /// <summary> Creates an List with given name, empty contents, and undefined ListType. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public ListTag(string tagName)
        : this(tagName, null, NBTTagType.Unknown) { }


    /// <summary> Creates an unnamed List with the given contents, and inferred ListType. 
    /// If given tag array is empty, NBTTagType remains Unknown. </summary>
    /// <param name="tags"> Collection of tags to insert into the list. All tags are expected to be of the same type.
    /// ListType is inferred from the first tag. List may be empty, but may not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tags"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If given tags are of mixed types. </exception>
    public ListTag(IEnumerable<NBTTag> tags)
        : this(null, tags, NBTTagType.Unknown)
    {
      // the base constructor will allow null "tags," but we don't want that in this constructor
      if (tags == null) throw new ArgumentNullException(nameof(tags));
    }


    /// <summary> Creates an unnamed List with empty contents and an explicitly specified ListType.
    /// If ListType is Unknown, it will be inferred from the type of the first added tag.
    /// Otherwise, all tags added to this list are expected to be of the given type. </summary>
    /// <param name="givenListType"> Name to assign to this tag. May be Unknown. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="givenListType"/> is not a recognized tag type. </exception>
    public ListTag(NBTTagType givenListType)
        : this(null, null, givenListType) { }


    /// <summary> Creates an List with the given name and contents, and inferred ListType. 
    /// If given tag array is empty, NBTTagType remains Unknown. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="tags"> Collection of tags to insert into the list. All tags are expected to be of the same type.
    /// ListType is inferred from the first tag. List may be empty, but may not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tags"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If given tags are of mixed types. </exception>
    public ListTag(string tagName, IEnumerable<NBTTag> tags)
        : this(tagName, tags, NBTTagType.Unknown)
    {
      // the base constructor will allow null "tags," but we don't want that in this constructor
      if (tags == null) throw new ArgumentNullException(nameof(tags));
    }


    /// <summary> Creates an unnamed List with the given contents, and an explicitly specified ListType. </summary>
    /// <param name="tags"> Collection of tags to insert into the list.
    /// All tags are expected to be of the same type (matching givenListType).
    /// List may be empty, but may not be <c>null</c>. </param>
    /// <param name="givenListType"> Name to assign to this tag. May be Unknown (to infer type from the first element of tags). </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tags"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="givenListType"/> is not a valid tag type. </exception>
    /// <exception cref="ArgumentException"> If given tags do not match <paramref name="givenListType"/>, or are of mixed types. </exception>
    public ListTag(IEnumerable<NBTTag> tags, NBTTagType givenListType)
        : this(null, tags, givenListType)
    {
      // the base constructor will allow null "tags," but we don't want that in this constructor
      if (tags == null) throw new ArgumentNullException(nameof(tags));
    }


    /// <summary> Creates an List with the given name, empty contents, and an explicitly specified ListType. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="givenListType"> Name to assign to this tag.
    /// If givenListType is Unknown, ListType will be inferred from the first tag added to this List. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="givenListType"/> is not a valid tag type. </exception>
    public ListTag(string tagName, NBTTagType givenListType)
        : this(tagName, null, givenListType) { }


    /// <summary> Creates an List with the given name and contents, and an explicitly specified ListType. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="tags"> Collection of tags to insert into the list.
    /// All tags are expected to be of the same type (matching givenListType). May be empty or <c>null</c>. </param>
    /// <param name="givenListType"> Name to assign to this tag. May be Unknown (to infer type from the first element of tags). </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="givenListType"/> is not a valid tag type. </exception>
    /// <exception cref="ArgumentException"> If given tags do not match <paramref name="givenListType"/>, or are of mixed types. </exception>
    public ListTag(string? tagName, IEnumerable<NBTTag>? tags, NBTTagType givenListType)
    {
      name = tagName;
      ListType = givenListType;

      if (tags == null) return;
      foreach (NBTTag tag in tags)
      {
        Add(tag);
      }
    }


    /// <summary> Creates a deep copy of given List. </summary>
    /// <param name="other"> An existing List to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    public ListTag(ListTag other)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      name = other.name;
      listType = other.listType;
      foreach (NBTTag tag in other.tags)
      {
        tags.Add((NBTTag)tag.Clone());
      }
    }


    /// <summary> Gets or sets the tag at the specified index. </summary>
    /// <returns> The tag at the specified index. </returns>
    /// <param name="tagIndex"> The zero-based index of the tag to get or set. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="tagIndex"/> is not a valid index in the List. </exception>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> Given tag's type does not match ListType. </exception>

    public override NBTTag this[int tagIndex]
    {
      get { return tags[tagIndex]; }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        else if (value.Parent != null)
        {
          throw new ArgumentException("A tag may only be added to one compound/list at a time.");
        }
        else if (value == this || value == Parent)
        {
          throw new ArgumentException("A list tag may not be added to itself or to its child tag.");
        }
        else if (value.Name != null)
        {
          throw new ArgumentException("Named tag given. A list may only contain unnamed tags.");
        }
        if (listType != NBTTagType.Unknown && value.NBTTagType != listType)
        {
          throw new ArgumentException("Items must be of type " + listType);
        }
        tags[tagIndex] = value;
        value.Parent = this;
      }
    }


    /// <summary> Gets or sets the tag with the specified name. </summary>
    /// <param name="tagIndex"> The zero-based index of the tag to get or set. </param>
    /// <typeparam name="T"> Type to cast the result to. Must derive from NBTTag. </typeparam>
    /// <returns> The tag with the specified key. </returns>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="tagIndex"/> is not a valid index in the List. </exception>
    /// <exception cref="InvalidCastException"> If tag could not be cast to the desired tag. </exception>


    public T Get<T>(int tagIndex) where T : NBTTag
    {
      return (T)tags[tagIndex];
    }


    /// <summary> Adds all tags from the specified collection to the end of this List. </summary>
    /// <param name="newTags"> The collection whose elements should be added to this List. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="newTags"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If given tags do not match ListType, or are of mixed types. </exception>
    public void AddRange(IEnumerable<NBTTag> newTags)
    {
      if (newTags == null) throw new ArgumentNullException(nameof(newTags));
      foreach (NBTTag tag in newTags)
      {
        Add(tag);
      }
    }


    /// <summary> Copies all tags in this List to an array. </summary>
    /// <returns> Array of Tags. </returns>


    // ReSharper disable ReturnTypeCanBeEnumerable.Global
    public NBTTag[] ToArray()
    {
      // ReSharper restore ReturnTypeCanBeEnumerable.Global
      return tags.ToArray();
    }


    /// <summary> Copies all tags in this List to an array, and casts it to the desired type. </summary>
    /// <typeparam name="T"> Type to cast every member of List to. Must derive from NBTTag. </typeparam>
    /// <returns> Array of Tags cast to the desired type. </returns>
    /// <exception cref="InvalidCastException"> If contents of this list cannot be cast to the given type. </exception>


    public T[] ToArray<T>() where T : NBTTag
    {
      var result = new T[tags.Count];
      for (int i = 0; i < result.Length; i++)
      {
        result[i] = (T)tags[i];
      }
      return result;
    }


    #region Reading / Writing

    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      if (readStream.Selector != null && !readStream.Selector(this))
      {
        SkipTag(readStream);
        return false;
      }

      ListType = readStream.ReadTagType();

      int length = readStream.ReadInt32();
      if (length < 0)
      {
        throw new FormatException("Negative list size given.");
      }

      for (int i = 0; i < length; i++)
      {
        NBTTag newTag;
        switch (ListType)
        {
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
            // should never happen, since ListType is checked beforehand
            throw new FormatException("Unsupported tag type found in a list: " + ListType);
        }
        newTag.Parent = this;
        if (newTag.ReadTag(readStream))
        {
          tags.Add(newTag);
        }
      }
      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      // read list type, and make sure it's defined
      ListType = readStream.ReadTagType();

      int length = readStream.ReadInt32();
      if (length < 0)
      {
        throw new FormatException("Negative list size given.");
      }

      switch (ListType)
      {
        case NBTTagType.Byte:
          readStream.Skip(length);
          break;
        case NBTTagType.Short:
          readStream.Skip(length * sizeof(short));
          break;
        case NBTTagType.Int:
          readStream.Skip(length * sizeof(int));
          break;
        case NBTTagType.Long:
          readStream.Skip(length * sizeof(long));
          break;
        case NBTTagType.Float:
          readStream.Skip(length * sizeof(float));
          break;
        case NBTTagType.Double:
          readStream.Skip(length * sizeof(double));
          break;
        default:
          for (int i = 0; i < length; i++)
          {
            switch (listType)
            {
              case NBTTagType.ByteArray:
                new ByteArrayTag().SkipTag(readStream);
                break;
              case NBTTagType.String:
                readStream.SkipString();
                break;
              case NBTTagType.List:
                new ListTag().SkipTag(readStream);
                break;
              case NBTTagType.Compound:
                new CompoundTag().SkipTag(readStream);
                break;
              case NBTTagType.IntArray:
                new IntArrayTag().SkipTag(readStream);
                break;
            }
          }
          break;
      }
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.List);
      if (Name == null) throw new FormatException("Name is null");
      writeStream.Write(Name);
      WriteData(writeStream);
    }


    internal override void WriteData(NBTBinaryWriter writeStream)
    {
      if (ListType == NBTTagType.Unknown)
      {
        throw new FormatException("List had no elements and an Unknown ListType");
      }
      writeStream.Write(ListType);
      writeStream.Write(tags.Count);
      foreach (NBTTag tag in tags)
      {
        tag.WriteData(writeStream);
      }
    }

    #endregion


    #region Implementation of IEnumerable<NBtTag> and IEnumerable

    /// <summary> Returns an enumerator that iterates through all tags in this List. </summary>
    /// <returns> An IEnumerator&gt;NBTTag&lt; that can be used to iterate through the list. </returns>
    public IEnumerator<NBTTag> GetEnumerator()
    {
      return tags.GetEnumerator();
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return tags.GetEnumerator();
    }

    #endregion


    #region Implementation of IList<NBTTag> and ICollection<NBTTag>

    /// <summary> Determines the index of a specific tag in this List </summary>
    /// <returns> The index of tag if found in the list; otherwise, -1. </returns>
    /// <param name="tag"> The tag to locate in this List. </param>
    public int IndexOf(NBTTag tag)
    {
      if (tag == null) return -1;
      return tags.IndexOf(tag);
    }


    /// <summary> Inserts an item to this List at the specified index. </summary>
    /// <param name="tagIndex"> The zero-based index at which newTag should be inserted. </param>
    /// <param name="newTag"> The tag to insert into this List. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="tagIndex"/> is not a valid index in this List. </exception>
    /// <exception cref="ArgumentNullException"> <paramref name="newTag"/> is <c>null</c>. </exception>
    public void Insert(int tagIndex, NBTTag newTag)
    {
      if (newTag == null)
      {
        throw new ArgumentNullException(nameof(newTag));
      }
      if (listType != NBTTagType.Unknown && newTag.NBTTagType != listType)
      {
        throw new ArgumentException("Items must be of type " + listType);
      }
      else if (newTag.Parent != null)
      {
        throw new ArgumentException("A tag may only be added to one compound/list at a time.");
      }
      tags.Insert(tagIndex, newTag);
      if (listType == NBTTagType.Unknown)
      {
        listType = newTag.NBTTagType;
      }
      newTag.Parent = this;
    }


    /// <summary> Removes a tag at the specified index from this List. </summary>
    /// <param name="index"> The zero-based index of the item to remove. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="index"/> is not a valid index in the List. </exception>
    public void RemoveAt(int index)
    {
      NBTTag tag = this[index];
      tags.RemoveAt(index);
      tag.Parent = null;
    }


    /// <summary> Adds a tag to this List. </summary>
    /// <param name="newTag"> The tag to add to this List. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="newTag"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> If <paramref name="newTag"/> does not match ListType. </exception>
    public void Add(NBTTag newTag)
    {
      if (newTag == null)
      {
        throw new ArgumentNullException(nameof(newTag));
      }
      else if (newTag.Parent != null)
      {
        throw new ArgumentException("A tag may only be added to one compound/list at a time.");
      }
      else if (newTag == this || newTag == Parent)
      {
        throw new ArgumentException("A list tag may not be added to itself or to its child tag.");
      }
      else if (newTag.Name != null)
      {
        throw new ArgumentException("Named tag given. A list may only contain unnamed tags.");
      }
      if (listType != NBTTagType.Unknown && newTag.NBTTagType != listType)
      {
        throw new ArgumentException("Items in this list must be of type " + listType + ". Given type: " +
                                    newTag.NBTTagType);
      }
      tags.Add(newTag);
      newTag.Parent = this;
      if (listType == NBTTagType.Unknown)
      {
        listType = newTag.NBTTagType;
      }
    }


    /// <summary> Removes all tags from this List. </summary>
    public void Clear()
    {
      for (int i = 0; i < tags.Count; i++)
      {
        tags[i].Parent = null;
      }
      tags.Clear();
    }


    /// <summary> Determines whether this List contains a specific tag. </summary>
    /// <returns> true if given tag is found in this List; otherwise, false. </returns>
    /// <param name="item"> The tag to locate in this List. </param>
    public bool Contains(NBTTag item)
    {
      return tags.Contains(item);
    }


    /// <summary> Copies the tags of this List to an array, starting at a particular array index. </summary>
    /// <param name="array"> The one-dimensional array that is the destination of the tag copied from List.
    /// The array must have zero-based indexing. </param>
    /// <param name="arrayIndex"> The zero-based index in array at which copying begins. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="array"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentOutOfRangeException"> arrayIndex is less than 0. </exception>
    /// <exception cref="ArgumentException"> Given array is multidimensional; arrayIndex is equal to or greater than the length of array;
    /// the number of tags in this List is greater than the available space from arrayIndex to the end of the destination array;
    /// or type NBTTag cannot be cast automatically to the type of the destination array. </exception>
    public void CopyTo(NBTTag[] array, int arrayIndex)
    {
      tags.CopyTo(array, arrayIndex);
    }


    /// <summary> Removes the first occurrence of a specific NBTTag from the Compound.
    /// Looks for exact object matches, not name matches. </summary>
    /// <returns> true if tag was successfully removed from this List; otherwise, false.
    /// This method also returns false if tag is not found. </returns>
    /// <param name="tag"> The tag to remove from this List. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="tag"/> is <c>null</c>. </exception>
    public bool Remove(NBTTag tag)
    {
      if (tag == null) throw new ArgumentNullException(nameof(tag));
      if (!tags.Remove(tag))
      {
        return false;
      }
      tag.Parent = null;
      return true;
    }


    /// <summary> Gets the number of tags contained in the List. </summary>
    /// <returns> The number of tags contained in the List. </returns>
    public int Count
    {
      get { return tags.Count; }
    }

    bool ICollection<NBTTag>.IsReadOnly
    {
      get { return false; }
    }

    #endregion


    #region Implementation of IList and ICollection

    void IList.Remove(object? value)
    {
      Remove((NBTTag)value!);
    }



    object? IList.this[int tagIndex]
    {
      get { return tags[tagIndex]; }
      set { this[tagIndex] = (NBTTag?)value!; }
    }


    int IList.Add(object? value)
    {
      Add((NBTTag)value!);
      return (tags.Count - 1);
    }


    bool IList.Contains(object? value)
    {
      return tags.Contains((NBTTag)value!);
    }


    int IList.IndexOf(object? value)
    {
      return tags.IndexOf((NBTTag)value!);
    }


    void IList.Insert(int index, object? value)
    {
      Insert(index, (NBTTag)value!);
    }


    bool IList.IsFixedSize
    {
      get { return false; }
    }


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

    bool IList.IsReadOnly
    {
      get { return false; }
    }

    #endregion


    /// <inheritdoc />
    public override object Clone()
    {
      return new ListTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_List");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.AppendFormat(": {0} entries {{", tags.Count);

      if (Count > 0)
      {
        sb.Append('\n');
        foreach (NBTTag tag in tags)
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
  }
}