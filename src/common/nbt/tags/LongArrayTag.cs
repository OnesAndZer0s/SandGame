using System;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing an array of signed 64-bit integers. </summary>
  public sealed class LongArrayTag : NBTTag
  {
    /// <summary> Type of this tag (LongArray). </summary>
    public override NBTTagType NBTTagType
    {
      get
      {
        return NBTTagType.LongArray;
      }
    }

    /// <summary> Value/payload of this tag (an array of signed 64-bit integers). Value is stored as-is and is NOT cloned. May not be <c>null</c>. </summary>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>

    public long[] Value
    {
      get { return longs; }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException(nameof(value));
        }

        longs = value;
      }
    }


    private long[] longs;

    /// <summary> Creates an unnamed LongArray tag, containing an empty array of longs. </summary>
    public LongArrayTag()
        : this((string)null!) { }

    /// <summary> Creates an unnamed LongArray tag, containing the given array of longs. </summary>
    /// <param name="value"> Long array to assign to this tag's Value. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    /// <remarks> Given long array will be cloned. To avoid unnecessary copying, call one of the other constructor
    /// overloads (that do not take a long[]) and then set the Value property yourself. </remarks>
    public LongArrayTag(long[] value)
        : this(null, value) { }

    /// <summary> Creates an LongArray tag with the given name, containing an empty array of longs. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public LongArrayTag(string tagName)
    {
      name = tagName;
      longs = Array.Empty<long>();
    }

    /// <summary> Creates an LongArray tag with the given name, containing the given array of longs. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="value"> Long array to assign to this tag's Value. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    /// <remarks> Given long array will be cloned. To avoid unnecessary copying, call one of the other constructor
    /// overloads (that do not take a long[]) and then set the Value property yourself. </remarks>
    public LongArrayTag(string? tagName, long[] value)
    {
      if (value == null) throw new ArgumentNullException(nameof(value));
      name = tagName;
      longs = (long[])value.Clone();
    }


    /// <summary> Creates a deep copy of given LongArray. </summary>
    /// <param name="other"> NBTTag to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    /// <remarks> Long array of given tag will be cloned. </remarks>
    public LongArrayTag(LongArrayTag other)
    {
      if (other == null)
      {
        throw new ArgumentNullException(nameof(other));
      }

      name = other.name;
      longs = (long[])other.longs.Clone();
    }


    /// <summary> Gets or sets a long at the given index. </summary>
    /// <param name="index"> The zero-based index of the element to get or set. </param>
    /// <returns> The long at the specified index. </returns>
    /// <exception cref="IndexOutOfRangeException"> <paramref name="index"/> is outside the array bounds. </exception>
    public new long this[int index]
    {
      get { return Value[index]; }
      set { Value[index] = value; }
    }


    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      int length = readStream.ReadInt32();

      if (length < 0)
      {
        throw new FormatException("Negative length given in TAG_Long_Array");
      }

      if (readStream.Selector != null && !readStream.Selector(this))
      {
        readStream.Skip(length * sizeof(long));
        return false;
      }

      Value = new long[length];

      for (int i = 0; i < length; i++)
      {
        Value[i] = readStream.ReadInt64();
      }

      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      int length = readStream.ReadInt32();

      if (length < 0)
      {
        throw new FormatException("Negative length given in TAG_Long_Array");
      }

      readStream.Skip(length * sizeof(long));
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.LongArray);

      if (Name == null)
      {
        throw new FormatException("Name is null");
      }

      writeStream.Write(Name);
      WriteData(writeStream);
    }


    internal override void WriteData(NBTBinaryWriter writeStream)
    {
      writeStream.Write(Value.Length);

      for (int i = 0; i < Value.Length; i++)
      {
        writeStream.Write(Value[i]);
      }
    }


    /// <inheritdoc />
    public override object Clone()
    {
      return new LongArrayTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }

      sb.Append("TAG_Long_Array");

      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }

      sb.AppendFormat(": [{0} longs]", Value.Length);
    }
  }
}