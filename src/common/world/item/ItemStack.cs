
// https://nekoyue.github.io/ForgeJavaDocs-NG/javadoc/1.19.3/net/minecraft/world/item/ItemStack.html

using System.Diagnostics;
using Sandbox.Common.NBT;
using Sandbox.Common.Registry;
using Sandbox.Common.Worlds.Tiles;
using Sandbox.Common.Worlds.Entities;
using Sandbox.Common.Worlds.Levels;

namespace Sandbox.Common.Worlds.Items
{
  public class ItemStack : ItemLike
  {

    // @Nullable
    // private AdventureModeCheck adventureBreakCheck;
    // @Nullable
    // private AdventureModeCheck adventurePlaceCheck;

    public static readonly ItemStack EMPTY = new ItemStack((Item)null!, 0);

    private Item? item;
    private int count;

    public int PopTime { get; set; }
    public Entity? EntityRepresentation { get; set; }
    private bool EmptyCacheFlag { get; set; }

    // private CompoundTag CapNBT;
    private CompoundTag? Tag;

    public ItemStack(Item? item, int count)
    {
      this.item = item;
      this.count = count;
    }
    public ItemStack(Item? item)
    {
      this.item = item;
      this.count = 1;
    }

    public ItemStack(ItemLike? itemLike) : this(itemLike, 1)
    { }


    public ItemStack(ItemLike? itemLike, int count) : this(itemLike, count, null)
    { }

    public ItemStack(ItemLike? itemLike, int count, CompoundTag? tags)
    {
      //   super(ItemStack.class, true);
      //     this.capNBT = p_41606_;
      //     this.item = ((p_41604_ == null) ? null : p_41604_.AsItem());
      //     this.delegate = ((p_41604_ == null) ? null : ForgeRegistries.ITEMS.getDelegateOrThrow(p_41604_.asItem()));
      //     this.count = p_41605_;
      //     this.forgeInit();
      //     if (this.item != null && this.item.isDamageable(this)) {
      // }
      //     this.UpdateEmptyCacheFlag();
    }

    private void UpdateEmptyCacheFlag()
    {
      this.EmptyCacheFlag = false;
      this.EmptyCacheFlag = this.IsEmpty();
    }

    public int GetCount()
    {
      return this.count;
    }

    public void SetCount(int newCount)
    {
      Debug.Print("SetCount to " + newCount);
      this.count = Math.Clamp(newCount, 0, this.GetMaxStackSize());
      if (this.count == 0)
      {
        Debug.Print("SetCount has to be between 0 and max stack size");

        this.item = ItemStack.EMPTY.GetItem();
      }

      this.UpdateEmptyCacheFlag();
    }

    public void Set(Item item, int count)
    {
      this.item = item;
      this.count = count;

      if (this.count == 0 || this.item == null)
      {
        Debug.Print("SetCount has to be between 0 and max stack size");

        this.item = ItemStack.EMPTY.GetItem();
        this.count = ItemStack.EMPTY.GetCount();
      }
    }

    public void Grow(int byCount)
    {
      this.SetCount(this.count + byCount);
    }

    public void Shrink(int byCount)
    {
      this.Grow(-byCount);
    }

    public Item? GetItem()
    {
      return this.item;
    }

    public void SetItem(Item newItem)
    {
      Debug.Print("SetItem to " + newItem);
      this.item = newItem;
      if (this.item == null)
      {
        Debug.Print("SetItem is null");
        this.count = ItemStack.EMPTY.GetCount();
      }
      this.UpdateEmptyCacheFlag();
    }

