using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.Client.Resources.Model;
using Sandbox.Server.Mods;
using Sandbox.Worlds;
using Sandbox.Worlds.Entities.Players;
using Sandbox.Worlds.Inventories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;


public class Hotbar : AbstractContainerMenu
{
  public Player player;
  public int currentHighlighted = 0;
  public RectTransform currentSelectedGraphic;
  public void Update()
  {
    UpdateHotbar();
  }
  public override bool Initialize(Transform parent, Container container)
  {
    this.container = container;

    player = GameObject.Find("Player").GetComponent<Player>();

    for (int i = 0; i < 10; ++i)
    {
      slots[i].BindSlot(container, i);
      // GameObject slot = Slot.Create(this);
      // slot.GetComponent<Slot>().CanHover = false;
      // RectTransform slotRect = slot.GetComponent<RectTransform>();
      // slotRect.anchorMin = new Vector2(0.5f, 0);
      // slotRect.anchorMax = new Vector2(0.5f, 0);
      // slotRect.pivot = new Vector2(0.5f, 0.5f);

      // slot.transform.localPosition = new Vector2(-80 + (i * 20), 16);


    }

    // elementPrefab.gameObject.SetActive(false);
    Rebuild();

    return true;
  }
  public void UpdateHotbar()
  {
    float scroll = Mouse.current.scroll.y.ReadValue();
    if (scroll != 0)
    {
      int scrollDirection = (int)Mathf.Sign(scroll);
      currentHighlighted -= scrollDirection;
      if (currentHighlighted < 0) currentHighlighted += 10;
      if (currentHighlighted > 9) currentHighlighted -= 10;
      Rebuild();
    }
    Key alpha1 = Key.Digit1;
    for (int i = 0; i < 10; ++i)
    {
      if (Keyboard.current[alpha1 + i].IsActuated())
      {
        currentHighlighted = i;
        Rebuild();
      }
    }

  }

  public override void UpdateSlots()
  {
    foreach (Slot slot in slots)
    {
      slot.Render();
    }
  }

  private void Rebuild()
  {
    Player player = GameObject.Find("Player").GetComponent<Player>();
    Vector2 graphicAnchoredPosition = currentSelectedGraphic.anchoredPosition;
    graphicAnchoredPosition.x = -90 + 20 * currentHighlighted;
    currentSelectedGraphic.anchoredPosition = graphicAnchoredPosition;
    for (int i = 0; i < 10; i++)
    {

      slots[i].Render();
      // Item item = ModManager.Get("item", elements[i]);
      // var e = elementRenders[i].GetComponentInChildren<MeshFilter>();
      // if (player.inventory.GetItem(i) == null)
      // {
      //   e.sharedMesh = null;
      //   continue;
      // }

      // // get item at index 0
      // // get resourceLocation of item
      // Item item = player.inventory.GetItem(i).item;
      // ResourceLocation rl = ModManager.EventBus.GetResourceLocation<Item>(item);
      // var itemModel = rl.ToString().Replace(":", ":item/");
      // e.sharedMesh = TileModelManager.GetMesh(itemModel, GameManager.Instance.PLACEHOLDERTINTINDEX);
      // var t = TileModelManager.GetDisplayForKey(itemModel, "gui");

      // e.transform.rotation = Quaternion.Euler(t.rotation[0], t.rotation[1], t.rotation[2]);
      // e.transform.localScale = new Vector3(t.scale[0], t.scale[1], t.scale[2]);

      // e.transform.localPosition = new Vector3(-80 + (20 * i), 11, -30);
      // e.transform.localPosition += new Vector3(t.translation[0], t.translation[1], t.translation[2]);
    }
  }

  public override bool StillValid(Player player)
  {
    return true;
  }

  public static AsyncOperationHandle<GameObject> Create(Transform parent, Player player)
  {
    return Addressables.LoadAssetAsync<GameObject>("Hotbar");
  }

  // public static GameObject Create(Transform parent, Container container)
  // {
  //   GameObject go = Instantiate(ModResources.LoadPrefab("core", "prefab/ui/hotbar"), parent);
  //   return go;
  // }
}
