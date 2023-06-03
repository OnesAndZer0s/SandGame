using System;
using System.IO;
using System.Text;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing an array of bytes. </summary>
  public sealed class ByteArrayTag : NBTTag
  {
    /// <summary> Type of this tag (ByteArray). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.ByteArray; }
    }

    /// <summary> Value/payload of this tag (an array of bytes). Value is stored as-is and is NOT cloned. May not be <c>null</c>. </summary>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>

    public byte[] Value
    {
      get { return bytes; }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        bytes = value;
      }
    }


    byte[] bytes;


    /// <summary> Creates an unnamed Byte tag, containing an empty array of bytes. </summary>
    public ByteArrayTag()
        : this((string)null!) { }


    /// <summary> Creates an unnamed Byte tag, containing the given array of bytes. </summary>
    /// <param name="value"> Byte array to assign to this tag's Value. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    /// <remarks> Given byte array will be cloned. To avoid unnecessary copying, call one of the other constructor
    /// overloads (that do not take a byte[]) and then set the Value property yourself. </remarks>
    public ByteArrayTag(byte[] value)
        : this(null, value) { }


    /// <summary> Creates an Byte tag with the given name, containing an empty array of bytes. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public ByteArrayTag(string tagName)
    {
      name = tagName;
      bytes = Array.Empty<byte>();
    }


    /// <summary> Creates an Byte tag with the given name, containing the given array of bytes. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="value"> Byte array to assign to this tag's Value. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    /// <remarks> Given byte array will be cloned. To avoid unnecessary copying, call one of the other constructor
    /// overloads (that do not take a byte[]) and then set the Value property yourself. </remarks>
    public ByteArrayTag(string? tagName, byte[] value)
    {
      if (value == null) throw new ArgumentNullException(nameof(value));
      name = tagName;
      bytes = (byte[])value.Clone();
    }


    /// <summary> Creates a deep copy of given ByteArray. </summary>
    /// <param name="other"> NBTTag to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    /// <remarks> Byte array of given tag will be cloned. </remarks>
    public ByteArrayTag(ByteArrayTag other)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      name = other.name;
      bytes = (byte[])other.Value.Clone();
    }


    /// <summary> Gets or sets a byte at the given index. </summary>
    /// <param name="tagIndex"> The zero-based index of the element to get or set. </param>
    /// <returns> The byte at the specified index. </returns>
    /// <exception cref="IndexOutOfRangeException"> <paramref name="tagIndex"/> is outside the array bounds. </exception>
    public new byte this[int tagIndex]
    {
      get { return Value[tagIndex]; }
      set { Value[tagIndex] = value; }
    }


    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      int length = readStream.ReadInt32();
      if (length < 0)
      {
        throw new FormatException("Negative length given in TAG_Byte_Array");
      }

      if (readStream.Selector != null && !readStream.Selector(this))
      {
        readStream.Skip(length);
        return false;
      }
      Value = readStream.ReadBytes(length);
      if (Value.Length < length)
      {
        throw new EndOfStreamException();
      }
      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      int length = readStream.ReadInt32();
      if (length < 0)
      {
        throw new FormatException("Negative length given in TAG_Byte_Array");
      }
      readStream.Skip(length);
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.ByteArray);
      if (Name == null) throw new FormatException("Name is null");
      writeStream.Write(Name);
      WriteData(writeStream);
    }


    internal override void WriteData(NBTBinaryWriter writeStream)
    {
      writeStream.Write(Value.Length);
      writeStream.Write(Value, 0, Value.Length);
    }


    /// <inheritdoc />
    public override object Clone()
    {
      return new ByteArrayTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_Byte_Array");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.AppendFormat(": [{0} bytes]", bytes.Length);
    }
  }
}