

using System.Diagnostics;
using Sandbox.Common.Mods;

public class CoreMod : Mod
{

  public CoreMod() : base("core", "The Core", 0, 0, 1, "The Core Content for Sandbox", "Sandbox Team")
  {
    Debug.Print("Hello World from Sandbox Core!");



  }

  public override async void Init(ModEventBus modEventBus)
  {
    // DEBUG just giving the player a bunch of items
    Player player = GameObject.Find("Player").GetComponent<Player>();
    var worldUI = GameObject.Find("WorldUI");

    PlayerInput input = GameObject.Find("InputManager").GetComponent<PlayerInput>();



    GameObject op = await Hotbar.Create(worldUI.transform, player).Task;
    GameObject hotbar = Instantiate(op, worldUI.transform);
    hotbar.name = hotbar.name.Replace("(Clone)", "");
    hotbar.GetComponent<Hotbar>().Initialize(worldUI.transform, player.inventory);
    Hotbar hb = hotbar.GetComponent<Hotbar>();



    op = await CreativeInventoryContainer.Create(worldUI.transform, player).Task;
    GameObject inventory = Instantiate(op, worldUI.transform);
    inventory.name = inventory.name.Replace("(Clone)", "");
    inventory.GetComponent<CreativeInventoryContainer>().Initialize(worldUI.transform, player.inventory);


    op = await CreativeItems.Create(worldUI.transform, player).Task;
    GameObject creativeItems = Instantiate(op, worldUI.transform);
    creativeItems.name = creativeItems.name.Replace("(Clone)", "");
    creativeItems.GetComponent<CreativeItems>().Initialize(worldUI.transform, player.inventory);


    inventory.GetComponentsInChildren<Tab>()[0].GetComponent<Button>().onClick.AddListener(() =>
    {
      Debug.Log("Clicked");
      inventory.GetComponent<CreativeInventoryContainer>().SetVisible(false);
      creativeItems.GetComponent<CreativeItems>().SetVisible(true);
    });

    creativeItems.GetComponentsInChildren<Tab>()[1].GetComponent<Button>().onClick.AddListener(() =>
    {
      Debug.Log("Clicked");

      inventory.GetComponent<CreativeInventoryContainer>().SetVisible(true);
      creativeItems.GetComponent<CreativeItems>().SetVisible(false);
    });



    InputActionMap playerMap = input.actions.FindActionMap("Player");
    InputActionMap uiMap = input.actions.FindActionMap("UI");
    playerMap.Disable();
    uiMap.Disable();

    var inventoryAction = playerMap.AddAction("inventory", binding: "<Keyboard>/e");
    inventoryAction.performed += ctx =>
    {
      input.currentActionMap = uiMap;

      // TODO REMEBER STATE
      bool visible = !hotbar.activeSelf;
      hotbar.GetComponent<Hotbar>().SetVisible(visible);
      inventory.GetComponent<CreativeInventoryContainer>().SetVisible(!visible);
      creativeItems.GetComponent<CreativeItems>().SetVisible(false);
      // TODO figure out a better way of this
      player.isInUI = !player.isInUI;

      if (!visible)
      {
        Cursor.lockState = CursorLockMode.None;
        inventory.GetComponent<CreativeInventoryContainer>().UpdateSlots();
      }
      else
      {
        hotbar.GetComponent<Hotbar>().UpdateSlots();

        Cursor.lockState = CursorLockMode.Locked;
      }
    };
    // inventoryAction.Enable();


    var exitContainerAction = uiMap.AddAction("exitContainer", binding: "<Keyboard>/escape");
    exitContainerAction.performed += ctx =>
    {
      if (player.isInUI)
      {

        // enable player controls
        input.currentActionMap = playerMap;

        // TODO REMEBER STATE
        // bool visible = !hotbar.activeSelf;
        hotbar.GetComponent<Hotbar>().SetVisible(true);
        inventory.GetComponent<CreativeInventoryContainer>().SetVisible(false);
        creativeItems.GetComponent<CreativeItems>().SetVisible(false);
        // TODO figure out a better way of this
        player.isInUI = false;
        Cursor.lockState = CursorLockMode.Locked;

        // if (!visible)
        // {
        // Cursor.lockState = CursorLockMode.None;
        // inventory.GetComponent<CreativeInventoryContainer>().UpdateSlots();
        // }
        // else
        // {
        hotbar.GetComponent<Hotbar>().UpdateSlots();
        // }
      };
    };
    // exitContainerAction.Enable();

    var placeAction = playerMap.AddAction("place", binding: "<Mouse>/rightButton");
    placeAction.performed += ctx =>
      {
        if (player.isInUI) return;

        ItemStack item = player.inventory.GetItem(hb.currentHighlighted);
        if (item == null)
        {
          return;
        }
        Dictionary<ResourceLocation, Item> itemObj = Registries.Get<Item>(BuiltInRegistries.ITEM);
        if (itemObj.ContainsValue(item.GetItem()))
        {
          Debug.Log("Item Found!");
          RaycastHit hitInfo;
          var hit = Physics.Raycast(player.pc.transform.position, player.pc.transform.forward, out hitInfo, 8, ~(1 << 8));

          item.GetItem().UseOn(new UseOnContext(GameObject.Find("Level").GetComponent<Level>(), player, InteractionHand.Main, item, hitInfo, hit));
        }

      };
    // placeAction.Enable();

    var breakAction = playerMap.AddAction("break", binding: "<Mouse>/leftButton");
    breakAction.performed += ctx =>
      {
        if (player.isInUI) return;

        int layerMask = ~(1 << 8);
        RaycastHit hitInfo;
        var hit = Physics.Raycast(player.pc.transform.position, player.pc.transform.forward, out hitInfo, 8, layerMask);

        if (ctx.performed && hit && Cursor.lockState == CursorLockMode.Locked)
        {
          Vector3Int removeBlock = Vector3Int.FloorToInt(hitInfo.point - (hitInfo.normal * 0.5f));

          // string blockToReplace = world.getTile(removeBlock);

          GameObject.Find("Level").GetComponent<Level>().Remove(removeBlock.x, removeBlock.y, removeBlock.z);

        };
      };
    // breakAction.Enable();


    playerMap.Enable();

    // INVENTORY


    // HOTBAR
    // {

    //   // create image
    //   var actualHotbar = new GameObject("Actual Hotbar");
    //   hotbar.layer = LayerMask.NameToLayer("UI");
    //   actualHotbar.layer = LayerMask.NameToLayer("UI");

    //   hotbar.transform.SetParent(worldUI.transform);
    //   actualHotbar.transform.SetParent(hotbar.transform);

    //   RectTransform trans = hotbar.AddComponent<RectTransform>();
    //   // set transform stretch
    //   trans.anchorMin = new Vector2(0, 0);
    //   trans.anchorMax = new Vector2(1, 1);
    //   trans.pivot = new Vector2(0.5f, 0f);

    //   trans.anchoredPosition = new Vector2(0, 0);
    //   trans.localScale = new Vector3(3.85f, 3.85f, 3.85f);
    //   trans.sizeDelta = new Vector2(0, 0);

    //   RectTransform what = actualHotbar.AddComponent<RectTransform>();
    //   // set transform bottom
    //   what.anchorMin = new Vector2(0.5f, 0);
    //   what.anchorMax = new Vector2(0.5f, 0);
    //   what.pivot = new Vector2(0.5f, 0);
    //   what.localScale = new Vector3(1f, 1f, 1f);
    //   what.sizeDelta = new Vector2(182, 22);
    //   what.anchoredPosition = new Vector2(0, 5);


    //   actualHotbar.AddComponent<CanvasRenderer>().cullTransparentMesh = false;
    //   RawImage img = actualHotbar.AddComponent<RawImage>();
    //   Texture2D tx = ModResources.LoadTexture2D("core", "texture/gui/container/player/hotbar.png");
    //   img.texture = tx;



    //   Hotbar hb = hotbar.AddComponent<Hotbar>();

    //   GameObject hotbarSelected = new GameObject("Hotbar Selected");

    //   RectTransform hSRect = hotbarSelected.AddComponent<RectTransform>();
    //   hotbarSelected.transform.SetParent(actualHotbar.transform);

    //   // set transform bottom
    //   hSRect.anchorMin = new Vector2(0.5f, 0);
    //   hSRect.anchorMax = new Vector2(0.5f, 0);
    //   hSRect.pivot = new Vector2(0.5f, 0);
    //   hSRect.localScale = new Vector3(1f, 1f, 1f);
    //   hSRect.sizeDelta = new Vector2(24, 24);
    //   hSRect.anchoredPosition = new Vector2(0, -1);

    //   hotbarSelected.AddComponent<CanvasRenderer>().cullTransparentMesh = false;
    //   RawImage hSImg = hotbarSelected.AddComponent<RawImage>();
    //   Texture2D hSTX = ModResources.LoadTexture2D("core", "texture/gui/container/player/hotbar_selected.png");
    //   hSImg.texture = hSTX;

    //   hb.currentSelectedGraphic = hSRect;
    //   // hb.itemSlotPrefab = GameObject.Find("ItemModel");
    //   // hb.Initialize(actualHotbar.transform, player.inventory);

    //   var placeAction = new InputAction("place", binding: "<Mouse>/rightButton");
    //   placeAction.performed += ctx =>
    //     {
    //       if (player.isInUI) return;

    //       ItemStack item = player.inventory.GetItem(hb.currentHighlighted);
    //       if (item == null)
    //       {
    //         return;
    //       }
    //       Dictionary<ResourceLocation, Item> itemObj = Registries.Get<Item>(BuiltInRegistries.ITEM);
    //       if (itemObj.ContainsValue(item.Item))
    //       {
    //         Debug.Log("Item Found!");
    //         item.Item.Use(GameObject.Find("Level").GetComponent<Level>(), player, InteractionHand.Main);
    //       }

    //     };
    //   placeAction.Enable();

    //   var breakAction = new InputAction("break", binding: "<Mouse>/leftButton");
    //   breakAction.performed += ctx =>
    //     {
    //       if (player.isInUI) return;

    //       int layerMask = ~(1 << 8);
    //       RaycastHit hitInfo;
    //       var hit = Physics.Raycast(player.pc.transform.position, player.pc.transform.forward, out hitInfo, 8, layerMask);

    //       if (ctx.performed && hit && Cursor.lockState == CursorLockMode.Locked)
    //       {
    //         Vector3 inCube = hitInfo.point - (hitInfo.normal * 0.5f);
    //         Vector3Int removeBlock = new Vector3Int(
    //           Mathf.FloorToInt(inCube.x),
    //           Mathf.FloorToInt(inCube.y),
    //           Mathf.FloorToInt(inCube.z)
    //         );
    //         Vector3 fromCube = hitInfo.point + (hitInfo.normal * 0.5f);
    //         Vector3Int placeBlock = new Vector3Int(
    //           Mathf.FloorToInt(fromCube.x),
    //           Mathf.FloorToInt(fromCube.y),
    //           Mathf.FloorToInt(fromCube.z)
    //         );
    //         // string blockToReplace = world.getTile(removeBlock);

    //         GameObject.Find("Level").GetComponent<Level>().Remove(removeBlock.x, removeBlock.y, removeBlock.z);

    //       };
    //       breakAction.Enable();
    //     };
    // }

  }
}