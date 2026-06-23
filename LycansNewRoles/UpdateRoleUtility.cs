using System.Collections.Generic;
using System.Linq;
using Fusion;
using LycansNewRoles.PowerObjects;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

public static class UpdateRoleUtility
{
	public static Color GetPrimaryRoleColor(PlayerRole playerRole, PlayerCustom.PlayerNewPrimaryRole newPrimaryRole, PlayerCustom.PlayerPrimaryRolePower primaryRolePower)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected I4, but got Unknown
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (newPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None)
		{
			return PlayerCustom.GetNewPrimaryRoleColor(newPrimaryRole);
		}
		Color result = Color.white;
		switch ((int)playerRole)
		{
		case 0:
			result = (((uint)(primaryRolePower - 29) > 1u) ? GameUI.VillagerColor : PlayerCustom.GetPrimaryRolePowerColor(primaryRolePower));
			break;
		case 1:
			result = GameUI.WolfColor;
			break;
		case 2:
			result = GameUI.HunterColor;
			break;
		case 3:
			result = GameUI.AlchemistColor;
			break;
		}
		return result;
	}

	public static string ListWolvesAsString(List<PlayerRef> playersToExclude)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom specificPrimaryRolePower = PlayerCustomRegistry.GetSpecificPrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Necromancer);
		if ((Object)(object)specificPrimaryRolePower != (Object)null && specificPrimaryRolePower.PrimaryRoleTargetRef != PlayerRef.None)
		{
			playersToExclude.Add(specificPrimaryRolePower.PrimaryRoleTargetRef);
		}
		List<PlayerController> players = (from o in PlayerCustomRegistry
			where ((int)o.PlayerController.Role == 1 || o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Mole) && !playersToExclude.Contains(o.Ref)
			select o.PlayerController).ToList();
		return ListPlayersAsString(players);
	}

	public static string ListPlayersAsString(List<PlayerController> players)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		string text = string.Empty;
		foreach (PlayerController player in players)
		{
			if (text != string.Empty)
			{
				text += " / ";
			}
			string text2 = text;
			NetworkString<_32> username = player.PlayerData.Username;
			text = text2 + ((object)username/*cast due to constrained. prefix*/).ToString();
		}
		return text;
	}

	public static string GetNewPrimaryRoleKey(PlayerController playerController, PlayerCustom playerCustom)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Invalid comparison between Unknown and I4
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None)
		{
			return "NALES_ROLE_" + PlayerCustom.GetNewPrimaryRoleString(playerCustom);
		}
		if (PlayerCustom.IsPrimaryRolePowerForEliteVillagers(playerCustom.PrimaryRolePower))
		{
			return "NALES_DRAFT_MAIN_ROLE_NAME_ELITE";
		}
		PlayerRole role = playerController.Role;
		PlayerRole val = role;
		if ((int)val != 0)
		{
			if ((int)val == 1)
			{
				return NetworkBool.op_Implicit(playerCustom.IsWolfPup) ? "NALES_DRAFT_MAIN_ROLE_NAME_PUP" : "NALES_DRAFT_MAIN_ROLE_NAME_WOLF";
			}
			return "";
		}
		return "NALES_DRAFT_MAIN_ROLE_NAME_VILLAGER";
	}

	public static string GetPrimaryRolePowerKey(PlayerCustom.PlayerPrimaryRolePower primaryRolePower)
	{
		if (primaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None)
		{
			return "NALES_ROLE_" + PlayerCustom.GetPrimaryRolePowerString(primaryRolePower);
		}
		return "";
	}

	public static string GetPrimaryRoleDetails(PlayerCustom playerCustom, bool forSpectator)
	{
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1099: Unknown result type (might be due to invalid IL or missing references)
		//IL_109e: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13de: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f61: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1407: Unknown result type (might be due to invalid IL or missing references)
		//IL_141a: Unknown result type (might be due to invalid IL or missing references)
		//IL_142a: Unknown result type (might be due to invalid IL or missing references)
		//IL_146f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d51: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Invalid comparison between Unknown and I4
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_118b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1190: Unknown result type (might be due to invalid IL or missing references)
		//IL_111c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1121: Unknown result type (might be due to invalid IL or missing references)
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_1245: Unknown result type (might be due to invalid IL or missing references)
		//IL_124a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1204: Unknown result type (might be due to invalid IL or missing references)
		//IL_1209: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		TickTimer primaryRolePowerCooldownTimer;
		switch (playerCustom.NewPrimaryRole)
		{
		case PlayerCustom.PlayerNewPrimaryRole.VillageIdiot:
		{
			string newValue = "";
			switch (playerCustom.SoloRoleObjectiveTarget)
			{
			case 0:
				newValue = TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_WARLOCK");
				break;
			case 1:
				newValue = TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_BOMBER");
				break;
			case 2:
				newValue = TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_SABOTEUR");
				break;
			}
			if (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
			{
				string text6 = "";
				text6 = ((playerCustom.PrimaryRolePowerCurrentMaterials < 3000) ? "<color=green>" : ((playerCustom.PrimaryRolePowerCurrentMaterials >= 7000) ? "<color=red>" : "<color=white>"));
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_NIGHT_AVAILABLE").Replace("{0}", newValue).Replace("{1}", text6 + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / 10000)) + "</color>");
				}
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
				{
					string text7 = TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_NIGHT_AVAILABLE_IN").Replace("{0}", newValue);
					primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
					return text7.Replace("{1}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString()).Replace("{2}", text6 + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / 10000)) + "</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_NIGHT_UNAVAILABLE").Replace("{0}", newValue).Replace("{1}", text6 + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / 10000)) + "</color>");
			}
			if (playerCustom.PrimaryRolePowerRemainingUses > 0)
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_DAY_AVAILABLE").Replace("{0}", newValue);
			}
			primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
			if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
			{
				string text8 = TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_DAY_AVAILABLE_IN").Replace("{0}", newValue);
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				return text8.Replace("{1}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
			}
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_VILLAGE_IDIOT_DAY_UNAVAILABLE").Replace("{0}", newValue);
		}
		case PlayerCustom.PlayerNewPrimaryRole.Spy:
			if (NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_SPY_DETAILS_IN_PROGRESS").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.SoloRoleObjectiveCount * 100 / BalancingValues.SpyGoal(PlayerRegistry.Count))).ToString());
			}
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_SPY_DETAILS_TOO_FAR").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.SoloRoleObjectiveCount * 100 / BalancingValues.SpyGoal(PlayerRegistry.Count))).ToString());
		case PlayerCustom.PlayerNewPrimaryRole.Scientist:
			if (playerCustom.PrimaryRolePowerRemainingUses > 0)
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_SCIENTIST_AVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.SoloRoleObjectiveCount * 100 / BalancingValues.ScientistGoal(PlayerRegistry.Count))).ToString());
			}
			primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
			if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
			{
				string text13 = TranslationManager.Instance.GetTranslation("NALES_ROLE_SCIENTIST_AVAILABLE_IN").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.SoloRoleObjectiveCount * 100 / BalancingValues.ScientistGoal(PlayerRegistry.Count))).ToString());
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				return text13.Replace("{1}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
			}
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_SCIENTIST_UNAVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.SoloRoleObjectiveCount * 100 / BalancingValues.ScientistGoal(PlayerRegistry.Count))).ToString());
		case PlayerCustom.PlayerNewPrimaryRole.Lover:
		{
			if (forSpectator)
			{
				return null;
			}
			PlayerCustom playerCustom2 = playerCustom.FindLoverPartner();
			if ((Object)(object)playerCustom2 == (Object)null)
			{
				return null;
			}
			PlayerController playerController = playerCustom2.PlayerController;
			string newValue2 = ((object)playerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
			int value = Mathf.RoundToInt(playerController.Hunger * 100f / (float)GameManager.Instance.MaxHunger);
			ShowRoleDescriptionPatch.LoverPartnerCurrentHealthPercentageToShow = value;
			if ((int)playerCustom.PlayerController.Role == 1)
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_LOVER_WOLF_DETAILS").Replace("{0}", newValue2).Replace("{1}", value.ToString());
			}
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_LOVER_VILLAGER_DETAILS").Replace("{0}", newValue2).Replace("{1}", value.ToString());
		}
		case PlayerCustom.PlayerNewPrimaryRole.Beast:
			if (playerCustom.PrimaryRolePowerRemainingUses > 0)
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_BEAST_AVAILABLE");
			}
			primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
			if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
			{
				string translation3 = TranslationManager.Instance.GetTranslation("NALES_ROLE_BEAST_AVAILABLE_IN");
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				return translation3.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
			}
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_BEAST_UNAVAILABLE");
		case PlayerCustom.PlayerNewPrimaryRole.Mercenary:
		{
			string text10 = ((playerCustom.SoloRoleObjectiveCount >= playerCustom.SoloRoleObjectiveTarget) ? "<color=green>" : "<color=red>");
			string newValue3 = text10 + playerCustom.SoloRoleObjectiveCount + "</color>";
			if (playerCustom.PrimaryRoleTargetRef == PlayerRef.None)
			{
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
				{
					string text11 = TranslationManager.Instance.GetTranslation("NALES_ROLE_MERCENARY_COOLDOWN").Replace("{0}", newValue3).Replace("{1}", playerCustom.SoloRoleObjectiveTarget.ToString());
					primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
					return text11.Replace("{2}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_MERCENARY_NO_TARGET").Replace("{0}", newValue3).Replace("{1}", playerCustom.SoloRoleObjectiveTarget.ToString());
			}
			primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
			if (!((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_MERCENARY_MARKED").Replace("{0}", newValue3).Replace("{1}", playerCustom.SoloRoleObjectiveTarget.ToString());
			}
			string text12 = TranslationManager.Instance.GetTranslation("NALES_ROLE_MERCENARY_NOT_MARKED").Replace("{0}", newValue3).Replace("{1}", playerCustom.SoloRoleObjectiveTarget.ToString());
			primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
			return text12.Replace("{2}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
		}
		case PlayerCustom.PlayerNewPrimaryRole.Voodoo:
			if (playerCustom.PrimaryRolePowerRemainingUses > 0)
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_VOODOO_AVAILABLE").Replace("{0}", PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie).ToString());
			}
			primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
			if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
			{
				string text5 = TranslationManager.Instance.GetTranslation("NALES_ROLE_VOODOO_AVAILABLE_IN").Replace("{0}", PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie).ToString());
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				return text5.Replace("{1}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
			}
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_VOODOO_UNAVAILABLE").Replace("{0}", PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie).ToString());
		case PlayerCustom.PlayerNewPrimaryRole.Zombie:
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_ZOMBIE_DETAILS").Replace("{0}", ((object)PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Voodoo).PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString());
		case PlayerCustom.PlayerNewPrimaryRole.Kidnapper:
			if (playerCustom.PrimaryRolePowerRemainingUses > 0)
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_KIDNAPPER_AVAILABLE").Replace("{0}", playerCustom.SoloRoleObjectiveCount.ToString()).Replace("{1}", BalancingValues.KidnapperTargetAmount(PlayerRegistry.Count).ToString());
			}
			primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
			if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
			{
				string text9 = TranslationManager.Instance.GetTranslation("NALES_ROLE_KIDNAPPER_AVAILABLE_IN").Replace("{0}", playerCustom.SoloRoleObjectiveCount.ToString()).Replace("{1}", BalancingValues.KidnapperTargetAmount(PlayerRegistry.Count).ToString());
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				return text9.Replace("{2}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
			}
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_KIDNAPPER_UNAVAILABLE").Replace("{0}", playerCustom.SoloRoleObjectiveCount.ToString()).Replace("{1}", BalancingValues.KidnapperTargetAmount(PlayerRegistry.Count).ToString());
		case PlayerCustom.PlayerNewPrimaryRole.Cultist:
			if (NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
			{
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_CULTIST_HUNT").Replace("{0}", CultistManager.Instance.CultistCaptures.ToString()).Replace("{1}", CultistManager.Instance.CultistCapturesObjective.ToString());
			}
			primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
			if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
			{
				string text4 = TranslationManager.Instance.GetTranslation("NALES_ROLE_CULTIST_COOLDOWN").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.SoloRoleObjectiveCount * 100) / 10000f).ToString()).Replace("{1}", CultistSkull.AllSkulls.Count.ToString());
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				return text4.Replace("{2}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
			}
			return TranslationManager.Instance.GetTranslation("NALES_ROLE_CULTIST_AVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.SoloRoleObjectiveCount * 100) / 10000f).ToString()).Replace("{1}", CultistSkull.AllSkulls.Count.ToString());
		default:
			switch (playerCustom.PrimaryRolePower)
			{
			case PlayerCustom.PlayerPrimaryRolePower.Necromancer:
			{
				int num = Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials));
				if (!NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_NECROMANCER_NO_TARGET").Replace("{0}", Mathf.FloorToInt((float)num).ToString());
				}
				string text = ((playerCustom.PrimaryRolePowerCurrentMaterials >= 10000) ? "<color=green>" : "<color=red>");
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_NECROMANCER_TARGET").Replace("{0}", text + Mathf.FloorToInt((float)num) + "</color>");
			}
			case PlayerCustom.PlayerPrimaryRolePower.Deceiver:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_DECEIVER_AVAILABLE");
				}
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
				{
					string translation2 = TranslationManager.Instance.GetTranslation("NALES_ROLE_DECEIVER_AVAILABLE_IN");
					primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
					return translation2.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_DECEIVER_UNAVAILABLE");
			case PlayerCustom.PlayerPrimaryRolePower.Tracker:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_TRACKER_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_TRACKER_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Warlock:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_WARLOCK_AVAILABLE");
				}
				primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
				if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
				{
					string translation = TranslationManager.Instance.GetTranslation("NALES_ROLE_WARLOCK_AVAILABLE_IN");
					primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
					return translation.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_WARLOCK_UNAVAILABLE");
			case PlayerCustom.PlayerPrimaryRolePower.Saboteur:
				if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_SABOTEUR_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_SABOTEUR_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Possessor:
			{
				PlayerRef primaryRoleTargetRef = playerCustom.PrimaryRoleTargetRef;
				if (((PlayerRef)(ref primaryRoleTargetRef)).IsNone)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_POSSESSOR_NO_TARGET");
				}
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef);
				if (playerCustom.PrimaryRolePowerCurrentMaterials < playerCustom.PowerMaterialsInfo.RequiredMaterials)
				{
					if (NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
					{
						return TranslationManager.Instance.GetTranslation("NALES_ROLE_POSSESSOR_TARGET_VISIBLE").Replace("{0}", ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString()).Replace("{1}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
					}
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_POSSESSOR_TARGET_NOT_VISIBLE").Replace("{0}", ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString());
				}
				if (NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool) || (NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf) && NetworkBool.op_Implicit(player.Possessed)))
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_POSSESSOR_POSSESSING").Replace("{0}", ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_POSSESSOR_AVAILABLE").Replace("{0}", ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString());
			}
			case PlayerCustom.PlayerPrimaryRolePower.Bomber:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_BOMBER_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_BOMBER_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Ritualist:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_RITUALIST_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_RITUALIST_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Predator:
			{
				if (playerCustom.PrimaryRoleTargetRef == PlayerRef.None)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_PREDATOR_NO_TARGET");
				}
				PlayerController player2 = PlayerRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef);
				int num2 = Mathf.RoundToInt(Vector3.Distance(((Component)player2).transform.position, ((Component)playerCustom.PlayerController).transform.position));
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_PREDATOR_WITH_TARGET").Replace("{0}", num2.ToString());
			}
			case PlayerCustom.PlayerPrimaryRolePower.Sneak:
				if (LycansUtility.WolvesCanTransform)
				{
					if (NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
					{
						return TranslationManager.Instance.GetTranslation("NALES_ROLE_SNEAK_SILENT").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)5));
					}
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_SNEAK_NOISY").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)5));
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_SNEAK_DAY").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)5));
			case PlayerCustom.PlayerPrimaryRolePower.Host:
				if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_HOST_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_HOST_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Peasant:
				if ((float)playerCustom.PrimaryRolePowerCurrentMaterials >= 2500f)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_PEASANT_AVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_PEASANT_UNAVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
			case PlayerCustom.PlayerPrimaryRolePower.Exorcist:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_EXORCIST_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_EXORCIST_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Avenger:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_AVENGER_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_AVENGER_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Investigator:
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_INVESTIGATOR_DETAILS").Replace("{0}", playerCustom.PrimaryRolePowerCurrentMaterials.ToString());
			case PlayerCustom.PlayerPrimaryRolePower.Survivalist:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_SURVIVALIST_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_SURVIVALIST_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Priest:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_PRIEST_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_PRIEST_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Scout:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_SCOUT_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_SCOUT_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Magician:
			{
				string text2 = ((playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials) ? "<color=green>" : "<color=red>");
				int count = MagicianBeacon.AssociatedBeacons.Count;
				string text3 = ((count > 0) ? "<color=green>" : "<color=red>");
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_MAGICIAN_DETAILS").Replace("{0}", text2 + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>").Replace("{1}", text3 + count + "</color>");
			}
			case PlayerCustom.PlayerPrimaryRolePower.Mystic:
				if ((float)playerCustom.PrimaryRolePowerCurrentMaterials >= 10000f)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_MYSTIC_AVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_MYSTIC_UNAVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
			case PlayerCustom.PlayerPrimaryRolePower.Shadow:
				if ((float)playerCustom.PrimaryRolePowerCurrentMaterials >= 10000f)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_SHADOW_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_SHADOW_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Hermit:
				if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_HERMIT_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_HERMIT_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>");
			case PlayerCustom.PlayerPrimaryRolePower.Runemaster:
				if (playerCustom.PrimaryRolePowerRemainingUses > 0)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_RUNEMASTER_DETAILS").Replace("{0}", "<color=green>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>").Replace("{1}", (playerCustom.Ref == PlayerController.Local.Ref) ? RunemasterRune.AssociatedRunes.Count.ToString() : "?")
						.Replace("{2}", 8.ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_RUNEMASTER_DETAILS").Replace("{0}", "<color=red>" + Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)) + "%</color>").Replace("{1}", (playerCustom.Ref == PlayerController.Local.Ref) ? RunemasterRune.AssociatedRunes.Count.ToString() : "?")
					.Replace("{2}", 8.ToString());
			case PlayerCustom.PlayerPrimaryRolePower.Avatar:
				if (!NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_AVATAR_AVAILABLE");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_AVATAR_UNAVAILABLE");
			case PlayerCustom.PlayerPrimaryRolePower.Hunter:
				if (NetworkBool.op_Implicit(playerCustom.PlayerController.IsGunLoaded))
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_HUNTER_WITH_BULLET");
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_HUNTER_WITHOUT_BULLET");
			case PlayerCustom.PlayerPrimaryRolePower.Alchemist:
				if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_ALCHEMIST_AVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_ALCHEMIST_UNAVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
			case PlayerCustom.PlayerPrimaryRolePower.Spotter:
				if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_SPOTTER_AVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_SPOTTER_UNAVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
			case PlayerCustom.PlayerPrimaryRolePower.Purifier:
				if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
				{
					return TranslationManager.Instance.GetTranslation("NALES_ROLE_PURIFIER_AVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
				}
				return TranslationManager.Instance.GetTranslation("NALES_ROLE_PURIFIER_UNAVAILABLE").Replace("{0}", Mathf.FloorToInt((float)(playerCustom.PrimaryRolePowerCurrentMaterials * 100 / playerCustom.PowerMaterialsInfo.RequiredMaterials)).ToString());
			default:
				return null;
			}
		}
	}

	public static string GetSecondaryRoleKey(PlayerCustom.PlayerSecondaryRole secondaryRole)
	{
		return "NALES_ROLE_" + PlayerCustom.GetSecondaryRoleString(secondaryRole);
	}

	public static RoleDescription GetFullRoleDescription(PlayerController player, PlayerCustom playerCustom, bool withAlly)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Invalid comparison between Unknown and I4
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom.PlayerNewPrimaryRole newPrimaryRole = playerCustom.NewPrimaryRole;
		PlayerCustom.PlayerPrimaryRolePower primaryRolePower = playerCustom.PrimaryRolePower;
		PlayerCustom.PlayerSecondaryRole secondaryRole = playerCustom.SecondaryRole;
		string text = null;
		Color primaryRoleColor = GetPrimaryRoleColor(player.Role, newPrimaryRole, primaryRolePower);
		List<object> list = new List<object>();
		if (newPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None)
		{
			text = "NALES_ROLE_BASE_DESCRIPTION_" + PlayerCustom.GetNewPrimaryRoleString(playerCustom);
		}
		else if (PlayerCustom.IsPrimaryRolePowerForEliteVillagers(playerCustom.PrimaryRolePower))
		{
			text = "NALES_ROLE_BASE_DESCRIPTION_ELITE";
		}
		else
		{
			PlayerRole role = player.Role;
			PlayerRole val = role;
			text = (((int)val == 0) ? "NALES_ROLE_BASE_DESCRIPTION_VILLAGER" : (((int)val != 1) ? "" : (NetworkBool.op_Implicit(playerCustom.IsWolfPup) ? "NALES_ROLE_BASE_DESCRIPTION_PUP" : "NALES_ROLE_BASE_DESCRIPTION_WOLF")));
		}
		if (primaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None)
		{
			if (PlayerCustom.IsPrimaryRolePowerForEliteVillagers(primaryRolePower))
			{
				list.Add(TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetPrimaryRolePowerString(primaryRolePower)));
			}
			else
			{
				list.Add(" " + TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetPrimaryRolePowerString(primaryRolePower)));
			}
		}
		else
		{
			list.Add("");
		}
		if (secondaryRole != PlayerCustom.PlayerSecondaryRole.None)
		{
			list.Add(" - " + TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetSecondaryRoleString(secondaryRole)));
		}
		else
		{
			list.Add("");
		}
		if (withAlly && newPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover && (newPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor || PlayerCustomRegistry.CountWhere((PlayerCustom o) => (int)o.PlayerController.Role == 1 && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover) > 1))
		{
			PlayerCustom playerCustom2 = PlayerCustomRegistry.AllPlayers.FirstOrDefault((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && (int)o.PlayerController.Role == 1);
			PlayerRef item = (((Object)(object)playerCustom2 != (Object)null) ? playerCustom2.Ref : PlayerRef.None);
			string text2 = ListWolvesAsString(new List<PlayerRef> { player.Ref, item });
			string text3 = "";
			text3 = ((newPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Traitor) ? TranslationManager.Instance.GetTranslation("NALES_ROLE_WOLF_PARTNER") : TranslationManager.Instance.GetTranslation("NALES_ROLE_TRAITOR_WOLF"));
			list.Add(" (" + text3 + text2 + ")");
		}
		else
		{
			list.Add("");
		}
		return new RoleDescription
		{
			Key = text,
			Arguments = list,
			Color = primaryRoleColor
		};
	}
}
