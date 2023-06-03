using System;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing an array of signed 32-bit integers. </summary>
  public sealed class IntArrayTag : NBTTag
  {
    /// <summary> Type of this tag (ByteArray). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.IntArray; }
    }

    /// <summary> Value/payload of this tag (an array of signed 32-bit integers). Value is stored as-is and is NOT cloned. May not be <c>null</c>. </summary>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>

    public int[] Value
    {
      get { return ints; }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        ints = value;
      }
    }


    int[] ints;


    /// <summary> Creates an unnamed IntArray tag, containing an empty array of ints. </summary>
    public IntArrayTag()
        : this((string)null!) { }


    /// <summary> Creates an unnamed IntArray tag, containing the given array of ints. </summary>
    /// <param name="value"> Int array to assign to this tag's Value. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    /// <remarks> Given int array will be cloned. To avoid unnecessary copying, call one of the other constructor
    /// overloads (that do not take a int[]) and then set the Value property yourself. </remarks>
    public IntArrayTag(int[] value)
        : this(null, value) { }


    /// <summary> Creates an IntArray tag with the given name, containing an empty array of ints. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public IntArrayTag(string tagName)
    {
      name = tagName;
      ints = Array.Empty<int>();
    }


    /// <summary> Creates an IntArray tag with the given name, containing the given array of ints. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="value"> Int array to assign to this tag's Value. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    /// <remarks> Given int array will be cloned. To avoid unnecessary copying, call one of the other constructor
    /// overloads (that do not take a int[]) and then set the Value property yourself. </remarks>
    public IntArrayTag(string? tagName, int[] value)
    {
      if (value == null) throw new ArgumentNullException(nameof(value));
      name = tagName;
      ints = (int[])value.Clone();
    }


    /// <summary> Creates a deep copy of given IntArray. </summary>
    /// <param name="other"> NBTTag to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    /// <remarks> Int array of given tag will be cloned. </remarks>
    public IntArrayTag(IntArrayTag other)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      name = other.name;
      ints = (int[])other.Value.Clone();
    }


    /// <summary> Gets or sets an integer at the given index. </summary>
    /// <param name="tagIndex"> The zero-based index of the element to get or set. </param>
    /// <returns> The integer at the specified index. </returns>
    /// <exception cref="IndexOutOfRangeException"> <paramref name="tagIndex"/> is outside the array bounds. </exception>
    public new int this[int tagIndex]
    {
      get { return Value[tagIndex]; }
      set { Value[tagIndex] = value; }
    }


    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      int length = readStream.ReadInt32();
      if (length < 0)
      {
        throw new FormatException("Negative length given in TAG_Int_Array");
      }

      if (readStream.Selector != null && !readStream.Selector(this))
      {
        readStream.Skip(length * sizeof(int));
        return false;
      }

      Value = new int[length];
      for (int i = 0; i < length; i++)
      {
        Value[i] = readStream.ReadInt32();
      }
      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      int length = readStream.ReadInt32();
      if (length < 0)
      {
        throw new FormatException("Negative length given in TAG_Int_Array");
      }
      readStream.Skip(length * sizeof(int));
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.IntArray);
      if (Name == null) throw new FormatException("Name is null");
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
      return new IntArrayTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_Int_Array");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.AppendFormat(": [{0} ints]", ints.Length);
    }
  }
}