using System.IO;
using System;
using System.Globalization;
using System.Text;


namespace Sandbox.Common.NBT
{
  /// <summary> Base class for different kinds of named binary tags. </summary>
  public abstract class NBTTag : ICloneable
  {
    /// <summary> Parent compound tag, either List or Compound, if any.
    /// May be <c>null</c> for detached tags. </summary>
    public NBTTag? Parent { get; internal set; }

    /// <summary> Type of this tag. </summary>
    public abstract NBTTagType NBTTagType { get; }

    /// <summary> Returns true if tags of this type have a value attached.
    /// All tags except Compound, List, and End have values. </summary>
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

    /// <summary> Name of this tag. Immutable, and set by the constructor. May be <c>null</c>. </summary>
    /// <exception cref="ArgumentNullException"> If <paramref name="value"/> is <c>null</c>, and <c>Parent</c> tag is an Compound.
    /// Name of tags inside an <c>Compound</c> may not be null. </exception>
    /// <exception cref="ArgumentException"> If this tag resides in an <c>Compound</c>, and a sibling tag with the name already exists. </exception>
    public string? Name
    {
      get { return name; }
      set
      {
        if (name == value)
        {
          return;
        }

        if (Parent is CompoundTag parentAsCompound)
        {
          if (value == null)
          {
            throw new ArgumentNullException(nameof(value),
                                            "Name of tags inside an Compound may not be null.");
          }
          else if (name != null)
          {
            parentAsCompound.RenameTag(name, value);
          }
        }

        name = value;
      }
    }

    // Used by impls to bypass setter checks (and avoid side effects) when initializing state
    internal string? name;

    /// <summary> Gets the full name of this tag, including all parent tag names, separated by dots. 
    /// Unnamed tags show up as empty strings. </summary>
    public string Path
    {
      get
      {
        if (Parent == null)
        {
          return Name ?? "";
        }
        if (Parent is ListTag parentAsList)
        {
          return parentAsList.Path + '[' + parentAsList.IndexOf(this) + ']';
        }
        else
        {
          return Parent.Path + '.' + Name;
        }
      }
    }

    internal abstract bool ReadTag(NBTBinaryReader readStream);

    internal abstract void SkipTag(NBTBinaryReader readStream);

    internal abstract void WriteTag(NBTBinaryWriter writeReader);

    // WriteData does not write the tag's ID byte or the name
    internal abstract void WriteData(NBTBinaryWriter writeStream);


    #region Shortcuts

    /// <summary> Gets or sets the tag with the specified name. May return <c>null</c>. </summary>
    /// <returns> The tag with the specified key. Null if tag with the given name was not found. </returns>
    /// <param name="tagName"> The name of the tag to get or set. Must match tag's actual name. </param>
    /// <exception cref="InvalidOperationException"> If used on a tag that is not Compound. </exception>
    /// <remarks> ONLY APPLICABLE TO Compound OBJECTS!
    /// Included in NBTTag base class for programmers' convenience, to avoid extra type casts. </remarks>
    public virtual NBTTag this[string tagName]
    {
      get { throw new InvalidOperationException("String indexers only work on Compound tags."); }
      set { throw new InvalidOperationException("String indexers only work on Compound tags."); }
    }

    /// <summary> Gets or sets the tag at the specified index. </summary>
    /// <returns> The tag at the specified index. </returns>
    /// <param name="tagIndex"> The zero-based index of the tag to get or set. </param>
    /// <exception cref="ArgumentOutOfRangeException"> tagIndex is not a valid index in this tag. </exception>
    /// <exception cref="ArgumentNullException"> Given tag is <c>null</c>. </exception>
    /// <exception cref="ArgumentException"> Given tag's type does not match ListType. </exception>
    /// <exception cref="InvalidOperationException"> If used on a tag that is not List, ByteArray, or IntArray. </exception>
    /// <remarks> ONLY APPLICABLE TO List, ByteArray, and IntArray OBJECTS!
    /// Included in NBTTag base class for programmers' convenience, to avoid extra type casts. </remarks>
    public virtual NBTTag this[int tagIndex]
    {
      get { throw new InvalidOperationException("Integer indexers only work on List tags."); }
      set { throw new InvalidOperationException("Integer indexers only work on List tags."); }
    }

