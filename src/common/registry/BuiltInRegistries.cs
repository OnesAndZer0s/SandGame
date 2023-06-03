
namespace Sandbox.Common.Registry
{
  public static class BuiltInRegistries
  {
    public static /*readonly*/ ResourceLocation ITEM;// = new ResourceLocation("core", "item");
    public static /*readonly*/ ResourceLocation TILE;// = new ResourceLocation("core", "tile");


    static BuiltInRegistries()
    {
      ITEM = new ResourceLocation("core", "item");
      TILE = new ResourceLocation("core", "tile");
      // ItemRegistry.Register(ITEM, new Item(ITEM));
      // TileRegistry.Register(TILE, new Tile(TILE));
    }
  }
}