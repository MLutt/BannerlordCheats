﻿using System;
using System.Linq;
using System.Linq.Expressions;
using BannerlordCheats.Localization;
using System.Reflection;
using System.Text.RegularExpressions;
using MCM.Abstractions.Settings.Base.PerSave;

namespace BannerlordCheats.Settings
{
    public class BannerlordCheatsSettings : AttributePerSaveSettings<BannerlordCheatsSettings>
    {
        #region Base

        private const string ModName = "ModName";
        private const string CombatGroupName = "Combat";
        private const string GeneralGroupName = "General";
        private const string MapGroupName = "Map";
        private const string InventoryGroupName = "Inventory";
        private const string PartyGroupName = "Party";
        private const string ClanGroupName = "Clan";
        private const string KingdomGroupName = "Kingdom";
        private const string ExperienceGroupName = "Experience";
        private const string SiegesGroupName = "Sieges";
        private const string ArmyGroupName = "Army";
        private const string SmithingGroupName = "Smithing";
        private const string SettlementsGroupName = "Settlements";
        private const string CharactersGroupName = "Characters";
        private const string WorkshopsGroupName = "Workshops";

        [Obsolete("Use TryGetModifiedValue instead of accessing the instance directly.", true)]
        public new static BannerlordCheatsSettings Instance => AttributePerSaveSettings<BannerlordCheatsSettings>.Instance;

        public override string Id { get; } = $"BannerlordCheats_v{Assembly.GetExecutingAssembly().GetName().Version.Major}";

        public override string FolderName { get; } = "Cheats";

        public override string DisplayName { get; }

        #endregion Base

        public BannerlordCheatsSettings()
        {
            string modName;

            try { modName = L10N.GetText(BannerlordCheatsSettings.ModName); }
            catch { modName = "Cheats"; }

            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            version = Regex.Replace(version, @"\.0", string.Empty);
            if (!version.Contains(".")) {  version += ".0"; }

            this.DisplayName = $"{modName} {version}";
        }

        public static bool TryGetModifiedValue<T>(Expression<Func<BannerlordCheatsSettings, T>> expression, out T modifiedValue)
        {
            var member = ((MemberExpression) expression.Body).Member;

            var attribute = member.GetCustomAttributes(typeof(LocalizedSettingProperty), true).OfType<LocalizedSettingProperty>().Single();

            var defaultValue = attribute.DefaultValue;

            var instance = AttributePerSaveSettings<BannerlordCheatsSettings>.Instance;

            if (instance == null)
            {
                modifiedValue = (T) defaultValue;

                return false;
            }

            var value = ((PropertyInfo) member).GetValue(instance);

            if (value.Equals(defaultValue))
            {
                modifiedValue = (T) defaultValue;

                return false;
            }

            modifiedValue = (T) value;

            return true;
        }

        #region General

        [LocalizedSettingPropertyGroup(GeneralGroupName)]
        [LocalizedSettingPropertyBool(nameof(EnableHotkeys), false)]
        public bool EnableHotkeys { get; set; } = false;

        [LocalizedSettingPropertyGroup(GeneralGroupName)]
        [LocalizedSettingPropertyBool(nameof(EnableHotkeyTips), false)]
        public bool EnableHotkeyTips { get; set; } = false;

        [LocalizedSettingPropertyGroup(GeneralGroupName)]
        [LocalizedSettingPropertyBool(nameof(OverrideCheatMode), false)]
        public bool OverrideCheatMode { get; set; } = false;

        #endregion General

        #region Map

