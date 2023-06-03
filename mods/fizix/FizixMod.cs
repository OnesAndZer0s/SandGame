
using System.Diagnostics;
using Sandbox.Common.Mods;

public class FizixMod : Mod
{
  public static new string ModID = "fizix";

  public FizixMod() : base(ModID, "Fizix", 0, 0, 1, "This bitch empty, yeet", "Sandbox Team")
  {
    Debug.Print("Hello World from Fizix!");
  }


  public override void PreInit(ModEventBus eventBus)
  {

  }

  public override void Init(ModEventBus eventBus)
  {
    // run static constructor for ExpanseItems
    FizixItems.Init();
    // run static constructor for ExpanseBlocks
    // ExpanseTiles.Init();
  }

  public override void PostInit(ModEventBus eventBus)
  {
    // Player player = GameObject.Find("Player").GetComponent<Player>();
    // Registry<Item> itemRegistry = Registries.Get<Item>(BuiltInRegistries.ITEM);
    // player.inventory.SetItem(0, new ItemStack(ExpanseItems.TILE_GRASS, 64));
    // player.inventory.SetItem(1, new ItemStack(ExpanseItems.DIRT, 64));
    // player.inventory.SetItem(2, new ItemStack(ExpanseItems.COBBLESTONE, 64));

  }
}