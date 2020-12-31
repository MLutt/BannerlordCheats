﻿using BannerlordCheats.Settings;
using HarmonyLib;
using SandBox;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BannerlordCheats.Patches.Combat
{
    [HarmonyPatch(typeof(SandboxAgentApplyDamageModel), nameof(SandboxAgentApplyDamageModel.CalculateDamage))]
    public static class PartyDamageMultiplier
    {
        [HarmonyPostfix]
        public static void CalculateDamage(ref AttackInformation attackInformation, ref AttackCollisionData collisionData, WeaponComponentData weapon, ref float __result)
        {
            if ((attackInformation.AttackerFormation?.Team?.IsPlayerTeam ?? false)
                && !attackInformation.IsFriendlyFire
                && !BannerlordCheatsSettings.Instance.PartyOneHitKill)
            {
                __result *= BannerlordCheatsSettings.Instance.PartyDamageMultiplier;
            }
        }
    }
}
