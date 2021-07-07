﻿using BannerlordCheats.Extensions;
using BannerlordCheats.Localization;
using BannerlordCheats.Settings;
using HarmonyLib;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace BannerlordCheats.Patches.General
{
    [HarmonyPatch(typeof(Module), "OnApplicationTick")]
    public static class EnableHotkeysMoney
    {
        [HarmonyPostfix]
        public static void OnApplicationTick()
        {
            if (ScreenManager.TopScreen is InventoryGauntletScreen
                && BannerlordCheatsSettings.Instance?.EnableHotkeys == true)
            {
                if (Keys.IsKeyPressed(InputKey.LeftControl, InputKey.LeftShift, InputKey.X))
                {
                    Hero.MainHero.ChangeHeroGold(100000);

                    InformationManager.DisplayMessage(new InformationMessage(string.Format(L10N.GetText("AddGoldMessage"), 100000), Color.White));
                }
                else if (Keys.IsKeyPressed(InputKey.LeftControl, InputKey.X))
                {
                    Hero.MainHero.ChangeHeroGold(1000);

                    InformationManager.DisplayMessage(new InformationMessage(string.Format(L10N.GetText("AddGoldMessage"), 1000), Color.White));
                }
            }
        }
    }
}
