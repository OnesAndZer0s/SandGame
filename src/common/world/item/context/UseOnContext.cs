
using Sandbox.Common.Maths.Raycasts;
using Sandbox.Common.Worlds;
using Sandbox.Common.Worlds.Items;
using Sandbox.Common.Worlds.Levels;
using Sandbox.Common.Worlds.Players;

namespace Sandbox.Worlds.Items.Context
{
  public class UseOnContext
  {
    public Player Player { get; private set; }
    public InteractionHand Hand { get; private set; }
    public Level Level { get; private set; }
    public RaycastHit RayHit { get; private set; }
    public ItemStack ItemStack { get; private set; }
    public bool Hit { get; private set; }

    public UseOnContext(Player player, InteractionHand intHand, RaycastHit raycastHit) :
      this(player.Level, player, intHand, player.GetItemInHand(intHand), raycastHit)
    {
    }

    public UseOnContext(Level level, Player player, InteractionHand intHand, ItemStack itemStack, RaycastHit rayHit, bool hit = false)
    {
      this.Player = player;
      this.Hand = intHand;
      this.RayHit = rayHit;
      // this.HitResult = p_43717_;
      this.ItemStack = itemStack;
      this.Level = level;
      this.Hit = hit;
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
      return this.Player != null && this.Player.IsSecondaryUseActive();
    }

    public virtual float GetRotation()
    {
      return (this.Player == null) ? 0.0f : this.Player.LookingRotation.y;
    }
  }

}