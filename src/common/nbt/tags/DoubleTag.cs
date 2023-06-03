using System;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing a double-precision floating point number. </summary>
  public sealed class DoubleTag : NBTTag
  {
    /// <summary> Type of this tag (Double). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.Double; }
    }

    /// <summary> Value/payload of this tag (a double-precision floating point number). </summary>
    public double Value { get; set; }


    /// <summary> Creates an unnamed Double tag with the default value of 0. </summary>
    public DoubleTag() { }


    /// <summary> Creates an unnamed Double tag with the given value. </summary>
    /// <param name="value"> Value to assign to this tag. </param>
    public DoubleTag(double value)
        : this(null, value) { }


    /// <summary> Creates an Double tag with the given name and the default value of 0. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public DoubleTag(string tagName)
        : this(tagName, 0) { }


    /// <summary> Creates an Double tag with the given name and value. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="value"> Value to assign to this tag. </param>
    public DoubleTag(string? tagName, double value)
    {
      name = tagName;
      Value = value;
    }


    /// <summary> Creates a copy of given Double tag. </summary>
    /// <param name="other"> NBTTag to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    public DoubleTag(DoubleTag other)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      name = other.name;
      Value = other.Value;
    }


    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      if (readStream.Selector != null && !readStream.Selector(this))
      {
        readStream.ReadDouble();
        return false;
      }
      Value = readStream.ReadDouble();
      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      readStream.ReadDouble();
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.Double);
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
      return new DoubleTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_Double");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.Append(": ");
      sb.Append(Value);
    }
  }
}