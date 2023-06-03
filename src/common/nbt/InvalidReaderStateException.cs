using System;
namespace Sandbox.Common.NBT
{
  [Serializable]
  public sealed class InvalidReaderStateException : InvalidOperationException
  {
    internal InvalidReaderStateException(string message)
        : base(message) { }
  }
}