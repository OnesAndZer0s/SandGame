using Sandbox.Worlds.Entities.Players;
using Sandbox.Worlds.Inventories;
using Sandbox.Worlds.Items;
using Sirenix.OdinInspector;

public class TrashSlot : Slot
{



  public override ItemStack GetItem()
  {
    return ItemStack.EMPTY;
  }

  public override void Set(ItemStack itemStack)
  {

  }

  public override void SetChanged()
  {

  }


  public override ItemStack TryRemove(int slot, int count, Player player)
  {
    return ItemStack.EMPTY;
  }

  public override ItemStack Remove(int slot)
  {
    return ItemStack.EMPTY;
  }

  public override ItemStack SafeInsert(ItemStack itemStack, int count)
  {
    return ItemStack.EMPTY;
  }

}