        [LocalizedSettingPropertyGroup(MapGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(MapSpeedMultiplier), minValue: 1.0f, maxValue: 100.0f, 1.0f)]
        public float MapSpeedMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(MapGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(MapVisibilityMultiplier), minValue: 1.0f, maxValue: 100.0f, 1.0f)]
        public float MapVisibilityMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(MapGroupName)]
        [LocalizedSettingPropertyPercent(nameof(NpcMapSpeedPercentage), 100.0f)]
        public float NpcMapSpeedPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(MapGroupName)]
        [LocalizedSettingPropertyBool(nameof(PartyInvisibleOnMap), false)]
        public bool PartyInvisibleOnMap { get; set; } = false;

        #endregion Map

        #region Combat

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyPercent(nameof(DamageTakenPercentage), 100.0f)]
        public float DamageTakenPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(Invincible), false)]
        public bool Invincible { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(PlayerHorseInvincible), false)]
        public bool PlayerHorseInvincible { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(PartyInvincible), false)]
        public bool PartyInvincible { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(PartyHeroesInvincible), false)]
        public bool PartyHeroesInvincible { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyPercent(nameof(PartyDamageTakenPercentage), 100.0f)]
        public float PartyDamageTakenPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(OneHitKill), false)]
        public bool OneHitKill { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(PartyOneHitKill), false)]
        public bool PartyOneHitKill { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(PartyOnlyKnockout), false)]
        public bool PartyOnlyKnockout { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(EnemyOnlyKnockout), false)]
        public bool EnemyOnlyKnockout { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(RenownRewardMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float RenownRewardMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(InfluenceRewardMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float InfluenceRewardMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(AlwaysWinBattleSimulation), false)]
        public bool AlwaysWinBattleSimulation { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(NoTroopSacrifice), false)]
        public bool NoTroopSacrifice { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(NoRunningAway), false)]
        public bool NoRunningAway { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(EnemiesNoRunningAway), false)]
        public bool EnemiesNoRunningAway { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyInteger(nameof(BanditHideoutTroopLimit), minValue: 0, maxValue: 1000, 0)]
        public int BanditHideoutTroopLimit { get; set; } = 0;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(AlwaysCrushThroughShields), false)]
        public bool AlwaysCrushThroughShields { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(SliceThroughEveryone), false)]
        public bool SliceThroughEveryone { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyPercent(nameof(HealthRegeneration), 0.0f)]
        public float HealthRegeneration { get; set; } = 0.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyPercent(nameof(PartyHealthRegeneration), 0.0f)]
        public float PartyHealthRegeneration { get; set; } = 0.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(InfiniteAmmo), false)]
        public bool InfiniteAmmo { get; set; } = false;

        // [LocalizedSettingPropertyGroup(CombatGroupName)]
        // [LocalizedSettingPropertyBool(nameof(PartyInfiniteAmmo))]
        // public bool PartyInfiniteAmmo { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(DamageMultiplier), minValue: 1.0f, maxValue: 10.0f, 1.0f)]
        public float DamageMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(PartyDamageMultiplier), minValue: 1.0f, maxValue: 10.0f, 1.0f)]
        public float PartyDamageMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(NoFriendlyFire), false)]
        public bool NoFriendlyFire { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(CombatZoomMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float CombatZoomMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(InstantCrossbowReload), false)]
        public bool InstantCrossbowReload { get; set; } = false;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyPercent(nameof(CompanionDeathPercentage), 100.0f)]
        public float CompanionDeathPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyPercent(nameof(FriendlyLordCombatDeathPercentage), 100.0f)]
        public float FriendlyLordCombatDeathPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyPercent(nameof(EnemyLordCombatDeathPercentage), 100.0f)]
        public float EnemyLordCombatDeathPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(CombatGroupName)]
        [LocalizedSettingPropertyBool(nameof(AlwaysKnockDown), false)]
        public bool AlwaysKnockDown { get; set; } = false;

        #endregion Combat

        #region Inventory

        [LocalizedSettingPropertyGroup(InventoryGroupName)]
        [LocalizedSettingPropertyInteger(nameof(ExtraInventoryCapacity), minValue: 0, maxValue: 1000000, 0)]
        public int ExtraInventoryCapacity { get; set; } = 0;

        #endregion Inventory

        #region Party

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyInteger(nameof(ExtraPartyMemberSize), minValue: 0, maxValue: 10000, 0)]
        public int ExtraPartyMemberSize { get; set; } = 0;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyInteger(nameof(ExtraPartyPrisonerSize), minValue: 0, maxValue: 10000, 0)]
        public int ExtraPartyPrisonerSize { get; set; } = 0;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyInteger(nameof(ExtraPartyMorale), minValue: 0, maxValue: 100, 0)]
        public int ExtraPartyMorale { get; set; } = 0;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyBool(nameof(InstantEscape), false)]
        public bool InstantEscape { get; set; } = false;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyPercent(nameof(FoodConsumptionPercentage), 100.0f)]
        public float FoodConsumptionPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyPercent(nameof(TroopWagesPercentage), 100.0f)]
        public float TroopWagesPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyBool(nameof(FreeTroopUpgrades), false)]
        public bool FreeTroopUpgrades { get; set; } = false;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyBool(nameof(FreeCompanionHiring), false)]
        public bool FreeCompanionHiring { get; set; } = false;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyBool(nameof(InstantPrisonerRecruitment), false)]
        public bool InstantPrisonerRecruitment { get; set; } = false;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyBool(nameof(NoPrisonerEscape), false)]
        public bool NoPrisonerEscape { get; set; } = false;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(PartyHealingMultiplier), minValue: 1.0f, maxValue: 100.0f, 1.0f)]
        public float PartyHealingMultiplier { get; set; } = 1.0f;

        #endregion

        #region Clan

        [LocalizedSettingPropertyGroup(ClanGroupName)]
        [LocalizedSettingPropertyInteger(nameof(ExtraCompanionLimit), minValue: 0, maxValue: 100, 0)]
        public int ExtraCompanionLimit { get; set; } = 0;

        [LocalizedSettingPropertyGroup(ClanGroupName)]
        [LocalizedSettingPropertyInteger(nameof(ExtraClanPartyLimit), minValue: 0, maxValue: 100, 0)]
        public int ExtraClanPartyLimit { get; set; } = 0;

        [LocalizedSettingPropertyGroup(ClanGroupName)]
        [LocalizedSettingPropertyInteger(nameof(ExtraClanPartySize), minValue: 0, maxValue: 10000, 0)]
        public int ExtraClanPartySize { get; set; } = 0;

        #endregion Clan

        #region Characters

        [LocalizedSettingPropertyGroup(CharactersGroupName)]
        [LocalizedSettingPropertyBool(nameof(PerfectRelationships), false)]
        public bool PerfectRelationships { get; set; } = false;

        [LocalizedSettingPropertyGroup(CharactersGroupName)]
        [LocalizedSettingPropertyBool(nameof(BarterOfferAlwaysAccepted), false)]
        public bool BarterOfferAlwaysAccepted { get; set; } = false;

        [LocalizedSettingPropertyGroup(CharactersGroupName)]
        [LocalizedSettingPropertyBool(nameof(NoBarterCooldown), false)]
        public bool NoBarterCooldown { get; set; } = false;

        [LocalizedSettingPropertyGroup(CharactersGroupName)]
        [LocalizedSettingPropertyBool(nameof(ConversationAlwaysSuccessful), false)]
        public bool ConversationAlwaysSuccessful { get; set; } = false;

        #endregion Characters

        #region Kingdom

        [LocalizedSettingPropertyGroup(KingdomGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(KingdomDecisionWeightMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float KingdomDecisionWeightMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(KingdomGroupName)]
        [LocalizedSettingPropertyBool(nameof(NoRelationshipLossOnDecision), false)]
        public bool NoRelationshipLossOnDecision { get; set; } = false;

        [LocalizedSettingPropertyGroup(KingdomGroupName)]
        [LocalizedSettingPropertyBool(nameof(NoCrimeRatingForCrimes), false)]
        public bool NoCrimeRatingForCrimes { get; set; } = false;

        [LocalizedSettingPropertyGroup(PartyGroupName)]
        [LocalizedSettingPropertyPercent(nameof(DecisionOverrideInfluenceCostPercentage), 100.0f)]
        public float DecisionOverrideInfluenceCostPercentage { get; set; } = 100.0f;

        #endregion Kingdom

        #region Experience

        [LocalizedSettingPropertyGroup(ExperienceGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(ExperienceMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float ExperienceMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(ExperienceGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(CompanionExperienceMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float CompanionExperienceMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(ExperienceGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(LearningRateMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float LearningRateMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(ExperienceGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(CompanionLearningRateMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float CompanionLearningRateMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(ExperienceGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(LearningLimitMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float LearningLimitMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(ExperienceGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(TroopExperienceMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float TroopExperienceMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(ExperienceGroupName)]
        [LocalizedSettingPropertyBool(nameof(BannerlordCheatsSettings.FreeFocusPointAssignment), false)]
        public bool FreeFocusPointAssignment { get; set; } = false;

        #endregion Experience

        #region Sieges

        [LocalizedSettingPropertyGroup(SiegesGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(SiegeBuildingSpeedMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float SiegeBuildingSpeedMultiplier { get; set; } = 1.0f;
        
        [LocalizedSettingPropertyGroup(SiegesGroupName)]
        [LocalizedSettingPropertyPercent(nameof(EnemySiegeBuildingSpeedPercentage), 100.0f)]
        public float EnemySiegeBuildingSpeedPercentage { get; set; } = 100.0f;

        #endregion Sieges

        #region Army

        [LocalizedSettingPropertyGroup(ArmyGroupName)]
        [LocalizedSettingPropertyPercent(nameof(FactionArmyCohesionLossPercentage), 100.0f)]
        public float FactionArmyCohesionLossPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(ArmyGroupName)]
        [LocalizedSettingPropertyPercent(nameof(ArmyCohesionLossPercentage), 100.0f)]
        public float ArmyCohesionLossPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(ArmyGroupName)]
        [LocalizedSettingPropertyPercent(nameof(ArmyFoodConsumptionPercentage), 100.0f)]
        public float ArmyFoodConsumptionPercentage { get; set; } = 100.0f;

        #endregion Army

        #region Settlements

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyBool(nameof(DisguiseAlwaysWorks), false)]
        public bool DisguiseAlwaysWorks { get; set; } = false;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyBool(nameof(OneDayConstruction), false)]
        public bool OneDayConstruction { get; set; } = false;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyBool(nameof(FreeTroopRecruitment), false)]
        public bool FreeTroopRecruitment { get; set; } = false;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyPercent(nameof(ItemTradingCostPercentage), 100.0f)]
        public float ItemTradingCostPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(SellingPriceMultiplier), minValue: 1.0f, maxValue: 1000.0f, 1.0f)]
        public float SellingPriceMultiplier { get; set; } = 1.0f;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyInteger(nameof(DailyFoodBonus), minValue: 0, maxValue: 10000, 0)]
        public int DailyFoodBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyInteger(nameof(DailyGarrisonBonus), minValue: 0, maxValue: 10000, 0)]
        public int DailyGarrisonBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyInteger(nameof(DailyMilitiaBonus), minValue: 0, maxValue: 10000, 0)]
        public int DailyMilitiaBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyInteger(nameof(DailyProsperityBonus), minValue: 0, maxValue: 10000, 0)]
        public int DailyProsperityBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyInteger(nameof(DailyLoyaltyBonus), minValue: 0, maxValue: 10000, 0)]
        public int DailyLoyaltyBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyInteger(nameof(DailySecurityBonus), minValue: 0, maxValue: 10000, 0)]
        public int DailySecurityBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyInteger(nameof(DailyHearthsBonus), minValue: 0, maxValue: 10000, 0)]
        public int DailyHearthsBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyPercent(nameof(GarrisonWagesPercentage), 100.0f)]
        public float GarrisonWagesPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(SettlementsGroupName)]
        [LocalizedSettingPropertyBool(nameof(NeverRequireCivilianEquipment), false)]
        public bool NeverRequireCivilianEquipment { get; set; } = false;

        #endregion Settlements

        #region Smithing

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyPercent(nameof(SmithingEnergyCostPercentage), 100.0f)]
        public float SmithingEnergyCostPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyBool(nameof(UnlockAllParts), false)]
        public bool UnlockAllParts { get; set; } = false;

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyPercent(nameof(SmithingDifficultyPercentage), 100.0f)]
        public float SmithingDifficultyPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyPercent(nameof(SmithingCostPercentage), 100.0f)]
        public float SmithingCostPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyInteger(nameof(CraftedWeaponHandlingBonus), minValue: 0, maxValue: 100, 0)]
        public int CraftedWeaponHandlingBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyInteger(nameof(CraftedWeaponSwingDamageBonus), minValue: 0, maxValue: 100, 0)]
        public int CraftedWeaponSwingDamageBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyInteger(nameof(CraftedWeaponSwingSpeedBonus), minValue: 0, maxValue: 100, 0)]
        public int CraftedWeaponSwingSpeedBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyInteger(nameof(CraftedWeaponThrustDamageBonus), minValue: 0, maxValue: 100, 0)]
        public int CraftedWeaponThrustDamageBonus { get; set; } = 0;

        [LocalizedSettingPropertyGroup(SmithingGroupName)]
        [LocalizedSettingPropertyInteger(nameof(CraftedWeaponThrustSpeedBonus), minValue: 0, maxValue: 100, 0)]
        public int CraftedWeaponThrustSpeedBonus { get; set; } = 0;

        #endregion Smithing

        #region Workshops

        [LocalizedSettingPropertyGroup(WorkshopsGroupName)]
        [LocalizedSettingPropertyPercent(nameof(WorkshopBuyingCostPercentage), 100.0f)]
        public float WorkshopBuyingCostPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(WorkshopsGroupName)]
        [LocalizedSettingPropertyPercent(nameof(WorkshopDailyExpensePercentage), 100.0f)]
        public float WorkshopDailyExpensePercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(WorkshopsGroupName)]
        [LocalizedSettingPropertyPercent(nameof(WorkshopUpgradeCostPercentage), 100.0f)]
        public float WorkshopUpgradeCostPercentage { get; set; } = 100.0f;

        [LocalizedSettingPropertyGroup(WorkshopsGroupName)]
        [LocalizedSettingPropertyFloatingInteger(nameof(WorkshopSellingCostMultiplier), minValue: 1.0f, maxValue: 100.0f, 1.0f)]
        public float WorkshopSellingCostMultiplier { get; set; } = 1.0f;

        #endregion Workshops
    }
}
