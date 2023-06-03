using OpenTK.Mathematics;
using Sandbox.Common.NBT;
using Sandbox.Common.Registry;
using Sandbox.Common.Worlds.Entities;
using Sandbox.Common.Worlds.Levels;
using Sandbox.Common.Worlds.Tiles;
using Sandbox.Worlds.Items.Context;

namespace Sandbox.Common.Worlds.Items
{
  public class Item : ItemLike
  {
    protected static Guid BASE_ATTACK_DAMAGE_UUID;
    protected static Guid BASE_ATTACK_SPEED_UUID;
    public static int MAX_STACK_SIZE = 64;
    public static int EAT_DURATION = 32;


    private string? descriptionId;

    private object? renderProperties;





    public static int GetID(Item p_41394_)
    {
      return (p_41394_ == null) ? 0 : Registries.Get<Item>(BuiltInRegistries.ITEM).GetID(p_41394_);
    }

    public static Item ByID(int p_41446_)
    {
      return Registries.Get<Item>(BuiltInRegistries.ITEM).ByID(p_41446_);
    }

    public Item()
    {
      // this.rarity = p_41383_.rarity;
      // this.maxStackSize = p_41383_.maxStackSize;
      // this.foodProperties = p_41383_.foodProperties;
      // this.isFireResistant = p_41383_.isFireResistant;
      // this.requiredFeatures = p_41383_.requiredFeatures;
      // if (SharedConstants.IS_RUNNING_IN_IDE)
      // {
      //   string s = this.GetClass().getSimpleName();
      //   if (!s.EndsWith("Item"))
      //   {
      //     Debug.LogErrorFormat("Item classes should end with Item and {} doesn't.", s);
      //   }
      // }
      this.InitClient();
    }

    // public void OnDestroyed(ItemEntity p_150887_)
    // {
    // }

    public void VerifyTagAfterLoad(CompoundTag tag)
    {
    }

    public bool CanAttackTile(TileState tileState, Level level, Vector3 pos, Player player)
    {
      return true;
    }

    public Item AsItem()
    {
      return this;
    }

    public virtual InteractionResult UseOn(UseOnContext context)
    {
      return InteractionResult.PASS;
    }

    public float GetDestroySpeed(ItemStack itemStack, TileState tileState)
    {
      return 1.0f;
    }

    public virtual InteractionResultHolder<ItemStack> Use(Level level, Player player, InteractionHand hand)
    {
      ItemStack itemstack = player.GetItemInHand(hand);
      if (!itemstack.IsConsumable())
      {
        return InteractionResultHolder<ItemStack>.Pass(player.GetItemInHand(hand));
      }

      // if (p_41433_.CanEat(itemstack.GetFoodProperties(p_41433_).CanAlwaysEat()))
      // {
      //   p_41433_.StartUsingItem(p_41434_);
      //   return InteractionResultHolder<ItemStack>.Consume(itemstack);
      // }
      return InteractionResultHolder<ItemStack>.Fail(itemstack);
    }

    public ItemStack FinishUsingItem(ItemStack stack, Level level, LivingEntity livingEntity)
    {
      return stack;
      // return this.IsConsumable() ? p_41411_.Eat(p_41410_, p_41409_) : p_41409_;
    }


    // public bool canBeDepleted()
    // {
    //   return this.maxDamage > 0;
    // }

    // public bool isBarVisible(ItemStack p_150899_)
    // {
    //   return p_150899_.IsDamaged();
    // }

    // public int getBarWidth(ItemStack p_150900_)
    // {
    //   return Math.Round(13.0f - p_150900_.GetDamageValue() * 13.0f / this.getMaxDamage(p_150900_));
    // }

    // public int getBarColor(ItemStack p_150901_)
    // {
    //   float stackMaxDamage = (float)this.getMaxDamage(p_150901_);
    //   float f = Math.max(0.0f, (stackMaxDamage - p_150901_.getDamageValue()) / stackMaxDamage);
    //   return Mth.hsvToRgb(f / 3.0f, 1.0f, 1.0f);
    // }

    // public bool OverrideStackedOnOther(ItemStack p_150888_, Slot p_150889_, ClickAction p_150890_, Player p_150891_)
    // {
    //   return false;
    // }

    // public bool OverrideOtherStackedOnMe(ItemStack p_150892_, ItemStack p_150893_, Slot p_150894_, ClickAction p_150895_, Player p_150896_, SlotAccess p_150897_)
    // {
    //   return false;
    // }

    public bool HurtEnemy(ItemStack itemStack, LivingEntity attacker, LivingEntity victim)
    {
      return false;
    }

    public bool MineTile(ItemStack itemStack, Level level, TileState tileState, Vector3 pos, LivingEntity livingEntity)
    {
      return false;
    }

    public bool IsCorrectToolForDrops(TileState tileState)
    {
      return false;
    }

    public InteractionResult InteractLivingEntity(ItemStack itemStack, Player player, LivingEntity interationVictim, InteractionHand hand)
    {
      return InteractionResult.PASS;
    }

    // public Component getDescription()
    // {
    //   return Component.translatable(this.getDescriptionId());
    // }

    // @Override
    // TODO
    // public override string ToString()
    // {
    //   return Registries.Get<Item>(BuiltInRegistries.ITEM).GetKey(this).Path;
    // }

    protected string GetOrCreateDescriptionId()
    {
      if (this.descriptionId == null)
      {
        // TODO
        // this.descriptionId = Util.makeDescriptionId("item", BuiltInRegistries.ITEM.getKey(this));
      }
      return this.descriptionId!;
    }

