﻿using BannerlordCheats.Extensions;
using BannerlordCheats.Settings;
using HarmonyLib;
using SandBox;
using TaleWorlds.MountAndBlade;

namespace BannerlordCheats.Patches.Combat
{
    [HarmonyPatch(typeof(SandboxAgentApplyDamageModel), nameof(SandboxAgentApplyDamageModel.DecidePassiveAttackCollisionReaction))]
    public static class AlwaysSliceThroughPassivePatch
    {
        [HarmonyPostfix]
        public static void DecidePassiveAttackCollisionReaction(
            ref Agent attacker,
            ref Agent defender,
            ref bool isFatalHit,
            ref MeleeCollisionReaction __result)
        {
            if (attacker.IsPlayer()
                && BannerlordCheatsSettings.TryGetModifiedValue(x => x.SliceThroughEveryone, out var sliceThroughEveryone)
                && sliceThroughEveryone)
            {
                __result = MeleeCollisionReaction.SlicedThrough;
            }
        }
    }

    [HarmonyPatch(typeof(Mission), "DecideWeaponCollisionReaction")]
    public static class AlwaysSliceThroughWeaponPatch
    {
        [HarmonyPostfix]
        public static void DecideWeaponCollisionReaction(
            ref Blow registeredBlow,
            ref AttackCollisionData collisionData,
            ref Agent attacker,
            ref Agent defender,
            ref MissionWeapon attackerWeapon,
            ref bool isFatalHit,
            ref bool isShruggedOff,
            ref MeleeCollisionReaction colReaction)
        {
            if (attacker.IsPlayer()
                && BannerlordCheatsSettings.TryGetModifiedValue(x => x.SliceThroughEveryone, out var sliceThroughEveryone)
                && sliceThroughEveryone)
            {
                colReaction = MeleeCollisionReaction.SlicedThrough;
            }
        }
    }
}
