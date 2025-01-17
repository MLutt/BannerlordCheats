﻿using System;
using BannerlordCheats.Settings;
using HarmonyLib;
using System.Linq;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem.Election;

namespace BannerlordCheats.Patches.Kingdom
{
    [HarmonyPatch(typeof(DecisionOutcome), nameof(DecisionOutcome.TotalSupportPoints), MethodType.Getter)]
    public static class KingdomDecisionWeightMultiplier
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void Getter(ref DecisionOutcome __instance, ref float __result)
        {
            try
            {
                if (__instance.SupporterList.Any(x => x.IsPlayer)
                    && BannerlordCheatsSettings.Instance?.KingdomDecisionWeightMultiplier > 1f)
                {
                    __result *= BannerlordCheatsSettings.Instance.KingdomDecisionWeightMultiplier;
                }
            }
            catch (Exception e)
            {
                SubModule.LogError(e, typeof(KingdomDecisionWeightMultiplier));
            }
        }
    }
}
