
using System.Diagnostics;
using Sandbox.Common.Registry;
using Sandbox.Common.Worlds.Tiles;

public static class ExpanseTiles
{
  public static Registry<Tile> TILES = Registries.Get<Tile>(BuiltInRegistries.TILE);

  public static Tile ANVIL = TILES.Register("expanse:anvil", new Tile());
  public static Tile BEDROCK = TILES.Register("expanse:bedrock", new Tile());
  public static Tile TILE_GRASS = TILES.Register("expanse:tile_grass", new Tile());
  public static Tile DIRT = TILES.Register("expanse:dirt", new Tile());
  public static Tile STONE = TILES.Register("expanse:stone", new Tile());
  public static Tile COBBLESTONE = TILES.Register("expanse:cobblestone", new Tile());
  public static Tile ORE_COAL = TILES.Register("expanse:ore_coal", new Tile());
  public static Tile ORE_IRON = TILES.Register("expanse:ore_iron", new Tile());
  public static Tile ORE_GOLD = TILES.Register("expanse:ore_gold", new Tile());
  public static Tile ORE_DIAMOND = TILES.Register("expanse:ore_diamond", new Tile());
  public static Tile LOG_OAK = TILES.Register("expanse:log_oak", new Tile());
  public static Tile PLANKS_OAK = TILES.Register("expanse:planks_oak", new Tile());
  // public static Tile GLOWSTONE = TILES.Register("expanse:glowstone", new LitTile());
  public static Tile DIORITE = TILES.Register("expanse:diorite", new Tile());
  public static Tile GRANITE = TILES.Register("expanse:granite", new Tile());
  public static Tile ANDESITE = TILES.Register("expanse:andesite", new Tile());
  public static Tile LEAVES_OAK = TILES.Register("expanse:leaves_oak", new Tile());
  public static Tile ENCHANTING_TABLE = TILES.Register("expanse:enchanting_table", new Tile());

  public static void Init()
  {
    Debug.Print("ExpanseItems static constructor");
  }
}