    private ItemStack(CompoundTag compTag)
    {
      //   super(ItemStack.class, true);
      //       this.CapNBT = (p_41608_.contains("ForgeCaps") ? p_41608_.getCompound("ForgeCaps") : null);
      Item item = Registries.Get<Item>(BuiltInRegistries.ITEM)[new ResourceLocation(compTag.Get<StringTag>("id")!.Value)];
      this.Set(item, compTag.Get<ByteTag>("Count")!.Value);
      // Item rawItem = item;
      // this.delegate = ForgeRegistries.ITEMS.getDelegateOrThrow(rawItem);
      // if (p_41608_.contains("Tag", 10))
      // {
      //   this.Tag = p_41608_.getCompound("Tag");
      //   this.Item.verifyTagAfterLoad(this.Tag);
      // }
      // this.forgeInit();
      // if (this.Item.isDamageable(this))
      // {
      //   this.setDamageValue(this.getDamageValue());
      // }
      // this.UpdateEmptyCacheFlag();
    }


    public static ItemStack Of(CompoundTag compTag)
    {
      try
      {
        return new ItemStack(compTag);
      }
      catch (Exception runtimeexception)
      {
        Debug.Fail("Tried to load invalid item: " + compTag);
        return ItemStack.EMPTY;
      }
    }

    public bool IsEmpty()
    {
      return this == ItemStack.EMPTY || this.GetItem() == null || this.GetCount() <= 0;
    }

    // public bool IsItemEnabled(FeatureFlagSet p_250869_)
    // {
    //   return this.IsEmpty() || this.Item.IsEnabled(p_250869_);
    // }

    public ItemStack Split(int byCount)
    {
      int i = Math.Min(byCount, this.GetCount());
      ItemStack itemstack = this.Copy();
      itemstack.SetCount(i);
      this.Shrink(i);
      return itemstack;
    }




    // public bool Is(TagKey<Item> p_204118_)
    // {
    //   return this.Item.BuiltInRegistryHolder().Is(p_204118_);
    // }

    public bool Is(Item sameItem)
    {
      return this.GetItem() == sameItem;
    }

    // public bool Is(Func</*buildinRegistryHolder, */Item> p_220168_)
    // {
    //   return this.Item == p_220168_(this.Item.BuiltInRegistryHolder());
    // }

    // public bool Is(Holder<Item> p_220166_)
    // {
    //   return this.Item.builtInRegistryHolder() == p_220166_;
    // }

    // public List<TagKey<Item>> GetTags()
    // {
    //   return this.Item.BuiltInRegistryHolder().tags();
    // }

    // public InteractionResult UseOn(UseOnContext p_41662_)
    // {
    //   if (!p_41662_.GetLevel().isClientSide)
    //   {
    //     return ForgeHooks.onPlaceItemIntoWorld(p_41662_);
    //   }
    //   return this.OnItemUse(p_41662_, c => this.Item.UseOn(p_41662_));
    // }


    // public InteractionResult OnItemUseFirst(UseOnContext p_41662_)
    // {
    //   return this.OnItemUse(p_41662_, c => this.Item.OnItemUseFirst(this, p_41662_));
    // }

    // private InteractionResult OnItemUse(UseOnContext p_41662_, Func<UseOnContext, InteractionResult> callback)
    // {
    // Player player = p_41662_.GetPlayer();
    // Vector3 Vector3 = p_41662_.GetClickedPos();
    // BlockInWorld blockinworld = new BlockInWorld(p_41662_.GetLevel(), Vector3, false);
    // if (player != null && !player.GetAbilities().mayBuild && !this.HasAdventureModePlaceTagForBlock(p_41662_.getLevel().registryAccess().registryOrThrow((ResourceKey <? extends Registry <? extends Block >>)Registries.BLOCK), blockinworld))
    // {
    //   return InteractionResult.PASS;
    // }
    // Item item = this.Item;
    // InteractionResult interactionresult = callback.apply(p_41662_);
    // if (player != null && interactionresult.shouldAwardStats())
    // {
    //   // player.awardStat(Stats.ITEM_USED.get(item));
    // }
    // return interactionresult;
    // }