    /// <summary> Returns the value of this tag, cast as a byte.
    /// Only supported by Byte tags. </summary>
    /// <exception cref="InvalidCastException"> When used on a tag other than Byte. </exception>
    public byte ByteValue
    {
      get
      {
        if (NBTTagType == NBTTagType.Byte)
        {
          return ((ByteTag)this).Value;
        }
        else
        {
          throw new InvalidCastException("Cannot get ByteValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as a short (16-bit signed integer).
    /// Only supported by Byte and Short. </summary>
    /// <exception cref="InvalidCastException"> When used on an unsupported tag. </exception>
    public short ShortValue
    {
      get
      {
        switch (NBTTagType)
        {
          case NBTTagType.Byte:
            return ((ByteTag)this).Value;
          case NBTTagType.Short:
            return ((ShortTag)this).Value;
          default:
            throw new InvalidCastException("Cannot get ShortValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as an int (32-bit signed integer).
    /// Only supported by Byte, Short, and Int. </summary>
    /// <exception cref="InvalidCastException"> When used on an unsupported tag. </exception>
    public int IntValue
    {
      get
      {
        switch (NBTTagType)
        {
          case NBTTagType.Byte:
            return ((ByteTag)this).Value;
          case NBTTagType.Short:
            return ((ShortTag)this).Value;
          case NBTTagType.Int:
            return ((IntTag)this).Value;
          default:
            throw new InvalidCastException("Cannot get IntValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as a long (64-bit signed integer).
    /// Only supported by Byte, Short, Int, and Long. </summary>
    /// <exception cref="InvalidCastException"> When used on an unsupported tag. </exception>
    public long LongValue
    {
      get
      {
        switch (NBTTagType)
        {
          case NBTTagType.Byte:
            return ((ByteTag)this).Value;
          case NBTTagType.Short:
            return ((ShortTag)this).Value;
          case NBTTagType.Int:
            return ((IntTag)this).Value;
          case NBTTagType.Long:
            return ((LongTag)this).Value;
          default:
            throw new InvalidCastException("Cannot get LongValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as a long (64-bit signed integer).
    /// Only supported by Float and, with loss of precision, by Double, Byte, Short, Int, and Long. </summary>
    /// <exception cref="InvalidCastException"> When used on an unsupported tag. </exception>
    public float FloatValue
    {
      get
      {
        switch (NBTTagType)
        {
          case NBTTagType.Byte:
            return ((ByteTag)this).Value;
          case NBTTagType.Short:
            return ((ShortTag)this).Value;
          case NBTTagType.Int:
            return ((IntTag)this).Value;
          case NBTTagType.Long:
            return ((LongTag)this).Value;
          case NBTTagType.Float:
            return ((FloatTag)this).Value;
          case NBTTagType.Double:
            return (float)((DoubleTag)this).Value;
          default:
            throw new InvalidCastException("Cannot get FloatValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as a long (64-bit signed integer).
    /// Only supported by Float, Double, and, with loss of precision, by Byte, Short, Int, and Long. </summary>
    /// <exception cref="InvalidCastException"> When used on an unsupported tag. </exception>
    public double DoubleValue
    {
      get
      {
        switch (NBTTagType)
        {
          case NBTTagType.Byte:
            return ((ByteTag)this).Value;
          case NBTTagType.Short:
            return ((ShortTag)this).Value;
          case NBTTagType.Int:
            return ((IntTag)this).Value;
          case NBTTagType.Long:
            return ((LongTag)this).Value;
          case NBTTagType.Float:
            return ((FloatTag)this).Value;
          case NBTTagType.Double:
            return ((DoubleTag)this).Value;
          default:
            throw new InvalidCastException("Cannot get DoubleValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as a byte array.
    /// Only supported by ByteArray tags. </summary>
    /// <exception cref="InvalidCastException"> When used on a tag other than ByteArray. </exception>
    public byte[] ByteArrayValue
    {
      get
      {
        if (NBTTagType == NBTTagType.ByteArray)
        {
          return ((ByteArrayTag)this).Value;
        }
        else
        {
          throw new InvalidCastException("Cannot get ByteArrayValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as an int array.
    /// Only supported by IntArray tags. </summary>
    /// <exception cref="InvalidCastException"> When used on a tag other than IntArray. </exception>
    public int[] IntArrayValue
    {
      get
      {
        if (NBTTagType == NBTTagType.IntArray)
        {
          return ((IntArrayTag)this).Value;
        }
        else
        {
          throw new InvalidCastException("Cannot get IntArrayValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as a long array.
    /// Only supported by LongArray tags. </summary>
    /// <exception cref="InvalidCastException"> When used on a tag other than LongArray. </exception>
    public long[] LongArrayValue
    {
      get
      {
        if (NBTTagType == NBTTagType.LongArray)
        {
          return ((LongArrayTag)this).Value;
        }
        else
        {
          throw new InvalidCastException("Cannot get LongArrayValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    /// <summary> Returns the value of this tag, cast as a string.
    /// Returns exact value for String, and stringified (using InvariantCulture) value for Byte, Double, Float, Int, Long, and Short.
    /// Not supported by Compound, List, ByteArray, or IntArray. </summary>
    /// <exception cref="InvalidCastException"> When used on an unsupported tag. </exception>
    public string StringValue
    {
      get
      {
        switch (NBTTagType)
        {
          case NBTTagType.String:
            return ((StringTag)this).Value;
          case NBTTagType.Byte:
            return ((ByteTag)this).Value.ToString(CultureInfo.InvariantCulture);
          case NBTTagType.Double:
            return ((DoubleTag)this).Value.ToString(CultureInfo.InvariantCulture);
          case NBTTagType.Float:
            return ((FloatTag)this).Value.ToString(CultureInfo.InvariantCulture);
          case NBTTagType.Int:
            return ((IntTag)this).Value.ToString(CultureInfo.InvariantCulture);
          case NBTTagType.Long:
            return ((LongTag)this).Value.ToString(CultureInfo.InvariantCulture);
          case NBTTagType.Short:
            return ((ShortTag)this).Value.ToString(CultureInfo.InvariantCulture);
          default:
            throw new InvalidCastException("Cannot get StringValue from " + GetCanonicalTagName(NBTTagType));
        }
      }
    }

    #endregion


    /// <summary> Returns a canonical (Notchy) name for the given NBTTagType,
    /// e.g. "TAG_Byte_Array" for NBTTagType.ByteArray </summary>
    /// <param name="type"> NBTTagType to name. </param>
    /// <returns> String representing the canonical name of a tag,
    /// or null of given NBTTagType does not have a canonical name (e.g. Unknown). </returns>
    public static string? GetCanonicalTagName(NBTTagType type)
    {
      switch (type)
      {
        case NBTTagType.Byte:
          return "TAG_Byte";
        case NBTTagType.ByteArray:
          return "TAG_Byte_Array";
        case NBTTagType.Compound:
          return "TAG_Compound";
        case NBTTagType.Double:
          return "TAG_Double";
        case NBTTagType.End:
          return "TAG_End";
        case NBTTagType.Float:
          return "TAG_Float";
        case NBTTagType.Int:
          return "TAG_Int";
        case NBTTagType.IntArray:
          return "TAG_Int_Array";
        case NBTTagType.LongArray:
          return "TAG_Long_Array";
        case NBTTagType.List:
          return "TAG_List";
        case NBTTagType.Long:
          return "TAG_Long";
        case NBTTagType.Short:
          return "TAG_Short";
        case NBTTagType.String:
          return "TAG_String";
        default:
          return null;
      }
    }


    /// <summary> Prints contents of this tag, and any child tags, to a string.
    /// Indents the string using multiples of the given indentation string. </summary>
    /// <returns> A string representing contents of this tag, and all child tags (if any). </returns>
    public override string ToString()
    {
      return ToString(DefaultIndentString);
    }


    /// <summary> Creates a deep copy of this tag. </summary>
    /// <returns> A new NBTTag object that is a deep copy of this instance. </returns>
    public abstract object Clone();


    /// <summary> Prints contents of this tag, and any child tags, to a string.
    /// Indents the string using multiples of the given indentation string. </summary>
    /// <param name="indentString"> String to be used for indentation. </param>
    /// <returns> A string representing contents of this tag, and all child tags (if any). </returns>
    /// <exception cref="ArgumentNullException"> <paramref name="indentString"/> is <c>null</c>. </exception>
    public string ToString(string indentString)
    {
      if (indentString == null) throw new ArgumentNullException(nameof(indentString));
      var sb = new StringBuilder();
      PrettyPrint(sb, indentString, 0);
      return sb.ToString();
    }


    internal abstract void PrettyPrint(StringBuilder sb, string indentString, int indentLevel);

    /// <summary> String to use for indentation in NBTTag's and File's ToString() methods by default. </summary>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    public static string DefaultIndentString
    {
      get { return defaultIndentString; }
      set
      {
        if (value == null) throw new ArgumentNullException(nameof(value));
        defaultIndentString = value;
      }
    }

    static string defaultIndentString = "  ";
  }
}