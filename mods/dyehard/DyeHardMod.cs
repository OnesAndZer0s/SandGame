using System.Diagnostics;
using Sandbox.Common.Mods;

public class DyeHardMod : Mod
{
  public static new string ModID = "dyehard";

  public DyeHardMod() : base(ModID, "Dye-Hard", 0, 0, 1, "Man, you must really like colors", "Sandbox Team")
  {
    Debug.Print("Hello World from Dye-Hard!");
  }


  public override void PreInit(ModEventBus eventBus)
  {

  }

  public override void Init(ModEventBus eventBus)
  {
    Debug.Print("INIT MEE!");
    // run static constructor for ExpanseItems
    DyeHardItems.Init();
    // run static constructor for ExpanseBlocks
    // ExpanseTiles.Init();
  }

  public override void PostInit(ModEventBus eventBus)
  {


  }
}