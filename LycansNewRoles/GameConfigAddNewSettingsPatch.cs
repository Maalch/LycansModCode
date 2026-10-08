using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib;
using LycansNewRoles.NewItems.Accessories;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameSettingsUI), "Start")]
internal class GameConfigAddNewSettingsPatch
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static UnityAction<bool> _003C0_003E__UpdateDeceiverHasFlatulenceChance;

		public static UnityAction<bool> _003C1_003E__UpdateLoverWolfReplacesVillagerChance;

		public static UnityAction<bool> _003C2_003E__UpdateTrapsModified;

		public static UnityAction<bool> _003C3_003E__UpdateSmokeBoosted;

		public static UnityAction<bool> _003C4_003E__UpdateAnonymousVotes;

		public static UnityAction<bool> _003C5_003E__UpdateSabotagesAvailable;

		public static UnityAction<bool> _003C6_003E__UpdateDropItemsAvailable;

		public static UnityAction<bool> _003C7_003E__UpdateWolvesCanUseItems;

		public static UnityAction<bool> _003C8_003E__UpdateDraftMode;

		public static UnityAction<bool> _003C9_003E__UpdateAllowMayor;

		public static UnityAction<bool> _003C10_003E__UpdateShowLastGameSummary;

		public static UnityAction<bool> _003C11_003E__UpdateTenacityHubris;

		public static UnityAction<bool> _003C12_003E__UpdateAdaptMeetingTimer;

		public static UnityAction<bool> _003C13_003E__UpdateForceWolfTransformation;

		public static UnityAction _003C14_003E__OnClickConfigBase;

		public static UnityAction _003C15_003E__OnClickConfigRolesVillagers;

		public static UnityAction _003C16_003E__OnClickConfigRolesEnemies;

		public static UnityAction _003C17_003E__OnClickConfigRolesSecondary;

		public static UnityAction _003C18_003E__OnClickConfigRolesDetails;

		public static UnityAction _003C19_003E__OnClickConfigOthers;

		public static UnityAction _003C20_003E__OnClickConfigPotions;

		public static UnityAction _003C21_003E__OnClickConfigGadgets;

		public static UnityAction _003C22_003E__OnClickConfigEvents;
	}

	private static void Postfix(GameSettingsUI __instance)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Expected I4, but got Unknown
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ede: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9c: Expected O, but got Unknown
		//IL_0f57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f31: Expected O, but got Unknown
		//IL_0fec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1008: Unknown result type (might be due to invalid IL or missing references)
		//IL_100d: Unknown result type (might be due to invalid IL or missing references)
		//IL_102c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc6: Expected O, but got Unknown
		//IL_1081: Unknown result type (might be due to invalid IL or missing references)
		//IL_108b: Unknown result type (might be due to invalid IL or missing references)
		//IL_109d: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1050: Unknown result type (might be due to invalid IL or missing references)
		//IL_1055: Unknown result type (might be due to invalid IL or missing references)
		//IL_105b: Expected O, but got Unknown
		//IL_1116: Unknown result type (might be due to invalid IL or missing references)
		//IL_1120: Unknown result type (might be due to invalid IL or missing references)
		//IL_1132: Unknown result type (might be due to invalid IL or missing references)
		//IL_1137: Unknown result type (might be due to invalid IL or missing references)
		//IL_1156: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f0: Expected O, but got Unknown
		//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_117a: Unknown result type (might be due to invalid IL or missing references)
		//IL_117f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1185: Expected O, but got Unknown
		//IL_1240: Unknown result type (might be due to invalid IL or missing references)
		//IL_124a: Unknown result type (might be due to invalid IL or missing references)
		//IL_125c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1261: Unknown result type (might be due to invalid IL or missing references)
		//IL_1280: Unknown result type (might be due to invalid IL or missing references)
		//IL_120f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1214: Unknown result type (might be due to invalid IL or missing references)
		//IL_121a: Expected O, but got Unknown
		//IL_12d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12df: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1315: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12af: Expected O, but got Unknown
		//IL_14d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1339: Unknown result type (might be due to invalid IL or missing references)
		//IL_133e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1344: Expected O, but got Unknown
		try
		{
			GameConfig.SoloRolesConfig.Clear();
			GameConfig.PrimaryRolePowerConfig.Clear();
			GameConfig.SecondaryRolesConfig.Clear();
			GameConfig.PotionsConfig.Clear();
			GameConfig.GadgetsConfig.Clear();
			GameConfig.AccessoriesConfig.Clear();
			Transform parent = ((TMP_Text)Object.FindObjectsOfType<TextMeshProUGUI>(true).First((TextMeshProUGUI o) => ((Object)((Component)((TMP_Text)o).transform.parent).gameObject).name == "GameSettingsTitle")).transform.parent;
			Transform val = Object.Instantiate<Transform>(parent, parent.parent);
			((TMP_Text)((Component)val).GetComponentInChildren<TextMeshProUGUI>()).fontSize = 24f;
			((LocalizedReference)((Component)val).GetComponentInChildren<LocalizeStringEvent>().StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_ROLE_INFO"));
			GameConfig.RoleInfoObject = ((Component)val).gameObject;
			GameConfig.VillagerPowersChanceConfig = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_ROLE_TYPE_MAIN_VILLAGER_COUNT", GameConfig.ConfigTypeEnum.RolesVillagers, GameConfig.ColorVillagerJob, 0, 100, 10);
			GameConfig.FillPrimaryRoleVillagerPowersChanceDropdown();
			List<PlayerCustom.PlayerPrimaryRolePower> list = (from PlayerCustom.PlayerPrimaryRolePower o in Enum.GetValues(typeof(PlayerCustom.PlayerPrimaryRolePower))
				where o != PlayerCustom.PlayerPrimaryRolePower.None && PlayerCustom.IsPrimaryRolePowerForNormalVillagers(o) && !PlayerCustom.IsPrimaryRolePowerDisabled(o) && !PlayerCustom.IsPrimaryRolePowerHidden(o) && o != PlayerCustom.PlayerPrimaryRolePower.Avatar && o != PlayerCustom.PlayerPrimaryRolePower.Mole
				select o).ToList();
			foreach (PlayerCustom.PlayerPrimaryRolePower item2 in list)
			{
				GameConfig.CreateAndAttachPrimaryRolePowerConfigToggle(__instance, item2, TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetPrimaryRolePowerString(item2)));
			}
			GameConfig.AvatarChanceConfig = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_ROLE_TYPE_MAIN_AVATAR_CHANCE", GameConfig.ConfigTypeEnum.RolesVillagers, GameConfig.ColorVillagerJob, 0, 100, 10);
			GameConfig.FillPrimaryRoleAvatarChanceDropdown();
			GameConfig.MoleChanceConfig = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_ROLE_TYPE_MAIN_MOLE_CHANCE", GameConfig.ConfigTypeEnum.RolesVillagers, GameConfig.ColorVillagerJob, 0, 100, 10);
			GameConfig.FillPrimaryRoleMoleChanceDropdown();
			GameConfig.ElitesCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_ELITES", 3, GameConfig.ConfigTypeEnum.RolesVillagers, GameConfig.ColorVillagerJob);
			GameConfig.FillElitesCountDropdown();
			List<PlayerCustom.PlayerPrimaryRolePower> list2 = (from PlayerCustom.PlayerPrimaryRolePower o in Enum.GetValues(typeof(PlayerCustom.PlayerPrimaryRolePower))
				where o != PlayerCustom.PlayerPrimaryRolePower.None && !PlayerCustom.IsPrimaryRolePowerDisabled(o) && !PlayerCustom.IsPrimaryRolePowerHidden(o) && PlayerCustom.IsPrimaryRolePowerForEliteVillagers(o)
				select o).ToList();
			foreach (PlayerCustom.PlayerPrimaryRolePower item3 in list2)
			{
				GameConfig.CreateAndAttachPrimaryRolePowerConfigToggle(__instance, item3, TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetPrimaryRolePowerString(item3)));
			}
			GameConfig.GuardianAngelsCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_GUARDIAN_ANGELS", 6, GameConfig.ConfigTypeEnum.RolesVillagers, GameConfig.ColorVillagerJob);
			GameConfig.FillGuardianAngelsCountDropdown();
			GameConfig.GhostsCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_GHOSTS", 6, GameConfig.ConfigTypeEnum.RolesVillagers, GameConfig.ColorVillagerJob);
			GameConfig.FillGhostsCountDropdown();
			GameConfig.SoloRolesCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_TYPE_MAIN_SOLO_COUNT", 5, GameConfig.ConfigTypeEnum.RolesEnemies, GameConfig.ColorSoloRole);
			GameConfig.FillSoloRolesCountDropdown();
			List<PlayerCustom.PlayerNewPrimaryRole> list3 = (from PlayerCustom.PlayerNewPrimaryRole o in Enum.GetValues(typeof(PlayerCustom.PlayerNewPrimaryRole))
				where o != PlayerCustom.PlayerNewPrimaryRole.None && o != PlayerCustom.PlayerNewPrimaryRole.Traitor && !PlayerCustom.IsNewPrimaryRoleDisabled(o)
				select o).ToList();
			foreach (PlayerCustom.PlayerNewPrimaryRole item4 in list3)
			{
				GameConfig.CreateAndAttachSoloRoleConfigToggle(__instance, item4, TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetNewPrimaryRoleString(item4)));
			}
			GameConfig.TraitorsCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_TYPE_MAIN_TRAITOR_COUNT", 4, GameConfig.ConfigTypeEnum.RolesEnemies, GameConfig.ColorTraitor);
			GameConfig.FillTraitorsCountDropdown();
			GameConfig.WolfPupsCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_TYPE_MAIN_WOLF_PUP_COUNT", 4, GameConfig.ConfigTypeEnum.RolesEnemies, GameConfig.ColorWolfPup);
			GameConfig.FillWolfPupsCountDropdown();
			GameConfig.WolfPowersCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_TYPE_MAIN_WOLF_COUNT", 6, GameConfig.ConfigTypeEnum.RolesEnemies, GameConfig.ColorWolfPower);
			GameConfig.FillPrimaryRolePowersCountDropdown();
			List<PlayerCustom.PlayerPrimaryRolePower> list4 = (from PlayerCustom.PlayerPrimaryRolePower o in Enum.GetValues(typeof(PlayerCustom.PlayerPrimaryRolePower))
				where o != PlayerCustom.PlayerPrimaryRolePower.None && PlayerCustom.IsPrimaryRolePowerForWolves(o) && !PlayerCustom.IsPrimaryRolePowerDisabled(o) && !PlayerCustom.IsPrimaryRolePowerHidden(o)
				select o).ToList();
			foreach (PlayerCustom.PlayerPrimaryRolePower item5 in list4)
			{
				GameConfig.CreateAndAttachPrimaryRolePowerConfigToggle(__instance, item5, TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetPrimaryRolePowerString(item5)));
			}
			GameConfig.SpectersCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_SPECTERS", 3, GameConfig.ConfigTypeEnum.RolesEnemies, GameConfig.ColorWolfPower);
			GameConfig.FillSpectersCountDropdown();
			GameConfig.SecondaryRolesCountConfig = GameConfig.CreateAndAttachConfigDropdownWithMax(__instance, "NALES_CONFIG_ROLE_TYPE_SECONDARY_COUNT", 15, GameConfig.ConfigTypeEnum.RolesSecondary, GameConfig.ColorSecondaryRole);
			GameConfig.FillSecondaryRolesCountDropdown();
			List<PlayerCustom.PlayerSecondaryRole> list5 = (from PlayerCustom.PlayerSecondaryRole o in Enum.GetValues(typeof(PlayerCustom.PlayerSecondaryRole))
				where o != PlayerCustom.PlayerSecondaryRole.None && !PlayerCustom.IsSecondaryRoleDisabled(o)
				select o).ToList();
			foreach (PlayerCustom.PlayerSecondaryRole item6 in list5)
			{
				GameConfig.CreateAndAttachSecondaryRoleConfigToggle(__instance, item6, TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetSecondaryRoleString(item6)));
			}
			GameConfig.SpyPercentageDropdown = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_ROLE_CONFIG_SPY_AMOUNT", GameConfig.ConfigTypeEnum.RolesDetails, Color.white, 50, 150, 10);
			GameConfig.FillSpyPercentageDropdown(GameConfig.SpyPercentageDropdown);
			GameConfig.ScientistResearchSpeedDropdown = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_ROLE_CONFIG_SCIENTIST_SPEED", GameConfig.ConfigTypeEnum.RolesDetails, Color.white, 50, 150, 10);
			GameConfig.FillScientistResearchSpeedDropdown(GameConfig.ScientistResearchSpeedDropdown);
			GameConfig.MercenaryPercentageDropdown = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_ROLE_CONFIG_MERCENARY_AMOUNT", GameConfig.ConfigTypeEnum.RolesDetails, Color.white, 50, 150, 10);
			GameConfig.FillMercenaryPercentageDropdown(GameConfig.MercenaryPercentageDropdown);
			GameConfig.LoverVillagerHungerSpeedDropdown = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_ROLE_CONFIG_LOVER_VILLAGER_HUNGER", GameConfig.ConfigTypeEnum.RolesDetails, Color.white, 50, 150, 10);
			GameConfig.FillLoverVillagerHungerSpeedDropdown(GameConfig.LoverVillagerHungerSpeedDropdown);
			GameConfig.CultistSpeedDropdown = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_ROLE_CONFIG_CULTIST_SPEED", GameConfig.ConfigTypeEnum.RolesDetails, Color.white, 50, 150, 10);
			GameConfig.FillCultistSpeedPercentageDropdown(GameConfig.CultistSpeedDropdown);
			GameConfig.DeceiverHasFlatulenceChanceToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_ROLE_CONFIG_DECEIVER_FLATULENCE", new List<object>(), GameConfig.ConfigTypeEnum.RolesDetails, Color.white);
			((UnityEvent<bool>)(object)GameConfig.DeceiverHasFlatulenceChanceToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateDeceiverHasFlatulenceChance);
			GameConfig.LoverWolfReplacesVillagerToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_ROLE_CONFIG_LOVER_WOLF", new List<object>(), GameConfig.ConfigTypeEnum.RolesDetails, Color.white);
			((UnityEvent<bool>)(object)GameConfig.LoverWolfReplacesVillagerToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateLoverWolfReplacesVillagerChance);
			GameConfig.TrapsModifiedToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_TRAP_CHANGES", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.TrapsModifiedToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateTrapsModified);
			GameConfig.SmokeBoostedToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_SMOKE_CHANGES", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.SmokeBoostedToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateSmokeBoosted);
			GameConfig.AnonymousVotesToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_ANONYMOUS_VOTES", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.AnonymousVotesToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateAnonymousVotes);
			GameConfig.SabotagesAvailableToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_SABOTAGES_AVAILABLE", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.SabotagesAvailableToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateSabotagesAvailable);
			GameConfig.DropItemsAvailableToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_DROP_ITEMS_AVAILABLE", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.DropItemsAvailableToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateDropItemsAvailable);
			GameConfig.NightFogDropdown = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_NIGHT_FOG", GameConfig.ConfigTypeEnum.Others, Color.white, 0, 100, 10);
			GameConfig.FillNightFogDropdown(GameConfig.NightFogDropdown);
			GameConfig.WolvesCanUseItemsToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_WOLVES_CAN_USE_ITEMS", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.WolvesCanUseItemsToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateWolvesCanUseItems);
			GameConfig.DraftModeToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_DRAFT_MODE", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.DraftModeToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateDraftMode);
			GameConfig.AllowMayorToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_ROLE_CONFIG_ALLOW_MAYOR", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.AllowMayorToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateAllowMayor);
			GameConfig.ShowLastGameSummaryToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_SHOW_LAST_GAME_SUMMARY", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.ShowLastGameSummaryToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateShowLastGameSummary);
			GameConfig.TenacityHubrisToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_ROLE_CONFIG_TENACITY_HUBRIS", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.TenacityHubrisToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateTenacityHubris);
			GameConfig.AdaptMeetingTimerToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_ADAPT_MEETING_TIMER", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.AdaptMeetingTimerToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateAdaptMeetingTimer);
			GameConfig.ForceWolfTransformationToggle = GameConfig.CreateAndAttachConfigToggle(__instance, "NALES_CONFIG_ROLE_CONFIG_FORCE_WOLF_TRANSFORMATION", new List<object>(), GameConfig.ConfigTypeEnum.Others, Color.white);
			((UnityEvent<bool>)(object)GameConfig.ForceWolfTransformationToggle.onValueChanged).AddListener((UnityAction<bool>)GameConfig.UpdateForceWolfTransformation);
			EffectManager value = Traverse.Create(typeof(EffectManager)).Field<EffectManager>("_instance").Value;
			Effect[] value2 = Traverse.Create((object)value).Field<Effect[]>("effects").Value;
			foreach (Effect item7 in value2.OrderBy((Effect o) => o.GetEffectType()))
			{
				Color textColor = Color.white;
				EffectType effectType = item7.GetEffectType();
				EffectType val2 = effectType;
				switch ((int)val2)
				{
				case 0:
					textColor = Color.green;
					break;
				case 1:
					textColor = Color.white;
					break;
				case 2:
					textColor = Color.red;
					break;
				}
				Toggle val3 = GameConfig.CreateAndAttachConfigToggle(__instance, item7.GetTranslateKey(), new List<object>(), GameConfig.ConfigTypeEnum.Potions, textColor);
				GameConfig.PotionsConfig[item7.GetTranslateKey()] = val3;
				GameConfig.FillPotionToggle(val3, item7);
			}
			Transform parent2 = ((TMP_Text)Object.FindObjectsOfType<TextMeshProUGUI>(true).First((TextMeshProUGUI o) => ((Object)((Component)((TMP_Text)o).transform.parent).gameObject).name == "GameSettingsTitle")).transform.parent;
			Transform val4 = Object.Instantiate<Transform>(parent, parent.parent);
			((TMP_Text)((Component)val4).GetComponentInChildren<TextMeshProUGUI>()).fontSize = 24f;
			((LocalizedReference)((Component)val4).GetComponentInChildren<LocalizeStringEvent>().StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_GADGETS_LIST"));
			GameConfig.GadgetsInfoObject = ((Component)val4).gameObject;
			Item[] value3 = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
			Item[] array = value3;
			foreach (Item item in array)
			{
				string text = ItemUtility.ItemToTranslateKey(item);
				Toggle val5 = GameConfig.CreateAndAttachConfigToggle(__instance, text, new List<object>(), GameConfig.ConfigTypeEnum.Gadgets, Color.white);
				GameConfig.GadgetsConfig[text] = val5;
				GameConfig.FillGadgetToggle(val5, text);
			}
			Transform parent3 = ((TMP_Text)Object.FindObjectsOfType<TextMeshProUGUI>(true).First((TextMeshProUGUI o) => ((Object)((Component)((TMP_Text)o).transform.parent).gameObject).name == "GameSettingsTitle")).transform.parent;
			Transform val6 = Object.Instantiate<Transform>(parent, parent.parent);
			((TMP_Text)((Component)val6).GetComponentInChildren<TextMeshProUGUI>()).fontSize = 24f;
			((LocalizedReference)((Component)val6).GetComponentInChildren<LocalizeStringEvent>().StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_ACCESSORIES_LIST"));
			GameConfig.AccessoriesInfoObject = ((Component)val6).gameObject;
			foreach (Accessory spawnableAccessory in GameManagerCustom.SpawnableAccessories)
			{
				string text2 = ItemUtility.ItemToTranslateKey((Item)(object)spawnableAccessory);
				Toggle val7 = GameConfig.CreateAndAttachConfigToggle(__instance, text2, new List<object>(), GameConfig.ConfigTypeEnum.Gadgets, Color.white);
				GameConfig.AccessoriesConfig[text2] = val7;
				GameConfig.FillAccessoryToggle(val7, text2);
			}
			GameConfig.EventChanceConfig = GameConfig.CreateAndAttachConfigDropdownWithPercentages(__instance, "NALES_CONFIG_EVENTS_CHANCE", GameConfig.ConfigTypeEnum.Events, Color.green, 0, 100, 10);
			GameConfig.FillEventChanceDropdown();
			List<EventsManager.EventType> list6 = (from EventsManager.EventType o in Enum.GetValues(typeof(EventsManager.EventType))
				where o != EventsManager.EventType.None && !EventsManager.IsEventDisabled(o)
				select o).ToList();
			foreach (EventsManager.EventType item8 in list6)
			{
				GameConfig.CreateAndAttachEventConfigToggle(__instance, item8);
			}
			Button val8 = (Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[]).First((Button o) => ((Object)((Component)o).gameObject).name == "ResetButton");
			((Component)val8).GetComponent<RectTransform>().sizeDelta = new Vector2(180f, 70f);
			GameConfig.GameSettingsObject = ((Component)((Component)val8).transform.parent.parent.parent.parent).gameObject;
			Transform transform = ((Component)val8).transform;
			Transform parent4 = ((Component)val8).transform.parent;
			((HorizontalOrVerticalLayoutGroup)((Component)parent4).GetComponent<HorizontalLayoutGroup>()).spacing = 8f;
			Transform val9 = Object.Instantiate<Transform>(transform, parent4);
			LocalizeStringEvent componentInChildren = ((Component)val9).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_BASE_GAME"));
			ColorBlock colors = ((Selectable)((Component)val9).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val9).GetComponent<Button>());
			Button val10 = ((Component)val9).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick = val10.onClick;
			object obj = _003C_003EO._003C14_003E__OnClickConfigBase;
			if (obj == null)
			{
				UnityAction val11 = GameConfig.OnClickConfigBase;
				_003C_003EO._003C14_003E__OnClickConfigBase = val11;
				obj = (object)val11;
			}
			((UnityEvent)onClick).AddListener((UnityAction)obj);
			Transform val12 = Object.Instantiate<Transform>(transform, parent4);
			componentInChildren = ((Component)val12).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_ROLES_VILLAGERS"));
			colors = ((Selectable)((Component)val12).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val12).GetComponent<Button>());
			val10 = ((Component)val12).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick2 = ((Component)val12).GetComponent<Button>().onClick;
			object obj2 = _003C_003EO._003C15_003E__OnClickConfigRolesVillagers;
			if (obj2 == null)
			{
				UnityAction val13 = GameConfig.OnClickConfigRolesVillagers;
				_003C_003EO._003C15_003E__OnClickConfigRolesVillagers = val13;
				obj2 = (object)val13;
			}
			((UnityEvent)onClick2).AddListener((UnityAction)obj2);
			Transform val14 = Object.Instantiate<Transform>(transform, parent4);
			componentInChildren = ((Component)val14).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_ROLES_ENEMIES"));
			colors = ((Selectable)((Component)val14).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val14).GetComponent<Button>());
			val10 = ((Component)val14).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick3 = ((Component)val14).GetComponent<Button>().onClick;
			object obj3 = _003C_003EO._003C16_003E__OnClickConfigRolesEnemies;
			if (obj3 == null)
			{
				UnityAction val15 = GameConfig.OnClickConfigRolesEnemies;
				_003C_003EO._003C16_003E__OnClickConfigRolesEnemies = val15;
				obj3 = (object)val15;
			}
			((UnityEvent)onClick3).AddListener((UnityAction)obj3);
			Transform val16 = Object.Instantiate<Transform>(transform, parent4);
			componentInChildren = ((Component)val16).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_ROLES_SECONDARY"));
			colors = ((Selectable)((Component)val16).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val16).GetComponent<Button>());
			val10 = ((Component)val16).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick4 = ((Component)val16).GetComponent<Button>().onClick;
			object obj4 = _003C_003EO._003C17_003E__OnClickConfigRolesSecondary;
			if (obj4 == null)
			{
				UnityAction val17 = GameConfig.OnClickConfigRolesSecondary;
				_003C_003EO._003C17_003E__OnClickConfigRolesSecondary = val17;
				obj4 = (object)val17;
			}
			((UnityEvent)onClick4).AddListener((UnityAction)obj4);
			Transform val18 = Object.Instantiate<Transform>(transform, parent4);
			componentInChildren = ((Component)val18).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_ROLES_DETAILS"));
			colors = ((Selectable)((Component)val18).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val18).GetComponent<Button>());
			val10 = ((Component)val18).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick5 = ((Component)val18).GetComponent<Button>().onClick;
			object obj5 = _003C_003EO._003C18_003E__OnClickConfigRolesDetails;
			if (obj5 == null)
			{
				UnityAction val19 = GameConfig.OnClickConfigRolesDetails;
				_003C_003EO._003C18_003E__OnClickConfigRolesDetails = val19;
				obj5 = (object)val19;
			}
			((UnityEvent)onClick5).AddListener((UnityAction)obj5);
			Transform val20 = Object.Instantiate<Transform>(transform, parent4);
			componentInChildren = ((Component)val20).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_OTHERS"));
			colors = ((Selectable)((Component)val20).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val20).GetComponent<Button>());
			val10 = ((Component)val20).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick6 = ((Component)val20).GetComponent<Button>().onClick;
			object obj6 = _003C_003EO._003C19_003E__OnClickConfigOthers;
			if (obj6 == null)
			{
				UnityAction val21 = GameConfig.OnClickConfigOthers;
				_003C_003EO._003C19_003E__OnClickConfigOthers = val21;
				obj6 = (object)val21;
			}
			((UnityEvent)onClick6).AddListener((UnityAction)obj6);
			Transform val22 = Object.Instantiate<Transform>(transform, parent4);
			componentInChildren = ((Component)val22).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_POTIONS"));
			colors = ((Selectable)((Component)val22).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val22).GetComponent<Button>());
			val10 = ((Component)val22).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick7 = ((Component)val22).GetComponent<Button>().onClick;
			object obj7 = _003C_003EO._003C20_003E__OnClickConfigPotions;
			if (obj7 == null)
			{
				UnityAction val23 = GameConfig.OnClickConfigPotions;
				_003C_003EO._003C20_003E__OnClickConfigPotions = val23;
				obj7 = (object)val23;
			}
			((UnityEvent)onClick7).AddListener((UnityAction)obj7);
			Transform val24 = Object.Instantiate<Transform>(transform, parent4);
			componentInChildren = ((Component)val24).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_GADGETS"));
			colors = ((Selectable)((Component)val24).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val24).GetComponent<Button>());
			val10 = ((Component)val24).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick8 = ((Component)val24).GetComponent<Button>().onClick;
			object obj8 = _003C_003EO._003C21_003E__OnClickConfigGadgets;
			if (obj8 == null)
			{
				UnityAction val25 = GameConfig.OnClickConfigGadgets;
				_003C_003EO._003C21_003E__OnClickConfigGadgets = val25;
				obj8 = (object)val25;
			}
			((UnityEvent)onClick8).AddListener((UnityAction)obj8);
			Transform val26 = Object.Instantiate<Transform>(transform, parent4);
			componentInChildren = ((Component)val26).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_CONFIG_BUTTON_EVENTS"));
			colors = ((Selectable)((Component)val26).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val26).GetComponent<Button>());
			val10 = ((Component)val26).gameObject.AddComponent<Button>();
			((Selectable)val10).colors = colors;
			ButtonClickedEvent onClick9 = ((Component)val26).GetComponent<Button>().onClick;
			object obj9 = _003C_003EO._003C22_003E__OnClickConfigEvents;
			if (obj9 == null)
			{
				UnityAction val27 = GameConfig.OnClickConfigEvents;
				_003C_003EO._003C22_003E__OnClickConfigEvents = val27;
				obj9 = (object)val27;
			}
			((UnityEvent)onClick9).AddListener((UnityAction)obj9);
			Object.Destroy((Object)(object)((Component)((Component)Traverse.Create((object)__instance).Field<TMP_Dropdown>("huntersCountDropdown").Value).transform.parent.parent.parent).gameObject);
			Object.Destroy((Object)(object)((Component)((Component)Traverse.Create((object)__instance).Field<TMP_Dropdown>("alchemistsCountDropdown").Value).transform.parent.parent.parent).gameObject);
			GameConfig.OnClickConfigBase();
			Traverse.Create((object)__instance).Field<TMP_Dropdown>("farmDropdown").Value.AddOptions(new List<string> { "2200", "2400", "2600", "2800", "3000", "3200", "3400", "3600", "3800", "4000" });
			Traverse.Create((object)__instance).Field<TMP_Dropdown>("wolvesCountDropdown").Value.AddOptions(new List<string> { "3" });
			Traverse.Create((object)__instance).Method("FillFarmDropdown", Array.Empty<object>()).GetValue();
			Traverse.Create((object)__instance).Method("FillWolvesCountDropdown", Array.Empty<object>()).GetValue();
			((Graphic)((Component)transform).gameObject.GetComponent<Image>()).color = new Color(1f, 0.25f, 0.25f, 1f);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameConfigAddNewSettingsPatch error: " + ex));
			StackTrace stackTrace = new StackTrace();
			Plugin.Logger.LogError((object)("StackTrace: " + stackTrace));
		}
	}
}
