using System;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing a single-precision floating point number. </summary>
  public sealed class FloatTag : NBTTag
  {
    /// <summary> Type of this tag (Float). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.Float; }
    }

    /// <summary> Value/payload of this tag (a single-precision floating point number). </summary>
    public float Value { get; set; }


    /// <summary> Creates an unnamed Float tag with the default value of 0f. </summary>
    public FloatTag() { }


    /// <summary> Creates an unnamed Float tag with the given value. </summary>
    /// <param name="value"> Value to assign to this tag. </param>
    public FloatTag(float value)
        : this(null, value) { }


    /// <summary> Creates an Float tag with the given name and the default value of 0f. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public FloatTag(string tagName)
        : this(tagName, 0) { }


    /// <summary> Creates an Float tag with the given name and value. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="value"> Value to assign to this tag. </param>
    public FloatTag(string? tagName, float value)
    {
      name = tagName;
      Value = value;
    }


    /// <summary> Creates a copy of given Float tag. </summary>
    /// <param name="other"> NBTTag to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    public FloatTag(FloatTag other)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      name = other.name;
      Value = other.Value;
    }


    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      if (readStream.Selector != null && !readStream.Selector(this))
      {
        readStream.ReadSingle();
        return false;
      }
      Value = readStream.ReadSingle();
      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      readStream.ReadSingle();
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.Float);
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
      return new FloatTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_Float");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.Append(": ");
      sb.Append(Value);
    }
  }
}