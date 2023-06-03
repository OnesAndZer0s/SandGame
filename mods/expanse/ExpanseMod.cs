

using System.Diagnostics;
using Sandbox.Common.Mods;

public class ExpanseMod : Mod
{
  public static new string ModID = "expanse";

  public ExpanseMod() : base(ModID, "The Expanse", 0, 0, 1, "Let there be an expanse between the waters to separate water from water.", "Sandbox Team")
  {
    Debug.Print("Hello World from Expanse!");
  }


  public override void PreInit(ModEventBus eventBus)
  {

  }

  public override void Init(ModEventBus eventBus)
  {
    Debug.Print("INIT MEE!");
    // run static constructor for ExpanseItems
    ExpanseItems.Init();
    // run static constructor for ExpanseBlocks
    ExpanseTiles.Init();
  }

  public override void PostInit(ModEventBus eventBus)
  {
    Player player = GameObject.Find("Player").GetComponent<Player>();
    Registry<Item> itemRegistry = Registries.Get<Item>(BuiltInRegistries.ITEM);
    player.inventory.SetItem(0, new ItemStack(ExpanseItems.TILE_GRASS, 64));
    player.inventory.SetItem(1, new ItemStack(ExpanseItems.DIRT, 64));
    player.inventory.SetItem(2, new ItemStack(ExpanseItems.COBBLESTONE, 64));

  }
}