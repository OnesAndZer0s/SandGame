

using OpenTK.Mathematics;
using Sandbox.Common.NBT;
using Sandbox.Common.Worlds.Levels;
using Sandbox.Common.Worlds.Players;
using Sandbox.Common.Worlds.Tiles;
using Sandbox.Worlds.Items.Context;

namespace Sandbox.Common.Worlds.Items
{
#nullable enable
  public class TileItem : Item
  {
    public Tile Tile { get; private set; }

    public TileItem(Tile tile)
    {
      this.Tile = tile;
    }

    public override InteractionResult UseOn(UseOnContext ctx)
    {
      if (ctx.Hit)
      {
        Vector3i placeTile = (Vector3i)(ctx.RayHit.point + (ctx.RayHit.normal * 0.5f));

        if (ctx.Level.Modify(placeTile, Tile))
        {
          // AudioManager.instance.dig.Play(TileTypes.digSound[TileToReplace], removeTile);
        }
        return InteractionResult.SUCCESS;
      }
      return InteractionResult.PASS;
    }


    // public new InteractionResult UseOn(UseOnContext p_40581_)
    // {
    //   InteractionResult interactionresult = this.Place(new TilePlaceContext(p_40581_));
    //   if (!interactionresult.ConsumesAction() && this.IsConsumable())
    //   {
    //     InteractionResult interactionresult2 = this.Use(p_40581_.GetLevel(), p_40581_.GetPlayer(), p_40581_.GetHand()).result;
    //     return (interactionresult2 == InteractionResult.CONSUME) ? InteractionResult.CONSUME_PARTIAL : interactionresult2;
    //   }
    //   return interactionresult;
    // }

    public InteractionResult Place(TilePlaceContext p_40577_)
    {
      // if (!this.tile.IsEnabled(p_40577_.GetLevel().EnabledFeatures()))
      // {
      //   return InteractionResult.FAIL;
      // }
      if (!p_40577_.CanPlace())
      {
        return InteractionResult.FAIL;
      }
      TilePlaceContext tpc = this.UpdatePlacementContext(p_40577_);
      if (tpc == null)
      {
        return InteractionResult.FAIL;
      }
      TileState ts = this.GetPlacementState(tpc);
      if (ts == null)
      {
        return InteractionResult.FAIL;
      }
      if (!this.PlaceTile(tpc, ts))
      {
        return InteractionResult.FAIL;
      }
      Vector3 tilePosition = tpc.RayHit.point;
      Level level = tpc.Level;
      Player player = tpc.Player;
      ItemStack itemstack = tpc.ItemStack;
      TileState ts2 = level.GetTileState(tilePosition);
      // if (ts2.Is(ts.Tile))
      // {
      //   ts2 = this.UpdateTileStateFromTag(tilePosition, level, itemstack, ts2);
      //   this.UpdateCustomTileEntityTag(tilePosition, level, player, itemstack, ts2);
      //   ts2.Tile.SetPlacedBy(level, tilePosition, ts2, player, itemstack);
      //   // if (player.GetType() == typeof(ServerPlayer))
      //   // {
      //   //   CriteriaTriggers.PLACED_Tile.trigger(player, tilePosition, itemstack);
      //   // }
      // }
      // level.GameEvent(GameEvent.Tile_PLACE, tilePosition, GameEvent.Context.of(player, ts2));
      // SoundType soundtype = ts2.GetSoundType(level, tilePosition, p_40577_.GetPlayer());
      // level.PlaySound(player, tilePosition, this.getPlaceSound(ts2, level, tilePosition, p_40577_.getPlayer()), SoundSource.TileS, (soundtype.getVolume() + 1.0f) / 2.0f, soundtype.getPitch() * 0.8f);
      // if (player == null || !player.GetAbilities().instabuild)
      // {
      //   itemstack.Shrink(1);
      // }
      return InteractionResult.SidedSuccess(level.IsClientSide());
    }


    protected bool PlaceTile(TilePlaceContext tPCtx, TileState tState)
    {
      return tPCtx.Level.SetTile(tPCtx.RayHit.point, tState, 11);
    }


    public TilePlaceContext UpdatePlacementContext(TilePlaceContext tPCtx)
    {
      return tPCtx;
    }

    protected TileState GetPlacementState(TilePlaceContext tPCtx)
    {
      TileState? blockstate = this.Tile.GetStateForPlacement(tPCtx);
      return (blockstate != null && this.CanPlace(tPCtx, blockstate)) ? blockstate : null;
    }

    // protected AudioSource GetPlaceSound(TileState state, Level world, Vector3 pos, Player entity)
    // {
    //   return state.GetSoundType(world, pos, entity).getPlaceSound();
    // }




    protected bool UpdateCustomTileEntityTag(Vector3 pos, Level level, Player player, ItemStack itemStack, TileState tState)
    {
      return UpdateCustomTileEntityTag(level, player, pos, itemStack);
    }


