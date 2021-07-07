﻿using BannerlordCheats.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;

namespace BannerlordCheats.Patches.Smithing
{
    [HarmonyPatch(typeof(CraftingCampaignBehavior), nameof(CraftingCampaignBehavior.IsOpened))]
    public static class UnlockAllParts
    {
        [HarmonyPostfix]
        public static void IsOpened(CraftingPiece craftingPiece, ref bool __result)
        {
            if (BannerlordCheatsSettings.Instance?.UnlockAllParts == true)
            {
                __result = true;
            }
        }
    }
}
