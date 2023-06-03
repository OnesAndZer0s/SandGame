// using Sandbox.Core;
// using UnityEngine;

// namespace Sandbox.Worlds.Items.Context
// {

//   public class DirectionalPlaceContext : TilePlaceContext
//   {
//     private Vector3 direction;

//     public DirectionalPlaceContext(Level p_43650_, Vector3 p_43651_, Vector3 p_43652_, ItemStack p_43653_, Vector3 p_43654_) : base(p_43650_, null, InteractionHand.MAIN_HAND, p_43653_, new BlockHitResult(Vector3.atBottomCenterOf(p_43651_), p_43654_, p_43651_, false))
//     {
//       this.direction = p_43652_;
//     }


//     public override Vector3 GetClickedPos()
//     {
//       return this.GetHitResult().GetTilePos();
//     }


//     public override bool CanPlace()
//     {
//       return this.GetLevel().GetTileState(this.GetHitResult().GetTilePos()).CanBeReplaced(this);
//     }

//     public override bool ReplacingClickedOnBlock()
//     {
//       return this.CanPlace();
//     }


//     public override Vector3 GetNearestLookingDirection()
//     {
//       return Vector3.DOWN;
//     }


//     public override Vector3[] GetNearestLookingDirections()
//     {
//       switch (this.direction)
//       {
//         default:
//           {
//             return new Vector3[] { Vector3.Down, Vector3.NORTH, Vector3.EAST, Vector3.SOUTH, Vector3.WEST, Vector3.UP };
//           }
//         case UP:
//           {
//             return new Vector3[] { Vector3.Down, Vector3.UP, Vector3.NORTH, Vector3.EAST, Vector3.SOUTH, Vector3.WEST };
//           }
//         case NORTH:
//           {
//             return new Vector3[] { Vector3.Down, Vector3.NORTH, Vector3.EAST, Vector3.WEST, Vector3.UP, Vector3.SOUTH };
//           }
//         case SOUTH:
//           {
//             return new Vector3[] { Vector3.Down, Vector3.SOUTH, Vector3.EAST, Vector3.WEST, Vector3.UP, Vector3.NORTH };
//           }
//         case WEST:
//           {
//             return new Vector3[] { Vector3.Down, Vector3.WEST, Vector3.SOUTH, Vector3.UP, Vector3.NORTH, Vector3.EAST };
//           }
//         case EAST:
//           {
//             return new Vector3[] { Vector3.Down, Vector3.EAST, Vector3.SOUTH, Vector3.UP, Vector3.NORTH, Vector3.WEST };
//           }
//       }
//     }

//     public override Vector3 GetHorizontalDirection()
//     {
//       return (this.direction.GetAxis() == Vector3.Axis.Y) ? Vector3.NORTH : this.direction;
//     }


//     public override bool IsSecondaryUseActive()
//     {
//       return false;
//     }

//     public override float GetRotation()
//     {
//       return (float)(this.direction.Get2DDataValue() * 90);
//     }
//   }
// }