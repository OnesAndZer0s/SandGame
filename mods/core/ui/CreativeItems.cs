using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.Client.Resources.Model;
using Sandbox.Core;
using Sandbox.Core.Registries;
using Sandbox.Server.Mods;
using Sandbox.Worlds;
using Sandbox.Worlds.Entities.Players;
using Sandbox.Worlds.Inventories;
using Sandbox.Worlds.Items;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;


public class CreativeItems : AbstractContainerMenu
{
  public Player player;
  public Slider scroll;
  public TMPro.TMP_InputField searchField;
  public InfiniteItemSlot[] creativeSlots;

  public new void Update()
  {
    base.Update();
    // UpdateSlots();
  }

  public override bool Initialize(Transform parent, Container container)
  {
    Debug.Log("Initializing CreativeItems");
    base.Initialize(parent, container);

    carriedSlot.BindSlot(container, slots.Count);

    for (int i = 0; i < slots.Count; ++i)
    {
      slots[i].BindSlot(container, i);
    }


    creativeSlots = GetComponentsInChildren<InfiniteItemSlot>();

    player = GameObject.Find("Player").GetComponent<Player>();

    scroll.onValueChanged.AddListener((float value) =>
    {
      UpdateSlots();
    });

    searchField.onValueChanged.AddListener((string value) =>
    {
      UpdateSlots();
    });


    UpdateSlots();

    gameObject.SetActive(false);
    // Rebuild();
    return true;
  }
  public override void UpdateSlots()
  {

    Registry<Item> items = Registries.Get<Item>(BuiltInRegistries.ITEM);

    // TODO filter by searchField
    // itemList = itemList.Where((Item item) =>
    // {
    //   return item.GetRegistryName().ToString().Contains(searchField.text);
    // }).ToList();
    // filter by iterations of 10
    IEnumerable<KeyValuePair<ResourceLocation, Item>> itemListOther = items.ToList().Where((KeyValuePair<ResourceLocation, Item> item) =>
    {
      return item.Key.ToString().Contains(searchField.text);
    });

    List<Item> itemList = itemListOther.Select((KeyValuePair<ResourceLocation, Item> item) =>
    {
      return item.Value;
    }).ToList();

    int skip = (int)((Mathf.FloorToInt(itemList.Count / 10) * scroll.value)) * 10;

    // Debug.Log(skip);
    // itemList = items.Values.Skip(5).ToList();
    itemList = itemList.Skip(skip).ToList();
    // Debug.Log(itemList.Count);
    // float iterations = Mathf.Floor(itemList.Count / 10f);
    // int startIndex = Mathf.FloorToInt(scroll.value * iterations);
    // int endIndex = Mathf.FloorToInt((scroll.value + 0.1f) * iterations);
    // itemList = itemList.GetRange(startIndex, endIndex - startIndex);

    for (int i = 0; i < creativeSlots.Length; ++i)
    {
      if (i >= itemList.Count)
        creativeSlots[i].itemStack = (ItemStack.EMPTY);
      else
        creativeSlots[i].itemStack = (new ItemStack(itemList[i], 1));
    }

    foreach (InfiniteItemSlot slot in creativeSlots)
    {
      slot.Render();
    }

    foreach (Slot slot in slots)
    {
      slot.Render();
    }
  }

  public override bool StillValid(Player player)
  {
    return true;
  }
  // public void UpdateHotbar()
  // {
  //   float scroll = Mouse.current.scroll.y.ReadValue();
  //   if (scroll != 0)
  //   {
  //     int scrollDirection = (int)Mathf.Sign(scroll);
  //     currentHighlighted -= scrollDirection;
  //     if (currentHighlighted < 0) currentHighlighted += 9;
  //     if (currentHighlighted > 8) currentHighlighted -= 9;
  //     Rebuild();
  //   }
  //   Key alpha1 = Key.Digit1;
  //   for (int i = 0; i < 9; ++i)
  //   {
  //     if (Keyboard.current[alpha1 + i].IsActuated())
  //     {
  //       currentHighlighted = i;
  //       Rebuild();
  //     }
  //   }

  // }

  // private void Rebuild()
  // {
  //   Player player = GameObject.Find("Player").GetComponent<Player>();
  //   Debug.Log("Rebuilding hotbar");
  //   Vector2 graphicAnchoredPosition = currentSelectedGraphic.anchoredPosition;
  //   graphicAnchoredPosition.x = -80 + 20 * currentHighlighted;
  //   currentSelectedGraphic.anchoredPosition = graphicAnchoredPosition;
  //   for (int i = 0; i < 9; i++)
  //   {
  //     // Item item = ModManager.Get("item", elements[i]);
  //     var e = elementRenders[i].GetComponentInChildren<MeshFilter>();
  //     if (player.inventory.GetItem(i) == null)
  //     {
  //       e.sharedMesh = null;
  //       continue;
  //     }

  //     // get item at index 0
  //     // get resourceLocation of item
  //     Item item = player.inventory.GetItem(i).item;
  //     ResourceLocation rl = ModManager.EventBus.GetResourceLocation<Item>(item);
  //     var itemModel = rl.ToString().Replace(":", ":item/");
  //     e.sharedMesh = TileModelManager.GetMesh(itemModel, GameManager.Instance.PLACEHOLDERTINTINDEX);
  //     var t = TileModelManager.GetDisplayForKey(itemModel, "gui");

  //     e.transform.rotation = Quaternion.Euler(t.rotation[0], t.rotation[1], t.rotation[2]);
  //     e.transform.localScale = new Vector3(t.scale[0], t.scale[1], t.scale[2]);

  //     e.transform.localPosition = new Vector3(-80 + (20 * i), 11, -30);
  //     e.transform.localPosition += new Vector3(t.translation[0], t.translation[1], t.translation[2]);
  //   }
  // }
  public static AsyncOperationHandle<GameObject> Create(Transform parent, Player player)
  {
    return Addressables.LoadAssetAsync<GameObject>("CreativeItems");
  }


}
