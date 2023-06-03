using Sandbox.Server.Mods;
using UnityEngine;
using Sandbox.Worlds;
using UnityEngine.UI;
using Sandbox.Worlds.Items;
using Sandbox.Worlds.Entities.Players;
using Sandbox.Core.Registries;
using Sandbox.Core;

public class DyeHardMod : Mod
{
  public static new string ModID = "dyehard";

  public DyeHardMod() : base(ModID, "Dye-Hard", 0, 0, 1, "Man, you must really like colors", "Sandbox Team")
  {
    Debug.Log("Hello World from Dye-Hard!");
  }


  public override void PreInit(ModEventBus eventBus)
  {

  }

  public override void Init(ModEventBus eventBus)
  {
    Debug.Log("INIT MEE!");
    // run static constructor for ExpanseItems
    DyeHardItems.Init();
    // run static constructor for ExpanseBlocks
    // ExpanseTiles.Init();
  }

  public override void PostInit(ModEventBus eventBus)
  {


  }
}