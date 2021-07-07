﻿using BannerlordCheats.Extensions;
using BannerlordCheats.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace BannerlordCheats.Patches.Characters
{
    [HarmonyPatch(typeof(Hero), nameof(Hero.GetRelation))]
    public static class PerfectRelationships
    {
        [HarmonyPostfix]
        public static void GetRelation(Hero otherHero, ref Hero __instance, ref int __result)
        {
            if ((__instance.IsPlayer() || otherHero.IsPlayer())
                && BannerlordCheatsSettings.Instance?.PerfectRelationships == true)
            {
                __result = 100;
            }
        }
    }
}
