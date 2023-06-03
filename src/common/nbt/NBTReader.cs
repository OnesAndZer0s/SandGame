using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sandbox.Common.NBT
{
  /// <summary> Represents a reader that provides fast, non-cached, forward-only access to NBT data.
  /// Each instance of NBTReader reads one complete file. </summary>
  public class NBTReader
  {
    NBTParseState state = NBTParseState.AtStreamBeginning;
    readonly NBTBinaryReader reader;
    Stack<NBTReaderNode>? nodes;
    readonly long streamStartOffset;
    bool atValue;
    object? valueCache;
    readonly bool canSeekStream;


    /// <summary> Initializes a new instance of the NBTReader class. </summary>
    /// <param name="stream"> Stream to read from. </param>
    /// <remarks> Assumes that data in the stream is Big-Endian encoded. </remarks>
    /// <exception cref="ArgumentNullException"> <paramref name="stream"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> <paramref name="stream"/> is not readable. </exception>
    public NBTReader(Stream stream)
        : this(stream, true) { }


    /// <summary> Initializes a new instance of the NBTReader class. </summary>
    /// <param name="stream"> Stream to read from. </param>
    /// <param name="bigEndian"> Whether NBT data is in Big-Endian encoding. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="stream"/> is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> <paramref name="stream"/> is not readable. </exception>
    public NBTReader(Stream stream, bool bigEndian)
    {
      if (stream == null) throw new ArgumentNullException(nameof(stream));
      SkipEndTags = true;
      CacheTagValues = false;
      ParentTagType = NBTTagType.Unknown;
      NBTTagType = NBTTagType.Unknown;

      canSeekStream = stream.CanSeek;
      if (canSeekStream)
      {
        streamStartOffset = stream.Position;
      }

      reader = new NBTBinaryReader(stream, bigEndian);
    }


    /// <summary> Gets the name of the root tag of this NBT stream. </summary>

    public string? RootName { get; private set; }

    /// <summary> Gets the name of the parent tag. May be null (for root tags and descendants of list elements). </summary>

    public string? ParentName { get; private set; }

    /// <summary> Gets the name of the current tag. May be null (for list elements and end tags). </summary>

    public string? TagName { get; private set; }

    /// <summary> Gets the type of the parent tag. Returns NBTTagType.Unknown if there is no parent tag. </summary>
    public NBTTagType ParentTagType { get; private set; }

    /// <summary> Gets the type of the current tag. </summary>
    public NBTTagType NBTTagType { get; private set; }

    /// <summary> Whether tag that we are currently on is a list element. </summary>
    public bool IsListElement
    {
      get { return (ParentTagType == NBTTagType.List); }
    }

    /// <summary> Whether current tag has a value to read. </summary>
    public bool HasValue
    {
      get
      {
        switch (NBTTagType)
        {
          case NBTTagType.Compound:
          case NBTTagType.End:
          case NBTTagType.List:
          case NBTTagType.Unknown:
            return false;
          default:
            return true;
        }
      }
    }

    /// <summary> Whether current tag has a name. </summary>
    public bool HasName
    {
      get { return (TagName != null); }
    }

    /// <summary> Whether this reader has reached the end of stream. </summary>
    public bool IsAtStreamEnd
    {
      get { return state == NBTParseState.AtStreamEnd; }
    }

    /// <summary> Whether the current tag is a Compound. </summary>
    public bool IsCompound
    {
      get { return (NBTTagType == NBTTagType.Compound); }
    }

    /// <summary> Whether the current tag is a List. </summary>
    public bool IsList
    {
      get { return (NBTTagType == NBTTagType.List); }
    }

    /// <summary> Whether the current tag has length (Lists, ByteArrays, and IntArrays have length).
    /// Compound tags also have length, technically, but it is not known until all child tags are read. </summary>
    public bool HasLength
    {
      get
      {
        switch (NBTTagType)
        {
          case NBTTagType.List:
          case NBTTagType.ByteArray:
          case NBTTagType.IntArray:
          case NBTTagType.LongArray:
            return true;
          default:
            return false;
        }
      }
    }

    /// <summary> Gets the Stream from which data is being read. </summary>

    public Stream BaseStream
    {
      get { return reader.BaseStream; }
    }

    /// <summary> Gets the number of bytes from the beginning of the stream to the beginning of this tag.
    /// If the stream is not seekable, this value will always be 0. </summary>
    public int TagStartOffset { get; private set; }

    /// <summary> Gets the number of tags read from the stream so far
    /// (including the current tag and all skipped tags). 
    /// If <c>SkipEndTags</c> is <c>false</c>, all end tags are also counted. </summary>
    public int TagsRead { get; private set; }

    /// <summary> Gets the depth of the current tag in the hierarchy.
    /// <c>RootTag</c> is at depth 1, its descendant tags are 2, etc. </summary>
    public int Depth { get; private set; }

    /// <summary> If the current tag is TAG_List, returns type of the list elements. </summary>
    public NBTTagType ListType { get; private set; }

    /// <summary> If the current tag is TAG_List, TAG_Byte_Array, or TAG_Int_Array, returns the number of elements. </summary>
    public int TagLength { get; private set; }

    /// <summary> If the parent tag is TAG_List, returns the number of elements. </summary>
    public int ParentTagLength { get; private set; }

    /// <summary> If the parent tag is TAG_List, returns index of the current tag. </summary>
    public int ListIndex { get; private set; }

    /// <summary> Gets whether this NBTReader instance is in state of error.
    /// No further reading can be done from this instance if a parse error occurred. </summary>
    public bool IsInErrorState
    {
      get { return (state == NBTParseState.Error); }
    }


    /// <summary> Reads the next tag from the stream. </summary>
    /// <returns> true if the next tag was read successfully; false if there are no more tags to read. </returns>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidReaderStateException"> If NBTReader cannot recover from a previous parsing error. </exception>
    public bool ReadToFollowing()
    {
      switch (state)
      {
        case NBTParseState.AtStreamBeginning:
          // set state to error in case reader.ReadTagType throws.
          state = NBTParseState.Error;
          // read first tag, make sure it's a compound
          if (reader.ReadTagType() != NBTTagType.Compound)
          {
            throw new NBTFormatException("Given NBT stream does not start with a TAG_Compound");
          }
          Depth = 1;
          NBTTagType = NBTTagType.Compound;
          // Read root name. Advance to the first inside tag.
          ReadTagHeader(true);
          RootName = TagName;
          return true;

        case NBTParseState.AtCompoundBeginning:
          GoDown();
          state = NBTParseState.InCompound;
          goto case NBTParseState.InCompound;

        case NBTParseState.InCompound:
          state = NBTParseState.Error;
          if (atValue)
          {
            SkipValue();
          }
          // Read next tag, check if we've hit the end
          if (canSeekStream)
          {
            TagStartOffset = (int)(reader.BaseStream.Position - streamStartOffset);
          }

          // set state to error in case reader.ReadTagType throws.
          NBTTagType = reader.ReadTagType();
          state = NBTParseState.InCompound;

          if (NBTTagType == NBTTagType.End)
          {
            TagName = null;
            TagsRead++;
            state = NBTParseState.AtCompoundEnd;
            if (SkipEndTags)
            {
              TagsRead--;
              goto case NBTParseState.AtCompoundEnd;
            }
            else
            {
              return true;
            }
          }
          else
          {
            ReadTagHeader(true);
            return true;
          }

        case NBTParseState.AtListBeginning:
          GoDown();
          ListIndex = -1;
          NBTTagType = ListType;
          state = NBTParseState.InList;
          goto case NBTParseState.InList;

        case NBTParseState.InList:
          state = NBTParseState.Error;
          if (atValue)
          {
            SkipValue();
          }
          ListIndex++;
          if (ListIndex >= ParentTagLength)
          {
            GoUp();
            if (ParentTagType == NBTTagType.List)
            {
              state = NBTParseState.InList;
              NBTTagType = NBTTagType.List;
              goto case NBTParseState.InList;
            }
            else if (ParentTagType == NBTTagType.Compound)
            {
              state = NBTParseState.InCompound;
              goto case NBTParseState.InCompound;
            }
            else
            {
              // This should not happen unless NBTReader is bugged
              throw new NBTFormatException(InvalidParentTagError);
            }
          }
          else
          {
            if (canSeekStream)
            {
              TagStartOffset = (int)(reader.BaseStream.Position - streamStartOffset);
            }
            state = NBTParseState.InList;
            ReadTagHeader(false);
          }
          return true;

        case NBTParseState.AtCompoundEnd:
          GoUp();
          if (ParentTagType == NBTTagType.List)
          {
            state = NBTParseState.InList;
            NBTTagType = NBTTagType.Compound;
            goto case NBTParseState.InList;
          }
          else if (ParentTagType == NBTTagType.Compound)
          {
            state = NBTParseState.InCompound;
            goto case NBTParseState.InCompound;
          }
          else if (ParentTagType == NBTTagType.Unknown)
          {
            state = NBTParseState.AtStreamEnd;
            return false;
          }
          else
          {
            // This should not happen unless NBTReader is bugged
            state = NBTParseState.Error;
            throw new NBTFormatException(InvalidParentTagError);
          }

        case NBTParseState.AtStreamEnd:
          // nothing left to read!
          return false;

        default:
          // Parsing error, or unexpected state.
          throw new InvalidReaderStateException(ErroneousStateError);
      }
    }


    void ReadTagHeader(bool readName)
    {
      // Setting state to error in case reader throws
      NBTParseState oldState = state;
      state = NBTParseState.Error;
      TagsRead++;
      TagName = (readName ? reader.ReadString() : null);

      valueCache = null;
      TagLength = 0;
      atValue = false;
      ListType = NBTTagType.Unknown;

      switch (NBTTagType)
      {
        case NBTTagType.Byte:
        case NBTTagType.Short:
        case NBTTagType.Int:
        case NBTTagType.Long:
        case NBTTagType.Float:
        case NBTTagType.Double:
        case NBTTagType.String:
          atValue = true;
          state = oldState;
          break;

        case NBTTagType.IntArray:
        case NBTTagType.ByteArray:
        case NBTTagType.LongArray:
          TagLength = reader.ReadInt32();
          if (TagLength < 0)
          {
            throw new NBTFormatException("Negative array length given: " + TagLength);
          }
          atValue = true;
          state = oldState;
          break;

        case NBTTagType.List:
          ListType = reader.ReadTagType();
          TagLength = reader.ReadInt32();
          if (TagLength < 0)
          {
            throw new NBTFormatException("Negative tag length given: " + TagLength);
          }
          state = NBTParseState.AtListBeginning;
          break;

        case NBTTagType.Compound:
          state = NBTParseState.AtCompoundBeginning;
          break;

        default:
          // This should not happen unless NBTBinaryReader is bugged
          throw new NBTFormatException("Trying to read tag of unknown type.");
      }
    }


    // Goes one step down the NBT file's hierarchy, preserving current state
    void GoDown()
    {
      if (nodes == null) nodes = new Stack<NBTReaderNode>();
      var newNode = new NBTReaderNode
      {
        ListIndex = ListIndex,
        ParentTagLength = ParentTagLength,
        ParentName = ParentName!,
        ParentTagType = ParentTagType,
        ListType = ListType
      };
      nodes.Push(newNode);

      ParentName = TagName;
      ParentTagType = NBTTagType;
      ParentTagLength = TagLength;
      ListIndex = 0;
      TagLength = 0;

      Depth++;
    }


    // Goes one step up the NBT file's hierarchy, restoring previous state
    void GoUp()
    {
      NBTReaderNode oldNode = nodes!.Pop();

      ParentName = oldNode.ParentName;
      ParentTagType = oldNode.ParentTagType;
      ParentTagLength = oldNode.ParentTagLength;
      ListIndex = oldNode.ListIndex;
      ListType = oldNode.ListType;
      TagLength = 0;

      Depth--;
    }


    void SkipValue()
    {
      // Make sure to check for "atValue" before calling this method
      switch (NBTTagType)
      {
        case NBTTagType.Byte:
          reader.ReadByte();
          break;

        case NBTTagType.Short:
          reader.ReadInt16();
          break;

        case NBTTagType.Float:
        case NBTTagType.Int:
          reader.ReadInt32();
          break;

        case NBTTagType.Double:
        case NBTTagType.Long:
          reader.ReadInt64();
          break;

        case NBTTagType.ByteArray:
          reader.Skip(TagLength);
          break;

        case NBTTagType.IntArray:
          reader.Skip(sizeof(int) * TagLength);
          break;

        case NBTTagType.LongArray:
          reader.Skip(sizeof(long) * TagLength);
          break;

        case NBTTagType.String:
          reader.SkipString();
          break;

        default:
          throw new InvalidOperationException(NonValueTagError);
      }
      atValue = false;
      valueCache = null;
    }


    /// <summary> Reads until a tag with the specified name is found. 
    /// Returns false if are no more tags to read (end of stream is reached). </summary>
    /// <param name="tagName"> Name of the tag. May be null (to look for next unnamed tag). </param>
    /// <returns> <c>true</c> if a matching tag is found; otherwise <c>false</c>. </returns>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidOperationException"> If NBTReader cannot recover from a previous parsing error. </exception>
    public bool ReadToFollowing(string tagName)
    {
      while (ReadToFollowing())
      {
        if (TagName == tagName)
        {
          return true;
        }
      }
      return false;
    }


    /// <summary> Advances the NBTReader to the next descendant tag with the specified name.
    /// If a matching child tag is not found, the NBTReader is positioned on the end tag. </summary>
    /// <param name="tagName"> Name of the tag you wish to move to. May be null (to look for next unnamed tag). </param>
    /// <returns> <c>true</c> if a matching descendant tag is found; otherwise <c>false</c>. </returns>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidReaderStateException"> If NBTReader cannot recover from a previous parsing error. </exception>
    public bool ReadToDescendant(string tagName)
    {
      if (state == NBTParseState.Error)
      {
        throw new InvalidReaderStateException(ErroneousStateError);
      }
      else if (state == NBTParseState.AtStreamEnd)
      {
        return false;
      }
      int currentDepth = Depth;
      while (ReadToFollowing())
      {
        if (Depth <= currentDepth)
        {
          return false;
        }
        else if (TagName == tagName)
        {
          return true;
        }
      }
      return false;
    }


    /// <summary> Advances the NBTReader to the next sibling tag, skipping any child tags.
    /// If there are no more siblings, NBTReader is positioned on the tag following the last of this tag's descendants. </summary>
    /// <returns> <c>true</c> if a sibling element is found; otherwise <c>false</c>. </returns>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidReaderStateException"> If NBTReader cannot recover from a previous parsing error. </exception>
    public bool ReadToNextSibling()
    {
      if (state == NBTParseState.Error)
      {
        throw new InvalidReaderStateException(ErroneousStateError);
      }
      else if (state == NBTParseState.AtStreamEnd)
      {
        return false;
      }
      int currentDepth = Depth;
      while (ReadToFollowing())
      {
        if (Depth == currentDepth)
        {
          return true;
        }
        else if (Depth < currentDepth)
        {
          return false;
        }
      }
      return false;
    }


    /// <summary> Advances the NBTReader to the next sibling tag with the specified name.
    /// If a matching sibling tag is not found, NBTReader is positioned on the tag following the last siblings. </summary>
    /// <param name="tagName"> The name of the sibling tag you wish to move to. </param>
    /// <returns> <c>true</c> if a matching sibling element is found; otherwise <c>false</c>. </returns>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidOperationException"> If NBTReader cannot recover from a previous parsing error. </exception>
    public bool ReadToNextSibling(string tagName)
    {
      while (ReadToNextSibling())
      {
        if (TagName == tagName)
        {
          return true;
        }
      }
      return false;
    }


    /// <summary> Skips current tag, its value/descendants, and any following siblings.
    /// In other words, reads until parent tag's sibling. </summary>
    /// <returns> Total number of tags that were skipped. Returns 0 if end of the stream is reached. </returns>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidReaderStateException"> If NBTReader cannot recover from a previous parsing error. </exception>
    public int Skip()
    {
      if (state == NBTParseState.Error)
      {
        throw new InvalidReaderStateException(ErroneousStateError);
      }
      else if (state == NBTParseState.AtStreamEnd)
      {
        return 0;
      }
      int startDepth = Depth;
      int skipped = 0;
      // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
      while (ReadToFollowing() && Depth >= startDepth)
      {
        skipped++;
      }
      return skipped;
    }


    /// <summary> Reads the entirety of the current tag, including any descendants,
    /// and constructs an NBTTag object of the appropriate type. </summary>
    /// <returns> Constructed NBTTag object;
    /// <c>null</c> if <c>SkipEndTags</c> is <c>true</c> and trying to read an End tag. </returns>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidReaderStateException"> If NBTReader cannot recover from a previous parsing error. </exception>
    /// <exception cref="EndOfStreamException"> End of stream has been reached (no more tags can be read). </exception>
    /// <exception cref="InvalidOperationException"> NBTTag value has already been read, and CacheTagValues is false. </exception>

    public NBTTag ReadAsTag()
    {
      switch (state)
      {
        case NBTParseState.Error:
          throw new InvalidReaderStateException(ErroneousStateError);

        case NBTParseState.AtStreamEnd:
          throw new EndOfStreamException();

        case NBTParseState.AtStreamBeginning:
        case NBTParseState.AtCompoundEnd:
          ReadToFollowing();
          break;
      }

      // get this tag
      NBTTag parent;
      if (NBTTagType == NBTTagType.Compound)
      {
        parent = new CompoundTag(TagName!);
      }
      else if (NBTTagType == NBTTagType.List)
      {
        parent = new ListTag(TagName!, ListType);
      }
      else if (atValue)
      {
        NBTTag result = ReadValueAsTag();
        ReadToFollowing();
        // if we're at a value tag, there are no child tags to read
        return result;
      }
      else
      {
        // end tags cannot be read-as-tags (there is no corresponding NBTTag object)
        throw new InvalidOperationException(NoValueToReadError);
      }

      int startingDepth = Depth;
      int parentDepth = Depth;

      do
      {
        ReadToFollowing();
        // Going up the file tree, or end of document: wrap up
        while (Depth <= parentDepth && parent.Parent != null)
        {
          parent = parent.Parent;
          parentDepth--;
        }
        if (Depth <= startingDepth) break;

        NBTTag thisTag;
        if (NBTTagType == NBTTagType.Compound)
        {
          thisTag = new CompoundTag(TagName!);
          AddToParent(thisTag, parent);
          parent = thisTag;
          parentDepth = Depth;
        }
        else if (NBTTagType == NBTTagType.List)
        {
          thisTag = new ListTag(TagName!, ListType);
          AddToParent(thisTag, parent);
          parent = thisTag;
          parentDepth = Depth;
        }
        else if (NBTTagType != NBTTagType.End)
        {
          thisTag = ReadValueAsTag();
          AddToParent(thisTag, parent);
        }
      } while (true);

      return parent;
    }


    void AddToParent(NBTTag thisTag, NBTTag parent)
    {
      if (parent is ListTag parentAsList)
      {
        parentAsList.Add(thisTag);
      }
      else if (parent is CompoundTag parentAsCompound)
      {
        parentAsCompound.Add(thisTag);
      }
      else
      {
        // cannot happen unless NBTReader is bugged
        throw new NBTFormatException(InvalidParentTagError);
      }
    }



    NBTTag ReadValueAsTag()
    {
      if (!atValue)
      {
        // Should never happen
        throw new InvalidOperationException(NoValueToReadError);
      }
      atValue = false;
      switch (NBTTagType)
      {
        case NBTTagType.Byte:
          return new ByteTag(TagName, reader.ReadByte());

        case NBTTagType.Short:
          return new ShortTag(TagName, reader.ReadInt16());

        case NBTTagType.Int:
          return new IntTag(TagName, reader.ReadInt32());

        case NBTTagType.Long:
          return new LongTag(TagName, reader.ReadInt64());

        case NBTTagType.Float:
          return new FloatTag(TagName, reader.ReadSingle());

        case NBTTagType.Double:
          return new DoubleTag(TagName, reader.ReadDouble());

        case NBTTagType.String:
          return new StringTag(TagName, reader.ReadString());

        case NBTTagType.ByteArray:
          byte[] value = reader.ReadBytes(TagLength);
          if (value.Length < TagLength)
          {
            throw new EndOfStreamException();
          }
          return new ByteArrayTag(TagName, value);

        case NBTTagType.IntArray:
          var ints = new int[TagLength];
          for (int i = 0; i < TagLength; i++)
          {
            ints[i] = reader.ReadInt32();
          }
          return new IntArrayTag(TagName, ints);

        case NBTTagType.LongArray:
          var longs = new long[TagLength];
          for (int i = 0; i < TagLength; i++)
          {
            longs[i] = reader.ReadInt64();
          }

          return new LongArrayTag(TagName, longs);

        default:
          throw new InvalidOperationException(NonValueTagError);
      }
    }


    /// <summary> Reads the value as an object of the type specified. </summary>
    /// <typeparam name="T"> The type of the value to be returned.
    /// NBTTag value should be convertible to this type. </typeparam>
    /// <returns> NBTTag value converted to the requested type. </returns>
    /// <exception cref="EndOfStreamException"> End of stream has been reached (no more tags can be read). </exception>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidOperationException"> Value has already been read, or there is no value to read. </exception>
    /// <exception cref="InvalidReaderStateException"> If NBTReader cannot recover from a previous parsing error. </exception>
    /// <exception cref="InvalidCastException"> NBTTag value cannot be converted to the requested type. </exception>
    public T ReadValueAs<T>()
    {
      return (T)ReadValue();
    }


    /// <summary> Reads the value as an object of the correct type, boxed.
    /// Cannot be called for tags that do not have a single-object value (compound, list, and end tags). </summary>
    /// <returns> NBTTag value converted to the requested type. </returns>
    /// <exception cref="EndOfStreamException"> End of stream has been reached (no more tags can be read). </exception>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>
    /// <exception cref="InvalidOperationException"> Value has already been read, or there is no value to read. </exception>
    /// <exception cref="InvalidReaderStateException"> If NBTReader cannot recover from a previous parsing error. </exception>

    public object ReadValue()
    {
      if (state == NBTParseState.AtStreamEnd)
      {
        throw new EndOfStreamException();
      }
      if (!atValue)
      {
        if (cacheTagValues)
        {
          if (valueCache == null)
          {
            throw new InvalidOperationException("No value to read.");
          }
          else
          {
            return valueCache;
          }
        }
        else
        {
          throw new InvalidOperationException(NoValueToReadError);
        }
      }
      valueCache = null;
      atValue = false;
      object value;
      switch (NBTTagType)
      {
        case NBTTagType.Byte:
          value = reader.ReadByte();
          break;

        case NBTTagType.Short:
          value = reader.ReadInt16();
          break;

        case NBTTagType.Float:
          value = reader.ReadSingle();
          break;

        case NBTTagType.Int:
          value = reader.ReadInt32();
          break;

        case NBTTagType.Double:
          value = reader.ReadDouble();
          break;

        case NBTTagType.Long:
          value = reader.ReadInt64();
          break;

        case NBTTagType.ByteArray:
          byte[] valueArr = reader.ReadBytes(TagLength);
          if (valueArr.Length < TagLength)
          {
            throw new EndOfStreamException();
          }
          value = valueArr;
          break;

        case NBTTagType.IntArray:
          var intValue = new int[TagLength];
          for (int i = 0; i < TagLength; i++)
          {
            intValue[i] = reader.ReadInt32();
          }
          value = intValue;
          break;

        case NBTTagType.LongArray:
          var longValue = new long[TagLength];
          for (int i = 0; i < TagLength; i++)
          {
            longValue[i] = reader.ReadInt64();
          }

          value = longValue;
          break;

        case NBTTagType.String:
          value = reader.ReadString();
          break;

        default:
          throw new InvalidOperationException(NonValueTagError);
      }
      valueCache = cacheTagValues ? value : null;
      return value;
    }


    /// <summary> If the current tag is a List, reads all elements of this list as an array.
    /// If any tags/values have already been read from this list, only reads the remaining unread tags/values.
    /// ListType must be a value type (byte, short, int, long, float, double, or string).
    /// Stops reading after the last list element. </summary>
    /// <typeparam name="T"> Element type of the array to be returned.
    /// NBTTag contents should be convertible to this type. </typeparam>
    /// <returns> List contents converted to an array of the requested type. </returns>
    /// <exception cref="EndOfStreamException"> End of stream has been reached (no more tags can be read). </exception>
    /// <exception cref="InvalidOperationException"> Current tag is not of type List. </exception>
    /// <exception cref="InvalidReaderStateException"> If NBTReader cannot recover from a previous parsing error. </exception>
    /// <exception cref="NBTFormatException"> If an error occurred while parsing data in NBT format. </exception>

    public T[] ReadListAsArray<T>()
    {
      switch (state)
      {
        case NBTParseState.AtStreamEnd:
          throw new EndOfStreamException();
        case NBTParseState.Error:
          throw new InvalidReaderStateException(ErroneousStateError);
        case NBTParseState.AtListBeginning:
          GoDown();
          ListIndex = 0;
          NBTTagType = ListType;
          state = NBTParseState.InList;
          break;
        case NBTParseState.InList:
          break;
        default:
          throw new InvalidOperationException("ReadListAsArray may only be used on List tags.");
      }

      int elementsToRead = ParentTagLength - ListIndex;

      // special handling for reading byte arrays (as byte arrays)
      if (ListType == NBTTagType.Byte && typeof(T) == typeof(byte))
      {
        TagsRead += elementsToRead;
        ListIndex = ParentTagLength - 1;
        T[] val = (T[])(object)reader.ReadBytes(elementsToRead);
        if (val.Length < elementsToRead)
        {
          throw new EndOfStreamException();
        }
        return val;
      }

      // for everything else, gotta read elements one-by-one
      var result = new T[elementsToRead];
      switch (ListType)
      {
        case NBTTagType.Byte:
          for (int i = 0; i < elementsToRead; i++)
          {
            result[i] = (T)Convert.ChangeType(reader.ReadByte(), typeof(T));
          }
          break;

        case NBTTagType.Short:
          for (int i = 0; i < elementsToRead; i++)
          {
            result[i] = (T)Convert.ChangeType(reader.ReadInt16(), typeof(T));
          }
          break;

        case NBTTagType.Int:
          for (int i = 0; i < elementsToRead; i++)
          {
            result[i] = (T)Convert.ChangeType(reader.ReadInt32(), typeof(T));
          }
          break;

        case NBTTagType.Long:
          for (int i = 0; i < elementsToRead; i++)
          {
            result[i] = (T)Convert.ChangeType(reader.ReadInt64(), typeof(T));
          }
          break;

        case NBTTagType.Float:
          for (int i = 0; i < elementsToRead; i++)
          {
            result[i] = (T)Convert.ChangeType(reader.ReadSingle(), typeof(T));
          }
          break;

        case NBTTagType.Double:
          for (int i = 0; i < elementsToRead; i++)
          {
            result[i] = (T)Convert.ChangeType(reader.ReadDouble(), typeof(T));
          }
          break;

        case NBTTagType.String:
          for (int i = 0; i < elementsToRead; i++)
          {
            result[i] = (T)Convert.ChangeType(reader.ReadString(), typeof(T));
          }
          break;

        default:
          throw new InvalidOperationException("ReadListAsArray may only be used on lists of value types.");
      }
      TagsRead += elementsToRead;
      ListIndex = ParentTagLength - 1;
      return result;
    }


    /// <summary> Parsing option: Whether NBTReader should skip End tags in ReadToFollowing() automatically while parsing.
    /// Default is <c>true</c>. </summary>
    public bool SkipEndTags { get; set; }

    /// <summary> Parsing option: Whether NBTReader should save a copy of the most recently read tag's value.
    /// Unless CacheTagValues is <c>true</c>, tag values can only be read once. Default is <c>false</c>. </summary>
    public bool CacheTagValues
    {
      get { return cacheTagValues; }
      set
      {
        cacheTagValues = value;
        if (!cacheTagValues)
        {
          valueCache = null;
        }
      }
    }

    bool cacheTagValues;


    /// <summary> Returns a String that represents the tag currently being read by this NBTReader instance.
    /// Prints current tag's depth, ordinal number, type, name, and size (for arrays and lists). Does not print value.
    /// Indents the tag according default indentation (NBTTag.DefaultIndentString). </summary>
    public override string ToString()
    {
      return ToString(false, NBTTag.DefaultIndentString);
    }


    /// <summary> Returns a String that represents the tag currently being read by this NBTReader instance.
    /// Prints current tag's depth, ordinal number, type, name, size (for arrays and lists), and optionally value.
    /// Indents the tag according default indentation (NBTTag.DefaultIndentString). </summary>
    /// <param name="includeValue"> If set to <c>true</c>, also reads and prints the current tag's value. 
    /// Note that unless CacheTagValues is set to <c>true</c>, you can only read every tag's value ONCE. </param>

    public string ToString(bool includeValue)
    {
      return ToString(includeValue, NBTTag.DefaultIndentString);
    }


    /// <summary> Returns a String that represents the current NBTReader object.
    /// Prints current tag's depth, ordinal number, type, name, size (for arrays and lists), and optionally value. </summary>
    /// <param name="indentString"> String to be used for indentation. May be empty string, but may not be <c>null</c>. </param>
    /// <param name="includeValue"> If set to <c>true</c>, also reads and prints the current tag's value. </param>

    public string ToString(bool includeValue, string indentString)
    {
      if (indentString == null) throw new ArgumentNullException(nameof(indentString));
      var sb = new StringBuilder();
      for (int i = 0; i < Depth; i++)
      {
        sb.Append(indentString);
      }
      sb.Append('#').Append(TagsRead).Append(". ").Append(NBTTagType);
      if (IsList)
      {
        sb.Append('<').Append(ListType).Append('>');
      }
      if (HasLength)
      {
        sb.Append('[').Append(TagLength).Append(']');
      }
      sb.Append(' ').Append(TagName);
      if (includeValue && (atValue || HasValue && cacheTagValues) && NBTTagType != NBTTagType.IntArray &&
          NBTTagType != NBTTagType.ByteArray && NBTTagType != NBTTagType.LongArray)
      {
        sb.Append(" = ").Append(ReadValue());
      }
      return sb.ToString();
    }


    const string NoValueToReadError = "Value already read, or no value to read.",
        NonValueTagError = "Trying to read value of a non-value tag.",
        InvalidParentTagError = "Parent tag is neither a Compound nor a List.",
        ErroneousStateError = "NBTReader is in an erroneous state!";
  }
}