    public float GetDestroySpeed(TileState tileState)
    {
      return this.GetItem()!.GetDestroySpeed(this, tileState);
    }

    // public InteractionResult Use(Level p_41683_, Player p_41684_, InteractionHand p_41685_)
    // {
    //   return this.Item.Use(p_41683_, p_41684_, p_41685_);
    // }

    // public ItemStack FinishUsingItem(Level p_41672_, LivingEntity p_41673_)
    // {
    //   return this.Item.FinishUsingItem(this, p_41672_, p_41673_);
    // }


    // public bool Is(Predicate<Holder<Item>> p_220168_)
    // {
    //   return p_220168_.test(Item.builtInRegistryHolder());
    // }

    // public bool Is(Holder<Item> p_220166_)
    // {
    //   return (Item.builtInRegistryHolder() == p_220166_);
    // }

    public CompoundTag Save(CompoundTag compTag)
    {
      ResourceLocation? resourcelocation = Registries.Get<Item>(BuiltInRegistries.ITEM).GetKey(GetItem()!);
      compTag.Add(new StringTag("id", (resourcelocation == null) ? "core:air" : resourcelocation.ToString()));
      compTag.Add(new ByteTag("Count", (byte)this.GetCount()));
      // if (this.Tag != null)
      // {
      //   p_41740_.Add(new CompoundTag("Tag", this.Tag));
      // }

      // CompoundTag cnbt = SerializeCaps();
      // if (cnbt != null && !cnbt.IsEmpty())
      // {
      //   p_41740_.Add(cnbt);
      // }
      return compTag;
    }
    public int GetMaxStackSize()
    {
      if (this.IsEmpty())
      {
        return 64;
      }
      return this.GetItem()!.GetMaxStackSize();
    }

    public bool IsStackable()
    {
      return this.GetMaxStackSize() > 1;
    }

    // public bool OverrideStackedOnOther(Slot p_150927_, ClickAction p_150928_, Player p_150929_)
    // {
    //   return this.Item.OverrideStackedOnOther(this, p_150927_, p_150928_, p_150929_);
    // }

    // public bool OverrideOtherStackedOnMe(ItemStack p_150933_, Slot p_150934_, ClickAction p_150935_, Player p_150936_, SlotAccess p_150937_)
    // {
    //   return this.Item.OverrideOtherStackedOnMe(this, p_150933_, p_150934_, p_150935_, p_150936_, p_150937_);
    // }

    // TODO

    // public void HurtEnemy(LivingEntity p_41641_, Player p_41642_)
    // {
    //   Item item = this.Item;
    //   if (item.HurtEnemy(this, p_41641_, p_41642_))
    //   {
    //     // p_41642_.awardStat(Stats.ITEM_USED.get(item));
    //   }
    // }

    // public void MineBlock(Level p_41687_, TileState p_41688_, Vector3 p_41689_, Player p_41690_)
    // {
    //   Item item = this.Item;
    //   if (item.MineBlock(this, p_41687_, p_41688_, p_41689_, p_41690_))
    //   {
    //     // p_41690_.awardStat(Stats.ITEM_USED.get(item));
    //   }
    // }

    // public bool IsCorrectToolForDrops(TileState p_41736_)
    // {
    //   return this.Item.IsCorrectToolForDrops(this, p_41736_);
    // }

    // TODO
    // public InteractionResult InteractLivingEntity(Player p_41648_, LivingEntity p_41649_, InteractionHand p_41650_)
    // {
    //   return this.Item.InteractLivingEntity(this, p_41648_, p_41649_, p_41650_);
    // }

    public ItemStack Copy()
    {
      if (this.IsEmpty())
      {
        return ItemStack.EMPTY;
      }
      ItemStack itemstack = new ItemStack(this.GetItem(), this.GetCount());
      itemstack.PopTime = this.PopTime;
      if (this.Tag != null)
      {
        itemstack.Tag = new CompoundTag(this.Tag);
      }
      return itemstack;
    }

