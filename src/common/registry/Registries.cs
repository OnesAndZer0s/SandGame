using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sandbox.Common.Registry
{
  public static class Registries
  {
    public static readonly ResourceLocation ROOT_REGISTRY_NAME;
    // public static readonly DefaultedRegistry<GameEvent> GAME_EVENT;

    // public static readonly Registry<AudioSource> SOUND_EVENT;

    // public static readonly DefaultedRegistry<Fluid> FLUID;

    // public static readonly Registry<MobEffect> MOB_EFFECT;


    // public static readonly Registry<Enchantment> ENCHANTMENT;

    // public static readonly DefaultedRegistry<EntityType<?>> ENTITY_TYPE;


    // public static readonly DefaultedRegistry<Potion> POTION;

    // public static readonly Registry<ParticleType<?>> PARTICLE_TYPE;

    // public static readonly Registry<BlockEntityType<?>> BLOCK_ENTITY_TYPE;

    // public static readonly DefaultedRegistry<PaintingVariant> PAINTING_VARIANT;
    // public static readonly Registry<ResourceLocation> CUSTOM_STAT;

    // public static readonly DefaultedRegistry<ChunkStatus> CHUNK_STATUS;
    // public static readonly Registry<RuleTestType<?>> RULE_TEST;
    // public static readonly Registry<PosRuleTestType<?>> POS_RULE_TEST;

    // public static readonly Registry<MenuType<?>> MENU;

    // public static readonly Registry<RecipeType<?>> RECIPE_TYPE;

    // public static readonly Registry<RecipeSerializer<?>> RECIPE_SERIALIZER;

    // public static readonly Registry<Attribute> ATTRIBUTE;
    // public static readonly Registry<PositionSourceType<?>> POSITION_SOURCE_TYPE;

    // public static readonly Registry<ArgumentTypeInfo<?, ?>> COMMAND_ARGUMENT_TYPE;

    // public static readonly Registry<StatType<?>> STAT_TYPE;
    // public static readonly DefaultedRegistry<VillagerType> VILLAGER_TYPE;

    // public static readonly DefaultedRegistry<VillagerProfession> VILLAGER_PROFESSION;

    // public static readonly Registry<PoiType> POINT_OF_INTEREST_TYPE;

    // public static readonly DefaultedRegistry<MemoryModuleType<?>> MEMORY_MODULE_TYPE;

    // public static readonly DefaultedRegistry<SensorType<?>> SENSOR_TYPE;

    // public static readonly Registry<Schedule> SCHEDULE;

    // public static readonly Registry<Activity> ACTIVITY;
    // public static readonly Registry<LootPoolEntryType> LOOT_POOL_ENTRY_TYPE;
    // public static readonly Registry<LootItemFunctionType> LOOT_FUNCTION_TYPE;
    // public static readonly Registry<LootItemConditionType> LOOT_CONDITION_TYPE;
    // public static readonly Registry<LootNumberProviderType> LOOT_NUMBER_PROVIDER_TYPE;
    // public static readonly Registry<LootNbtProviderType> LOOT_NBT_PROVIDER_TYPE;
    // public static readonly Registry<LootScoreProviderType> LOOT_SCORE_PROVIDER_TYPE;
    // public static readonly Registry<FloatProviderType<?>> FLOAT_PROVIDER_TYPE;
    // public static readonly Registry<IntProviderType<?>> INT_PROVIDER_TYPE;
    // public static readonly Registry<HeightProviderType<?>> HEIGHT_PROVIDER_TYPE;
    // public static readonly Registry<BlockPredicateType<?>> BLOCK_PREDICATE_TYPE;

    // public static readonly Registry<WorldCarver<?>> CARVER;

    // public static readonly Registry<Feature<?>> FEATURE;
    // public static readonly Registry<StructurePlacementType<?>> STRUCTURE_PLACEMENT;
    // public static readonly Registry<StructurePieceType> STRUCTURE_PIECE;
    // public static readonly Registry<StructureType<?>> STRUCTURE_TYPE;
    // public static readonly Registry<PlacementModifierType<?>> PLACEMENT_MODIFIER_TYPE;

    // public static readonly Registry<BlockStateProviderType<?>> BLOCKSTATE_PROVIDER_TYPE;

    // public static readonly Registry<FoliagePlacerType<?>> FOLIAGE_PLACER_TYPE;
    // public static readonly Registry<TrunkPlacerType<?>> TRUNK_PLACER_TYPE;
    // public static readonly Registry<RootPlacerType<?>> ROOT_PLACER_TYPE;

    // public static readonly Registry<TreeDecoratorType<?>> TREE_DECORATOR_TYPE;
    // public static readonly Registry<FeatureSizeType<?>> FEATURE_SIZE_TYPE;
    // public static readonly Registry<Codec<BiomeSource>> BIOME_SOURCE;
    // public static readonly Registry<Codec<ChunkGenerator>> CHUNK_GENERATOR;
    // public static readonly Registry<Codec<SurfaceRules.ConditionSource>> MATERIAL_CONDITION;
    // public static readonly Registry<Codec<SurfaceRules.RuleSource>> MATERIAL_RULE;
    // public static readonly Registry<Codec<DensityFunction>> DENSITY_FUNCTION_TYPE;
    // public static readonly Registry<StructureProcessorType<?>> STRUCTURE_PROCESSOR;
    // public static readonly Registry<StructurePoolElementType<?>> STRUCTURE_POOL_ELEMENT;
    // public static readonly Registry<CatVariant> CAT_VARIANT;
    // public static readonly Registry<FrogVariant> FROG_VARIANT;
    // public static readonly Registry<BannerPattern> BANNER_PATTERN;
    // public static readonly Registry<Instrument> INSTRUMENT;
    public static Registry<IRegistry> REGISTRY /*{ get; private set; }*/ = new Registry<IRegistry>(ROOT_REGISTRY_NAME);
    // private static Registry<T> registerSimple<T>(ResourceKey<Registry<T>> p_260095_, RegistryBootstrap<T> p_259057_)
    // {
    //   return registerSimple(p_260095_, p_259057_);
    // }

    // private static Registry<T> forge<T>(ResourceKey<Registry<T>> key, RegistryBootstrap<T> def)
    // {
    //   return forge(key, def);
    // }

    // private static DefaultedRegistry<T> registerDefaulted<T>(ResourceKey<Registry<T>> p_259887_, string p_259325_, RegistryBootstrap<T> p_259759_)
    // {
    //   return registerDefaulted(p_259887_, p_259325_, p_259759_);
    // }

    // private static DefaultedRegistry<T> forge<T>(ResourceKey<Registry<T>> key, string defKey, RegistryBootstrap<T> def)
    // {
    //   return forge(key, defKey, def);
    // }


    private static Registry<T> AddRegistry<T>(ResourceLocation name)
    {
      Registry<T> registry = new Registry<T>(name);
      Debug.Print("Adding registry: " + name.ToString());
      Debug.Print("previous length: " + Registries.REGISTRY.Count);
      Registries.REGISTRY.Register(name, registry);
      Debug.Print("new length: " + Registries.REGISTRY.Count);
      return registry;
    }

    public static void BootStrap()
    {
      CreateContents();
      Freeze();
      Validate<IRegistry>(Registries.REGISTRY);
    }

    public static Registry<T> Get<T>(ResourceLocation location)
    {
      return (Registry<T>)Registries.REGISTRY[location];
    }
    public static void CreateContents()
    {
      // BuiltInRegistries.LOADERS.ForEach((p_259863_, p_259387_) =>
      // {
      //   if (p_259387_.get() == null)
      //   {
      //     BuiltInRegistries.LOGGER.error("Unable to bootstrap registry '{}'", (Object)p_259863_);
      //   }
      // });
    }

    private static void Freeze()
    {
      Registries.REGISTRY.Freeze();
      foreach (KeyValuePair<ResourceLocation, IRegistry> p_259863_ in Registries.REGISTRY)
      {
        p_259863_.Value.Freeze();
      }
    }

    private static void Validate<T>(Registry<T> p_260209_) where T : IRegistry
    {
      // check if registry is empty
      if (p_260209_.Count == 0)
      {
        // TODO
        Debug.Fail("Registry '" + p_260209_.GetType().ToString() + "' was empty after loading");
        // Util.logAndPauseIfInIde("Registry '" + p_260209_.getKey(p_259410_) + "' was empty after loading");
      }
    }

    static Registries()
    {
      AddRegistry<Item>(BuiltInRegistries.ITEM);
      AddRegistry<Tile>(BuiltInRegistries.TILE);

      //         LOGGER = LogUtils.getLogger();
      //         LOADERS = Maps.newLinkedHashMap();
      //         ROOT_REGISTRY_NAME = new ResourceLocation("root");
      //     WRITABLE_REGISTRY = new MappedRegistry<WritableRegistry<?>>((ResourceKey<Registry<WritableRegistry<?>>>) ResourceKey.createRegistryKey(BuiltInRegistries.ROOT_REGISTRY_NAME), Lifecycle.stable());
      //         GAME_EVENT = registerDefaultedWithIntrusiveHolders(Registries.GAME_EVENT, "step", p_260052_ -> GameEvent.STEP);
      //     SOUND_EVENT = forge(Registries.SOUND_EVENT, p_260167_ -> SoundEvents.ITEM_PICKUP);
      //     FLUID = forge(Registries.FLUID, "empty", p_259453_ -> Fluids.EMPTY);
      //     MOB_EFFECT = forge(Registries.MOB_EFFECT, p_259689_ -> MobEffects.LUCK);
      //     ENCHANTMENT = forge(Registries.ENCHANTMENT, p_259104_ -> Enchantments.BLOCK_FORTUNE);
      //     ENTITY_TYPE = forge((ResourceKey<Registry<EntityType<Pig>>>) Registries.ENTITY_TYPE, "pig", p_259175_ -> EntityType.PIG);
      //     POTION = forge(Registries.POTION, "empty", p_259869_ -> Potions.EMPTY);
      //     PARTICLE_TYPE = forge((ResourceKey<Registry<ParticleType<BlockParticleOption>>>) Registries.PARTICLE_TYPE, p_260266_ -> ParticleTypes.BLOCK);
      //     BLOCK_ENTITY_TYPE = forge((ResourceKey<Registry<BlockEntityType<FurnaceBlockEntity>>>) Registries.BLOCK_ENTITY_TYPE, p_259434_ -> BlockEntityType.FURNACE);
      //     PAINTING_VARIANT = forge(Registries.PAINTING_VARIANT, "kebab", PaintingVariants::bootstrap);
      //     CUSTOM_STAT = registerSimple(Registries.CUSTOM_STAT, p_259833_ -> Stats.JUMP);
      //     CHUNK_STATUS = forge(Registries.CHUNK_STATUS, "empty", p_259971_ -> ChunkStatus.EMPTY);
      //     RULE_TEST = registerSimple((ResourceKey<Registry<RuleTestType<AlwaysTrueTest>>>) Registries.RULE_TEST, p_259641_ -> RuleTestType.ALWAYS_TRUE_TEST);
      //     POS_RULE_TEST = registerSimple((ResourceKey<Registry<PosRuleTestType<PosAlwaysTrueTest>>>) Registries.POS_RULE_TEST, p_259262_ -> PosRuleTestType.ALWAYS_TRUE_TEST);
      //     MENU = forge((ResourceKey<Registry<MenuType<AnvilMenu>>>) Registries.MENU, p_259341_ -> MenuType.ANVIL);
      //     RECIPE_TYPE = forge((ResourceKey<Registry<RecipeType<CraftingRecipe>>>) Registries.RECIPE_TYPE, p_259086_ -> RecipeType.CRAFTING);
      //     RECIPE_SERIALIZER = forge((ResourceKey<Registry<RecipeSerializer<ShapelessRecipe>>>) Registries.RECIPE_SERIALIZER, p_260230_ -> RecipeSerializer.SHAPELESS_RECIPE);
      //     ATTRIBUTE = forge(Registries.ATTRIBUTE, p_260300_ -> Attributes.LUCK);
      //     POSITION_SOURCE_TYPE = registerSimple((ResourceKey<Registry<PositionSourceType<BlockPositionSource>>>) Registries.POSITION_SOURCE_TYPE, p_259113_ -> PositionSourceType.BLOCK);
      //     COMMAND_ARGUMENT_TYPE = forge((ResourceKey<Registry<ArgumentTypeInfo>>) Registries.COMMAND_ARGUMENT_TYPE, ArgumentTypeInfos::bootstrap);
      //     STAT_TYPE = forge((ResourceKey<Registry<StatType<Item>>>) Registries.STAT_TYPE, p_259967_ -> Stats.ITEM_USED);
      //     VILLAGER_TYPE = registerDefaulted(Registries.VILLAGER_TYPE, "plains", p_259473_ -> VillagerType.PLAINS);
      //     VILLAGER_PROFESSION = forge(Registries.VILLAGER_PROFESSION, "none", p_259037_ -> VillagerProfession.NONE);
      //     POINT_OF_INTEREST_TYPE = forge(Registries.POINT_OF_INTEREST_TYPE, PoiTypes::bootstrap);
      //     MEMORY_MODULE_TYPE = forge((ResourceKey<Registry<MemoryModuleType<Void>>>) Registries.MEMORY_MODULE_TYPE, "dummy", p_259248_ -> MemoryModuleType.DUMMY);
      //     SENSOR_TYPE = forge((ResourceKey<Registry<SensorType<DummySensor>>>) Registries.SENSOR_TYPE, "dummy", p_259757_ -> SensorType.DUMMY);
      //     SCHEDULE = forge(Registries.SCHEDULE, p_259540_ -> Schedule.EMPTY);
      //     ACTIVITY = forge(Registries.ACTIVITY, p_260197_ -> Activity.IDLE);
      //     LOOT_POOL_ENTRY_TYPE = registerSimple(Registries.LOOT_POOL_ENTRY_TYPE, p_260042_ -> LootPoolEntries.EMPTY);
      //     LOOT_FUNCTION_TYPE = registerSimple(Registries.LOOT_FUNCTION_TYPE, p_259836_ -> LootItemFunctions.SET_COUNT);
      //     LOOT_CONDITION_TYPE = registerSimple(Registries.LOOT_CONDITION_TYPE, p_259742_ -> LootItemConditions.INVERTED);
      //     LOOT_NUMBER_PROVIDER_TYPE = registerSimple(Registries.LOOT_NUMBER_PROVIDER_TYPE, p_259329_ -> NumberProviders.CONSTANT);
      //     LOOT_NBT_PROVIDER_TYPE = registerSimple(Registries.LOOT_NBT_PROVIDER_TYPE, p_259862_ -> NbtProviders.CONTEXT);
      //     LOOT_SCORE_PROVIDER_TYPE = registerSimple(Registries.LOOT_SCORE_PROVIDER_TYPE, p_259313_ -> ScoreboardNameProviders.CONTEXT);
      //     FLOAT_PROVIDER_TYPE = registerSimple((ResourceKey<Registry<FloatProviderType<ConstantFloat>>>) Registries.FLOAT_PROVIDER_TYPE, p_260093_ -> FloatProviderType.CONSTANT);
      //     INT_PROVIDER_TYPE = registerSimple((ResourceKey<Registry<IntProviderType<ConstantInt>>>) Registries.INT_PROVIDER_TYPE, p_259607_ -> IntProviderType.CONSTANT);
      //     HEIGHT_PROVIDER_TYPE = registerSimple((ResourceKey<Registry<HeightProviderType<ConstantHeight>>>) Registries.HEIGHT_PROVIDER_TYPE, p_259663_ -> HeightProviderType.CONSTANT);
      //     BLOCK_PREDICATE_TYPE = registerSimple((ResourceKey<Registry<BlockPredicateType<NotPredicate>>>) Registries.BLOCK_PREDICATE_TYPE, p_260006_ -> BlockPredicateType.NOT);
      //     CARVER = forge((ResourceKey<Registry<WorldCarver<CaveCarverConfiguration>>>) Registries.CARVER, p_260200_ -> WorldCarver.CAVE);
      //     FEATURE = forge((ResourceKey<Registry<Feature<OreConfiguration>>>) Registries.FEATURE, p_259143_ -> Feature.ORE);
      //     STRUCTURE_PLACEMENT = registerSimple((ResourceKey<Registry<StructurePlacementType<RandomSpreadStructurePlacement>>>) Registries.STRUCTURE_PLACEMENT, p_259179_ -> StructurePlacementType.RANDOM_SPREAD);
      //     STRUCTURE_PIECE = registerSimple(Registries.STRUCTURE_PIECE, p_259722_ -> StructurePieceType.MINE_SHAFT_ROOM);
      //     STRUCTURE_TYPE = registerSimple((ResourceKey<Registry<StructureType<JigsawStructure>>>) Registries.STRUCTURE_TYPE, p_259466_ -> StructureType.JIGSAW);
      //     PLACEMENT_MODIFIER_TYPE = registerSimple((ResourceKey<Registry<PlacementModifierType<CountPlacement>>>) Registries.PLACEMENT_MODIFIER_TYPE, p_260335_ -> PlacementModifierType.COUNT);
      //     BLOCKSTATE_PROVIDER_TYPE = forge((ResourceKey<Registry<BlockStateProviderType<SimpleStateProvider>>>) Registries.BLOCK_STATE_PROVIDER_TYPE, p_259345_ -> BlockStateProviderType.SIMPLE_STATE_PROVIDER);
      //     FOLIAGE_PLACER_TYPE = forge((ResourceKey<Registry<FoliagePlacerType<BlobFoliagePlacer>>>) Registries.FOLIAGE_PLACER_TYPE, p_260329_ -> FoliagePlacerType.BLOB_FOLIAGE_PLACER);
      //     TRUNK_PLACER_TYPE = registerSimple((ResourceKey<Registry<TrunkPlacerType<StraightTrunkPlacer>>>) Registries.TRUNK_PLACER_TYPE, p_259690_ -> TrunkPlacerType.STRAIGHT_TRUNK_PLACER);
      //     ROOT_PLACER_TYPE = registerSimple((ResourceKey<Registry<RootPlacerType<MangroveRootPlacer>>>) Registries.ROOT_PLACER_TYPE, p_259493_ -> RootPlacerType.MANGROVE_ROOT_PLACER);
      //     TREE_DECORATOR_TYPE = forge((ResourceKey<Registry<TreeDecoratorType<LeaveVineDecorator>>>) Registries.TREE_DECORATOR_TYPE, p_259122_ -> TreeDecoratorType.LEAVE_VINE);
      //     FEATURE_SIZE_TYPE = registerSimple((ResourceKey<Registry<FeatureSizeType<TwoLayersFeatureSize>>>) Registries.FEATURE_SIZE_TYPE, p_259370_ -> FeatureSizeType.TWO_LAYERS_FEATURE_SIZE);
      //     BIOME_SOURCE = registerSimple(Registries.BIOME_SOURCE, Lifecycle.stable(), BiomeSources::bootstrap);
      //     CHUNK_GENERATOR = registerSimple(Registries.CHUNK_GENERATOR, Lifecycle.stable(), ChunkGenerators::bootstrap);
      //     MATERIAL_CONDITION = registerSimple(Registries.MATERIAL_CONDITION, SurfaceRules.ConditionSource::bootstrap);
      //     MATERIAL_RULE = registerSimple(Registries.MATERIAL_RULE, SurfaceRules.RuleSource::bootstrap);
      //     DENSITY_FUNCTION_TYPE = registerSimple(Registries.DENSITY_FUNCTION_TYPE, DensityFunctions::bootstrap);
      //     STRUCTURE_PROCESSOR = registerSimple((ResourceKey<Registry<StructureProcessorType<BlockIgnoreProcessor>>>) Registries.STRUCTURE_PROCESSOR, p_259305_ -> StructureProcessorType.BLOCK_IGNORE);
      //     STRUCTURE_POOL_ELEMENT = registerSimple((ResourceKey<Registry<StructurePoolElementType<EmptyPoolElement>>>) Registries.STRUCTURE_POOL_ELEMENT, p_259361_ -> StructurePoolElementType.EMPTY);
      //     CAT_VARIANT = registerSimple(Registries.CAT_VARIANT, CatVariant::bootstrap);
      //     FROG_VARIANT = registerSimple(Registries.FROG_VARIANT, p_259261_ -> FrogVariant.TEMPERATE);
      //     BANNER_PATTERN = registerSimple(Registries.BANNER_PATTERN, BannerPatterns::bootstrap);
      //     INSTRUMENT = registerSimple(Registries.INSTRUMENT, Instruments::bootstrap);
      //     REGISTRY = BuiltInRegistries.WRITABLE_REGISTRY;
    }

    //   @FunctionalInterface
    //     interface RegistryBootstrap<T>
    //   {
    //     T run(readonly Registry<T> p0);
    //   }
  }
}