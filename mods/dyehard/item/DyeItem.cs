
using System.Drawing;
using Sandbox.Common.Worlds.Items;

public class DyeItem : Item
{

  private Color color;
  public DyeItem(Color color)
  {
    this.color = color;
  }
}