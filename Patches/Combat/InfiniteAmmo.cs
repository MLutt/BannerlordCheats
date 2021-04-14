﻿using BannerlordCheats.Extensions;
using BannerlordCheats.Settings;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BannerlordCheats.Patches.Combat
{
    [HarmonyPatch(typeof(Agent), "OnWeaponAmountChange")]
    public static class InfiniteAmmoValue
    {
        [HarmonyPrefix]
        public static void OnWeaponAmountChange(ref EquipmentIndex slotIndex, ref short amount, ref Agent __instance)
        {
            if (__instance.IsPlayer()
                && BannerlordCheatsSettings.TryGetModifiedValue(x => x.InfiniteAmmo, out var infiniteAmmo)
                && infiniteAmmo)
            {
                var fullAmount = __instance.Equipment[slotIndex].MaxAmmo;

                if (amount < fullAmount)
                {
                    __instance.SetWeaponAmountInSlot(slotIndex, fullAmount, false);
                }
            }
        }
    }
}