    public ItemStack CopyWithCount(int newCount)
    {
      ItemStack itemstack = this.Copy();
      itemstack.SetCount(newCount);
      return itemstack;
    }

    public static bool TagMatches(ItemStack firstItem, ItemStack secondItem)
    {
      return (firstItem.IsEmpty() && secondItem.IsEmpty()) ||
      (!firstItem.IsEmpty() && !secondItem.IsEmpty() &&
      (firstItem.Tag != null || secondItem.Tag == null) &&
      (firstItem.Tag == null || firstItem.Tag.Equals(secondItem.Tag)) //&&
      // p_41659_.AreCompatible(p_41660_)
      );
    }

    public static bool Matches(ItemStack firstItem, ItemStack secondItem)
    {
      return (firstItem.IsEmpty() && secondItem.IsEmpty()) ||
      (!firstItem.IsEmpty() && !secondItem.IsEmpty() &&
      firstItem.Matches(secondItem));
    }

    private bool Matches(ItemStack otherItem)
    {
      return this.GetCount() == otherItem.GetCount() &&
      this.Is(otherItem.GetItem()!) &&
      (this.Tag != null || otherItem.Tag == null) &&
      (this.Tag == null || this.Tag.Equals(otherItem.Tag))
      // && 
      // this.AreCompatible(p_41745_)
      ;
    }

    public static bool IsSame(ItemStack firstItem, ItemStack secondItem)
    {
      return firstItem == secondItem || (!firstItem.IsEmpty() && !secondItem.IsEmpty() && firstItem.SameItem(secondItem));
    }

    public bool SameItem(ItemStack otherItem)
    {
      return !otherItem.IsEmpty() && this.Is(otherItem.GetItem()!);
    }

    public static bool IsSameItemSameTags(ItemStack firstItem, ItemStack secondItem)
    {
      return firstItem.Is(secondItem.GetItem()!) && TagMatches(firstItem, secondItem);
    }

    public string GetDescriptionId()
    {
      return this.GetItem()!.GetDescriptionId(this);
    }


    public override string ToString()
    {
      return this.GetCount() + " " + this.GetItem();
    }

    public void InventoryTick(Level level, Entity entity, int p_41669_, bool p_41670_)
    {
      if (this.PopTime > 0)
      {
        --this.PopTime;
      }
      if (this.GetItem() != null)
      {
        this.GetItem()!.InventoryTick(this, level, entity, p_41669_, p_41670_);
      }
    }

    public int GetUseDuration()
    {
      return this.GetItem()!.GetUseDuration(this);
    }

    // public UseAnim GetUseAnimation()
    // {
    //   return this.Item.GetUseAnimation(this);
    // }

    public void ReleaseUsing(Level level, LivingEntity livingEntity, int p_41677_)
    {
      this.GetItem()!.ReleaseUsing(this, level, livingEntity, p_41677_);
    }

    public bool UseOnRelease()
    {
      return this.GetItem()!.UseOnRelease(this);
    }

    public bool HasTag()
    {
      return !this.EmptyCacheFlag && this.Tag != null && !this.Tag.IsEmpty();
    }

    public CompoundTag? GetTag()
    {
      return this.Tag;
    }

    public CompoundTag GetOrCreateTag()
    {
      if (this.Tag == null)
      {
        this.SetTag(new CompoundTag());
      }
      return this.Tag!;
    }

    public CompoundTag GetOrCreateTagElement(string tagElement)
    {
      if (this.Tag != null && this.Tag.Contains(tagElement))
      {
        return this.Tag.Get<CompoundTag>(tagElement)!;
      }
      CompoundTag compoundtag = new CompoundTag();
      this.AddTagElement(tagElement, compoundtag);
      return compoundtag;
    }

    public CompoundTag? GetTagElement(string tagElement)
    {
      return (this.Tag != null && this.Tag.Contains(tagElement)) ? this.Tag.Get<CompoundTag>(tagElement) : null;
    }

