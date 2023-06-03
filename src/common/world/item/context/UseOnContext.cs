
using Sandbox.Common.Worlds;
using Sandbox.Common.Worlds.Items;
using Sandbox.Common.Worlds.Levels;

namespace Sandbox.Worlds.Items.Context
{
  public class UseOnContext
  {
    private Player player;
    private InteractionHand hand;
    private RaycastHit hitResult;
    private Level level;
    private ItemStack itemStack;
    private bool hit;

    public UseOnContext(Player player, InteractionHand intHand, RaycastHit p_43711_) :
      this(player.GetLevel(), player, intHand, player.GetItemInHand(intHand), p_43711_)
    {
    }

    public UseOnContext(Level level, Player player, InteractionHand intHand, ItemStack itemStack, RaycastHit p_43717_, bool hit = false)
    {
      this.player = player;
      this.hand = intHand;
      this.hitResult = p_43717_;
      this.itemStack = itemStack;
      this.level = level;
      this.hit = hit;
    }

    public virtual RaycastHit GetHitResult()
    {
      return this.hitResult;
    }

    // public virtual Vector3 GetClickedPos()
    // {
    //   return this.hitResult.GetTilePos();
    // }

    // public virtual Vector3 GetClickedFace()
    // {
    //   return this.hitResult.GetDirection();
    // }

    // public virtual Vector3 GetClickLocation()
    // {
    //   return this.hitResult.GetLocation();
    // }

    public virtual ItemStack GetItemInHand()
    {
      return this.itemStack;
    }

    public virtual bool GetHit()
    {
      return this.hit;
    }

    public virtual Player GetPlayer()
    {
      return this.player;
    }

    public virtual InteractionHand GetHand()
    {
      return this.hand;
    }

    public virtual Level GetLevel()
    {
      return this.level;
    }

    // public virtual Vector3 GetHorizontalDirection()
    // {
    //   return (this.player == null) ? Vector3.forward : this.player.GetDirection();
    // }

    public virtual bool IsSecondaryUseActive()
    {
      return this.player != null && this.player.IsSecondaryUseActive();
    }

    public virtual float GetRotation()
    {
      return (this.player == null) ? 0.0f : this.player.LookingRotation.y;
    }
  }

}