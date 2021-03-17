﻿using BannerlordCheats.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;

namespace BannerlordCheats.Patches.Combat
{
    [HarmonyPatch(typeof(DefaultTroopSacrificeModel), nameof(DefaultTroopSacrificeModel.GetNumberOfTroopsSacrificedForTryingToGetAway))]
    public static class NoTroopSacrificePatchRunaway
    {
        [HarmonyPostfix]
        public static void GetNumberOfTroopsSacrificedForTryingToGetAway(BattleSideEnum battleSide, MapEvent mapEvent, ref int __result)
        {
            if (BannerlordCheatsSettings.Instance.NoTroopSacrifice
                && battleSide == mapEvent.PlayerSide)
            {
                __result = 0;
            }
        }
    }

    [HarmonyPatch(typeof(DefaultTroopSacrificeModel), nameof(DefaultTroopSacrificeModel.GetLostTroopCountForBreakingInBesiegedSettlement))]
    public static class NoTroopSacrificePatchBreakIn
    {
        [HarmonyPostfix]
        public static void GetLostTroopCountForBreakingInBesiegedSettlement(MobileParty party, SiegeEvent siegeEvent, ref int __result)
        {
            if (BannerlordCheatsSettings.Instance.NoTroopSacrifice
                && (party?.IsMainParty ?? false))
            {
                __result = 0;
            }
        }
    }

    [HarmonyPatch(typeof(DefaultTroopSacrificeModel), nameof(DefaultTroopSacrificeModel.GetLostTroopCountForBreakingOutOfBesiegedSettlement))]
    public static class NoTroopSacrificePatchBreakOut
    {
        [HarmonyPostfix]
        public static void GetLostTroopCountForBreakingOutOfBesiegedSettlement(MobileParty party, SiegeEvent siegeEvent, ref int __result)
        {
            if (BannerlordCheatsSettings.Instance.NoTroopSacrifice
                && (party?.IsMainParty ?? false))
            {
                __result = 0;
            }
        }
    }
}
