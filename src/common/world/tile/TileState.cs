

namespace Sandbox.Common.Worlds.Tiles
{


  public class TileState : Dictionary<string, object>
  {
    private Tile tile;
    public TileState(Tile tile)
    {
      this.tile = tile;
    }

    public Tile GetTile()
    {
      return tile;
    }


    // public TileState(Tile p_61042_, ImmutableDictionary<Property<Tile>, IComparable<TileState>> p_61043_) : base(p_61042_, p_61043_)
    // {
    // }

    // protected override TileState AsState()
    // {
    //   return this;
    // }
  }
}
