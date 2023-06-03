using System;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing a single signed 32-bit integer. </summary>
  public sealed class IntTag : NBTTag
  {
    /// <summary> Type of this tag (Int). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.Int; }
    }

    /// <summary> Value/payload of this tag (a single signed 32-bit integer). </summary>
    public int Value { get; set; }


    /// <summary> Creates an unnamed Int tag with the default value of 0. </summary>
    public IntTag() { }


    /// <summary> Creates an unnamed Int tag with the given value. </summary>
    /// <param name="value"> Value to assign to this tag. </param>
    public IntTag(int value)
        : this(null, value) { }


    /// <summary> Creates an Int tag with the given name and the default value of 0. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public IntTag(string tagName)
        : this(tagName, 0) { }


    /// <summary> Creates an Int tag with the given name and value. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="value"> Value to assign to this tag. </param>
    public IntTag(string? tagName, int value)
    {
      name = tagName;
      Value = value;
    }


    /// <summary> Creates a copy of given Int tag. </summary>
    /// <param name="other"> NBTTag to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    public IntTag(IntTag other)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      name = other.name;
      Value = other.Value;
    }


    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      if (readStream.Selector != null && !readStream.Selector(this))
      {
        readStream.ReadInt32();
        return false;
      }
      Value = readStream.ReadInt32();
      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      readStream.ReadInt32();
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.Int);
      if (Name == null) throw new FormatException("Name is null");
      writeStream.Write(Name);
      writeStream.Write(Value);
    }


    internal override void WriteData(NBTBinaryWriter writeStream)
    {
      writeStream.Write(Value);
    }


    /// <inheritdoc />
    public override object Clone()
    {
      return new IntTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_Int");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.Append(": ");
      sb.Append(Value);
    }
  }
}