    // private TileState UpdateTileStateFromTag(Vector3 p_40603_, Level p_40604_, ItemStack p_40605_, TileState p_40606_)
    // {
    //   TileState Tilestate = p_40606_;
    //   CompoundTag compoundtag = p_40605_.GetTag();
    //   if (compoundtag != null)
    //   {
    //     CompoundTag compoundtag2 = compoundtag.Get<CompoundTag>("TileStateTag");
    //     StateDefinition<Tile, TileState> statedefinition = p_40606_.Tile.getStateDefinition();
    //     foreach (string s in compoundtag2.GetAllKeys())
    //     {
    //       Property<object> property = statedefinition.GetProperty(s);
    //       if (property != null)
    //       {
    //         string s2 = compoundtag2.Get(s).StringValue;
    //         Tilestate = UpdateState(Tilestate, property, s2);
    //       }
    //     }
    //   }
    //   if (Tilestate != p_40606_)
    //   {
    //     p_40604_.SetTile(p_40603_, Tilestate, 2);
    //   }
    //   return Tilestate;
    // }

    // private static TileState UpdateState<T>(TileState p_40594_, Property<T> p_40595_, string p_40596_) where T : IComparable<T>
    // {
    //   return p_40595_.GetValue(p_40596_).ForEach(p_40592_ => (p_40594_).SetValue(p_40595_, p_40592_)).orElse(p_40594_);
    // }

    protected bool CanPlace(TilePlaceContext tPCtx, TileState tState)
    {

      return true;
      // Player player = p_40611_.GetPlayer();
      // CollisionContext collisioncontext = (player == null) ? CollisionContext.Empty() : CollisionContext.of(player);
      // return (!this.MustSurvive() || p_40612_.CanSurvive(p_40611_.GetLevel(), p_40611_.GetClickedPos())) && p_40611_.GetLevel().IsUnobstructed(p_40612_, p_40611_.GetClickedPos(), collisioncontext);
    }

    protected bool MustSurvive()
    {
      return true;
    }

    public static bool UpdateCustomTileEntityTag(Level level, Player player, Vector3 pos, ItemStack itemStack)
    {
      // MinecraftServer minecraftserver = p_40583_.GetServer();
      // if (minecraftserver == null)
      // {
      //   return false;
      // }
      // CompoundTag compoundtag = GetTileEntityData(p_40586_);
      // if (compoundtag != null)
      // {
      //   TileEntity Tileentity = p_40583_.GetTileEntity(p_40585_);
      //   if (Tileentity != null)
      //   {
      //     if (!p_40583_.IsClientSide && Tileentity.OnlyOpCanSetNbt() && (p_40584_ == null || !p_40584_.canUseGameMasterTiles()))
      //     {
      //       return false;
      //     }
      //     CompoundTag compoundtag2 = Tileentity.SaveWithoutMetadata();
      //     CompoundTag compoundtag3 = compoundtag2.Copy();
      //     compoundtag2.Merge(compoundtag);
      //     if (!compoundtag2.Equals(compoundtag3))
      //     {
      //       Tileentity.Load(compoundtag2);
      //       Tileentity.SetChanged();
      //       return true;
      //     }
      //   }
      // }
      return false;
    }


    public new string GetDescriptionId()
    {
      return "TODO";
      // return this.tile.GetDescriptionId();
    }


    // public void AppendHoverText(ItemStack p_40572_, Level p_40573_, List<Component> p_40574_, TooltipFlag p_40575_)
    // {
    //   // base.AppendHoverText(p_40572_, p_40573_, p_40574_, p_40575_);
    //   // this.tile.AppendHoverText(p_40572_, p_40573_, p_40574_, p_40575_);
    // }



    public void RegisterTiles(Dictionary<Tile, Item> registry, Item item)
    {
      registry.Add(this.Tile, item);
    }

    public void RemoveFromTileToItemMap(Dictionary<Tile, Item> TileToItemMap, Item itemIn)
    {
      TileToItemMap.Remove(this.Tile);
    }

    // public void OnDestroyed(ItemEntity p_150700_)
    // {

    // }


    public static CompoundTag? GetTileEntityData(ItemStack itemStack)
    {
      return itemStack.GetTagElement("TileEntityTag");
    }

    // public static void SetTileEntityData(ItemStack p_186339_, TileEntity p_186340_, CompoundTag p_186341_)
    // {
    //   if (p_186341_.IsEmpty())
    //   {
    //     p_186339_.RemoveTagKey("TileEntityTag");
    //   }
    //   else
    //   {
    //     TileEntity.AddEntity(p_186341_, p_186340_);
    //     p_186339_.AddTagElement("TileEntityTag", p_186341_);
    //   }
    // }


    // public FeatureFlagSet RequiredFeatures()
    // {
    //   return this.tile.RequiredFeatures();
    // }
  }

}