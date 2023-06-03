using Sandbox.Common.NBT;

namespace Sandbox.Common.Util
{
  public interface INBTSerializable<T> where T : NBTTag
  {
    T SerializeNBT();

    void DeserializeNBT(T p0);
  }

}