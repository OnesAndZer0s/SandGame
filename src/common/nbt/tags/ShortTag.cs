using System;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing a single signed 16-bit integer. </summary>
  public sealed class ShortTag : NBTTag
  {
    /// <summary> Type of this tag (Short). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.Short; }
    }

    /// <summary> Value/payload of this tag (a single signed 16-bit integer). </summary>
    public short Value { get; set; }


    /// <summary> Creates an unnamed Short tag with the default value of 0. </summary>
    public ShortTag() { }


    /// <summary> Creates an unnamed Short tag with the given value. </summary>
    /// <param name="value"> Value to assign to this tag. </param>
    public ShortTag(short value)
        : this(null, value) { }


    /// <summary> Creates an Short tag with the given name and the default value of 0. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    public ShortTag(string tagName)
        : this(tagName, 0) { }


    /// <summary> Creates an Short tag with the given name and value. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="value"> Value to assign to this tag. </param>
    public ShortTag(string? tagName, short value)
    {
      name = tagName;
      Value = value;
    }


    /// <summary> Creates a copy of given Short tag. </summary>
    /// <param name="other"> NBTTag to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    public ShortTag(ShortTag other)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      name = other.name;
      Value = other.Value;
    }


    #region Reading / Writing

    internal override bool ReadTag(NBTBinaryReader readStream)
    {
      if (readStream.Selector != null && !readStream.Selector(this))
      {
        readStream.ReadInt16();
        return false;
      }
      Value = readStream.ReadInt16();
      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      readStream.ReadInt16();
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.Short);
      if (Name == null) throw new FormatException("Name is null");
      writeStream.Write(Name);
      writeStream.Write(Value);
    }


    internal override void WriteData(NBTBinaryWriter writeStream)
    {
      writeStream.Write(Value);
    }

    #endregion


    /// <inheritdoc />
    public override object Clone()
    {
      return new ShortTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_Short");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.Append(": ");
      sb.Append(Value);
    }
  }
}