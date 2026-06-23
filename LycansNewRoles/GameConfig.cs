using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(20)]
public class GameConfig : NetworkBehaviour
{
	public enum ConfigTypeEnum
	{
		Base,
		RolesVillagers,
		RolesEnemies,
		RolesSecondary,
		RolesDetails,
		Others,
		Potions,
		Gadgets,
		Events
	}

	public static Color ColorSoloRole = new Color(1f, 0f, 1f, 1f);

	public static Color ColorWolfPower = new Color(1f, 0f, 0f, 1f);

	public static Color ColorVillagerJob = new Color(0f, 0.5f, 1f, 1f);

	public static Color ColorElite = new Color(0f, 0.25f, 0.8f, 1f);

	public static Color ColorTraitor = new Color(1f, 0.5f, 0f, 1f);

	public static Color ColorWolfPup = new Color(1f, 0.25f, 0f, 1f);

	public static Color ColorSecondaryRole = new Color(1f, 1f, 1f, 1f);

	public static TMP_Dropdown SoloRolesCountConfig;

	public static TMP_Dropdown TraitorsCountConfig;

	public static TMP_Dropdown WolfPupsCountConfig;

	public static TMP_Dropdown WolfPowersCountConfig;

	public static TMP_Dropdown VillagerPowersChanceConfig;

	public static TMP_Dropdown ElitesCountConfig;

	public static TMP_Dropdown AvatarChanceConfig;

	public static TMP_Dropdown MoleChanceConfig;

	public static TMP_Dropdown GuardianAngelsCountConfig;

	public static TMP_Dropdown GhostsCountConfig;

	public static TMP_Dropdown SpectersCountConfig;

	public static TMP_Dropdown SecondaryRolesCountConfig;

	public static TMP_Dropdown EventChanceConfig;

	public static Dictionary<PlayerCustom.PlayerNewPrimaryRole, Toggle> SoloRolesConfig = new Dictionary<PlayerCustom.PlayerNewPrimaryRole, Toggle>();

	public static Dictionary<PlayerCustom.PlayerPrimaryRolePower, Toggle> PrimaryRolePowerConfig = new Dictionary<PlayerCustom.PlayerPrimaryRolePower, Toggle>();

	public static Dictionary<PlayerCustom.PlayerSecondaryRole, Toggle> SecondaryRolesConfig = new Dictionary<PlayerCustom.PlayerSecondaryRole, Toggle>();

	public static Dictionary<EventsManager.EventType, Toggle> EventsConfig = new Dictionary<EventsManager.EventType, Toggle>();

	public static Dictionary<string, Toggle> PotionsConfig = new Dictionary<string, Toggle>();

	public static Dictionary<string, Toggle> GadgetsConfig = new Dictionary<string, Toggle>();

	public static Dictionary<string, Toggle> AccessoriesConfig = new Dictionary<string, Toggle>();

	public int SoloRolesCount;

	public int TraitorsCount;

	public int WolfPupsCount;

	public int WolfPowersCount;

	public int VillagerPowersChance;

	public int ElitesCount;

	public int AvatarChance;

	public int MoleChance;

	public int GuardianAngelsCount;

	public int GhostsCount;

	public int SpectersCount;

	public int SecondaryRolesCount;

	public int EventChance;

	public Dictionary<PlayerCustom.PlayerNewPrimaryRole, bool> SoloRoleActive = new Dictionary<PlayerCustom.PlayerNewPrimaryRole, bool>();

	public Dictionary<PlayerCustom.PlayerPrimaryRolePower, bool> PrimaryRolePowerActive = new Dictionary<PlayerCustom.PlayerPrimaryRolePower, bool>();

	public Dictionary<PlayerCustom.PlayerSecondaryRole, bool> SecondaryRoleActive = new Dictionary<PlayerCustom.PlayerSecondaryRole, bool>();

	public Dictionary<EventsManager.EventType, bool> EventsActive = new Dictionary<EventsManager.EventType, bool>();

	public Dictionary<string, bool> PotionsAvailability = new Dictionary<string, bool>();

	public Dictionary<string, bool> GadgetsAvailability = new Dictionary<string, bool>();

	public Dictionary<string, bool> AccessoriesAvailability = new Dictionary<string, bool>();

	public static TMP_Dropdown SpyPercentageDropdown;

	public static TMP_Dropdown ScientistResearchSpeedDropdown;

	public static TMP_Dropdown LoverVillagerHungerSpeedDropdown;

	public static TMP_Dropdown MercenaryPercentageDropdown;

	public static TMP_Dropdown CultistSpeedDropdown;

	public static Toggle DeceiverHasFlatulenceChanceToggle;

	public static Toggle LoverWolfReplacesVillagerToggle;

	public static Toggle AllowMayorToggle;

	public static Toggle TenacityHubrisToggle;

	public static Toggle TrapsModifiedToggle;

	public static Toggle SmokeBoostedToggle;

	public static Toggle AnonymousVotesToggle;

	public static Toggle SabotagesAvailableToggle;

	public static Toggle DropItemsAvailableToggle;

	public static TMP_Dropdown NightFogDropdown;

	public static Toggle WolvesCanUseItemsToggle;

	public static Toggle DraftModeToggle;

	public static Toggle ShowLastGameSummaryToggle;

	public static Button ButtonConfigBaseGame;

	public static Button ButtonConfigMod;

	public static GameObject GameSettingsObject;

	public static GameObject RoleInfoObject;

	public static GameObject GadgetsInfoObject;

