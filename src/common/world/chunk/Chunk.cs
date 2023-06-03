using Sandbox.Worlds.Chunks;

namespace Sandbox.Common.World.Chunks
{
  public class Chunk
  {
    private ChunkData data;
    public Chunk(ChunkData data)
    {
      this.data = data;
    }


    public static Chunk FromChunkData(ChunkData data)
    {
      return new Chunk(data);
    }
  }
}