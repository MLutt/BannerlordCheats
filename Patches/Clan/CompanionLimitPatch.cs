﻿using BannerlordCheats.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordCheats.Patches.Clan
{
    [HarmonyPatch(typeof(DefaultClanTierModel), nameof(DefaultClanTierModel.GetCompanionLimit))]
    public static class CompanionLimitPatch
    {
        [HarmonyPostfix]
        public static void GetCompanionLimit(ref TaleWorlds.CampaignSystem.Clan clan, ref int __result)
        {
            if ((clan?.Leader?.IsHumanPlayerCharacter ?? false)
                && BannerlordCheatsSettings.Instance.ExtraCompanionLimit > 0)
            {
                __result += BannerlordCheatsSettings.Instance.ExtraCompanionLimit;
            }
        }
    }
}