    public void RemoveTagKey(string tagKey)
    {
      if (this.Tag != null && this.Tag.Contains(tagKey))
      {
        this.Tag.Remove(tagKey);
        if (this.Tag.IsEmpty())
        {
          this.Tag = null;
        }
      }
    }


    public void SetTag(CompoundTag tag)
    {
      this.Tag = tag;

      if (tag != null)
      {
        this.GetItem()!.VerifyTagAfterLoad(tag);
      }
    }

    // public Component GetHoverName()
    // {
    //   CompoundTag compoundtag = this.getTagElement("display");
    //   if (compoundtag != null && compoundtag.contains("Name", 8))
    //   {
    //     try
    //     {
    //       Component component = Component.Serializer.fromJson(compoundtag.getString("Name"));
    //       if (component != null)
    //       {
    //         return component;
    //       }
    //       compoundtag.remove("Name");
    //     }
    //     catch (Exception exception)
    //     {
    //       compoundtag.remove("Name");
    //     }
    //   }
    //   return this.Item.getName(this);
    // }

    // public ItemStack SetHoverName(Component p_41715_)
    // {
    //   CompoundTag compoundtag = this.GetOrCreateTagElement("display");
    //   if (p_41715_ != null)
    //   {
    //     compoundtag.putString("Name", Component.Serializer.toJson(p_41715_));
    //   }
    //   else
    //   {
    //     compoundtag.remove("Name");
    //   }
    //   return this;
    // }

    // public void resetHoverName()
    // {
    //   CompoundTag compoundtag = this.getTagElement("display");
    //   if (compoundtag != null)
    //   {
    //     compoundtag.remove("Name");
    //     if (compoundtag.IsEmpty())
    //     {
    //       this.removeTagKey("display");
    //     }
    //   }
    //   if (this.Tag != null && this.Tag.IsEmpty())
    //   {
    //     this.Tag = null;
    //   }
    // }

