
using Sandbox.Common.Maths.Raycasts;
using Sandbox.Common.Worlds;
using Sandbox.Common.Worlds.Items;
using Sandbox.Common.Worlds.Levels;
using Sandbox.Common.Worlds.Players;

namespace Sandbox.Worlds.Items.Context
{
  public class UseOnContext
  {
    public Player player { get; private set; }
    public InteractionHand hand { get; private set; }
    public Level level { get; private set; }
    public RaycastHit rayHit { get; private set; }
    public ItemStack itemStack { get; private set; }
    public bool hit { get; private set; }

    public UseOnContext(Player player, InteractionHand intHand, RaycastHit raycastHit) :
      this(player.level, player, intHand, player.GetItemInHand(intHand), raycastHit)
    {
    }

    public UseOnContext(Level level, Player player, InteractionHand intHand, ItemStack itemStack, RaycastHit rayHit, bool hit = false)
    {
      this.player = player;
      this.hand = intHand;
      this.rayHit = rayHit;
      // this.HitResult = p_43717_;
      this.itemStack = itemStack;
      this.level = level;
      this.hit = hit;
    }

    // public virtual RaycastHit GetHitResult()
    // {
    //   return this.hitResult;
    // }

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

    // public virtual ItemStack GetItemInHand()
    // {
    //   return this.itemStack;
    // }

    // public virtual bool GetHit()
    // {
    //   return this.hit;
    // }

    // public virtual Player GetPlayer()
    // {
    //   return this.player;
    // }

    // public virtual InteractionHand GetHand()
    // {
    //   return this.hand;
    // }

    // public virtual Level GetLevel()
    // {
    //   return this.level;
    // }

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