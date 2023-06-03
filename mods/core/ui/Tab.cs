using System;
using System.Collections.Generic;
using Sandbox.Server.Mods;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TabDirection
{
  Up, Down, Left, Right
}

public enum BevelDirection
{
  Left, Middle, Right
}

[RequireComponent(typeof(RectTransform), typeof(RawImage), typeof(Button))]
public class Tab : MonoBehaviour
{

  public List<Sprite> sprites = new List<Sprite>();

  public UnityEvent onClick = new UnityEvent();
  public GameObject tab;
  public GameObject content;

  [ShowInInspector]
  public TabDirection Direction
  {
    get { return _direction; }
    set { _direction = value; UpdateGraphic(); }
  }
  [SerializeField]
  [HideInInspector]
  private TabDirection _direction;

  [ShowInInspector]

  public bool Selected
  {
    get { return _selected; }
    set { _selected = value; UpdateGraphic(); }
  }
  [SerializeField]
  [HideInInspector]

  private bool _selected;
  [ShowInInspector]


  public bool Selectable
  {
    get { return _selectable; }
    set { _selectable = value; UpdateGraphic(); }
  }
  [SerializeField]
  [HideInInspector]
  private bool _selectable;
  [ShowInInspector]

  public BevelDirection Bevel
  {
    get { return _bevel; }
    set { _bevel = value; UpdateGraphic(); }
  }
  [SerializeField]
  [HideInInspector]
  private BevelDirection _bevel;



  private void UpdateGraphic()
  {
    RawImage image = GetComponent<RawImage>();

    image.texture = sprites.Find(s => s.name == "tab_" + Enum.GetName(typeof(TabDirection), Direction) + "_" + Enum.GetName(typeof(BevelDirection), Bevel) + (Selectable ? "" : "_desel") + (Selected ? "_active" : "")).texture;
  }



  // public static GameObject Create(Transform parent)
  // {
  // instantiate prefab
  // return Instantiate(ModResources.LoadPrefab("core", "prefab/ui/container/tab"), parent);
  // }
}