    public bool HasCustomHoverName()
    {
      CompoundTag? compoundtag = this.GetTagElement("display");
      return compoundtag != null && compoundtag.Contains("Name");
    }
    // public List<Component> GetTooltipLines(Player p_41652_, TooltipFlag p_41653_)
    // {
    //   List<Component> list = Lists.newArrayList();
    //   MutableComponent mutablecomponent = Component.empty().append(this.getHoverName()).withStyle(this.getRarity().getStyleModifier());
    //   if (this.hasCustomHoverName())
    //   {
    //     mutablecomponent.withStyle(ChatFormatting.ITALIC);
    //   }
    //   list.add(mutablecomponent);
    //   if (!p_41653_.isAdvanced() && !this.hasCustomHoverName() && this.is (Items.FILLED_MAP))
    //   {
    //     Integer integer = MapItem.getMapId(this);
    //     if (integer != null)
    //     {
    //       list.add(Component.literal("#" + integer).withStyle(ChatFormatting.GRAY));
    //     }
    //   }
    //   int j = this.getHideFlags();
    //   if (shouldShowInTooltip(j, TooltipPart.ADDITIONAL))
    //   {
    //     this.Item.appendHoverText(this, (p_41652_ == null) ? null : p_41652_.level, list, p_41653_);
    //   }
    //   if (this.hasTag())
    //   {
    //     if (shouldShowInTooltip(j, TooltipPart.ENCHANTMENTS))
    //     {
    //       appendEnchantmentNames(list, this.getEnchantmentTags());
    //     }
    //     if (this.Tag.contains("display", 10))
    //     {
    //       CompoundTag compoundtag = this.Tag.getCompound("display");
    //       if (shouldShowInTooltip(j, TooltipPart.DYE) && compoundtag.contains("color", 99))
    //       {
    //         if (p_41653_.isAdvanced())
    //         {
    //           list.add(Component.translatable("item.color", string.format(Locale.ROOT, "#%06X", compoundtag.getInt("color"))).withStyle(ChatFormatting.GRAY));
    //         }
    //         else
    //         {
    //           list.add(Component.translatable("item.dyed").withStyle(ChatFormatting.GRAY, ChatFormatting.ITALIC));
    //         }
    //       }
    //       if (compoundtag.getTagType("Lore") == 9)
    //       {
    //         ListTag listtag = compoundtag.getList("Lore", 8);
    //         for (int i = 0; i < listtag.size(); ++i)
    //         {
    //           string s = listtag.getString(i);
    //           try
    //           {
    //             MutableComponent mutablecomponent2 = Component.Serializer.fromJson(s);
    //             if (mutablecomponent2 != null)
    //             {
    //               list.add(ComponentUtils.mergeStyles(mutablecomponent2, ItemStack.LORE_STYLE));
    //             }
    //           }
    //           catch (Exception exception)
    //           {
    //             compoundtag.remove("Lore");
    //           }
    //         }
    //       }
    //     }
    //   }
    //   if (shouldShowInTooltip(j, TooltipPart.MODIFIERS))
    //   {
    //     EquipmentSlot[] values = EquipmentSlot.values();
    //     for (int length = values.length, n = 0; n < length; ++n)
    //     {
    //       EquipmentSlot equipmentslot = values[n];
    //       Multimap<Attribute, AttributeModifier> multimap = this.getAttributeModifiers(equipmentslot);
    //       if (!multimap.IsEmpty())
    //       {
    //         list.add(CommonComponents.EMPTY);
    //         list.add(Component.translatable("item.modifiers." + equipmentslot.getName()).withStyle(ChatFormatting.GRAY));
    //         for (Map.Entry<Attribute, AttributeModifier> entry : multimap.entries())
    //         {
    //           AttributeModifier attributemodifier = entry.getValue();
    //           double d0 = attributemodifier.getAmount();
    //           bool flag = false;
    //           if (p_41652_ != null)
    //           {
    //             if (attributemodifier.getId() == Item.BASE_ATTACK_DAMAGE_UUID)
    //             {
    //               d0 += p_41652_.getAttributeBaseValue(Attributes.ATTACK_DAMAGE);
    //               d0 += EnchantmentHelper.getDamageBonus(this, MobType.UNDEFINED);
    //               flag = true;
    //             }
    //             else if (attributemodifier.getId() == Item.BASE_ATTACK_SPEED_UUID)
    //             {
    //               d0 += p_41652_.getAttributeBaseValue(Attributes.ATTACK_SPEED);
    //               flag = true;
    //             }
    //           }
    //           double d2;
    //           if (attributemodifier.getOperation() != AttributeModifier.Operation.MULTIPLY_BASE && attributemodifier.getOperation() != AttributeModifier.Operation.MULTIPLY_TOTAL)
    //           {
    //             if (entry.getKey().equals(Attributes.KNOCKBACK_RESISTANCE))
    //             {
    //               d2 = d0 * 10.0;
    //             }
    //             else
    //             {
    //               d2 = d0;
    //             }
    //           }
    //           else
    //           {
    //             d2 = d0 * 100.0;
    //           }
    //           if (flag)
    //           {
    //             list.add(Component.literal(" ").append(Component.translatable("attribute.modifier.equals." + attributemodifier.getOperation().toValue(), ItemStack.ATTRIBUTE_MODIFIER_FORMAT.format(d2), Component.translatable(entry.getKey().getDescriptionId()))).withStyle(ChatFormatting.DARK_GREEN));
    //           }
    //           else if (d0 > 0.0)
    //           {
    //             list.add(Component.translatable("attribute.modifier.plus." + attributemodifier.getOperation().toValue(), ItemStack.ATTRIBUTE_MODIFIER_FORMAT.format(d2), Component.translatable(entry.getKey().getDescriptionId())).withStyle(ChatFormatting.BLUE));
    //           }
    //           else
    //           {
    //             if (d0 >= 0.0)
    //             {
    //               continue;
    //             }
    //             d2 *= -1.0;
    //             list.add(Component.translatable("attribute.modifier.take." + attributemodifier.getOperation().toValue(), ItemStack.ATTRIBUTE_MODIFIER_FORMAT.format(d2), Component.translatable(entry.getKey().getDescriptionId())).withStyle(ChatFormatting.RED));
    //           }
    //         }
    //       }
    //     }
    //   }
    //   if (this.hasTag())
    //   {
    //     if (shouldShowInTooltip(j, TooltipPart.UNBREAKABLE) && this.Tag.getbool("Unbreakable"))
    //     {
    //       list.add(Component.translatable("item.unbreakable").withStyle(ChatFormatting.BLUE));
    //     }
    //     if (shouldShowInTooltip(j, TooltipPart.CAN_DESTROY) && this.Tag.contains("CanDestroy", 9))
    //     {
    //       ListTag listtag2 = this.Tag.getList("CanDestroy", 8);
    //       if (!listtag2.IsEmpty())
    //       {
    //         list.add(CommonComponents.EMPTY);
    //         list.add(Component.translatable("item.canBreak").withStyle(ChatFormatting.GRAY));
    //         for (int k = 0; k < listtag2.size(); ++k)
    //         {
    //           list.addAll(expandBlockState(listtag2.getString(k)));
    //         }
    //       }
    //     }
    //     if (shouldShowInTooltip(j, TooltipPart.CAN_PLACE) && this.Tag.contains("CanPlaceOn", 9))
    //     {
    //       ListTag listtag3 = this.Tag.getList("CanPlaceOn", 8);
    //       if (!listtag3.IsEmpty())
    //       {
    //         list.add(CommonComponents.EMPTY);
    //         list.add(Component.translatable("item.canPlace").withStyle(ChatFormatting.GRAY));
    //         for (int l = 0; l < listtag3.size(); ++l)
    //         {
    //           list.addAll(expandBlockState(listtag3.getString(l)));
    //         }
    //       }
    //     }
    //   }
    //   if (p_41653_.isAdvanced())
    //   {
    //     if (this.isDamaged())
    //     {
    //       list.add(Component.translatable("item.durability", this.getMaxDamage() - this.getDamageValue(), this.getMaxDamage()));
    //     }
    //     list.add(Component.literal(BuiltInRegistries.ITEM.getKey(this.Item).toString()).withStyle(ChatFormatting.DARK_GRAY));
    //     if (this.hasTag())
    //     {
    //       list.add(Component.translatable("item.nbt_tags", this.Tag.getAllKeys().size()).withStyle(ChatFormatting.DARK_GRAY));
    //     }
    //   }
    //   if (p_41652_ != null && !this.Item.isEnabled(p_41652_.getLevel().enabledFeatures()))
    //   {
    //     list.add(ItemStack.DISABLED_ITEM_TOOLTIP);
    //   }
    //   ForgeEventFactory.onItemTooltip(this, p_41652_, list, p_41653_);
    //   return list;
    // }