	public static GameObject AccessoriesInfoObject;

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe NetworkBool TrapsModified
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.TrapsModified. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.TrapsModified. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool SmokeBoosted
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.SmokeBoosted. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		private set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.SmokeBoosted. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe NetworkBool AnonymousVotes
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.AnonymousVotes. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[2];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.AnonymousVotes. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 2, value);
		}
	}

	[Networked]
	[NetworkedWeaved(3, 1)]
	public unsafe NetworkBool UnusedVariable
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.AmogusMode. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[3];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.AmogusMode. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 3, value);
		}
	}

	[Networked]
	[NetworkedWeaved(4, 1)]
	public unsafe NetworkBool SabotagesAvailable
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.SabotagesAvailable. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[4];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.SabotagesAvailable. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 4, value);
		}
	}

	[Networked]
	[NetworkedWeaved(5, 1)]
	public unsafe NetworkBool DropItemsAvailable
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.DropItemsAvailable. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[5];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.DropItemsAvailable. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 5, value);
		}
	}

	[Networked]
	[NetworkedWeaved(6, 1)]
	public unsafe int NightFog
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.NightFog. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[6];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.NightFog. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[6] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(7, 1)]
	public unsafe NetworkBool WolvesCanUseItems
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.WolvesCanUseItems. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[7];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.WolvesCanUseItems. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 7, value);
		}
	}

	[Networked]
	[NetworkedWeaved(8, 1)]
	public unsafe NetworkBool DraftMode
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.DraftMode. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[8];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.DraftMode. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 8, value);
		}
	}

	[Networked]
	[NetworkedWeaved(9, 1)]
	public unsafe NetworkBool UnusedConfig
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.VillageIdiotCanTransform. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[9];
		}
		private set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.VillageIdiotCanTransform. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 9, value);
		}
	}

	[Networked]
	[NetworkedWeaved(10, 1)]
	public unsafe int MercenaryPercentage
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.MercenaryPercentage. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[10];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.MercenaryPercentage. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[10] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(11, 1)]
	public unsafe int SpyPercentage
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SpyPercentage. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[11];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SpyPercentage. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[11] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(12, 1)]
	public unsafe int ScientistResearchSpeed
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ScientistResearchSpeed. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[12];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ScientistResearchSpeed. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[12] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(13, 1)]
	public unsafe int LoverVillagerHungerSpeed
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.LoverVillagerHungerSpeed. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[13];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.LoverVillagerHungerSpeed. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[13] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(14, 1)]
	public unsafe NetworkBool DeceiverHasFlatulenceChance
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.DeceiverHasFlatulenceChance. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[14];
		}
		private set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.DeceiverHasFlatulenceChance. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 14, value);
		}
	}

	[Networked]
	[NetworkedWeaved(15, 1)]
	public unsafe NetworkBool LoverWolfReplacesVillager
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.WolfLoverReplacesWolf. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[15];
		}
		private set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.WolfLoverReplacesWolf. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 15, value);
		}
	}

	[Networked]
	[NetworkedWeaved(16, 1)]
	public unsafe NetworkBool AllowMayor
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.AllowMayor. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[16];
		}
		private set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.AllowMayor. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 16, value);
		}
	}

	[Networked]
	[NetworkedWeaved(17, 1)]
	public unsafe NetworkBool ShowLastGameSummary
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.ShowLastGameSummary. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[17];
		}
		private set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.ShowLastGameSummary. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 17, value);
		}
	}

	[Networked]
	[NetworkedWeaved(18, 1)]
	public unsafe NetworkBool TenacityHubris
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.TenacityHubris. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[18];
		}
		private set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.TenacityHubris. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 18, value);
		}
	}

	[Networked]
	[NetworkedWeaved(19, 1)]
	public unsafe int CultistSpeed
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.CultistSpeed. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[19];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameConfig.CultistSpeed. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[19] = value;
		}
	}

	private void Start()
	{
		FillTrapsModifiedToggle();
		FillSmokeBoostedToggle();
		FillAnonymousVotesToggle();
		FillSabotagesAvailableToggle();
		FillDropItemsAvailableToggle();
		FillWolvesCanUseItemsToggle();
		FillDraftModeToggle();
		FillShowLastGameSummaryToggle();
		FillDeceiverHasFlatulenceChanceToggle();
		FillLoverWolfReplacesVillagerToggle();
		FillAllowMayorToggle();
		FillTenacityHubrisToggle();
	}

	public override void Spawned()
	{
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsClient)
		{
			Plugin.NetworkObject = ((Component)this).GetComponent<NetworkObject>();
		}
		try
		{
			SoloRolesCount = int.Parse(SoloRolesCountConfig.options[SoloRolesCountConfig.value].text);
			TraitorsCount = int.Parse(TraitorsCountConfig.options[TraitorsCountConfig.value].text);
			WolfPupsCount = int.Parse(WolfPupsCountConfig.options[WolfPupsCountConfig.value].text);
			WolfPowersCount = int.Parse(WolfPowersCountConfig.options[WolfPowersCountConfig.value].text);
			VillagerPowersChance = int.Parse(VillagerPowersChanceConfig.options[VillagerPowersChanceConfig.value].text.Replace("%", ""));
			ElitesCount = int.Parse(ElitesCountConfig.options[ElitesCountConfig.value].text.Replace("%", ""));
			AvatarChance = int.Parse(AvatarChanceConfig.options[AvatarChanceConfig.value].text.Replace("%", ""));
			MoleChance = int.Parse(MoleChanceConfig.options[MoleChanceConfig.value].text.Replace("%", ""));
			GuardianAngelsCount = int.Parse(GuardianAngelsCountConfig.options[GuardianAngelsCountConfig.value].text.Replace("%", ""));
			GhostsCount = int.Parse(GhostsCountConfig.options[GhostsCountConfig.value].text.Replace("%", ""));
			SpectersCount = int.Parse(SpectersCountConfig.options[SpectersCountConfig.value].text.Replace("%", ""));
			SecondaryRolesCount = int.Parse(SecondaryRolesCountConfig.options[SecondaryRolesCountConfig.value].text);
			EventChance = int.Parse(EventChanceConfig.options[EventChanceConfig.value].text.Replace("%", ""));
			foreach (KeyValuePair<PlayerCustom.PlayerNewPrimaryRole, Toggle> item in SoloRolesConfig)
			{
				SoloRoleActive[item.Key] = item.Value.isOn;
			}
			foreach (KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, Toggle> item2 in PrimaryRolePowerConfig)
			{
				PrimaryRolePowerActive[item2.Key] = item2.Value.isOn;
			}
			foreach (KeyValuePair<PlayerCustom.PlayerSecondaryRole, Toggle> item3 in SecondaryRolesConfig)
			{
				SecondaryRoleActive[item3.Key] = item3.Value.isOn;
			}
			foreach (KeyValuePair<EventsManager.EventType, Toggle> item4 in EventsConfig)
			{
				EventsActive[item4.Key] = item4.Value.isOn;
			}
			NightFog = int.Parse(NightFogDropdown.options[NightFogDropdown.value].text.Replace("%", ""));
			foreach (KeyValuePair<string, Toggle> item5 in PotionsConfig)
			{
				PotionsAvailability[item5.Key] = item5.Value.isOn;
			}
			foreach (KeyValuePair<string, Toggle> item6 in GadgetsConfig)
			{
				GadgetsAvailability[item6.Key] = item6.Value.isOn;
			}
			foreach (KeyValuePair<string, Toggle> item7 in AccessoriesConfig)
			{
				AccessoriesAvailability[item7.Key] = item7.Value.isOn;
			}
			SpyPercentage = int.Parse(SpyPercentageDropdown.options[SpyPercentageDropdown.value].text.Replace("%", ""));
			ScientistResearchSpeed = int.Parse(ScientistResearchSpeedDropdown.options[ScientistResearchSpeedDropdown.value].text.Replace("%", ""));
			MercenaryPercentage = int.Parse(MercenaryPercentageDropdown.options[MercenaryPercentageDropdown.value].text.Replace("%", ""));
			CultistSpeed = int.Parse(CultistSpeedDropdown.options[CultistSpeedDropdown.value].text.Replace("%", ""));
			LoverVillagerHungerSpeed = int.Parse(LoverVillagerHungerSpeedDropdown.options[LoverVillagerHungerSpeedDropdown.value].text.Replace("%", ""));
			DeceiverHasFlatulenceChance = NetworkBool.op_Implicit(DeceiverHasFlatulenceChanceToggle.isOn);
			LoverWolfReplacesVillager = NetworkBool.op_Implicit(LoverWolfReplacesVillagerToggle.isOn);
			AllowMayor = NetworkBool.op_Implicit(AllowMayorToggle.isOn);
			TenacityHubris = NetworkBool.op_Implicit(TenacityHubrisToggle.isOn);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameConfigSetGameSettingsPatch error: " + ex));
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
	}

	public static void CreateAndAttachSoloRoleConfigToggle(GameSettingsUI gameSettingsUI, PlayerCustom.PlayerNewPrimaryRole role, string key)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		string key2 = ((role == PlayerCustom.PlayerNewPrimaryRole.Lover) ? "NALES_CONFIG_ROLE_TYPE_MAIN_DUO" : "NALES_CONFIG_ROLE_TYPE_MAIN_SOLO");
		Toggle toggle = GetToggle(gameSettingsUI, key2, new List<object> { key }, ConfigTypeEnum.RolesEnemies, PlayerCustom.GetNewPrimaryRoleColor(role));
		SoloRolesConfig[role] = toggle;
		FillSoloRoleToggle(toggle, role);
	}

	public static void CreateAndAttachPrimaryRolePowerConfigToggle(GameSettingsUI gameSettingsUI, PlayerCustom.PlayerPrimaryRolePower role, string key)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Toggle val = (PlayerCustom.IsPrimaryRolePowerForEliteVillagers(role) ? GetToggle(gameSettingsUI, "NALES_CONFIG_ROLE_TYPE_MAIN_ELITE", new List<object> { key }, ConfigTypeEnum.RolesVillagers, ColorVillagerJob) : ((!PlayerCustom.IsPrimaryRolePowerForNormalVillagers(role)) ? GetToggle(gameSettingsUI, "NALES_CONFIG_ROLE_TYPE_MAIN_WOLF", new List<object> { key }, ConfigTypeEnum.RolesEnemies, ColorWolfPower) : GetToggle(gameSettingsUI, "NALES_CONFIG_ROLE_TYPE_MAIN_VILLAGER", new List<object> { key }, ConfigTypeEnum.RolesVillagers, ColorVillagerJob)));
		PrimaryRolePowerConfig[role] = val;
		FillPrimaryRolePowerToggle(val, role);
	}

	public static void CreateAndAttachSecondaryRoleConfigToggle(GameSettingsUI gameSettingsUI, PlayerCustom.PlayerSecondaryRole role, string key)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Toggle toggle = GetToggle(gameSettingsUI, "NALES_CONFIG_ROLE_TYPE_SECONDARY", new List<object> { key }, ConfigTypeEnum.RolesSecondary, ColorSecondaryRole);
		SecondaryRolesConfig[role] = toggle;
		FillSecondaryRoleToggle(toggle, role);
	}

	public static void CreateAndAttachEventConfigToggle(GameSettingsUI gameSettingsUI, EventsManager.EventType dailyEvent)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Toggle toggle = GetToggle(gameSettingsUI, "NALES_EVENT_" + dailyEvent.ToString().ToUpper(), new List<object>(), ConfigTypeEnum.Events, Color.white);
		EventsConfig[dailyEvent] = toggle;
		FillEventToggle(toggle, dailyEvent);
	}

	public static Toggle CreateAndAttachConfigToggle(GameSettingsUI gameSettingsUI, string key, List<object> arguments, ConfigTypeEnum type, Color textColor)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		return GetToggle(gameSettingsUI, key, arguments, type, textColor);
	}

	public static TMP_Dropdown CreateAndAttachConfigDropdownWithMax(GameSettingsUI gameSettingsUI, string key, int max, ConfigTypeEnum type, Color textColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		List<string> list = new List<string>();
		for (int i = 0; i <= max; i++)
		{
			list.Add(i.ToString());
		}
		return GetDropdown(gameSettingsUI, key, list, type, textColor);
	}

	public static TMP_Dropdown CreateAndAttachConfigDropdownWithMinAndMax(GameSettingsUI gameSettingsUI, string key, int min, int max, ConfigTypeEnum type, Color textColor)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		List<string> list = new List<string>();
		for (int i = min; i <= max; i++)
		{
			list.Add(i.ToString());
		}
		return GetDropdown(gameSettingsUI, key, list, type, textColor);
	}

	public static TMP_Dropdown CreateAndAttachConfigDropdownWithPercentages(GameSettingsUI gameSettingsUI, string key, ConfigTypeEnum type, Color textColor, int minValue, int maxValue, int interval)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		List<string> list = new List<string>();
		for (int i = minValue; i <= maxValue; i += interval)
		{
			list.Add(i + "%");
		}
		return GetDropdown(gameSettingsUI, key, list, type, textColor);
	}

	public static void FillSoloRolesCountDropdown()
	{
		TMP_Dropdown dropdown = SoloRolesCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_SOLO_ROLES_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_SOLO_ROLES_COUNT", 2);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.SoloRolesCount = int.Parse(dropdown.options[index].text);
			PlayerPrefs.SetInt("GAME_SETTINGS_SOLO_ROLES_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_SOLO_ROLES_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_SOLO_ROLES_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillTraitorsCountDropdown()
	{
		TMP_Dropdown dropdown = TraitorsCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_TRAITORS_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_TRAITORS_COUNT", 0);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.TraitorsCount = int.Parse(dropdown.options[index].text);
			PlayerPrefs.SetInt("GAME_SETTINGS_TRAITORS_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_TRAITORS_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_TRAITORS_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillWolfPupsCountDropdown()
	{
		TMP_Dropdown dropdown = WolfPupsCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_WOLF_PUPS_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_WOLF_PUPS_COUNT", 0);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.WolfPupsCount = int.Parse(dropdown.options[index].text);
			PlayerPrefs.SetInt("GAME_SETTINGS_WOLF_PUPS_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_WOLF_PUPS_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_WOLF_PUPS_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillPrimaryRolePowersCountDropdown()
	{
		TMP_Dropdown dropdown = WolfPowersCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_POWERS_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_POWERS_COUNT", 2);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.WolfPowersCount = int.Parse(dropdown.options[index].text);
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_POWERS_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_POWERS_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_PRIMARY_ROLE_POWERS_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillPrimaryRoleVillagerPowersChanceDropdown()
	{
		TMP_Dropdown dropdown = VillagerPowersChanceConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_VILLAGER_POWERS_CHANCE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_VILLAGER_POWERS_CHANCE", 10);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.VillagerPowersChance = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_VILLAGER_POWERS_CHANCE", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_VILLAGER_POWERS_CHANCE"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_PRIMARY_ROLE_VILLAGER_POWERS_CHANCE");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillElitesCountDropdown()
	{
		TMP_Dropdown dropdown = ElitesCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_ELITES_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_ELITES_COUNT", 2);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.ElitesCount = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_ELITES_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_ELITES_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_PRIMARY_ROLE_ELITES_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillPrimaryRoleAvatarChanceDropdown()
	{
		TMP_Dropdown dropdown = AvatarChanceConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_AVATAR_CHANCE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_AVATAR_CHANCE", 3);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.AvatarChance = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_AVATAR_CHANCE", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_AVATAR_CHANCE"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_PRIMARY_ROLE_AVATAR_CHANCE");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillPrimaryRoleMoleChanceDropdown()
	{
		TMP_Dropdown dropdown = MoleChanceConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_MOLE_CHANCE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_MOLE_CHANCE", 3);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.MoleChance = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_MOLE_CHANCE", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_MOLE_CHANCE"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_PRIMARY_ROLE_MOLE_CHANCE");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillGuardianAngelsCountDropdown()
	{
		TMP_Dropdown dropdown = GuardianAngelsCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_GUARDIAN_ANGELS_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_GUARDIAN_ANGELS_COUNT", 1);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.GuardianAngelsCount = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_GUARDIAN_ANGELS_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_GUARDIAN_ANGELS_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_PRIMARY_ROLE_GUARDIAN_ANGELS_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillGhostsCountDropdown()
	{
		TMP_Dropdown dropdown = GhostsCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_GHOSTS_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_GHOSTS_COUNT", 1);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.GhostsCount = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_GHOSTS_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_GHOSTS_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_PRIMARY_ROLE_GHOSTS_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillSpectersCountDropdown()
	{
		TMP_Dropdown dropdown = SpectersCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_SPECTERS_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_SPECTERS_COUNT", 1);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.SpectersCount = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_SPECTERS_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_PRIMARY_ROLE_SPECTERS_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_PRIMARY_ROLE_SPECTERS_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillSecondaryRolesCountDropdown()
	{
		TMP_Dropdown dropdown = SecondaryRolesCountConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_SECONDARY_ROLES_COUNT"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_SECONDARY_ROLES_COUNT", 15);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.SecondaryRolesCount = int.Parse(dropdown.options[index].text);
			PlayerPrefs.SetInt("GAME_SETTINGS_SECONDARY_ROLES_COUNT", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_SECONDARY_ROLES_COUNT"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_SECONDARY_ROLES_COUNT");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillSoloRoleToggle(Toggle toggle, PlayerCustom.PlayerNewPrimaryRole role)
	{
		string playerPref = "GAME_SETTINGS_NALES_ROLE_" + role;
		if (!PlayerPrefs.HasKey(playerPref))
		{
			PlayerPrefs.SetInt(playerPref, 1);
		}
		((UnityEventBase)toggle.onValueChanged).RemoveAllListeners();
		((UnityEvent<bool>)(object)toggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool value)
		{
			Plugin.CustomConfig.SoloRoleActive[role] = value;
			PlayerPrefs.SetInt(playerPref, value ? 1 : 0);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey(playerPref) && PlayerPrefs.GetInt(playerPref) == 1)
		{
			SoloRolesConfig[role].SetIsOnWithoutNotify(true);
		}
	}

	public static void FillPrimaryRolePowerToggle(Toggle toggle, PlayerCustom.PlayerPrimaryRolePower role)
	{
		string playerPref = "GAME_SETTINGS_NALES_ROLE_" + role;
		if (!PlayerPrefs.HasKey(playerPref))
		{
			PlayerPrefs.SetInt(playerPref, 1);
		}
		((UnityEventBase)toggle.onValueChanged).RemoveAllListeners();
		((UnityEvent<bool>)(object)toggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool value)
		{
			Plugin.CustomConfig.PrimaryRolePowerActive[role] = value;
			PlayerPrefs.SetInt(playerPref, value ? 1 : 0);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey(playerPref) && PlayerPrefs.GetInt(playerPref) == 1)
		{
			PrimaryRolePowerConfig[role].SetIsOnWithoutNotify(true);
		}
	}

	public static void FillSecondaryRoleToggle(Toggle toggle, PlayerCustom.PlayerSecondaryRole role)
	{
		string playerPref = "GAME_SETTINGS_NALES_ROLE_" + role;
		if (!PlayerPrefs.HasKey(playerPref))
		{
			PlayerPrefs.SetInt(playerPref, 1);
		}
		((UnityEventBase)toggle.onValueChanged).RemoveAllListeners();
		((UnityEvent<bool>)(object)toggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool value)
		{
			Plugin.CustomConfig.SecondaryRoleActive[role] = value;
			PlayerPrefs.SetInt(playerPref, value ? 1 : 0);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey(playerPref) && PlayerPrefs.GetInt(playerPref) == 1)
		{
			SecondaryRolesConfig[role].SetIsOnWithoutNotify(true);
		}
	}

	public static void FillEventChanceDropdown()
	{
		TMP_Dropdown dropdown = EventChanceConfig;
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_EVENT_CHANCE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_EVENT_CHANCE", 2);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.EventChance = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_EVENT_CHANCE", index);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_EVENT_CHANCE"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_EVENT_CHANCE");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillEventToggle(Toggle toggle, EventsManager.EventType dailyEvent)
	{
		string playerPref = "NALES_EVENT_" + dailyEvent.ToString().ToUpper();
		if (!PlayerPrefs.HasKey(playerPref))
		{
			PlayerPrefs.SetInt(playerPref, 1);
		}
		((UnityEventBase)toggle.onValueChanged).RemoveAllListeners();
		((UnityEvent<bool>)(object)toggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool value)
		{
			Plugin.CustomConfig.EventsActive[dailyEvent] = value;
			PlayerPrefs.SetInt(playerPref, value ? 1 : 0);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey(playerPref) && PlayerPrefs.GetInt(playerPref) == 1)
		{
			EventsConfig[dailyEvent].SetIsOnWithoutNotify(true);
		}
	}

	public void FillTrapsModifiedToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_TRAPS_MODIFIED"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_TRAPS_MODIFIED", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_TRAPS_MODIFIED") && PlayerPrefs.GetInt("GAME_SETTINGS_TRAPS_MODIFIED") == 1)
		{
			TrapsModified = NetworkBool.op_Implicit(true);
			TrapsModifiedToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateTrapsModified(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.TrapsModified = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_TRAPS_MODIFIED", value ? 1 : 0);
	}

	public void UpdateTrapsModifiedSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			TrapsModified = NetworkBool.op_Implicit(value);
		}
	}

	public void FillSmokeBoostedToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_SMOKE_BOOSTED"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_SMOKE_BOOSTED", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_SMOKE_BOOSTED") && PlayerPrefs.GetInt("GAME_SETTINGS_SMOKE_BOOSTED") == 1)
		{
			SmokeBoosted = NetworkBool.op_Implicit(true);
			SmokeBoostedToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateSmokeBoosted(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.SmokeBoosted = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_SMOKE_BOOSTED", value ? 1 : 0);
	}

	public void UpdateSmokeBoostedSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			SmokeBoosted = NetworkBool.op_Implicit(value);
		}
	}

	public void FillAnonymousVotesToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_ANONYMOUS_VOTES"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_ANONYMOUS_VOTES", 0);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_ANONYMOUS_VOTES") && PlayerPrefs.GetInt("GAME_SETTINGS_ANONYMOUS_VOTES") == 1)
		{
			AnonymousVotes = NetworkBool.op_Implicit(true);
			AnonymousVotesToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateAnonymousVotes(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.AnonymousVotes = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_ANONYMOUS_VOTES", value ? 1 : 0);
	}

	public void UpdateAnonymousVotesSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			AnonymousVotes = NetworkBool.op_Implicit(value);
		}
	}

	public void FillSabotagesAvailableToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_SABOTAGES_AVAILABLE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_SABOTAGES_AVAILABLE", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_SABOTAGES_AVAILABLE") && PlayerPrefs.GetInt("GAME_SETTINGS_SABOTAGES_AVAILABLE") == 1)
		{
			SabotagesAvailable = NetworkBool.op_Implicit(true);
			SabotagesAvailableToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateSabotagesAvailable(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.SabotagesAvailable = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_SABOTAGES_AVAILABLE", value ? 1 : 0);
	}

	public void UpdateSabotagesAvailableSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			SabotagesAvailable = NetworkBool.op_Implicit(value);
		}
	}

	public void FillDropItemsAvailableToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_DROP_ITEMS_AVAILABLE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_DROP_ITEMS_AVAILABLE", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_DROP_ITEMS_AVAILABLE") && PlayerPrefs.GetInt("GAME_SETTINGS_DROP_ITEMS_AVAILABLE") == 1)
		{
			DropItemsAvailable = NetworkBool.op_Implicit(true);
			DropItemsAvailableToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateDropItemsAvailable(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.DropItemsAvailable = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_DROP_ITEMS_AVAILABLE", value ? 1 : 0);
	}

	public void UpdateDropItemsAvailableSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			DropItemsAvailable = NetworkBool.op_Implicit(value);
		}
	}

	public static void FillNightFogDropdown(TMP_Dropdown dropdown)
	{
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_NIGHT_FOG"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_NIGHT_FOG", 5);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.NightFog = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_NIGHT_FOG", index);
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_NIGHT_FOG"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_NIGHT_FOG");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public void FillWolvesCanUseItemsToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_WOLVES_CAN_USE_ITEMS"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_WOLVES_CAN_USE_ITEMS", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_WOLVES_CAN_USE_ITEMS") && PlayerPrefs.GetInt("GAME_SETTINGS_WOLVES_CAN_USE_ITEMS") == 1)
		{
			WolvesCanUseItems = NetworkBool.op_Implicit(true);
			WolvesCanUseItemsToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateWolvesCanUseItems(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.WolvesCanUseItems = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_WOLVES_CAN_USE_ITEMS", value ? 1 : 0);
	}

	public void UpdateWolvesCanUseItemsSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			WolvesCanUseItems = NetworkBool.op_Implicit(value);
		}
	}

	public void FillDraftModeToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_DRAFT_MODE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_DRAFT_MODE", 0);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_DRAFT_MODE") && PlayerPrefs.GetInt("GAME_SETTINGS_DRAFT_MODE") == 1)
		{
			DraftMode = NetworkBool.op_Implicit(true);
			DraftModeToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateDraftMode(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.DraftMode = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_DRAFT_MODE", value ? 1 : 0);
	}

	public void UpdateDraftModeSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			DraftMode = NetworkBool.op_Implicit(value);
		}
	}

	public void FillShowLastGameSummaryToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_SHOW_LAST_GAME_SUMMARY"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_SHOW_LAST_GAME_SUMMARY", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_SHOW_LAST_GAME_SUMMARY") && PlayerPrefs.GetInt("GAME_SETTINGS_SHOW_LAST_GAME_SUMMARY") == 1)
		{
			ShowLastGameSummary = NetworkBool.op_Implicit(true);
			ShowLastGameSummaryToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateShowLastGameSummary(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.ShowLastGameSummary = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_SHOW_LAST_GAME_SUMMARY", value ? 1 : 0);
	}

	public void UpdateShowLastGameSummarySetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			ShowLastGameSummary = NetworkBool.op_Implicit(value);
		}
	}

	public static void FillSpyPercentageDropdown(TMP_Dropdown dropdown)
	{
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_SPY_PERCENTAGE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_SPY_PERCENTAGE", 5);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.SpyPercentage = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_SPY_PERCENTAGE", index);
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_SPY_PERCENTAGE"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_SPY_PERCENTAGE");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillScientistResearchSpeedDropdown(TMP_Dropdown dropdown)
	{
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_SCIENTIST_RESEARCH_SPEED"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_SCIENTIST_RESEARCH_SPEED", 5);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.ScientistResearchSpeed = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_SCIENTIST_RESEARCH_SPEED", index);
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_SCIENTIST_RESEARCH_SPEED"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_SCIENTIST_RESEARCH_SPEED");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillMercenaryPercentageDropdown(TMP_Dropdown dropdown)
	{
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_SMUGGLER_PERCENTAGE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_SMUGGLER_PERCENTAGE", 5);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.MercenaryPercentage = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_SMUGGLER_PERCENTAGE", index);
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_SMUGGLER_PERCENTAGE"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_SMUGGLER_PERCENTAGE");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillLoverVillagerHungerSpeedDropdown(TMP_Dropdown dropdown)
	{
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_LOVER_VILLAGER_HUNGER_SPEED"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_LOVER_VILLAGER_HUNGER_SPEED", 5);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.LoverVillagerHungerSpeed = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_LOVER_VILLAGER_HUNGER_SPEED", index);
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_LOVER_VILLAGER_HUNGER_SPEED"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_LOVER_VILLAGER_HUNGER_SPEED");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public static void FillCultistSpeedPercentageDropdown(TMP_Dropdown dropdown)
	{
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_CULTIST_SPEED"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_CULTIST_SPEED", 5);
		}
		((UnityEventBase)dropdown.onValueChanged).RemoveAllListeners();
		((UnityEvent<int>)(object)dropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
		{
			Plugin.CustomConfig.CultistSpeed = int.Parse(dropdown.options[index].text.Replace("%", ""));
			PlayerPrefs.SetInt("GAME_SETTINGS_CULTIST_SPEED", index);
		});
		if (PlayerPrefs.HasKey("GAME_SETTINGS_CULTIST_SPEED"))
		{
			int num = PlayerPrefs.GetInt("GAME_SETTINGS_CULTIST_SPEED");
			if (num < dropdown.options.Count)
			{
				dropdown.SetValueWithoutNotify(num);
			}
		}
	}

	public void FillDeceiverHasFlatulenceChanceToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_DECEIVER_HAS_FLATULENCE_CHANCE"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_DECEIVER_HAS_FLATULENCE_CHANCE", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_DECEIVER_HAS_FLATULENCE_CHANCE") && PlayerPrefs.GetInt("GAME_SETTINGS_DECEIVER_HAS_FLATULENCE_CHANCE") == 1)
		{
			DeceiverHasFlatulenceChance = NetworkBool.op_Implicit(true);
			DeceiverHasFlatulenceChanceToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateDeceiverHasFlatulenceChance(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.DeceiverHasFlatulenceChance = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_DECEIVER_HAS_FLATULENCE_CHANCE", value ? 1 : 0);
	}

	public void UpdateDeceiverHasFlatulenceChanceSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			DeceiverHasFlatulenceChance = NetworkBool.op_Implicit(value);
		}
	}

	public void FillLoverWolfReplacesVillagerToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_LOVER_WOLF_REPLACES_VILLAGER"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_LOVER_WOLF_REPLACES_VILLAGER", 0);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_LOVER_WOLF_REPLACES_VILLAGER") && PlayerPrefs.GetInt("GAME_SETTINGS_LOVER_WOLF_REPLACES_VILLAGER") == 1)
		{
			LoverWolfReplacesVillager = NetworkBool.op_Implicit(false);
			LoverWolfReplacesVillagerToggle.SetIsOnWithoutNotify(false);
		}
	}

	public static void UpdateLoverWolfReplacesVillagerChance(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.LoverWolfReplacesVillager = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_LOVER_WOLF_REPLACES_VILLAGER", value ? 1 : 0);
	}

	public void UpdateLoverWolfReplacesVillagerSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			LoverWolfReplacesVillager = NetworkBool.op_Implicit(value);
		}
	}

	public void FillAllowMayorToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_ALLOW_MAYOR"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_ALLOW_MAYOR", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_ALLOW_MAYOR") && PlayerPrefs.GetInt("GAME_SETTINGS_ALLOW_MAYOR") == 1)
		{
			AllowMayor = NetworkBool.op_Implicit(true);
			AllowMayorToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateAllowMayor(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.AllowMayor = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_ALLOW_MAYOR", value ? 1 : 0);
	}

	public void UpdateAllowMayorSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			AllowMayor = NetworkBool.op_Implicit(value);
		}
	}

	public void FillTenacityHubrisToggle()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("GAME_SETTINGS_TENACITY_HUBRIS"))
		{
			PlayerPrefs.SetInt("GAME_SETTINGS_TENACITY_HUBRIS", 1);
		}
		if (PlayerPrefs.HasKey("GAME_SETTINGS_TENACITY_HUBRIS") && PlayerPrefs.GetInt("GAME_SETTINGS_TENACITY_HUBRIS") == 1)
		{
			TenacityHubris = NetworkBool.op_Implicit(true);
			TenacityHubrisToggle.SetIsOnWithoutNotify(true);
		}
	}

	public static void UpdateTenacityHubris(bool value)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Plugin.CustomConfig.TenacityHubris = NetworkBool.op_Implicit(value);
		PlayerPrefs.SetInt("GAME_SETTINGS_TENACITY_HUBRIS", value ? 1 : 0);
	}

	public void UpdateTenacityHubrisSetting(bool value)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 1)
		{
			TenacityHubris = NetworkBool.op_Implicit(value);
		}
	}

	public static void FillPotionToggle(Toggle potionToggle, Effect effect)
	{
		string playerPref = "GAME_SETTINGS_NALES_POTION_" + effect.GetTranslateKey();
		if (!PlayerPrefs.HasKey(playerPref))
		{
			PlayerPrefs.SetInt(playerPref, 1);
		}
		((UnityEventBase)potionToggle.onValueChanged).RemoveAllListeners();
		((UnityEvent<bool>)(object)potionToggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool value)
		{
			Plugin.CustomConfig.PotionsAvailability[effect.GetTranslateKey()] = value;
			PlayerPrefs.SetInt(playerPref, value ? 1 : 0);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey(playerPref) && PlayerPrefs.GetInt(playerPref) == 1)
		{
			PotionsConfig[effect.GetTranslateKey()].SetIsOnWithoutNotify(true);
		}
	}

	public static void FillGadgetToggle(Toggle gadgetToggle, string itemKey)
	{
		string playerPref = "GAME_SETTINGS_" + itemKey;
		if (!PlayerPrefs.HasKey(playerPref))
		{
			PlayerPrefs.SetInt(playerPref, 1);
		}
		((UnityEventBase)gadgetToggle.onValueChanged).RemoveAllListeners();
		((UnityEvent<bool>)(object)gadgetToggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool value)
		{
			Plugin.CustomConfig.GadgetsAvailability[itemKey] = value;
			PlayerPrefs.SetInt(playerPref, value ? 1 : 0);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey(playerPref) && PlayerPrefs.GetInt(playerPref) == 1)
		{
			GadgetsConfig[itemKey].SetIsOnWithoutNotify(true);
		}
	}

	public static void FillAccessoryToggle(Toggle accessoryToggle, string itemKey)
	{
		string playerPref = "GAME_SETTINGS_" + itemKey;
		if (!PlayerPrefs.HasKey(playerPref))
		{
			PlayerPrefs.SetInt(playerPref, 1);
		}
		((UnityEventBase)accessoryToggle.onValueChanged).RemoveAllListeners();
		((UnityEvent<bool>)(object)accessoryToggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool value)
		{
			Plugin.CustomConfig.AccessoriesAvailability[itemKey] = value;
			PlayerPrefs.SetInt(playerPref, value ? 1 : 0);
			UIOptionsDisplayPanel.SendRefreshToClients();
		});
		if (PlayerPrefs.HasKey(playerPref) && PlayerPrefs.GetInt(playerPref) == 1)
		{
			AccessoriesConfig[itemKey].SetIsOnWithoutNotify(true);
		}
	}

	public void ResetToDefault()
	{
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		SoloRolesCountConfig.value = 2;
		TraitorsCountConfig.value = 0;
		WolfPupsCountConfig.value = 0;
		WolfPowersCountConfig.value = 3;
		VillagerPowersChanceConfig.value = 10;
		ElitesCountConfig.value = 2;
		AvatarChanceConfig.value = 3;
		MoleChanceConfig.value = 5;
		GuardianAngelsCountConfig.value = 1;
		GhostsCountConfig.value = 1;
		SpectersCountConfig.value = 1;
		SecondaryRolesCountConfig.value = 15;
		EventChanceConfig.value = 2;
		PlayerPrefs.SetInt("GAME_SETTINGS_SOLO_ROLES_COUNT", 1);
		PlayerPrefs.SetInt("GAME_SETTINGS_TRAITORS_COUNT", 0);
		PlayerPrefs.SetInt("GAME_SETTINGS_WOLF_PUPS_COUNT", 0);
		PlayerPrefs.SetInt("GAME_SETTINGS_PRIMARY_ROLE_POWERS_COUNT", 3);
		PlayerPrefs.SetInt("GAME_SETTINGS_VILLAGER_PRIMARY_ROLE_POWERS_CHANCE", 10);
		PlayerPrefs.SetInt("GAME_SETTINGS_AVATAR_CHANCE", 3);
		PlayerPrefs.SetInt("GAME_SETTINGS_MOLE_CHANCE", 5);
		PlayerPrefs.SetInt("GAME_SETTINGS_GUARDIAN_ANGELS_COUNT", 1);
		PlayerPrefs.SetInt("GAME_SETTINGS_GHOSTS_COUNT", 1);
		PlayerPrefs.SetInt("GAME_SETTINGS_SPECTERS_COUNT", 1);
		PlayerPrefs.SetInt("GAME_SETTINGS_SECONDARY_ROLES_COUNT", 15);
		PlayerPrefs.SetInt("GAME_SETTINGS_EVENT_CHANCE", 2);
		foreach (KeyValuePair<PlayerCustom.PlayerNewPrimaryRole, Toggle> item in SoloRolesConfig)
		{
			item.Value.isOn = true;
			PlayerPrefs.SetInt("GAME_SETTINGS_NALES_ROLE_" + item.Key, 1);
		}
		foreach (KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, Toggle> item2 in PrimaryRolePowerConfig)
		{
			item2.Value.isOn = true;
			PlayerPrefs.SetInt("GAME_SETTINGS_NALES_ROLE_" + item2.Key, 1);
		}
		foreach (KeyValuePair<PlayerCustom.PlayerSecondaryRole, Toggle> item3 in SecondaryRolesConfig)
		{
			item3.Value.isOn = true;
			PlayerPrefs.SetInt("GAME_SETTINGS_NALES_ROLE_" + item3.Key, 1);
		}
		SpyPercentageDropdown.value = 5;
		PlayerPrefs.SetInt("GAME_SETTINGS_SPY_PERCENTAGE", 5);
		ScientistResearchSpeedDropdown.value = 5;
		PlayerPrefs.SetInt("GAME_SETTINGS_SCIENTIST_RESEARCH_SPEED", 5);
		MercenaryPercentageDropdown.value = 5;
		PlayerPrefs.SetInt("GAME_SETTINGS_SMUGGLER_PERCENTAGE", 5);
		LoverVillagerHungerSpeedDropdown.value = 5;
		PlayerPrefs.SetInt("GAME_SETTINGS_LOVER_VILLAGER_HUNGER_SPEED", 5);
		CultistSpeedDropdown.value = 5;
		PlayerPrefs.SetInt("GAME_SETTINGS_CULTIST_SPEED", 5);
		DeceiverHasFlatulenceChanceToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_DECEIVER_HAS_FLATULENCE_CHANCE", 1);
		DeceiverHasFlatulenceChance = NetworkBool.op_Implicit(true);
		LoverWolfReplacesVillagerToggle.SetIsOnWithoutNotify(false);
		PlayerPrefs.SetInt("GAME_SETTINGS_LOVER_WOLF_REPLACES_VILLAGER", 0);
		LoverWolfReplacesVillager = NetworkBool.op_Implicit(false);
		AllowMayorToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_ALLOW_MAYOR", 1);
		AllowMayor = NetworkBool.op_Implicit(true);
		TenacityHubrisToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_TENACITY_HUBRIS", 1);
		TenacityHubris = NetworkBool.op_Implicit(true);
		TrapsModifiedToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_TRAPS_MODIFIED", 1);
		TrapsModified = NetworkBool.op_Implicit(true);
		SmokeBoostedToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_SMOKE_BOOSTED", 1);
		SmokeBoosted = NetworkBool.op_Implicit(true);
		AnonymousVotesToggle.SetIsOnWithoutNotify(false);
		PlayerPrefs.SetInt("GAME_SETTINGS_ANONYMOUS_VOTES", 0);
		AnonymousVotes = NetworkBool.op_Implicit(false);
		SabotagesAvailableToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_SABOTAGES_AVAILABLE", 1);
		SabotagesAvailable = NetworkBool.op_Implicit(true);
		DropItemsAvailableToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_DROP_ITEMS_AVAILABLE", 1);
		DropItemsAvailable = NetworkBool.op_Implicit(true);
		NightFogDropdown.value = 5;
		PlayerPrefs.SetInt("GAME_SETTINGS_NIGHT_FOG", 5);
		WolvesCanUseItemsToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_WOLVES_CAN_USE_ITEMS", 1);
		WolvesCanUseItems = NetworkBool.op_Implicit(true);
		DraftModeToggle.SetIsOnWithoutNotify(false);
		PlayerPrefs.SetInt("GAME_SETTINGS_DRAFT_MODE", 0);
		DraftMode = NetworkBool.op_Implicit(false);
		ShowLastGameSummaryToggle.SetIsOnWithoutNotify(true);
		PlayerPrefs.SetInt("GAME_SETTINGS_SHOW_LAST_GAME_SUMMARY", 1);
		DraftMode = NetworkBool.op_Implicit(true);
		foreach (KeyValuePair<string, Toggle> item4 in PotionsConfig)
		{
			item4.Value.isOn = true;
			PlayerPrefs.SetInt("GAME_SETTINGS_NALES_POTION_" + item4.Key, 1);
		}
		foreach (KeyValuePair<string, Toggle> item5 in GadgetsConfig)
		{
			item5.Value.isOn = true;
			PlayerPrefs.SetInt("GAME_SETTINGS_" + item5.Key, 1);
		}
		foreach (KeyValuePair<string, Toggle> item6 in AccessoriesConfig)
		{
			item6.Value.isOn = true;
			PlayerPrefs.SetInt("GAME_SETTINGS_" + item6.Key, 1);
		}
		UIOptionsDisplayPanel.SendRefreshToClients();
	}

	private static Toggle GetToggle(GameSettingsUI gameSettingsUI, string key, List<object> arguments, ConfigTypeEnum type, Color textColor)
	{
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		Toggle value = Traverse.Create((object)gameSettingsUI).Field<Toggle>("showAllyToggle").Value;
		Transform parent = ((Component)value).transform.parent.parent.parent;
		Transform parent2 = ((Component)value).transform.parent.parent.parent.parent;
		Transform val = Object.Instantiate<Transform>(parent, parent2);
		switch (type)
		{
		case ConfigTypeEnum.RolesVillagers:
			((Component)val).gameObject.AddComponent<RoleVillagerConfigComponent>();
			break;
		case ConfigTypeEnum.RolesEnemies:
			((Component)val).gameObject.AddComponent<RoleEnemyConfigComponent>();
			break;
		case ConfigTypeEnum.RolesSecondary:
			((Component)val).gameObject.AddComponent<RoleSecondaryConfigComponent>();
			break;
		case ConfigTypeEnum.RolesDetails:
			((Component)val).gameObject.AddComponent<RoleDetailsConfigComponent>();
			break;
		case ConfigTypeEnum.Others:
			((Component)val).gameObject.AddComponent<OtherConfigComponent>();
			break;
		case ConfigTypeEnum.Potions:
			((Component)val).gameObject.AddComponent<PotionConfigComponent>();
			break;
		case ConfigTypeEnum.Gadgets:
			((Component)val).gameObject.AddComponent<GadgetConfigComponent>();
			break;
		case ConfigTypeEnum.Events:
			((Component)val).gameObject.AddComponent<EventConfigComponent>();
			break;
		}
		LocalizeStringEvent val2 = ((Component)val).GetComponentsInChildren<LocalizeStringEvent>().First((LocalizeStringEvent o) => ((Object)o).name == "SettingNameText");
		val2.StringReference.Arguments = arguments;
		((LocalizedReference)val2.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit(key));
		((Graphic)((Component)val).GetComponentInChildren<TextMeshProUGUI>()).color = textColor;
		Toggle componentInChildren = ((Component)val).GetComponentInChildren<Toggle>();
		GameObject gameObject = ((Component)componentInChildren).gameObject;
		Object.DestroyImmediate((Object)(object)componentInChildren);
		componentInChildren = gameObject.AddComponent<Toggle>();
		componentInChildren.graphic = ((Component)((Component)componentInChildren).transform.Find("Background").Find("Checkmark")).GetComponent<Graphic>();
		return componentInChildren;
	}

	private static TMP_Dropdown GetDropdown(GameSettingsUI gameSettingsUI, string key, List<string> options, ConfigTypeEnum type, Color textColor)
	{
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		TMP_Dropdown value = Traverse.Create((object)gameSettingsUI).Field<TMP_Dropdown>("alchemistsCountDropdown").Value;
		Transform parent = ((Component)value).transform.parent.parent.parent;
		Transform parent2 = ((Component)value).transform.parent.parent.parent.parent;
		Transform val = Object.Instantiate<Transform>(parent, parent2);
		switch (type)
		{
		case ConfigTypeEnum.RolesVillagers:
			((Component)val).gameObject.AddComponent<RoleVillagerConfigComponent>();
			break;
		case ConfigTypeEnum.RolesEnemies:
			((Component)val).gameObject.AddComponent<RoleEnemyConfigComponent>();
			break;
		case ConfigTypeEnum.RolesSecondary:
			((Component)val).gameObject.AddComponent<RoleSecondaryConfigComponent>();
			break;
		case ConfigTypeEnum.RolesDetails:
			((Component)val).gameObject.AddComponent<RoleDetailsConfigComponent>();
			break;
		case ConfigTypeEnum.Others:
			((Component)val).gameObject.AddComponent<OtherConfigComponent>();
			break;
		case ConfigTypeEnum.Potions:
			((Component)val).gameObject.AddComponent<PotionConfigComponent>();
			break;
		case ConfigTypeEnum.Gadgets:
			((Component)val).gameObject.AddComponent<GadgetConfigComponent>();
			break;
		case ConfigTypeEnum.Events:
			((Component)val).gameObject.AddComponent<EventConfigComponent>();
			break;
		}
		((Graphic)((Component)val).GetComponentInChildren<TextMeshProUGUI>()).color = textColor;
		LocalizeStringEvent val2 = ((Component)val).GetComponentsInChildren<LocalizeStringEvent>().First((LocalizeStringEvent o) => ((Object)o).name == "SettingNameText");
		val2.StringReference.Arguments = new List<object> { TranslationManager.Instance.GetTranslation(key) };
		((LocalizedReference)val2.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit(key));
		TMP_Dropdown componentInChildren = ((Component)val).GetComponentInChildren<TMP_Dropdown>();
		componentInChildren.ClearOptions();
		componentInChildren.AddOptions(options);
		return componentInChildren;
	}

	public static void OnClickConfigBase()
	{
		ToggleConfigType(ConfigTypeEnum.Base);
	}

	public static void OnClickConfigRolesVillagers()
	{
		ToggleConfigType(ConfigTypeEnum.RolesVillagers);
	}

	public static void OnClickConfigRolesEnemies()
	{
		ToggleConfigType(ConfigTypeEnum.RolesEnemies);
	}

	public static void OnClickConfigRolesSecondary()
	{
		ToggleConfigType(ConfigTypeEnum.RolesSecondary);
	}

	public static void OnClickConfigRolesDetails()
	{
		ToggleConfigType(ConfigTypeEnum.RolesDetails);
	}

	public static void OnClickConfigOthers()
	{
		ToggleConfigType(ConfigTypeEnum.Others);
	}

	public static void OnClickConfigPotions()
	{
		ToggleConfigType(ConfigTypeEnum.Potions);
	}

	public static void OnClickConfigGadgets()
	{
		ToggleConfigType(ConfigTypeEnum.Gadgets);
	}

	public static void OnClickConfigEvents()
	{
		ToggleConfigType(ConfigTypeEnum.Events);
	}

	private static void ToggleConfigType(ConfigTypeEnum configType)
	{
		try
		{
			Toggle[] componentsInChildren = GameSettingsObject.GetComponentsInChildren<Toggle>(true);
			TMP_Dropdown[] componentsInChildren2 = GameSettingsObject.GetComponentsInChildren<TMP_Dropdown>(true);
			List<GameObject> list = new List<GameObject>();
			Toggle[] array = componentsInChildren;
			foreach (Toggle val in array)
			{
				if (((Object)((Component)val).gameObject).name != "Item")
				{
					Transform parent = ((Component)val).transform.parent.parent.parent;
					list.Add(((Component)parent).gameObject);
				}
			}
			TMP_Dropdown[] array2 = componentsInChildren2;
			foreach (TMP_Dropdown val2 in array2)
			{
				Transform parent2 = ((Component)val2).transform.parent.parent.parent;
				list.Add(((Component)parent2).gameObject);
			}
			RoleVillagerConfigComponent roleVillagerConfigComponent = default(RoleVillagerConfigComponent);
			RoleEnemyConfigComponent roleEnemyConfigComponent = default(RoleEnemyConfigComponent);
			RoleSecondaryConfigComponent roleSecondaryConfigComponent = default(RoleSecondaryConfigComponent);
			RoleDetailsConfigComponent roleDetailsConfigComponent = default(RoleDetailsConfigComponent);
			OtherConfigComponent otherConfigComponent = default(OtherConfigComponent);
			PotionConfigComponent potionConfigComponent = default(PotionConfigComponent);
			GadgetConfigComponent gadgetConfigComponent = default(GadgetConfigComponent);
			EventConfigComponent eventConfigComponent = default(EventConfigComponent);
			foreach (GameObject item in list)
			{
				if (item.TryGetComponent<RoleVillagerConfigComponent>(ref roleVillagerConfigComponent))
				{
					item.SetActive(configType == ConfigTypeEnum.RolesVillagers);
				}
				else if (item.TryGetComponent<RoleEnemyConfigComponent>(ref roleEnemyConfigComponent))
				{
					item.SetActive(configType == ConfigTypeEnum.RolesEnemies);
				}
				else if (item.TryGetComponent<RoleSecondaryConfigComponent>(ref roleSecondaryConfigComponent))
				{
					item.SetActive(configType == ConfigTypeEnum.RolesSecondary);
				}
				else if (item.TryGetComponent<RoleDetailsConfigComponent>(ref roleDetailsConfigComponent))
				{
					item.SetActive(configType == ConfigTypeEnum.RolesDetails);
				}
				else if (item.TryGetComponent<OtherConfigComponent>(ref otherConfigComponent))
				{
					item.SetActive(configType == ConfigTypeEnum.Others);
				}
				else if (item.TryGetComponent<PotionConfigComponent>(ref potionConfigComponent))
				{
					item.SetActive(configType == ConfigTypeEnum.Potions);
				}
				else if (item.TryGetComponent<GadgetConfigComponent>(ref gadgetConfigComponent))
				{
					item.SetActive(configType == ConfigTypeEnum.Gadgets);
				}
				else if (item.TryGetComponent<EventConfigComponent>(ref eventConfigComponent))
				{
					item.SetActive(configType == ConfigTypeEnum.Events);
				}
				else
				{
					item.SetActive(configType == ConfigTypeEnum.Base);
				}
			}
			RoleInfoObject.SetActive(configType == ConfigTypeEnum.RolesVillagers || configType == ConfigTypeEnum.RolesEnemies || configType == ConfigTypeEnum.RolesSecondary);
			GadgetsInfoObject.SetActive(configType == ConfigTypeEnum.Gadgets);
			AccessoriesInfoObject.SetActive(configType == ConfigTypeEnum.Gadgets);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ToggleConfigType error: " + ex));
		}
	}
}
