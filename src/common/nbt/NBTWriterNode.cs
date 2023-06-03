namespace Sandbox.Common.NBT
{
  // Represents state of a node in the NBT file tree, used by NBTWriter
  internal sealed class NBTWriterNode
  {
    public NBTTagType ParentType;
    public NBTTagType ListType;
    public int ListSize;
    public int ListIndex;
  }
}