    public string GetDescriptionId()
    {
      return this.GetOrCreateDescriptionId();
    }

    public string GetDescriptionId(ItemStack itemStack)
    {
      return this.GetDescriptionId();
    }

    public bool ShouldOverrideMultiplayerNbt()
    {
      return true;
    }


    public void InventoryTick(ItemStack itemStack, Level level, Entity entity, int p_41407_, bool p_41408_)
    {
    }

    public bool IsComplex()
    {
      return false;
    }

    // TODO
    // public UseAnim getUseAnimation(ItemStack p_41452_)
    // {
    //   return p_41452_.GetItem().IsConsumable() ? UseAnim.EAT : UseAnim.NONE;
    // }

    public int GetUseDuration(ItemStack itemStack)
    {
      // if (p_41454_.Item.IsConsumable())
      // {
      //   return p_41454_.GetFoodProperties(null).isFastFood() ? 16 : 32;
      // }
      return 0;
    }

    public void ReleaseUsing(ItemStack itemStack, Level level, LivingEntity user, int duration)
    {
    }

    // public void AppendHoverText(ItemStack p_41421_, Level p_41422_, List<Component> p_41423_, TooltipFlag p_41424_)
    // {
    // }

    // public TooltipComponent GetTooltipImage(ItemStack p_150902_)
    // {
    //   return null;
    // }

    // public Component getName(ItemStack p_41458_)
    // {
    //   return Component.translatable(this.getDescriptionId(p_41458_));
    // }

    // public bool isFoil(ItemStack p_41453_)
    // {
    //   return p_41453_.isEnchanted();
    // }

    // public Rarity getRarity(ItemStack p_41461_)
    // {
    //   if (!p_41461_.isEnchanted())
    //   {
    //     return this.rarity;
    //   }
    //   switch (this.rarity)
    //   {
    //     case COMMON:
    //     case UNCOMMON:
    //       {
    //         return Rarity.RARE;
    //       }
    //     case RARE:
    //       {
    //         return Rarity.EPIC;
    //       }
    //     default:
    //       {
    //         return this.rarity;
    //       }
    //   }
    // }

    // public bool isEnchantable(ItemStack p_41456_)
    // {
    //   return this.getMaxStackSize(p_41456_) == 1 && this.isDamageable(p_41456_);
    // }



    // protected static RaycastHit GetPlayerPOVHitResult(Level p_41436_, Player p_41437_, ClipContext.Fluid p_41438_)
    // {
    //   float f = p_41437_.getXRot();
    //   float f2 = p_41437_.getYRot();
    //   Vector3 Vector3 = p_41437_.getEyePosition();
    //   float f3 = Mth.cos(-f2 * 0.017453292f - 3.1415927f);
    //   float f4 = Mth.sin(-f2 * 0.017453292f - 3.1415927f);
    //   float f5 = -Mth.cos(-f * 0.017453292f);
    //   float f6 = Mth.sin(-f * 0.017453292f);
    //   float f7 = f4 * f5;
    //   float f8 = f3 * f5;
    //   double d0 = p_41437_.getReachDistance();
    //   Vector3 vec4 = Vector3.add(f7 * d0, f6 * d0, f8 * d0);
    //   return p_41436_.clip(new ClipContext(Vector3, vec4, ClipContext.Block.OUTLINE, p_41438_, p_41437_));
    // }

    public virtual int GetMaxStackSize()
    {
      return 64;
    }

    public bool UseOnRelease(ItemStack itemStack)
    {
      return false;
    }

    public ItemStack GetDefaultInstance()
    {
      return new ItemStack(this);
    }

    public bool IsConsumable()
    {
      return false;//this.foodProperties != null;
    }



    // public AudioSource getDrinkingSound()
    // {
    //   return SoundEvents.GENERIC_DRINK;
    // }

    // public AudioSource getEatingSound()
    // {
    //   return SoundEvents.GENERIC_EAT;
    // }

    // public bool isFireResistant()
    // {
    //   return this.isFireResistant;
    // }

    // public bool canBeHurtBy(DamageSource p_41387_)
    // {
    //   return !this.isFireResistant || !p_41387_.isFire();
    // }


    // public AudioSource getEquipSound()
    // {
    //   return null;
    // }

    public bool CanFitInsideContainerItems()
    {
      return true;
    }


    public object? GetRenderPropertiesInternal()
    {
      return this.renderProperties;
    }
    private void InitClient()
    {
      // if (FMLEnvironment.dist == Dist.CLIENT && !FMLLoader.getLaunchHandler().isData())
      // {
      //   this.initializeClient(properties =>
      //   {
      //     if (properties == this)
      //     {
      //       throw new IllegalStateException("Don't extend IItemRenderProperties in your item, use an anonymous class instead.");
      //     }
      //     else
      //     {
      //       this.renderProperties = properties;
      //     }
      //   });
      // }
    }

    // public void initializeClient(Action<IClientItemExtensions> consumer)
    // {
    // }

    static Item()
    {
      // BY_TILE = GameData.GetTileItemDictionary();
      BASE_ATTACK_DAMAGE_UUID = new Guid("CB3F55D3-645C-4F38-A497-9C13A33DB5CF");
      BASE_ATTACK_SPEED_UUID = new Guid("FA233E1C-4180-4865-B01B-BCCE9785ACA3");
    }



  }
}