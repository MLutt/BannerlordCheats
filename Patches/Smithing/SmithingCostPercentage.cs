﻿using System;
using BannerlordCheats.Settings;
using HarmonyLib;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;

namespace BannerlordCheats.Patches.Smithing
{
    [HarmonyPatch(typeof(DefaultSmithingModel), nameof(DefaultSmithingModel.GetSmithingCostsForWeaponDesign))]
    public static class SmithingCostPercentage
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void GetSmithingCostsForWeaponDesign(WeaponDesign weaponDesign, ref int[] __result)
        {
            try
            {
                if (BannerlordCheatsSettings.Instance?.SmithingCostPercentage < 100f)
                {
                    var factor = BannerlordCheatsSettings.Instance.SmithingCostPercentage / 100f;

                    for (var i = 0; i < __result.Length; i++)
                    {
                        var newValue = (int)Math.Round(factor * __result[i]);

                        __result[i] = newValue;
                    }
                }
            }
            catch (Exception e)
            {
                SubModule.LogError(e, typeof(SmithingCostPercentage));
            }
        }
    }
}
