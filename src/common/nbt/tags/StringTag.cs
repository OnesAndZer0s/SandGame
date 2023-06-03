using System;
using System.Text;
using System.IO;

namespace Sandbox.Common.NBT
{
  /// <summary> A tag containing a single string. String is stored in UTF-8 encoding. </summary>
  public sealed class StringTag : NBTTag
  {
    /// <summary> Type of this tag (String). </summary>
    public override NBTTagType NBTTagType
    {
      get { return NBTTagType.String; }
    }

    /// <summary> Value/payload of this tag (a single string). May not be <c>null</c>. </summary>

    public string Value
    {
      get { return stringVal; }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        stringVal = value;
      }
    }


    string stringVal = "";


    /// <summary> Creates an unnamed String tag with the default value (empty string). </summary>
    public StringTag() { }


    /// <summary> Creates an unnamed String tag with the given value. </summary>
    /// <param name="value"> String value to assign to this tag. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    public StringTag(string value)
        : this(null, value) { }


    /// <summary> Creates an String tag with the given name and value. </summary>
    /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
    /// <param name="value"> String value to assign to this tag. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="value"/> is <c>null</c>. </exception>
    public StringTag(string? tagName, string value)
    {
      if (value == null) throw new ArgumentNullException(nameof(value));
      name = tagName;
      Value = value;
    }


    /// <summary> Creates a copy of given String tag. </summary>
    /// <param name="other"> NBTTag to copy. May not be <c>null</c>. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="other"/> is <c>null</c>. </exception>
    public StringTag(StringTag other)
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
        readStream.SkipString();
        return false;
      }
      Value = readStream.ReadString();
      return true;
    }


    internal override void SkipTag(NBTBinaryReader readStream)
    {
      readStream.SkipString();
    }


    internal override void WriteTag(NBTBinaryWriter writeStream)
    {
      writeStream.Write(NBTTagType.String);
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
      return new StringTag(this);
    }


    internal override void PrettyPrint(StringBuilder sb, string indentString, int indentLevel)
    {
      for (int i = 0; i < indentLevel; i++)
      {
        sb.Append(indentString);
      }
      sb.Append("TAG_String");
      if (!String.IsNullOrEmpty(Name))
      {
        sb.AppendFormat("(\"{0}\")", Name);
      }
      sb.Append(": \"");
      sb.Append(Value);
      sb.Append('"');
    }
  }
}