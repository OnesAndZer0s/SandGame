using Sandbox.Worlds.Entities.Players;
using Sandbox.Worlds.Inventories;
using Sandbox.Worlds.Items;
using Sirenix.OdinInspector;

public class InfiniteItemSlot : Slot
{

  [ShowInInspector]
  public ItemStack itemStack;


  public override ItemStack GetItem()
  {
    return itemStack;
  }

  public override void Set(ItemStack itemStack)
  {
    if (this.itemStack == null)
    {
      this.itemStack = itemStack;
    }
    SetChanged();
  }

  public override void SetChanged()
  {
    this.itemStack.SetCount(1);
    Render();
  }


  public override ItemStack TryRemove(int slot, int count, Player player)
  {
    ItemStack itemStack = base.TryRemove(slot, count, player);
    return itemStack.CopyWithCount(itemStack.GetMaxStackSize());
  }

  public override ItemStack Remove(int slot)
  {
    return this.itemStack;
  }

  public override ItemStack SafeInsert(ItemStack itemStack, int count)
  {
    return ItemStack.EMPTY;
  }

}