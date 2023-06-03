namespace Sandbox.Common.NBT
{
  // Represents state of a node in the NBT file tree, used by NBTReader
  internal sealed class NBTReaderNode
  {
    public string? ParentName;
    public NBTTagType ParentTagType;
    public NBTTagType ListType;
    public int ParentTagLength;
    public int ListIndex;
  }
}