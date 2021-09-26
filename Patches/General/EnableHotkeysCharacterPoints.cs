﻿using BannerlordCheats.Extensions;
using BannerlordCheats.Localization;
using BannerlordCheats.Settings;
using HarmonyLib;
using JetBrains.Annotations;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace BannerlordCheats.Patches.General
{
    [HarmonyPatch(typeof(Module), "OnApplicationTick")]
    public static class EnableHotkeysCharacterPoints
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void OnApplicationTick()
        {
            if (ScreenManager.TopScreen is GauntletCharacterDeveloperScreen
                && BannerlordCheatsSettings.Instance?.EnableHotkeys == true)
            {
                if (Keys.IsKeyPressed(InputKey.LeftControl, InputKey.F))
                {
                    var charVM = ScreenManager.TopScreen.GetViewModel<CharacterDeveloperVM>();

                    var currentHero = charVM.CurrentCharacter.Hero;

                    currentHero.HeroDeveloper.UnspentFocusPoints++;

                    charVM.CurrentCharacter.UnspentCharacterPoints++;

                    var message = string.Format(L10N.GetText("AddUnspentFocusPointMessage"), currentHero.Name);

                    InformationManager.DisplayMessage(new InformationMessage(message, Color.White));
                }
                else if (Keys.IsKeyPressed(InputKey.LeftControl, InputKey.G))
                {
                    var charVM = ScreenManager.TopScreen.GetViewModel<CharacterDeveloperVM>();

                    var currentHero = charVM.CurrentCharacter.Hero;

                    currentHero.HeroDeveloper.UnspentAttributePoints++;

                    charVM.CurrentCharacter.UnspentAttributePoints++;

                    var message = string.Format(L10N.GetText("AddUnspentAttributePointMessage"), currentHero.Name);

                    InformationManager.DisplayMessage(new InformationMessage(message, Color.White));
                }
            }
        }
    }
}
