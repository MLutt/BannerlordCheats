﻿using BannerlordCheats.Settings;
using HarmonyLib;
using System;
using JetBrains.Annotations;
using TaleWorlds.MountAndBlade;

namespace BannerlordCheats.Patches.Combat
{
    [HarmonyPatch(typeof(Module), "OnApplicationTick")]
    public static class HealthRegeneration
    {
        private static int? LastSet = null;

        [UsedImplicitly]
        [HarmonyPostfix]
        public static void OnApplicationTick(float dt)
        {
            try
            {
                if (Mission.Current != null
                    && Agent.Main != null
                    && MBCommon.IsPaused != true
                    && BannerlordCheatsSettings.Instance?.HealthRegeneration > 0f)
                {
                    var now = DateTime.Now.Second;

                    if (LastSet == null || now != LastSet)
                    {
                        LastSet = now;

                        float health = Agent.Main.Health;
                        float maxHealth = Agent.Main.HealthLimit;

                        if (health < maxHealth)
                        {
                            float regen = (BannerlordCheatsSettings.Instance.HealthRegeneration / maxHealth) * 100;
                            float newHealth = (float) Math.Round(health + regen);

                            Agent.Main.Health = Math.Min(maxHealth, newHealth);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SubModule.LogError(e, typeof(HealthRegeneration));
            }
        }
    }
}
