

using OpenTK.Mathematics;
using Sandbox.Common.Maths.Raycasts;
using Sandbox.Common.Worlds;
using Sandbox.Common.Worlds.Items;
using Sandbox.Common.Worlds.Levels;
using Sandbox.Common.Worlds.Players;

namespace Sandbox.Worlds.Items.Context
{
  public class TilePlaceContext : UseOnContext
  {
    private Vector3 relativePos;
    // protected bool replaceClicked;

    public TilePlaceContext(Player player, InteractionHand intHand, ItemStack itemStack, RaycastHit rayHit) : this(player.level, player, intHand, itemStack, rayHit)
    { }


    public TilePlaceContext(UseOnContext useOnCtx) : this(useOnCtx.level, useOnCtx.player, useOnCtx.hand, useOnCtx.itemStack, useOnCtx.rayHit)
    { }

    public TilePlaceContext(Level level, Player player, InteractionHand intHand, ItemStack itemStack, RaycastHit rayHit) : base(level, player, intHand, itemStack, rayHit)
    {
      // this.replaceClicked = true;
      // this.relativePos = p_43642_.point + p_43642_.normal;
      // this.replaceClicked = p_43638_.GetTileState(p_43642_.point).CanBeReplaced(this);
    }

    // public static TilePlaceContext At(TilePlaceContext p_43645_, Vector3 p_43646_, Vector3 p_43647_)
    // {
    //   return new TilePlaceContext(p_43645_.GetLevel(), p_43645_.GetPlayer(), p_43645_.GetHand(), p_43645_.GetItemInHand(), new TileHitResult(new Vector3(p_43646_.x + 0.5f + p_43647_.GetStepX() * 0.5f, p_43646_.y + 0.5f + p_43647_.GetStepY() * 0.5f, p_43646_.z + 0.5f + p_43647_.GetStepZ() * 0.5f), p_43647_, p_43646_, false));
    // }

    public virtual bool CanPlace()
    {
      return true;
      // return this.GetLevel().GetTileState(this.GetHitResult().point).CanBeReplaced(this);
    }

    // public virtual bool ReplacingClickedOnBlock()
    // {
    //   return this.replaceClicked;
    // }

    // public virtual Vector3 GetNearestLookingDirection()
    // {
    //   return Vector3.OrderedByNearest(this.GetPlayer())[0];
    // }

    // public virtual Vector3[] GetNearestLookingDirections()
    // {
    //   Vector3[] adirection = Vector3.OrderedByNearest(this.GetPlayer());
    //   if (this.replaceClicked)
    //   {
    //     return adirection;
    //   }
    //   Vector3 direction;
    //   int i;
    //   for (direction = this.GetHitResult().normal, i = 0; i < adirection.Length && adirection[i] != direction.GetOpposite(); ++i) { }
    //   if (i > 0)
    //   {

    //     Array.Copy(adirection, 0, adirection, 1, i);
    //     adirection[0] = direction * -1;
    //   }
    //   return adirection;
    // }
  }
}