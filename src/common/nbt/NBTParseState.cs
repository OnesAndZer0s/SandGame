namespace Sandbox.Common.NBT
{
  internal enum NBTParseState
  {
    AtStreamBeginning,
    AtCompoundBeginning,
    InCompound,
    AtCompoundEnd,
    AtListBeginning,
    InList,
    AtStreamEnd,
    Error
  }
}