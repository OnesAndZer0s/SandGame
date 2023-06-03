using Sandbox.Core;
using Sandbox.Core.Registries;
using Sandbox.Worlds;
using Sandbox.Worlds.Items;
using UnityEngine;

public static class DyeHardItems
{
  public static Registry<Item> ITEMS = Registries.Get<Item>(BuiltInRegistries.ITEM);



  public static Item ALICE_BLUE_DYE = ITEMS.Register("dyehard:alice_blue_dye", new DyeItem(new Color(0xF0, 0xF8, 0xFF)));
  public static Item ANTIQUE_WHITE_DYE = ITEMS.Register("dyehard:antique_white_dye", new DyeItem(new Color(0xFA, 0xEB, 0xD7)));
  public static Item AQUAMARINE_DYE = ITEMS.Register("dyehard:aquamarine_dye", new DyeItem(new Color(0x7F, 0xFF, 0xD4)));
  public static Item AZURE_DYE = ITEMS.Register("dyehard:azure_dye", new DyeItem(new Color(0xF0, 0xFF, 0xFF)));
  public static Item BEIGE_DYE = ITEMS.Register("dyehard:beige_dye", new DyeItem(new Color(0xF5, 0xF5, 0xDC)));
  public static Item BISQUE_DYE = ITEMS.Register("dyehard:bisque_dye", new DyeItem(new Color(0xFF, 0xE4, 0xC4)));
  public static Item BLACK_DYE = ITEMS.Register("dyehard:black_dye", new DyeItem(new Color(0x00, 0x00, 0x00)));
  public static Item BLANCHED_ALMOND_DYE = ITEMS.Register("dyehard:blanched_almond_dye", new DyeItem(new Color(0xFF, 0xEB, 0xCD)));
  public static Item BLUE_DYE = ITEMS.Register("dyehard:blue_dye", new DyeItem(new Color(0x00, 0x00, 0xFF)));
  public static Item BLUE_VIOLET_DYE = ITEMS.Register("dyehard:blue_violet_dye", new DyeItem(new Color(0x8A, 0x2B, 0xE2)));
  public static Item BROWN_DYE = ITEMS.Register("dyehard:brown_dye", new DyeItem(new Color(0xA5, 0x2A, 0x2A)));
  public static Item BURLY_WOOD_DYE = ITEMS.Register("dyehard:burly_wood_dye", new DyeItem(new Color(0xDE, 0xB8, 0x87)));
  public static Item CADET_BLUE_DYE = ITEMS.Register("dyehard:cadet_blue_dye", new DyeItem(new Color(0x5F, 0x9E, 0xA0)));
  public static Item CHARTREUSE_DYE = ITEMS.Register("dyehard:chartreuse_dye", new DyeItem(new Color(0x7F, 0xFF, 0x00)));
  public static Item CHOCOLATE_DYE = ITEMS.Register("dyehard:chocolate_dye", new DyeItem(new Color(0xD2, 0x69, 0x1E)));
  public static Item CORAL_DYE = ITEMS.Register("dyehard:coral_dye", new DyeItem(new Color(0xFF, 0x7F, 0x50)));
  public static Item CORNFLOWER_BLUE_DYE = ITEMS.Register("dyehard:cornflower_blue_dye", new DyeItem(new Color(0x64, 0x95, 0xED)));
  public static Item CORNSILK_DYE = ITEMS.Register("dyehard:cornsilk_dye", new DyeItem(new Color(0xFF, 0xF8, 0xDC)));
  public static Item CRIMSON_DYE = ITEMS.Register("dyehard:crimson_dye", new DyeItem(new Color(0xDC, 0x14, 0x3C)));
  public static Item CYAN_DYE = ITEMS.Register("dyehard:cyan_dye", new DyeItem(new Color(0x00, 0xFF, 0xFF)));
  public static Item DARK_BLUE_DYE = ITEMS.Register("dyehard:dark_blue_dye", new DyeItem(new Color(0x00, 0x00, 0x8B)));
  public static Item DARK_CYAN_DYE = ITEMS.Register("dyehard:dark_cyan_dye", new DyeItem(new Color(0x00, 0x8B, 0x8B)));
  public static Item DARK_GOLDEN_ROD_DYE = ITEMS.Register("dyehard:dark_golden_rod_dye", new DyeItem(new Color(0xB8, 0x86, 0x0B)));
  public static Item DARK_GRAY_DYE = ITEMS.Register("dyehard:dark_gray_dye", new DyeItem(new Color(0xA9, 0xA9, 0xA9)));
  public static Item DARK_GREEN_DYE = ITEMS.Register("dyehard:dark_green_dye", new DyeItem(new Color(0x00, 0x64, 0x00)));
  public static Item DARK_KHAKI_DYE = ITEMS.Register("dyehard:dark_khaki_dye", new DyeItem(new Color(0xBD, 0xB7, 0x6B)));
  public static Item DARK_MAGENTA_DYE = ITEMS.Register("dyehard:dark_magenta_dye", new DyeItem(new Color(0x8B, 0x00, 0x8B)));
  public static Item DARK_OLIVE_GREEN_DYE = ITEMS.Register("dyehard:dark_olive_green_dye", new DyeItem(new Color(0x55, 0x6B, 0x2F)));
  public static Item DARK_oRANGE_DYE = ITEMS.Register("dyehard:dark_orange_dye", new DyeItem(new Color(0xFF, 0x8C, 0x00)));
  public static Item DARK_ORCHID_DYE = ITEMS.Register("dyehard:dark_orchid_dye", new DyeItem(new Color(0x99, 0x32, 0xCC)));
  public static Item DARK_RED_DYE = ITEMS.Register("dyehard:dark_red_dye", new DyeItem(new Color(0x8B, 0x00, 0x00)));
  public static Item DARK_SALMON_DYE = ITEMS.Register("dyehard:dark_salmon_dye", new DyeItem(new Color(0xE9, 0x96, 0x7A)));
  public static Item DARK_SEA_GREEN_DYE = ITEMS.Register("dyehard:dark_sea_green_dye", new DyeItem(new Color(0x8F, 0xBC, 0x8F)));
  public static Item DARK_SLATE_BLUE_DYE = ITEMS.Register("dyehard:dark_slate_blue_dye", new DyeItem(new Color(0x48, 0x3D, 0x8B)));
  public static Item DARK_SLATE_GRAY_DYE = ITEMS.Register("dyehard:dark_slate_gray_dye", new DyeItem(new Color(0x2F, 0x4F, 0x4F)));
  public static Item DARK_TURQUOISE_DYE = ITEMS.Register("dyehard:dark_turquoise_dye", new DyeItem(new Color(0x00, 0xCE, 0xD1)));
  public static Item DARK_VIOLET_DYE = ITEMS.Register("dyehard:dark_violet_dye", new DyeItem(new Color(0x94, 0x00, 0xD3)));
  public static Item DEEP_PINK_DYE = ITEMS.Register("dyehard:deep_pink_dye", new DyeItem(new Color(0xFF, 0x14, 0x93)));
  public static Item DEEP_SKY_BLUE_DYE = ITEMS.Register("dyehard:deep_sky_blue_dye", new DyeItem(new Color(0x00, 0xBF, 0xFF)));
  public static Item DIM_GRAY_DYE = ITEMS.Register("dyehard:dim_gray_dye", new DyeItem(new Color(0x69, 0x69, 0x69)));
  public static Item DODGER_BLUE_DYE = ITEMS.Register("dyehard:dodger_blue_dye", new DyeItem(new Color(0x1E, 0x90, 0xFF)));
  public static Item FIRE_BRICK_DYE = ITEMS.Register("dyehard:fire_brick_dye", new DyeItem(new Color(0xB2, 0x22, 0x22)));
  public static Item FLORAL_WHITE_DYE = ITEMS.Register("dyehard:floral_white_dye", new DyeItem(new Color(0xFF, 0xFA, 0xF0)));
  public static Item FOREST_GREEN_DYE = ITEMS.Register("dyehard:forest_green_dye", new DyeItem(new Color(0x22, 0x8B, 0x22)));
  public static Item GAINSBORO_DYE = ITEMS.Register("dyehard:gainsboro_dye", new DyeItem(new Color(0xDC, 0xDC, 0xDC)));
  public static Item GHOST_WHOTE_DYE = ITEMS.Register("dyehard:ghost_white_dye", new DyeItem(new Color(0xF8, 0xF8, 0xFF)));
  public static Item GOLD_DYE = ITEMS.Register("dyehard:gold_dye", new DyeItem(new Color(0xFF, 0xD7, 0x00)));
  public static Item GOLDEN_ROD_DYE = ITEMS.Register("dyehard:golden_rod_dye", new DyeItem(new Color(0xDA, 0xA5, 0x20)));
  public static Item GRAY_DYE = ITEMS.Register("dyehard:gray_dye", new DyeItem(new Color(0x80, 0x80, 0x80)));
  public static Item GREEN_DYE = ITEMS.Register("dyehard:green_dye", new DyeItem(new Color(0x00, 0x80, 0x00)));
  public static Item GREEN_YELLOW_DYE = ITEMS.Register("dyehard:green_yellow_dye", new DyeItem(new Color(0xAD, 0xFF, 0x2F)));
  public static Item HONEY_DEW_DYE = ITEMS.Register("dyehard:honey_dew_dye", new DyeItem(new Color(0xF0, 0xFF, 0xF0)));
  public static Item HOT_PINK_DYE = ITEMS.Register("dyehard:hot_pink_dye", new DyeItem(new Color(0xFF, 0x69, 0xB4)));
  public static Item INDIAN_RED_DYE = ITEMS.Register("dyehard:indian_red_dye", new DyeItem(new Color(0xCD, 0x5C, 0x5C)));
  public static Item INDIGO_DYE = ITEMS.Register("dyehard:indigo_dye", new DyeItem(new Color(0x4B, 0x00, 0x82)));
  public static Item IVORY_DYE = ITEMS.Register("dyehard:ivory_dye", new DyeItem(new Color(0xFF, 0xFF, 0xF0)));
  public static Item KHAKI_DYE = ITEMS.Register("dyehard:khaki_dye", new DyeItem(new Color(0xF0, 0xE6, 0x8C)));
  public static Item LAVENDER_DYE = ITEMS.Register("dyehard:lavender_dye", new DyeItem(new Color(0xE6, 0xE6, 0xFA)));
  public static Item LAVENDER_BLUSH_DYE = ITEMS.Register("dyehard:lavender_blush_dye", new DyeItem(new Color(0xFF, 0xF0, 0xF5)));
  public static Item LAWN_GREEN_DYE = ITEMS.Register("dyehard:lawn_green_dye", new DyeItem(new Color(0x7C, 0xFC, 0x00)));
  public static Item LEMON_CHIFFRON_DYE = ITEMS.Register("dyehard:lemon_chiffon_dye", new DyeItem(new Color(0xFF, 0xFA, 0xCD)));
  public static Item LIGHT_BLUE_DYE = ITEMS.Register("dyehard:light_blue_dye", new DyeItem(new Color(0xAD, 0xD8, 0xE6)));
  public static Item LIGHT_CORAL_DYE = ITEMS.Register("dyehard:light_coral_dye", new DyeItem(new Color(0xF0, 0x80, 0x80)));
  public static Item LIGHT_CYAN_DYE = ITEMS.Register("dyehard:light_cyan_dye", new DyeItem(new Color(0xE0, 0xFF, 0xFF)));
  public static Item LIGHT_GOLDEN_ROD_YELLOW_DYE = ITEMS.Register("dyehard:light_golden_rod_yellow_dye", new DyeItem(new Color(0xFA, 0xFA, 0xD2)));
  public static Item LIGHT_GRAY_DYE = ITEMS.Register("dyehard:light_gray_dye", new DyeItem(new Color(0xD3, 0xD3, 0xD3)));
  public static Item LIGHT_GREEN_DYE = ITEMS.Register("dyehard:light_green_dye", new DyeItem(new Color(0x90, 0xEE, 0x90)));
  public static Item LIGHT_PINK_DYE = ITEMS.Register("dyehard:light_pink_dye", new DyeItem(new Color(0xFF, 0xB6, 0xC1)));
  public static Item LIGHT_SALMON_DYE = ITEMS.Register("dyehard:light_salmon_dye", new DyeItem(new Color(0xFF, 0xA0, 0x7A)));
  public static Item LIGHT_SEA_GREEN_DYE = ITEMS.Register("dyehard:light_sea_green_dye", new DyeItem(new Color(0x20, 0xB2, 0xAA)));
  public static Item LIGHT_SKY_BLUE_DYE = ITEMS.Register("dyehard:light_sky_blue_dye", new DyeItem(new Color(0x87, 0xCE, 0xFA)));
  public static Item LIGHT_SLATE_GRAY_DYE = ITEMS.Register("dyehard:light_slate_gray_dye", new DyeItem(new Color(0x77, 0x88, 0x99)));
  public static Item LIGHT_STEEL_BLUE_DYE = ITEMS.Register("dyehard:light_steel_blue_dye", new DyeItem(new Color(0xB0, 0xC4, 0xDE)));
  public static Item LIGHT_YELLOW_DYE = ITEMS.Register("dyehard:light_yellow_dye", new DyeItem(new Color(0xFF, 0xFF, 0xE0)));
  public static Item LIME_DYE = ITEMS.Register("dyehard:lime_dye", new DyeItem(new Color(0x00, 0xFF, 0x00)));
  public static Item LIME_GREEN_DYE = ITEMS.Register("dyehard:lime_green_dye", new DyeItem(new Color(0x32, 0xCD, 0x32)));
  public static Item LINEN_DYE = ITEMS.Register("dyehard:linen_dye", new DyeItem(new Color(0xFA, 0xF0, 0xE6)));
  public static Item MAGENTA_DYE = ITEMS.Register("dyehard:magenta_dye", new DyeItem(new Color(0xFF, 0x00, 0xFF)));
  public static Item MAROON_DYE = ITEMS.Register("dyehard:maroon_dye", new DyeItem(new Color(0x80, 0x00, 0x00)));
  public static Item MEDIUM_AQUAMARINE_DYE = ITEMS.Register("dyehard:medium_aquamarine_dye", new DyeItem(new Color(0x66, 0xCD, 0xAA)));
  public static Item MEDIUM_BLUE_DYE = ITEMS.Register("dyehard:medium_blue_dye", new DyeItem(new Color(0x00, 0x00, 0xCD)));
  public static Item MEDIUM_ORCHID_DYE = ITEMS.Register("dyehard:medium_orchid_dye", new DyeItem(new Color(0xBA, 0x55, 0xD3)));
  public static Item MEDIUM_PURPLE_DYE = ITEMS.Register("dyehard:medium_purple_dye", new DyeItem(new Color(0x93, 0x70, 0xD8)));
  public static Item MEDIUM_SEA_GREEN_DYE = ITEMS.Register("dyehard:medium_sea_green_dye", new DyeItem(new Color(0x3C, 0xB3, 0x71)));
  public static Item MEDIUM_SLATE_BLUE_DYE = ITEMS.Register("dyehard:medium_slate_blue_dye", new DyeItem(new Color(0x7B, 0x68, 0xEE)));
  public static Item MEDIUM_SPRING_GREEN_DYE = ITEMS.Register("dyehard:medium_spring_green_dye", new DyeItem(new Color(0x00, 0xFA, 0x9A)));
  public static Item MEDIUM_TURQUOISE_DYE = ITEMS.Register("dyehard:medium_turqoise_dye", new DyeItem(new Color(0x48, 0xD1, 0xCC)));
  public static Item MEDIUM_VIOLET_RED_DYE = ITEMS.Register("dyehard:medium_violet_red_dye", new DyeItem(new Color(0xC7, 0x15, 0x85)));
  public static Item MIDNIGHT_BLUE_DYE = ITEMS.Register("dyehard:midnight_blue_dye", new DyeItem(new Color(0x19, 0x19, 0x70)));
  public static Item Mint_CREAM_DYE = ITEMS.Register("dyehard:mint_cream_dye", new DyeItem(new Color(0xF5, 0xFF, 0xFA)));
  public static Item MISTY_ROSE_DYE = ITEMS.Register("dyehard:misty_rose_dye", new DyeItem(new Color(0xFF, 0xE4, 0xE1)));
  public static Item MOCCASIN_DYE = ITEMS.Register("dyehard:moccasin_dye", new DyeItem(new Color(0xFF, 0xE4, 0xB5)));
  public static Item NAVAJO_WHITE_DYE = ITEMS.Register("dyehard:navajo_white_dye", new DyeItem(new Color(0xFF, 0xDE, 0xAD)));
  public static Item NAVY_DYE = ITEMS.Register("dyehard:navy_dye", new DyeItem(new Color(0x00, 0x00, 0x80)));
  public static Item OLD_LACE_DYE = ITEMS.Register("dyehard:old_lace_dye", new DyeItem(new Color(0xFD, 0xF5, 0xE6)));
  public static Item OLIVE_DYE = ITEMS.Register("dyehard:olive_dye", new DyeItem(new Color(0x80, 0x80, 0x00)));
  public static Item OLIVE_DRAB_DYE = ITEMS.Register("dyehard:olive_drab_dye", new DyeItem(new Color(0x6B, 0x8E, 0x23)));
  public static Item ORANGE_DYE = ITEMS.Register("dyehard:orange_dye", new DyeItem(new Color(0xFF, 0xA5, 0x00)));
  public static Item ORANGE_RED_DYE = ITEMS.Register("dyehard:orange_red_dye", new DyeItem(new Color(0xFF, 0x45, 0x00)));
  public static Item ORCHID_DYE = ITEMS.Register("dyehard:orchid_dye", new DyeItem(new Color(0xDA, 0x70, 0xD6)));
  public static Item PALE_GOLDEN_ROD_DYE = ITEMS.Register("dyehard:pale_golden_rod_dye", new DyeItem(new Color(0xEE, 0xE8, 0xAA)));
  public static Item PALE_GREEN_DYE = ITEMS.Register("dyehard:pale_green_dye", new DyeItem(new Color(0x98, 0xFB, 0x98)));
  public static Item PALE_TURQUOISE_DYE = ITEMS.Register("dyehard:pale_turquoise_dye", new DyeItem(new Color(0xAF, 0xEE, 0xEE)));
  public static Item PALE_VIOLET_RED_DYE = ITEMS.Register("dyehard:pale_violet_red_dye", new DyeItem(new Color(0xD8, 0x70, 0x93)));
  public static Item PAPAYA_WHIP_DYE = ITEMS.Register("dyehard:papaya_whip_dye", new DyeItem(new Color(0xFF, 0xEF, 0xD5)));
  public static Item PEACH_PUFF_DYE = ITEMS.Register("dyehard:peach_puff_dye", new DyeItem(new Color(0xFF, 0xDA, 0xB9)));
  public static Item PERU_DYE = ITEMS.Register("dyehard:peru_dye", new DyeItem(new Color(0xCD, 0x85, 0x3F)));
  public static Item PINK_DYE = ITEMS.Register("dyehard:pink_dye", new DyeItem(new Color(0xFF, 0xC0, 0xCB)));
  public static Item PLUM_DYE = ITEMS.Register("dyehard:plum_dye", new DyeItem(new Color(0xDD, 0xA0, 0xDD)));
  public static Item POWDER_BLUE_DYE = ITEMS.Register("dyehard:powder_blue_dye", new DyeItem(new Color(0xB0, 0xE0, 0xE6)));
  public static Item PURPLE_DYE = ITEMS.Register("dyehard:purple_dye", new DyeItem(new Color(0x80, 0x00, 0x80)));
  public static Item RED_DYE = ITEMS.Register("dyehard:red_dye", new DyeItem(new Color(0xFF, 0x00, 0x00)));
  public static Item ROSY_BROWN_DYE = ITEMS.Register("dyehard:rosy_brown_dye", new DyeItem(new Color(0xBC, 0x8F, 0x8F)));
  public static Item ROYAL_BLUE_DYE = ITEMS.Register("dyehard:royal_blue_dye", new DyeItem(new Color(0x41, 0x69, 0xE1)));
  public static Item SADDLE_BROWN_DYE = ITEMS.Register("dyehard:saddle_brown_dye", new DyeItem(new Color(0x8B, 0x45, 0x13)));
  public static Item SALMON_DYE = ITEMS.Register("dyehard:salmon_dye", new DyeItem(new Color(0xFA, 0x80, 0x72)));
  public static Item SANDY_BROWN_DYE = ITEMS.Register("dyehard:sandy_brown_dye", new DyeItem(new Color(0xF4, 0xA4, 0x60)));
  public static Item SEA_GREEN_DYE = ITEMS.Register("dyehard:sea_green_dye", new DyeItem(new Color(0x2E, 0x8B, 0x57)));
  public static Item SEA_SHELL_DYE = ITEMS.Register("dyehard:sea_shell_dye", new DyeItem(new Color(0xFF, 0xF5, 0xEE)));
  public static Item SIENNA_DYE = ITEMS.Register("dyehard:sienna_dye", new DyeItem(new Color(0xA0, 0x52, 0x2D)));
  public static Item SILVER_DYE = ITEMS.Register("dyehard:silver_dye", new DyeItem(new Color(0xC0, 0xC0, 0xC0)));
  public static Item SKY_BLUE_DYE = ITEMS.Register("dyehard:sky_blue_dye", new DyeItem(new Color(0x87, 0xCE, 0xEB)));
  public static Item SlateBlue_DYE = ITEMS.Register("dyehard:slate_blue_dye", new DyeItem(new Color(0x6A, 0x5A, 0xCD)));
  public static Item SLATE_GRAY_DYE = ITEMS.Register("dyehard:slate_gray_dye", new DyeItem(new Color(0x70, 0x80, 0x90)));
  public static Item SNOW_DYE = ITEMS.Register("dyehard:snow_dye", new DyeItem(new Color(0xFF, 0xFA, 0xFA)));
  public static Item SPRING_GREEN_DYE = ITEMS.Register("dyehard:spring_green_dye", new DyeItem(new Color(0x00, 0xFF, 0x7F)));
  public static Item STEEL_BLUE_DYE = ITEMS.Register("dyehard:steel_blue_dye", new DyeItem(new Color(0x46, 0x82, 0xB4)));
  public static Item TAN_DYE = ITEMS.Register("dyehard:tan_dye", new DyeItem(new Color(0xD2, 0xB4, 0x8C)));
  public static Item TEAL_DYE = ITEMS.Register("dyehard:teal_dye", new DyeItem(new Color(0x00, 0x80, 0x80)));
  public static Item THISTLE_DYE = ITEMS.Register("dyehard:thistle_dye", new DyeItem(new Color(0xD8, 0xBF, 0xD8)));
  public static Item TOMATO_DYE = ITEMS.Register("dyehard:tomato_dye", new DyeItem(new Color(0xFF, 0x63, 0x47)));
  public static Item TURQUOISE_DYE = ITEMS.Register("dyehard:turquoise_dye", new DyeItem(new Color(0x40, 0xE0, 0xD0)));
  public static Item VIOLET_DYE = ITEMS.Register("dyehard:violet_dye", new DyeItem(new Color(0xEE, 0x82, 0xEE)));
  public static Item WHEAT_DYE = ITEMS.Register("dyehard:wheat_dye", new DyeItem(new Color(0xF5, 0xDE, 0xB3)));
  public static Item WHITE_DYE = ITEMS.Register("dyehard:white_dye", new DyeItem(new Color(0xFF, 0xFF, 0xFF)));
  public static Item WHITE_SMOKE_DYE = ITEMS.Register("dyehard:white_smoke_dye", new DyeItem(new Color(0xF5, 0xF5, 0xF5)));
  public static Item YELLOW_DYE = ITEMS.Register("dyehard:yellow_dye", new DyeItem(new Color(0xFF, 0xFF, 0x00)));
  public static Item YELLOW_GREEN_DYE = ITEMS.Register("dyehard:yellow_green_dye", new DyeItem(new Color(0x9A, 0xCD, 0x32)));
  public static Item REBECCA_PURPLE_DYE = ITEMS.Register("dyehard:rebecca_purple_dye", new DyeItem(new Color(0x66, 0x33, 0x99)));

  public static void Init()
  {
    Debug.Log("DyeHardItems static constructor");
  }
}