    public void AddTagElement(string tagElement, NBTTag tag)
    {
      // TODO
      // this.GetOrCreateTag().Add(p_41701_, p_41702_);
    }

    // public Multimap<Attribute, AttributeModifier> getAttributeModifiers(EquipmentSlot p_41639_)
    // {
    //   Multimap<Attribute, AttributeModifier> multimap;
    //   if (this.hasTag() && this.Tag.contains("AttributeModifiers", 9))
    //   {
    //     multimap = (Multimap<Attribute, AttributeModifier>)HashMultimap.create();
    //     ListTag listtag = this.Tag.getList("AttributeModifiers", 10);
    //     for (int i = 0; i < listtag.size(); ++i)
    //     {
    //       CompoundTag compoundtag = listtag.getCompound(i);
    //       if (!compoundtag.contains("Slot", 8) || compoundtag.getString("Slot").equals(p_41639_.getName()))
    //       {
    //         Optional<Attribute> optional = BuiltInRegistries.ATTRIBUTE.getOptional(ResourceLocation.tryParse(compoundtag.getString("AttributeName")));
    //         if (optional.isPresent())
    //         {
    //           AttributeModifier attributemodifier = AttributeModifier.load(compoundtag);
    //           if (attributemodifier != null && attributemodifier.getId().getLeastSignificantBits() != 0L && attributemodifier.getId().getMostSignificantBits() != 0L)
    //           {
    //             multimap.put((Object)optional.get(), (Object)attributemodifier);
    //           }
    //         }
    //       }
    //     }
    //   }
    //   else
    //   {
    //     multimap = this.Item.getAttributeModifiers(p_41639_, this);
    //   }
    //   multimap = ForgeHooks.getAttributeModifiers(this, p_41639_, multimap);
    //   return multimap;
    // }


