﻿using System;
using BannerlordCheats.Extensions;
using BannerlordCheats.Settings;
using HarmonyLib;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem.Settlements;

namespace BannerlordCheats.Patches.Settlements
{
    [HarmonyPatch(typeof(Town), nameof(Town.ProsperityChange), MethodType.Getter)]
    public static class DailyProsperityBonus
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void ProsperityChange(ref Town __instance, ref float __result)
        {
            try
            {
                if (__instance.IsPlayerTown()
                    && BannerlordCheatsSettings.Instance?.DailyProsperityBonus > 0)
                {
                    __result += BannerlordCheatsSettings.Instance.DailyProsperityBonus;
                }
            }
            catch (Exception e)
            {
                SubModule.LogError(e, typeof(DailyProsperityBonus));
            }
        }
    }
}
