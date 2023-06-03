using Sandbox.Core;
using Sandbox.Core.Registries;
using Sandbox.Worlds;
using Sandbox.Worlds.Items;
using UnityEngine;

public static class ExpanseItems
{
  public static Registry<Item> ITEMS = Registries.Get<Item>(BuiltInRegistries.ITEM);

  // public static Item STICK = ITEMS.Register("expanse:stick", new Item());
  // public static Item COAL = ITEMS.Register("expanse:coal", new Item());
  // public static Item DIAMOND = ITEMS.Register("expanse:diamond", new Item());

  public static Item ANVIL = ITEMS.Register("expanse:anvil", new TileItem(ExpanseTiles.ANVIL));
  public static Item BEDROCK = ITEMS.Register("expanse:bedrock", new TileItem(ExpanseTiles.BEDROCK));
  public static Item TILE_GRASS = ITEMS.Register("expanse:tile_grass", new TileItem(ExpanseTiles.TILE_GRASS));
  public static Item DIRT = ITEMS.Register("expanse:dirt", new TileItem(ExpanseTiles.DIRT));
  public static Item STONE = ITEMS.Register("expanse:stone", new TileItem(ExpanseTiles.STONE));
  public static Item COBBLESTONE = ITEMS.Register("expanse:cobblestone", new TileItem(ExpanseTiles.COBBLESTONE));
  public static Item ORE_COAL = ITEMS.Register("expanse:ore_coal", new TileItem(ExpanseTiles.ORE_COAL));
  public static Item ORE_IRON = ITEMS.Register("expanse:ore_iron", new TileItem(ExpanseTiles.ORE_IRON));
  public static Item ORE_GOLD = ITEMS.Register("expanse:ore_gold", new TileItem(ExpanseTiles.ORE_GOLD));
  public static Item ORE_DIAMOND = ITEMS.Register("expanse:ore_diamond", new TileItem(ExpanseTiles.ORE_DIAMOND));
  public static Item LOG_OAK = ITEMS.Register("expanse:log_oak", new TileItem(ExpanseTiles.LOG_OAK));
  public static Item PLANKS_OAK = ITEMS.Register("expanse:planks_oak", new TileItem(ExpanseTiles.PLANKS_OAK));
  public static Item GLOWSTONE = ITEMS.Register("expanse:glowstone", new TileItem(ExpanseTiles.GLOWSTONE));
  public static Item DIORITE = ITEMS.Register("expanse:diorite", new TileItem(ExpanseTiles.DIORITE));
  public static Item GRANITE = ITEMS.Register("expanse:granite", new TileItem(ExpanseTiles.GRANITE));
  public static Item ANDESITE = ITEMS.Register("expanse:andesite", new TileItem(ExpanseTiles.ANDESITE));
  public static Item LEAVES_OAK = ITEMS.Register("expanse:leaves_oak", new TileItem(ExpanseTiles.LEAVES_OAK));
  public static Item ENCHANTING_TABLE = ITEMS.Register("expanse:enchanting_table", new TileItem(ExpanseTiles.ENCHANTING_TABLE));

  public static void Init()
  {
    Debug.Log("ExpanseItems static constructor");
  }
}