    // public void AddAttributeModifier(Attribute p_41644_, AttributeModifier p_41645_, EquipmentSlot p_41646_)
    // {
    //   this.GetOrCreateTag();
    //   if (!this.Tag.Contains("AttributeModifiers"))
    //   {
    //     this.Tag.Add(new ListTag("AttributeModifiers"));
    //   }
    //   ListTag listtag = this.Tag.Get<ListTag>("AttributeModifiers");
    //   CompoundTag compoundtag = p_41645_.save();
    //   compoundtag.Add(new StringTag("AttributeName", BuiltInRegistries.ATTRIBUTE.GetKey(p_41644_).ToString()));
    //   if (p_41646_ != null)
    //   {
    //     compoundtag.Add(new StringTag("Slot", p_41646_.GetName()));
    //   }
    //   listtag.Add(compoundtag);
    // }

    // TODO
    // public Component GetDisplayName()
    // {
    //    MutableComponent mutablecomponent = Component.empty().append(this.getHoverName());
    //   if (this.hasCustomHoverName())
    //   {
    //     mutablecomponent.withStyle(ChatFormatting.ITALIC);
    //   }
    //    MutableComponent mutablecomponent2 = ComponentUtils.wrapInSquareBrackets(mutablecomponent);
    //   if (!this.emptyCacheFlag)
    //   {
    //     mutablecomponent2.withStyle(this.getRarity().getStyleModifier()).withStyle(p_220170_=> {
    //       new HoverEvent(HoverEvent.Action.SHOW_ITEM, new HoverEvent.ItemStackInfo(this));
    //        HoverEvent p_131145_;
    //       return p_220170_.withHoverEvent(p_131145_);
    //     });
    //   }
    //   return mutablecomponent2;
    // }

    // public void OnUseTick(Level p_41732_, LivingEntity p_41733_, int p_41734_)
    // {
    //   this.Item.OnUseTick(p_41732_, p_41733_, this, p_41734_);
    // }

    // public void DeserializeNBT(CompoundTag nbt)
    // {
    //   ItemStack itemStack = Of(nbt);
    //   this.SetTag(itemStack.GetTag());
    //   if (itemStack.CapNBT != null)
    //   {
    //     this.DeserializeCaps(itemStack.CapNBT);
    //   }
    // }

    // public AudioSource GetDrinkingSound()
    // {
    //   return Item.GetDrinkingSound();
    // }

    // public AudioSource getEatingSound()
    // {
    //   return Item.GetEatingSound();
    // }

    // public AudioSource GetEquipSound()
    // {
    //   return Item.GetEquipSound();
    // }


    public bool IsConsumable()
    {
      return this.GetItem()!.IsConsumable();
    }

    public Item AsItem()
    {
      return this.GetItem()!;
    }
  }

}