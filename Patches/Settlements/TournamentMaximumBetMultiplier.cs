﻿using System;
using BannerlordCheats.Settings;
using HarmonyLib;
using JetBrains.Annotations;
using SandBox.Tournaments.MissionLogics;

namespace BannerlordCheats.Patches.Settlements
{
    [HarmonyPatch(typeof(TournamentBehavior), nameof(TournamentBehavior.GetMaximumBet))]
    public static class TournamentMaximumBetMultiplier
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void GetMaximumBet(ref int __result)
        {
            try
            {
                if (BannerlordCheatsSettings.Instance?.TournamentMaximumBetMultiplier > 1)
                {
                    var newValue = (int) Math.Round(__result * BannerlordCheatsSettings.Instance.TournamentMaximumBetMultiplier);

                    __result = newValue;
                }
            }
            catch (Exception e)
            {
                SubModule.LogError(e, typeof(TournamentMaximumBetMultiplier));
            }
        }
    }
}
