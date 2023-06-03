using System;

namespace Sandbox.Common.NBT
{
  /// <summary> Exception thrown when a format violation is detected while
  /// parsing or serializing an NBT file. </summary>
  [Serializable]
  public sealed class NBTFormatException : Exception
  {
    internal NBTFormatException(string message)
        : base(message) { }
  }

}