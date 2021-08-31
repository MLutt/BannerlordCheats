﻿using BannerlordCheats.Settings;
using HarmonyLib;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace BannerlordCheats.Patches.Experience
{
    [HarmonyPatch(typeof(HeroDeveloper), nameof(HeroDeveloper.GetRequiredFocusPointsToAddFocus))]
    public static class FreeFocusPointAssignment
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void GetRequiredFocusPointsToAddFocus(ref SkillObject skill, ref int __result)
        {
            if (BannerlordCheatsSettings.Instance?.FreeFocusPointAssignment == true)
            {
                __result = 0;
            }
        }
    }
}
