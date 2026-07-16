using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using BepInEx.Logging;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.NewMaps;
using LycansNewRoles.NewPrimaryRoles;
using LycansNewRoles.PowerObjects;
using LycansNewRoles.Sabotages;
using LycansNewRoles.Stats;
using Managers;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(136)]
public class PlayerCustom : NetworkBehaviour
{
	public enum PlayerSecondaryRole
	{
		None,
		BothAlcoholic,
		BothSprinter,
		BothInfected,
		BothTeleporter,
		BothEngineer,
		BothPolitician,
		BothMetabolic,
		BothIllusionist,
		BothSherif,
		BothGambler,
		BothMedium,
		BothAstral,
		BothScavenger,
		BothBlueMage,
		BothActor,
		BothScribe,
		BothCarabineer,
		BothForger,
		BothImitator,
		BothMerchant,
		BothTinkerer,
		BothTelepath
	}

	public enum PlayerNewPrimaryRole
	{
		None,
		VillageIdiot,
		Agent,
		Spy,
		Scientist,
		Lover,
		Beast,
		Voodoo,
		Mercenary,
		Kidnapper,
		Cultist,
		Zombie,
		Traitor
	}

	public enum PlayerPrimaryRolePower
	{
		None,
		Necromancer,
		Deceiver,
		Saboteur,
		Tracker,
		Warlock,
		Possessor,
		Bomber,
		Poacher,
		Ritualist,
		Predator,
		Sneak,
		Host,
		Peasant,
		Exorcist,
		Avenger,
		Investigator,
		Survivalist,
		Priest,
		Scout,
		Magician,
		Mystic,
		Shadow,
		Hermit,
		Runemaster,
		Avatar,
		Mole,
		Hunter,
		Alchemist,
		Spotter,
		Purifier,
		Angel,
		Ghost,
		Specter
	}

	public enum NewPrimaryRoleType
	{
		SoloRole,
		Traitor,
		Wolf
	}

	private enum IconAbovePlayerType
	{
		SurvivalistDyingIcon,
		BombIcon,
		MercenaryTargetIcon,
		HeldItem,
		AngelShield,
		None
	}

	public enum EffectOnPlayer
	{
		ShieldEffect,
		FlashScreenBlue,
		FlashScreenRed,
		FlashPlayerBlue,
		Megafart,
		HearHauntedSound,
		ChaosEffect,
		Shift,
		ParasiteExplosion,
		KilledByCrystalBallGuess,
		WolfAttack,
		BombExplosion,
		SoundSuccess,
		DiscipleAnchorActivation,
		HauntedLanternsFlicker,
		KidnapperKidnap,
		ForceTransform
	}

	[HarmonyPatch(typeof(PlayerEffectsManager), "GiantChanged")]
	private class GiantChangedPatch
	{
		private static bool Prefix(Changed<PlayerEffectsManager> changed)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(Traverse.Create((object)changed.Behaviour).Field<PlayerController>("_playerController").Value.Ref);
			player.UpdateScaleAndPitch();
			return false;
		}
	}

	[HarmonyPatch(typeof(PlayerEffectsManager), "InvisibleChanged")]
	private class InvisibleChangedPatch
	{
		private static bool Prefix(Changed<PlayerEffectsManager> changed)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(Traverse.Create((object)changed.Behaviour).Field<PlayerController>("_playerController").Value.Ref);
			player.UpdateVisibility();
			return false;
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Func<PlayerPrimaryRolePower, bool> _003C_003E9__88_0;

		public static Func<PlayerPrimaryRolePower, bool> _003C_003E9__90_0;

		public static Predicate<PlayerCustom> _003C_003E9__519_0;

		public static Predicate<HauntedEffect.HauntedPossibleEffect> _003C_003E9__522_0;

		public static Predicate<HauntedEffect.HauntedPossibleEffect> _003C_003E9__522_1;

		public static Func<Effect, bool> _003C_003E9__522_2;

		public static Func<Effect, bool> _003C_003E9__522_3;

		public static Func<Effect, bool> _003C_003E9__522_4;

		public static Func<Effect, bool> _003C_003E9__522_5;

		public static Predicate<ChaosEffect.ChaosPossibleEffect> _003C_003E9__522_7;

		public static Func<Door, bool> _003C_003E9__522_11;

		public static Predicate<ChaosEffect.ChaosPossibleEffect> _003C_003E9__522_9;

		public static Func<Door, bool> _003C_003E9__522_12;

		public static Predicate<ChaosEffect.ChaosPossibleEffect> _003C_003E9__522_10;

		public static Func<Item, bool> _003C_003E9__522_13;

		public static Func<Door, bool> _003C_003E9__522_14;

		public static Func<Door, bool> _003C_003E9__522_15;

		public static Func<Item, bool> _003C_003E9__522_16;

		public static Func<Effect, bool> _003C_003E9__522_20;

		public static Predicate<PlayerCustom> _003C_003E9__522_22;

		public static Func<PlayerCustom, PlayerRef> _003C_003E9__522_23;

		public static Predicate<PlayerCustom> _003C_003E9__522_24;

		public static Func<PlayerCustom, PlayerRef> _003C_003E9__522_25;

		public static Predicate<PlayerCustom> _003C_003E9__522_26;

		public static Func<PlayerCustom, PlayerRef> _003C_003E9__522_27;

		public static Predicate<PlayerCustom> _003C_003E9__522_28;

		public static Func<PlayerCustom, PlayerRef> _003C_003E9__522_29;

		public static Func<PlayerRef, bool> _003C_003E9__522_31;

		public static Func<Effect, Effect> _003C_003E9__529_0;

		public static Func<Item, bool> _003C_003E9__529_3;

		public static Func<Item, bool> _003C_003E9__529_4;

		public static Func<Item, bool> _003C_003E9__530_0;

		public static Func<Item, bool> _003C_003E9__530_1;

		public static Predicate<PlayerDetectiveIntel.PlayerDetectiveIntelType> _003C_003E9__539_0;

		public static Predicate<PlayerDetectiveIntel.PlayerDetectiveIntelType> _003C_003E9__539_1;

		public static Predicate<PlayerDetectiveIntel.PlayerDetectiveIntelType> _003C_003E9__539_2;

		public static Predicate<PlayerDetectiveIntel.PlayerDetectiveIntelType> _003C_003E9__539_3;

		public static Predicate<PlayerCustom> _003C_003E9__539_16;

		public static Predicate<PlayerController> _003C_003E9__539_17;

		public static Func<PlayerCustom, PlayerRef> _003C_003E9__539_18;

		public static Func<PlayerCustom, bool> _003C_003E9__539_20;

		public static Predicate<PlayerCustom> _003C_003E9__539_7;

		public static Predicate<PlayerCustom> _003C_003E9__539_8;

		public static Predicate<PlayerCustom> _003C_003E9__539_9;

		public static Predicate<PlayerCustom> _003C_003E9__539_10;

		public static Predicate<PlayerCustom> _003C_003E9__539_12;

		public static Predicate<PlayerCustom> _003C_003E9__539_13;

		public static Predicate<PlayerCustom> _003C_003E9__539_14;

		public static Func<Effect, bool> _003C_003E9__546_9;

		public static Func<Item, bool> _003C_003E9__546_3;

		public static Func<Item, bool> _003C_003E9__546_10;

		public static Func<Effect, bool> _003C_003E9__546_11;

		public static Func<PlayerCustom, bool> _003C_003E9__546_12;

		public static OnBeforeSpawned _003C_003E9__548_4;

		public static Func<Effect, bool> _003C_003E9__548_6;

		public static Func<Effect, bool> _003C_003E9__548_7;

		public static Func<Effect, bool> _003C_003E9__548_8;

		public static Func<Effect, bool> _003C_003E9__548_9;

		public static Func<PlayerCustom, PlayerRef> _003C_003E9__550_1;

		public static Predicate<PlayerController> _003C_003E9__550_2;

		public static Func<Item, bool> _003C_003E9__550_4;

		public static Func<Item, bool> _003C_003E9__550_8;

		public static Func<Item, bool> _003C_003E9__550_9;

		public static Func<Item, bool> _003C_003E9__550_10;

		public static Func<Item, bool> _003C_003E9__550_11;

		public static Func<Item, bool> _003C_003E9__550_12;

		public static Func<Item, bool> _003C_003E9__550_13;

		public static Func<Item, bool> _003C_003E9__550_14;

		public static Func<Item, bool> _003C_003E9__550_15;

		public static Func<Item, bool> _003C_003E9__550_16;

		public static Func<Effect, bool> _003C_003E9__551_4;

		public static Func<Effect, bool> _003C_003E9__551_5;

		public static Predicate<PlayerCustom> _003C_003E9__553_0;

		public static Predicate<PlayerCustom> _003C_003E9__553_1;

		public static Predicate<PlayerCustom> _003C_003E9__553_2;

		public static Predicate<PlayerCustom> _003C_003E9__557_0;

		public static Predicate<PlayerController> _003C_003E9__557_1;

		public static Func<Effect, bool> _003C_003E9__557_3;

		public static Func<Teleporter, bool> _003C_003E9__557_4;

		public static Func<Effect, bool> _003C_003E9__573_0;

		public static Func<Effect, bool> _003C_003E9__576_0;

		public static Predicate<PlayerCustom> _003C_003E9__590_0;

		public static Predicate<PlayerCustom> _003C_003E9__593_0;

		public static Func<Effect, bool> _003C_003E9__594_0;

		public static Func<Effect, bool> _003C_003E9__601_0;

		public static Func<Effect, bool> _003C_003E9__604_0;

		public static Func<Effect, bool> _003C_003E9__606_0;

		public static Func<Effect, bool> _003C_003E9__606_1;

		public static Predicate<PlayerCustom> _003C_003E9__618_0;

		public static Func<Effect, bool> _003C_003E9__636_0;

		public static Func<Effect, bool> _003C_003E9__638_0;

		public static Func<Effect, bool> _003C_003E9__642_0;

		public static Func<Effect, bool> _003C_003E9__642_1;

		public static Predicate<PlayerCustom> _003C_003E9__642_2;

		public static Predicate<PlayerCustom> _003C_003E9__642_3;

		public static Func<Effect, bool> _003C_003E9__644_0;

		public static Func<Effect, bool> _003C_003E9__644_1;

		public static Func<Effect, bool> _003C_003E9__644_2;

		public static Func<Effect, bool> _003C_003E9__644_3;

		public static Func<Effect, bool> _003C_003E9__677_0;

		public static Func<Accessory, bool> _003C_003E9__677_1;

		public static Predicate<PlayerController> _003C_003E9__690_0;

		public static Func<Item, bool> _003C_003E9__691_0;

		public static Func<Accessory, bool> _003C_003E9__691_1;

		public static Func<Item, bool> _003C_003E9__691_2;

		public static Func<Accessory, bool> _003C_003E9__691_3;

		public static Predicate<PlayerCustom> _003C_003E9__691_4;

		public static Predicate<PlayerCustom> _003C_003E9__691_5;

		public static Predicate<PlayerCustom> _003C_003E9__691_6;

		public static Func<Teleporter, bool> _003C_003E9__691_8;

		public static Predicate<PlayerCustom> _003C_003E9__695_1;

		public static Func<Effect, bool> _003C_003E9__695_2;

		public static Func<Effect, bool> _003C_003E9__695_3;

		public static Func<RunemasterRune, bool> _003C_003E9__695_5;

		public static Func<Effect, bool> _003C_003E9__695_6;

		public static Func<Effect, bool> _003C_003E9__707_0;

		public static Func<Teleporter, bool> _003C_003E9__710_0;

		public static Action<PlayerController> _003C_003E9__721_0;

		public static Predicate<PlayerCustom> _003C_003E9__721_1;

		public static Predicate<PlayerCustom> _003C_003E9__721_2;

		public static Func<PlayerCustom, bool> _003C_003E9__721_3;

		public static Predicate<PlayerCustom> _003C_003E9__721_6;

		public static Predicate<PlayerCustom> _003C_003E9__721_7;

		public static Func<PlayerCustom, PlayerController> _003C_003E9__721_8;

		public static Func<PlayerCustom, PlayerController> _003C_003E9__721_4;

		public static Predicate<PlayerCustom> _003C_003E9__721_5;

		public static Predicate<PlayerController> _003C_003E9__721_9;

		public static Predicate<PlayerController> _003C_003E9__721_10;

		public static Predicate<PlayerController> _003C_003E9__721_11;

		public static Predicate<PlayerController> _003C_003E9__721_12;

		public static Predicate<PlayerController> _003C_003E9__721_13;

		internal bool _003Cget_AllVillagerJobs_003Eb__88_0(PlayerPrimaryRolePower o)
		{
			return IsPrimaryRolePowerForNormalVillagers(o);
		}

		internal bool _003Cget_AllWolfPowers_003Eb__90_0(PlayerPrimaryRolePower o)
		{
			return IsPrimaryRolePowerForWolves(o);
		}

		internal bool _003CDespawned_003Eb__519_0(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return NetworkBool.op_Implicit(o.Kidnapped);
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_0(HauntedEffect.HauntedPossibleEffect o)
		{
			return o == HauntedEffect.HauntedPossibleEffect.HealthGain;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_1(HauntedEffect.HauntedPossibleEffect o)
		{
			return o == HauntedEffect.HauntedPossibleEffect.HealthLoss;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_2(Effect o)
		{
			return o is FlatulenceEffect;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_3(Effect o)
		{
			return o is InvisibilityEffect;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_4(Effect o)
		{
			return o is ParanoiaEffect;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_5(Effect o)
		{
			return o is SpeedEffect;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_7(ChaosEffect.ChaosPossibleEffect o)
		{
			return o == ChaosEffect.ChaosPossibleEffect.UseScrollOnNearbyPlayer || o == ChaosEffect.ChaosPossibleEffect.UseDiamondOnNearbyPlayer;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_11(Door o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return NetworkBool.op_Implicit(o.IsLocked);
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_9(ChaosEffect.ChaosPossibleEffect o)
		{
			return o == ChaosEffect.ChaosPossibleEffect.UnlockNearbyDoor;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_12(Door o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.IsLocked);
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_10(ChaosEffect.ChaosPossibleEffect o)
		{
			return o == ChaosEffect.ChaosPossibleEffect.LockNearbyDoor;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_13(Item o)
		{
			return o is TrapItem;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_14(Door o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.IsLocked);
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_15(Door o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return NetworkBool.op_Implicit(o.IsLocked);
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_16(Item o)
		{
			return o is SmokeItem;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_20(Effect o)
		{
			return o is RecuperatingEffect;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_22(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return NetworkBool.op_Implicit(o.BeastMark);
		}

		internal PlayerRef _003CFixedUpdateNetwork_003Eb__522_23(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return o.Ref;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_24(PlayerCustom o)
		{
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Zombie;
		}

		internal PlayerRef _003CFixedUpdateNetwork_003Eb__522_25(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return o.Ref;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_26(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return NetworkBool.op_Implicit(o.Kidnapped);
		}

		internal PlayerRef _003CFixedUpdateNetwork_003Eb__522_27(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return o.Ref;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_28(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return NetworkBool.op_Implicit(o.Parasite);
		}

		internal PlayerRef _003CFixedUpdateNetwork_003Eb__522_29(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return o.Ref;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__522_31(PlayerRef o)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			return !PlayersWithSpecificColor.Contains(o);
		}

		internal Effect _003CGiveRandomItem_003Eb__529_0(Effect e)
		{
			return e;
		}

		internal bool _003CGiveRandomItem_003Eb__529_3(Item o)
		{
			return Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)];
		}

		internal bool _003CGiveRandomItem_003Eb__529_4(Item o)
		{
			return o is LockItem || o is TrapItem || o is SmokeItem || o is SpyglassItem || o is MagicScrollItem || o is PhasingDiamondItem || o is GrenadeItem || o is SleepingGasItem || o is MolotovItem || o is RadarItem;
		}

		internal bool _003CGiveScientistGadget_003Eb__530_0(Item o)
		{
			return Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)];
		}

		internal bool _003CGiveScientistGadget_003Eb__530_1(Item o)
		{
			return o is SmokeItem || o is GrenadeItem || o is SleepingGasItem || o is RadarItem;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_0(PlayerDetectiveIntel.PlayerDetectiveIntelType o)
		{
			return o == PlayerDetectiveIntel.PlayerDetectiveIntelType.DifferentSides;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_1(PlayerDetectiveIntel.PlayerDetectiveIntelType o)
		{
			return o == PlayerDetectiveIntel.PlayerDetectiveIntelType.OneIsEvil;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_2(PlayerDetectiveIntel.PlayerDetectiveIntelType o)
		{
			return o == PlayerDetectiveIntel.PlayerDetectiveIntelType.TransformationsAndDetransformations;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_3(PlayerDetectiveIntel.PlayerDetectiveIntelType o)
		{
			return o == PlayerDetectiveIntel.PlayerDetectiveIntelType.WolvesAndSoloRolesRemaining;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_16(PlayerCustom o)
		{
			return o.SecondaryRole == PlayerSecondaryRole.BothTelepath;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_17(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.IsDead);
		}

		internal PlayerRef _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_18(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return o.Ref;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_20(PlayerCustom o)
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Invalid comparison between Unknown and I4
			return o.NewPrimaryRole != PlayerNewPrimaryRole.None || (int)o.PlayerController.Role == 1;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_7(PlayerCustom o)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Invalid comparison between Unknown and I4
			return (int)o.PlayerController.Role == 1 || o.NewPrimaryRole == PlayerNewPrimaryRole.Traitor;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_8(PlayerCustom o)
		{
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Lover;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_9(PlayerCustom o)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Invalid comparison between Unknown and I4
			return (int)o.PlayerController.Role != 1 && o.NewPrimaryRole == PlayerNewPrimaryRole.None;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_10(PlayerCustom o)
		{
			return o.SecondaryRole == PlayerSecondaryRole.BothTelepath;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_12(PlayerCustom o)
		{
			return o.SecondaryRole == PlayerSecondaryRole.BothTelepath;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_13(PlayerCustom o)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Invalid comparison between Unknown and I4
			return !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Resurrected) && (int)o.PlayerController.Role == 1;
		}

		internal bool _003CPrimaryRolePowerCurrentMaterialsChanged_003Eb__539_14(PlayerCustom o)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.NewPrimaryRole != PlayerNewPrimaryRole.None && o.NewPrimaryRole != PlayerNewPrimaryRole.Traitor && o.NewPrimaryRole != PlayerNewPrimaryRole.Zombie;
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_Without_Target_003Eb__546_9(Effect o)
		{
			return o is FlatulenceEffect;
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_Without_Target_003Eb__546_3(Item o)
		{
			return Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)];
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_Without_Target_003Eb__546_10(Item o)
		{
			return o is MagicScrollItem;
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_Without_Target_003Eb__546_11(Effect o)
		{
			return o is TelepathyEffect;
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_Without_Target_003Eb__546_12(PlayerCustom o)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return NetworkBool.op_Implicit(o.PlayerController.IsWolf);
		}

		internal void _003CRpc_Activate_Secondary_Role_Power_With_Target_003Eb__548_4(NetworkRunner _, NetworkObject no)
		{
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_With_Target_003Eb__548_6(Effect o)
		{
			return o is InvisibilityEffect;
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_With_Target_003Eb__548_7(Effect o)
		{
			return o is SpeedEffect;
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_With_Target_003Eb__548_8(Effect o)
		{
			return o is NightVision;
		}

		internal bool _003CRpc_Activate_Secondary_Role_Power_With_Target_003Eb__548_9(Effect o)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Invalid comparison between Unknown and I4
			return Plugin.CustomConfig.PotionsAvailability[o.GetTranslateKey()] && (int)o.GetEffectType() == 0;
		}

		internal PlayerRef _003CSecondaryRoleActionTimerExpired_003Eb__550_1(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return o.Ref;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_2(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.IsDead);
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_4(Item o)
		{
			return o is MagicScrollItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_8(Item o)
		{
			return o is LockItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_9(Item o)
		{
			return o is TrapItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_10(Item o)
		{
			return o is SmokeItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_11(Item o)
		{
			return o is SpyglassItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_12(Item o)
		{
			return o is PhasingDiamondItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_13(Item o)
		{
			return o is GrenadeItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_14(Item o)
		{
			return o is SleepingGasItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_15(Item o)
		{
			return o is MolotovItem;
		}

		internal bool _003CSecondaryRoleActionTimerExpired_003Eb__550_16(Item o)
		{
			return o is RadarItem;
		}

		internal bool _003CRpc_Activate_Primary_Role_Power_Without_Target_003Eb__551_4(Effect o)
		{
			return o is TelepathyEffect;
		}

		internal bool _003CRpc_Activate_Primary_Role_Power_Without_Target_003Eb__551_5(Effect o)
		{
			return o is KidnapperSilenceEffect;
		}

		internal bool _003CRpc_Activate_Primary_Role_Power_With_Target_003Eb__553_0(PlayerCustom o)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Avenger && !NetworkBool.op_Implicit(o.PlayerController.IsDead);
		}

		internal bool _003CRpc_Activate_Primary_Role_Power_With_Target_003Eb__553_1(PlayerCustom o)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Shadow && !NetworkBool.op_Implicit(o.PlayerController.IsDead);
		}

		internal bool _003CRpc_Activate_Primary_Role_Power_With_Target_003Eb__553_2(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return NetworkBool.op_Implicit(o.Parasite);
		}

		internal bool _003CPrimaryRoleActionTimerExpired_003Eb__557_0(PlayerCustom o)
		{
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Zombie;
		}

		internal bool _003CPrimaryRoleActionTimerExpired_003Eb__557_1(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.IsDead);
		}

		internal bool _003CPrimaryRoleActionTimerExpired_003Eb__557_3(Effect o)
		{
			return o is DisguisedEffect;
		}

		internal bool _003CPrimaryRoleActionTimerExpired_003Eb__557_4(Teleporter o)
		{
			return o.MapID == GameManager.Instance.MapID;
		}

		internal bool _003CRpc_Assassinate_003Eb__573_0(Effect o)
		{
			return o is AssassinEffect;
		}

		internal bool _003CRpc_Petrify_003Eb__576_0(Effect o)
		{
			return o is MidasEffect;
		}

		internal bool _003CKidnappedChanged_003Eb__590_0(PlayerCustom o)
		{
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Kidnapper;
		}

		internal bool _003CPossessedChanged_003Eb__593_0(PlayerCustom o)
		{
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Possessor;
		}

		internal bool _003CRpc_Manipulate_Item_003Eb__594_0(Effect o)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Invalid comparison between Unknown and I4
			return Plugin.CustomConfig.PotionsAvailability[o.GetTranslateKey()] && (int)o.GetEffectType() == 0;
		}

		internal bool _003CUpdateIconAbovePlayer_003Eb__601_0(Effect o)
		{
			return o is SpiritResistanceEffect;
		}

		internal bool _003CRpc_Give_Bomb_003Eb__604_0(Effect o)
		{
			return o is BombEffect;
		}

		internal bool _003CBombActiveChanged_003Eb__606_0(Effect o)
		{
			return o is FleeingEffect;
		}

		internal bool _003CBombActiveChanged_003Eb__606_1(Effect o)
		{
			return o is GlowingEffect;
		}

		internal bool _003CRpc_Ritualist_Ritual_003Eb__618_0(PlayerCustom o)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.PlayerController.IsWolf);
		}

		internal bool _003CRpc_Save_003Eb__636_0(Effect o)
		{
			return o is DyingEffect;
		}

		internal bool _003CFinishSurvivalistSave_003Eb__638_0(Effect o)
		{
			return o is DyingEffect;
		}

		internal bool _003CRpc_Wolf_Attack_003Eb__642_0(Effect o)
		{
			return o is AsleepEffect;
		}

		internal bool _003CRpc_Wolf_Attack_003Eb__642_1(Effect o)
		{
			return o is MoleClockEffect;
		}

		internal bool _003CRpc_Wolf_Attack_003Eb__642_2(PlayerCustom o)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Avenger && !NetworkBool.op_Implicit(o.PlayerController.IsDead);
		}

		internal bool _003CRpc_Wolf_Attack_003Eb__642_3(PlayerCustom o)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Shadow && !NetworkBool.op_Implicit(o.PlayerController.IsDead);
		}

		internal bool _003CRpc_Spirit_Attack_003Eb__644_0(Effect o)
		{
			return o is SpiritResistanceEffect;
		}

		internal bool _003CRpc_Spirit_Attack_003Eb__644_1(Effect o)
		{
			return o is SpiritResistanceEffect;
		}

		internal bool _003CRpc_Spirit_Attack_003Eb__644_2(Effect o)
		{
			return o is ParanoiaEffect;
		}

		internal bool _003CRpc_Spirit_Attack_003Eb__644_3(Effect o)
		{
			return o is FlatulenceEffect;
		}

		internal bool _003CGiveSecondaryRole_003Eb__677_0(Effect o)
		{
			return o is AuditionEffect || o is NightVision || o is StinkingEffect || o is HauntedEffect || o is FlatulenceEffect || o is DeafnessEffect;
		}

		internal bool _003CGiveSecondaryRole_003Eb__677_1(Accessory o)
		{
			return Plugin.CustomConfig.AccessoriesAvailability[ItemUtility.ItemToTranslateKey((Item)(object)o)];
		}

		internal bool _003CUpdatePrimaryRole_003Eb__690_0(PlayerController p)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			return (int)p.Role == 1;
		}

		internal bool _003CInitForGameStart_003Eb__691_0(Item o)
		{
			return (o is LockItem || o is PhasingDiamondItem || o is GrenadeItem) && Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)];
		}

		internal bool _003CInitForGameStart_003Eb__691_1(Accessory o)
		{
			return o is AccessorySpellbook;
		}

		internal bool _003CInitForGameStart_003Eb__691_2(Item o)
		{
			return o is MagicScrollItem;
		}

		internal bool _003CInitForGameStart_003Eb__691_3(Accessory o)
		{
			return Plugin.CustomConfig.AccessoriesAvailability[ItemUtility.ItemToTranslateKey((Item)(object)o)];
		}

		internal bool _003CInitForGameStart_003Eb__691_4(PlayerCustom o)
		{
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Warlock;
		}

		internal bool _003CInitForGameStart_003Eb__691_5(PlayerCustom o)
		{
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Bomber;
		}

		internal bool _003CInitForGameStart_003Eb__691_6(PlayerCustom o)
		{
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Saboteur;
		}

		internal bool _003CInitForGameStart_003Eb__691_8(Teleporter o)
		{
			return o.MapID == GameManager.Instance.MapID;
		}

		internal bool _003CUpdateVisible_003Eb__695_1(PlayerCustom o)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return o.PrimaryRolePower == PlayerPrimaryRolePower.Shadow && NetworkBool.op_Implicit(o.NewPrimaryRoleUniqueBool);
		}

		internal bool _003CUpdateVisible_003Eb__695_2(Effect o)
		{
			return o is VampireEffect;
		}

		internal bool _003CUpdateVisible_003Eb__695_3(Effect o)
		{
			return o is HauntedEffect;
		}

		internal bool _003CUpdateVisible_003Eb__695_5(RunemasterRune o)
		{
			return o.IsSelected;
		}

		internal bool _003CUpdateVisible_003Eb__695_6(Effect o)
		{
			return o is CamouflageEffect;
		}

		internal bool _003CUpdateMoveSpeed_003Eb__707_0(Effect o)
		{
			return o is ChasingEffect;
		}

		internal bool _003CFindRandomTeleporter_003Eb__710_0(Teleporter o)
		{
			return o.MapID == GameManager.Instance.MapID;
		}

		internal void _003CRpc_End_Game_003Eb__721_0(PlayerController pObj)
		{
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			pObj.CanMove = NetworkBool.op_Implicit(false);
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(pObj.Ref);
			if ((Object)(object)player != (Object)null)
			{
				player.PrimaryRoleActionTimer = TickTimer.None;
				player.PrimaryRolePowerCooldownTimer = TickTimer.None;
				player.SecondaryRolePowerCooldownTimer = TickTimer.None;
				player.TrapDisarmTimer = TickTimer.None;
				if (NetworkBool.op_Implicit(player.Dying))
				{
					player.Dying = NetworkBool.op_Implicit(false);
				}
			}
		}

		internal bool _003CRpc_End_Game_003Eb__721_1(PlayerCustom o)
		{
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Agent;
		}

		internal bool _003CRpc_End_Game_003Eb__721_2(PlayerCustom o)
		{
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Lover;
		}

		internal bool _003CRpc_End_Game_003Eb__721_3(PlayerCustom o)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Invalid comparison between Unknown and I4
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Lover && (int)o.PlayerController.Role == 1;
		}

		internal bool _003CRpc_End_Game_003Eb__721_6(PlayerCustom o)
		{
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Traitor;
		}

		internal bool _003CRpc_End_Game_003Eb__721_7(PlayerCustom o)
		{
			return o.NewPrimaryRole == PlayerNewPrimaryRole.Traitor;
		}

		internal PlayerController _003CRpc_End_Game_003Eb__721_8(PlayerCustom o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return PlayerRegistry.GetPlayer(o.Ref);
		}

		internal PlayerController _003CRpc_End_Game_003Eb__721_4(PlayerCustom o)
		{
			return o.PlayerController;
		}

		internal bool _003CRpc_End_Game_003Eb__721_5(PlayerCustom o)
		{
			return IsPrimaryRolePowerForEliteVillagers(o.InitialPower);
		}

		internal bool _003CRpc_End_Game_003Eb__721_9(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return o.PlayerData.ID == "76561198034021995";
		}

		internal bool _003CRpc_End_Game_003Eb__721_10(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return o.PlayerData.ID == "76561198045789440";
		}

		internal bool _003CRpc_End_Game_003Eb__721_11(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return o.PlayerData.ID == "76561199060053791";
		}

		internal bool _003CRpc_End_Game_003Eb__721_12(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return o.PlayerData.ID == "76561197973106144";
		}

		internal bool _003CRpc_End_Game_003Eb__721_13(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return PlayerRef.op_Implicit(o.Ref) >= 1000;
		}
	}

	public static Color NewPrimaryRoleVillageIdiotColor = new Color(0f, 1f, 0f, 1f);

	public static Color NewPrimaryRoleTraitorColor = new Color(1f, 0.5f, 0f, 1f);

	public static Color NewPrimaryRoleAgentColor = new Color(0.5f, 0f, 0.5f, 1f);

	public static Color NewPrimaryRoleSpyColor = new Color(0.75f, 0.25f, 0.75f, 1f);

	public static Color NewPrimaryRoleScientistColor = new Color(0.5f, 0.5f, 1f, 1f);

	public static Color NewPrimaryRoleLoverColor = new Color(1f, 0.5f, 1f, 1f);

	public static Color NewPrimaryRoleBeastColor = new Color(0.5f, 0.5f, 0.5f, 1f);

	public static Color NewPrimaryRoleMercenaryColor = new Color(0.5f, 0f, 0.5f, 1f);

	public static Color NewPrimaryRoleVoodooColor = new Color(0.65f, 0.15f, 1f, 1f);

	public static Color NewPrimaryRoleKidnapperColor = new Color(0f, 0.5f, 0.5f, 1f);

	public static Color NewPrimaryRoleCultistColor = new Color(0.75f, 0.55f, 0.25f, 1f);

	public static Color NewPrimaryRoleWolfRoleColor = new Color(1f, 0f, 0f, 1f);

	public static Color PrimaryRolePowerSpotterColor = new Color(0f, 1f, 0f);

	public static Color PrimaryRolePowerPurifierColor = new Color(0f, 1f, 0.5f);

	public static Color PlayerColorInListForGenericSoloRole = new Color(1f, 0f, 1f, 1f);

	public static Color PlayerColorInListForSpy = new Color(1f, 0f, 0f, 1f);

	public static Color PlayerColorInListForBeast = new Color(1f, 0f, 0f, 1f);

	public static Color PlayerColorInListForMercenary = new Color(1f, 0f, 0f, 1f);

	public static Color PlayerColorInListForVoodoo = new Color(0.65f, 0.15f, 1f, 1f);

	public static Color PlayerColorInListForKidnapper = new Color(1f, 0f, 0f, 1f);

	public static Color PlayerColorInListForLovers = new Color(1f, 0.5f, 1f, 1f);

	public static Color PlayerColorInListForInvestigator = new Color(1f, 1f, 0f, 1f);

	public static Color PlayerColorInListForDetective = new Color(1f, 1f, 0f, 1f);

	public static Color PlayerColorInListForNecromancer = new Color(0.6f, 0.15f, 1f, 1f);

	public static Color PlayerColorInListForWarlock = new Color(1f, 0f, 0f, 1f);

	public static Color PlayerColorInListForPredator = new Color(1f, 0f, 0f, 1f);

	public static Color PlayerColorInListForHost = new Color(0f, 1f, 0f, 1f);

	private static Color SkinColorZombieHuman = new Color(0.8f, 0f, 0.8f);

	private static Color SkinColorZombieWolf = new Color(0.5f, 0f, 0.5f);

	private static Color SkinColorPetrified = new Color(1f, 1f, 0f);

	private static Color SkinColorPoison = new Color(0f, 1f, 0f);

	public static Color BeastModelColor = new Color(1f, 0.4f, 0.4f, 1f);

	public static Color HealthVeryHigh = Color.green;

	public static Color HealthHigh = new Color(0.6f, 0.8f, 0f);

	public static Color HealthMedium = Color.yellow;

	public static Color HealthLow = new Color(1f, 0.5f, 0f);

	public static Color HealthCritical = Color.red;

	public static GameObject PlayerCustomColliderPrefab;

	public static GameObject StinkingParticleSystemPrefab;

	public static GameObject BombExplosionParticleSystemPrefab;

	public static GameObject DownedParticleSystemPrefab;

	public static GameObject PoacherMarkPrefab;

	public static GameObject ChaosParticleSystemPrefab;

	public static GameObject ExorcisedParticleSystemPrefab;

	public static GameObject PriestShieldParticleSystemPrefab;

	public static GameObject MegaFartParticleSystemPrefab;

	public static GameObject TruesightParticleSystemPrefab;

	public static GameObject KilledByCrystalBallGuessParticleSystemPrefab;

	public static GameObject MidasParticleSystemPrefab;

	public static GameObject VampireParticleSystemPrefab;

	public static GameObject SpeedParticleSystemPrefab;

	public static GameObject HauntedParticleSystemPrefab;

	public static GameObject AsleepParticleSystemPrefab;

	public static GameObject TrappedItemExplosionParticleSystemPrefab;

	public static GameObject MayorParticleSystemPrefab;

	public static GameObject TeleportParticleSystemPrefab;

	public static GameObject AlchemistTransformParticleSystemPrefab;

	public static GameObject MolotovBurnParticleSystemPrefab;

	public static GameObject PurifierBurnParticleSystemPrefab;

	public static GameObject BanishedParticleSystemPrefab;

	public static GameObject KidnapperKidnapParticleSystemPrefab;

	public static GameObject ConfusedParticleSystemPrefab;

	public static Shader RegularVillagerShader;

	public static Shader RegularWolfShader;

	public static Shader SeeThroughShaderHuman;

	public static Shader SeeThroughShaderWolf;

	public static Shader CamouflageLevel1Shader;

	public static Shader CamouflageLevel2Shader;

	public static Shader CamouflageLevel3Shader;

	public int? PrimaryRolePowerCooldownTimerTicksBeforeMeeting = null;

	public int? SecondaryRolePowerCooldownTimerTicksBeforeMeeting = null;

	private PlayerRef SurvivalistSaveTargetPlayerRef = PlayerRef.None;

	private PlayerController _playerController;

	public float? ModVersion = null;

	public GameObject PlayerCustomCollider;

	public GameObject StinkingParticleSystem;

	public GameObject DownedParticleSystem;

	public PlayerCustomAudioComponent CustomAudio;

	private MinimapPlayerComponent MinimapObject;

	public KnockbackComponent Knockback;

	public ForcedRotationComponent ForcedRotation;

	public GameObject ChaosParticleSystem;

	public GameObject ExorcisedParticleSystem;

	public GameObject PriestShieldParticleSystem;

	public GameObject TruesightParticleSystem;

	public GameObject MidasParticleSystemHandLeft;

	public GameObject MidasParticleSystemHandRight;

	public GameObject VampireParticleSystem;

	public GameObject SpeedParticleSystem;

	public GameObject HauntedParticleSystem;

	public GameObject AsleepParticleSystem;

	public GameObject BanishedParticleSystem;

	public GameObject MolotovBurnParticleSystem;

	public GameObject PurifierBurnParticleSystem;

	public GameObject ConfusedParticleSystem;

	public PlayerAstralSpiritComponent AstralSpirit;

	public PlayerSummonedSpiritComponent SummonedSpirit;

	public List<PlayerRef> PrimaryRolePowerPlayersList = new List<PlayerRef>();

	public List<PlayerSecondaryRole> ImitatorChoicesForToday = new List<PlayerSecondaryRole>();

	public List<MerchantOffer> CurrentMerchantOffers = new List<MerchantOffer>();

	public int MerchantDeliveryOfferIndex = 0;

	public List<PlayerDetectiveIntel> DetectiveIntelList = new List<PlayerDetectiveIntel>();

	public List<PlayerRef> MercenaryTargetsAlreadyHit = new List<PlayerRef>();

	public PlayerStats Stats;

	public PlayerPrimaryRolePower NonDraftLastGamePower = PlayerPrimaryRolePower.None;

	public bool StarvationDormant;

	public bool StarvationActive;

	public SleepingGasPlaced PlacedSleepingGas = null;

	public bool HasZombieColor = false;

	public PlayerPrimaryRolePower InitialPower = PlayerPrimaryRolePower.None;

	public bool AlreadyPossessed;

	public bool AlreadyAngeledToday;

	public int SecondsTransformedOrNearTransformedWolfToday;

	public Stopwatch WolfRecuperateStopwatch = new Stopwatch();

	public bool GainEmpoweredOnEachTransformation;

	public int CamouflageLevelForPovPlayer = 0;

	public PlayerLocalEachSecondTimerComponent PersonalComponent;

	public int LootCollectedTodayDuringDay = 0;

	public PlayerPetComponent CurrentPet = null;

	public bool Visible = true;

	public bool MoleWarningIssued = false;

	public bool DraftWantsSecondAgent = true;

	public bool DraftWantsSecondLover = true;

	public PlayerNewAnimationsComponent PlayerAnimations;

	private static PlayerCustom _local;

	public Trap TrapToDisarm;

	public static List<PlayerRef> PlayersWithSpecificColor = new List<PlayerRef>();

	public static List<PlayerPrimaryRolePower> AllJobs => Enum.GetValues(typeof(PlayerPrimaryRolePower)).Cast<PlayerPrimaryRolePower>().ToList();

	public static List<PlayerPrimaryRolePower> AllVillagerJobs
	{
		get
		{
			List<PlayerPrimaryRolePower> source = Enum.GetValues(typeof(PlayerPrimaryRolePower)).Cast<PlayerPrimaryRolePower>().ToList();
			return source.Where((PlayerPrimaryRolePower o) => IsPrimaryRolePowerForNormalVillagers(o)).ToList();
		}
	}

	public static List<PlayerPrimaryRolePower> AllWolfPowers
	{
		get
		{
			List<PlayerPrimaryRolePower> source = Enum.GetValues(typeof(PlayerPrimaryRolePower)).Cast<PlayerPrimaryRolePower>().ToList();
			return source.Where((PlayerPrimaryRolePower o) => IsPrimaryRolePowerForWolves(o)).ToList();
		}
	}

	[Networked(OnChanged = "SecondaryRoleChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe PlayerSecondaryRole SecondaryRole
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRole. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerSecondaryRole)(*base.Ptr);
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRole. Networked properties can only be accessed when Spawned() has been called.");
			}
			*base.Ptr = (int)value;
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool Stunned
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Stunned. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Stunned. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	[Networked(OnChanged = "SecondaryRoleFirstRemainingUsesChanged")]
	[NetworkedWeaved(2, 1)]
	public unsafe int SecondaryRoleFirstRemainingUses
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleFirstRemainingUsesChanged. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[2];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleFirstRemainingUsesChanged. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[2] = value;
		}
	}

	[Networked(OnChanged = "ColorIndexChanged")]
	[NetworkedWeaved(3, 1)]
	public unsafe int ColorIndex
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ColorIndex. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[3];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ColorIndex. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[3] = value;
		}
	}

	[Networked(OnChanged = "SubtleSabotagingChanged")]
	[NetworkedWeaved(4, 1)]
	public unsafe NetworkBool SubtleSabotaging
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SubtleSabotaging. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[4];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SubtleSabotaging. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 4, value);
		}
	}

	[Networked]
	[NetworkedWeaved(5, 1)]
	public unsafe NetworkBool Burning
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Burning. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[5];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Burning. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 5, value);
		}
	}

	[Networked]
	[NetworkedWeaved(6, 1)]
	public unsafe NetworkBool Sprinting
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Sprinting. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[6];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Sprinting. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 6, value);
		}
	}

	[Networked(OnChanged = "RevertingChanged")]
	[NetworkedWeaved(7, 1)]
	public unsafe NetworkBool Reverting
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Reverting. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[7];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Reverting. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 7, value);
		}
	}

	[Networked]
	[NetworkedWeaved(8, 1)]
	public unsafe NetworkBool Disoriented
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Disoriented. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[8];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Disoriented. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 8, value);
		}
	}

	[Networked]
	[NetworkedWeaved(9, 1)]
	public unsafe PlayerRef Ref
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Ref. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[9];
		}
		private set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Ref. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 9, value);
		}
	}

	[Networked]
	[NetworkedWeaved(10, 1)]
	public unsafe byte Index
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Index. Networked properties can only be accessed when Spawned() has been called.");
			}
			return ((byte*)base.Ptr)[(nint)10 * (nint)4];
		}
		private set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Index. Networked properties can only be accessed when Spawned() has been called.");
			}
			((sbyte*)base.Ptr)[(nint)10 * (nint)4] = (sbyte)value;
		}
	}

	[Networked(OnChanged = "NewPrimaryRoleChanged")]
	[NetworkedWeaved(11, 1)]
	public unsafe PlayerNewPrimaryRole NewPrimaryRole
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.NewPrimaryRole. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerNewPrimaryRole)base.Ptr[11];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.NewPrimaryRole. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[11] = (int)value;
		}
	}

	[NetworkedWeaved(12, 1)]
	public unsafe NetworkBool RoleDeathUniqueBool
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.RoleDeathUniqueBool. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[12];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.RoleDeathUniqueBool. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 12, value);
		}
	}

	[Networked(OnChanged = "UpdatePlayerVisibility")]
	[NetworkedWeaved(13, 1)]
	public unsafe NetworkBool Camouflage
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Camouflage. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[13];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Camouflage. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 13, value);
		}
	}

	[Networked(OnChanged = "DeafChanged")]
	[NetworkedWeaved(14, 1)]
	public unsafe NetworkBool Deaf
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Deaf. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[14];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Deaf. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 14, value);
		}
	}

	[Networked(OnChanged = "SoloRoleObjectiveCountChanged")]
	[NetworkedWeaved(15, 1)]
	public unsafe int SoloRoleObjectiveTarget
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SoloRoleObjectiveTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[15];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SoloRoleObjectiveTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[15] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(16, 1)]
	public unsafe NetworkBool CapturedByCultist
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CapturedByCultist. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[16];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CapturedByCultist. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 16, value);
		}
	}

	[Networked]
	[NetworkedWeaved(17, 1)]
	public unsafe NetworkBool Immune
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Immune. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[17];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Immune. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 17, value);
		}
	}

	[Networked(OnChanged = "SurvivalistSaveTimerChanged")]
	[NetworkedWeaved(18, 1)]
	public unsafe TickTimer SurvivalistSaveTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SurvivalistSaveTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[18];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SurvivalistSaveTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 18, value);
		}
	}

	[Networked(OnChanged = "BlindChanged")]
	[NetworkedWeaved(19, 1)]
	public unsafe NetworkBool Blind
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Blind. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[19];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Blind. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 19, value);
		}
	}

	[Networked(OnChanged = "IllusionTargetChanged")]
	[NetworkedWeaved(20, 1)]
	public unsafe PlayerRef IllusionTarget
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.IllusionTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[20];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.IllusionTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 20, value);
		}
	}

	[Networked(OnChanged = "PrimaryRoleActionTimerChanged")]
	[NetworkedWeaved(21, 1)]
	public unsafe TickTimer PrimaryRoleActionTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRoleActionTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[21];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRoleActionTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 21, value);
		}
	}

	[Networked(OnChanged = "SoloRoleObjectiveCountChanged")]
	[NetworkedWeaved(22, 1)]
	public unsafe int SoloRoleObjectiveCount
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SoloRoleObjectiveCount. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[22];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SoloRoleObjectiveCount. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[22] = value;
		}
	}

	[Networked(OnChanged = "PrimaryRoleTargetRefChanged")]
	[NetworkedWeaved(23, 1)]
	public unsafe PlayerRef PrimaryRoleTargetRef
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRoleTargetRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[23];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRoleTargetRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 23, value);
		}
	}

	[Networked(OnChanged = "UpdatePlayerVisibility")]
	[NetworkedWeaved(24, 1)]
	public unsafe NetworkBool Disappeared
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DisappearedChanged. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[24];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DisappearedChanged. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 24, value);
		}
	}

	[Networked(OnChanged = "PrimaryRolePowerRemainingUsesChanged")]
	[NetworkedWeaved(25, 1)]
	public unsafe int PrimaryRolePowerRemainingUses
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRolePowerRemainingUses. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[25];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRolePowerRemainingUses. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[25] = value;
		}
	}

	[Networked(OnChanged = "TrapDisarmTimerChanged")]
	[NetworkedWeaved(26, 1)]
	public unsafe TickTimer TrapDisarmTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.TrapDisarmTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[26];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.TrapDisarmTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 26, value);
		}
	}

	[Networked]
	[NetworkedWeaved(27, 1)]
	public unsafe TickTimer SecondaryRolePowerCooldownTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRolePowerCooldownTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[27];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRolePowerCooldownTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 27, value);
		}
	}

	[Networked(OnChanged = "PoliticianVictimAlltimeChanged")]
	[NetworkedWeaved(28, 1)]
	public unsafe NetworkBool PoliticianVictimAlltime
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PoliticianVictimAlltime. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[28];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PoliticianVictimAlltime. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 28, value);
		}
	}

	[Networked(OnChanged = "IsWolfPupChanged")]
	[NetworkedWeaved(29, 1)]
	public unsafe NetworkBool IsWolfPup
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.IsWolfPup. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[29];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.IsWolfPup. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 29, value);
		}
	}

	[Networked(OnChanged = "SecondaryRoleUniqueIntChanged")]
	[NetworkedWeaved(30, 1)]
	public unsafe int SecondaryRoleUniqueInt
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleUniqueInt. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[30];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleUniqueInt. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[30] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(31, 1)]
	public unsafe int PetIndex
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PetIndex. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[31];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PetIndex. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[31] = value;
		}
	}

	[Networked(OnChanged = "AsleepChanged")]
	[NetworkedWeaved(32, 1)]
	public unsafe NetworkBool Asleep
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Asleep. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[32];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Asleep. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 32, value);
		}
	}

	[Networked(OnChanged = "ParalyzedChanged")]
	[NetworkedWeaved(33, 1)]
	public unsafe NetworkBool Paralyzed
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ParalyzedChanged. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[33];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ParalyzedChanged. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 33, value);
		}
	}

	[Networked]
	[NetworkedWeaved(34, 1)]
	public unsafe int SleepStacks
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SleepStacks. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[34];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SleepStacks. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[34] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(35, 1)]
	public unsafe NetworkBool Midas
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Midas. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[35];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Midas. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 35, value);
		}
	}

	[Networked]
	[NetworkedWeaved(36, 1)]
	public unsafe NetworkBool Vampire
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Vampire. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[36];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Vampire. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 36, value);
		}
	}

	[Networked(OnChanged = "TinyChanged")]
	[NetworkedWeaved(37, 1)]
	public unsafe NetworkBool Tiny
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Tiny. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[37];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Tiny. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 37, value);
		}
	}

	[Networked(OnChanged = "ResurrectedChanged")]
	[NetworkedWeaved(38, 1)]
	public unsafe NetworkBool Resurrected
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Resurrected. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[38];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Resurrected. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 38, value);
		}
	}

	[Networked(OnChanged = "SecondaryRolePowerActiveChanged")]
	[NetworkedWeaved(39, 1)]
	public unsafe NetworkBool SecondaryRolePowerActive
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRolePowerActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[39];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRolePowerActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 39, value);
		}
	}

	[Networked(OnChanged = "SabotageTimerChanged")]
	[NetworkedWeaved(40, 1)]
	public unsafe TickTimer SabotageTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[40];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 40, value);
		}
	}

	[Networked]
	[NetworkedWeaved(41, 1)]
	public unsafe int SabotageObjectIndexTarget
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageObjectIndexTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[41];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageObjectIndexTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[41] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(42, 1)]
	public unsafe NetworkBool Disease
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Disease. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[42];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Disease. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 42, value);
		}
	}

	[Networked]
	[NetworkedWeaved(43, 1)]
	public unsafe NetworkBool NoDeadRole
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.NoDeadRole. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[43];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.NoDeadRole. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 43, value);
		}
	}

	[Networked]
	[NetworkedWeaved(44, 1)]
	public unsafe NetworkBool CurseDormant
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CurseDormant. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[44];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CurseDormant. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 44, value);
		}
	}

	[Networked]
	[NetworkedWeaved(45, 1)]
	public unsafe TickTimer CurseTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CurseTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[45];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CurseTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 45, value);
		}
	}

	[Networked(OnChanged = "SecondaryRoleActionTimerChanged")]
	[NetworkedWeaved(46, 1)]
	public unsafe TickTimer SecondaryRoleActionTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleActionTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[46];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleActionTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 46, value);
		}
	}

	[Networked(OnChanged = "SecondaryRoleTeleportDataChanged")]
	[NetworkedWeaved(47, 8)]
	public unsafe NetworkTeleportData SecondaryRoleTeleportData
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleTeleportData. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkTeleportData)base.Ptr[47];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleTeleportData. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 47, value);
		}
	}

	[Networked(OnChanged = "NewPrimaryRoleUniqueBoolChanged")]
	[NetworkedWeaved(55, 1)]
	public unsafe NetworkBool NewPrimaryRoleUniqueBool
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.NewPrimaryRoleUniqueBool. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[55];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.NewPrimaryRoleUniqueBool. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 55, value);
		}
	}

	[Networked]
	[NetworkedWeaved(56, 1)]
	public unsafe NetworkBool Undetected
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Undetected. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[56];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Undetected. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 56, value);
		}
	}

	[Networked(OnChanged = "PossessedChanged")]
	[NetworkedWeaved(57, 1)]
	public unsafe NetworkBool Possessed
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Possessed. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[57];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Possessed. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 57, value);
		}
	}

	[Networked]
	[NetworkedWeaved(58, 1)]
	public unsafe TickTimer HauntedTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.HauntedTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[58];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.HauntedTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 58, value);
		}
	}

	[Networked]
	[NetworkedWeaved(59, 1)]
	public unsafe TickTimer ChaosTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ChaosTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[59];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ChaosTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 59, value);
		}
	}

	[Networked]
	[NetworkedWeaved(60, 1)]
	public unsafe NetworkBool Nearsighted
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Nearsighted. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[60];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Nearsighted. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 60, value);
		}
	}

	[Networked]
	[NetworkedWeaved(61, 1)]
	public unsafe NetworkBool Assassin
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Assassin. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[61];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Assassin. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 61, value);
		}
	}

	[Networked(OnChanged = "DownedChanged")]
	[NetworkedWeaved(62, 1)]
	public unsafe NetworkBool Downed
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Downed. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[62];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Downed. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 62, value);
		}
	}

	[Networked]
	[NetworkedWeaved(63, 1)]
	public unsafe NetworkBool Wounded
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Wounded. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[63];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Wounded. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 63, value);
		}
	}

	[Networked]
	[NetworkedWeaved(64, 1)]
	public unsafe NetworkBool Poison
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Poison. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[64];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Poison. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 64, value);
		}
	}

	[Networked(OnChanged = "PrimaryRolePowerChanged")]
	[NetworkedWeaved(65, 1)]
	public unsafe PlayerPrimaryRolePower PrimaryRolePower
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRolePowerChanged. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerPrimaryRolePower)base.Ptr[65];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRolePowerChanged. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[65] = (int)value;
		}
	}

	[Networked]
	[NetworkedWeaved(66, 1)]
	public unsafe NetworkBool Empowered
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Empowered. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[66];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Empowered. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 66, value);
		}
	}

	[Networked(OnChanged = "PetrifiedChanged")]
	[NetworkedWeaved(67, 1)]
	public unsafe NetworkBool Petrified
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Petrified. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[67];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Petrified. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 67, value);
		}
	}

	[Networked]
	[NetworkedWeaved(68, 1)]
	public unsafe PlayerRef Exorciser
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Exorciser. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[68];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Exorciser. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 68, value);
		}
	}

	[Networked(OnChanged = "ExorcisedChanged")]
	[NetworkedWeaved(69, 1)]
	public unsafe NetworkBool Exorcised
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Exorcised. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[69];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Exorcised. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 69, value);
		}
	}

	[Networked(OnChanged = "QuickSabotagingChanged")]
	[NetworkedWeaved(70, 1)]
	public unsafe NetworkBool QuickSabotaging
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.QuickSabotaging. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[70];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.QuickSabotaging. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 70, value);
		}
	}

	[Networked(OnChanged = "StinkingChanged")]
	[NetworkedWeaved(71, 1)]
	public unsafe NetworkBool Stinking
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Stinking. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[71];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Stinking. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 71, value);
		}
	}

	[Networked]
	[NetworkedWeaved(72, 1)]
	public unsafe NetworkBool Nauseated
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Nauseated. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[72];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Nauseated. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 72, value);
		}
	}

	[Networked]
	[NetworkedWeaved(73, 1)]
	public unsafe NetworkBool Mute
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Mute. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[73];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Mute. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 73, value);
		}
	}

	[Networked]
	[NetworkedWeaved(74, 1)]
	public unsafe NetworkBool SurvivalistBuff
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SurvivalistBuff. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[74];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SurvivalistBuff. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 74, value);
		}
	}

	[Networked(OnChanged = "PhasingChanged")]
	[NetworkedWeaved(75, 1)]
	public unsafe NetworkBool Phasing
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Phasing. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[75];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Phasing. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 75, value);
		}
	}

	[Networked(OnChanged = "DyingChanged")]
	[NetworkedWeaved(76, 1)]
	public unsafe NetworkBool Dying
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Dying. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[76];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Dying. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 76, value);
		}
	}

	[Networked]
	[NetworkedWeaved(77, 1)]
	public unsafe NetworkBool Scavenged
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Scavenged. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[77];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Scavenged. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 77, value);
		}
	}

	[Networked]
	[NetworkedWeaved(78, 1)]
	public unsafe PlayerRef SecondaryRoleTargetRef
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleTargetRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[78];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SecondaryRoleTargetRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 78, value);
		}
	}

	[Networked]
	[NetworkedWeaved(79, 1)]
	public unsafe TickTimer PrimaryRolePowerCooldownTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRolePowerCooldownTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[79];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRolePowerCooldownTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 79, value);
		}
	}

	[Networked(OnChanged = "UpdatePlayerVisibility")]
	[NetworkedWeaved(80, 1)]
	public unsafe NetworkBool BombDormant
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Scavenged. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[80];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Scavenged. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 80, value);
		}
	}

	[Networked(OnChanged = "BombActiveChanged")]
	[NetworkedWeaved(81, 1)]
	public unsafe NetworkBool BombActive
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BombActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[81];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BombActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 81, value);
		}
	}

	[Networked]
	[NetworkedWeaved(82, 1)]
	public unsafe NetworkBool Panic
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Panic. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[82];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Panic. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 82, value);
		}
	}

	[Networked]
	[NetworkedWeaved(83, 1)]
	public unsafe NetworkBool Fleeing
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Fleeing. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[83];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Fleeing. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 83, value);
		}
	}

	[Networked]
	[NetworkedWeaved(84, 1)]
	public unsafe TickTimer BombTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BombDormantTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[84];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BombDormantTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 84, value);
		}
	}

	[Networked(OnChanged = "PrimaryRolePowerCurrentMaterialsChanged")]
	[NetworkedWeaved(85, 1)]
	public unsafe int PrimaryRolePowerCurrentMaterials
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRolePowerCurrentMaterials. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[85];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PrimaryRolePowerCurrentMaterials. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[85] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(86, 1)]
	public unsafe NetworkBool ProtectedPriest
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ProtectedPriest. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[86];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ProtectedPriest. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 86, value);
		}
	}

	[Networked(OnChanged = "PoacherMarkChanged")]
	[NetworkedWeaved(87, 1)]
	public unsafe NetworkBool PoacherMark
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PoacherMark. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[87];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PoacherMark. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 87, value);
		}
	}

	[Networked(OnChanged = "AngelChanged")]
	[NetworkedWeaved(88, 1)]
	public unsafe NetworkBool Angel
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Angel. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[88];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Angel. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 88, value);
		}
	}

	[Networked]
	[NetworkedWeaved(89, 1)]
	public unsafe NetworkBool Sleepy
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Sleepy. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[89];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Sleepy. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 89, value);
		}
	}

	[Networked]
	[NetworkedWeaved(90, 1)]
	public unsafe NetworkBool Predator
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Predator. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[90];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Predator. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 90, value);
		}
	}

	[Networked]
	[NetworkedWeaved(91, 1)]
	public unsafe NetworkBool BeastMark
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BeastMark. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[91];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BeastMark. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 91, value);
		}
	}

	[Networked]
	[NetworkedWeaved(92, 1)]
	public unsafe NetworkBool DeceiverTrickThisMeeting
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DeceiverTrickThisMeeting. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[92];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DeceiverTrickThisMeeting. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 92, value);
		}
	}

	[Networked]
	[NetworkedWeaved(93, 1)]
	public unsafe NetworkBool DeceiverTrickAllTime
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DeceiverTrickAllTime. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[93];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DeceiverTrickAllTime. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 93, value);
		}
	}

	[Networked]
	[NetworkedWeaved(94, 1)]
	public unsafe int DraftPosition
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DraftPosition. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[94];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DraftPosition. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[94] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(95, 1)]
	public unsafe NetworkBool Sneaky
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Sneaky. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[95];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Sneaky. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 95, value);
		}
	}

	[Networked]
	[NetworkedWeaved(96, 1)]
	public unsafe NetworkBool IsImitator
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.IsImitator. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[96];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.IsImitator. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 96, value);
		}
	}

	[Networked(OnChanged = "UpdateOthersVisibility")]
	[NetworkedWeaved(97, 1)]
	public unsafe NetworkBool Clairvoyance
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Clairvoyance. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[97];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Clairvoyance. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 97, value);
		}
	}

	[Networked(OnChanged = "ConfusedChanged")]
	[NetworkedWeaved(98, 1)]
	public unsafe NetworkBool Confused
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Confused. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[98];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Confused. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 98, value);
		}
	}

	[Networked]
	[NetworkedWeaved(99, 1)]
	public unsafe PlayerRef MayorVoteTarget
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.MayorVoteTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[99];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.MayorVoteTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 99, value);
		}
	}

	[Networked]
	[NetworkedWeaved(100, 1)]
	public unsafe float ForcedRotationY
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ForcedRotationY. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)base.Ptr[100] * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.ForcedRotationY. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr + 100, 999.99994f, value);
		}
	}

	[Networked(OnChanged = "UpdateOthersVisibility")]
	[NetworkedWeaved(101, 1)]
	public unsafe NetworkBool Isolation
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Isolation. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[101];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Isolation. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 101, value);
		}
	}

	[Networked]
	[NetworkedWeaved(109, 1)]
	public unsafe TickTimer LootCorpseTimer
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.LootCorpseTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[109];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.LootCorpseTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 109, value);
		}
	}

	[Networked]
	[NetworkedWeaved(110, 1)]
	public unsafe NetworkBool Portal
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Portal. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[110];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Portal. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 110, value);
		}
	}

	[Networked]
	[NetworkedWeaved(111, 1)]
	public unsafe NetworkBool Weakened
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Weakened. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[111];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Weakened. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 111, value);
		}
	}

	[Networked]
	[NetworkedWeaved(112, 1)]
	public unsafe int RepulsionStacks
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.RepulsionStacks. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[112];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.RepulsionStacks. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[112] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(113, 1)]
	public unsafe NetworkBool Banished
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Banished. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[113];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Banished. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 113, value);
		}
	}

	[Networked]
	[NetworkedWeaved(114, 1)]
	public unsafe NetworkBool TournamentWinner
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.TournamentWinner. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[114];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.TournamentWinner. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 114, value);
		}
	}

	[Networked]
	[NetworkedWeaved(115, 1)]
	public unsafe NetworkBool Energized
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Energized. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[115];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Energized. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 115, value);
		}
	}

	[Networked]
	[NetworkedWeaved(116, 1)]
	public unsafe NetworkBool DiedFromNotBeingSaved
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DiedFromNotBeingSaved. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[116];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.DiedFromNotBeingSaved. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 116, value);
		}
	}

	[Networked]
	[NetworkedWeaved(117, 1)]
	public unsafe NetworkBool Resilience
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Resilience. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[117];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Resilience. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 117, value);
		}
	}

	[Networked]
	[NetworkedWeaved(118, 1)]
	public unsafe NetworkBool Escaping
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Escaping. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[118];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Escaping. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 118, value);
		}
	}

	[Networked]
	[NetworkedWeaved(119, 1)]
	public unsafe NetworkBool Recuperating
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Recuperating. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[119];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Recuperating. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 119, value);
		}
	}

	[Networked]
	[NetworkedWeaved(120, 1)]
	public unsafe NetworkBool Repulsion
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Repulsion. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[120];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Repulsion. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 120, value);
		}
	}

	[Networked]
	[NetworkedWeaved(121, 1)]
	public unsafe NetworkBool Tenacity
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Tenacity. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[121];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Tenacity. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 121, value);
		}
	}

	[Networked]
	[NetworkedWeaved(122, 1)]
	public unsafe NetworkBool Hubris
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Hubris. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[122];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Hubris. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 122, value);
		}
	}

	[Networked]
	[NetworkedWeaved(123, 2)]
	public unsafe Accessory Accessory
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerController.Accessory. Networked properties can only be accessed when Spawned() has been called.");
			}
			NetworkBehaviour val = null;
			ReadWriteUtilsForWeaver.VerifyRawNetworkUnwrap<Accessory>(NetworkBehaviour.NetworkDeserialize(((SimulationBehaviour)this).Runner, (byte*)base.Ptr + (nint)123 * (nint)4, ref val), 8);
			return (Accessory)(object)val;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerController.Accessory. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.VerifyRawNetworkWrap<Accessory>(NetworkBehaviour.NetworkSerialize(((SimulationBehaviour)this).Runner, (NetworkBehaviour)(object)value, (byte*)base.Ptr + (nint)123 * (nint)4), 8);
		}
	}

	[Networked]
	[NetworkedWeaved(125, 1)]
	public unsafe NetworkBool Spotter
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Spotter. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[125];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Spotter. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 125, value);
		}
	}

	[Networked]
	[NetworkedWeaved(126, 1)]
	public unsafe NetworkBool PurifierBurn
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PurifierBurn. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[126];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.PurifierBurn. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 126, value);
		}
	}

	[Networked]
	[NetworkedWeaved(127, 1)]
	public unsafe PlayerRef LootCorpseTarget
	{
		get
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.LootCorpseTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[127];
		}
		set
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.LootCorpseTarget. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 127, value);
		}
	}

	[Networked]
	[NetworkedWeaved(128, 1)]
	public unsafe NetworkBool Tracked
	{
		get
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Tracked. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[128];
		}
		set
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Tracked. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 128, value);
		}
	}

	[Networked(OnChanged = "KidnappedChanged")]
	[NetworkedWeaved(129, 1)]
	public unsafe NetworkBool Kidnapped
	{
		get
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Kidnapped. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[129];
		}
		set
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Kidnapped. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 129, value);
		}
	}

	[Networked(OnChanged = "UpdatePlayerVisibility")]
	[NetworkedWeaved(130, 1)]
	public unsafe NetworkBool Hidden
	{
		get
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Hidden. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[130];
		}
		set
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Hidden. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 130, value);
		}
	}

	[Networked(OnChanged = "UpdatePlayerVisibility")]
	[NetworkedWeaved(131, 1)]
	public unsafe NetworkBool Parasite
	{
		get
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Parasite. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[131];
		}
		set
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Parasite. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 131, value);
		}
	}

	[Networked]
	[NetworkedWeaved(132, 1)]
	public unsafe float SoloRoleHalfDayProgress
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SoloRoleHalfDayProgress. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)base.Ptr[132] * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SoloRoleHalfDayProgress. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr + 132, 999.99994f, value);
		}
	}

	public PlayerController PlayerController
	{
		get
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_026e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Unknown result type (might be due to invalid IL or missing references)
			//IL_0280: Unknown result type (might be due to invalid IL or missing references)
			//IL_0287: Unknown result type (might be due to invalid IL or missing references)
			//IL_028e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0295: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0304: Unknown result type (might be due to invalid IL or missing references)
			//IL_030b: Unknown result type (might be due to invalid IL or missing references)
			//IL_035a: Unknown result type (might be due to invalid IL or missing references)
			//IL_035f: Unknown result type (might be due to invalid IL or missing references)
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0373: Unknown result type (might be due to invalid IL or missing references)
			//IL_037a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0381: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0400: Unknown result type (might be due to invalid IL or missing references)
			//IL_040d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0414: Unknown result type (might be due to invalid IL or missing references)
			//IL_041b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0422: Unknown result type (might be due to invalid IL or missing references)
			//IL_0492: Unknown result type (might be due to invalid IL or missing references)
			//IL_0497: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04de: Unknown result type (might be due to invalid IL or missing references)
			//IL_054e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0553: Unknown result type (might be due to invalid IL or missing references)
			//IL_0560: Unknown result type (might be due to invalid IL or missing references)
			//IL_0567: Unknown result type (might be due to invalid IL or missing references)
			//IL_056e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0575: Unknown result type (might be due to invalid IL or missing references)
			//IL_059a: Unknown result type (might be due to invalid IL or missing references)
			//IL_060a: Unknown result type (might be due to invalid IL or missing references)
			//IL_060f: Unknown result type (might be due to invalid IL or missing references)
			//IL_061c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0623: Unknown result type (might be due to invalid IL or missing references)
			//IL_0630: Unknown result type (might be due to invalid IL or missing references)
			//IL_0637: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_071d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0722: Unknown result type (might be due to invalid IL or missing references)
			//IL_072f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0736: Unknown result type (might be due to invalid IL or missing references)
			//IL_073d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0744: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0836: Unknown result type (might be due to invalid IL or missing references)
			//IL_083b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0848: Unknown result type (might be due to invalid IL or missing references)
			//IL_084f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0856: Unknown result type (might be due to invalid IL or missing references)
			//IL_085d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)_playerController == (Object)null)
			{
				_playerController = PlayerRegistry.GetPlayer(Ref);
				if ((Object)(object)_playerController != (Object)null)
				{
					PlayerCustomCollider = Object.Instantiate<GameObject>(PlayerCustomColliderPrefab, ((Component)PlayerController).transform);
					PlayerCustomCollider.SetActive(false);
					StinkingParticleSystem = Object.Instantiate<GameObject>(StinkingParticleSystemPrefab, ((Component)PlayerController).transform);
					StinkingParticleSystem.SetActive(true);
					StinkingParticleSystem.GetComponent<ParticleSystem>().Stop();
					((Component)_playerController).gameObject.AddComponent<PlayerBombTickingComponent>();
					((Component)_playerController).gameObject.AddComponent<PlayerBombIconComponent>();
					((Component)_playerController).gameObject.AddComponent<PlayerSurvivalistHeartbeatComponent>();
					DownedParticleSystem = Object.Instantiate<GameObject>(DownedParticleSystemPrefab, ((Component)PlayerController).transform);
					DownedParticleSystem.SetActive(true);
					ParticleSystem[] componentsInChildren = DownedParticleSystem.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val in componentsInChildren)
					{
						val.Stop();
					}
					((Component)_playerController).gameObject.AddComponent<PlayerPoacherMarkComponent>();
					((Component)_playerController).gameObject.AddComponent<PlayerMercenaryTargetIconComponent>();
					((Component)_playerController).gameObject.AddComponent<PlayerHeldItemComponent>();
					PlayerPredatorComponent playerPredatorComponent = ((Component)_playerController).gameObject.AddComponent<PlayerPredatorComponent>();
					((Behaviour)playerPredatorComponent).enabled = false;
					((Component)_playerController).gameObject.AddComponent<PlayerAngelIconComponent>();
					((Component)_playerController).gameObject.AddComponent<PlayerResurrectedComponent>();
					((Component)_playerController).gameObject.AddComponent<PlayerSpotterLightComponent>();
					((Component)_playerController).gameObject.AddComponent<PlayerDyingComponent>();
					if (_playerController.Ref == PlayerController.Local.Ref)
					{
						PersonalComponent = ((Component)_playerController).gameObject.AddComponent<PlayerLocalEachSecondTimerComponent>();
						PersonalComponent.Init(this);
					}
					PlayerTargetArrowComponent playerTargetArrowComponent = ((Component)_playerController).gameObject.AddComponent<PlayerTargetArrowComponent>();
					playerTargetArrowComponent.Init(_playerController, this);
					VoiceSpeaker componentInChildren = ((Component)_playerController).GetComponentInChildren<VoiceSpeaker>();
					CustomAudio = ((Component)componentInChildren).gameObject.AddComponent<PlayerCustomAudioComponent>();
					CustomAudio.Init(this);
					ChaosParticleSystem = Object.Instantiate<GameObject>(ChaosParticleSystemPrefab, ((Component)PlayerController).transform);
					Vector3 position = ChaosParticleSystem.transform.position;
					ChaosParticleSystem.transform.position = new Vector3(position.x, position.y, position.z);
					ChaosParticleSystem.SetActive(true);
					ChaosParticleSystem.GetComponent<ParticleSystem>().Stop();
					ExorcisedParticleSystem = Object.Instantiate<GameObject>(ExorcisedParticleSystemPrefab, ((Component)PlayerController).transform);
					position = ExorcisedParticleSystem.transform.position;
					ExorcisedParticleSystem.transform.position = new Vector3(position.x, position.y, position.z);
					ExorcisedParticleSystem.SetActive(true);
					ExorcisedParticleSystem.GetComponent<ParticleSystem>().Stop();
					PriestShieldParticleSystem = Object.Instantiate<GameObject>(PriestShieldParticleSystemPrefab, ((Component)PlayerController).transform);
					position = PriestShieldParticleSystem.transform.position;
					PriestShieldParticleSystem.transform.position = new Vector3(position.x, position.y, position.z);
					PriestShieldParticleSystem.SetActive(true);
					ParticleSystem[] componentsInChildren2 = PriestShieldParticleSystem.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val2 in componentsInChildren2)
					{
						val2.Stop();
					}
					TruesightParticleSystem = Object.Instantiate<GameObject>(TruesightParticleSystemPrefab, PlayerController.hatsContainer.transform.parent);
					position = TruesightParticleSystem.transform.position;
					TruesightParticleSystem.transform.position = new Vector3(position.x, position.y, position.z);
					TruesightParticleSystem.SetActive(true);
					ParticleSystem[] componentsInChildren3 = TruesightParticleSystem.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val3 in componentsInChildren3)
					{
						val3.Stop();
					}
					MidasParticleSystemHandLeft = Object.Instantiate<GameObject>(MidasParticleSystemPrefab, PlayerController.FindVillagerHandLeft());
					position = MidasParticleSystemHandLeft.transform.position;
					MidasParticleSystemHandLeft.transform.position = new Vector3(position.x, position.y, position.z);
					MidasParticleSystemHandLeft.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
					MidasParticleSystemHandLeft.SetActive(true);
					ParticleSystem[] componentsInChildren4 = MidasParticleSystemHandLeft.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val4 in componentsInChildren4)
					{
						val4.Stop();
					}
					MidasParticleSystemHandRight = Object.Instantiate<GameObject>(MidasParticleSystemPrefab, PlayerController.FindVillagerHandRight());
					position = MidasParticleSystemHandRight.transform.position;
					MidasParticleSystemHandRight.transform.position = new Vector3(position.x, position.y, position.z);
					MidasParticleSystemHandRight.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
					MidasParticleSystemHandRight.SetActive(true);
					ParticleSystem[] componentsInChildren5 = MidasParticleSystemHandRight.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val5 in componentsInChildren5)
					{
						val5.Stop();
					}
					VampireParticleSystem = Object.Instantiate<GameObject>(VampireParticleSystemPrefab, ((Component)PlayerController).transform);
					position = VampireParticleSystem.transform.position;
					VampireParticleSystem.transform.position = new Vector3(position.x, position.y + 0.4f, position.z);
					VampireParticleSystem.SetActive(true);
					ParticleSystem[] componentsInChildren6 = VampireParticleSystem.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val6 in componentsInChildren6)
					{
						val6.Stop();
					}
					SpeedParticleSystem = Object.Instantiate<GameObject>(SpeedParticleSystemPrefab, ((Component)PlayerController).transform);
					position = SpeedParticleSystem.transform.position;
					SpeedParticleSystem.transform.position = new Vector3(position.x, position.y, position.z);
					SpeedParticleSystem.SetActive(true);
					SpeedParticleSystem.GetComponent<ParticleSystem>().Stop();
					HauntedParticleSystem = Object.Instantiate<GameObject>(HauntedParticleSystemPrefab, ((Component)PlayerController).transform);
					position = HauntedParticleSystem.transform.position;
					HauntedParticleSystem.transform.position = new Vector3(position.x, position.y, position.z);
					HauntedParticleSystem.SetActive(true);
					ParticleSystem[] componentsInChildren7 = HauntedParticleSystem.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val7 in componentsInChildren7)
					{
						val7.Stop();
					}
					AsleepParticleSystem = Object.Instantiate<GameObject>(AsleepParticleSystemPrefab, ((Component)PlayerController).transform);
					position = AsleepParticleSystem.transform.position;
					AsleepParticleSystem.transform.position = new Vector3(position.x - 0.25f, position.y - 1.5f, position.z);
					AsleepParticleSystem.SetActive(true);
					AsleepParticleSystem.GetComponent<ParticleSystem>().Stop();
					BanishedParticleSystem = Object.Instantiate<GameObject>(BanishedParticleSystemPrefab, ((Component)PlayerController).transform);
					position = BanishedParticleSystem.transform.position;
					BanishedParticleSystem.transform.position = new Vector3(position.x, position.y, position.z);
					BanishedParticleSystem.SetActive(true);
					BanishedParticleSystem.GetComponent<ParticleSystem>().Stop();
					ParticleSystem[] componentsInChildren8 = BanishedParticleSystem.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val8 in componentsInChildren8)
					{
						val8.Stop();
					}
					MolotovBurnParticleSystem = Object.Instantiate<GameObject>(MolotovBurnParticleSystemPrefab, ((Component)PlayerController).transform);
					MolotovBurnParticleSystem.SetActive(true);
					MolotovBurnParticleSystem.GetComponent<ParticleSystem>().Stop();
					PurifierBurnParticleSystem = Object.Instantiate<GameObject>(PurifierBurnParticleSystemPrefab, ((Component)PlayerController).transform);
					PurifierBurnParticleSystem.SetActive(true);
					PurifierBurnParticleSystem.GetComponent<ParticleSystem>().Stop();
					ConfusedParticleSystem = Object.Instantiate<GameObject>(ConfusedParticleSystemPrefab, ((Component)PlayerController).transform);
					ConfusedParticleSystem.SetActive(true);
					ParticleSystem[] componentsInChildren9 = ConfusedParticleSystem.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem val9 in componentsInChildren9)
					{
						val9.Stop();
						val9.Clear();
					}
					Knockback = ((Component)_playerController).gameObject.AddComponent<KnockbackComponent>();
					ForcedRotation = ((Component)_playerController).gameObject.AddComponent<ForcedRotationComponent>();
					ForcedRotation.SetPlayer(this);
					if (((SimulationBehaviour)_playerController).Runner.IsServer)
					{
						UIManager.ModInstallationPanel.AddOrUpdatePlayer(Ref);
						((Component)_playerController.CharacterMovementHandler).gameObject.AddComponent<GravityComponent>();
					}
					PlayerAnimations = ((Component)_playerController).gameObject.AddComponent<PlayerNewAnimationsComponent>();
					PlayerGlowingChangesComponent playerGlowingChangesComponent = ((Component)_playerController).gameObject.AddComponent<PlayerGlowingChangesComponent>();
					playerGlowingChangesComponent.Init(this, _playerController.PlayerEffectManager, Traverse.Create((object)_playerController.PlayerEffectManager).Field<Light>("glowingLight").Value);
					((Component)_playerController).gameObject.AddComponent<PlayerHeartSeethroughComponent>();
					if (PlayerRef.op_Implicit(Ref) >= 1000)
					{
						PetIndex = Random.RandomRangeInt(0, Plugin.PetNames.Count);
					}
				}
			}
			return _playerController;
		}
	}

	public static PlayerCustom Local
	{
		get
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)_local == (Object)null && (Object)(object)PlayerController.Local != (Object)null)
			{
				LycansUtility.DebugLog("BUG2: set PlayerCustom.Local");
				_local = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			}
			return _local;
		}
	}

	public bool IsCurrentlyPlayedOrObserved => (!NetworkBool.op_Implicit(PlayerController.Local.IsDead) && PlayerController.Local.Ref == Ref) || (NetworkBool.op_Implicit(PlayerController.Local.IsDead) && PlayerController.Local.LocalCameraHandler.PovPlayer.Ref == Ref);

	public bool IsOutOfTheWorld => NetworkBool.op_Implicit(Possessed) || NetworkBool.op_Implicit(Kidnapped) || (NetworkBool.op_Implicit(CultistManager.Instance.CultistActive) && NewPrimaryRole == PlayerNewPrimaryRole.Cultist);

	public PowerMaterialsInfo? PowerMaterialsInfo => BalancingValues.GetMaterialsInfoForPrimaryRolePower(PrimaryRolePower);

	public int? SecondaryRolePowerCastTime => BalancingValues.GetCastTimeForSecondaryRole(SecondaryRole);

	public int? PrimaryRolePowerCooldown => BalancingValues.GetCooldownForPrimaryRolePower(PrimaryRolePower, GameManager.Instance.DayDuration + GameManager.Instance.NightDuration);

	public int? SecondaryRolePowerCooldown
	{
		get
		{
			if (SecondaryRole == PlayerSecondaryRole.BothBlueMage)
			{
				return BalancingValues.GetModifiedEffectData(EffectManager.GetEffect(SecondaryRoleUniqueInt)).BlueMageCooldownSeconds;
			}
			if (SecondaryRole == PlayerSecondaryRole.BothTinkerer)
			{
				return ((Object)(object)Accessory != (Object)null) ? new int?(Accessory.TinkererPowerCooldown) : ((int?)null);
			}
			return BalancingValues.GetCooldownForSecondaryRole(SecondaryRole, GameManager.Instance.DayDuration + GameManager.Instance.NightDuration);
		}
	}

	public int? SecondaryRolePowerSecondCooldown
	{
		get
		{
			PlayerSecondaryRole secondaryRole = SecondaryRole;
			PlayerSecondaryRole playerSecondaryRole = secondaryRole;
			if (playerSecondaryRole == PlayerSecondaryRole.BothScribe || playerSecondaryRole == PlayerSecondaryRole.BothForger)
			{
				return BalancingValues.GetSecondCooldownForSecondaryRole(SecondaryRole, GameManager.Instance.DayDuration + GameManager.Instance.NightDuration);
			}
			return null;
		}
	}

	public Color WolfColor
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && NewPrimaryRole == PlayerNewPrimaryRole.Beast)
			{
				return BeastModelColor;
			}
			if (HasZombieColor)
			{
				return SkinColorZombieWolf;
			}
			return Color.black;
		}
	}

	public bool CanPerformActions
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				int result;
				if (!NetworkBool.op_Implicit(PlayerController.IsDead) && !NetworkBool.op_Implicit(Dying) && !NetworkBool.op_Implicit(Asleep) && !NetworkBool.op_Implicit(Petrified) && !NetworkBool.op_Implicit(Downed) && !NetworkBool.op_Implicit(Tiny) && !IsOutOfTheWorld && !NetworkBool.op_Implicit(Banished) && !NetworkBool.op_Implicit(CapturedByCultist) && !NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.Giant))
				{
					if ((Object)(object)PlayerController.Item != (Object)null)
					{
						TickTimer val = PlayerController.Item.TriggerTimer;
						if (!((TickTimer)(ref val)).IsRunning)
						{
							val = PlayerController.Item.AnimationTimer;
							result = ((!((TickTimer)(ref val)).IsRunning) ? 1 : 0);
						}
						else
						{
							result = 0;
						}
					}
					else
					{
						result = 1;
					}
				}
				else
				{
					result = 0;
				}
				return (byte)result != 0;
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError((object)("CanPerformActions exception: " + ex));
				return true;
			}
		}
	}

	public static NewPrimaryRoleType GetPrimaryRoleTarget(PlayerNewPrimaryRole newPrimaryRole)
	{
		switch (newPrimaryRole)
		{
		case PlayerNewPrimaryRole.VillageIdiot:
		case PlayerNewPrimaryRole.Agent:
		case PlayerNewPrimaryRole.Spy:
		case PlayerNewPrimaryRole.Scientist:
		case PlayerNewPrimaryRole.Lover:
		case PlayerNewPrimaryRole.Beast:
		case PlayerNewPrimaryRole.Voodoo:
		case PlayerNewPrimaryRole.Mercenary:
		case PlayerNewPrimaryRole.Kidnapper:
		case PlayerNewPrimaryRole.Cultist:
		case PlayerNewPrimaryRole.Zombie:
			return NewPrimaryRoleType.SoloRole;
		case PlayerNewPrimaryRole.Traitor:
			return NewPrimaryRoleType.Traitor;
		default:
			return NewPrimaryRoleType.SoloRole;
		}
	}

	public static string GetNewPrimaryRoleString(PlayerCustom playerCustom)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Invalid comparison between Unknown and I4
		string text = GetNewPrimaryRoleString(playerCustom.NewPrimaryRole);
		if (playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Lover)
		{
			text = (((int)playerCustom.PlayerController.Role != 1) ? (text + "_VILLAGER") : (text + "_WOLF"));
		}
		return text;
	}

	public static string GetNewPrimaryRoleString(PlayerNewPrimaryRole newPrimaryRole)
	{
		return newPrimaryRole switch
		{
			PlayerNewPrimaryRole.VillageIdiot => "VILLAGE_IDIOT", 
			PlayerNewPrimaryRole.Agent => "AGENT", 
			PlayerNewPrimaryRole.Spy => "SPY", 
			PlayerNewPrimaryRole.Scientist => "SCIENTIST", 
			PlayerNewPrimaryRole.Lover => "LOVER", 
			PlayerNewPrimaryRole.Beast => "BEAST", 
			PlayerNewPrimaryRole.Mercenary => "MERCENARY", 
			PlayerNewPrimaryRole.Voodoo => "VOODOO", 
			PlayerNewPrimaryRole.Kidnapper => "KIDNAPPER", 
			PlayerNewPrimaryRole.Cultist => "CULTIST", 
			PlayerNewPrimaryRole.Zombie => "ZOMBIE", 
			PlayerNewPrimaryRole.Traitor => "TRAITOR", 
			_ => "NONE", 
		};
	}

	public static string GetPrimaryRolePowerString(PlayerPrimaryRolePower primaryRolePower)
	{
		return primaryRolePower switch
		{
			PlayerPrimaryRolePower.Necromancer => "NECROMANCER", 
			PlayerPrimaryRolePower.Deceiver => "DECEIVER", 
			PlayerPrimaryRolePower.Saboteur => "SABOTEUR", 
			PlayerPrimaryRolePower.Tracker => "TRACKER", 
			PlayerPrimaryRolePower.Warlock => "WARLOCK", 
			PlayerPrimaryRolePower.Possessor => "POSSESSOR", 
			PlayerPrimaryRolePower.Bomber => "BOMBER", 
			PlayerPrimaryRolePower.Poacher => "POACHER", 
			PlayerPrimaryRolePower.Ritualist => "RITUALIST", 
			PlayerPrimaryRolePower.Predator => "PREDATOR", 
			PlayerPrimaryRolePower.Sneak => "SNEAK", 
			PlayerPrimaryRolePower.Host => "HOST", 
			PlayerPrimaryRolePower.Peasant => "PEASANT", 
			PlayerPrimaryRolePower.Exorcist => "EXORCIST", 
			PlayerPrimaryRolePower.Avenger => "AVENGER", 
			PlayerPrimaryRolePower.Investigator => "INVESTIGATOR", 
			PlayerPrimaryRolePower.Survivalist => "SURVIVALIST", 
			PlayerPrimaryRolePower.Priest => "PRIEST", 
			PlayerPrimaryRolePower.Angel => "ANGEL", 
			PlayerPrimaryRolePower.Ghost => "GHOST", 
			PlayerPrimaryRolePower.Specter => "SPECTER", 
			PlayerPrimaryRolePower.Scout => "SCOUT", 
			PlayerPrimaryRolePower.Magician => "MAGICIAN", 
			PlayerPrimaryRolePower.Mystic => "MYSTIC", 
			PlayerPrimaryRolePower.Shadow => "SHADOW", 
			PlayerPrimaryRolePower.Hermit => "HERMIT", 
			PlayerPrimaryRolePower.Runemaster => "RUNEMASTER", 
			PlayerPrimaryRolePower.Avatar => "AVATAR", 
			PlayerPrimaryRolePower.Mole => "MOLE", 
			PlayerPrimaryRolePower.Hunter => "HUNTER", 
			PlayerPrimaryRolePower.Alchemist => "ALCHEMIST", 
			PlayerPrimaryRolePower.Spotter => "SPOTTER", 
			PlayerPrimaryRolePower.Purifier => "PURIFIER", 
			_ => "NONE", 
		};
	}

	public static string GetSecondaryRoleString(PlayerSecondaryRole secondaryRole)
	{
		return secondaryRole switch
		{
			PlayerSecondaryRole.None => "NONE", 
			PlayerSecondaryRole.BothAlcoholic => "ALCOHOLIC", 
			PlayerSecondaryRole.BothSprinter => "SPRINTER", 
			PlayerSecondaryRole.BothInfected => "INFECTED", 
			PlayerSecondaryRole.BothTeleporter => "TELEPORTER", 
			PlayerSecondaryRole.BothEngineer => "ENGINEER", 
			PlayerSecondaryRole.BothPolitician => "POLITICIAN", 
			PlayerSecondaryRole.BothMetabolic => "METABOLIC", 
			PlayerSecondaryRole.BothIllusionist => "ILLUSIONIST", 
			PlayerSecondaryRole.BothSherif => "SHERIF", 
			PlayerSecondaryRole.BothGambler => "GAMBLER", 
			PlayerSecondaryRole.BothMedium => "MEDIUM", 
			PlayerSecondaryRole.BothAstral => "ASTRAL", 
			PlayerSecondaryRole.BothScavenger => "SCAVENGER", 
			PlayerSecondaryRole.BothBlueMage => "BLUE_MAGE", 
			PlayerSecondaryRole.BothActor => "ACTOR", 
			PlayerSecondaryRole.BothScribe => "SCRIBE", 
			PlayerSecondaryRole.BothCarabineer => "CARABINEER", 
			PlayerSecondaryRole.BothForger => "FORGER", 
			PlayerSecondaryRole.BothImitator => "IMITATOR", 
			PlayerSecondaryRole.BothMerchant => "MERCHANT", 
			PlayerSecondaryRole.BothTinkerer => "TINKERER", 
			PlayerSecondaryRole.BothTelepath => "TELEPATH", 
			_ => "NONE", 
		};
	}

	public static Color GetPrimaryRolePowerColor(PlayerPrimaryRolePower primaryRolePower)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(primaryRolePower switch
		{
			PlayerPrimaryRolePower.Hunter => GameUI.HunterColor, 
			PlayerPrimaryRolePower.Alchemist => GameUI.AlchemistColor, 
			PlayerPrimaryRolePower.Spotter => PrimaryRolePowerSpotterColor, 
			PlayerPrimaryRolePower.Purifier => PrimaryRolePowerPurifierColor, 
			_ => Color.white, 
		});
	}

	public static Color GetNewPrimaryRoleColor(PlayerNewPrimaryRole newPrimaryRole)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(newPrimaryRole switch
		{
			PlayerNewPrimaryRole.VillageIdiot => NewPrimaryRoleVillageIdiotColor, 
			PlayerNewPrimaryRole.Agent => NewPrimaryRoleAgentColor, 
			PlayerNewPrimaryRole.Spy => NewPrimaryRoleSpyColor, 
			PlayerNewPrimaryRole.Scientist => NewPrimaryRoleScientistColor, 
			PlayerNewPrimaryRole.Lover => NewPrimaryRoleLoverColor, 
			PlayerNewPrimaryRole.Beast => NewPrimaryRoleBeastColor, 
			PlayerNewPrimaryRole.Mercenary => NewPrimaryRoleMercenaryColor, 
			PlayerNewPrimaryRole.Voodoo => NewPrimaryRoleVoodooColor, 
			PlayerNewPrimaryRole.Kidnapper => NewPrimaryRoleKidnapperColor, 
			PlayerNewPrimaryRole.Cultist => NewPrimaryRoleCultistColor, 
			PlayerNewPrimaryRole.Zombie => NewPrimaryRoleVoodooColor, 
			PlayerNewPrimaryRole.Traitor => NewPrimaryRoleTraitorColor, 
			_ => Color.white, 
		});
	}

	public static List<PlayerSecondaryRole> GetAvailableSecondaryRoles(DraftManager.DraftPlayerMainRole draftPrimaryRole, PlayerNewPrimaryRole newPrimaryRole, PlayerPrimaryRolePower primaryRolePower)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		PlayerRole basePrimaryRole;
		switch (draftPrimaryRole)
		{
		case DraftManager.DraftPlayerMainRole.NormalVillager:
		case DraftManager.DraftPlayerMainRole.Traitor:
		case DraftManager.DraftPlayerMainRole.EliteVillager:
			basePrimaryRole = (PlayerRole)0;
			break;
		case DraftManager.DraftPlayerMainRole.Wolf:
		case DraftManager.DraftPlayerMainRole.WolfPup:
			basePrimaryRole = (PlayerRole)1;
			break;
		default:
			basePrimaryRole = (PlayerRole)0;
			break;
		}
		return GetAvailableSecondaryRoles(basePrimaryRole, newPrimaryRole, primaryRolePower);
	}

	public static List<PlayerSecondaryRole> GetAvailableSecondaryRoles(PlayerRole basePrimaryRole, PlayerNewPrimaryRole newPrimaryRole, PlayerPrimaryRolePower primaryRolePower)
	{
		return newPrimaryRole switch
		{
			PlayerNewPrimaryRole.VillageIdiot => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothTeleporter,
				PlayerSecondaryRole.BothGambler,
				PlayerSecondaryRole.BothMedium,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothMerchant,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Agent => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothMetabolic,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothMedium,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothMerchant,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Spy => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothMetabolic,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothTeleporter,
				PlayerSecondaryRole.BothGambler,
				PlayerSecondaryRole.BothMedium,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Scientist => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothMetabolic,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothTeleporter,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Lover => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothTeleporter,
				PlayerSecondaryRole.BothGambler,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothMerchant,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Beast => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothTeleporter,
				PlayerSecondaryRole.BothGambler,
				PlayerSecondaryRole.BothMedium,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothMerchant,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Voodoo => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothMetabolic,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothTeleporter,
				PlayerSecondaryRole.BothGambler,
				PlayerSecondaryRole.BothMedium,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothMerchant,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Mercenary => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothMetabolic,
				PlayerSecondaryRole.BothGambler,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Kidnapper => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothTeleporter,
				PlayerSecondaryRole.BothGambler,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothMerchant,
				PlayerSecondaryRole.BothTinkerer
			}, 
			PlayerNewPrimaryRole.Cultist => new List<PlayerSecondaryRole>
			{
				PlayerSecondaryRole.BothAlcoholic,
				PlayerSecondaryRole.BothEngineer,
				PlayerSecondaryRole.BothIllusionist,
				PlayerSecondaryRole.BothInfected,
				PlayerSecondaryRole.BothSprinter,
				PlayerSecondaryRole.BothTeleporter,
				PlayerSecondaryRole.BothGambler,
				PlayerSecondaryRole.BothAstral,
				PlayerSecondaryRole.BothScavenger,
				PlayerSecondaryRole.BothBlueMage,
				PlayerSecondaryRole.BothActor,
				PlayerSecondaryRole.BothScribe,
				PlayerSecondaryRole.BothCarabineer,
				PlayerSecondaryRole.BothImitator,
				PlayerSecondaryRole.BothForger,
				PlayerSecondaryRole.BothMerchant,
				PlayerSecondaryRole.BothTinkerer
			}, 
			_ => primaryRolePower switch
			{
				PlayerPrimaryRolePower.Hunter => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothTeleporter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothMerchant,
					PlayerSecondaryRole.BothTinkerer,
					PlayerSecondaryRole.BothTelepath
				}, 
				PlayerPrimaryRolePower.Alchemist => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSherif,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothTeleporter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothCarabineer,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothMerchant,
					PlayerSecondaryRole.BothTinkerer,
					PlayerSecondaryRole.BothTelepath
				}, 
				PlayerPrimaryRolePower.Investigator => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothTeleporter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothCarabineer,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothTinkerer,
					PlayerSecondaryRole.BothTelepath
				}, 
				PlayerPrimaryRolePower.Scout => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSherif,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothTeleporter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothCarabineer,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothTinkerer,
					PlayerSecondaryRole.BothTelepath
				}, 
				PlayerPrimaryRolePower.Avatar => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothTeleporter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothCarabineer,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothTinkerer
				}, 
				PlayerPrimaryRolePower.Warlock => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSherif,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothTeleporter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothCarabineer,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothMerchant,
					PlayerSecondaryRole.BothTinkerer,
					PlayerSecondaryRole.BothTelepath
				}, 
				PlayerPrimaryRolePower.Poacher => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSherif,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothTeleporter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothMerchant,
					PlayerSecondaryRole.BothTinkerer,
					PlayerSecondaryRole.BothTelepath
				}, 
				PlayerPrimaryRolePower.Sneak => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSherif,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothCarabineer,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothMerchant,
					PlayerSecondaryRole.BothTinkerer,
					PlayerSecondaryRole.BothTelepath
				}, 
				PlayerPrimaryRolePower.Tracker => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSherif,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothCarabineer,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothMerchant,
					PlayerSecondaryRole.BothTelepath
				}, 
				_ => new List<PlayerSecondaryRole>
				{
					PlayerSecondaryRole.BothAlcoholic,
					PlayerSecondaryRole.BothEngineer,
					PlayerSecondaryRole.BothIllusionist,
					PlayerSecondaryRole.BothInfected,
					PlayerSecondaryRole.BothMetabolic,
					PlayerSecondaryRole.BothPolitician,
					PlayerSecondaryRole.BothSherif,
					PlayerSecondaryRole.BothSprinter,
					PlayerSecondaryRole.BothTeleporter,
					PlayerSecondaryRole.BothGambler,
					PlayerSecondaryRole.BothMedium,
					PlayerSecondaryRole.BothAstral,
					PlayerSecondaryRole.BothScavenger,
					PlayerSecondaryRole.BothBlueMage,
					PlayerSecondaryRole.BothActor,
					PlayerSecondaryRole.BothScribe,
					PlayerSecondaryRole.BothCarabineer,
					PlayerSecondaryRole.BothForger,
					PlayerSecondaryRole.BothImitator,
					PlayerSecondaryRole.BothMerchant,
					PlayerSecondaryRole.BothTinkerer,
					PlayerSecondaryRole.BothTelepath
				}, 
			}, 
		};
	}

	public static bool IsPrimaryRolePowerForNormalVillagers(PlayerPrimaryRolePower primaryRolePower)
	{
		return primaryRolePower switch
		{
			PlayerPrimaryRolePower.Peasant => true, 
			PlayerPrimaryRolePower.Exorcist => true, 
			PlayerPrimaryRolePower.Avenger => true, 
			PlayerPrimaryRolePower.Investigator => true, 
			PlayerPrimaryRolePower.Survivalist => true, 
			PlayerPrimaryRolePower.Priest => true, 
			PlayerPrimaryRolePower.Scout => true, 
			PlayerPrimaryRolePower.Magician => true, 
			PlayerPrimaryRolePower.Mystic => true, 
			PlayerPrimaryRolePower.Shadow => true, 
			PlayerPrimaryRolePower.Hermit => true, 
			PlayerPrimaryRolePower.Runemaster => true, 
			PlayerPrimaryRolePower.Avatar => true, 
			PlayerPrimaryRolePower.Mole => true, 
			_ => false, 
		};
	}

	public static bool IsPrimaryRolePowerForEliteVillagers(PlayerPrimaryRolePower primaryRolePower)
	{
		if ((uint)(primaryRolePower - 27) <= 3u)
		{
			return true;
		}
		return false;
	}

	public static bool IsPrimaryRolePowerForWolves(PlayerPrimaryRolePower primaryRolePower)
	{
		if ((uint)(primaryRolePower - 1) <= 11u)
		{
			return true;
		}
		return false;
	}

	public static bool IsNewPrimaryRoleDisabled(PlayerNewPrimaryRole newPrimaryRole)
	{
		if (newPrimaryRole == PlayerNewPrimaryRole.Zombie)
		{
			return true;
		}
		return false;
	}

	public static bool IsPrimaryRolePowerDisabled(PlayerPrimaryRolePower primaryRolePower)
	{
		switch (primaryRolePower)
		{
		case PlayerPrimaryRolePower.Tracker:
		case PlayerPrimaryRolePower.Poacher:
		case PlayerPrimaryRolePower.Peasant:
		case PlayerPrimaryRolePower.Avenger:
		case PlayerPrimaryRolePower.Mystic:
		case PlayerPrimaryRolePower.Angel:
		case PlayerPrimaryRolePower.Ghost:
		case PlayerPrimaryRolePower.Specter:
			return true;
		default:
			return false;
		}
	}

	public static bool IsWolfPowerAvailableForTraitor(PlayerPrimaryRolePower primaryRolePower)
	{
		switch (primaryRolePower)
		{
		case PlayerPrimaryRolePower.Deceiver:
		case PlayerPrimaryRolePower.Saboteur:
		case PlayerPrimaryRolePower.Warlock:
		case PlayerPrimaryRolePower.Bomber:
		case PlayerPrimaryRolePower.Poacher:
		case PlayerPrimaryRolePower.Ritualist:
		case PlayerPrimaryRolePower.Host:
			return true;
		default:
			return false;
		}
	}

	public static bool IsSecondaryRoleDisabled(PlayerSecondaryRole secondaryRole)
	{
		return false;
	}

	public static bool IsSecondaryRoleDisabledForTraitor(PlayerSecondaryRole secondaryRole)
	{
		if (secondaryRole == PlayerSecondaryRole.BothInfected || (uint)(secondaryRole - 8) <= 1u)
		{
			return true;
		}
		return false;
	}

	public Vector3 ActualPositionIncludingTeleport()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		NetworkTeleportData teleportData = PlayerController.CharacterMovementHandler.TeleportData;
		if (((NetworkTeleportData)(ref teleportData)).IsNone)
		{
			return ((Component)PlayerController).transform.position;
		}
		teleportData = PlayerController.CharacterMovementHandler.TeleportData;
		return ((NetworkTeleportData)(ref teleportData)).Position;
	}

	[Rpc]
	public unsafe static void Rpc_Disarm_Trap(NetworkRunner runner, int playerIndex, NetworkId trapId)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Disarm_Trap(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					Unsafe.Write(data + num2, trapId);
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerController playerController = player.PlayerController;
			float num3 = 2.5f;
			if (player.SecondaryRole == PlayerSecondaryRole.BothEngineer)
			{
				num3 = 1f;
			}
			else if (NetworkBool.op_Implicit(playerController.IsWolf))
			{
				num3 = 5f;
			}
			if (runner.IsServer)
			{
				player.TrapToDisarm = ((Component)runner.FindObject(trapId)).GetComponent<Trap>();
				playerController.MovementAction = 0;
				playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
				playerController.IsAiming = NetworkBool.op_Implicit(false);
				player.TrapDisarmTimer = TickTimer.CreateFromSeconds(runner, num3);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("TrapDisarm"), ((Component)player.TrapToDisarm).transform.position, 10f, 1f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Disarm_Trap error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Disarm_Trap(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")]
	[Preserve]
	protected unsafe static void Rpc_Disarm_Trap_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkId trapId = (NetworkId)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Disarm_Trap(runner, playerIndex, trapId);
	}

	private void TrapDisarmTimerExpired()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Invalid comparison between Unknown and I4
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController playerController = PlayerController;
			TrapDisarmTimer = TickTimer.None;
			playerController.MovementAction = 0;
			UpdateCanMoveAnimation();
			if (!NetworkBool.op_Implicit(playerController.IsDead) && !((Object)(object)TrapToDisarm == (Object)null) && (int)GameManager.LocalGameState == 2 && TrapToDisarm.TrapCanBeDisarmed())
			{
				Traverse<PlayerRef> val = Traverse.Create((object)TrapToDisarm).Field<PlayerRef>("_TrappedPlayer");
				Traverse.Create((object)TrapToDisarm).Field<Animator>("animator").Value.SetBool(Animator.StringToHash("Closing"), true);
				val.Value = playerController.Ref;
				if (((SimulationBehaviour)TrapToDisarm).HasStateAuthority)
				{
					GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)TrapToDisarm).Runner, NetworkString<_16>.op_Implicit("TRAP_CLOSING"), ((Component)TrapToDisarm).transform.position, 20f, 1f);
				}
				((NetworkBehaviour)TrapToDisarm).CopyBackingFieldsToState(true);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TrapDisarmTimerExpired error: " + ex));
		}
	}

	private void PrimaryRolePowerCooldownTimerExpired()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Invalid comparison between Unknown and I4
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (IsCurrentlyPlayedOrObserved)
			{
				UpdateDescriptionStatusIfNeeded();
			}
			if (!((SimulationBehaviour)this).Runner.IsServer)
			{
				return;
			}
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || (int)val == 5)
			{
				return;
			}
			PrimaryRolePowerCooldownTimer = TickTimer.None;
			if (PrimaryRolePower == PlayerPrimaryRolePower.Angel)
			{
				PrimaryRolePowerRemainingUses++;
			}
			else
			{
				if (NetworkBool.op_Implicit(PlayerController.IsDead))
				{
					return;
				}
				switch (NewPrimaryRole)
				{
				case PlayerNewPrimaryRole.VillageIdiot:
				case PlayerNewPrimaryRole.Scientist:
				case PlayerNewPrimaryRole.Beast:
				case PlayerNewPrimaryRole.Voodoo:
				case PlayerNewPrimaryRole.Kidnapper:
				case PlayerNewPrimaryRole.Cultist:
					PrimaryRolePowerRemainingUses++;
					break;
				}
				switch (PrimaryRolePower)
				{
				case PlayerPrimaryRolePower.Investigator:
					if (PrimaryRoleTargetRef == PlayerRef.None && Random.value < 0.6f)
					{
						InvestigatorGiveNewTarget();
					}
					else
					{
						InvestigatorHint.CreateNewHint(((SimulationBehaviour)this).Runner, this, new List<Teleporter>());
					}
					TriggerPrimaryRolePowerCooldown(((SimulationBehaviour)this).Runner);
					break;
				case PlayerPrimaryRolePower.Hermit:
					HermitHideout.CreateNewHideout(((SimulationBehaviour)this).Runner, this, new List<Teleporter>());
					TriggerPrimaryRolePowerCooldown(((SimulationBehaviour)this).Runner);
					break;
				case PlayerPrimaryRolePower.Deceiver:
				case PlayerPrimaryRolePower.Warlock:
					PrimaryRolePowerRemainingUses++;
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRolePowerCooldownTimerExpired error: " + ex));
		}
	}

	private void SecondaryRolePowerCooldownTimerExpired()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Invalid comparison between Unknown and I4
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Invalid comparison between Unknown and I4
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			SecondaryRolePowerCooldownTimer = TickTimer.None;
			if (!((SimulationBehaviour)this).Runner.IsServer || NetworkBool.op_Implicit(PlayerController.IsDead))
			{
				return;
			}
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val > 1 && (int)val != 5)
			{
				switch (SecondaryRole)
				{
				case PlayerSecondaryRole.BothAlcoholic:
				case PlayerSecondaryRole.BothSprinter:
				case PlayerSecondaryRole.BothInfected:
				case PlayerSecondaryRole.BothTeleporter:
				case PlayerSecondaryRole.BothEngineer:
				case PlayerSecondaryRole.BothMetabolic:
				case PlayerSecondaryRole.BothIllusionist:
				case PlayerSecondaryRole.BothSherif:
				case PlayerSecondaryRole.BothGambler:
				case PlayerSecondaryRole.BothAstral:
				case PlayerSecondaryRole.BothScavenger:
				case PlayerSecondaryRole.BothBlueMage:
				case PlayerSecondaryRole.BothActor:
				case PlayerSecondaryRole.BothScribe:
				case PlayerSecondaryRole.BothForger:
				case PlayerSecondaryRole.BothTinkerer:
					SecondaryRoleFirstRemainingUses++;
					break;
				case PlayerSecondaryRole.BothCarabineer:
					PlayerController.IsGunLoaded = NetworkBool.op_Implicit(true);
					break;
				case PlayerSecondaryRole.BothPolitician:
				case PlayerSecondaryRole.BothMedium:
				case PlayerSecondaryRole.BothImitator:
				case PlayerSecondaryRole.BothMerchant:
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRolePowerCooldownTimerExpired error: " + ex));
		}
	}

	public override void Spawned()
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Spawned();
		try
		{
			if (((SimulationBehaviour)this).HasInputAuthority)
			{
				if (((SimulationBehaviour)this).Runner.IsServer)
				{
					ModVersion = float.Parse("0.321", CultureInfo.InvariantCulture.NumberFormat);
				}
				else
				{
					Rpc_Set_Validation(((SimulationBehaviour)this).Runner, Index, float.Parse("0.321", CultureInfo.InvariantCulture.NumberFormat));
				}
			}
			GameObject val = Object.Instantiate<GameObject>(MinimapPlayerComponent.MinimapPlayerPrefab);
			MinimapObject = val.GetComponent<MinimapPlayerComponent>();
			((Component)MinimapObject).gameObject.SetActive(true);
			MinimapObject.Init(this);
			LycansUtility.DebugLog("BUG2 spawned PlayerCustom with ref " + ((object)Ref/*cast due to constrained. prefix*/).ToString() + " and index " + Index);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PlayerCustom Spawned error: " + ex));
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		try
		{
			if ((Object)(object)MinimapObject != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)MinimapObject).gameObject);
			}
			if (NewPrimaryRole == PlayerNewPrimaryRole.Kidnapper)
			{
				foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.Kidnapped)))
				{
					item.Kidnapped = NetworkBool.op_Implicit(false);
				}
			}
			LycansUtility.DebugLog("BUG2 despawned PlayerCustom with ref " + ((object)Ref/*cast due to constrained. prefix*/).ToString() + " and index " + Index);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PlayerCustom Despawned error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Set_Validation(NetworkRunner runner, int playerIndex, float version)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Set_Validation(Fusion.NetworkRunner,System.Int32,System.Single)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(float*)(data + num2) = version;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (runner.IsServer)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
				player.ModVersion = version;
				UIManager.ModInstallationPanel.AddOrUpdatePlayer(player.Ref);
				UIOptionsDisplayPanel.SendRefreshToClients();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Set_Validation error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Set_Validation(Fusion.NetworkRunner,System.Int32,System.Single)")]
	[Preserve]
	protected unsafe static void Rpc_Set_Validation_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		float version = *(float*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Set_Validation(runner, playerIndex, version);
	}

	public override void FixedUpdateNetwork()
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Invalid comparison between Unknown and I4
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a18: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Invalid comparison between Unknown and I4
		//IL_1a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1358: Unknown result type (might be due to invalid IL or missing references)
		//IL_135d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Invalid comparison between Unknown and I4
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1375: Unknown result type (might be due to invalid IL or missing references)
		//IL_1381: Unknown result type (might be due to invalid IL or missing references)
		//IL_1399: Unknown result type (might be due to invalid IL or missing references)
		//IL_139f: Invalid comparison between Unknown and I4
		//IL_134b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b78: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Invalid comparison between Unknown and I4
		//IL_1b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_151e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1535: Unknown result type (might be due to invalid IL or missing references)
		//IL_153a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1459: Unknown result type (might be due to invalid IL or missing references)
		//IL_1465: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1546: Unknown result type (might be due to invalid IL or missing references)
		//IL_154b: Unknown result type (might be due to invalid IL or missing references)
		//IL_159c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1557: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1564: Unknown result type (might be due to invalid IL or missing references)
		//IL_156e: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1744: Unknown result type (might be due to invalid IL or missing references)
		//IL_1749: Unknown result type (might be due to invalid IL or missing references)
		//IL_148e: Unknown result type (might be due to invalid IL or missing references)
		//IL_149c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1755: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ead: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1856: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_180d: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_164c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d71: Expected O, but got Unknown
		//IL_12d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1306: Unknown result type (might be due to invalid IL or missing references)
		//IL_1086: Unknown result type (might be due to invalid IL or missing references)
		//IL_115c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1168: Unknown result type (might be due to invalid IL or missing references)
		//IL_1180: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d4: Expected O, but got Unknown
		//IL_11f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_121a: Unknown result type (might be due to invalid IL or missing references)
		//IL_122f: Expected O, but got Unknown
		//IL_1253: Unknown result type (might be due to invalid IL or missing references)
		//IL_1870: Unknown result type (might be due to invalid IL or missing references)
		//IL_1876: Invalid comparison between Unknown and I4
		//IL_181f: Unknown result type (might be due to invalid IL or missing references)
		//IL_177a: Unknown result type (might be due to invalid IL or missing references)
		//IL_177f: Unknown result type (might be due to invalid IL or missing references)
		//IL_176d: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1109: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_187e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_178b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1795: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1840: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6c: Expected O, but got Unknown
		//IL_0e71: Expected O, but got Unknown
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_196f: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_17aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e1: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			MinimapObject.UpdateIcon();
			if (IsCurrentlyPlayedOrObserved && (int)GameManager.LocalGameState == 2)
			{
				if (NewPrimaryRole == PlayerNewPrimaryRole.Lover && !ShowRoleDescriptionPatch.ShowingExplanation)
				{
					PlayerCustom playerCustom = FindLoverPartner();
					if ((Object)(object)playerCustom != (Object)null && Mathf.RoundToInt(playerCustom.PlayerController.Hunger * 100f / (float)GameManager.Instance.MaxHunger) != ShowRoleDescriptionPatch.LoverPartnerCurrentHealthPercentageToShow)
					{
						UpdateDescriptionStatusIfNeeded();
					}
				}
				if (PrimaryRolePower == PlayerPrimaryRolePower.Predator && PrimaryRoleTargetRef != PlayerRef.None)
				{
					UpdateDescriptionStatusIfNeeded();
				}
				if (NetworkBool.op_Implicit(PlayerController.IsWolf))
				{
					ColorAdjustmentManager.UpdateColorAdjustment();
				}
			}
			TickTimer val = PrimaryRoleActionTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				PrimaryRoleActionTimerExpired();
			}
			val = SecondaryRoleActionTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				SecondaryRoleActionTimerExpired();
			}
			val = TrapDisarmTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				TrapDisarmTimerExpired();
			}
			val = PrimaryRolePowerCooldownTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				PrimaryRolePowerCooldownTimerExpired();
			}
			val = SecondaryRolePowerCooldownTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				SecondaryRolePowerCooldownTimerExpired();
			}
			val = SabotageTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				FinishSabotage();
			}
			val = SurvivalistSaveTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				FinishSurvivalistSave();
			}
			val = LootCorpseTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				FinishLootCorpse();
			}
			PlayerRef primaryRoleTargetRef;
			if (((SimulationBehaviour)this).Runner.IsServer)
			{
				val = CurseTimer;
				if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
				{
					PlayerController playerController = PlayerController;
					if (NetworkBool.op_Implicit(CurseDormant))
					{
						if ((int)GameManager.State.Current == 2 && !NetworkBool.op_Implicit(playerController.IsDead) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && !NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
						{
							if (NetworkBool.op_Implicit(playerController.CanMoveAnimation))
							{
								CurseDormant = NetworkBool.op_Implicit(false);
								Rpc_Warlock_Transform(((SimulationBehaviour)this).Runner, Index);
							}
							else
							{
								CurseTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 1f);
							}
						}
						else
						{
							CurseDormant = NetworkBool.op_Implicit(false);
							CurseTimer = TickTimer.None;
						}
					}
					else
					{
						if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.State.Current == 2 && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && !NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
						{
							playerController.IsWolf = NetworkBool.op_Implicit(false);
							string text = DateTime.UtcNow.ToString();
							NetworkString<_32> username = PlayerController.PlayerData.Username;
							LycansUtility.AddLogOnlyForMe("Adding detransformation from warlock curse at date: " + text + ", player: " + ((object)username/*cast due to constrained. prefix*/).ToString());
							GameManagerCustom.Instance.AddDetransformation();
						}
						CurseTimer = TickTimer.None;
						CurseDormant = NetworkBool.op_Implicit(false);
					}
				}
				val = HauntedTimer;
				if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
				{
					if (LycansUtility.GameActuallyInPlay)
					{
						PlayerController playerController2 = PlayerController;
						List<HauntedEffect.HauntedPossibleEffect> list = new List<HauntedEffect.HauntedPossibleEffect>();
						foreach (KeyValuePair<HauntedEffect.HauntedPossibleEffect, int> item in BalancingValues.HauntedChanceByEffect)
						{
							for (int i = 0; i < item.Value; i++)
							{
								list.Add(item.Key);
							}
						}
						if (NewPrimaryRole == PlayerNewPrimaryRole.Lover && (int)PlayerController.Role == 0)
						{
							list.RemoveAll((HauntedEffect.HauntedPossibleEffect o) => o == HauntedEffect.HauntedPossibleEffect.HealthGain);
							list.RemoveAll((HauntedEffect.HauntedPossibleEffect o) => o == HauntedEffect.HauntedPossibleEffect.HealthLoss);
						}
						HauntedEffect.HauntedPossibleEffect hauntedPossibleEffect = CollectionsUtil.Grab<HauntedEffect.HauntedPossibleEffect>(list, 1).First();
						switch (hauntedPossibleEffect)
						{
						case HauntedEffect.HauntedPossibleEffect.HealthGain:
							IncreaseHealth(0.25f);
							Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, playerController2.Index, 1);
							break;
						case HauntedEffect.HauntedPossibleEffect.HealthLoss:
							playerController2.Hunger = Mathf.Max((float)GameManager.Instance.MaxHunger * 0.35f, playerController2.Hunger - (float)GameManager.Instance.MaxHunger * 0.2f);
							Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, playerController2.Index, 2);
							break;
						case HauntedEffect.HauntedPossibleEffect.OpenDoors:
						case HauntedEffect.HauntedPossibleEffect.CloseDoors:
						{
							List<Door> list3 = Object.FindObjectsOfType<Door>().ToList();
							foreach (Door item2 in list3)
							{
								if (Vector3.Distance(((Component)playerController2).transform.position, ((Component)item2).transform.position) <= 40f)
								{
									if (hauntedPossibleEffect == HauntedEffect.HauntedPossibleEffect.OpenDoors && !NetworkBool.op_Implicit(item2.IsOpen) && !NetworkBool.op_Implicit(item2.IsLocked) && !NetworkBool.op_Implicit(item2.IsMoving))
									{
										item2.Rpc_Interact(playerController2.Ref);
									}
									if (hauntedPossibleEffect == HauntedEffect.HauntedPossibleEffect.CloseDoors && NetworkBool.op_Implicit(item2.IsOpen) && !NetworkBool.op_Implicit(item2.IsMoving))
									{
										item2.Rpc_Interact(playerController2.Ref);
									}
								}
							}
							break;
						}
						case HauntedEffect.HauntedPossibleEffect.Teleport:
							TeleportToRandomTeleporter();
							break;
						case HauntedEffect.HauntedPossibleEffect.GainItem:
							if (!((Object)(object)playerController2.Item == (Object)null))
							{
								if (NetworkBool.op_Implicit(playerController2.IsZooming))
								{
									break;
								}
								val = playerController2.Item.AnimationTimer;
								if (((TickTimer)(ref val)).IsRunning)
								{
									break;
								}
								val = playerController2.Item.TriggerTimer;
								if (((TickTimer)(ref val)).IsRunning)
								{
									break;
								}
							}
							GiveRandomItem();
							Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, playerController2.Index, 1);
							break;
						case HauntedEffect.HauntedPossibleEffect.RandomEffect:
						{
							List<string> list2 = new List<string> { "LycansNewRoles.EffectStunned", "LycansNewRoles.EffectBlind", "LycansNewRoles.EffectClairvoyance", "Flatulences", "Invisibility", "Paranoia", "Speed" };
							string text2 = CollectionsUtil.Grab<string>(list2, 1).First();
							switch (text2)
							{
							case "LycansNewRoles.EffectStunned":
								ApplyEffectToPlayer(playerController2, text2, ((SimulationBehaviour)this).Runner, 0.6f);
								break;
							case "LycansNewRoles.EffectBlind":
								ApplyEffectToPlayer(playerController2, text2, ((SimulationBehaviour)this).Runner);
								break;
							case "LycansNewRoles.EffectClairvoyance":
								ApplyEffectToPlayer(playerController2, text2, ((SimulationBehaviour)this).Runner, 0.3f);
								break;
							case "Flatulences":
							{
								Effect effect3 = EffectManager.GetEffects().First((Effect o) => o is FlatulenceEffect);
								ApplyEffectToPlayer(playerController2, effect3, ((SimulationBehaviour)this).Runner, 0.1f);
								break;
							}
							case "Invisibility":
							{
								Effect effect4 = EffectManager.GetEffects().First((Effect o) => o is InvisibilityEffect);
								ApplyEffectToPlayer(playerController2, effect4, ((SimulationBehaviour)this).Runner, 0.2f);
								break;
							}
							case "Paranoia":
							{
								Effect effect2 = EffectManager.GetEffects().First((Effect o) => o is ParanoiaEffect);
								ApplyEffectToPlayer(playerController2, effect2, ((SimulationBehaviour)this).Runner, 0.1f);
								break;
							}
							case "Speed":
							{
								Effect effect = EffectManager.GetEffects().First((Effect o) => o is SpeedEffect);
								ApplyEffectToPlayer(playerController2, effect, ((SimulationBehaviour)this).Runner, 0.15f);
								break;
							}
							}
							break;
						}
						case HauntedEffect.HauntedPossibleEffect.Sound:
							Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, playerController2.Index, 5);
							break;
						case HauntedEffect.HauntedPossibleEffect.FlickerLanterns:
							Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, playerController2.Index, 14);
							break;
						case HauntedEffect.HauntedPossibleEffect.ForcedRotation:
							((Component)playerController2).GetComponent<ForcedRotationComponent>().Init(new Vector3(0f, 1f, 0f), 3000f, 2000f);
							GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerController2).transform.position, BalancingValues.WolfKillSoundRangeByMap(GameManager.Instance.MapID) * 0.5f, 1f);
							break;
						}
					}
					HauntedTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, Random.Range(3f, 6f));
				}
				val = ChaosTimer;
				if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
				{
					if (LycansUtility.GameActuallyInPlay)
					{
						PlayerController player = PlayerController;
						List<ChaosEffect.ChaosPossibleEffect> list4 = new List<ChaosEffect.ChaosPossibleEffect>();
						foreach (KeyValuePair<ChaosEffect.ChaosPossibleEffect, int> item3 in BalancingValues.ChaosChanceByEffect)
						{
							for (int num = 0; num < item3.Value; num++)
							{
								list4.Add(item3.Key);
							}
						}
						List<PlayerCustom> list5 = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != Ref && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && Vector3.Distance(((Component)player).transform.position, ((Component)o.PlayerController).transform.position) <= 15f).ToList();
						if (!list5.Any())
						{
							list4.RemoveAll((ChaosEffect.ChaosPossibleEffect o) => o == ChaosEffect.ChaosPossibleEffect.UseScrollOnNearbyPlayer || o == ChaosEffect.ChaosPossibleEffect.UseDiamondOnNearbyPlayer);
						}
						List<Door> source = (from o in Object.FindObjectsOfType<Door>()
							where Vector3.Distance(((Component)player).transform.position, ((Component)o).transform.position) <= 20f && !NetworkBool.op_Implicit(o.IsMoving)
							select o).ToList();
						if (!source.Any((Door o) => NetworkBool.op_Implicit(o.IsLocked)))
						{
							list4.RemoveAll((ChaosEffect.ChaosPossibleEffect o) => o == ChaosEffect.ChaosPossibleEffect.UnlockNearbyDoor);
						}
						if (!source.Any((Door o) => !NetworkBool.op_Implicit(o.IsLocked)))
						{
							list4.RemoveAll((ChaosEffect.ChaosPossibleEffect o) => o == ChaosEffect.ChaosPossibleEffect.LockNearbyDoor);
						}
						switch (CollectionsUtil.Grab<ChaosEffect.ChaosPossibleEffect>(list4, 1).First())
						{
						case ChaosEffect.ChaosPossibleEffect.CreateTraps:
						{
							TrapItem val7 = (TrapItem)Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value.First((Item o) => o is TrapItem);
							Trap value4 = Traverse.Create((object)val7).Field<Trap>("trapPrefab").Value;
							int num2 = Random.RandomRangeInt(1, 4);
							Vector3 val8 = default(Vector3);
							OnBeforeSpawned val11 = default(OnBeforeSpawned);
							for (int num3 = 0; num3 < num2; num3++)
							{
								((Vector3)(ref val8))._002Ector(((Component)player).transform.position.x + Random.Range(-2f, 2f), ((Component)player).transform.position.y + 0.026f, ((Component)player).transform.position.z + Random.Range(-2f, 2f));
								GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("TRAP_DROP"), val8, 10f, 0.5f);
								NetworkRunner runner = ((SimulationBehaviour)this).Runner;
								Vector3? val9 = val8;
								Quaternion? val10 = Quaternion.identity;
								OnBeforeSpawned obj = val11;
								if (obj == null)
								{
									OnBeforeSpawned val12 = delegate(NetworkRunner _, NetworkObject no)
									{
										//IL_000d: Unknown result type (might be due to invalid IL or missing references)
										((Component)no).GetComponent<Trap>().Init(player.Ref);
									};
									OnBeforeSpawned val13 = val12;
									val11 = val12;
									obj = val13;
								}
								runner.Spawn<Trap>(value4, val9, val10, (PlayerRef?)null, obj, (NetworkObjectPredictionKey?)null, true);
							}
							break;
						}
						case ChaosEffect.ChaosPossibleEffect.CreateGrenade:
						{
							Vector3 value3 = default(Vector3);
							((Vector3)(ref value3))._002Ector(((Component)player).transform.position.x + Random.Range(-2f, 2f), ((Component)player).transform.position.y + 1f, ((Component)player).transform.position.z + Random.Range(-2f, 2f));
							NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemGrenadeActive");
							NetworkObject val6 = ((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)value3, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
							((Component)val6).GetComponent<GrenadeActive>().Init(Vector3.zero);
							((Component)val6).GetComponent<GrenadeActive>().CreatedByChaosEffect = true;
							break;
						}
						case ChaosEffect.ChaosPossibleEffect.UseScrollOnNearbyPlayer:
						{
							PlayerCustom playerCustom3 = CollectionsUtil.Grab<PlayerCustom>(list5, 1).First();
							Effect randomEffectWithPonderation = MagicScrollItem.GetRandomEffectWithPonderation();
							float value2 = Traverse.Create((object)randomEffectWithPonderation).Field<float>("duration").Value;
							ApplyEffectToPlayer(playerCustom3.PlayerController, randomEffectWithPonderation, ((SimulationBehaviour)this).Runner, 0.75f);
							Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, playerCustom3.Index, 1);
							GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)playerCustom3.PlayerController).transform.position, 15f, 0.5f);
							if (playerCustom3.SecondaryRole == PlayerSecondaryRole.BothBlueMage)
							{
								playerCustom3.SecondaryRoleUniqueInt = EffectManager.GetEffectIndex(randomEffectWithPonderation);
							}
							Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, playerCustom3.Index, 1);
							break;
						}
						case ChaosEffect.ChaosPossibleEffect.LockNearbyDoor:
						{
							Door val4 = CollectionsUtil.Grab<Door>(source.Where((Door o) => !NetworkBool.op_Implicit(o.IsLocked)).ToList(), 1).First();
							if (NetworkBool.op_Implicit(val4.IsOpen))
							{
								val4.IsMoving = NetworkBool.op_Implicit(true);
								val4.IsOpen = NetworkBool.op_Implicit(false);
								Traverse.Create((object)val4).Field<Animator>("_animator").Value.SetBool(Animator.StringToHash("Open"), false);
							}
							GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("LOCK_PLACE"), ((Component)this).transform.position, 25f, 1f);
							val4.IsLocked = NetworkBool.op_Implicit(true);
							break;
						}
						case ChaosEffect.ChaosPossibleEffect.UnlockNearbyDoor:
						{
							Door val5 = CollectionsUtil.Grab<Door>(source.Where((Door o) => NetworkBool.op_Implicit(o.IsLocked)).ToList(), 1).First();
							GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("LOCK_BREAK"), ((Component)val5).transform.position, 50f, 1f);
							val5.IsLocked = NetworkBool.op_Implicit(false);
							break;
						}
						case ChaosEffect.ChaosPossibleEffect.CreateSmoke:
						{
							SmokeItem val2 = (SmokeItem)Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value.First((Item o) => o is SmokeItem);
							GameObject value = Traverse.Create((object)val2).Field<GameObject>("smokePrefab").Value;
							NetworkObject val3 = ((SimulationBehaviour)this).Runner.Spawn(value, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
							{
								//IL_0012: Unknown result type (might be due to invalid IL or missing references)
								((Component)no).GetComponent<Smoke>().Init(((Component)player).transform.position);
							}, (NetworkObjectPredictionKey?)null, true);
							Traverse.Create((object)((Component)val3).GetComponent<Smoke>()).Property<TickTimer>("SmokeTimer", (object[])null).Value = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 8f);
							break;
						}
						case ChaosEffect.ChaosPossibleEffect.UseDiamondOnNearbyPlayer:
						{
							PlayerCustom playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(list5, 1).First();
							ApplyEffectToPlayer(playerCustom2.PlayerController, "LycansNewRoles.EffectPhasing", ((SimulationBehaviour)this).Runner);
							GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("DiamondEffect"), ((Component)playerCustom2.PlayerController).transform.position, 20f, 0.2f);
							break;
						}
						}
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("ChaosEffect"), ((Component)player).transform.position, 40f, 0.5f);
						if (!NetworkBool.op_Implicit(player.PlayerEffectManager.Invisible))
						{
							Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, player.Index, 6);
						}
					}
					ChaosTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, Random.Range(6f, 12f));
				}
				val = BombTimer;
				if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
				{
					BombDormant = NetworkBool.op_Implicit(false);
					BombTimer = TickTimer.None;
					PlayerController playerController3 = PlayerController;
					if ((int)GameManager.State.Current == 2 && !NetworkBool.op_Implicit(playerController3.IsDead))
					{
						ApplyEffectToPlayer(playerController3, "LycansNewRoles.EffectBomb", ((SimulationBehaviour)playerController3).Runner);
					}
				}
				if (NetworkBool.op_Implicit(Tiny) && !NetworkBool.op_Implicit(PlayerController.IsWolf) && !NetworkBool.op_Implicit(Petrified))
				{
					PlayerController playerController4 = PlayerController;
					List<PlayerController> list6 = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => o.Ref != Ref && !NetworkBool.op_Implicit(o.IsDead))).ToList();
					foreach (PlayerController item4 in list6)
					{
						if (Vector3.Distance(((Component)playerController4).transform.position, ((Component)item4).transform.position) < 0.6f && NetworkBool.op_Implicit(item4.IsMoving))
						{
							PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(item4.Ref);
							if (!NetworkBool.op_Implicit(player2.Tiny))
							{
								Stats.UpdateDeathType("CRUSHED");
								playerController4.Rpc_Kill(item4.Ref);
							}
						}
					}
				}
				switch (NewPrimaryRole)
				{
				case PlayerNewPrimaryRole.Spy:
				{
					if (!LycansUtility.GameActuallyInPlay || NetworkBool.op_Implicit(PlayerController.IsDead))
					{
						break;
					}
					NetworkTeleportData teleportData = PlayerController.CharacterMovementHandler.TeleportData;
					if (((NetworkTeleportData)(ref teleportData)).IsNone)
					{
						primaryRoleTargetRef = PrimaryRoleTargetRef;
						if (((PlayerRef)(ref primaryRoleTargetRef)).IsNone || !PlayerRegistry.HasPlayer(PrimaryRoleTargetRef) || NetworkBool.op_Implicit(PlayerRegistry.GetPlayer(PrimaryRoleTargetRef).IsDead))
						{
							SpyGiveNewTarget();
						}
					}
					break;
				}
				case PlayerNewPrimaryRole.Mercenary:
					if (!LycansUtility.GameActuallyInPlay || NetworkBool.op_Implicit(PlayerController.IsDead))
					{
						break;
					}
					val = PrimaryRolePowerCooldownTimer;
					if (!((TickTimer)(ref val)).IsRunning)
					{
						primaryRoleTargetRef = PrimaryRoleTargetRef;
						if (((PlayerRef)(ref primaryRoleTargetRef)).IsNone || !PlayerRegistry.HasPlayer(PrimaryRoleTargetRef) || NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(PrimaryRoleTargetRef).Kidnapped))
						{
							MercenaryGiveNewTarget();
						}
					}
					break;
				}
				switch (PrimaryRolePower)
				{
				case PlayerPrimaryRolePower.Peasant:
					if (NetworkBool.op_Implicit(NewPrimaryRoleUniqueBool))
					{
						PrimaryRolePowerCurrentMaterials = Mathf.RoundToInt(Mathf.Max(0f, (float)PrimaryRolePowerCurrentMaterials - 1250f * ((SimulationBehaviour)this).Runner.DeltaTime));
						if (PrimaryRolePowerCurrentMaterials == 0 || PlayerController.MovementAction != 1 || Knockback.Knockback.HasValue)
						{
							NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
						}
					}
					break;
				case PlayerPrimaryRolePower.Shadow:
					if (NetworkBool.op_Implicit(NewPrimaryRoleUniqueBool))
					{
						PrimaryRolePowerCurrentMaterials = Mathf.RoundToInt(Mathf.Max(0f, (float)PrimaryRolePowerCurrentMaterials - 1500f * ((SimulationBehaviour)this).Runner.DeltaTime));
						if (PrimaryRolePowerCurrentMaterials == 0 || !LycansUtility.GameActuallyInPlay)
						{
							NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
						}
					}
					break;
				case PlayerPrimaryRolePower.Necromancer:
				case PlayerPrimaryRolePower.Warlock:
					primaryRoleTargetRef = PrimaryRoleTargetRef;
					if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && !PlayerRegistry.HasPlayer(PrimaryRoleTargetRef))
					{
						PrimaryRoleTargetRef = PlayerRef.None;
					}
					primaryRoleTargetRef = PrimaryRoleTargetRef;
					if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(PrimaryRoleTargetRef).Disappeared))
					{
						PrimaryRoleTargetRef = PlayerRef.None;
					}
					break;
				case PlayerPrimaryRolePower.Possessor:
				{
					primaryRoleTargetRef = PrimaryRoleTargetRef;
					if (((PlayerRef)(ref primaryRoleTargetRef)).IsNone)
					{
						break;
					}
					if (!PlayerRegistry.HasPlayer(PrimaryRoleTargetRef))
					{
						PrimaryRoleTargetRef = PlayerRef.None;
						break;
					}
					PlayerCustom player3 = PlayerCustomRegistry.GetPlayer(PrimaryRoleTargetRef);
					if (NetworkBool.op_Implicit(player3.PlayerController.IsDead) || (!NetworkBool.op_Implicit(PlayerController.IsWolf) && player3.AlreadyPossessed))
					{
						PrimaryRoleTargetRef = PlayerRef.None;
					}
					break;
				}
				}
				if (NetworkBool.op_Implicit(PlayerController.IsWolf))
				{
					bool flag = (int)PlayerController.Role == 1 && !NetworkBool.op_Implicit(PlayerController.IsMoving) && PlayerController.MovementAction == 0 && CanMoveCustom() && NetworkBool.op_Implicit(PlayerController.CanMoveAnimation) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive);
					if (!NetworkBool.op_Implicit(Recuperating))
					{
						if (!WolfRecuperateStopwatch.IsRunning)
						{
							if (flag)
							{
								WolfRecuperateStopwatch.Restart();
							}
						}
						else if (!flag)
						{
							WolfRecuperateStopwatch.Reset();
						}
						else if (WolfRecuperateStopwatch.ElapsedMilliseconds >= 4000)
						{
							ApplyEffectToPlayer(PlayerController, "LycansNewRoles.EffectRecuperating", ((SimulationBehaviour)this).Runner, 1f, 3600f);
						}
					}
					if (NetworkBool.op_Implicit(Recuperating) && !flag)
					{
						Effect val14 = PlayerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is RecuperatingEffect);
						if ((Object)(object)val14 != (Object)null)
						{
							PlayerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val14).Object.Id);
						}
					}
				}
				if (Accessory is AccessoryBackpack accessoryBackpack)
				{
					accessoryBackpack.TransferItemToRegularSlotIfNeeded();
				}
			}
			val = PrimaryRolePowerCooldownTimer;
			if (((TickTimer)(ref val)).IsRunning && IsCurrentlyPlayedOrObserved)
			{
				ShowRoleDescriptionPatch.UpdatePrimaryRolePowerCooldownSeconds(this);
			}
			val = SecondaryRolePowerCooldownTimer;
			if (((TickTimer)(ref val)).IsRunning && IsCurrentlyPlayedOrObserved)
			{
				ShowRoleDescriptionPatch.UpdateSecondaryRolePowerCooldownSeconds(this);
			}
			if (!IsCurrentlyPlayedOrObserved)
			{
				return;
			}
			List<PlayerRef> playersWithSpecificColor = new List<PlayerRef>();
			switch (NewPrimaryRole)
			{
			case PlayerNewPrimaryRole.Spy:
				playersWithSpecificColor.Add(PrimaryRoleTargetRef);
				break;
			case PlayerNewPrimaryRole.Lover:
			{
				PlayerCustom playerCustom4 = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Lover && o.Ref != Ref).FirstOrDefault();
				if ((Object)(object)playerCustom4 != (Object)null)
				{
					playersWithSpecificColor.Add(playerCustom4.Ref);
				}
				break;
			}
			case PlayerNewPrimaryRole.Beast:
				playersWithSpecificColor.AddRange(from o in PlayerCustomRegistry
					where NetworkBool.op_Implicit(o.BeastMark)
					select o.Ref);
				break;
			case PlayerNewPrimaryRole.Mercenary:
				playersWithSpecificColor.Add(PrimaryRoleTargetRef);
				break;
			case PlayerNewPrimaryRole.Voodoo:
				playersWithSpecificColor.AddRange(from o in PlayerCustomRegistry
					where o.NewPrimaryRole == PlayerNewPrimaryRole.Zombie
					select o.Ref);
				break;
			case PlayerNewPrimaryRole.Kidnapper:
				playersWithSpecificColor.AddRange(from o in PlayerCustomRegistry
					where NetworkBool.op_Implicit(o.Kidnapped)
					select o.Ref);
				break;
			}
			switch (PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Necromancer:
			case PlayerPrimaryRolePower.Warlock:
			case PlayerPrimaryRolePower.Possessor:
			case PlayerPrimaryRolePower.Predator:
				primaryRoleTargetRef = PrimaryRoleTargetRef;
				if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone)
				{
					playersWithSpecificColor.Add(PrimaryRoleTargetRef);
				}
				break;
			case PlayerPrimaryRolePower.Host:
				playersWithSpecificColor.AddRange(from o in PlayerCustomRegistry
					where NetworkBool.op_Implicit(o.Parasite)
					select o.Ref);
				break;
			case PlayerPrimaryRolePower.Investigator:
				primaryRoleTargetRef = PrimaryRoleTargetRef;
				if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone)
				{
					playersWithSpecificColor.Add(PrimaryRoleTargetRef);
				}
				break;
			}
			if (PlayersWithSpecificColor.Any((PlayerRef o) => !playersWithSpecificColor.Contains(o)) || playersWithSpecificColor.Any((PlayerRef o) => !PlayersWithSpecificColor.Contains(o)))
			{
				UpdatePlayersWithSpecificColor(playersWithSpecificColor);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("FixedUpdateNetwork error: " + ex));
		}
	}

	public void IncreaseHealth(float amount)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Invalid comparison between Unknown and I4
		if (!NetworkBool.op_Implicit(Dying) && (NewPrimaryRole != PlayerNewPrimaryRole.Lover || (int)PlayerController.Role != 0))
		{
			PlayerController.Hunger = Mathf.Min((float)GameManager.Instance.MaxHunger, PlayerController.Hunger + amount);
		}
	}

	public void UpdateDescriptionStatusIfNeeded()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		if (!NetworkBool.op_Implicit(PlayerController.Local.IsDead) && !NetworkBool.op_Implicit(Local.Kidnapped))
		{
			if (PlayerController.Local.Ref == Ref)
			{
				ShowRoleDescriptionPatch.NeedsUpdate = true;
			}
		}
		else if (PlayerController.Local.LocalCameraHandler.PovPlayer.Ref == Ref)
		{
			GameManager.Instance.gameUI.UpdateSpectatedUsername(((object)PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerData.Username/*cast due to constrained. prefix*/).ToString(), PlayerController.Local.LocalCameraHandler.PovPlayer.Role);
		}
	}

	public void GiveRandomItem()
	{
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)PlayerController.Item != (Object)null && (!(Accessory is AccessoryBackpack) || (Object)(object)(Accessory as AccessoryBackpack).ItemInside != (Object)null))
			{
				PlayerController.Item.DestroyItem();
			}
			if (Random.value > 0.4f)
			{
				Potion value = Traverse.Create((object)GameManager.Instance).Field<Potion>("potionPrefab").Value;
				List<Effect> value2 = Traverse.Create((object)GameManager.Instance).Field<List<Effect>>("_potionEffects").Value;
				if (value2.Count == 0)
				{
					return;
				}
				Effect randomEffect = CollectionsUtil.Grab<Effect>(value2.Select((Effect e) => e).ToList(), 1).First();
				Effect item = value2.First((Effect o) => ((object)o).GetType() == ((object)randomEffect).GetType());
				int localEffectIndex = value2.IndexOf(item);
				if ((Object)(object)randomEffect != (Object)null)
				{
					((Item)((SimulationBehaviour)this).Runner.Spawn<Potion>(value, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						((Component)no).GetComponent<Potion>().Init(localEffectIndex, EffectManager.GetEffectIndex(randomEffect));
					}, (NetworkObjectPredictionKey?)null, true)).Rpc_ClaimItem(Ref);
				}
				return;
			}
			Item[] value3 = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
			List<Item> list = value3.Where((Item o) => Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)]).ToList();
			if (list.Count != 0)
			{
				Item prefab = CollectionsUtil.Grab<Item>(list.Where((Item o) => o is LockItem || o is TrapItem || o is SmokeItem || o is SpyglassItem || o is MagicScrollItem || o is PhasingDiamondItem || o is GrenadeItem || o is SleepingGasItem || o is MolotovItem || o is RadarItem).ToList(), 1).First();
				Item val = ItemUtility.SpawnItem(prefab, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
				val.Rpc_ClaimItem(Ref);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GiveRandomItem error: " + ex));
		}
	}

	public void GiveScientistGadget()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Item[] value = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
			List<Item> list = value.Where((Item o) => Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)]).ToList();
			if (list.Count != 0)
			{
				Item prefab = CollectionsUtil.Grab<Item>(list.Where((Item o) => o is SmokeItem || o is GrenadeItem || o is SleepingGasItem || o is RadarItem).ToList(), 1).First();
				Item val = ItemUtility.SpawnItem(prefab, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
				val.Rpc_ClaimItem(Ref);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GiveScientistGadget error: " + ex));
		}
	}

	public void Server_Init(PlayerRef pRef, byte index)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Ref = pRef;
		Index = index;
	}

	public void Reset()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		PrimaryRoleTargetRef = PlayerRef.None;
		SecondaryRoleTargetRef = PlayerRef.None;
		RoleDeathUniqueBool = NetworkBool.op_Implicit(false);
		PrimaryRolePowerRemainingUses = 1;
		SecondaryRoleFirstRemainingUses = 1;
		PrimaryRolePowerCooldownTimer = TickTimer.None;
		SecondaryRolePowerCooldownTimer = TickTimer.None;
		SoloRoleObjectiveCount = 0;
		Disappeared = NetworkBool.op_Implicit(false);
		PoliticianVictimAlltime = NetworkBool.op_Implicit(false);
		StarvationDormant = false;
		StarvationActive = false;
		IllusionTarget = PlayerRef.None;
		Resurrected = NetworkBool.op_Implicit(false);
		CurseDormant = NetworkBool.op_Implicit(false);
		CurseTimer = TickTimer.None;
		SecondaryRolePowerActive = NetworkBool.op_Implicit(false);
		SecondaryRoleTeleportData = NetworkTeleportData.None;
		NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
		SabotageTimer = TickTimer.None;
		QuickSabotaging = NetworkBool.op_Implicit(false);
		SubtleSabotaging = NetworkBool.op_Implicit(false);
		PrimaryRoleActionTimer = TickTimer.None;
		SecondaryRoleActionTimer = TickTimer.None;
		TrapDisarmTimer = TickTimer.None;
		Petrified = NetworkBool.op_Implicit(false);
		Exorciser = PlayerRef.None;
		Exorcised = NetworkBool.op_Implicit(false);
		SurvivalistSaveTimer = TickTimer.None;
		SurvivalistSaveTargetPlayerRef = PlayerRef.None;
		SurvivalistBuff = NetworkBool.op_Implicit(false);
		Vampire = NetworkBool.op_Implicit(false);
		ProtectedPriest = NetworkBool.op_Implicit(false);
		Dying = NetworkBool.op_Implicit(false);
		Scavenged = NetworkBool.op_Implicit(false);
		BombDormant = NetworkBool.op_Implicit(false);
		BombActive = NetworkBool.op_Implicit(false);
		Panic = NetworkBool.op_Implicit(false);
		Fleeing = NetworkBool.op_Implicit(false);
		Downed = NetworkBool.op_Implicit(false);
		Predator = NetworkBool.op_Implicit(false);
		Empowered = NetworkBool.op_Implicit(false);
		Portal = NetworkBool.op_Implicit(false);
		BombTimer = TickTimer.None;
		PoacherMark = NetworkBool.op_Implicit(false);
		BeastMark = NetworkBool.op_Implicit(false);
		Asleep = NetworkBool.op_Implicit(false);
		Isolation = NetworkBool.op_Implicit(false);
		PrimaryRolePowerCurrentMaterials = 0;
		DeceiverTrickAllTime = NetworkBool.op_Implicit(false);
		DeceiverTrickThisMeeting = NetworkBool.op_Implicit(false);
		PrimaryRolePowerPlayersList.Clear();
		IsImitator = NetworkBool.op_Implicit(false);
		ImitatorChoicesForToday.Clear();
		CurrentMerchantOffers.Clear();
		DetectiveIntelList.Clear();
		MercenaryTargetsAlreadyHit.Clear();
		IsWolfPup = NetworkBool.op_Implicit(false);
		MayorVoteTarget = PlayerRef.None;
		PlacedSleepingGas = null;
		DiedFromNotBeingSaved = NetworkBool.op_Implicit(false);
		Banished = NetworkBool.op_Implicit(false);
		Repulsion = NetworkBool.op_Implicit(false);
		Burning = NetworkBool.op_Implicit(false);
		GainEmpoweredOnEachTransformation = false;
		SleepStacks = 0;
		RepulsionStacks = 0;
		LootCorpseTimer = TickTimer.None;
		LootCorpseTarget = PlayerRef.None;
		PurifierBurn = NetworkBool.op_Implicit(false);
		Tracked = NetworkBool.op_Implicit(false);
		Kidnapped = NetworkBool.op_Implicit(false);
		CapturedByCultist = NetworkBool.op_Implicit(false);
		Hidden = NetworkBool.op_Implicit(false);
		Parasite = NetworkBool.op_Implicit(false);
		Immune = NetworkBool.op_Implicit(false);
		LootCollectedTodayDuringDay = 0;
		MoleWarningIssued = false;
		SoloRoleHalfDayProgress = 0f;
		NewPrimaryRole = PlayerNewPrimaryRole.None;
		SecondaryRole = PlayerSecondaryRole.None;
		PrimaryRolePower = PlayerPrimaryRolePower.None;
		PlayerController.UpdateAnimation(Animator.StringToHash("Dead"), false);
		PlayerController.UpdateModel(false);
		((Component)PlayerController).GetComponent<PlayerResurrectedComponent>().UpdateState();
		((Component)PlayerController).GetComponent<PlayerSpotterLightComponent>().UpdateState();
		UpdateTargetArrowComponent();
		if ((Object)(object)AstralSpirit != (Object)null)
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)AstralSpirit).GetComponent<NetworkObject>(), false);
		}
		if (IsCurrentlyPlayedOrObserved)
		{
			UpdatePlayersWithSpecificColor(new List<PlayerRef>());
		}
		if ((Object)(object)Accessory != (Object)null)
		{
			((Item)Accessory).DestroyItem();
			Accessory = null;
		}
	}

	[Preserve]
	public static void ColorIndexChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.UpdateColor();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ColorIndexChanged error: " + ex));
		}
	}

	public void UpdateColor()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		PlayerController playerController = PlayerController;
		((Renderer)Traverse.Create((object)playerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).material.mainTexture = ColorManager.GetTexture(ColorIndex);
		Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
		if (value.ContainsKey(playerController.Ref))
		{
			PlayerDisplay val = value[playerController.Ref];
			((Graphic)Traverse.Create((object)val).Field<Image>("borderColor").Value).color = ColorManager.GetColor(ColorIndex);
			if (((SimulationBehaviour)playerController).HasInputAuthority)
			{
				PlayerPrefs.SetInt("FavoriteColor", ColorIndex);
			}
		}
	}

	public void UpdatePet()
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)PlayerController).HasInputAuthority)
		{
			PlayerPrefs.SetInt("FavoritePet", PetIndex);
		}
		if (!((SimulationBehaviour)this).Runner.IsServer)
		{
			return;
		}
		if ((Object)(object)CurrentPet != (Object)null)
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)CurrentPet).GetComponent<NetworkObject>(), false);
		}
		if (PetIndex > 0)
		{
			if (PetIndex > Plugin.PetNames.Count)
			{
				ManualLogSource logger = Plugin.Logger;
				NetworkString<_32> username = PlayerController.PlayerData.Username;
				logger.LogError((object)("Player " + ((object)username/*cast due to constrained. prefix*/).ToString() + " has incorrect pet!"));
				return;
			}
			Vector3 val = ((Component)PlayerController).transform.position - ((Component)PlayerController).transform.forward * 0.8f;
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject(Plugin.PetNames[PetIndex - 1]);
			NetworkObject val2 = ((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)val, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			((Component)val2).GetComponent<PlayerPetComponent>().Init(Ref, PetIndex);
			((Component)val2).transform.position = val;
			CurrentPet = ((Component)val2).GetComponent<PlayerPetComponent>();
		}
	}

	[Preserve]
	public static void PrimaryRolePowerRemainingUsesChanged(Changed<PlayerCustom> changed)
	{
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Invalid comparison between Unknown and I4
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Invalid comparison between Unknown and I4
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Invalid comparison between Unknown and I4
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Invalid comparison between Unknown and I4
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController playerController = behaviour.PlayerController;
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				behaviour.UpdateDescriptionStatusIfNeeded();
				PlayerNewPrimaryRole newPrimaryRole = behaviour.NewPrimaryRole;
				PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
				if ((playerNewPrimaryRole == PlayerNewPrimaryRole.Scientist || playerNewPrimaryRole == PlayerNewPrimaryRole.Beast) && behaviour.PrimaryRolePowerRemainingUses > 0 && ((int)GameManager.LocalGameState == 2 || (int)GameManager.LocalGameState == 3))
				{
					AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
				}
				switch (behaviour.PrimaryRolePower)
				{
				case PlayerPrimaryRolePower.Avenger:
					if (behaviour.PrimaryRolePowerCurrentMaterials >= behaviour.PowerMaterialsInfo.RequiredMaterials)
					{
						UIManager.ShowRedCenterMessage("NALES_UI_AVENGER_KILL_AVAILABLE", 0.4f, 4f);
						AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
					}
					break;
				case PlayerPrimaryRolePower.Deceiver:
				case PlayerPrimaryRolePower.Warlock:
				case PlayerPrimaryRolePower.Bomber:
				case PlayerPrimaryRolePower.Ritualist:
				case PlayerPrimaryRolePower.Exorcist:
				case PlayerPrimaryRolePower.Survivalist:
				case PlayerPrimaryRolePower.Priest:
				case PlayerPrimaryRolePower.Scout:
				case PlayerPrimaryRolePower.Runemaster:
					if (behaviour.PrimaryRolePowerRemainingUses > 0 && ((int)GameManager.LocalGameState == 2 || (int)GameManager.LocalGameState == 3))
					{
						AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
					}
					break;
				}
			}
			if (Local.Ref == behaviour.Ref && behaviour.PrimaryRolePower == PlayerPrimaryRolePower.Angel && behaviour.PrimaryRolePowerRemainingUses > 0)
			{
				AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRolePowerRemainingUsesChanged error: " + ex));
		}
	}

	public void AddMaterials(int amount)
	{
		if (amount > 0)
		{
			int num = Mathf.RoundToInt((float)PowerMaterialsInfo.RequiredMaterials * PowerMaterialsInfo.MaximumMaterialsPercentage);
			if (PrimaryRolePowerCurrentMaterials < num)
			{
				PrimaryRolePowerCurrentMaterials = Math.Min(PrimaryRolePowerCurrentMaterials + amount, num);
			}
		}
		else
		{
			PrimaryRolePowerCurrentMaterials = Math.Max(0, PrimaryRolePowerCurrentMaterials + amount);
		}
	}

	public void ReduceMaterialAfterPowerUse()
	{
		PrimaryRolePowerCurrentMaterials -= PowerMaterialsInfo.RequiredMaterials;
	}

	[Preserve]
	public static void PrimaryRolePowerCurrentMaterialsChanged(Changed<PlayerCustom> changed)
	{
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Invalid comparison between Unknown and I4
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom playerCustom = changed.Behaviour;
			PlayerController playerController = playerCustom.PlayerController;
			if (((SimulationBehaviour)playerCustom).Runner.IsServer && playerCustom.PowerMaterialsInfo != null && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials && playerCustom.PrimaryRolePowerRemainingUses == 0 && playerCustom.PrimaryRolePower != PlayerPrimaryRolePower.Peasant && playerCustom.PrimaryRolePower != PlayerPrimaryRolePower.Mystic && playerCustom.PrimaryRolePower != PlayerPrimaryRolePower.Shadow)
			{
				playerCustom.PrimaryRolePowerRemainingUses++;
			}
			if (((SimulationBehaviour)playerCustom).Runner.IsServer && playerCustom.PrimaryRolePower == PlayerPrimaryRolePower.Investigator && playerCustom.PrimaryRolePowerCurrentMaterials >= 100)
			{
				int num = 2;
				while (num > 0)
				{
					List<PlayerDetectiveIntel.PlayerDetectiveIntelType> list = new List<PlayerDetectiveIntel.PlayerDetectiveIntelType>();
					foreach (KeyValuePair<PlayerDetectiveIntel.PlayerDetectiveIntelType, int> item in BalancingValues.InvestigatorChanceByIntelType)
					{
						for (int i = 0; i < item.Value; i++)
						{
							list.Add(item.Key);
						}
					}
					if (!PlayerDetectiveIntelDifferentSides.CanGet(playerCustom))
					{
						list.RemoveAll((PlayerDetectiveIntel.PlayerDetectiveIntelType o) => o == PlayerDetectiveIntel.PlayerDetectiveIntelType.DifferentSides);
					}
					if (!PlayerDetectiveIntelOneIsEvil.CanGet(playerCustom))
					{
						list.RemoveAll((PlayerDetectiveIntel.PlayerDetectiveIntelType o) => o == PlayerDetectiveIntel.PlayerDetectiveIntelType.OneIsEvil);
					}
					if (!PlayerDetectiveIntelTransformationsAndDetransformations.CanGet(playerCustom))
					{
						list.RemoveAll((PlayerDetectiveIntel.PlayerDetectiveIntelType o) => o == PlayerDetectiveIntel.PlayerDetectiveIntelType.TransformationsAndDetransformations);
					}
					if (!PlayerDetectiveIntelWolvesAndSoloRolesRemaining.CanGet(playerCustom))
					{
						list.RemoveAll((PlayerDetectiveIntel.PlayerDetectiveIntelType o) => o == PlayerDetectiveIntel.PlayerDetectiveIntelType.WolvesAndSoloRolesRemaining);
					}
					int num2 = Mathf.RoundToInt(6f / (1f + (float)PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j is PlayerDetectiveIntelNotWolf playerDetectiveIntelNotWolf && playerDetectiveIntelNotWolf.Target == o.Ref))));
					for (int num3 = 0; num3 < num2; num3++)
					{
						list.Add(PlayerDetectiveIntel.PlayerDetectiveIntelType.NotWolf);
					}
					bool flag = false;
					int num4 = 0;
					while (!flag && num4 < 100)
					{
						num4++;
						if (!list.Any())
						{
							break;
						}
						PlayerDetectiveIntel.PlayerDetectiveIntelType playerDetectiveIntelType = CollectionsUtil.Grab<PlayerDetectiveIntel.PlayerDetectiveIntelType>(list, 1).First();
						int num5 = 0;
						switch (playerDetectiveIntelType)
						{
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.OneIsEvil:
							num5 = PlayerDetectiveIntelOneIsEvil.Cost;
							break;
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.DifferentSides:
							num5 = PlayerDetectiveIntelDifferentSides.Cost;
							break;
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.NotWolf:
							num5 = PlayerDetectiveIntelNotWolf.Cost;
							break;
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.TransformationsAndDetransformations:
							num5 = PlayerDetectiveIntelTransformationsAndDetransformations.Cost;
							break;
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.WolvesAndSoloRolesRemaining:
							num5 = PlayerDetectiveIntelWolvesAndSoloRolesRemaining.Cost;
							break;
						}
						if (num5 > num)
						{
							continue;
						}
						switch (playerDetectiveIntelType)
						{
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.OneIsEvil:
						{
							LycansUtility.AddLogOnlyForMe("Try to find detective OneIsEvil");
							int num6 = 0;
							while (!flag && num6 < 20)
							{
								num6++;
								List<PlayerCustom> list2 = new List<PlayerCustom>();
								List<PlayerCustom> list3 = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != playerCustom.Ref && !IsPrimaryRolePowerForEliteVillagers(o.PrimaryRolePower) && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j.Type == PlayerDetectiveIntel.PlayerDetectiveIntelType.OneIsEvil && (j as PlayerDetectiveIntelOneIsEvil).ContainsSpecificPlayer(o.Ref))).ToList();
								if (playerCustom.SecondaryRole == PlayerSecondaryRole.BothTelepath)
								{
									list3.RemoveAll((PlayerCustom o) => o.SecondaryRole == PlayerSecondaryRole.BothTelepath);
								}
								int num7 = BalancingValues.DetectiveOneIsEvilPlayersToAdd(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))));
								if (list3.Count < num7)
								{
									LycansUtility.AddLogOnlyForMe("Not enough players available for OneIsEvil");
									continue;
								}
								IEnumerable<PlayerRef> enumerable = from o in CollectionsUtil.Grab<PlayerCustom>(list3, num7)
									select o.Ref;
								foreach (PlayerRef item2 in enumerable)
								{
									list2.Add(PlayerCustomRegistry.GetPlayer(item2));
								}
								if (list2.Any((PlayerCustom o) => o.NewPrimaryRole != PlayerNewPrimaryRole.None || (int)o.PlayerController.Role == 1))
								{
									flag = true;
									num -= PlayerDetectiveIntelOneIsEvil.Cost;
									Rpc_Detective_Intel_One_Is_Evil(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, (list2.Count >= 1) ? list2[0].Index : (-1), (list2.Count >= 2) ? list2[1].Index : (-1), (list2.Count >= 3) ? list2[2].Index : (-1));
								}
							}
							break;
						}
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.DifferentSides:
						{
							LycansUtility.AddLogOnlyForMe("Try to find detective DifferentSides");
							List<PlayerCustom> list4 = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != playerCustom.Ref && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Resurrected) && !IsPrimaryRolePowerForEliteVillagers(o.PrimaryRolePower) && o.NewPrimaryRole != PlayerNewPrimaryRole.Zombie && o.PrimaryRolePower != PlayerPrimaryRolePower.Avenger && (o.SecondaryRole != PlayerSecondaryRole.BothSherif || (int)o.PlayerController.Role == 1) && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j is PlayerDetectiveIntelDifferentSides playerDetectiveIntelDifferentSides && playerDetectiveIntelDifferentSides.PlayerRefs.Contains(o.Ref)) && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j is PlayerDetectiveIntelNotWolf playerDetectiveIntelNotWolf && playerDetectiveIntelNotWolf.Target == o.Ref)).ToList();
							if (!list4.Any())
							{
								LycansUtility.AddLogOnlyForMe("No player available for DifferentSides");
								break;
							}
							PlayerCustom playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(list4, 1).First();
							list4.Remove(playerCustom2);
							if ((int)playerCustom2.PlayerController.Role == 1 || playerCustom2.NewPrimaryRole == PlayerNewPrimaryRole.Traitor)
							{
								list4.RemoveAll((PlayerCustom o) => (int)o.PlayerController.Role == 1 || o.NewPrimaryRole == PlayerNewPrimaryRole.Traitor);
							}
							else if (playerCustom2.NewPrimaryRole == PlayerNewPrimaryRole.Lover)
							{
								list4.RemoveAll((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Lover);
							}
							else if (playerCustom2.NewPrimaryRole == PlayerNewPrimaryRole.None)
							{
								list4.RemoveAll((PlayerCustom o) => (int)o.PlayerController.Role != 1 && o.NewPrimaryRole == PlayerNewPrimaryRole.None);
							}
							if (playerCustom.SecondaryRole == PlayerSecondaryRole.BothTelepath)
							{
								list4.RemoveAll((PlayerCustom o) => o.SecondaryRole == PlayerSecondaryRole.BothTelepath);
							}
							if (list4.Any())
							{
								flag = true;
								num -= PlayerDetectiveIntelDifferentSides.Cost;
								PlayerCustom playerCustom3 = CollectionsUtil.Grab<PlayerCustom>(list4, 1).First();
								Rpc_Detective_Intel_Different_Sides(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, playerCustom2.Index, playerCustom3.Index);
							}
							break;
						}
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.NotWolf:
						{
							LycansUtility.AddLogOnlyForMe("Try to find detective NotWolf");
							List<PlayerCustom> list5 = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != playerCustom.Ref && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j is PlayerDetectiveIntelNotWolf playerDetectiveIntelNotWolf && playerDetectiveIntelNotWolf.Target == o.Ref) && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (int)o.PlayerController.Role != 1 && !IsPrimaryRolePowerForEliteVillagers(o.PrimaryRolePower) && o.NewPrimaryRole != PlayerNewPrimaryRole.VillageIdiot && o.NewPrimaryRole != PlayerNewPrimaryRole.Zombie).ToList();
							if (playerCustom.SecondaryRole == PlayerSecondaryRole.BothTelepath)
							{
								list5.RemoveAll((PlayerCustom o) => o.SecondaryRole == PlayerSecondaryRole.BothTelepath);
							}
							if (list5.Any())
							{
								flag = true;
								num -= PlayerDetectiveIntelNotWolf.Cost;
								PlayerCustom playerCustom4 = CollectionsUtil.Grab<PlayerCustom>(list5, 1).First();
								Rpc_Detective_Intel_Not_Wolf(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, playerCustom4.Index);
							}
							break;
						}
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.TransformationsAndDetransformations:
						{
							LycansUtility.AddLogOnlyForMe("Try to find detective TransformationsAndDetransformations");
							flag = true;
							num -= PlayerDetectiveIntelTransformationsAndDetransformations.Cost;
							int key = GameManagerCustom.Instance.CurrentDay - 1;
							Rpc_Detective_Intel_Transformations_And_Detransformations(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, GameManagerCustom.Instance.TransformationsAmountByDay.ContainsKey(key) ? GameManagerCustom.Instance.TransformationsAmountByDay[key] : 0, GameManagerCustom.Instance.DetransformationsAmountByDay.ContainsKey(key) ? GameManagerCustom.Instance.DetransformationsAmountByDay[key] : 0);
							break;
						}
						case PlayerDetectiveIntel.PlayerDetectiveIntelType.WolvesAndSoloRolesRemaining:
							LycansUtility.AddLogOnlyForMe("Try to find detective Remaining");
							flag = true;
							num -= PlayerDetectiveIntelWolvesAndSoloRolesRemaining.Cost;
							Rpc_Detective_Intel_Wolves_And_Solo_Roles_Remaining(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Resurrected) && (int)o.PlayerController.Role == 1), PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.NewPrimaryRole != PlayerNewPrimaryRole.None && o.NewPrimaryRole != PlayerNewPrimaryRole.Traitor && o.NewPrimaryRole != PlayerNewPrimaryRole.Zombie));
							break;
						}
					}
					if (!flag)
					{
						Plugin.Logger.LogError((object)"Could not find valid intel for detective!");
						playerCustom.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)playerCustom).Runner, 10f);
						num--;
					}
				}
				playerCustom.PrimaryRolePowerCurrentMaterials -= 100;
			}
			if (playerCustom.IsCurrentlyPlayedOrObserved)
			{
				playerCustom.UpdateDescriptionStatusIfNeeded();
				PlayerPrimaryRolePower primaryRolePower = playerCustom.PrimaryRolePower;
				PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
				if (playerPrimaryRolePower == PlayerPrimaryRolePower.Possessor && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
				{
					AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRolePowerCurrentMaterialsChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SecondaryRoleFirstRemainingUsesChanged(Changed<PlayerCustom> changed)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Invalid comparison between Unknown and I4
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Invalid comparison between Unknown and I4
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController playerController = behaviour.PlayerController;
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				behaviour.UpdateDescriptionStatusIfNeeded();
				if (behaviour.SecondaryRoleFirstRemainingUses > 0 && ((int)GameManager.LocalGameState == 2 || (int)GameManager.LocalGameState == 3))
				{
					AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleFirstRemainingUsesChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SecondaryRoleSecondRemainingUsesChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController playerController = behaviour.PlayerController;
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				behaviour.UpdateDescriptionStatusIfNeeded();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleSecondRemainingUsesChanged error: " + ex));
		}
	}

	[Preserve]
	public static void IsWolfPupChanged(Changed<PlayerCustom> changed)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Invalid comparison between Unknown and I4
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Invalid comparison between Unknown and I4
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
			PlayerController playerController = behaviour.PlayerController;
			if ((Object)(object)playerController == (Object)(object)povPlayer)
			{
				behaviour.UpdatePrimaryRole();
				if (!NetworkBool.op_Implicit(behaviour.IsWolfPup) && (int)GameManager.LocalGameState != 0 && (int)GameManager.LocalGameState != 1 && (int)GameManager.LocalGameState != 5)
				{
					UIManager.ShowRedCenterMessage("NALES_UI_WOLF_PUP_CAN_TRANSFORM", 0.7f, 4f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewPrimaryRoleChanged error: " + ex));
		}
	}

	[Preserve]
	public static void NewPrimaryRoleChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
			PlayerController playerController = behaviour.PlayerController;
			if ((Object)(object)playerController == (Object)(object)povPlayer)
			{
				behaviour.UpdatePrimaryRole();
			}
			((Component)playerController).GetComponent<PlayerResurrectedComponent>().UpdateState();
			behaviour.UpdateMoveSpeed();
			if (((SimulationBehaviour)behaviour).Runner.IsServer && (Object)(object)behaviour.SummonedSpirit != (Object)null)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)behaviour.SummonedSpirit).GetComponent<NetworkObject>(), false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewPrimaryRoleChanged error: " + ex));
		}
	}

	[Preserve]
	public static void PrimaryRolePowerChanged(Changed<PlayerCustom> changed)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
			PlayerController playerController = behaviour.PlayerController;
			if (playerController.Ref == povPlayer.Ref)
			{
				behaviour.UpdatePrimaryRole();
				behaviour.UpdatePredatorComponent();
				behaviour.UpdateTargetArrowComponent();
			}
			if (playerController.Ref == PlayerController.Local.Ref)
			{
				switch (behaviour.PrimaryRolePower)
				{
				case PlayerPrimaryRolePower.Angel:
					behaviour.UpdateDescriptionStatusIfNeeded();
					UIManager.ShowRedCenterMessage("NALES_UI_ANGEL_INFO", 0.5f, 5f);
					AudioManager.Play("Mayor", (MixerTarget)2, 0.35f, 1f);
					break;
				case PlayerPrimaryRolePower.Ghost:
					behaviour.UpdateDescriptionStatusIfNeeded();
					UIManager.ShowRedCenterMessage("NALES_UI_GHOST_INFO", 0.5f, 5f);
					AudioManager.Play("Mayor", (MixerTarget)2, 0.35f, 1f);
					break;
				case PlayerPrimaryRolePower.Specter:
					behaviour.UpdateDescriptionStatusIfNeeded();
					UIManager.ShowRedCenterMessage("NALES_UI_SPECTER_INFO", 0.5f, 5f);
					AudioManager.Play("SeerCorrectGuessSound", (MixerTarget)2, 0.35f, 1f);
					break;
				}
			}
			if (((SimulationBehaviour)behaviour).Runner.IsServer && (Object)(object)behaviour.SummonedSpirit != (Object)null && behaviour.PrimaryRolePower != PlayerPrimaryRolePower.Ghost && behaviour.PrimaryRolePower != PlayerPrimaryRolePower.Specter)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)behaviour.SummonedSpirit).GetComponent<NetworkObject>(), false);
			}
			behaviour.UpdateMoveSpeed();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRolePowerChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SecondaryRoleChanged(Changed<PlayerCustom> changed)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Invalid comparison between Unknown and I4
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
			PlayerController playerController = behaviour.PlayerController;
			if ((Object)(object)playerController == (Object)(object)povPlayer)
			{
				behaviour.UpdatePrimaryRole();
				behaviour.UpdateDescriptionStatusIfNeeded();
				behaviour.UpdateTargetArrowComponent();
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			if (player.SecondaryRole == PlayerSecondaryRole.BothMedium && ((int)player.PlayerController.Role == 1 || player.NewPrimaryRole == PlayerNewPrimaryRole.Traitor))
			{
				Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
				foreach (KeyValuePair<PlayerRef, PlayerDisplay> item in value)
				{
					TextMeshProUGUI value2 = Traverse.Create((object)item.Value).Field<TextMeshProUGUI>("username").Value;
					PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(item.Key);
					if (player2.SecondaryRole != PlayerSecondaryRole.None)
					{
						((TMP_Text)value2).text = ((object)player2.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + " - " + TranslationManager.Instance.GetTranslation("NALES_ROLE_" + GetSecondaryRoleString(player2.SecondaryRole));
					}
					else
					{
						((TMP_Text)value2).text = ((object)player2.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
					}
				}
			}
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.UpdateVisibility();
			}
			behaviour.ImitatorChoicesForToday.Clear();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleChanged error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Activate_Secondary_Role_Power_Without_Target(NetworkRunner runner, int playerIndex)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Invalid comparison between Unknown and I4
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Invalid comparison between Unknown and I4
		//IL_0bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef0: Invalid comparison between Unknown and I4
		//IL_0fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_110d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Invalid comparison between Unknown and I4
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_1050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1039: Unknown result type (might be due to invalid IL or missing references)
		//IL_1169: Unknown result type (might be due to invalid IL or missing references)
		//IL_116f: Invalid comparison between Unknown and I4
		//IL_0b7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0976: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Unknown result type (might be due to invalid IL or missing references)
		//IL_099e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Expected O, but got Unknown
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Invalid comparison between Unknown and I4
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Secondary_Role_Power_Without_Target(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom customPlayer = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerController playerController = customPlayer.PlayerController;
			if (customPlayer.IsOutOfTheWorld)
			{
				return;
			}
			if (runner.IsServer)
			{
				switch (customPlayer.SecondaryRole)
				{
				case PlayerSecondaryRole.BothAlcoholic:
				{
					if (customPlayer.SecondaryRoleFirstRemainingUses <= 0 || (Object)(object)playerController.Item == (Object)null || playerController.Item is Potion)
					{
						return;
					}
					List<Effect> value2 = Traverse.Create((object)GameManager.Instance).Field<List<Effect>>("_potionEffects").Value;
					Effect randomEffect = CollectionsUtil.Grab<Effect>(value2, 1).FirstOrDefault();
					if ((Object)(object)randomEffect != (Object)null)
					{
						playerController.Item.DestroyItem();
						Potion value3 = Traverse.Create((object)GameManager.Instance).Field<Potion>("potionPrefab").Value;
						Effect potionEffect = EffectManager.GetEffects().First((Effect o) => ((object)o).GetType() == ((object)randomEffect).GetType());
						int localEffectIndex = value2.FindIndex((Effect o) => ((object)o).GetType() == ((object)potionEffect).GetType());
						((Item)runner.Spawn<Potion>(value3, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							((Component)no).GetComponent<Potion>().Init(localEffectIndex, EffectManager.GetEffectIndex(potionEffect));
						}, (NetworkObjectPredictionKey?)null, true)).Rpc_ClaimItem(playerController.Ref);
					}
					customPlayer.SecondaryRoleFirstRemainingUses--;
					customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					break;
				}
				case PlayerSecondaryRole.BothInfected:
					if (customPlayer.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					if (NetworkBool.op_Implicit(customPlayer.PlayerController.IsWolf))
					{
						foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
						{
							if (NetworkBool.op_Implicit(allPlayer.PlayerController.IsWolf) || NetworkBool.op_Implicit(allPlayer.PlayerController.IsDead) || NetworkBool.op_Implicit(allPlayer.Dying))
							{
								continue;
							}
							float num3 = Vector3.Distance(((Component)playerController).transform.position, ((Component)allPlayer.PlayerController).transform.position);
							if (num3 < 30f)
							{
								Effect effect = EffectManager.GetEffects().First((Effect o) => o is FlatulenceEffect);
								ApplyEffectToPlayer(allPlayer.PlayerController, effect, runner, 0.1f);
							}
						}
					}
					else
					{
						foreach (NetworkId item in ((IEnumerable<NetworkId>)(object)customPlayer.PlayerController.PlayerEffectManager.ActiveEffects).ToList())
						{
							Effect val2 = ((SimulationBehaviour)customPlayer).Runner.TryGetNetworkedBehaviourFromNetworkedObjectRef<Effect>(item);
							if ((int)val2.GetEffectType() != 2)
							{
								continue;
							}
							if (val2 is CustomEffect customEffect)
							{
								if (customEffect.CanBeDispelled)
								{
									customPlayer.PlayerController.PlayerEffectManager.RemoveEffect(item);
								}
							}
							else if (val2 is ParanoiaEffect || val2 is GlowingEffect || val2 is FlatulenceEffect)
							{
								customPlayer.PlayerController.PlayerEffectManager.RemoveEffect(item);
							}
						}
						Rpc_Effect_On_Player(runner, customPlayer.Index, 1);
					}
					customPlayer.SecondaryRoleFirstRemainingUses--;
					customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					break;
				case PlayerSecondaryRole.BothSprinter:
					if (NetworkBool.op_Implicit(playerController.IsWolf))
					{
						if (customPlayer.SecondaryRoleFirstRemainingUses <= 0)
						{
							return;
						}
						ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectChasing", runner);
						customPlayer.SecondaryRoleFirstRemainingUses--;
						customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					}
					else if (playerController.Hunger >= (float)GameManager.Instance.MaxHunger * 0.5f && !NetworkBool.op_Implicit(customPlayer.Sprinting))
					{
						ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectSprinting", runner);
					}
					break;
				case PlayerSecondaryRole.BothTeleporter:
				{
					if (customPlayer.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					NetworkTeleportData secondaryRoleTeleportData = customPlayer.SecondaryRoleTeleportData;
					if (((NetworkTeleportData)(ref secondaryRoleTeleportData)).IsNone)
					{
						if (!NetworkBool.op_Implicit(playerController.IsClimbing))
						{
							customPlayer.SecondaryRoleTeleportData = new NetworkTeleportData(((Component)playerController).transform.position, ((Component)playerController).transform.rotation, true);
							if (((SimulationBehaviour)customPlayer.PlayerController).HasInputAuthority)
							{
								UIManager.ShowRedCenterMessage("NALES_UI_BEACON_PLACED", 0.35f, 3f);
							}
						}
					}
					else
					{
						customPlayer.SecondaryRoleActionTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)playerController).Runner, (float)customPlayer.SecondaryRolePowerCastTime.Value);
						playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
						playerController.IsAiming = NetworkBool.op_Implicit(false);
						customPlayer.SecondaryRoleFirstRemainingUses--;
						customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					}
					break;
				}
				case PlayerSecondaryRole.BothIllusionist:
					if (customPlayer.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					if ((int)playerController.Role == 1)
					{
						PlayerRegistry.ForEach((Action<PlayerController>)delegate(PlayerController p)
						{
							//IL_000b: Unknown result type (might be due to invalid IL or missing references)
							if ((Object)(object)p != (Object)null && !NetworkBool.op_Implicit(p.IsWolf))
							{
								ApplyEffectToPlayer(p, "LycansNewRoles.EffectIllusion", runner);
							}
						});
					}
					else
					{
						PlayerRegistry.ForEach((Action<PlayerController>)delegate(PlayerController p)
						{
							//IL_000b: Unknown result type (might be due to invalid IL or missing references)
							if ((Object)(object)p != (Object)null && NetworkBool.op_Implicit(p.IsWolf))
							{
								ApplyEffectToPlayer(p, "LycansNewRoles.EffectBlind", runner);
							}
						});
					}
					customPlayer.SecondaryRoleFirstRemainingUses--;
					customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					break;
				case PlayerSecondaryRole.BothSherif:
					if ((int)playerController.Role == 1)
					{
						ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectResistance", runner);
						customPlayer.SecondaryRoleFirstRemainingUses--;
						customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					}
					break;
				case PlayerSecondaryRole.BothGambler:
				{
					if (customPlayer.SecondaryRoleFirstRemainingUses <= 0 || !NetworkBool.op_Implicit(playerController.IsWolf))
					{
						return;
					}
					List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != playerController.Ref && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld).ToList();
					if (list.Count > 0)
					{
						PlayerCustom playerCustom = CollectionsUtil.Grab<PlayerCustom>(list, 1).First();
						playerController.CharacterMovementHandler.TeleportData = new NetworkTeleportData(((Component)playerCustom).transform.position, ((Component)playerCustom).transform.rotation, true);
						playerCustom.PlayerController.CharacterMovementHandler.TeleportData = new NetworkTeleportData(((Component)playerController).transform.position, ((Component)playerController).transform.rotation, true);
						NetworkBool isClimbing = playerController.IsClimbing;
						playerController.IsClimbing = playerCustom.PlayerController.IsClimbing;
						playerCustom.PlayerController.IsClimbing = isClimbing;
						GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("TELEPORT_END"), ((Component)playerController).transform.position, 20f, 1f);
						GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("TELEPORT_END"), ((Component)playerCustom).transform.position, 20f, 1f);
					}
					customPlayer.SecondaryRoleFirstRemainingUses--;
					customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					break;
				}
				case PlayerSecondaryRole.BothEngineer:
				{
					if (customPlayer.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					Item[] value = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
					List<Item> list3 = value.Where((Item o) => Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)]).ToList();
					if (list3.Count == 0)
					{
						return;
					}
					Item prefab2 = CollectionsUtil.Grab<Item>(list3.Where((Item o) => (o is LockItem || o is TrapItem || o is SmokeItem || o is SpyglassItem || o is PhasingDiamondItem || o is GrenadeItem || o is SleepingGasItem || o is MolotovItem || o is RadarItem) && ((Object)(object)playerController.Item == (Object)null || ((object)playerController.Item).GetType() != ((object)o).GetType())).ToList(), 1).First();
					if ((Object)(object)playerController.Item != (Object)null && (!(customPlayer.Accessory is AccessoryBackpack accessoryBackpack) || !((Object)(object)accessoryBackpack.ItemInside == (Object)null)))
					{
						playerController.Item.DestroyItem();
					}
					Item val6 = ItemUtility.SpawnItem(prefab2, Vector3.zero, Quaternion.identity, runner);
					val6.Rpc_ClaimItem(playerController.Ref);
					customPlayer.SecondaryRoleFirstRemainingUses--;
					customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					break;
				}
				case PlayerSecondaryRole.BothAstral:
					if (!NetworkBool.op_Implicit(customPlayer.SecondaryRolePowerActive) && customPlayer.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					if (NetworkBool.op_Implicit(customPlayer.SecondaryRolePowerActive))
					{
						customPlayer.SecondaryRoleFirstRemainingUses--;
						customPlayer.TriggerSecondaryRolePowerCooldown(runner);
						customPlayer.SecondaryRolePowerActive = NetworkBool.op_Implicit(false);
					}
					else
					{
						customPlayer.SecondaryRolePowerActive = NetworkBool.op_Implicit(true);
					}
					break;
				case PlayerSecondaryRole.BothActor:
					if (!NetworkBool.op_Implicit(customPlayer.SecondaryRolePowerActive) && customPlayer.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					if (NetworkBool.op_Implicit(customPlayer.SecondaryRolePowerActive))
					{
						customPlayer.SecondaryRoleFirstRemainingUses--;
						customPlayer.TriggerSecondaryRolePowerCooldown(runner);
						customPlayer.SecondaryRolePowerActive = NetworkBool.op_Implicit(false);
					}
					else
					{
						customPlayer.SecondaryRolePowerActive = NetworkBool.op_Implicit(true);
					}
					break;
				case PlayerSecondaryRole.BothScribe:
					if (customPlayer.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					customPlayer.SecondaryRoleFirstRemainingUses--;
					if ((Object)(object)playerController.Item == (Object)null || !(playerController.Item is MagicScrollItem))
					{
						Item val3 = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value.FirstOrDefault((Item o) => o is MagicScrollItem);
						if ((Object)(object)val3 == (Object)null)
						{
							return;
						}
						if ((Object)(object)playerController.Item != (Object)null)
						{
							playerController.Item.DestroyItem();
						}
						Item val4 = ItemUtility.SpawnItem(val3, Vector3.zero, Quaternion.identity, runner);
						val4.Rpc_ClaimItem(playerController.Ref);
						customPlayer.TriggerSecondaryRolePowerCooldown(runner);
					}
					else
					{
						(playerController.Item as MagicScrollItem).RandomizeEffect();
						customPlayer.TriggerSecondaryRolePowerCooldown(runner, secondCooldown: true);
					}
					break;
				case PlayerSecondaryRole.BothCarabineer:
					playerController.IsAiming = NetworkBool.op_Implicit(!NetworkBool.op_Implicit(playerController.IsAiming));
					break;
				case PlayerSecondaryRole.BothMerchant:
					if (!LycansUtility.GameActuallyInPlay)
					{
						return;
					}
					if (customPlayer.SecondaryRoleUniqueInt >= 3)
					{
						customPlayer.SecondaryRoleUniqueInt -= 3;
						customPlayer.GenerateMerchantOffers();
					}
					break;
				case PlayerSecondaryRole.BothTinkerer:
				{
					if ((int)GameManager.LocalGameState != 4 || customPlayer.SecondaryRoleUniqueInt == 0)
					{
						return;
					}
					List<Accessory> list2 = GameManagerCustom.SpawnableAccessories.Where((Accessory o) => Plugin.CustomConfig.AccessoriesAvailability[ItemUtility.ItemToTranslateKey((Item)(object)o)] && ((Object)(object)customPlayer.Accessory == (Object)null || ((object)customPlayer.Accessory).GetType() != ((object)o).GetType())).ToList();
					if (list2.Count == 0)
					{
						return;
					}
					Accessory prefab = CollectionsUtil.Grab<Accessory>(list2, 1).First();
					if ((Object)(object)customPlayer.Accessory != (Object)null)
					{
						((Item)customPlayer.Accessory).DestroyItem();
					}
					Item val5 = ItemUtility.SpawnItem((Item)(object)prefab, Vector3.zero, Quaternion.identity, runner);
					val5.Rpc_ClaimItem(playerController.Ref);
					customPlayer.SecondaryRoleUniqueInt = 0;
					break;
				}
				case PlayerSecondaryRole.BothTelepath:
					if (NetworkBool.op_Implicit(customPlayer.SecondaryRolePowerActive))
					{
						customPlayer.SecondaryRolePowerActive = NetworkBool.op_Implicit(false);
						Effect val = playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is TelepathyEffect);
						if ((Object)(object)val != (Object)null)
						{
							playerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
						}
					}
					else
					{
						customPlayer.SecondaryRolePowerActive = NetworkBool.op_Implicit(true);
						ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectTelepathy", runner);
					}
					break;
				}
			}
			switch (customPlayer.SecondaryRole)
			{
			case PlayerSecondaryRole.BothTeleporter:
				customPlayer.PlayerAnimations.SetLoopAnimation("CastingLoop", active: true);
				break;
			case PlayerSecondaryRole.BothInfected:
				customPlayer.PlayerAnimations.PlayNonLoopAnimation("Buff");
				break;
			case PlayerSecondaryRole.BothEngineer:
			case PlayerSecondaryRole.BothScribe:
				customPlayer.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackCall1H01_L");
				break;
			}
			if (!(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref == playerController.Ref))
			{
				return;
			}
			PlayerSecondaryRole secondaryRole = customPlayer.SecondaryRole;
			PlayerSecondaryRole playerSecondaryRole = secondaryRole;
			if (playerSecondaryRole != PlayerSecondaryRole.BothIllusionist || customPlayer.SecondaryRoleFirstRemainingUses <= 0 || (int)playerController.Role == 1)
			{
				return;
			}
			List<PlayerCustom> list4 = PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf)).ToList();
			foreach (PlayerCustom item2 in list4)
			{
				item2.FlashPlayer(Color.white);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Activate_Secondary_Role_Power_Without_Target error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Secondary_Role_Power_Without_Target(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Activate_Secondary_Role_Power_Without_Target_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Activate_Secondary_Role_Power_Without_Target(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Activate_Secondary_Role_Power_With_Target(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Invalid comparison between Unknown and I4
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Invalid comparison between Unknown and I4
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Invalid comparison between Unknown and I4
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1217: Unknown result type (might be due to invalid IL or missing references)
		//IL_1227: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Invalid comparison between Unknown and I4
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1263: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Invalid comparison between Unknown and I4
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1254: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b06: Expected O, but got Unknown
		//IL_0b11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b96: Expected O, but got Unknown
		//IL_0f7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1000: Unknown result type (might be due to invalid IL or missing references)
		//IL_1010: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c3: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
		PlayerController playerController = player.PlayerController;
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Secondary_Role_Power_With_Target(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (!PlayerCustomRegistry.HasPlayerWithIndex(playerIndex))
			{
				Plugin.Logger.LogInfo((object)"Does not have player index");
				return;
			}
			if (player.SecondaryRole == PlayerSecondaryRole.BothImitator)
			{
				if (runner.IsServer)
				{
					PlayerSecondaryRole playerSecondaryRole = Enum.Parse<PlayerSecondaryRole>(targetPlayerIndex.ToString());
					player.GiveSecondaryRole(playerSecondaryRole);
					if (playerSecondaryRole == PlayerSecondaryRole.BothSherif)
					{
						player.SecondaryRoleFirstRemainingUses = 1;
					}
				}
				if (player.Ref == PlayerController.Local.Ref)
				{
					PlaySuccessSound();
				}
				return;
			}
			PlayerCustom playerCustom = null;
			PlayerController targetPlayer = null;
			if (PlayerCustomRegistry.HasPlayerWithIndex(targetPlayerIndex))
			{
				playerCustom = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
				targetPlayer = playerCustom.PlayerController;
			}
			if (player.SecondaryRole == PlayerSecondaryRole.BothBlueMage)
			{
				playerCustom.FlashPlayer(Color.blue);
			}
			if (runner.IsServer)
			{
				Item[] value = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
				TickTimer val;
				switch (player.SecondaryRole)
				{
				case PlayerSecondaryRole.BothMetabolic:
					if ((int)playerController.Role != 1 || player.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					if ((int)targetPlayer.Role != 1)
					{
						playerCustom.StarvationDormant = true;
					}
					player.SecondaryRoleFirstRemainingUses--;
					player.TriggerSecondaryRolePowerCooldown(runner);
					break;
				case PlayerSecondaryRole.BothPolitician:
				{
					if (player.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
					player.SecondaryRoleTargetRef = player2.Ref;
					player.SecondaryRoleFirstRemainingUses--;
					break;
				}
				case PlayerSecondaryRole.BothSherif:
				{
					if (!LycansUtility.GameActuallyInPlay)
					{
						return;
					}
					PlayerController playerController2 = player.PlayerController;
					if ((int)playerController2.Role != 1 && !NetworkBool.op_Implicit(playerCustom.Petrified))
					{
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)playerController2).Runner, NetworkString<_16>.op_Implicit("SHOT_CLOSE"), ((Component)playerController2).transform.position, 30f, 1f);
						if ((int)targetPlayer.Role == 1 || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Traitor || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.VillageIdiot || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Agent || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Spy || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Scientist || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Lover || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Beast || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Voodoo || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Mercenary || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Kidnapper || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Cultist || playerCustom.NewPrimaryRole == PlayerNewPrimaryRole.Zombie)
						{
							playerCustom.Stats.UpdateDeathType("SHERIF_SUCCESS");
							targetPlayer.Rpc_Kill(playerController2.Ref);
							player.SecondaryRoleFirstRemainingUses--;
							player.TriggerSecondaryRolePowerCooldown(runner);
						}
						else
						{
							player.Stats.UpdateDeathType("SHERIF_MISTAKE");
							playerController2.Rpc_Kill(PlayerRef.None);
						}
					}
					break;
				}
				case PlayerSecondaryRole.BothGambler:
				{
					if (player.SecondaryRoleFirstRemainingUses <= 0 || NetworkBool.op_Implicit(playerController.IsWolf))
					{
						return;
					}
					if (NetworkBool.op_Implicit(targetPlayer.IsWolf))
					{
						if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
						{
							targetPlayer.Feed(Mathf.RoundToInt(0.15f * (float)GameManager.Instance.MaxHunger));
						}
						else
						{
							targetPlayer.Feed(Mathf.RoundToInt(0.5f * (float)GameManager.Instance.MaxHunger));
						}
					}
					else
					{
						targetPlayer.Feed(Mathf.RoundToInt(0.2f * (float)GameManager.Instance.MaxHunger));
					}
					List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != playerController.Ref && o.Ref != targetPlayer.Ref && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld).ToList();
					if (list.Count > 0)
					{
						PlayerCustom playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(list, 1).First();
						playerCustom2.PlayerController.CharacterMovementHandler.TeleportData = new NetworkTeleportData(((Component)targetPlayer).transform.position, ((Component)targetPlayer).transform.rotation, true);
						targetPlayer.CharacterMovementHandler.TeleportData = new NetworkTeleportData(((Component)playerCustom2).transform.position, ((Component)playerCustom2).transform.rotation, true);
						NetworkBool isClimbing = targetPlayer.IsClimbing;
						targetPlayer.IsClimbing = playerCustom2.PlayerController.IsClimbing;
						playerCustom2.PlayerController.IsClimbing = isClimbing;
						GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("TELEPORT_END"), ((Component)playerCustom2).transform.position, 20f, 1f);
						GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("TELEPORT_END"), ((Component)targetPlayer).transform.position, 20f, 1f);
					}
					player.SecondaryRoleFirstRemainingUses--;
					player.TriggerSecondaryRolePowerCooldown(runner);
					break;
				}
				case PlayerSecondaryRole.BothMedium:
					if (player.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					val = player.SecondaryRoleActionTimer;
					if (!((TickTimer)(ref val)).IsRunning)
					{
						player.SecondaryRoleTargetRef = playerCustom.Ref;
						player.SecondaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, (float)BalancingValues.GetCastTimeForSecondaryRole(PlayerSecondaryRole.BothMedium).Value);
						player.PlayerController.MovementAction = 1;
						player.PlayerController.UpdateAnimation(Animator.StringToHash("Crouching"), true);
						player.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
					}
					break;
				case PlayerSecondaryRole.BothScavenger:
					if (player.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					val = player.SecondaryRoleActionTimer;
					if (!((TickTimer)(ref val)).IsRunning)
					{
						player.SecondaryRoleTargetRef = playerCustom.Ref;
						player.SecondaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, (float)BalancingValues.GetCastTimeForSecondaryRole(PlayerSecondaryRole.BothScavenger).Value);
						player.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
					}
					break;
				case PlayerSecondaryRole.BothBlueMage:
				{
					if (player.SecondaryRoleFirstRemainingUses <= 0)
					{
						return;
					}
					Effect effect5 = EffectManager.GetEffect(player.SecondaryRoleUniqueInt);
					ApplyEffectToPlayer(targetPlayer, effect5, runner);
					Rpc_Effect_On_Player(runner, playerCustom.Index, 1);
					GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)targetPlayer).transform.position, 15f, 0.5f);
					player.SecondaryRoleFirstRemainingUses--;
					player.TriggerSecondaryRolePowerCooldown(runner);
					break;
				}
				case PlayerSecondaryRole.BothForger:
				{
					if ((Object)(object)targetPlayer.Item == (Object)null)
					{
						return;
					}
					val = targetPlayer.Item.TriggerTimer;
					if (((TickTimer)(ref val)).IsRunning)
					{
						return;
					}
					val = targetPlayer.Item.AnimationTimer;
					if (((TickTimer)(ref val)).IsRunning || !PlayerHeldItemComponent.CanSeeItem(player, targetPlayer.Item))
					{
						return;
					}
					if ((Object)(object)playerController.Item != (Object)null)
					{
						playerController.Item.DestroyItem();
					}
					Item item = targetPlayer.Item;
					if (Vector3.Distance(((Component)playerController).transform.position, ((Component)targetPlayer).transform.position) < 3f)
					{
						((NetworkBehaviour)item).CopyStateToBackingFields();
						Traverse.Create((object)item).Field("_Owner").SetValue((object)PlayerRef.None);
						((NetworkBehaviour)item).CopyBackingFieldsToState(true);
						((SimulationBehaviour)item).Object.AssignInputAuthority(PlayerRef.None);
						targetPlayer.Item = null;
						item.Rpc_ClaimItem(playerController.Ref);
						player.SecondaryRoleFirstRemainingUses--;
						player.TriggerSecondaryRolePowerCooldown(runner, secondCooldown: true);
						break;
					}
					Item obj = item;
					Potion potion = (Potion)(object)((obj is Potion) ? obj : null);
					if (potion != null)
					{
						Potion value6 = Traverse.Create((object)GameManager.Instance).Field<Potion>("potionPrefab").Value;
						List<Effect> value7 = Traverse.Create((object)GameManager.Instance).Field<List<Effect>>("_potionEffects").Value;
						Effect potionEffect = EffectManager.GetEffect(potion.EffectIndex);
						Effect item2 = value7.First((Effect o) => ((object)o).GetType() == ((object)potionEffect).GetType());
						int localEffectIndex = value7.IndexOf(item2);
						((Item)runner.Spawn<Potion>(value6, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							((Component)no).GetComponent<Potion>().Init(localEffectIndex, potion.EffectIndex);
						}, (NetworkObjectPredictionKey?)null, true)).Rpc_ClaimItem(playerController.Ref);
					}
					else if (item is BulletItem)
					{
						BulletItem value8 = Traverse.Create((object)GameManager.Instance).Field<BulletItem>("bulletPrefab").Value;
						Vector3? val2 = Vector3.zero;
						Quaternion? val3 = Quaternion.identity;
						object obj2 = _003C_003Ec._003C_003E9__548_4;
						if (obj2 == null)
						{
							OnBeforeSpawned val4 = delegate
							{
							};
							_003C_003Ec._003C_003E9__548_4 = val4;
							obj2 = (object)val4;
						}
						((Item)runner.Spawn<BulletItem>(value8, val2, val3, (PlayerRef?)null, (OnBeforeSpawned)obj2, (NetworkObjectPredictionKey?)null, true)).Rpc_ClaimItem(playerController.Ref);
					}
					else
					{
						Item prefab = value.First((Item o) => ((object)o).GetType() == ((object)item).GetType());
						Item val5 = ItemUtility.SpawnItem(prefab, Vector3.zero, Quaternion.identity, runner);
						val5.Rpc_ClaimItem(playerController.Ref);
						if (val5 is MagicScrollItem magicScrollItem)
						{
							magicScrollItem.EffectIndex = (item as MagicScrollItem).EffectIndex;
						}
					}
					player.SecondaryRoleFirstRemainingUses--;
					player.TriggerSecondaryRolePowerCooldown(runner);
					break;
				}
				case PlayerSecondaryRole.BothMerchant:
				{
					if (!LycansUtility.GameActuallyInPlay)
					{
						return;
					}
					MerchantOffer merchantOffer = player.CurrentMerchantOffers.First((MerchantOffer o) => o.Index == targetPlayerIndex);
					if (player.SecondaryRoleUniqueInt >= merchantOffer.Price)
					{
						player.MerchantDeliveryOfferIndex = merchantOffer.Index;
						player.SecondaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 5f);
						player.SecondaryRoleUniqueInt -= merchantOffer.Price;
					}
					break;
				}
				case PlayerSecondaryRole.BothTinkerer:
				{
					if (!LycansUtility.GameActuallyInPlay || player.SecondaryRoleFirstRemainingUses <= 0 || (Object)(object)player.Accessory == (Object)null)
					{
						return;
					}
					Accessory accessory = player.Accessory;
					Accessory accessory2 = accessory;
					if (!(accessory2 is AccessoryBoots))
					{
						if (!(accessory2 is AccessoryHorn))
						{
							if (!(accessory2 is AccessoryRing))
							{
								if (!(accessory2 is AccessoryMagnifier))
								{
									if (!(accessory2 is AccessoryCrystalBall))
									{
										if (!(accessory2 is AccessoryBackpack))
										{
											if (accessory2 is AccessorySpellbook)
											{
												Effect effect = CollectionsUtil.Grab<Effect>((from o in EffectManager.GetEffects()
													where Plugin.CustomConfig.PotionsAvailability[o.GetTranslateKey()] && (int)o.GetEffectType() == 0
													select o).ToList(), 1).First();
												ApplyEffectToPlayer(targetPlayer, effect, runner, 0.75f);
												Rpc_Effect_On_Player(runner, playerCustom.Index, 1);
												Rpc_Effect_On_Player(runner, playerCustom.Index, 3);
												GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)targetPlayer).transform.position, 15f, 0.5f);
											}
										}
										else
										{
											player.GiveRandomItem();
										}
									}
									else
									{
										float value2 = (NetworkBool.op_Implicit(targetPlayer.IsWolf) ? 45f : 45f);
										Effect effect2 = EffectManager.GetEffects().First((Effect o) => o is NightVision);
										ApplyEffectToPlayer(targetPlayer, effect2, runner, 1f, value2);
										Rpc_Effect_On_Player(runner, playerCustom.Index, 1);
										Rpc_Effect_On_Player(runner, playerCustom.Index, 3);
										GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)targetPlayer).transform.position, 15f, 0.5f);
									}
								}
								else
								{
									float value3 = (NetworkBool.op_Implicit(targetPlayer.IsWolf) ? 3f : 6f);
									ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectBlind", runner, 1f, value3);
									Rpc_Effect_On_Player(runner, playerCustom.Index, 1);
									Rpc_Effect_On_Player(runner, playerCustom.Index, 3);
									GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)targetPlayer).transform.position, 15f, 0.5f);
								}
							}
							else
							{
								float value4 = (NetworkBool.op_Implicit(targetPlayer.IsWolf) ? 10f : 20f);
								Effect effect3 = EffectManager.GetEffects().First((Effect o) => o is SpeedEffect);
								ApplyEffectToPlayer(targetPlayer, effect3, runner, 1f, value4);
								Rpc_Effect_On_Player(runner, playerCustom.Index, 1);
								Rpc_Effect_On_Player(runner, playerCustom.Index, 3);
								GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)targetPlayer).transform.position, 15f, 0.5f);
							}
						}
						else
						{
							playerCustom.IncreaseHealth(0.25f * (float)GameManager.Instance.MaxHunger);
						}
					}
					else
					{
						float value5 = (NetworkBool.op_Implicit(targetPlayer.IsWolf) ? 6f : 3f);
						Effect effect4 = EffectManager.GetEffects().First((Effect o) => o is InvisibilityEffect);
						ApplyEffectToPlayer(targetPlayer, effect4, runner, 1f, value5);
						Rpc_Effect_On_Player(runner, playerCustom.Index, 1);
						Rpc_Effect_On_Player(runner, playerCustom.Index, 3);
						GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)targetPlayer).transform.position, 15f, 0.5f);
					}
					player.SecondaryRoleFirstRemainingUses--;
					player.TriggerSecondaryRolePowerCooldown(runner);
					break;
				}
				}
			}
			switch (player.SecondaryRole)
			{
			case PlayerSecondaryRole.BothGambler:
			case PlayerSecondaryRole.BothBlueMage:
				player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackDirect1H01_R");
				break;
			case PlayerSecondaryRole.BothScavenger:
				player.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: true);
				break;
			case PlayerSecondaryRole.BothTeleporter:
			case PlayerSecondaryRole.BothMedium:
				player.PlayerAnimations.SetLoopAnimation("CastingLoop", active: true);
				break;
			case PlayerSecondaryRole.BothMerchant:
				player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackCall1H01_L");
				break;
			case PlayerSecondaryRole.BothTinkerer:
			{
				Accessory accessory3 = player.Accessory;
				Accessory accessory4 = accessory3;
				if (!(accessory4 is AccessoryHorn) && !(accessory4 is AccessoryCrystalBall) && !(accessory4 is AccessoryBackpack) && !(accessory4 is AccessorySpellbook))
				{
					if (accessory4 is AccessoryRing || accessory4 is AccessoryMagnifier)
					{
						player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackDirect1H01_R");
					}
				}
				else
				{
					player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackCall1H01_L");
				}
				break;
			}
			}
			if (PlayerController.Local.LocalCameraHandler.PovPlayer.Ref == playerController.Ref)
			{
				switch (player.SecondaryRole)
				{
				case PlayerSecondaryRole.BothMetabolic:
					playerCustom.FlashPlayer(Color.red);
					break;
				case PlayerSecondaryRole.BothPolitician:
					playerCustom.FlashPlayer(Color.black);
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Activate_Secondary_Role_Power_With_Target error: " + ex?.ToString() + ", secondary role: " + player.SecondaryRole));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Secondary_Role_Power_With_Target(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Activate_Secondary_Role_Power_With_Target_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Activate_Secondary_Role_Power_With_Target(runner, playerIndex, targetPlayerIndex);
	}

	private void SecondaryRoleActionTimerExpired()
	{
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Invalid comparison between Unknown and I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Invalid comparison between Unknown and I4
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Invalid comparison between Unknown and I4
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Expected O, but got Unknown
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController playerController = PlayerController;
			switch (SecondaryRole)
			{
			case PlayerSecondaryRole.BothTeleporter:
				SecondaryRoleActionTimer = TickTimer.None;
				playerController.MovementAction = 0;
				UpdateCanMoveAnimation();
				if ((int)GameManager.LocalGameState == 2 && !NetworkBool.op_Implicit(playerController.IsDead) && !NetworkBool.op_Implicit(Dying) && ((SimulationBehaviour)this).Runner.IsServer)
				{
					CharacterMovementHandler characterMovementHandler = playerController.CharacterMovementHandler;
					NetworkTeleportData val2 = SecondaryRoleTeleportData;
					Vector3 position = ((NetworkTeleportData)(ref val2)).Position;
					val2 = SecondaryRoleTeleportData;
					Quaternion rotation = ((NetworkTeleportData)(ref val2)).Rotation;
					val2 = SecondaryRoleTeleportData;
					characterMovementHandler.TeleportData = new NetworkTeleportData(position, rotation, ((NetworkTeleportData)(ref val2)).ResetLook);
					SecondaryRoleTeleportData = NetworkTeleportData.None;
					playerController.IsClimbing = NetworkBool.op_Implicit(false);
					GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("TELEPORT_START"), ((Component)playerController).transform.position, 20f, 1f);
					NetworkRunner runner = ((SimulationBehaviour)this).Runner;
					NetworkString<_16> val3 = NetworkString<_16>.op_Implicit("TELEPORT_END");
					val2 = playerController.CharacterMovementHandler.TeleportData;
					GameManager.Rpc_BroadcastFollowSound(runner, val3, ((NetworkTeleportData)(ref val2)).Position, 20f, 1f);
					ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectTrapResistance", ((SimulationBehaviour)this).Runner);
					ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectDisoriented", ((SimulationBehaviour)playerController).Runner);
				}
				break;
			case PlayerSecondaryRole.BothMedium:
				SecondaryRoleActionTimer = TickTimer.None;
				playerController.MovementAction = 0;
				playerController.UpdateAnimation(Animator.StringToHash("Crouching"), false);
				UpdateCanMoveAnimation();
				if ((int)GameManager.LocalGameState != 2 || NetworkBool.op_Implicit(playerController.IsDead))
				{
					break;
				}
				if (IsCurrentlyPlayedOrObserved)
				{
					PlayerCustom targetPlayerCustom = PlayerCustomRegistry.GetPlayer(SecondaryRoleTargetRef);
					PlayerRef killer = targetPlayerCustom.PlayerController.Killer;
					if (((PlayerRef)(ref killer)).IsNone)
					{
						UIManager.ShowRedCenterMessage("NALES_UI_MEDIUM_ANALYSIS_FAILURE", 0.5f, 5f);
					}
					else
					{
						List<PlayerRef> list = new List<PlayerRef> { targetPlayerCustom.PlayerController.Killer };
						List<PlayerRef> list2 = (from o in PlayerCustomRegistry
							where o.Ref != Ref && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != targetPlayerCustom.PlayerController.Killer && !IsPrimaryRolePowerForEliteVillagers(o.PrimaryRolePower)
							select o.Ref).ToList();
						int num = Math.Min(list2.Count, BalancingValues.MediumAdditionalPlayersToAdd(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead)))));
						list.AddRange(CollectionsUtil.Grab<PlayerRef>(list2, num));
						list.Shuffle();
						string text = "";
						foreach (PlayerRef item2 in list)
						{
							if (text != "")
							{
								text += ", ";
							}
							text += ((object)PlayerRegistry.GetPlayer(item2).PlayerData.Username/*cast due to constrained. prefix*/).ToString();
						}
						UIManager.ShowRedCenterMessage("NALES_UI_MEDIUM_ANALYSIS", 0.5f, 8f, new List<object> { text });
					}
				}
				if (((SimulationBehaviour)this).Runner.IsServer)
				{
					SecondaryRoleFirstRemainingUses--;
				}
				break;
			case PlayerSecondaryRole.BothScavenger:
			{
				SecondaryRoleActionTimer = TickTimer.None;
				playerController.MovementAction = 0;
				UpdateCanMoveAnimation();
				if ((int)GameManager.LocalGameState != 2 || NetworkBool.op_Implicit(playerController.IsDead))
				{
					break;
				}
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(SecondaryRoleTargetRef);
				if (NetworkBool.op_Implicit(player.Scavenged))
				{
					return;
				}
				player.Scavenged = NetworkBool.op_Implicit(true);
				if (!((SimulationBehaviour)this).Runner.IsServer)
				{
					break;
				}
				SecondaryRoleFirstRemainingUses--;
				TriggerSecondaryRolePowerCooldown(((SimulationBehaviour)this).Runner);
				if (NetworkBool.op_Implicit(playerController.IsWolf))
				{
					PlayerCustom specificPrimaryRolePower = PlayerCustomRegistry.GetSpecificPrimaryRolePower(PlayerPrimaryRolePower.Necromancer);
					if ((!((Object)(object)specificPrimaryRolePower != (Object)null) || !(specificPrimaryRolePower.PrimaryRoleTargetRef == SecondaryRoleTargetRef)) && player.NewPrimaryRole != PlayerNewPrimaryRole.Zombie)
					{
						IncreaseHealth((float)GameManager.Instance.MaxHunger * 0.75f);
						player.Disappeared = NetworkBool.op_Implicit(true);
					}
				}
				else
				{
					IncreaseHealth((float)GameManager.Instance.MaxHunger * 0.25f);
					GiveRandomItem();
				}
				break;
			}
			case PlayerSecondaryRole.BothMerchant:
			{
				if (!LycansUtility.GameActuallyInPlay)
				{
					return;
				}
				if (!((SimulationBehaviour)this).Runner.IsServer)
				{
					break;
				}
				MerchantOffer offer = CurrentMerchantOffers.First((MerchantOffer o) => o.Index == MerchantDeliveryOfferIndex);
				Item[] value = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
				switch (offer.Type)
				{
				case MerchantOffer.MerchantOfferType.Scroll:
				{
					Item prefab2 = value.First((Item o) => o is MagicScrollItem);
					MagicScrollItem magicScrollItem = (MagicScrollItem)(object)ItemUtility.SpawnItem(prefab2, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
					((Item)magicScrollItem).Rpc_ClaimItem(playerController.Ref);
					magicScrollItem.EffectIndex = offer.TypeIndex.Value;
					break;
				}
				case MerchantOffer.MerchantOfferType.OtherGadget:
				{
					Item prefab;
					switch (offer.TypeIndex.Value)
					{
					default:
						return;
					case 0:
						prefab = value.First((Item o) => o is LockItem);
						break;
					case 1:
						prefab = value.First((Item o) => o is TrapItem);
						break;
					case 2:
						prefab = value.First((Item o) => o is SmokeItem);
						break;
					case 3:
						prefab = value.First((Item o) => o is SpyglassItem);
						break;
					case 4:
						prefab = value.First((Item o) => o is PhasingDiamondItem);
						break;
					case 5:
						prefab = value.First((Item o) => o is GrenadeItem);
						break;
					case 6:
						prefab = value.First((Item o) => o is SleepingGasItem);
						break;
					case 7:
						prefab = value.First((Item o) => o is MolotovItem);
						break;
					case 8:
						prefab = value.First((Item o) => o is RadarItem);
						break;
					}
					Item val = ItemUtility.SpawnItem(prefab, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
					val.Rpc_ClaimItem(playerController.Ref);
					break;
				}
				case MerchantOffer.MerchantOfferType.Potion:
				{
					Potion value3 = Traverse.Create((object)GameManager.Instance).Field<Potion>("potionPrefab").Value;
					List<Effect> value4 = Traverse.Create((object)GameManager.Instance).Field<List<Effect>>("_potionEffects").Value;
					Effect potionEffect = EffectManager.GetEffects().First((Effect o) => BalancingValues.GetModifiedEffectData(o).RealIndex == offer.TypeIndex.Value);
					Effect item = value4.First((Effect o) => ((object)o).GetType() == ((object)potionEffect).GetType());
					int localEffectIndex = value4.IndexOf(item);
					((Item)((SimulationBehaviour)this).Runner.Spawn<Potion>(value3, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						((Component)no).GetComponent<Potion>().Init(localEffectIndex, EffectManager.GetEffectIndex(potionEffect));
					}, (NetworkObjectPredictionKey?)null, true)).Rpc_ClaimItem(playerController.Ref);
					break;
				}
				case MerchantOffer.MerchantOfferType.ImmediateEffect:
				{
					Effect effect = EffectManager.GetEffect(offer.TypeIndex.Value);
					float value2 = Traverse.Create((object)effect).Field<float>("duration").Value;
					ApplyEffectToPlayer(playerController, effect, ((SimulationBehaviour)this).Runner, 0.75f);
					Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, Index, 1);
					GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)playerController).transform.position, 15f, 0.5f);
					break;
				}
				case MerchantOffer.MerchantOfferType.PriestProtection:
					ProtectedPriest = NetworkBool.op_Implicit(true);
					break;
				case MerchantOffer.MerchantOfferType.Heal:
					IncreaseHealth((float)GameManager.Instance.MaxHunger * 0.5f);
					break;
				}
				SecondaryRoleActionTimer = TickTimer.None;
				GenerateMerchantOffers();
				break;
			}
			}
			switch (SecondaryRole)
			{
			case PlayerSecondaryRole.BothScavenger:
				PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
				break;
			case PlayerSecondaryRole.BothTeleporter:
			case PlayerSecondaryRole.BothMedium:
				PlayerAnimations.SetLoopAnimation("CastingLoop", active: false);
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleActionTimerExpired error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Activate_Primary_Role_Power_Without_Target(NetworkRunner runner, int playerIndex)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Invalid comparison between Unknown and I4
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0990: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0921: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1008: Unknown result type (might be due to invalid IL or missing references)
		//IL_1016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d71: Expected O, but got Unknown
		//IL_0d7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Expected O, but got Unknown
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Primary_Role_Power_Without_Target(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom customPlayer = PlayerCustomRegistry.GetPlayer(playerIndex);
			if (customPlayer.IsOutOfTheWorld)
			{
				return;
			}
			if (runner.IsServer)
			{
				PlayerController playerController = customPlayer.PlayerController;
				TickTimer primaryRoleActionTimer;
				switch (customPlayer.PrimaryRolePower)
				{
				case PlayerPrimaryRolePower.Avatar:
					if (customPlayer.PlayerController.Hunger >= 0.5f * (float)GameManager.Instance.MaxHunger)
					{
						PlayerController playerController2 = customPlayer.PlayerController;
						playerController2.Hunger -= 0.25f * (float)GameManager.Instance.MaxHunger;
						customPlayer.GiveRandomItem();
					}
					break;
				case PlayerPrimaryRolePower.Peasant:
					if (!NetworkBool.op_Implicit(customPlayer.NewPrimaryRoleUniqueBool) && (float)customPlayer.PrimaryRolePowerCurrentMaterials >= 2500f && playerController.MovementAction != 2 && !NetworkBool.op_Implicit(playerController.IsWolf))
					{
						customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
						playerController.MovementAction = 1;
						playerController.IsAiming = NetworkBool.op_Implicit(false);
						playerController.UpdateAnimation(Animator.StringToHash("Crouching"), true);
					}
					break;
				case PlayerPrimaryRolePower.Exorcist:
					if (customPlayer.PrimaryRolePowerRemainingUses > 0 && NetworkBool.op_Implicit(playerController.CanMove))
					{
						primaryRoleActionTimer = customPlayer.PrimaryRoleActionTimer;
						if (((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
						{
							return;
						}
						customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 3f);
						playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
						break;
					}
					return;
				case PlayerPrimaryRolePower.Survivalist:
					if (!NetworkBool.op_Implicit(customPlayer.SurvivalistBuff) && customPlayer.PrimaryRolePowerRemainingUses > 0)
					{
						primaryRoleActionTimer = customPlayer.PrimaryRoleActionTimer;
						if (((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
						{
							return;
						}
						customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 3f);
						playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
						break;
					}
					return;
				case PlayerPrimaryRolePower.Scout:
					if (customPlayer.PrimaryRolePowerRemainingUses > 0 && NetworkBool.op_Implicit(playerController.CanMove))
					{
						primaryRoleActionTimer = customPlayer.PrimaryRoleActionTimer;
						if (((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
						{
							return;
						}
						customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 2f);
						playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
						break;
					}
					return;
				case PlayerPrimaryRolePower.Magician:
					if (customPlayer.PrimaryRolePowerCurrentMaterials < customPlayer.PowerMaterialsInfo.RequiredMaterials || !NetworkBool.op_Implicit(playerController.CanMove) || !LycansUtility.GameActuallyInPlay)
					{
						return;
					}
					customPlayer.ReduceMaterialAfterPowerUse();
					if (runner.IsServer)
					{
						NetworkPrefabId networkObject2 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectMagicianBeaconName");
						Vector3 position2 = ((Component)customPlayer.PlayerController).transform.position;
						NetworkObject val2 = runner.Spawn(networkObject2, (Vector3?)((Component)customPlayer.PlayerController).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							//IL_0008: Unknown result type (might be due to invalid IL or missing references)
							((Component)no).transform.position = position2;
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val2).transform.position = position2;
						((Component)val2).transform.forward = ((Component)customPlayer.PlayerController).transform.forward;
						((Component)val2).GetComponent<MagicianBeacon>().SetCreatorRef(customPlayer.Ref);
					}
					break;
				case PlayerPrimaryRolePower.Mystic:
					if (customPlayer.PrimaryRolePowerCurrentMaterials >= customPlayer.PowerMaterialsInfo.RequiredMaterials && customPlayer.CanPerformActions)
					{
						primaryRoleActionTimer = customPlayer.PrimaryRoleActionTimer;
						if (((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
						{
							return;
						}
						customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 1f);
						playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
						break;
					}
					return;
				case PlayerPrimaryRolePower.Shadow:
					if (!NetworkBool.op_Implicit(customPlayer.NewPrimaryRoleUniqueBool))
					{
						if (customPlayer.PrimaryRolePowerCurrentMaterials >= customPlayer.PowerMaterialsInfo.RequiredMaterials)
						{
							customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
							customPlayer.PrimaryRolePowerCurrentMaterials -= 2000;
						}
					}
					else
					{
						customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
					}
					break;
				case PlayerPrimaryRolePower.Hermit:
				{
					if (customPlayer.PrimaryRolePowerCurrentMaterials < customPlayer.PowerMaterialsInfo.RequiredMaterials || !NetworkBool.op_Implicit(playerController.CanMove) || !LycansUtility.GameActuallyInPlay)
					{
						return;
					}
					customPlayer.ReduceMaterialAfterPowerUse();
					if (!runner.IsServer)
					{
						break;
					}
					List<HermitHideout> list = (from o in Object.FindObjectsOfType<HermitHideout>()
						where o.CreatorRef == customPlayer.Ref
						select o).ToList();
					foreach (HermitHideout item in list)
					{
						item.RemainingDuration += 20;
					}
					break;
				}
				case PlayerPrimaryRolePower.Runemaster:
					if (NetworkBool.op_Implicit(playerController.CanMove))
					{
						primaryRoleActionTimer = customPlayer.PrimaryRoleActionTimer;
						if (((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
						{
							return;
						}
						customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 2f);
						playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
						break;
					}
					return;
				case PlayerPrimaryRolePower.Spotter:
					if (customPlayer.PrimaryRolePowerCurrentMaterials < customPlayer.PowerMaterialsInfo.RequiredMaterials || NetworkBool.op_Implicit(customPlayer.Spotter))
					{
						return;
					}
					ApplyEffectToPlayer(customPlayer.PlayerController, "LycansNewRoles.EffectSpotter", runner, 1f, 20f);
					customPlayer.ReduceMaterialAfterPowerUse();
					break;
				case PlayerPrimaryRolePower.Purifier:
				{
					if (customPlayer.PrimaryRolePowerCurrentMaterials < customPlayer.PowerMaterialsInfo.RequiredMaterials)
					{
						return;
					}
					NetworkPrefabId networkObject3 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemPurifierActive");
					Vector3 value = default(Vector3);
					((Vector3)(ref value))._002Ector(((Component)customPlayer.PlayerController).transform.position.x + ((Component)customPlayer.PlayerController).transform.forward.x / 2f, ((Component)customPlayer.PlayerController).transform.position.y + 1f, ((Component)customPlayer.PlayerController).transform.position.z + ((Component)customPlayer.PlayerController).transform.forward.z / 2f);
					NetworkObject val3 = runner.Spawn(networkObject3, (Vector3?)value, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					Vector3 forward = ((Component)customPlayer.PlayerController.LocalCameraHandler.LocalCamera).transform.forward;
					forward.y = Mathf.Min(1f, ((Component)customPlayer.PlayerController.LocalCameraHandler.LocalCamera).transform.forward.y + 0.1f);
					((Component)val3).GetComponent<PurifierActive>().Init(forward * 25f);
					GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("GrenadeThrow"), ((Component)customPlayer.PlayerController).transform.position, 15f, 0.8f);
					customPlayer.PlayerController.UpdateAnimation(Animator.StringToHash("Attacking"), true);
					((MonoBehaviour)customPlayer.PlayerController).StartCoroutine("WaitAndResetAttackAnimation");
					customPlayer.ReduceMaterialAfterPowerUse();
					break;
				}
				case PlayerPrimaryRolePower.Necromancer:
				{
					PlayerCustom player = PlayerCustomRegistry.GetPlayer(customPlayer.PrimaryRoleTargetRef);
					if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) || !NetworkBool.op_Implicit(customPlayer.NewPrimaryRoleUniqueBool) || player.NewPrimaryRole == PlayerNewPrimaryRole.Zombie || !NetworkBool.op_Implicit(player.PlayerController.IsDead) || customPlayer.PrimaryRolePowerCurrentMaterials < 10000)
					{
						return;
					}
					primaryRoleActionTimer = customPlayer.PrimaryRoleActionTimer;
					if (!((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
					{
						customPlayer.PrimaryRoleTargetRef = player.Ref;
						customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 2f);
						playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
					}
					break;
				}
				case PlayerPrimaryRolePower.Possessor:
				{
					PlayerRef primaryRoleTargetRef = customPlayer.PrimaryRoleTargetRef;
					if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && customPlayer.PrimaryRolePowerCurrentMaterials >= customPlayer.PowerMaterialsInfo.RequiredMaterials)
					{
						customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(!NetworkBool.op_Implicit(customPlayer.NewPrimaryRoleUniqueBool));
					}
					break;
				}
				case PlayerPrimaryRolePower.Sneak:
				{
					if (LycansUtility.WolvesCanTransform)
					{
						customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(!NetworkBool.op_Implicit(customPlayer.NewPrimaryRoleUniqueBool));
						break;
					}
					DiscipleAnchor discipleAnchor = Object.FindObjectsOfType<DiscipleAnchor>().FirstOrDefault((DiscipleAnchor o) => o.CreatorRef == customPlayer.Ref);
					if ((Object)(object)discipleAnchor == (Object)null)
					{
						ManualLogSource logger = Plugin.Logger;
						NetworkString<_32> username = customPlayer.PlayerController.PlayerData.Username;
						logger.LogError((object)("No anchor for player " + ((object)username/*cast due to constrained. prefix*/).ToString() + "!"));
					}
					else
					{
						((Component)discipleAnchor).transform.position = ((Component)customPlayer.PlayerController).transform.position;
						((Component)discipleAnchor).transform.rotation = ((Component)customPlayer.PlayerController).transform.rotation;
						customPlayer.SecondaryRoleTeleportData = new NetworkTeleportData(((Component)discipleAnchor).transform.position, ((Component)discipleAnchor).transform.rotation, true);
					}
					break;
				}
				case PlayerPrimaryRolePower.Tracker:
					if (LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(customPlayer.PlayerController.IsWolf) && LycansUtility.WolvesCanTransform && customPlayer.PrimaryRolePowerCurrentMaterials >= customPlayer.PowerMaterialsInfo.RequiredMaterials)
					{
						primaryRoleActionTimer = customPlayer.PrimaryRoleActionTimer;
						if (!((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
						{
							customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 6f);
							playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
							GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("Tracker"), ((Component)customPlayer.PlayerController).transform.position, 30f, 1f);
						}
					}
					break;
				case PlayerPrimaryRolePower.Host:
					if (customPlayer.PrimaryRolePowerCurrentMaterials < customPlayer.PowerMaterialsInfo.RequiredMaterials || !NetworkBool.op_Implicit(playerController.CanMove) || !LycansUtility.GameActuallyInPlay)
					{
						return;
					}
					customPlayer.ReduceMaterialAfterPowerUse();
					if (runner.IsServer)
					{
						NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectHostParasite");
						Vector3 position = ((Component)customPlayer.PlayerController).transform.position;
						NetworkObject val = runner.Spawn(networkObject, (Vector3?)((Component)customPlayer.PlayerController).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							//IL_0008: Unknown result type (might be due to invalid IL or missing references)
							((Component)no).transform.position = position;
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val).transform.position = position;
						((Component)val).transform.forward = ((Component)customPlayer.PlayerController).transform.forward;
						((Component)val).GetComponent<HostParasite>().SetCreatorRef(customPlayer.Ref);
					}
					break;
				}
				switch (customPlayer.NewPrimaryRole)
				{
				case PlayerNewPrimaryRole.Scientist:
					if (customPlayer.PrimaryRolePowerRemainingUses > 0)
					{
						customPlayer.GiveScientistGadget();
						customPlayer.PrimaryRolePowerRemainingUses--;
						customPlayer.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)customPlayer).Runner, 60f);
					}
					break;
				case PlayerNewPrimaryRole.Lover:
					if (NetworkBool.op_Implicit(customPlayer.NewPrimaryRoleUniqueBool))
					{
						customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
						Effect val4 = playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is TelepathyEffect);
						if ((Object)(object)val4 != (Object)null)
						{
							playerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val4).Object.Id);
						}
					}
					else
					{
						customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
						ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectTelepathy", runner);
					}
					break;
				case PlayerNewPrimaryRole.Kidnapper:
					if (NetworkBool.op_Implicit(customPlayer.NewPrimaryRoleUniqueBool))
					{
						customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
						Effect val5 = playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is KidnapperSilenceEffect);
						if ((Object)(object)val5 != (Object)null)
						{
							playerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val5).Object.Id);
						}
					}
					else
					{
						customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
						ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectKidnapperSilence", runner);
					}
					break;
				case PlayerNewPrimaryRole.Cultist:
					if (customPlayer.PrimaryRolePowerRemainingUses > 0 && customPlayer.PrimaryRolePowerRemainingUses > 0)
					{
						customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 1f);
						playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
					}
					break;
				}
			}
			switch (customPlayer.PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Necromancer:
				customPlayer.PlayerAnimations.SetLoopAnimation("CastingLoop", active: true);
				break;
			case PlayerPrimaryRolePower.Tracker:
				customPlayer.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: true);
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Activate_Primary_Role_Power_Without_Target error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Primary_Role_Power_Without_Target(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Activate_Primary_Role_Power_Without_Target_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Activate_Primary_Role_Power_Without_Target(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Activate_Primary_Role_Power_With_Target(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Invalid comparison between Unknown and I4
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1172: Unknown result type (might be due to invalid IL or missing references)
		//IL_1183: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Invalid comparison between Unknown and I4
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Invalid comparison between Unknown and I4
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_119b: Unknown result type (might be due to invalid IL or missing references)
		//IL_120f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Invalid comparison between Unknown and I4
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Invalid comparison between Unknown and I4
		//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11db: Unknown result type (might be due to invalid IL or missing references)
		//IL_1282: Unknown result type (might be due to invalid IL or missing references)
		//IL_1330: Unknown result type (might be due to invalid IL or missing references)
		//IL_1336: Invalid comparison between Unknown and I4
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1640: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_111a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1057: Unknown result type (might be due to invalid IL or missing references)
		//IL_105c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1060: Unknown result type (might be due to invalid IL or missing references)
		//IL_1065: Unknown result type (might be due to invalid IL or missing references)
		//IL_137a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1380: Invalid comparison between Unknown and I4
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_164e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_112c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1142: Unknown result type (might be due to invalid IL or missing references)
		//IL_1075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Invalid comparison between Unknown and I4
		//IL_0e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_165c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1089: Unknown result type (might be due to invalid IL or missing references)
		//IL_1090: Unknown result type (might be due to invalid IL or missing references)
		//IL_1095: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c7: Invalid comparison between Unknown and I4
		//IL_1463: Unknown result type (might be due to invalid IL or missing references)
		//IL_1393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Primary_Role_Power_With_Target(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom customPlayer = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = customPlayer.PlayerController;
			PlayerController targetPlayer = player.PlayerController;
			if (runner.IsServer)
			{
				TickTimer val;
				Vector3 forward;
				switch (customPlayer.NewPrimaryRole)
				{
				case PlayerNewPrimaryRole.VillageIdiot:
					if (customPlayer.PrimaryRolePowerRemainingUses <= 0 || NetworkBool.op_Implicit(targetPlayer.IsWolf) || NetworkBool.op_Implicit(playerController.IsWolf) || NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return;
					}
					switch (customPlayer.SoloRoleObjectiveTarget)
					{
					case 0:
						if ((int)targetPlayer.Role != 1)
						{
							player.CurseDormant = NetworkBool.op_Implicit(true);
						}
						customPlayer.PrimaryRolePowerRemainingUses--;
						customPlayer.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(runner, (float)BalancingValues.VillageIdiotCurseCooldown);
						break;
					case 1:
						player.BombDormant = NetworkBool.op_Implicit(true);
						customPlayer.PrimaryRolePowerRemainingUses--;
						customPlayer.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(runner, (float)BalancingValues.VillageIdiotBombCooldown);
						break;
					case 2:
						if ((Object)(object)targetPlayer.Item != (Object)null && !NetworkBool.op_Implicit(((Component)targetPlayer.Item).GetComponentInChildren<ItemCustom>().Sabotaged))
						{
							((Component)targetPlayer.Item).GetComponentInChildren<ItemCustom>().Sabotaged = NetworkBool.op_Implicit(true);
							customPlayer.PrimaryRolePowerRemainingUses--;
							customPlayer.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(runner, (float)BalancingValues.VillageIdiotTrapItemCooldown);
						}
						break;
					}
					break;
				case PlayerNewPrimaryRole.Agent:
					if (player.NewPrimaryRole == PlayerNewPrimaryRole.Agent && NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && !NetworkBool.op_Implicit(customPlayer.PlayerController.IsDead) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) && !NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia) && !NetworkBool.op_Implicit(player.Petrified))
					{
						player.Stats.UpdateDeathType("OTHER_AGENT");
						targetPlayer.Rpc_Kill(playerController.Ref);
						ApplyEffectToPlayer(customPlayer.PlayerController, "LycansNewRoles.EffectSneaky", runner, 1f, 8f);
					}
					break;
				case PlayerNewPrimaryRole.Beast:
					val = customPlayer.PrimaryRolePowerCooldownTimer;
					if (((TickTimer)(ref val)).IsRunning)
					{
						return;
					}
					if ((int)GameManager.LocalGameState == 2 && !NetworkBool.op_Implicit(playerController.IsDead) && !NetworkBool.op_Implicit(playerController.IsWolf))
					{
						PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayer.Ref);
						player2.BeastMark = NetworkBool.op_Implicit(true);
						customPlayer.PrimaryRolePowerRemainingUses--;
						customPlayer.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(runner, (float)BalancingValues.BeastMarkCooldown);
					}
					break;
				case PlayerNewPrimaryRole.Voodoo:
					if (customPlayer.PrimaryRolePowerRemainingUses <= 0 || (int)targetPlayer.Role == 1 || NetworkBool.op_Implicit(targetPlayer.IsWolf))
					{
						return;
					}
					customPlayer.PrimaryRoleTargetRef = player.Ref;
					customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 2f);
					playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
					playerController.IsAiming = NetworkBool.op_Implicit(false);
					break;
				case PlayerNewPrimaryRole.Zombie:
				{
					if (player.NewPrimaryRole == PlayerNewPrimaryRole.Voodoo || player.NewPrimaryRole == PlayerNewPrimaryRole.Zombie)
					{
						return;
					}
					if (NetworkBool.op_Implicit(targetPlayer.IsWolf))
					{
						ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectDowned", ((SimulationBehaviour)targetPlayer).Runner, 1f, 7f);
						forward = ((Component)customPlayer.PlayerController).transform.forward;
						Vector3 normalized = ((Vector3)(ref forward)).normalized;
						((Component)targetPlayer).GetComponent<KnockbackComponent>().Init(new Vector3(normalized.x, 0f, normalized.z), 7f, 7f);
						playerController.Rpc_Kill(PlayerRef.None);
						break;
					}
					player.Stats.UpdateDeathType("BY_ZOMBIE");
					targetPlayer.Rpc_Kill(playerController.Ref);
					playerController.UpdateAnimation(Animator.StringToHash("Attacking"), true);
					((MonoBehaviour)playerController).StartCoroutine("WaitAndResetAttackAnimation");
					GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)targetPlayer).Runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerController).transform.position, BalancingValues.WolfKillSoundRangeByMap(GameManager.Instance.MapID), 1f);
					PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.Voodoo);
					val = specificNewPrimaryRole.PrimaryRolePowerCooldownTimer;
					if (((TickTimer)(ref val)).IsRunning)
					{
						val = specificNewPrimaryRole.PrimaryRolePowerCooldownTimer;
						float value = ((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)targetPlayer).Runner).Value;
						specificNewPrimaryRole.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)targetPlayer).Runner, Mathf.Max(1f, value * 0.3f));
					}
					List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Avenger && !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
					foreach (PlayerCustom item in list)
					{
						if (Vector3.Distance(((Component)item.PlayerController).transform.position, ((Component)targetPlayer).transform.position) <= 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
						{
							item.AddMaterials(2500);
						}
					}
					List<PlayerCustom> list2 = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Shadow && !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
					foreach (PlayerCustom item2 in list2)
					{
						if (Vector3.Distance(((Component)item2.PlayerController).transform.position, ((Component)targetPlayer).transform.position) <= 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
						{
							item2.AddMaterials(10000);
						}
					}
					break;
				}
				case PlayerNewPrimaryRole.Kidnapper:
					if (customPlayer.PrimaryRolePowerRemainingUses > 0 && customPlayer.PrimaryRoleTargetRef == PlayerRef.None && !NetworkBool.op_Implicit(player.Kidnapped) && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(player.Downed) && (int)GameManager.LocalGameState == 2)
					{
						customPlayer.PrimaryRoleTargetRef = targetPlayer.Ref;
						customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 3f);
					}
					break;
				}
				switch (customPlayer.PrimaryRolePower)
				{
				case PlayerPrimaryRolePower.Necromancer:
					if (!NetworkBool.op_Implicit(customPlayer.NewPrimaryRoleUniqueBool))
					{
						if (player.NewPrimaryRole == PlayerNewPrimaryRole.Zombie)
						{
							return;
						}
						val = customPlayer.PrimaryRoleActionTimer;
						if (!((TickTimer)(ref val)).IsRunning)
						{
							customPlayer.PrimaryRoleTargetRef = player.Ref;
							customPlayer.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 2f);
							playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
						}
					}
					break;
				case PlayerPrimaryRolePower.Deceiver:
					if (customPlayer.PrimaryRolePowerRemainingUses <= 0)
					{
						return;
					}
					player.DeceiverTrickThisMeeting = NetworkBool.op_Implicit(true);
					player.DeceiverTrickAllTime = NetworkBool.op_Implicit(true);
					customPlayer.PrimaryRolePowerRemainingUses--;
					customPlayer.TriggerPrimaryRolePowerCooldown(runner);
					break;
				case PlayerPrimaryRolePower.Warlock:
					if (customPlayer.PrimaryRolePowerRemainingUses <= 0 || NetworkBool.op_Implicit(targetPlayer.IsWolf) || NetworkBool.op_Implicit(playerController.IsWolf))
					{
						return;
					}
					if ((int)targetPlayer.Role != 1)
					{
						player.CurseDormant = NetworkBool.op_Implicit(true);
					}
					customPlayer.PrimaryRolePowerRemainingUses--;
					customPlayer.TriggerPrimaryRolePowerCooldown(runner);
					break;
				case PlayerPrimaryRolePower.Saboteur:
					if (customPlayer.PrimaryRolePowerCurrentMaterials < customPlayer.PowerMaterialsInfo.RequiredMaterials)
					{
						return;
					}
					if ((Object)(object)targetPlayer.Item != (Object)null && !NetworkBool.op_Implicit(((Component)targetPlayer.Item).GetComponentInChildren<ItemCustom>().Sabotaged))
					{
						((Component)targetPlayer.Item).GetComponentInChildren<ItemCustom>().Sabotaged = NetworkBool.op_Implicit(true);
						customPlayer.ReduceMaterialAfterPowerUse();
					}
					break;
				case PlayerPrimaryRolePower.Possessor:
					if (player.AlreadyPossessed)
					{
						return;
					}
					customPlayer.PrimaryRoleTargetRef = targetPlayer.Ref;
					customPlayer.PrimaryRolePowerCurrentMaterials = 0;
					customPlayer.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
					break;
				case PlayerPrimaryRolePower.Bomber:
					if (customPlayer.PrimaryRolePowerRemainingUses <= 0 || NetworkBool.op_Implicit(player.BombDormant))
					{
						return;
					}
					player.BombDormant = NetworkBool.op_Implicit(true);
					customPlayer.PrimaryRolePowerRemainingUses--;
					customPlayer.ReduceMaterialAfterPowerUse();
					break;
				case PlayerPrimaryRolePower.Predator:
					if (customPlayer.PrimaryRoleTargetRef == PlayerRef.None && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						customPlayer.PrimaryRoleTargetRef = targetPlayer.Ref;
					}
					break;
				case PlayerPrimaryRolePower.Host:
				{
					List<PlayerCustom> list4 = PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.Parasite)).ToList();
					foreach (PlayerCustom playerWithParasite in list4)
					{
						Rpc_Effect_On_Player(runner, playerWithParasite.Index, 8);
						GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("Parasite"), ((Component)playerWithParasite).transform.position, 15f, 0.8f);
						playerWithParasite.PlayerController.Hunger = Mathf.Max(0f, playerWithParasite.PlayerController.Hunger - 0.2f * (float)GameManager.Instance.MaxHunger);
						ApplyEffectToPlayer(playerWithParasite.PlayerController, "LycansNewRoles.EffectPoisoned", runner, 1f, 15f);
						List<PlayerCustom> list5 = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != playerWithParasite.Ref && Vector3.Distance(((Component)playerWithParasite.PlayerController).transform.position, ((Component)o.PlayerController).transform.position) < 12f).ToList();
						foreach (PlayerCustom item3 in list5)
						{
							float num3 = Vector3.Distance(((Component)playerWithParasite.PlayerController).transform.position, ((Component)item3.PlayerController).transform.position);
							float num4 = Mathf.InverseLerp(12f, 0f, num3);
							item3.PlayerController.Hunger = Mathf.Max(0f, item3.PlayerController.Hunger - 0.2f * (float)GameManager.Instance.MaxHunger * num4);
							float num5 = 15f * num4;
							if (num5 >= 1f)
							{
								ApplyEffectToPlayer(item3.PlayerController, "LycansNewRoles.EffectPoisoned", runner, 1f, num5);
							}
						}
						playerWithParasite.Parasite = NetworkBool.op_Implicit(false);
					}
					break;
				}
				case PlayerPrimaryRolePower.Avenger:
					if (customPlayer.PrimaryRolePowerCurrentMaterials < customPlayer.PowerMaterialsInfo.RequiredMaterials || NetworkBool.op_Implicit(customPlayer.PlayerController.IsDead) || NetworkBool.op_Implicit(player.Petrified))
					{
						return;
					}
					customPlayer.PrimaryRolePowerRemainingUses--;
					customPlayer.ReduceMaterialAfterPowerUse();
					if (NetworkBool.op_Implicit(targetPlayer.IsWolf))
					{
						ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectDowned", ((SimulationBehaviour)targetPlayer).Runner);
						forward = ((Component)customPlayer.PlayerController).transform.forward;
						Vector3 val2 = ((Vector3)(ref forward)).normalized;
						if (player.NewPrimaryRole == PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
						{
							val2 *= 0.6f;
						}
						((Component)targetPlayer).GetComponent<KnockbackComponent>().Init(new Vector3(val2.x, 0f, val2.z), 6f, 6f);
						ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectResilience", runner, 1f, 8f);
					}
					else
					{
						player.Stats.UpdateDeathType("AVENGER");
						targetPlayer.Rpc_Kill(playerController.Ref);
					}
					GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)targetPlayer).transform.position, BalancingValues.WolfKillSoundRangeByMap(GameManager.Instance.MapID), 1f);
					break;
				case PlayerPrimaryRolePower.Investigator:
					if (customPlayer.PrimaryRoleTargetRef != targetPlayer.Ref || NetworkBool.op_Implicit(targetPlayer.IsWolf))
					{
						return;
					}
					customPlayer.PrimaryRolePowerCurrentMaterials += 35;
					customPlayer.PrimaryRoleTargetRef = PlayerRef.None;
					break;
				case PlayerPrimaryRolePower.Survivalist:
					if (customPlayer.PrimaryRolePowerRemainingUses <= 0)
					{
						return;
					}
					player.SurvivalistBuff = NetworkBool.op_Implicit(true);
					customPlayer.PrimaryRolePowerRemainingUses--;
					customPlayer.ReduceMaterialAfterPowerUse();
					break;
				case PlayerPrimaryRolePower.Priest:
					if (customPlayer.PrimaryRolePowerRemainingUses <= 0 || NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
					{
						return;
					}
					player.ProtectedPriest = NetworkBool.op_Implicit(true);
					customPlayer.PrimaryRolePowerRemainingUses--;
					customPlayer.ReduceMaterialAfterPowerUse();
					break;
				case PlayerPrimaryRolePower.Magician:
				{
					IEnumerable<MagicianBeacon> enumerable = from o in Object.FindObjectsOfType<MagicianBeacon>()
						where o.CreatorRef == customPlayer.Ref
						select o;
					foreach (MagicianBeacon item4 in enumerable)
					{
						item4.CreateIllusion();
					}
					break;
				}
				case PlayerPrimaryRolePower.Angel:
				{
					if (customPlayer.PrimaryRolePowerRemainingUses <= 0 || (int)GameManager.LocalGameState != 2)
					{
						return;
					}
					EffectManager effectManager = Traverse.Create(typeof(EffectManager)).Field<EffectManager>("_instance").Value;
					if ((int)targetPlayer.Role != 1 && player.NewPrimaryRole == PlayerNewPrimaryRole.None && !player.AlreadyAngeledToday && !NetworkBool.op_Implicit(player.Dying) && (targetPlayer.Hunger <= (float)GameManager.Instance.MaxHunger * 0.25f || PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && Vector3.Distance(((Component)targetPlayer).transform.position, ((Component)o).transform.position) < Traverse.Create((object)effectManager).Method("WolfMusicDistance", new List<Type> { typeof(PlayerController) }.ToArray(), (object[])null).GetValue<float>(new object[1] { o })))))
					{
						targetPlayer.Hunger = Mathf.Min((float)GameManager.Instance.MaxHunger, targetPlayer.Hunger + (float)GameManager.Instance.MaxHunger * 0.2f);
						ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectAngel", ((SimulationBehaviour)targetPlayer).Runner);
						player.Angel = NetworkBool.op_Implicit(true);
						customPlayer.PrimaryRolePowerRemainingUses--;
						customPlayer.TriggerPrimaryRolePowerCooldown(((SimulationBehaviour)targetPlayer).Runner);
					}
					else if ((int)targetPlayer.Role == 1 && NetworkBool.op_Implicit(targetPlayer.IsWolf))
					{
						List<string> list3 = new List<string> { "LycansNewRoles.EffectNearsighted", "LycansNewRoles.EffectDeafness" };
						string effectName = CollectionsUtil.Grab<string>(list3, 1).First();
						ApplyEffectToPlayer(targetPlayer, effectName, runner, 1f, 15f);
						customPlayer.PrimaryRolePowerRemainingUses--;
						customPlayer.TriggerPrimaryRolePowerCooldown(((SimulationBehaviour)targetPlayer).Runner);
					}
					break;
				}
				}
			}
			PlayerPrimaryRolePower primaryRolePower = customPlayer.PrimaryRolePower;
			PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
			if (playerPrimaryRolePower == PlayerPrimaryRolePower.Necromancer)
			{
				customPlayer.PlayerAnimations.SetLoopAnimation("CastingLoop", active: true);
			}
			switch (customPlayer.NewPrimaryRole)
			{
			case PlayerNewPrimaryRole.Voodoo:
				customPlayer.PlayerAnimations.SetLoopAnimation("CastingLoop", active: true);
				break;
			case PlayerNewPrimaryRole.Cultist:
				customPlayer.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: true);
				break;
			}
			if (customPlayer.IsCurrentlyPlayedOrObserved)
			{
				switch (customPlayer.PrimaryRolePower)
				{
				case PlayerPrimaryRolePower.Deceiver:
					player.FlashPlayer(Color.black);
					break;
				case PlayerPrimaryRolePower.Warlock:
					player.FlashPlayer(Color.gray);
					break;
				case PlayerPrimaryRolePower.Priest:
					player.FlashPlayer(Color.green);
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Activate_Primary_Role_Power_With_Target error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Primary_Role_Power_With_Target(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Activate_Primary_Role_Power_With_Target_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Activate_Primary_Role_Power_With_Target(runner, playerIndex, targetPlayerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Shapeshift(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Invalid comparison between Unknown and I4
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBehaviourUtils.InvokeRpc)
		{
			NetworkBehaviourUtils.InvokeRpc = false;
		}
		else
		{
			if ((Object)(object)runner == (Object)null)
			{
				throw new ArgumentNullException("runner");
			}
			if ((int)runner.Stage == 4)
			{
				return;
			}
			if (runner.HasAnyActiveConnections())
			{
				int num = 24;
				SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
				byte* data = SimulationMessage.GetData(ptr);
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Shapeshift(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
				*(int*)(data + num2) = playerIndex;
				num2 += 4;
				*(int*)(data + num2) = targetPlayerIndex;
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 8;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
		PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
		PlayerController playerController = player.PlayerController;
		PlayerController playerController2 = player2.PlayerController;
		TickTimer primaryRoleActionTimer = player.PrimaryRoleActionTimer;
		if (!((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
		{
			player.PrimaryRoleTargetRef = playerController2.Ref;
			player.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(runner, 1f);
			playerController.MovementAction = 0;
			playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Shapeshift(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Shapeshift_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Shapeshift(runner, playerIndex, targetPlayerIndex);
	}

	private void PrimaryRoleActionTimerExpired()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Invalid comparison between Unknown and I4
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Invalid comparison between Unknown and I4
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Expected O, but got Unknown
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e77: Expected O, but got Unknown
		//IL_0e82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc7: Expected O, but got Unknown
		//IL_0fd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb9: Expected O, but got Unknown
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1045: Unknown result type (might be due to invalid IL or missing references)
		//IL_104a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1054: Unknown result type (might be due to invalid IL or missing references)
		//IL_1059: Unknown result type (might be due to invalid IL or missing references)
		//IL_1064: Unknown result type (might be due to invalid IL or missing references)
		//IL_106c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1076: Unknown result type (might be due to invalid IL or missing references)
		//IL_1092: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a8: Expected O, but got Unknown
		//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbb: Expected O, but got Unknown
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0919: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad8: Expected O, but got Unknown
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0816: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PrimaryRoleActionTimer = TickTimer.None;
			PlayerController playerController = PlayerController;
			switch (NewPrimaryRole)
			{
			case PlayerNewPrimaryRole.Voodoo:
			{
				UpdateCanMoveAnimation();
				playerController.MovementAction = 0;
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(PrimaryRoleTargetRef);
				if (!LycansUtility.GameActuallyInPlay || NetworkBool.op_Implicit(playerController.IsDead) || player.NewPrimaryRole == PlayerNewPrimaryRole.Zombie || (int)player.PlayerController.Role == 1 || NetworkBool.op_Implicit(player.PlayerController.IsWolf))
				{
					break;
				}
				if (((SimulationBehaviour)this).Runner.IsServer)
				{
					if (!NetworkBool.op_Implicit(player.PlayerController.IsDead))
					{
						return;
					}
					PrimaryRolePowerRemainingUses--;
					PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, (float)BalancingValues.VoodooReanimationCooldown(PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Zombie), PlayerRegistry.Count));
					player.PlayerController.Role = (PlayerRole)0;
					player.GiveNewPrimaryRole(PlayerNewPrimaryRole.Zombie);
					player.GivePrimaryRolePower(PlayerPrimaryRolePower.None);
					player.GiveSecondaryRole(PlayerSecondaryRole.None);
					player.Stats.MainRoleChanges.Add(new PlayerStats.MainRoleChangeEvent(TranslationManager.Instance.GetTranslationForStats(UpdateRoleUtility.GetNewPrimaryRoleKey(player.PlayerController, player)).Replace("{0}", "").Replace("{1}", "")
						.Replace("{2}", "")));
					if (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
					{
						player.PlayerController.IsDead = NetworkBool.op_Implicit(false);
						player.PlayerController.IsAiming = NetworkBool.op_Implicit(false);
						player.PlayerController.IsDeadChannel = NetworkBool.op_Implicit(false);
						player.PlayerController.Hunger = GameManager.Instance.MaxHunger;
					}
				}
				if (((SimulationBehaviour)playerController).HasInputAuthority)
				{
					PlaySuccessSound();
				}
				if (player.Ref == PlayerController.Local.Ref)
				{
					AudioManager.Play("VoodooRez", (MixerTarget)2, 0.7f, 1f);
					UIManager.ShowRedCenterMessage("NALES_UI_ZOMBIE_NOTIFICATION", 0.4f, 3f);
				}
				break;
			}
			case PlayerNewPrimaryRole.Kidnapper:
			{
				if ((int)GameManager.LocalGameState != 2 || NetworkBool.op_Implicit(playerController.IsDead) || NetworkBool.op_Implicit(playerController.IsWolf))
				{
					PrimaryRoleTargetRef = PlayerRef.None;
					return;
				}
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(PrimaryRoleTargetRef);
				if (NetworkBool.op_Implicit(player2.Downed))
				{
					PrimaryRoleTargetRef = PlayerRef.None;
					return;
				}
				player2.Kidnapped = NetworkBool.op_Implicit(true);
				SoloRoleObjectiveCount++;
				player2.CurseDormant = NetworkBool.op_Implicit(false);
				player2.BombDormant = NetworkBool.op_Implicit(false);
				GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("Isolation"), ((Component)player2.PlayerController).transform.position, 7f, 0.5f);
				Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, player2.Index, 15);
				PrimaryRolePowerRemainingUses--;
				PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, (float)BalancingValues.KidnapperFinalCooldown(SoloRoleObjectiveCount, BalancingValues.KidnapperTargetAmount(PlayerRegistry.Count), PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))), PlayerRegistry.Count));
				PrimaryRoleTargetRef = PlayerRef.None;
				break;
			}
			case PlayerNewPrimaryRole.Cultist:
				UpdateCanMoveAnimation();
				playerController.MovementAction = 0;
				if (((SimulationBehaviour)this).Runner.IsServer && LycansUtility.GameActuallyInPlay && !NetworkBool.op_Implicit(playerController.IsDead))
				{
					PrimaryRolePowerRemainingUses--;
					PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, BalancingValues.CultistSkullCreationCooldown(CultistSkull.AllSkulls.Count + 1));
					NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectCultistSkull");
					Vector3 position = new Vector3(((Component)playerController).transform.position.x, ((Component)playerController).transform.position.y, ((Component)playerController).transform.position.z);
					NetworkObject val = ((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						//IL_0008: Unknown result type (might be due to invalid IL or missing references)
						((Component)no).transform.position = position;
					}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val).transform.position = position;
					((Component)val).GetComponent<CultistSkull>().SetCreatorRef(playerController.Ref);
				}
				break;
			}
			switch (PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Necromancer:
				PlayerAnimations.SetLoopAnimation("CastingLoop", active: false);
				break;
			case PlayerPrimaryRolePower.Tracker:
				PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
				break;
			}
			switch (NewPrimaryRole)
			{
			case PlayerNewPrimaryRole.Voodoo:
				PlayerAnimations.SetLoopAnimation("CastingLoop", active: false);
				break;
			case PlayerNewPrimaryRole.Cultist:
				PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
				break;
			}
			if (NetworkBool.op_Implicit(playerController.IsDead))
			{
				return;
			}
			switch (PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Warlock:
			{
				UpdateCanMoveAnimation();
				if (!LycansUtility.GameActuallyInPlay)
				{
					break;
				}
				PlayerController playerController2 = PlayerController;
				if (PrimaryRoleTargetRef == Ref)
				{
					Effect val5 = playerController2.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is DisguisedEffect);
					if ((Object)(object)val5 != (Object)null)
					{
						playerController2.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val5).Object.Id);
					}
				}
				else
				{
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectDisguised", ((SimulationBehaviour)this).Runner);
				}
				break;
			}
			case PlayerPrimaryRolePower.Necromancer:
			{
				UpdateCanMoveAnimation();
				if (!LycansUtility.GameActuallyInPlay)
				{
					break;
				}
				PlayerController playerController3 = PlayerController;
				if (!((Object)(object)playerController3 != (Object)null) || NetworkBool.op_Implicit(playerController3.IsDead))
				{
					break;
				}
				PlayerController player3 = PlayerRegistry.GetPlayer(PrimaryRoleTargetRef);
				PlayerCustom player4 = PlayerCustomRegistry.GetPlayer(PrimaryRoleTargetRef);
				if (NetworkBool.op_Implicit(NewPrimaryRoleUniqueBool))
				{
					if (((SimulationBehaviour)this).Runner.IsServer)
					{
						if (NetworkBool.op_Implicit(player4.Disappeared))
						{
							break;
						}
						player3.IsDead = NetworkBool.op_Implicit(false);
						player3.IsAiming = NetworkBool.op_Implicit(false);
						player3.IsDeadChannel = NetworkBool.op_Implicit(false);
						player3.Hunger = GameManager.Instance.MaxHunger;
						if (((SimulationBehaviour)this).Object.HasStateAuthority)
						{
							Traverse.Create((object)player3).Property("WolfDelay", (object[])null).SetValue((object)TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, (float)GameManager.Instance.TransformationTime));
						}
						player3.TransformedNight = NetworkBool.op_Implicit(true);
						player3.IsWolf = NetworkBool.op_Implicit(true);
						player3.MovementAction = 0;
						player3.CanMoveAnimation = NetworkBool.op_Implicit(false);
						player3.IsZooming = NetworkBool.op_Implicit(false);
						ApplyEffectToPlayer(player3, "LycansNewRoles.EffectResurrected", ((SimulationBehaviour)this).Runner, 1f, 300f);
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("WOLF_TRANSFORM"), ((Component)player3).transform.position, 30f, 1f);
						string text = DateTime.UtcNow.ToString();
						NetworkString<_32> username = PlayerController.PlayerData.Username;
						LycansUtility.AddLogOnlyForMe("Adding transformation from necromancer at date: " + text + ", player: " + ((object)username/*cast due to constrained. prefix*/).ToString());
						GameManagerCustom.Instance.AddTransformation();
					}
					Traverse.Create((object)player3).Field<ParticleSystem>("smokeParticleSystem").Value.Play();
				}
				else
				{
					NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
					player4.GiveNewPrimaryRole(PlayerNewPrimaryRole.None);
					player4.GivePrimaryRolePower(PlayerPrimaryRolePower.None);
					player3.Role = (PlayerRole)1;
					player3.IsWolf = NetworkBool.op_Implicit(true);
					if (((SimulationBehaviour)this).Runner.IsServer)
					{
						player4.Stats.MainRoleChanges.Add(new PlayerStats.MainRoleChangeEvent(TranslationManager.Instance.GetTranslationForStats(UpdateRoleUtility.GetNewPrimaryRoleKey(player4.PlayerController, player4)).Replace("{0}", "").Replace("{1}", "")
							.Replace("{2}", "")));
					}
				}
				if (((SimulationBehaviour)player3).HasInputAuthority)
				{
					AudioManager.Play("WOLF", (MixerTarget)2, 1f, 1f);
				}
				break;
			}
			case PlayerPrimaryRolePower.Deceiver:
			{
				if (!LycansUtility.GameActuallyInPlay)
				{
					break;
				}
				PlayerController playerController4 = PlayerController;
				if (!((Object)(object)playerController4 != (Object)null) || NetworkBool.op_Implicit(playerController4.IsDead) || !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) || !((SimulationBehaviour)this).Runner.IsServer)
				{
					break;
				}
				List<Teleporter> list = (from o in Object.FindObjectsOfType<Teleporter>()
					where o.MapID == GameManager.Instance.MapID
					select o).ToList();
				if (list.Any())
				{
					NetworkPrefabId networkObject7 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectDeceiverIllusion");
					Teleporter chosenTeleporter = CollectionsUtil.Grab<Teleporter>(list, 1).FirstOrDefault();
					NetworkObject val8 = ((SimulationBehaviour)this).Runner.Spawn(networkObject7, (Vector3?)((Component)chosenTeleporter).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						//IL_0012: Unknown result type (might be due to invalid IL or missing references)
						((Component)no).transform.position = ((Component)chosenTeleporter).transform.position;
					}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val8).GetComponent<DeceiverIllusionComponent>().SetCreatorRef(playerController4.Ref);
				}
				PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, Random.Range(15f, 30f));
				break;
			}
			case PlayerPrimaryRolePower.Tracker:
				UpdateCanMoveAnimation();
				if (LycansUtility.GameActuallyInPlay && ((SimulationBehaviour)this).Runner.IsServer)
				{
					NetworkPrefabId networkObject3 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectTrackerRadar");
					Vector3 position3 = ((Component)playerController).transform.position;
					NetworkObject val3 = ((SimulationBehaviour)this).Runner.Spawn(networkObject3, (Vector3?)((Component)playerController).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						//IL_0008: Unknown result type (might be due to invalid IL or missing references)
						((Component)no).transform.position = position3;
					}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val3).transform.position = position3;
					((Component)val3).GetComponent<TrackerRadar>().SetCreatorRef(playerController.Ref);
					((Component)val3).GetComponent<TrackerRadar>().Init(30);
					ReduceMaterialAfterPowerUse();
				}
				break;
			case PlayerPrimaryRolePower.Exorcist:
				UpdateCanMoveAnimation();
				if (!LycansUtility.GameActuallyInPlay)
				{
					break;
				}
				PrimaryRolePowerRemainingUses--;
				ReduceMaterialAfterPowerUse();
				if (((SimulationBehaviour)this).Runner.IsServer)
				{
					NetworkPrefabId networkObject5 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectExorcistDetector");
					Vector3 position5 = ((Component)playerController).transform.position;
					NetworkObject val6 = ((SimulationBehaviour)this).Runner.Spawn(networkObject5, (Vector3?)((Component)playerController).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						//IL_0008: Unknown result type (might be due to invalid IL or missing references)
						((Component)no).transform.position = position5;
					}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val6).transform.position = position5;
					((Component)val6).GetComponent<ExorcistDetector>().SetCreatorRef(playerController.Ref);
					Vector3 position6 = Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value[GameManager.Instance.MapID - 1].position;
					float num = Vector3.Distance(position6, position5);
					float num2 = Mathf.InverseLerp(0f, 80f, num);
					int duration = Mathf.RoundToInt(Mathf.Lerp(80f, 210f, num2));
					((Component)val6).GetComponent<ExorcistDetector>().Init(duration);
				}
				break;
			case PlayerPrimaryRolePower.Survivalist:
				UpdateCanMoveAnimation();
				if (LycansUtility.GameActuallyInPlay)
				{
					PrimaryRolePowerRemainingUses--;
					ReduceMaterialAfterPowerUse();
					if (((SimulationBehaviour)this).Runner.IsServer)
					{
						SurvivalistBuff = NetworkBool.op_Implicit(true);
					}
				}
				break;
			case PlayerPrimaryRolePower.Scout:
				UpdateCanMoveAnimation();
				if (!LycansUtility.GameActuallyInPlay)
				{
					break;
				}
				PrimaryRolePowerRemainingUses--;
				ReduceMaterialAfterPowerUse();
				if (((SimulationBehaviour)this).Runner.IsServer)
				{
					NetworkPrefabId networkObject6 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectScoutRadar");
					Vector3 position7 = ((Component)playerController).transform.position;
					NetworkObject val7 = ((SimulationBehaviour)this).Runner.Spawn(networkObject6, (Vector3?)((Component)playerController).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						//IL_0008: Unknown result type (might be due to invalid IL or missing references)
						((Component)no).transform.position = position7;
					}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val7).transform.position = position7;
					((Component)val7).GetComponent<ScoutRadar>().SetCreatorRef(playerController.Ref);
					Vector3 position8 = Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value[GameManager.Instance.MapID - 1].position;
					float num3 = Vector3.Distance(position8, position7);
					float num4 = Mathf.InverseLerp(0f, 60f, num3);
					int duration2 = Mathf.RoundToInt(Mathf.Lerp(70f, 140f, num4));
					((Component)val7).GetComponent<ScoutRadar>().Init(duration2);
				}
				break;
			case PlayerPrimaryRolePower.Mystic:
				UpdateCanMoveAnimation();
				if (!LycansUtility.GameActuallyInPlay)
				{
					break;
				}
				ReduceMaterialAfterPowerUse();
				if (((SimulationBehaviour)this).Runner.IsServer)
				{
					NetworkPrefabId networkObject4 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectMysticRepulsor");
					Vector3 position4 = ((Component)playerController).transform.position;
					NetworkObject val4 = ((SimulationBehaviour)this).Runner.Spawn(networkObject4, (Vector3?)((Component)playerController).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						//IL_0008: Unknown result type (might be due to invalid IL or missing references)
						((Component)no).transform.position = position4;
					}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val4).transform.position = position4;
					((Component)val4).GetComponent<MysticRepulsor>().SetCreatorRef(playerController.Ref);
					((Component)val4).GetComponent<MysticRepulsor>().Init(15);
				}
				break;
			case PlayerPrimaryRolePower.Runemaster:
				UpdateCanMoveAnimation();
				if (LycansUtility.GameActuallyInPlay && ((SimulationBehaviour)this).Runner.IsServer)
				{
					NetworkPrefabId networkObject2 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectRunemasterRune");
					Vector3 position2 = ((Component)playerController).transform.position;
					NetworkObject val2 = ((SimulationBehaviour)this).Runner.Spawn(networkObject2, (Vector3?)((Component)playerController).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						//IL_0008: Unknown result type (might be due to invalid IL or missing references)
						((Component)no).transform.position = position2;
					}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val2).transform.position = position2;
					((Component)val2).GetComponent<RunemasterRune>().SetCreatorRef(playerController.Ref);
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRoleActionTimerExpired error: " + ex));
		}
	}

	public void StartSabotage(int index, float duration, bool subtly)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NewPrimaryRole == PlayerNewPrimaryRole.Traitor)
			{
				duration *= 1f;
			}
			PlayerController playerController = PlayerController;
			playerController.MovementAction = 0;
			playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
			SabotageObjectIndexTarget = index;
			SabotageTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, duration);
			if (subtly)
			{
				QuickSabotaging = NetworkBool.op_Implicit(false);
				SubtleSabotaging = NetworkBool.op_Implicit(true);
			}
			else
			{
				QuickSabotaging = NetworkBool.op_Implicit(true);
				SubtleSabotaging = NetworkBool.op_Implicit(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("StartSabotage error: " + ex));
		}
	}

	public void FinishSabotage()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			SabotageTimer = TickTimer.None;
			UpdateCanMoveAnimation();
			if (!((SimulationBehaviour)this).Runner.IsServer)
			{
				return;
			}
			QuickSabotaging = NetworkBool.op_Implicit(false);
			SubtleSabotaging = NetworkBool.op_Implicit(false);
			if (!NetworkBool.op_Implicit(PlayerController.IsDead) && NewPrimaryRole != PlayerNewPrimaryRole.VillageIdiot)
			{
				SabotageManager.Instance.OnSabotageCompleted(SabotageObjectIndexTarget);
				if (Stats != null)
				{
					Stats.AddAction(new PlayerStats.PlayerAction
					{
						ActionType = "Sabotage"
					}, ((Component)PlayerController).transform.position);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("FinishSabotage error: " + ex));
		}
	}

	[Preserve]
	public static void ExorcisedChanged(Changed<PlayerCustom> changed)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.UpdateAudition();
			if (NetworkBool.op_Implicit(behaviour.Exorcised))
			{
				behaviour.ExorcisedParticleSystem.GetComponent<ParticleSystem>().Play();
			}
			else
			{
				behaviour.ExorcisedParticleSystem.GetComponent<ParticleSystem>().Stop();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ExorcisedChanged error: " + ex));
		}
	}

	[Preserve]
	public static void QuickSabotagingChanged(Changed<PlayerCustom> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (!NetworkBool.op_Implicit(behaviour.QuickSabotaging) && ((SimulationBehaviour)behaviour).HasInputAuthority && !NetworkBool.op_Implicit(behaviour.PlayerController.IsDead) && behaviour.NewPrimaryRole != PlayerNewPrimaryRole.VillageIdiot)
			{
				AudioManager.Play("KILL_2", (MixerTarget)2, 0.25f, 1f);
			}
			if (NetworkBool.op_Implicit(behaviour.QuickSabotaging))
			{
				behaviour.PlayerAnimations.SetLoopAnimation("HumanM@Gathering01", active: true);
			}
			else
			{
				behaviour.PlayerAnimations.SetLoopAnimation("HumanM@Gathering01", active: false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SabotagingChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SubtleSabotagingChanged(Changed<PlayerCustom> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (!NetworkBool.op_Implicit(behaviour.SubtleSabotaging) && ((SimulationBehaviour)behaviour).HasInputAuthority && !NetworkBool.op_Implicit(behaviour.PlayerController.IsDead) && behaviour.NewPrimaryRole != PlayerNewPrimaryRole.VillageIdiot)
			{
				AudioManager.Play("KILL_2", (MixerTarget)2, 0.25f, 1f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SabotagingSubtlyChanged error: " + ex));
		}
	}

	[Preserve]
	public static void StinkingChanged(Changed<PlayerCustom> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (NetworkBool.op_Implicit(behaviour.Stinking))
			{
				if (behaviour.Ref == PlayerController.Local.Ref && !PlayerPrefs.HasKey("NALES_TUTORIAL_EFFECT_STINKING"))
				{
					UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_STINKING", 0.4f, 6f);
					PlayerPrefs.SetInt("NALES_TUTORIAL_EFFECT_STINKING", 1);
				}
			}
			else
			{
				behaviour.StinkingParticleSystem.GetComponent<ParticleSystem>().Clear();
			}
			behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("StinkingChanged error: " + ex));
		}
	}

	[Preserve]
	public static void RevertingChanged(Changed<PlayerCustom> changed)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
			PlayerController playerController = behaviour.PlayerController;
			if ((Object)(object)playerController == (Object)(object)povPlayer && NetworkBool.op_Implicit(behaviour.Reverting))
			{
				AudioManager.Play("BELL", (MixerTarget)2, 1f, 1f);
			}
			if (((SimulationBehaviour)playerController).HasStateAuthority && !NetworkBool.op_Implicit(behaviour.Reverting) && NetworkBool.op_Implicit(playerController.IsWolf))
			{
				string text = DateTime.UtcNow.ToString();
				NetworkString<_32> username = playerController.PlayerData.Username;
				LycansUtility.AddLogOnlyForMe("Adding detransformation from reverting at date: " + text + ", player: " + ((object)username/*cast due to constrained. prefix*/).ToString());
				playerController.IsWolf = NetworkBool.op_Implicit(false);
				playerController.Hunger = GameManager.Instance.MaxHunger;
				GameManagerCustom.Instance.AddDetransformation();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("RevertingChanged error: " + ex));
		}
	}

	[Preserve]
	public static void BlindChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController playerController = behaviour.PlayerController;
			if ((Object)(object)playerController == (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer)
			{
				ColorAdjustmentManager.UpdateColorAdjustment();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("BlindChanged error: " + ex));
		}
	}

	[Preserve]
	public static void IllusionTargetChanged(Changed<PlayerCustom> changed)
	{
		changed.Behaviour.UpdateIllusion(forceUpdatePet: true);
	}

	public void UpdateIllusion(bool forceUpdatePet = false)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController playerController = PlayerController;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			SkinnedMeshRenderer value = Traverse.Create((object)playerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value;
			PlayerController val = PlayerController;
			if (IllusionTarget != PlayerRef.None && !NetworkBool.op_Implicit(player.PlayerController.PlayerEffectManager.NightVision))
			{
				val = PlayerRegistry.GetPlayer(IllusionTarget);
			}
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(val.Ref);
			((Renderer)value).material.mainTexture = ColorManager.GetTexture(player2.ColorIndex);
			int childCount = playerController.hats.transform.childCount;
			foreach (object item in playerController.hats.transform)
			{
				((Component)(Transform)item).gameObject.SetActive(false);
			}
			int num = (int)Traverse.Create((object)val).Property("HatIndex", (object[])null).GetValue();
			if (num >= 0 && num < childCount)
			{
				((Component)playerController.hats.transform.GetChild(num)).gameObject.SetActive(true);
			}
			if (!NetworkBool.op_Implicit(player.PlayerController.PlayerEffectManager.Paranoia))
			{
				playerController.UpdateModel(NetworkBool.op_Implicit(playerController.IsWolf));
			}
			if (PetIndex != player2.PetIndex || forceUpdatePet)
			{
				if ((Object)(object)CurrentPet != (Object)null)
				{
					((SimulationBehaviour)this).Runner.Despawn(((Component)CurrentPet).GetComponent<NetworkObject>(), false);
				}
				if (player2.PetIndex > 0)
				{
					Vector3 val2 = ((Component)PlayerController).transform.position - ((Component)PlayerController).transform.forward * 0.8f;
					NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject(Plugin.PetNames[player2.PetIndex - 1]);
					NetworkObject val3 = ((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)val2, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val3).GetComponent<PlayerPetComponent>().Init(Ref, player2.PetIndex);
					((Component)val3).transform.position = val2;
				}
			}
			if (PrimaryRolePower == PlayerPrimaryRolePower.Warlock && IsCurrentlyPlayedOrObserved)
			{
				UpdateDescriptionStatusIfNeeded();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("IllusionTargetChanged error: " + ex));
		}
	}

	[Preserve]
	public static void DeafChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			changed.Behaviour.UpdateAudition();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DeafChanged error: " + ex));
		}
	}

	private void GenerateMerchantOffers()
	{
		List<MerchantOffer> list = MerchantOffer.GenerateOffers(this);
		foreach (MerchantOffer item in list)
		{
			Rpc_Send_Merchant_Offer(((SimulationBehaviour)this).Runner, Index, item.Index, item.Price, (int)item.Type, item.TypeIndex.HasValue ? item.TypeIndex.Value : (-1));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Send_Merchant_Offer(NetworkRunner runner, int playerIndex, int offerIndex, int price, int type, int typeIndex)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Invalid comparison between Unknown and I4
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 36;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Send_Merchant_Offer(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = offerIndex;
					num2 += 4;
					*(int*)(data + num2) = price;
					num2 += 4;
					*(int*)(data + num2) = type;
					num2 += 4;
					*(int*)(data + num2) = typeIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 20;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			player.CurrentMerchantOffers.RemoveAll((MerchantOffer o) => o.Index == offerIndex);
			MerchantOffer item = new MerchantOffer
			{
				Index = offerIndex,
				Price = price,
				Type = (MerchantOffer.MerchantOfferType)type,
				TypeIndex = ((typeIndex != -1) ? new int?(typeIndex) : ((int?)null))
			};
			player.CurrentMerchantOffers.Add(item);
			if (PlayerController.Local.Ref == player.Ref && UIManager.GenericChoicePanel.Active)
			{
				UIManager.GenericChoicePanel.RefreshMerchantOffers(player.CurrentMerchantOffers);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Send_Merchant_Offer error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Send_Merchant_Offer(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Send_Merchant_Offer_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int offerIndex = *(int*)(data + num);
		num += 4;
		int price = *(int*)(data + num);
		num += 4;
		int type = *(int*)(data + num);
		num += 4;
		int typeIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Send_Merchant_Offer(runner, playerIndex, offerIndex, price, type, typeIndex);
	}

	[Preserve]
	public static void TinyChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			changed.Behaviour.UpdateScaleAndPitch();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TinyChanged error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Assassinate(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Assassinate(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = player.PlayerController;
			PlayerController playerController2 = player2.PlayerController;
			if (player2.NewPrimaryRole == PlayerNewPrimaryRole.Mercenary && NetworkBool.op_Implicit(player2.NewPrimaryRoleUniqueBool))
			{
				if (runner.IsServer)
				{
					player2.Stats.UpdateDeathType("MERCENARY_HUNT_KILL");
					playerController2.Rpc_Kill(playerController.Ref);
					GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)playerController2).Runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerController2).transform.position, 30f, 1f);
					playerController.UpdateAnimation(Animator.StringToHash("Attacking"), true);
					((MonoBehaviour)playerController).StartCoroutine("WaitAndResetAttackAnimation");
				}
			}
			else
			{
				if (!NetworkBool.op_Implicit(player.Assassin) || NetworkBool.op_Implicit(playerController.IsDead) || NetworkBool.op_Implicit(playerController2.IsDead) || NetworkBool.op_Implicit(playerController2.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(player2.Petrified))
				{
					return;
				}
				if (runner.IsServer)
				{
					if (NetworkBool.op_Implicit(playerController2.IsWolf))
					{
						ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectDowned", ((SimulationBehaviour)playerController2).Runner, 1f, 6f);
						ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectResilience", ((SimulationBehaviour)playerController2).Runner, 1f, 9f);
						Vector3 forward = ((Component)playerController).transform.forward;
						Vector3 val = ((Vector3)(ref forward)).normalized;
						if (player2.NewPrimaryRole == PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
						{
							val *= 0.6f;
						}
						((Component)playerController2).GetComponent<KnockbackComponent>().Init(new Vector3(val.x, 0f, val.z), 6f, 6f);
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)playerController2).Runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerController2).transform.position, 30f, 1f);
					}
					else
					{
						player2.Stats.UpdateDeathType("ASSASSIN");
						playerController2.Rpc_Kill(playerController.Ref);
					}
					Effect val2 = playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is AssassinEffect);
					if ((Object)(object)val2 != (Object)null)
					{
						playerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val2).Object.Id);
					}
				}
				playerController.UpdateAnimation(Animator.StringToHash("Attacking"), true);
				((MonoBehaviour)playerController).StartCoroutine("WaitAndResetAttackAnimation");
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Assassinate error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Assassinate(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Assassinate_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Assassinate(runner, playerIndex, targetPlayerIndex);
	}

	[Preserve]
	public void CheckEffectTutorial()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(Assassin) && !PlayerPrefs.HasKey("NALES_TUTORIAL_EFFECT_ASSASSIN"))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_ASSASSIN", 0.4f, 6f);
				PlayerPrefs.SetInt("NALES_TUTORIAL_EFFECT_ASSASSIN", 1);
			}
			if (NetworkBool.op_Implicit(Clairvoyance) && !PlayerPrefs.HasKey("NALES_TUTORIAL_EFFECT_CLAIRVOYANCE"))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_CLAIRVOYANCE", 0.4f, 6f);
				PlayerPrefs.SetInt("NALES_TUTORIAL_EFFECT_CLAIRVOYANCE", 1);
			}
			if (NetworkBool.op_Implicit(Midas) && !PlayerPrefs.HasKey("NALES_TUTORIAL_EFFECT_MIDAS_MODIFIED"))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_MIDAS", 0.4f, 6f);
				PlayerPrefs.SetInt("NALES_TUTORIAL_EFFECT_MIDAS_MODIFIED", 1);
			}
			if (NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.NightVision) && !PlayerPrefs.HasKey("NALES_TUTORIAL_EFFECT_NIGHTVISION"))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_TRUESIGHT", 0.4f, 6f);
				PlayerPrefs.SetInt("NALES_TUTORIAL_EFFECT_NIGHTVISION", 1);
			}
			if (NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.Audition) && !PlayerPrefs.HasKey("NALES_TUTORIAL_EFFECT_AUDITION_PLUS"))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_AUDITION_PLUS", 0.4f, 6f);
				PlayerPrefs.SetInt("NALES_TUTORIAL_EFFECT_AUDITION_PLUS", 1);
			}
			if (NetworkBool.op_Implicit(Energized) && !PlayerPrefs.HasKey("NALES_TUTORIAL_EFFECT_ENERGIZED"))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_ENERGIZED", 0.4f, 6f);
				PlayerPrefs.SetInt("NALES_TUTORIAL_EFFECT_ENERGIZED", 1);
			}
			if (NetworkBool.op_Implicit(Immune) && !PlayerPrefs.HasKey("NALES_TUTORIAL_EFFECT_IMMUNE"))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_IMMUNE", 0.4f, 6f);
				PlayerPrefs.SetInt("NALES_TUTORIAL_EFFECT_IMMUNE", 1);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CheckEffectTutorial error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Petrify(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Petrify(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = player.PlayerController;
			PlayerController playerController2 = player2.PlayerController;
			if (!NetworkBool.op_Implicit(player.Midas) || NetworkBool.op_Implicit(playerController.IsDead) || NetworkBool.op_Implicit(playerController2.IsDead) || NetworkBool.op_Implicit(player2.Dying) || NetworkBool.op_Implicit(player2.Asleep))
			{
				return;
			}
			if (runner.IsServer)
			{
				foreach (NetworkId item in ((IEnumerable<NetworkId>)(object)playerController2.PlayerEffectManager.ActiveEffects).ToList())
				{
					Effect val = ((SimulationBehaviour)playerController2).Runner.TryGetNetworkedBehaviourFromNetworkedObjectRef<Effect>(item);
					if (!(val is CustomEffect) || (val as CustomEffect).CanBeDispelled)
					{
						playerController2.PlayerEffectManager.RemoveEffect(item);
					}
				}
				float value = (NetworkBool.op_Implicit(playerController2.IsWolf) ? 8f : 30f);
				ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectPetrified", ((SimulationBehaviour)playerController2).Runner, 1f, value);
				Effect val2 = playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is MidasEffect);
				if ((Object)(object)val2 != (Object)null)
				{
					playerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val2).Object.Id);
				}
			}
			player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackDirect1H01_R");
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Petrify error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Petrify(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Petrify_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Petrify(runner, playerIndex, targetPlayerIndex);
	}

	[Preserve]
	public static void PetrifiedChanged(Changed<PlayerCustom> changed)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(!NetworkBool.op_Implicit(changed.Behaviour.Petrified));
			behaviour.UpdateSkinColor();
			float speed = (NetworkBool.op_Implicit(behaviour.Petrified) ? 0f : 1f);
			Traverse.Create((object)behaviour.PlayerController).Field<Animator>("villagerAnimator").Value.speed = speed;
			Traverse.Create((object)behaviour.PlayerController).Field<Animator>("wolfAnimator").Value.speed = speed;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PetrifiedChanged error: " + ex));
		}
	}

	[Preserve]
	public static void PrimaryRoleTargetRefChanged(Changed<PlayerCustom> changed)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)GameManager.LocalGameState == 5 || (int)GameManager.LocalGameState == 1)
			{
				return;
			}
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController playerController = behaviour.PlayerController;
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				if (behaviour.NewPrimaryRole == PlayerNewPrimaryRole.Spy)
				{
					behaviour.UpdatePlayersWithSpecificColor(new List<PlayerRef> { behaviour.PrimaryRoleTargetRef });
					if (behaviour.PrimaryRoleTargetRef != PlayerRef.None)
					{
						AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
					}
				}
				else if (behaviour.NewPrimaryRole == PlayerNewPrimaryRole.Mercenary)
				{
					behaviour.UpdatePlayersWithSpecificColor(new List<PlayerRef> { behaviour.PrimaryRoleTargetRef });
					if (behaviour.PrimaryRoleTargetRef != PlayerRef.None)
					{
						AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
						AudioManager.Play("RELOAD", (MixerTarget)2, 0.35f, 1f);
					}
					foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
					{
						allPlayer.UpdateVisibility();
					}
					behaviour.UpdateDescriptionStatusIfNeeded();
				}
				else
				{
					switch (behaviour.PrimaryRolePower)
					{
					case PlayerPrimaryRolePower.Necromancer:
						behaviour.UpdateDescriptionStatusIfNeeded();
						break;
					case PlayerPrimaryRolePower.Warlock:
						behaviour.UpdateDescriptionStatusIfNeeded();
						break;
					case PlayerPrimaryRolePower.Possessor:
						behaviour.UpdateDescriptionStatusIfNeeded();
						break;
					case PlayerPrimaryRolePower.Investigator:
						if (behaviour.PrimaryRoleTargetRef != PlayerRef.None)
						{
							AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
						}
						break;
					}
				}
				behaviour.UpdateTargetArrowComponent();
			}
			if (behaviour.PrimaryRolePower == PlayerPrimaryRolePower.Predator && behaviour.PrimaryRoleTargetRef == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref)
			{
				UIManager.ShowRedCenterMessage("NALES_UI_PREDATOR_TARGET_WARNING", 0.5f, 4f);
				AudioManager.Play("WOLF", (MixerTarget)2, 1f, 1f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRoleTargetRefChanged error: " + ex));
		}
	}

	private void UpdatePlayersWithSpecificColor(List<PlayerRef> playersWithSpecificColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		Color color = Color.cyan;
		switch (NewPrimaryRole)
		{
		case PlayerNewPrimaryRole.Spy:
			color = PlayerColorInListForSpy;
			break;
		case PlayerNewPrimaryRole.Lover:
			color = PlayerColorInListForLovers;
			break;
		case PlayerNewPrimaryRole.Beast:
			color = PlayerColorInListForBeast;
			break;
		case PlayerNewPrimaryRole.Mercenary:
			color = PlayerColorInListForMercenary;
			break;
		case PlayerNewPrimaryRole.Voodoo:
			color = PlayerColorInListForVoodoo;
			break;
		case PlayerNewPrimaryRole.Kidnapper:
			color = PlayerColorInListForKidnapper;
			break;
		}
		switch (PrimaryRolePower)
		{
		case PlayerPrimaryRolePower.Necromancer:
			color = PlayerColorInListForNecromancer;
			break;
		case PlayerPrimaryRolePower.Warlock:
			color = PlayerColorInListForWarlock;
			break;
		case PlayerPrimaryRolePower.Possessor:
			color = PlayerColorInListForDetective;
			break;
		case PlayerPrimaryRolePower.Predator:
			color = PlayerColorInListForPredator;
			break;
		case PlayerPrimaryRolePower.Host:
			color = PlayerColorInListForHost;
			break;
		case PlayerPrimaryRolePower.Investigator:
			color = PlayerColorInListForInvestigator;
			break;
		}
		Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
		foreach (KeyValuePair<PlayerRef, PlayerDisplay> item in value)
		{
			TextMeshProUGUI value2 = Traverse.Create((object)item.Value).Field<TextMeshProUGUI>("username").Value;
			if (playersWithSpecificColor.Contains(item.Key))
			{
				((Graphic)value2).color = color;
			}
			else if (Traverse.Create((object)item.Value).Field<bool>("_isDead").Value)
			{
				((Graphic)value2).color = new Color(255f, 255f, 255f, 0.2f);
			}
			else
			{
				((Graphic)value2).color = new Color(255f, 255f, 255f, 0.6f);
			}
		}
		PlayersWithSpecificColor = playersWithSpecificColor;
	}

	public void SpyGiveNewTarget()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld && !NetworkBool.op_Implicit(o.Resurrected) && o.NewPrimaryRole != PlayerNewPrimaryRole.Zombie && o.Ref != Ref).ToList();
			List<PlayerCustom> preferredTargets = list.Where((PlayerCustom o) => !PrimaryRolePowerPlayersList.Contains(o.Ref) && (int)o.PlayerController.Role != 1 && Vector3.Distance(o.ActualPositionIncludingTeleport(), ActualPositionIncludingTeleport()) <= 80f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID)).ToList();
			PlayerCustom playerCustom = FindTarget(preferredTargets, list);
			if ((Object)(object)playerCustom != (Object)null)
			{
				PrimaryRoleTargetRef = playerCustom.Ref;
				PrimaryRolePowerPlayersList.Clear();
				PrimaryRolePowerPlayersList.Add(playerCustom.Ref);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SpyGiveNewTarget error: " + ex));
		}
	}

	public void InvestigatorGiveNewTarget()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld && !NetworkBool.op_Implicit(o.Resurrected) && o.NewPrimaryRole != PlayerNewPrimaryRole.Zombie && o.Ref != Ref).ToList();
			if (!list.Any())
			{
				Plugin.Logger.LogInfo((object)"Investigator: no target found");
				return;
			}
			List<PlayerCustom> preferredTargets = list.Where((PlayerCustom o) => Vector3.Distance(o.ActualPositionIncludingTeleport(), ActualPositionIncludingTeleport()) <= 25f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID)).ToList();
			PlayerCustom playerCustom = FindTarget(preferredTargets, list);
			if ((Object)(object)playerCustom != (Object)null)
			{
				PrimaryRoleTargetRef = playerCustom.Ref;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SpyGiveNewTarget error: " + ex));
		}
	}

	private PlayerCustom FindTarget(List<PlayerCustom> preferredTargets, List<PlayerCustom> allTargets)
	{
		if (preferredTargets.Any())
		{
			return CollectionsUtil.Grab<PlayerCustom>(preferredTargets, 1).First();
		}
		if (allTargets.Any())
		{
			return CollectionsUtil.Grab<PlayerCustom>(allTargets, 1).First();
		}
		return null;
	}

	public PlayerCustom FindLoverPartner()
	{
		return PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Lover && o.Ref != Ref).FirstOrDefault();
	}

	[Preserve]
	public static void SecondaryRoleTeleportDataChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			UpdateTeleporterBeaconOnMinimap();
			PlayerCustom behaviour = changed.Behaviour;
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				behaviour.UpdateDescriptionStatusIfNeeded();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleTeleportDataChanged error: " + ex));
		}
	}

	public static void UpdateTeleporterBeaconOnMinimap()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Plugin.Minimap.RemoveTeleporterBeaconIcon();
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		NetworkTeleportData secondaryRoleTeleportData = player.SecondaryRoleTeleportData;
		if (!((NetworkTeleportData)(ref secondaryRoleTeleportData)).IsNone)
		{
			MinimapComponent minimap = Plugin.Minimap;
			secondaryRoleTeleportData = player.SecondaryRoleTeleportData;
			minimap.AddTeleporterBeaconIcon(((NetworkTeleportData)(ref secondaryRoleTeleportData)).Position);
		}
	}

	public void MercenaryGiveNewTarget()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld && !NetworkBool.op_Implicit(o.Resurrected) && o.NewPrimaryRole != PlayerNewPrimaryRole.Zombie && !NetworkBool.op_Implicit(o.Isolation) && o.Ref != Ref).ToList();
			List<PlayerCustom> preferredTargets = list.Where((PlayerCustom o) => !MercenaryTargetsAlreadyHit.Contains(o.Ref)).ToList();
			PlayerCustom playerCustom = FindTarget(preferredTargets, list);
			if ((Object)(object)playerCustom != (Object)null)
			{
				PrimaryRoleTargetRef = playerCustom.Ref;
				PlayerController.IsGunLoaded = NetworkBool.op_Implicit(true);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MercenaryGiveNewTarget error: " + ex));
		}
	}

	[Preserve]
	public static void AsleepChanged(Changed<PlayerCustom> changed)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.UpdateVisibility();
			behaviour.UpdateAudition();
			if ((Object)(object)behaviour.PlayerController == (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer)
			{
				ColorAdjustmentManager.UpdateColorAdjustment();
			}
			behaviour.UpdateCanMoveAnimation();
			if (NetworkBool.op_Implicit(behaviour.Asleep))
			{
				behaviour.PlayerController.UpdateAnimation(Animator.StringToHash("Dead"), true);
				behaviour.PlayerController.EnablePlayerHitBox(false);
			}
			else if (!NetworkBool.op_Implicit(behaviour.PlayerController.IsDead))
			{
				behaviour.PlayerController.UpdateAnimation(Animator.StringToHash("Dead"), false);
				behaviour.PlayerController.EnablePlayerHitBox(true);
			}
			Traverse.Create((object)behaviour.PlayerController).Method("UpdateCollider", Array.Empty<object>()).GetValue();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("AsleepChanged error: " + ex));
		}
	}

	[Preserve]
	public static void KidnappedChanged(Changed<PlayerCustom> changed)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Invalid comparison between Unknown and I4
		PlayerCustom playerCustom = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Kidnapper).FirstOrDefault();
		if (NetworkBool.op_Implicit(changed.Behaviour.Kidnapped))
		{
			changed.Behaviour.CurseDormant = NetworkBool.op_Implicit(false);
			changed.Behaviour.BombDormant = NetworkBool.op_Implicit(false);
			changed.Behaviour.PlayerController.PlayerEffectManager.ClearEffects();
			((Component)changed.Behaviour.PlayerController).transform.position = new Vector3(999f, 999f, 999f);
			changed.Behaviour.PlayerController.IsClimbing = NetworkBool.op_Implicit(false);
			if (Local.NewPrimaryRole == PlayerNewPrimaryRole.Kidnapper || NetworkBool.op_Implicit(Local.Kidnapped))
			{
				GameManager.Instance.gameUI.UpdateDeadPlayer(changed.Behaviour.Ref);
			}
		}
		else
		{
			Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
			if (value.TryGetValue(changed.Behaviour.Ref, out var value2))
			{
				Image value3 = Traverse.Create((object)value2).Field<Image>("deadIcon").Value;
				if ((Object)(object)UIManager.DefaultDeadPlayerIcon != (Object)null)
				{
					value3.sprite = UIManager.DefaultDeadPlayerIcon;
				}
				((Behaviour)value3).enabled = false;
			}
		}
		if (changed.Behaviour.Ref == PlayerController.Local.Ref)
		{
			if (NetworkBool.op_Implicit(changed.Behaviour.Kidnapped))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_KIDNAPPED", 0.5f, 4f);
				PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerRegistry.GetPlayer(playerCustom.Ref));
			}
			else
			{
				PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerController.Local);
			}
			ColorAdjustmentManager.UpdateColorAdjustment();
		}
		if (NetworkBool.op_Implicit(PlayerController.Local.IsDead) && PlayerController.Local.LocalCameraHandler.PovPlayer.Ref == changed.Behaviour.Ref && NetworkBool.op_Implicit(changed.Behaviour.Kidnapped) && (Object)(object)playerCustom != (Object)null)
		{
			PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerRegistry.GetPlayer(playerCustom.Ref));
		}
		if (((SimulationBehaviour)changed.Behaviour).Runner.IsServer && !NetworkBool.op_Implicit(changed.Behaviour.Kidnapped) && (int)GameManager.LocalGameState == 2)
		{
			changed.Behaviour.TeleportToRandomTeleporter();
		}
		if (playerCustom.IsCurrentlyPlayedOrObserved)
		{
			changed.Behaviour.CustomAudio.UpdateVolume();
		}
		changed.Behaviour.UpdateCanMoveAnimation();
		Local.UpdatePrimaryRole();
		playerCustom.UpdateDescriptionStatusIfNeeded();
		changed.Behaviour.UpdateDescriptionStatusIfNeeded();
	}

	[Preserve]
	public static void ConfusedChanged(Changed<PlayerCustom> changed)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				if (NetworkBool.op_Implicit(behaviour.Confused))
				{
					GameManager.LightingManager.UpdateWolfEffect(1f);
				}
				else
				{
					GameManager.LightingManager.UpdateWolfEffect(0f);
				}
			}
			behaviour.UpdateScaleAndPitch();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DrunkChanged error: " + ex));
		}
	}

	[Preserve]
	public static void ResurrectedChanged(Changed<PlayerCustom> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Invalid comparison between Unknown and I4
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (!NetworkBool.op_Implicit(behaviour.Resurrected) && !NetworkBool.op_Implicit(behaviour.PlayerController.IsDead) && (int)GameManager.LocalGameState != 5 && (int)GameManager.LocalGameState != 1)
			{
				behaviour.PlayerController.IsDead = NetworkBool.op_Implicit(true);
			}
			if (NetworkBool.op_Implicit(behaviour.Resurrected))
			{
				behaviour.HasZombieColor = true;
			}
			((Component)behaviour.PlayerController).GetComponent<PlayerResurrectedComponent>().UpdateState();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ResurrectedChanged error: " + ex));
		}
	}

	[Preserve]
	public static void PossessedChanged(Changed<PlayerCustom> changed)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom playerCustom = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Possessor).FirstOrDefault();
		changed.Behaviour.AlreadyPossessed = true;
		if (NetworkBool.op_Implicit(changed.Behaviour.Possessed))
		{
			changed.Behaviour.CurseDormant = NetworkBool.op_Implicit(false);
			changed.Behaviour.BombDormant = NetworkBool.op_Implicit(false);
		}
		if (changed.Behaviour.Ref == PlayerController.Local.Ref)
		{
			if (NetworkBool.op_Implicit(changed.Behaviour.Possessed))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_POSSESSOR_POSSESSED", 0.5f, 4f);
				PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerRegistry.GetPlayer(playerCustom.Ref));
			}
			else
			{
				PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerController.Local);
				if ((Object)(object)playerCustom != (Object)null)
				{
					playerCustom.PrimaryRoleTargetRef = PlayerRef.None;
					playerCustom.PrimaryRolePowerCurrentMaterials = 0;
				}
			}
		}
		if (NetworkBool.op_Implicit(PlayerController.Local.IsDead) && PlayerController.Local.LocalCameraHandler.PovPlayer.Ref == changed.Behaviour.Ref && NetworkBool.op_Implicit(changed.Behaviour.Possessed) && (Object)(object)playerCustom != (Object)null)
		{
			PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerRegistry.GetPlayer(playerCustom.Ref));
		}
		Local.UpdatePrimaryRole();
		playerCustom.UpdateDescriptionStatusIfNeeded();
		changed.Behaviour.UpdateDescriptionStatusIfNeeded();
	}

	[Rpc]
	public unsafe static void Rpc_Manipulate_Item(NetworkRunner runner, int playerIndex, NetworkId itemId)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Manipulate_Item(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					Unsafe.Write(data + num2, itemId);
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerPrimaryRolePower primaryRolePower = player.PrimaryRolePower;
			PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
			if (playerPrimaryRolePower == PlayerPrimaryRolePower.Alchemist)
			{
				Loot component = ((Component)runner.FindObject(itemId)).GetComponent<Loot>();
				GameObject val = Object.Instantiate<GameObject>(AlchemistTransformParticleSystemPrefab, ((Component)component).transform.position, Quaternion.identity);
				val.SetActive(true);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
				selfDestroyingObjectComponent.Init(1.5f);
			}
			if (!runner.IsServer)
			{
				return;
			}
			switch (player.PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Saboteur:
				if (player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials)
				{
					ItemCustom componentInChildren = ((Component)runner.FindObject(itemId)).GetComponentInChildren<ItemCustom>();
					componentInChildren.Sabotaged = NetworkBool.op_Implicit(true);
					player.ReduceMaterialAfterPowerUse();
				}
				break;
			case PlayerPrimaryRolePower.Alchemist:
			{
				Loot component2 = ((Component)runner.FindObject(itemId)).GetComponent<Loot>();
				component2.Available = NetworkBool.op_Implicit(false);
				Vector3 position = ((Component)component2).transform.position;
				Potion value = Traverse.Create((object)GameManager.Instance).Field<Potion>("potionPrefab").Value;
				Effect alchemistPotionEffect = CollectionsUtil.Grab<Effect>((from o in EffectManager.GetEffects()
					where Plugin.CustomConfig.PotionsAvailability[o.GetTranslateKey()] && (int)o.GetEffectType() == 0
					select o).ToList(), 1).First();
				runner.Spawn<Potion>(value, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					Color[] value2 = Traverse.Create(typeof(Potion)).Field<Color[]>("PotionColors").Value;
					((Component)no).GetComponent<Potion>().Init(value2.Length - 1, EffectManager.GetEffectIndex(alchemistPotionEffect));
				}, (NetworkObjectPredictionKey?)null, true);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("AlchemistT"), position, 20f, 0.8f);
				player.ReduceMaterialAfterPowerUse();
				break;
			}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Manipulate_Item error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Manipulate_Item(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")]
	[Preserve]
	protected unsafe static void Rpc_Manipulate_Item_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkId itemId = (NetworkId)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Manipulate_Item(runner, playerIndex, itemId);
	}

	[Rpc]
	public unsafe static void Rpc_Use_Sabotaged_Item(NetworkRunner runner, int playerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Use_Sabotaged_Item(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerController playerController = player.PlayerController;
			if (runner.IsServer && (Object)(object)playerController.Item != (Object)null && !(playerController.Item is Potion) && NetworkBool.op_Implicit(((Component)playerController.Item).GetComponentInChildren<ItemCustom>().Sabotaged))
			{
				playerController.Hunger = Mathf.Max((float)GameManager.Instance.MaxHunger * 0.05f, playerController.Hunger - (float)GameManager.Instance.MaxHunger * 0.25f);
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectWounded", ((SimulationBehaviour)playerController).Runner);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("UseTrappedItem"), ((Component)playerController).transform.position, 20f, 1f);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("BULLET_HIT"), ((Component)playerController).transform.position, 20f, 1f);
				playerController.Item.DestroyItem();
			}
			GameObject val = Object.Instantiate<GameObject>(TrappedItemExplosionParticleSystemPrefab, playerController.FindVillagerItem().position, Quaternion.identity);
			val.SetActive(true);
			SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
			selfDestroyingObjectComponent.Init(4f);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Use_Sabotaged_Item error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Use_Sabotaged_Item(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Use_Sabotaged_Item_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Use_Sabotaged_Item(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Loot_Corpse(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Invalid comparison between Unknown and I4
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBehaviourUtils.InvokeRpc)
		{
			NetworkBehaviourUtils.InvokeRpc = false;
		}
		else
		{
			if ((Object)(object)runner == (Object)null)
			{
				throw new ArgumentNullException("runner");
			}
			if ((int)runner.Stage == 4)
			{
				return;
			}
			if (runner.HasAnyActiveConnections())
			{
				int num = 24;
				SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
				byte* data = SimulationMessage.GetData(ptr);
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Loot_Corpse(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
				*(int*)(data + num2) = playerIndex;
				num2 += 4;
				*(int*)(data + num2) = targetPlayerIndex;
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 8;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
		PlayerController playerController = PlayerCustomRegistry.GetPlayer(targetPlayerIndex).PlayerController;
		TickTimer lootCorpseTimer = player.LootCorpseTimer;
		if (!((TickTimer)(ref lootCorpseTimer)).IsRunning && (Object)(object)playerController.Item != (Object)null && NetworkBool.op_Implicit(playerController.IsDead))
		{
			player.LootCorpseTarget = playerController.Ref;
			player.LootCorpseTimer = TickTimer.CreateFromSeconds(runner, 3f);
			player.UpdateCanMoveAnimation();
			if (((SimulationBehaviour)player).HasInputAuthority)
			{
				UIManager.UpdateTimer(player.LootCorpseTimer, ((SimulationBehaviour)player).Runner, "NALES_UI_ACTION_LOOTING_CORPSE");
			}
		}
		player.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: true);
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Loot_Corpse(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Loot_Corpse(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Loot_Corpse(runner, playerIndex, targetPlayerIndex);
	}

	private void FinishLootCorpse()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		PlayerController player = PlayerRegistry.GetPlayer(LootCorpseTarget);
		if (NetworkBool.op_Implicit(player.IsDead) && (Object)(object)player.Item != (Object)null)
		{
			Item item = player.Item;
			((NetworkBehaviour)item).CopyStateToBackingFields();
			Traverse.Create((object)item).Field("_Owner").SetValue((object)PlayerRef.None);
			((NetworkBehaviour)item).CopyBackingFieldsToState(true);
			((SimulationBehaviour)item).Object.AssignInputAuthority(PlayerRef.None);
			player.Item = null;
			item.Rpc_ClaimItem(Ref);
		}
		LootCorpseTarget = PlayerRef.None;
		LootCorpseTimer = TickTimer.None;
		UpdateCanMoveAnimation();
		PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
	}

	public void UpdateIconAbovePlayer(bool visible)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Invalid comparison between Unknown and I4
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Invalid comparison between Unknown and I4
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		if (!visible)
		{
			SetIconAbovePlayer(IconAbovePlayerType.None);
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (NetworkBool.op_Implicit(player.Phasing) || (NetworkBool.op_Implicit(player.Isolation) && !NetworkBool.op_Implicit(PlayerController.IsWolf) && NewPrimaryRole != PlayerNewPrimaryRole.Zombie) || (NetworkBool.op_Implicit(Isolation) && !NetworkBool.op_Implicit(player.PlayerController.IsWolf)) || NetworkBool.op_Implicit(Phasing) || NetworkBool.op_Implicit(Disappeared) || NetworkBool.op_Implicit(PlayerController.IsDead) || Ref == player.Ref)
		{
			SetIconAbovePlayer(IconAbovePlayerType.None);
		}
		else if (player.NewPrimaryRole == PlayerNewPrimaryRole.Mercenary && Ref == player.PrimaryRoleTargetRef)
		{
			SetIconAbovePlayer(IconAbovePlayerType.MercenaryTargetIcon);
		}
		else if (NetworkBool.op_Implicit(Dying) && (!NetworkBool.op_Implicit(PlayerController.Local.IsWolf) || NetworkBool.op_Implicit(PlayerController.Local.IsDead)) && Ref != PlayerController.Local.LocalCameraHandler.PovPlayer.Ref)
		{
			SetIconAbovePlayer(IconAbovePlayerType.SurvivalistDyingIcon);
		}
		else if (CamouflageLevelForPovPlayer == 0 && (NetworkBool.op_Implicit(BombDormant) || NetworkBool.op_Implicit(BombActive)) && ((int)player.PlayerController.Role == 1 || player.NewPrimaryRole == PlayerNewPrimaryRole.Traitor))
		{
			SetIconAbovePlayer(IconAbovePlayerType.BombIcon);
		}
		else if (NetworkBool.op_Implicit(player.PlayerController.IsWolf) && (int)player.PlayerController.Role == 1 && NetworkBool.op_Implicit(Angel))
		{
			SetIconAbovePlayer(IconAbovePlayerType.AngelShield);
		}
		else if ((Object)(object)player.SummonedSpirit != (Object)null && NetworkBool.op_Implicit(player.SummonedSpirit.HasFocus) && PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is SpiritResistanceEffect))
		{
			SetIconAbovePlayer(IconAbovePlayerType.AngelShield);
		}
		else if (CamouflageLevelForPovPlayer == 0 && player.SecondaryRole == PlayerSecondaryRole.BothForger && !NetworkBool.op_Implicit(PlayerController.IsWolf) && !NetworkBool.op_Implicit(PlayerController.IsDead) && !NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.Invisible) && !NetworkBool.op_Implicit(Sneaky) && !NetworkBool.op_Implicit(player.PlayerController.PlayerEffectManager.Paranoia))
		{
			SetIconAbovePlayer(IconAbovePlayerType.HeldItem);
		}
		else
		{
			SetIconAbovePlayer(IconAbovePlayerType.None);
		}
	}

	private void SetIconAbovePlayer(IconAbovePlayerType icon)
	{
		((Component)_playerController).GetComponent<PlayerMercenaryTargetIconComponent>().SetVisible(icon == IconAbovePlayerType.MercenaryTargetIcon);
		((Component)_playerController).GetComponent<PlayerBombIconComponent>().SetVisible(icon == IconAbovePlayerType.BombIcon);
		((Component)_playerController).GetComponent<PlayerDyingComponent>().SetVisible(icon == IconAbovePlayerType.SurvivalistDyingIcon);
		((Component)_playerController).GetComponent<PlayerSurvivalistHeartbeatComponent>().ToggleEffect(icon == IconAbovePlayerType.SurvivalistDyingIcon);
		((Component)_playerController).GetComponent<PlayerHeldItemComponent>().SetVisible(icon == IconAbovePlayerType.HeldItem);
		((Component)_playerController).GetComponent<PlayerAngelIconComponent>().SetVisible(icon == IconAbovePlayerType.AngelShield);
	}

	public void UpdateSkinColor()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(Petrified))
		{
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).material.color = SkinColorPetrified;
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = SkinColorPetrified;
		}
		else if (HasZombieColor)
		{
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).material.color = SkinColorZombieHuman;
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = SkinColorZombieWolf;
		}
		else if (NetworkBool.op_Implicit(Poison))
		{
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).material.color = SkinColorPoison;
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = SkinColorPoison;
		}
		else
		{
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).material.color = Color.white;
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = WolfColor;
		}
	}

	[Rpc]
	public unsafe static void Rpc_Give_Bomb(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Invalid comparison between Unknown and I4
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Give_Bomb(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = player.PlayerController;
			PlayerController targetPlayer = player2.PlayerController;
			if (!NetworkBool.op_Implicit(player.BombActive) || NetworkBool.op_Implicit(player2.BombActive) || NetworkBool.op_Implicit(player.Panic) || NetworkBool.op_Implicit(targetPlayer.IsDead) || !runner.IsServer)
			{
				return;
			}
			Effect val = playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is BombEffect);
			if ((Object)(object)val != (Object)null)
			{
				CustomTickTimer value = Traverse.Create((object)val).Property<CustomTickTimer>("EffectTimer", (object[])null).Value;
				float duration = (float)(Traverse.Create((object)value).Field<int>("_target").Value - Tick.op_Implicit(runner.Simulation.Tick)) / (float)runner.Config.Simulation.TickRate;
				duration = Mathf.Max(duration, 1f);
				NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.EffectBomb");
				((SimulationBehaviour)targetPlayer).Runner.Spawn(networkObject, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					CustomEffect component = ((Component)no).GetComponent<CustomEffect>();
					component.InitWithSpecificDuration(targetPlayer, duration);
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectFleeing", ((SimulationBehaviour)playerController).Runner);
				playerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
				ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectPanic", ((SimulationBehaviour)playerController).Runner);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Give_Bomb error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Give_Bomb(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Give_Bomb_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Give_Bomb(runner, playerIndex, targetPlayerIndex);
	}

	[Preserve]
	public static void BombActiveChanged(Changed<PlayerCustom> changed)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.UpdateVisibility();
			PlayerController playerController = behaviour.PlayerController;
			if (NetworkBool.op_Implicit(playerController.IsDead) || NetworkBool.op_Implicit(behaviour.Kidnapped) || !LycansUtility.GameActuallyInPlay)
			{
				((Component)playerController).GetComponent<PlayerBombTickingComponent>().ToggleTickEffect(active: false);
				return;
			}
			((Component)playerController).GetComponent<PlayerBombTickingComponent>().ToggleTickEffect(NetworkBool.op_Implicit(behaviour.BombActive));
			if (NetworkBool.op_Implicit(behaviour.BombActive))
			{
				if (playerController.Ref == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref)
				{
					ColorAdjustmentManager.FlashScreen(Color.red);
				}
			}
			else
			{
				if (NetworkBool.op_Implicit(behaviour.Fleeing) || behaviour.PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is FleeingEffect))
				{
					return;
				}
				behaviour.FlashPlayer(Color.red);
				if (!((SimulationBehaviour)behaviour).Runner.IsServer || NetworkBool.op_Implicit(playerController.IsDead))
				{
					return;
				}
				PlayerCustom specificPrimaryRolePower = PlayerCustomRegistry.GetSpecificPrimaryRolePower(PlayerPrimaryRolePower.Bomber);
				PlayerRef val = (((Object)(object)specificPrimaryRolePower != (Object)null) ? specificPrimaryRolePower.Ref : PlayerRef.None);
				Rpc_Effect_On_Player(((SimulationBehaviour)behaviour).Runner, behaviour.Index, 11);
				if (NetworkBool.op_Implicit(behaviour.Petrified))
				{
					return;
				}
				float num = ((!NetworkBool.op_Implicit(playerController.IsWolf)) ? 0.35f : (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) ? 0.15f : 0.35f));
				if ((double)playerController.Hunger / (double)GameManager.Instance.MaxHunger < (double)num)
				{
					behaviour.Stats.UpdateDeathType("BOMB");
					playerController.Rpc_Kill(val);
					return;
				}
				playerController.Hunger -= num * (float)GameManager.Instance.MaxHunger;
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectWounded", ((SimulationBehaviour)behaviour).Runner, 1f, NetworkBool.op_Implicit(playerController.IsWolf) ? 30f : 180f);
				Effect effect = EffectManager.GetEffects().First((Effect o) => o is GlowingEffect);
				ApplyEffectToPlayer(playerController, effect, ((SimulationBehaviour)behaviour).Runner, 1f, 180f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("BombActiveChanged error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Try_Guess_Power(NetworkRunner runner, int playerIndex, int targetIndex, int powerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Invalid comparison between Unknown and I4
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Invalid comparison between Unknown and I4
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 32;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Try_Guess_Power(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetIndex;
					num2 += 4;
					*(int*)(data + num2) = powerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetIndex);
			PlayerController playerController = player.PlayerController;
			PlayerController playerController2 = player2.PlayerController;
			if (NetworkBool.op_Implicit(playerController2.IsDead) || !(player.Accessory is AccessoryCrystalBall { Available: not false } accessoryCrystalBall))
			{
				return;
			}
			bool flag = (int)playerController.Role == 1 || player.NewPrimaryRole != PlayerNewPrimaryRole.None;
			bool flag2 = player2.PrimaryRolePower == AllJobs[powerIndex];
			if (runner.IsServer && flag2)
			{
				if ((int)playerController2.Role == 1 || player2.NewPrimaryRole == PlayerNewPrimaryRole.Traitor)
				{
					player2.GivePrimaryRolePower(PlayerPrimaryRolePower.None);
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectWeakened", runner, 1f, 3600f);
				}
				else
				{
					player2.Stats.UpdateDeathType("SEER");
					Rpc_Effect_On_Player(runner, player2.Index, 9);
					player2.PlayerController.IdVoted = -1;
					player2.PlayerController.Rpc_Kill(player.Ref);
					GameManager.Rpc_DisplayDeadPlayers(runner);
				}
			}
			accessoryCrystalBall.Available = false;
			if (((SimulationBehaviour)player).HasInputAuthority)
			{
				UIManager.GenericChoicePanel.Hide();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Try_Guess_Power error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Try_Guess_Power(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Try_Guess_Power0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetIndex = *(int*)(data + num);
		num += 4;
		int powerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Try_Guess_Power(runner, playerIndex, targetIndex, powerIndex);
	}

	[Preserve]
	public static void PoacherMarkChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.UpdatePoacherMarkVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("BombActiveChanged error: " + ex));
		}
	}

	public void UpdatePoacherMarkVisibility()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Invalid comparison between Unknown and I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(PoacherMark) && NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.IsWolf) && (int)PlayerController.Local.LocalCameraHandler.PovPlayer.Role == 1 && PlayerController.Local.LocalCameraHandler.PovPlayer.Ref != Ref)
		{
			((Component)PlayerController).GetComponent<PlayerPoacherMarkComponent>().SetPoacherMarkActive(active: true);
		}
		else
		{
			((Component)PlayerController).GetComponent<PlayerPoacherMarkComponent>().SetPoacherMarkActive(active: false);
		}
	}

	[Rpc]
	public unsafe static void Rpc_Custom_Shot_With_Target(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Invalid comparison between Unknown and I4
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Expected O, but got Unknown
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Expected O, but got Unknown
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Custom_Shot_With_Target(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = player.PlayerController;
			PlayerController targetPlayer = player2.PlayerController;
			if (!runner.IsServer)
			{
				return;
			}
			if ((Object)(object)playerController.Item != (Object)null && ((object)playerController.Item).GetType() == typeof(BulletItem))
			{
				playerController.Item.StartDelayTimer(10f);
			}
			PlayerPrimaryRolePower primaryRolePower = player.PrimaryRolePower;
			PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
			if (playerPrimaryRolePower == PlayerPrimaryRolePower.Poacher)
			{
				playerController.IsGunLoaded = NetworkBool.op_Implicit(false);
				player2.PoacherMark = NetworkBool.op_Implicit(true);
				Rpc_ShowShot_Custom(runner, playerController.Index);
			}
			PlayerNewPrimaryRole newPrimaryRole = player.NewPrimaryRole;
			PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
			if (playerNewPrimaryRole == PlayerNewPrimaryRole.Mercenary)
			{
				playerController.IsGunLoaded = NetworkBool.op_Implicit(false);
				ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectAsleep", runner, 1f, NetworkBool.op_Implicit(targetPlayer.IsWolf) ? 8f : 30f);
				Rpc_ShowShot_Custom(runner, playerController.Index);
				if (targetPlayer.Ref == player.PrimaryRoleTargetRef)
				{
					player.AddSoloRoleProgress(200, player.SoloRoleObjectiveTarget);
					player.PrimaryRoleTargetRef = PlayerRef.None;
					player.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(runner, 90f);
					player.MercenaryTargetsAlreadyHit.Add(targetPlayer.Ref);
				}
			}
			PlayerSecondaryRole secondaryRole = player.SecondaryRole;
			PlayerSecondaryRole playerSecondaryRole = secondaryRole;
			if (playerSecondaryRole != PlayerSecondaryRole.BothCarabineer)
			{
				return;
			}
			playerController.IsGunLoaded = NetworkBool.op_Implicit(false);
			player.TriggerSecondaryRolePowerCooldown(runner);
			Vector3 forward = ((Component)playerController).transform.forward;
			Vector3 val = ((Vector3)(ref forward)).normalized;
			if (player2.NewPrimaryRole == PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				val *= 0.6f;
			}
			((Component)targetPlayer).GetComponent<KnockbackComponent>().Init(new Vector3(val.x, 0f, val.z), 14f, 9.5f);
			if (NetworkBool.op_Implicit(targetPlayer.IsWolf))
			{
				NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.EffectDisoriented");
				runner.Spawn(networkObject, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<CustomEffect>().InitWithSpecificDuration(targetPlayer, 3f);
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				((Component)targetPlayer).GetComponent<ForcedRotationComponent>().Init(new Vector3(0f, 1f, 0f), 900f, 700f);
				ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectResilience", runner, 1f, 3f);
			}
			else
			{
				NetworkPrefabId networkObject2 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.EffectDowned");
				runner.Spawn(networkObject2, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<CustomEffect>().InitWithSpecificDuration(targetPlayer, 4f);
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			}
			Rpc_ShowShot_Custom(runner, playerController.Index);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Custom_Shot_With_Target error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Custom_Shot_With_Target(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Custom_Shot_With_Target_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Custom_Shot_With_Target(runner, playerIndex, targetPlayerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Custom_Shot_Without_Target(NetworkRunner runner, int playerIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Custom_Shot_Without_Target(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (runner.IsServer)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
				PlayerController playerController = player.PlayerController;
				playerController.IsGunLoaded = NetworkBool.op_Implicit(false);
				if ((Object)(object)playerController.Item != (Object)null && ((object)playerController.Item).GetType() == typeof(BulletItem))
				{
					playerController.Item.StartDelayTimer(10f);
				}
				if (player.SecondaryRole == PlayerSecondaryRole.BothCarabineer)
				{
					player.TriggerSecondaryRolePowerCooldown(runner);
				}
				Rpc_ShowShot_Custom(runner, playerController.Index);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Custom_Shot_Without_Target error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Custom_Shot_Without_Target(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Custom_Shot_Without_TargetInvoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Custom_Shot_Without_Target(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_ShowShot_Custom(NetworkRunner runner, int playerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_ShowShot_Custom(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerController playerController = player.PlayerController;
			float num3 = Vector3.Distance(((Component)PlayerController.Local.LocalCameraHandler.PovPlayer).transform.position, ((Component)playerController).transform.position);
			if (player.PrimaryRolePower == PlayerPrimaryRolePower.Poacher)
			{
				if (num3 < 20f)
				{
					AudioManager.PlayAndFollow("BountyHunterShot", ((Component)playerController).transform, (MixerTarget)2, 20f, 0.45f);
				}
			}
			else if (player.SecondaryRole == PlayerSecondaryRole.BothCarabineer)
			{
				if (num3 < 35f)
				{
					AudioManager.PlayAndFollow("SHOT_CLOSE", ((Component)playerController).transform, (MixerTarget)2, 50f, 0.5f);
				}
				else
				{
					AudioManager.PlayAndFollow("SHOT_LONG", ((Component)playerController).transform, (MixerTarget)2, 500f, 0.25f);
				}
			}
			else if (player.NewPrimaryRole != PlayerNewPrimaryRole.Mercenary)
			{
			}
			((Behaviour)Traverse.Create((object)playerController).Field<Light>("shotLight").Value).enabled = true;
			((MonoBehaviour)player).StartCoroutine(player.WaitAndDisableLight(0.1f));
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_ShowShot_Custom error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_ShowShot_Custom(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_ShowShot_CustomInvoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_ShowShot_Custom(runner, playerIndex);
	}

	private IEnumerator WaitAndDisableLight(float waitTime)
	{
		yield return (object)new WaitForSeconds(waitTime);
		((Behaviour)Traverse.Create((object)PlayerController).Field<Light>("shotLight").Value).enabled = false;
	}

	[Rpc]
	public unsafe static void Rpc_Ritualist_Ritual(NetworkRunner runner, int playerIndex, int effectIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Ritualist_Ritual(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = effectIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			if (player.PrimaryRolePower != PlayerPrimaryRolePower.Ritualist)
			{
				return;
			}
			Effect effect = EffectManager.GetEffect(effectIndex);
			float value = 0f;
			if (effect is MuteEffect)
			{
				value = 55f;
			}
			if (effect is DeafnessEffect)
			{
				value = 20f;
			}
			if (effect is ParanoiaEffect)
			{
				value = 12f;
			}
			if (effect is FlatulenceEffect)
			{
				value = 20f;
			}
			if (effect is NearsightedEffect)
			{
				value = 25f;
			}
			if (effect is StunnedEffect)
			{
				value = 10f;
			}
			foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.PlayerController.IsWolf)))
			{
				if (runner.IsServer && player.PrimaryRolePowerRemainingUses > 0)
				{
					ApplyEffectToPlayer(item.PlayerController, effect, runner, 1f, value);
					Rpc_Effect_On_Player(runner, item.Index, 2);
					GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)item.PlayerController).transform.position, 15f, 0.5f);
				}
				item.FlashPlayer(Color.blue);
			}
			player.PrimaryRolePowerRemainingUses--;
			player.ReduceMaterialAfterPowerUse();
			UIManager.ShowRedCenterMessage("NALES_UI_RITUAL_NOTIFICATION", 0.5f, 3f, new List<object> { TranslationManager.Instance.GetTranslation(effect.GetTranslateKey()) });
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Ritualist_Ritual error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Ritualist_Ritual(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Ritualist_Ritual_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int effectIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Ritualist_Ritual(runner, playerIndex, effectIndex);
	}

	[Preserve]
	public static void AngelChanged(Changed<PlayerCustom> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (behaviour.Ref == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref && NetworkBool.op_Implicit(behaviour.Angel))
			{
				ColorAdjustmentManager.FlashScreen(Color.green);
				AudioManager.Play("AngelHeal", (MixerTarget)2, 0.4f, 1f);
			}
			if (NetworkBool.op_Implicit(behaviour.Angel))
			{
				behaviour.AlreadyAngeledToday = true;
			}
			behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("AngelChanged error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Detective_Intel_One_Is_Evil(NetworkRunner runner, int playerIndex, int targetPlayerIndex1, int targetPlayerIndex2, int targetPlayerIndex3)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_One_Is_Evil(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex1;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex2;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex3;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 16;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerDetectiveIntelOneIsEvil playerDetectiveIntelOneIsEvil = new PlayerDetectiveIntelOneIsEvil
			{
				DayObtained = GameManagerCustom.Instance.CurrentDay
			};
			if (targetPlayerIndex1 != -1)
			{
				playerDetectiveIntelOneIsEvil.PlayerRefs.Add(PlayerCustomRegistry.GetPlayer(targetPlayerIndex1).Ref);
			}
			if (targetPlayerIndex2 != -1)
			{
				playerDetectiveIntelOneIsEvil.PlayerRefs.Add(PlayerCustomRegistry.GetPlayer(targetPlayerIndex2).Ref);
			}
			if (targetPlayerIndex3 != -1)
			{
				playerDetectiveIntelOneIsEvil.PlayerRefs.Add(PlayerCustomRegistry.GetPlayer(targetPlayerIndex3).Ref);
			}
			player.DetectiveIntelList.Add(playerDetectiveIntelOneIsEvil);
			if (PlayerController.Local.Ref == player.Ref)
			{
				UIManager.DetectivePanel.Show();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Detective_Intel_One_Is_Evil error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_One_Is_Evil(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Detective_Intel_One_Is_Evil_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex2 = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex3 = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Detective_Intel_One_Is_Evil(runner, playerIndex, targetPlayerIndex, targetPlayerIndex2, targetPlayerIndex3);
	}

	[Rpc]
	public unsafe static void Rpc_Detective_Intel_Different_Sides(NetworkRunner runner, int playerIndex, int targetPlayerIndex1, int targetPlayerIndex2)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_Different_Sides(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex1;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex2;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 12;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerDetectiveIntelDifferentSides playerDetectiveIntelDifferentSides = new PlayerDetectiveIntelDifferentSides
			{
				DayObtained = GameManagerCustom.Instance.CurrentDay
			};
			if (targetPlayerIndex1 != -1)
			{
				playerDetectiveIntelDifferentSides.PlayerRefs.Add(PlayerCustomRegistry.GetPlayer(targetPlayerIndex1).Ref);
			}
			if (targetPlayerIndex2 != -1)
			{
				playerDetectiveIntelDifferentSides.PlayerRefs.Add(PlayerCustomRegistry.GetPlayer(targetPlayerIndex2).Ref);
			}
			player.DetectiveIntelList.Add(playerDetectiveIntelDifferentSides);
			if (PlayerController.Local.Ref == player.Ref)
			{
				UIManager.DetectivePanel.Show();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Detective_Intel_Different_Sides error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_Different_Sides(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Detective_Intel_Different_Sides_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex2 = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Detective_Intel_Different_Sides(runner, playerIndex, targetPlayerIndex, targetPlayerIndex2);
	}

	[Rpc]
	public unsafe static void Rpc_Detective_Intel_Not_Wolf(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_Not_Wolf(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 12;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerDetectiveIntelNotWolf item = new PlayerDetectiveIntelNotWolf
			{
				DayObtained = GameManagerCustom.Instance.CurrentDay,
				Target = PlayerCustomRegistry.GetPlayer(targetPlayerIndex).Ref
			};
			player.DetectiveIntelList.Add(item);
			if (PlayerController.Local.Ref == player.Ref)
			{
				UIManager.DetectivePanel.Show();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Detective_Intel_Not_Wolf error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_Not_Wolf(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Detective_Intel_Not_Wolf_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Detective_Intel_Not_Wolf(runner, playerIndex, targetPlayerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Detective_Intel_Transformations_And_Detransformations(NetworkRunner runner, int playerIndex, int transformations, int detransformations)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_Transformations_And_Detransformations(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = transformations;
					num2 += 4;
					*(int*)(data + num2) = detransformations;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 12;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerDetectiveIntelTransformationsAndDetransformations item = new PlayerDetectiveIntelTransformationsAndDetransformations
			{
				DayObtained = GameManagerCustom.Instance.CurrentDay,
				Transformations = transformations,
				Detransformations = detransformations
			};
			player.DetectiveIntelList.Add(item);
			if (PlayerController.Local.Ref == player.Ref)
			{
				UIManager.DetectivePanel.Show();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Detective_Intel_Transformations_And_Detransformations error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_Transformations_And_Detransformations(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Detective_Intel_Transformations_And_Detransformations_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int transformations = *(int*)(data + num);
		num += 4;
		int detransformations = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Detective_Intel_Transformations_And_Detransformations(runner, playerIndex, transformations, detransformations);
	}

	[Rpc]
	public unsafe static void Rpc_Detective_Intel_Wolves_And_Solo_Roles_Remaining(NetworkRunner runner, int playerIndex, int wolves, int soloRoles)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_Wolves_And_Solo_Roles_Remaining(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = wolves;
					num2 += 4;
					*(int*)(data + num2) = soloRoles;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 12;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerDetectiveIntelWolvesAndSoloRolesRemaining item = new PlayerDetectiveIntelWolvesAndSoloRolesRemaining
			{
				DayObtained = GameManagerCustom.Instance.CurrentDay,
				Wolves = wolves,
				SoloRoles = soloRoles
			};
			player.DetectiveIntelList.Add(item);
			if (PlayerController.Local.Ref == player.Ref)
			{
				UIManager.DetectivePanel.Show();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Detective_Intel_Wolves_And_Solo_Roles_Remaining error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Detective_Intel_Wolves_And_Solo_Roles_Remaining(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Detective_Intel_Wolves_And_Solo_Roles_Remaining_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int wolves = *(int*)(data + num);
		num += 4;
		int soloRoles = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Detective_Intel_Wolves_And_Solo_Roles_Remaining(runner, playerIndex, wolves, soloRoles);
	}

	[Rpc]
	public unsafe static void Rpc_Drop_Item(NetworkRunner runner, int playerIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Drop_Item(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (!runner.IsServer)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerController playerController = player.PlayerController;
			if ((Object)(object)playerController.Item != (Object)null && !(playerController.Item is BulletItem))
			{
				Item item = playerController.Item;
				((NetworkBehaviour)item).CopyStateToBackingFields();
				Traverse.Create((object)item).Field("_Owner").SetValue((object)PlayerRef.None);
				((NetworkBehaviour)item).CopyBackingFieldsToState(true);
				((SimulationBehaviour)item).Object.AssignInputAuthority(PlayerRef.None);
				playerController.Item = null;
				((Component)item).transform.SetParent((Transform)null);
				((Component)item).transform.position = ((Component)playerController).transform.position + ((Component)playerController).transform.forward / 3f;
				((Component)item).transform.rotation = Quaternion.identity;
				Traverse.Create((object)item).Field<Collider>("itemCollider").Value.enabled = true;
				MeshRenderer[] value = Traverse.Create((object)item).Field<MeshRenderer[]>("meshRenderers").Value;
				for (int i = 0; i < value.Length; i++)
				{
					((Renderer)value[i]).enabled = true;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Drop_Item error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Drop_Item(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Drop_Item_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Drop_Item(runner, playerIndex);
	}

	[Preserve]
	public static void DownedChanged(Changed<PlayerCustom> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Invalid comparison between Unknown and I4
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (NetworkBool.op_Implicit(behaviour.Downed))
			{
				behaviour.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
				behaviour.PlayerAnimations.SetLoopAnimation("HumanM@Stun01", active: true);
				ParticleSystem[] componentsInChildren = behaviour.DownedParticleSystem.GetComponentsInChildren<ParticleSystem>();
				foreach (ParticleSystem val in componentsInChildren)
				{
					val.Clear();
					val.Play();
				}
			}
			else
			{
				ParticleSystem[] componentsInChildren2 = behaviour.DownedParticleSystem.GetComponentsInChildren<ParticleSystem>();
				foreach (ParticleSystem val2 in componentsInChildren2)
				{
					val2.Stop();
				}
				if (!NetworkBool.op_Implicit(behaviour.PlayerController.IsDead))
				{
					behaviour.UpdateCanMoveAnimation();
					if ((int)GameManager.LocalGameState == 4 && NetworkBool.op_Implicit(GameManager.Instance.CanVote) && behaviour.PlayerController.IdVoted == -2)
					{
						behaviour.PlayerAnimations.SetLoopAnimation("HumanM@Question01", active: true);
					}
					else
					{
						behaviour.PlayerAnimations.SetLoopAnimation("HumanM@Stun01", active: false);
					}
				}
			}
			if ((Object)(object)behaviour.PlayerController == (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer)
			{
				ColorAdjustmentManager.UpdateColorAdjustment();
			}
			Traverse.Create((object)behaviour.PlayerController).Method("UpdateCollider", Array.Empty<object>()).GetValue();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DownedChanged error: " + ex));
		}
	}

	[Preserve]
	public static void ParalyzedChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.UpdateCanMoveAnimation();
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				ColorAdjustmentManager.UpdateColorAdjustment();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ParalyzedChanged error: " + ex));
		}
	}

	[Preserve]
	public static void DyingChanged(Changed<PlayerCustom> changed)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.UpdateVisibility();
			if ((Object)(object)behaviour.PlayerController == (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer)
			{
				ColorAdjustmentManager.UpdateColorAdjustment();
			}
			if (!NetworkBool.op_Implicit(behaviour.Dying) && !(behaviour.PlayerController.Hunger >= (float)GameManager.Instance.MaxHunger * 0.25f))
			{
				if (((SimulationBehaviour)behaviour).Runner.IsServer)
				{
					NetworkString<_32> username = behaviour.PlayerController.PlayerData.Username;
					LycansUtility.AddLogOnlyForMe("Player not saved: " + ((object)username/*cast due to constrained. prefix*/).ToString());
					behaviour.Stats.UpdateDeathType("SURVIVALIST_NOT_SAVED");
				}
				behaviour.DiedFromNotBeingSaved = NetworkBool.op_Implicit(true);
				behaviour.PlayerController.Rpc_Kill(behaviour.PlayerController.Killer);
				GameManager.Instance.CheckForEndGame();
			}
			if (NetworkBool.op_Implicit(behaviour.Dying))
			{
				behaviour.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
				behaviour.PlayerController.UpdateAnimation(Animator.StringToHash("Dead"), true);
				behaviour.PlayerController.EnablePlayerHitBox(false);
				if (!NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.IsWolf) && (Object)(object)behaviour.PlayerController != (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer && Vector3.Distance(((Component)PlayerController.Local.LocalCameraHandler.PovPlayer).transform.position, ((Component)behaviour.PlayerController).transform.position) <= 40f)
				{
					((Component)behaviour.PlayerController).GetComponent<PlayerDyingComponent>().PlayDyingEffect();
				}
				if (behaviour.PrimaryRolePower == PlayerPrimaryRolePower.Shadow && NetworkBool.op_Implicit(behaviour.NewPrimaryRoleUniqueBool))
				{
					behaviour.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
				}
			}
			else if (!NetworkBool.op_Implicit(behaviour.PlayerController.IsDead))
			{
				behaviour.UpdateCanMoveAnimation();
				behaviour.PlayerController.UpdateAnimation(Animator.StringToHash("Dead"), false);
				behaviour.PlayerController.EnablePlayerHitBox(true);
				behaviour.PlayerController.Killer = PlayerRef.None;
			}
			Traverse.Create((object)behaviour.PlayerController).Method("UpdateCollider", Array.Empty<object>()).GetValue();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DyingChanged error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Save(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Save(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (!runner.IsServer)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = player.PlayerController;
			PlayerController playerController2 = player2.PlayerController;
			if (!NetworkBool.op_Implicit(player2.Dying) || NetworkBool.op_Implicit(playerController.IsWolf) || player.NewPrimaryRole == PlayerNewPrimaryRole.Zombie)
			{
				return;
			}
			player.SurvivalistSaveTargetPlayerRef = playerController2.Ref;
			player.SurvivalistSaveTimer = TickTimer.CreateFromSeconds(runner, 4f);
			playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
			playerController.IsAiming = NetworkBool.op_Implicit(false);
			player.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: true);
			Effect val = playerController2.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is DyingEffect);
			if ((Object)(object)val != (Object)null)
			{
				CustomTickTimer effectTimer = val.EffectTimer;
				if (((CustomTickTimer)(ref effectTimer)).NormalizedValue(runner) >= 0.8f)
				{
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectDying", runner, 1f, 6f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Save error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Save(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Save_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Save(runner, playerIndex, targetPlayerIndex);
	}

	public void FinishSurvivalistSave()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		if (!((PlayerRef)(ref SurvivalistSaveTargetPlayerRef)).IsValid || NetworkBool.op_Implicit(PlayerController.IsDead))
		{
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(SurvivalistSaveTargetPlayerRef);
		PlayerController playerController = player.PlayerController;
		SurvivalistSaveTargetPlayerRef = PlayerRef.None;
		SurvivalistSaveTimer = TickTimer.None;
		UpdateCanMoveAnimation();
		PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
		NetworkString<_32> username = PlayerController.PlayerData.Username;
		string? text = ((object)username/*cast due to constrained. prefix*/).ToString();
		username = playerController.PlayerData.Username;
		LycansUtility.AddLogOnlyForMe(text + " survivalist save on " + ((object)username/*cast due to constrained. prefix*/).ToString());
		if (!NetworkBool.op_Implicit(playerController.IsDead) && NetworkBool.op_Implicit(player.Dying))
		{
			playerController.Hunger = (float)GameManager.Instance.MaxHunger * 0.395f;
			Effect val = playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is DyingEffect);
			if ((Object)(object)val != (Object)null)
			{
				playerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
			}
			playerController.MovementAction = 0;
			player.UpdateCanMoveAnimation();
		}
	}

	[Preserve]
	public static void PoliticianVictimAlltimeChanged(Changed<PlayerCustom> changed)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (PlayerController.Local.Ref == behaviour.Ref && NetworkBool.op_Implicit(behaviour.PoliticianVictimAlltime))
			{
				UIManager.ShowRedCenterMessage("NALES_UI_POLITICIAN_VICTIM", 0.6f, 5f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PoliticianVictimAlltimeChanged error: " + ex));
		}
	}

	public void UpdatePredatorComponent()
	{
		((Behaviour)((Component)PlayerController).GetComponent<PlayerPredatorComponent>()).enabled = IsCurrentlyPlayedOrObserved && PrimaryRolePower == PlayerPrimaryRolePower.Predator;
	}

	public void UpdateTargetArrowComponent()
	{
		((Component)PlayerController).GetComponent<PlayerTargetArrowComponent>().UpdateState(IsCurrentlyPlayedOrObserved);
	}

	[Rpc]
	public unsafe static void Rpc_Wolf_Attack(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Invalid comparison between Unknown and I4
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Invalid comparison between Unknown and I4
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0904: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Wolf_Attack(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (!runner.IsServer)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = player.PlayerController;
			PlayerController playerController2 = player2.PlayerController;
			bool flag = false;
			if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				if (NetworkBool.op_Implicit(playerController.CanMove) && player.CanPerformActions && !NetworkBool.op_Implicit(playerController2.IsDead) && NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(player2.Dying) && !NetworkBool.op_Implicit(playerController.PlayerEffectManager.Giant) && !NetworkBool.op_Implicit(player.Paralyzed) && !NetworkBool.op_Implicit(player2.Phasing) && !NetworkBool.op_Implicit(player2.Angel))
				{
					flag = true;
				}
			}
			else if (NetworkBool.op_Implicit(playerController.CanMove) && player.CanPerformActions && !NetworkBool.op_Implicit(playerController2.IsDead) && (int)playerController.Role == 1 && ((int)playerController2.Role != 1 || (player.NewPrimaryRole == PlayerNewPrimaryRole.Lover && NetworkBool.op_Implicit(playerController2.IsWolf))) && !NetworkBool.op_Implicit(player2.Dying) && !NetworkBool.op_Implicit(playerController.PlayerEffectManager.Giant) && !NetworkBool.op_Implicit(player.Paralyzed) && !NetworkBool.op_Implicit(player2.Phasing) && !NetworkBool.op_Implicit(player2.Angel))
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			if (NetworkBool.op_Implicit(player2.ProtectedPriest))
			{
				bool flag2 = IsPrimaryRolePowerForEliteVillagers(player2.PrimaryRolePower);
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectDowned", runner, 1f, flag2 ? 1f : 2f);
				Vector3 val = ((Component)player2.PlayerController).transform.position - ((Component)playerController).transform.position;
				Vector3 val2 = -((Vector3)(ref val)).normalized;
				if (player.NewPrimaryRole == PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					val2 *= 0.6f;
				}
				((Component)playerController).GetComponent<KnockbackComponent>().Init(new Vector3(val2.x, 0f, val2.z), flag2 ? 4f : 6f, flag2 ? 2f : 3f);
				((Component)playerController).GetComponent<ForcedRotationComponent>().Init(new Vector3(0f, 1f, 0f), 4000f, 3000f);
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectResilience", runner, 1f, 6f);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerController).transform.position, BalancingValues.WolfKillSoundRangeByMap(GameManager.Instance.MapID), 1f);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SurvivalistDying"), ((Component)playerController).transform.position, BalancingValues.WolfKillSoundRangeByMap(GameManager.Instance.MapID), 1f);
				player2.ProtectedPriest = NetworkBool.op_Implicit(false);
				if (NetworkBool.op_Implicit(player2.Asleep))
				{
					Effect val3 = playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is AsleepEffect);
					if ((Object)(object)val3 != (Object)null)
					{
						player2.PlayerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val3).Object.Id);
					}
				}
				ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectFleeing", runner, 1f, flag2 ? 4f : 6f);
				if (player2.PrimaryRolePower == PlayerPrimaryRolePower.Avatar)
				{
					player2.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
				}
				Rpc_Effect_On_Player(runner, targetPlayerIndex, 0);
				return;
			}
			if (player2.PrimaryRolePower == PlayerPrimaryRolePower.Mole && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				if (!playerController2.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is MoleClockEffect))
				{
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectMoleClock", runner, 1f, 3600f);
					playerController2.Killer = playerController.Ref;
					playerController.Feed(GameManager.Instance.MaxHunger);
				}
				return;
			}
			if (player2.NewPrimaryRole != PlayerNewPrimaryRole.Zombie)
			{
				if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					playerController.Feed(Mathf.RoundToInt(0.15f * (float)GameManager.Instance.MaxHunger));
					BeastManager.Instance.BeastKills++;
				}
				else
				{
					playerController.Feed(GameManager.Instance.MaxHunger);
				}
			}
			GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerController2).transform.position, BalancingValues.WolfKillSoundRangeByMap(GameManager.Instance.MapID), 1f);
			if (player2.SecondaryRole == PlayerSecondaryRole.BothInfected && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				if (player.PrimaryRolePower == PlayerPrimaryRolePower.Possessor)
				{
					PlayerRef primaryRoleTargetRef = player.PrimaryRoleTargetRef;
					if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(player.PrimaryRoleTargetRef).Possessed))
					{
						goto IL_06a5;
					}
				}
				if (!NetworkBool.op_Implicit(player.Resurrected))
				{
					ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectReverting", runner);
				}
			}
			goto IL_06a5;
			IL_06a5:
			if (NetworkBool.op_Implicit(player2.Stinking))
			{
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectPoisoned", runner);
			}
			if (player2.NewPrimaryRole != PlayerNewPrimaryRole.Zombie)
			{
				List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Avenger && !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
				foreach (PlayerCustom item in list)
				{
					if (Vector3.Distance(((Component)item.PlayerController).transform.position, ((Component)playerController).transform.position) <= 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
					{
						item.AddMaterials(2500);
					}
				}
				List<PlayerCustom> list2 = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Shadow && !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
				foreach (PlayerCustom item2 in list2)
				{
					if (Vector3.Distance(((Component)item2.PlayerController).transform.position, ((Component)playerController).transform.position) <= 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
					{
						item2.AddMaterials(10000);
					}
				}
			}
			if (player.NewPrimaryRole == PlayerNewPrimaryRole.Lover)
			{
				PlayerCustom playerCustom = player.FindLoverPartner();
				if ((Object)(object)playerCustom != (Object)null && !NetworkBool.op_Implicit(playerCustom.Dying))
				{
					playerCustom.PlayerController.Hunger = Mathf.Min((float)GameManager.Instance.MaxHunger, playerCustom.PlayerController.Hunger + (float)GameManager.Instance.MaxHunger * 0.4f);
				}
			}
			if (NetworkBool.op_Implicit(player2.SurvivalistBuff) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && player2.NewPrimaryRole != PlayerNewPrimaryRole.Zombie)
			{
				player2.SurvivalistBuff = NetworkBool.op_Implicit(false);
				playerController2.Killer = playerController.Ref;
				playerController2.PlayerEffectManager.ClearEffects();
				playerController2.IsClimbing = NetworkBool.op_Implicit(false);
				player2.Parasite = NetworkBool.op_Implicit(false);
				player2.Dying = NetworkBool.op_Implicit(true);
				playerController2.Hunger = (float)GameManager.Instance.MaxHunger * 0.095f;
				ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectDying", runner, 1f, IsPrimaryRolePowerForEliteVillagers(player2.PrimaryRolePower) ? 35f : 50f);
			}
			else
			{
				if (player2.PrimaryRolePower == PlayerPrimaryRolePower.Purifier)
				{
					ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectPurifierBurn", runner, 1f, 15f);
				}
				player2.Stats.UpdateDeathType(NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) ? "BY_BEAST" : "BY_WOLF");
				playerController2.Rpc_Kill(playerController.Ref);
			}
			Rpc_Effect_On_Player(runner, player.Index, 10);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Wolf_Attack error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Wolf_Attack(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Wolf_Attack_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Wolf_Attack(runner, playerIndex, targetPlayerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Spirit_Attack(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Invalid comparison between Unknown and I4
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Invalid comparison between Unknown and I4
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Invalid comparison between Unknown and I4
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Spirit_Attack(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = player.PlayerController;
			PlayerController playerController2 = player2.PlayerController;
			TickTimer attackCooldown;
			switch (player.PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Ghost:
				if (!runner.IsServer || !((Object)(object)player.SummonedSpirit != (Object)null))
				{
					break;
				}
				attackCooldown = player.SummonedSpirit.AttackCooldown;
				if (!((TickTimer)(ref attackCooldown)).IsRunning && NetworkBool.op_Implicit(playerController2.IsWolf) && !NetworkBool.op_Implicit(playerController2.IsDead) && !playerController2.IsStarving())
				{
					if (playerController2.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is SpiritResistanceEffect))
					{
						return;
					}
					GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("PredatorKill"), ((Component)playerController2).transform.position, BalancingValues.WolfKillSoundRangeByMap(GameManager.Instance.MapID), 1f);
					playerController2.Hunger = Mathf.Max((float)GameManager.Instance.MaxHunger * 0.25f, playerController2.Hunger - (float)GameManager.Instance.MaxHunger * 0.05f);
					Vector3 val = ((Component)player.SummonedSpirit).transform.position - ((Component)playerController2).transform.position;
					Vector3 val2 = -((Vector3)(ref val)).normalized;
					((Component)playerController2).GetComponent<KnockbackComponent>().Init(new Vector3(val2.x, 0f, val2.z), 15f, 21f);
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectWounded", runner, 1f, 6f);
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectResilience", runner, 1f, 5f);
					player.SummonedSpirit.AttackCooldown = TickTimer.CreateFromSeconds(runner, 5f);
					ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectParalyzed", runner, 1f, 3f);
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectSpiritResistance", runner, 1f, 20f);
				}
				break;
			case PlayerPrimaryRolePower.Specter:
			{
				if (!runner.IsServer || !((Object)(object)player.SummonedSpirit != (Object)null))
				{
					break;
				}
				attackCooldown = player.SummonedSpirit.AttackCooldown;
				if (((TickTimer)(ref attackCooldown)).IsRunning || NetworkBool.op_Implicit(playerController2.IsDead))
				{
					break;
				}
				if (!NetworkBool.op_Implicit(playerController2.IsWolf) && playerController2.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is SpiritResistanceEffect))
				{
					return;
				}
				if (NetworkBool.op_Implicit(playerController2.IsWolf) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectEmpowered", runner, 1f, 15f);
				}
				else
				{
					List<string> list = (((int)GameManager.LocalGameState != 4) ? new List<string> { "LycansNewRoles.EffectNearsighted", "LycansNewRoles.EffectDeafness", "LycansNewRoles.EffectMute", "LycansNewRoles.EffectConfused", "Paranoia" } : new List<string> { "LycansNewRoles.EffectMute", "LycansNewRoles.EffectConfused", "Flatulences", "Paranoia" });
					string text = CollectionsUtil.Grab<string>(list, 1).First();
					float value = (((int)GameManager.LocalGameState == 4) ? 8f : 25f);
					string text2 = text;
					string text3 = text2;
					if (!(text3 == "Paranoia"))
					{
						if (text3 == "Flatulences")
						{
							Effect effect = EffectManager.GetEffects().First((Effect o) => o is FlatulenceEffect);
							ApplyEffectToPlayer(playerController2, effect, runner, 1f, value);
						}
						else
						{
							ApplyEffectToPlayer(playerController2, text, runner, 1f, value);
						}
					}
					else
					{
						Effect effect2 = EffectManager.GetEffects().First((Effect o) => o is ParanoiaEffect);
						ApplyEffectToPlayer(playerController2, effect2, runner, 1f, value);
					}
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectSpiritResistance", runner, 1f, 50f);
				}
				Rpc_Effect_On_Player(runner, player2.Index, 1);
				Rpc_Effect_On_Player(runner, player2.Index, 3);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)playerController2).transform.position, 10f, 0.5f);
				float num3 = (((int)GameManager.LocalGameState == 4) ? 40f : 25f);
				player.SummonedSpirit.AttackCooldown = TickTimer.CreateFromSeconds(runner, num3);
				break;
			}
			}
			PlayerNewPrimaryRole newPrimaryRole = player.NewPrimaryRole;
			PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
			if (playerNewPrimaryRole == PlayerNewPrimaryRole.Cultist && runner.IsServer && (Object)(object)player.SummonedSpirit != (Object)null)
			{
				attackCooldown = player.SummonedSpirit.AttackCooldown;
				if (!((TickTimer)(ref attackCooldown)).IsRunning && !NetworkBool.op_Implicit(playerController2.IsDead) && !NetworkBool.op_Implicit(player2.CapturedByCultist) && LycansUtility.GameActuallyInPlay)
				{
					playerController2.PlayerEffectManager.ClearEffects();
					player2.BombDormant = NetworkBool.op_Implicit(false);
					player2.CurseDormant = NetworkBool.op_Implicit(false);
					ApplyEffectToPlayer(playerController2, "LycansNewRoles.EffectCaptured", runner, 1f, 3600f);
					player.SummonedSpirit.AttackCooldown = TickTimer.CreateFromSeconds(runner, 5f);
					CultistManager.Instance.CultistCaptures++;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Spirit_Attack error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Spirit_Attack(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Spirit_Attack_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Spirit_Attack(runner, playerIndex, targetPlayerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Spirit_Spell(NetworkRunner runner, int playerIndex)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Invalid comparison between Unknown and I4
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Expected O, but got Unknown
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Spirit_Spell(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom customPlayer = PlayerCustomRegistry.GetPlayer(playerIndex);
			if (!runner.IsServer || !((Object)(object)customPlayer.SummonedSpirit != (Object)null) || !NetworkBool.op_Implicit(customPlayer.SummonedSpirit.HasFocus))
			{
				return;
			}
			TickTimer spellCooldown = customPlayer.SummonedSpirit.SpellCooldown;
			if (((TickTimer)(ref spellCooldown)).IsRunning)
			{
				return;
			}
			switch (customPlayer.PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Ghost:
			{
				PlayerController val2 = (from o in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead) && Vector3.Distance(((Component)o).transform.position, ((Component)customPlayer.SummonedSpirit).transform.position) <= 20f))
					orderby Vector3.Distance(((Component)o).transform.position, ((Component)customPlayer.SummonedSpirit).transform.position)
					select o).FirstOrDefault();
				if ((Object)(object)val2 != (Object)null)
				{
					Vector3 forward = ((Component)val2).transform.forward;
					forward.y = 0f;
					Vector3 position = ((Component)val2).transform.position + forward * 15f;
					float num3 = (float)(((double)Random.value - 0.5) * 32.0);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3 + 8f);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3 + 16f);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3 + 24f);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3 + 32f);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3 - 8f);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3 - 16f);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3 - 24f);
					CreateSpiritSpellEffect(runner, position, ((Component)val2).transform.position, num3 - 32f);
					customPlayer.SummonedSpirit.SpellCooldown = TickTimer.CreateFromSeconds(runner, 20f);
				}
				break;
			}
			case PlayerPrimaryRolePower.Specter:
				if (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
				{
					NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectDeceiverIllusion");
					NetworkObject val = runner.Spawn(networkObject, (Vector3?)((Component)customPlayer.SummonedSpirit).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
					{
						//IL_0017: Unknown result type (might be due to invalid IL or missing references)
						((Component)no).transform.position = ((Component)customPlayer.SummonedSpirit).transform.position;
					}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val).GetComponent<DeceiverIllusionComponent>().SetCreatorRef(customPlayer.Ref);
					customPlayer.SummonedSpirit.SpellCooldown = TickTimer.CreateFromSeconds(runner, 120f);
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Spirit_Spell error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Spirit_Spell(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Spirit_Spell_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Spirit_Spell(runner, playerIndex);
	}

	private static void CreateSpiritSpellEffect(NetworkRunner runner, Vector3 position, Vector3 rotatePosition, float angle)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectSpiritSpell");
		NetworkObject val = runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			((Component)no).transform.position = position;
		}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		((Component)val).transform.position = position;
		((Component)val).transform.RotateAround(rotatePosition, Vector3.up, angle);
		((Component)val).GetComponent<SelfDestroyingObjectComponent>().Init(5.5f);
	}

	[Rpc]
	public unsafe static void Rpc_Exorcism(NetworkRunner runner, int exorcistPlayerIndex, int targetPlayerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Exorcism(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = exorcistPlayerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(exorcistPlayerIndex);
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController = player2.PlayerController;
			if (runner.IsServer)
			{
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectExorcismActive", runner, 1f, 15f);
				player2.Exorciser = player.Ref;
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectResilience", runner, 1f, 5f);
				ApplyEffectToPlayer(playerController, "LycansNewRoles.EffectBlind", runner, 1f, 1.5f);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("Exorcism"), ((Component)playerController).transform.position, 20f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID), 1f);
			}
			GameObject val = Object.Instantiate<GameObject>(ExorcistDetector.ActivationParticleSystemPrefab, ((Component)playerController).transform.position, Quaternion.identity);
			val.SetActive(true);
			SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
			MainModule main = val.GetComponent<ParticleSystem>().main;
			selfDestroyingObjectComponent.Init(((MainModule)(ref main)).duration);
			if (player.IsCurrentlyPlayedOrObserved)
			{
				AudioManager.PlayPosition("Exorcism", ((Component)playerController).transform.position, (MixerTarget)2, 500f, 1f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Exorcism error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Exorcism(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Exorcism0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int exorcistPlayerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Exorcism(runner, exorcistPlayerIndex, targetPlayerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Effect_On_Player(NetworkRunner runner, int playerIndex, int effectIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Effect_On_Player(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = effectIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerController playerController = player.PlayerController;
			MainModule main;
			switch ((EffectOnPlayer)effectIndex)
			{
			case EffectOnPlayer.ShieldEffect:
			{
				player.FlashPlayer(Color.blue);
				ParticleSystem[] componentsInChildren = player.PriestShieldParticleSystem.GetComponentsInChildren<ParticleSystem>();
				foreach (ParticleSystem val9 in componentsInChildren)
				{
					val9.Clear();
					val9.Play();
				}
				if (player.IsCurrentlyPlayedOrObserved)
				{
					ColorAdjustmentManager.FlashScreen(Color.blue);
				}
				break;
			}
			case EffectOnPlayer.FlashScreenBlue:
				if (player.IsCurrentlyPlayedOrObserved)
				{
					ColorAdjustmentManager.FlashScreen(Color.blue);
				}
				break;
			case EffectOnPlayer.FlashScreenRed:
				if (player.IsCurrentlyPlayedOrObserved)
				{
					ColorAdjustmentManager.FlashScreen(Color.red);
				}
				break;
			case EffectOnPlayer.FlashPlayerBlue:
				player.FlashPlayer(Color.blue);
				break;
			case EffectOnPlayer.Megafart:
			{
				if (runner.IsServer)
				{
					if (NetworkBool.op_Implicit(playerController.IsWolf))
					{
						((Component)playerController).GetComponent<KnockbackComponent>().Init(((Component)playerController).transform.forward, 15f, 8f);
					}
					else
					{
						((Component)playerController).GetComponent<KnockbackComponent>().Init(((Component)playerController).transform.forward, 25f, 10f);
					}
				}
				AudioManager.PlayPosition("MegaFart", ((Component)playerController).transform.position, (MixerTarget)2, 200f, 1f);
				GameObject val6 = Object.Instantiate<GameObject>(MegaFartParticleSystemPrefab, ((Component)playerController).transform.position, Quaternion.identity);
				val6.SetActive(true);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent4 = val6.AddComponent<SelfDestroyingObjectComponent>();
				main = val6.GetComponent<ParticleSystem>().main;
				selfDestroyingObjectComponent4.Init(((MainModule)(ref main)).duration);
				break;
			}
			case EffectOnPlayer.HearHauntedSound:
				if (player.IsCurrentlyPlayedOrObserved)
				{
					float value = Random.value;
					if (value > 0.66f)
					{
						Vector3 position = ((Component)playerController).transform.position;
						position.x += 30f * Random.Range(-1f, 1f);
						position.z += 30f * Random.Range(-1f, 1f);
						AudioManager.PlayPosition("WOLF_TRANSFORM", position, (MixerTarget)2, 30f, 0.25f);
					}
					else if (value > 0.33f)
					{
						Vector3 position2 = ((Component)playerController).transform.position;
						position2.x += 100f * Random.Range(-1f, 1f);
						position2.z += 100f * Random.Range(-1f, 1f);
						AudioManager.PlayPosition("SHOT_LONG", position2, (MixerTarget)2, 500f, 0.25f);
					}
					else
					{
						Vector3 position3 = ((Component)playerController).transform.position;
						position3.x += 30f * Random.Range(-1f, 1f);
						position3.z += 30f * Random.Range(-1f, 1f);
						AudioManager.PlayPosition("PUNCH", position3, (MixerTarget)2, 30f, 0.4f);
					}
				}
				break;
			case EffectOnPlayer.ChaosEffect:
				player.ChaosParticleSystem.GetComponent<ParticleSystem>().Clear();
				player.ChaosParticleSystem.GetComponent<ParticleSystem>().Play();
				break;
			case EffectOnPlayer.Shift:
				if (runner.IsServer)
				{
					if ((Object)(object)player.AstralSpirit != (Object)null)
					{
						player.AstralSpirit.Shift();
					}
					if ((Object)(object)player.SummonedSpirit != (Object)null)
					{
						player.SummonedSpirit.Shift();
					}
				}
				break;
			case EffectOnPlayer.ParasiteExplosion:
			{
				GameObject val8 = Object.Instantiate<GameObject>(HostParasite.ParasiteExplosionParticleSystemPrefab, ((Component)player.PlayerController).transform.position, Quaternion.identity);
				val8.SetActive(true);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent6 = val8.AddComponent<SelfDestroyingObjectComponent>();
				selfDestroyingObjectComponent6.Init(1.5f);
				break;
			}
			case EffectOnPlayer.KilledByCrystalBallGuess:
			{
				AudioManager.PlayPosition("SeerCorrectGuessSound", ((Component)playerController).transform.position, (MixerTarget)2, 200f, 1f);
				GameObject val7 = Object.Instantiate<GameObject>(KilledByCrystalBallGuessParticleSystemPrefab, ((Component)playerController).transform.position, Quaternion.identity);
				val7.SetActive(true);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent5 = val7.AddComponent<SelfDestroyingObjectComponent>();
				main = val7.GetComponent<ParticleSystem>().main;
				selfDestroyingObjectComponent5.Init(((MainModule)(ref main)).duration + 2f);
				break;
			}
			case EffectOnPlayer.WolfAttack:
				playerController.UpdateAnimation(Animator.StringToHash("Attacking"), true);
				((MonoBehaviour)playerController).StartCoroutine("WaitAndResetAttackAnimation");
				if (NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Audition))
				{
					AudioManager.PlayPosition("PUNCH", ((Component)playerController).transform.position, (MixerTarget)2, 1000f, 1f);
					Plugin.Minimap.AddDeathPositionIcon(((Component)playerController).transform.position);
				}
				if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Vengeance)
				{
					GameManagerCustom.Instance.EventsManager.CurrentEventUniqueBool = true;
				}
				break;
			case EffectOnPlayer.BombExplosion:
			{
				AudioManager.PlayPosition("BombExplosion", ((Component)playerController).transform.position, (MixerTarget)2, 60f, 1f);
				Vector3 val4 = default(Vector3);
				((Vector3)(ref val4))._002Ector(((Component)playerController).transform.position.x, ((Component)playerController).transform.position.y + 0.5f, ((Component)playerController).transform.position.z);
				GameObject val5 = Object.Instantiate<GameObject>(BombExplosionParticleSystemPrefab, val4, Quaternion.identity);
				val5.SetActive(true);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent3 = val5.AddComponent<SelfDestroyingObjectComponent>();
				main = val5.GetComponent<ParticleSystem>().main;
				selfDestroyingObjectComponent3.Init(((MainModule)(ref main)).duration + 2f);
				break;
			}
			case EffectOnPlayer.SoundSuccess:
				if (player.IsCurrentlyPlayedOrObserved)
				{
					PlaySuccessSound();
				}
				break;
			case EffectOnPlayer.DiscipleAnchorActivation:
			{
				GameObject val3 = Object.Instantiate<GameObject>(DiscipleAnchor.ActivationParticleSystemPrefab, ((Component)player.PlayerController).transform.position, Quaternion.identity);
				val3.SetActive(true);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent2 = val3.AddComponent<SelfDestroyingObjectComponent>();
				selfDestroyingObjectComponent2.Init(2f);
				break;
			}
			case EffectOnPlayer.HauntedLanternsFlicker:
			{
				List<LanternCustom> list = Object.FindObjectsOfType<LanternCustom>().ToList();
				{
					foreach (LanternCustom item in list)
					{
						if (Vector3.Distance(((Component)player.PlayerController).transform.position, ((Component)item).transform.position) <= 30f)
						{
							item.StartFlicker(20);
						}
					}
					break;
				}
			}
			case EffectOnPlayer.KidnapperKidnap:
			{
				Vector3 val = default(Vector3);
				((Vector3)(ref val))._002Ector(((Component)playerController).transform.position.x, ((Component)playerController).transform.position.y + 0.75f, ((Component)playerController).transform.position.z);
				GameObject val2 = Object.Instantiate<GameObject>(KidnapperKidnapParticleSystemPrefab, val, Quaternion.identity);
				val2.SetActive(true);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent = val2.AddComponent<SelfDestroyingObjectComponent>();
				main = val2.GetComponent<ParticleSystem>().main;
				selfDestroyingObjectComponent.Init(((MainModule)(ref main)).duration);
				break;
			}
			case EffectOnPlayer.ForceTransform:
				TransformClass.TransformPrefix(playerController);
				TransformClass.TransformPostfix(playerController);
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Effect_On_Player error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Effect_On_Player(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Effect_On_Player_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int effectIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Effect_On_Player(runner, playerIndex, effectIndex);
	}

	[Preserve]
	public static void UpdatePlayerVisibility(Changed<PlayerCustom> changed)
	{
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdatePlayerVisibility error: " + ex));
		}
	}

	[Preserve]
	public static void UpdateOthersVisibility(Changed<PlayerCustom> changed)
	{
		try
		{
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.UpdateVisibility();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateOthersVisibility error: " + ex));
		}
	}

	[Preserve]
	public static void PhasingChanged(Changed<PlayerCustom> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (NetworkBool.op_Implicit(behaviour.Phasing))
			{
				((Component)behaviour.PlayerController).GetComponent<PlayerPhasingComponent>().Activate();
			}
			behaviour.UpdateMoveSpeed();
			behaviour.UpdateAudition();
			behaviour.PlayerController.EnablePlayerHitBox(!NetworkBool.op_Implicit(behaviour.Phasing));
			Traverse.Create((object)behaviour.PlayerController).Method("UpdateCollider", Array.Empty<object>()).GetValue();
			if (behaviour.Ref == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref)
			{
				foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer.UpdateVisibility();
				}
			}
			behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PhasingChanged error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Warlock_Transform(NetworkRunner runner, int playerIndex)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Warlock_Transform(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (!NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && !NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
				PlayerController playerController = player.PlayerController;
				if (((SimulationBehaviour)player).Object.HasStateAuthority)
				{
					Traverse.Create((object)playerController).Property("WolfDelay", (object[])null).SetValue((object)TickTimer.CreateFromSeconds(runner, (float)GameManager.Instance.TransformationTime));
					player.CurseTimer = TickTimer.CreateFromSeconds(runner, Random.Range(30f, 60f));
				}
				playerController.IsWolf = NetworkBool.op_Implicit(true);
				playerController.MovementAction = 0;
				playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
				playerController.IsZooming = NetworkBool.op_Implicit(false);
				AudioManager.PlayAndFollow("WOLF_TRANSFORM", ((Component)playerController).transform, (MixerTarget)2, 30f, 1f);
				Traverse.Create((object)playerController).Field<ParticleSystem>("smokeParticleSystem").Value.Play();
				string text = DateTime.UtcNow.ToString();
				NetworkString<_32> username = playerController.PlayerData.Username;
				LycansUtility.AddLogOnlyForMe("Adding transformation from warlock curse at date: " + text + ", player: " + ((object)username/*cast due to constrained. prefix*/).ToString());
				GameManagerCustom.Instance.AddTransformation();
				if (((SimulationBehaviour)playerController).HasInputAuthority)
				{
					UIManager.ShowRedCenterMessage("NALES_UI_WARLOCK_CURSE", 0.4f, 4f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Warlock_Transform error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Warlock_Transform(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Warlock_Transform_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Warlock_Transform(runner, playerIndex);
	}

	public void FlashPlayer(Color color)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		PlayerController playerController = PlayerController;
		if (!NetworkBool.op_Implicit(playerController.IsDead))
		{
			((Renderer)Traverse.Create((object)playerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).material.color = color;
			((Renderer)Traverse.Create((object)playerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = color;
			((MonoBehaviour)this).StartCoroutine(WaitAndRemoveFlashPlayer(0.1f));
		}
	}

	private IEnumerator WaitAndRemoveFlashPlayer(float waitTime)
	{
		yield return (object)new WaitForSeconds(waitTime);
		PlayerController playerController = PlayerController;
		((Component)playerController).GetComponent<PlayerResurrectedComponent>().UpdateState();
		UpdateWolfColor();
	}

	public void UpdateWolfColor()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = WolfColor;
	}

	[Preserve]
	public static void SoloRoleObjectiveCountChanged(Changed<PlayerCustom> changed)
	{
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				behaviour.UpdateDescriptionStatusIfNeeded();
			}
			if (((SimulationBehaviour)behaviour).Runner.IsServer && behaviour.SoloRoleObjectiveCount > 0)
			{
				GameManager.Instance.CheckForEndGame();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SoloRoleObjectiveCountChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SecondaryRolePowerActiveChanged(Changed<PlayerCustom> changed)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			PlayerController playerController = behaviour.PlayerController;
			switch (behaviour.SecondaryRole)
			{
			case PlayerSecondaryRole.BothAstral:
				if (NetworkBool.op_Implicit(behaviour.SecondaryRolePowerActive))
				{
					playerController.MovementAction = 0;
					playerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
					playerController.UpdateIsMoving(false);
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectAstralSpirit");
						Vector3 position = new Vector3(((Component)playerController).transform.position.x + ((Component)playerController).transform.forward.x, ((Component)playerController).transform.position.y, ((Component)playerController).transform.position.z + ((Component)playerController).transform.forward.z);
						NetworkObject val = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							//IL_0008: Unknown result type (might be due to invalid IL or missing references)
							((Component)no).transform.position = position;
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val).transform.position = position;
						((Component)val).GetComponent<PlayerAstralSpiritComponent>().Ref = playerController.Ref;
					}
				}
				else
				{
					playerController.MovementAction = 0;
					behaviour.UpdateCanMoveAnimation();
					if (((SimulationBehaviour)behaviour).Runner.IsServer && (Object)(object)behaviour.AstralSpirit != (Object)null)
					{
						((SimulationBehaviour)behaviour).Runner.Despawn(((Component)behaviour.AstralSpirit).GetComponent<NetworkObject>(), false);
					}
				}
				behaviour.UpdateVisibility();
				break;
			case PlayerSecondaryRole.BothActor:
				if ((Object)(object)behaviour.PlayerController == (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer)
				{
					ColorAdjustmentManager.UpdateColorAdjustment();
				}
				if (NetworkBool.op_Implicit(behaviour.SecondaryRolePowerActive))
				{
					playerController.MovementAction = 0;
					behaviour.UpdateCanMoveAnimation();
					behaviour.PlayerController.UpdateAnimation(Animator.StringToHash("Dead"), true);
					break;
				}
				playerController.MovementAction = 0;
				if (!NetworkBool.op_Implicit(playerController.IsDead))
				{
					behaviour.UpdateCanMoveAnimation();
					behaviour.PlayerController.UpdateAnimation(Animator.StringToHash("Dead"), false);
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRolePowerActiveChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SecondaryRoleUniqueIntChanged(Changed<PlayerCustom> changed)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom behaviour = changed.Behaviour;
		switch (changed.Behaviour.SecondaryRole)
		{
		case PlayerSecondaryRole.BothBlueMage:
		{
			if (behaviour.IsCurrentlyPlayedOrObserved)
			{
				behaviour.UpdateDescriptionStatusIfNeeded();
			}
			if (!((SimulationBehaviour)behaviour).Runner.IsServer)
			{
				break;
			}
			TickTimer secondaryRolePowerCooldownTimer = behaviour.SecondaryRolePowerCooldownTimer;
			if (((TickTimer)(ref secondaryRolePowerCooldownTimer)).IsRunning)
			{
				secondaryRolePowerCooldownTimer = behaviour.SecondaryRolePowerCooldownTimer;
				if (!(((TickTimer)(ref secondaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)behaviour).Runner).Value < 10f))
				{
					break;
				}
			}
			behaviour.SecondaryRoleFirstRemainingUses = 0;
			behaviour.SecondaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)behaviour).Runner, 10f);
			break;
		}
		case PlayerSecondaryRole.BothMerchant:
			if (changed.Behaviour.IsCurrentlyPlayedOrObserved)
			{
				PlaySuccessSound();
				behaviour.UpdateDescriptionStatusIfNeeded();
			}
			break;
		}
	}

	[Preserve]
	public static void NewPrimaryRoleUniqueBoolChanged(Changed<PlayerCustom> changed)
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			switch (behaviour.PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Peasant:
				behaviour.UpdateVisibility();
				break;
			case PlayerPrimaryRolePower.Shadow:
			{
				PlayerCustom povPlayerCustom = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				if (!NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf))
				{
					break;
				}
				foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != povPlayerCustom.Ref))
				{
					item.UpdateVisibility();
				}
				break;
			}
			case PlayerPrimaryRolePower.Possessor:
				if (!behaviour.IsCurrentlyPlayedOrObserved)
				{
					break;
				}
				behaviour.UpdateDescriptionStatusIfNeeded();
				if (behaviour.PrimaryRolePowerCurrentMaterials >= behaviour.PowerMaterialsInfo.RequiredMaterials)
				{
					if (NetworkBool.op_Implicit(behaviour.NewPrimaryRoleUniqueBool))
					{
						PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerRegistry.GetPlayer(behaviour.PrimaryRoleTargetRef));
					}
					else
					{
						PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerController.Local);
					}
				}
				break;
			case PlayerPrimaryRolePower.Specter:
				behaviour.UpdateVisibility();
				break;
			case PlayerPrimaryRolePower.Necromancer:
			case PlayerPrimaryRolePower.Sneak:
			case PlayerPrimaryRolePower.Avatar:
				if (behaviour.IsCurrentlyPlayedOrObserved)
				{
					behaviour.UpdateDescriptionStatusIfNeeded();
				}
				break;
			}
			switch (behaviour.NewPrimaryRole)
			{
			case PlayerNewPrimaryRole.Spy:
				if (behaviour.IsCurrentlyPlayedOrObserved)
				{
					behaviour.UpdateDescriptionStatusIfNeeded();
				}
				break;
			case PlayerNewPrimaryRole.Mercenary:
				if (NetworkBool.op_Implicit(behaviour.NewPrimaryRoleUniqueBool))
				{
					if (behaviour.IsCurrentlyPlayedOrObserved)
					{
						UIManager.ShowRedCenterMessage("NALES_UI_MERCENARY_HUNTED", 0.4f, 5f);
					}
					else
					{
						UIManager.ShowRedCenterMessage("NALES_UI_MERCENARY_HUNT", 0.4f, 5f, new List<object> { LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "") });
					}
					AudioManager.Play("ScoutAlert", (MixerTarget)2, 1f, 1f);
				}
				break;
			case PlayerNewPrimaryRole.Kidnapper:
				if (!behaviour.IsCurrentlyPlayedOrObserved)
				{
					break;
				}
				{
					foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
					{
						allPlayer.CustomAudio.UpdateVolume();
					}
					break;
				}
			case PlayerNewPrimaryRole.Cultist:
				if (NetworkBool.op_Implicit(behaviour.NewPrimaryRoleUniqueBool))
				{
					if (behaviour.IsCurrentlyPlayedOrObserved)
					{
						UIManager.ShowRedCenterMessage("NALES_UI_CULTIST_HUNT_CULTIST", 0.4f, 5f);
					}
					else
					{
						UIManager.ShowRedCenterMessage("NALES_UI_CULTIST_HUNT_OTHERS", 0.4f, 5f);
					}
					AudioManager.Play("ScoutAlert", (MixerTarget)2, 1f, 1f);
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewPrimaryRoleUniqueBoolChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SabotageTimerChanged(Changed<PlayerCustom> changed)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (((SimulationBehaviour)behaviour).HasInputAuthority)
			{
				UIManager.UpdateTimer(behaviour.SabotageTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_SABOTAGE");
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SabotageTimerChanged error: " + ex));
		}
	}

	[Preserve]
	public static void TrapDisarmTimerChanged(Changed<PlayerCustom> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			TickTimer trapDisarmTimer = behaviour.TrapDisarmTimer;
			if (((TickTimer)(ref trapDisarmTimer)).IsRunning)
			{
				behaviour.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: true);
			}
			else
			{
				behaviour.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
			}
			if (((SimulationBehaviour)behaviour).HasInputAuthority)
			{
				UIManager.UpdateTimer(behaviour.TrapDisarmTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_DISARM");
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TrapDisarmTimerChanged error: " + ex));
		}
	}

	[Preserve]
	public static void PrimaryRoleActionTimerChanged(Changed<PlayerCustom> changed)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (!((SimulationBehaviour)behaviour).HasInputAuthority)
			{
				return;
			}
			switch (behaviour.PrimaryRolePower)
			{
			case PlayerPrimaryRolePower.Necromancer:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_RESURRECT");
				return;
			case PlayerPrimaryRolePower.Warlock:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_SHAPESHIFT");
				return;
			case PlayerPrimaryRolePower.Tracker:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_PLACE_TRACKER");
				ColorAdjustmentManager.UpdateColorAdjustment();
				behaviour.UpdateAudition();
				return;
			case PlayerPrimaryRolePower.Exorcist:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_PLACE_DETECTOR");
				return;
			case PlayerPrimaryRolePower.Survivalist:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_SURVIVALIST_SELF_BUFF");
				return;
			case PlayerPrimaryRolePower.Scout:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_PLACE_RADAR");
				return;
			case PlayerPrimaryRolePower.Magician:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_PLACE_MAGICIAN_BEACON");
				return;
			case PlayerPrimaryRolePower.Mystic:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_MYSTIC_POWER");
				return;
			case PlayerPrimaryRolePower.Runemaster:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_PLACE_RUNE");
				return;
			case PlayerPrimaryRolePower.Deceiver:
			case PlayerPrimaryRolePower.Ritualist:
			case PlayerPrimaryRolePower.Peasant:
				return;
			}
			switch (behaviour.NewPrimaryRole)
			{
			case PlayerNewPrimaryRole.Voodoo:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_REANIMATE");
				break;
			case PlayerNewPrimaryRole.Cultist:
				UIManager.UpdateTimer(behaviour.PrimaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_PLACE_SKULL");
				break;
			case PlayerNewPrimaryRole.VillageIdiot:
			case PlayerNewPrimaryRole.Spy:
			case PlayerNewPrimaryRole.Scientist:
				break;
			default:
				UIManager.HideTimer();
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRoleActionTimerChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SecondaryRoleActionTimerChanged(Changed<PlayerCustom> changed)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (!((SimulationBehaviour)behaviour).HasInputAuthority)
			{
				return;
			}
			switch (behaviour.SecondaryRole)
			{
			case PlayerSecondaryRole.BothTeleporter:
				UIManager.UpdateTimer(behaviour.SecondaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_TELEPORT");
				break;
			case PlayerSecondaryRole.BothMedium:
				UIManager.UpdateTimer(behaviour.SecondaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_CONSULT");
				break;
			case PlayerSecondaryRole.BothScavenger:
				if (NetworkBool.op_Implicit(behaviour.PlayerController.IsWolf))
				{
					UIManager.UpdateTimer(behaviour.SecondaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_SCAVENGER_EAT");
				}
				else
				{
					UIManager.UpdateTimer(behaviour.SecondaryRoleActionTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_SCAVENGER_SEARCH");
				}
				break;
			case PlayerSecondaryRole.BothMerchant:
				behaviour.UpdateDescriptionStatusIfNeeded();
				break;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleActionTimerChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SurvivalistSaveTimerChanged(Changed<PlayerCustom> changed)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom behaviour = changed.Behaviour;
			if (((SimulationBehaviour)behaviour).HasInputAuthority)
			{
				UIManager.UpdateTimer(behaviour.SurvivalistSaveTimer, ((SimulationBehaviour)behaviour).Runner, "NALES_UI_ACTION_SURVIVALIST_SAVE");
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SurvivalistSaveTimerChanged error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Activate_Item_Secondary(NetworkRunner runner, int playerIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Invalid comparison between Unknown and I4
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Item_Secondary(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (!runner.IsServer || (int)GameManager.LocalGameState != 2)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			if (!player.CanPerformActions)
			{
				return;
			}
			SleepingGasPlaced sleepingGasPlaced = SleepingGasPlaced.FindPlayerPlacedSleepingGas(player.Ref);
			if ((Object)(object)sleepingGasPlaced != (Object)null)
			{
				sleepingGasPlaced.Detonate();
				if (player.Stats != null)
				{
					player.Stats.AddAction(new PlayerStats.PlayerAction
					{
						ActionType = "UseGadget",
						ActionName = TranslationManager.Instance.GetTranslation("NALES_ITEM_SLEEPING_GAS")
					}, ((Component)player.PlayerController).transform.position);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Activate_Item_Secondary error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Activate_Item_Secondary(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Activate_Item_SecondaryInvoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Activate_Item_Secondary(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Attack_Magician_Illusion(NetworkRunner runner, int playerIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Attack_Magician_Illusion(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			if (runner.IsServer && NetworkBool.op_Implicit(player.PlayerController.IsWolf) && !NetworkBool.op_Implicit(player.Blind))
			{
				ApplyEffectToPlayer(player.PlayerController, "LycansNewRoles.EffectBlind", runner, 1f, 2f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Attack_Magician_Illusion error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Attack_Magician_Illusion(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Attack_Magician_Illusion_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Attack_Magician_Illusion(runner, playerIndex);
	}

	public void GiveNewPrimaryRole(PlayerNewPrimaryRole role)
	{
		NewPrimaryRole = role;
		switch (role)
		{
		case PlayerNewPrimaryRole.VillageIdiot:
		case PlayerNewPrimaryRole.Scientist:
		case PlayerNewPrimaryRole.Beast:
		case PlayerNewPrimaryRole.Voodoo:
		case PlayerNewPrimaryRole.Kidnapper:
		case PlayerNewPrimaryRole.Cultist:
			PrimaryRolePowerRemainingUses = 1;
			break;
		case PlayerNewPrimaryRole.Agent:
		case PlayerNewPrimaryRole.Spy:
		case PlayerNewPrimaryRole.Lover:
		case PlayerNewPrimaryRole.Mercenary:
			break;
		}
	}

	public void GivePrimaryRolePower(PlayerPrimaryRolePower power)
	{
		PrimaryRolePower = power;
		switch (power)
		{
		case PlayerPrimaryRolePower.Hunter:
			PlayerController.Role = (PlayerRole)2;
			return;
		case PlayerPrimaryRolePower.Alchemist:
			PlayerController.Role = (PlayerRole)3;
			return;
		case PlayerPrimaryRolePower.Necromancer:
		case PlayerPrimaryRolePower.Deceiver:
		case PlayerPrimaryRolePower.Warlock:
		case PlayerPrimaryRolePower.Predator:
			PrimaryRolePowerRemainingUses = 1;
			return;
		}
		if (NewPrimaryRole != PlayerNewPrimaryRole.VillageIdiot && NewPrimaryRole != PlayerNewPrimaryRole.Scientist && NewPrimaryRole != PlayerNewPrimaryRole.Beast && NewPrimaryRole != PlayerNewPrimaryRole.Voodoo && NewPrimaryRole != PlayerNewPrimaryRole.Kidnapper && NewPrimaryRole != PlayerNewPrimaryRole.Cultist)
		{
			PrimaryRolePowerRemainingUses = 0;
		}
	}

	public void GiveSecondaryRole(PlayerSecondaryRole role)
	{
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		SecondaryRole = role;
		switch (role)
		{
		case PlayerSecondaryRole.BothAlcoholic:
		case PlayerSecondaryRole.BothSprinter:
		case PlayerSecondaryRole.BothInfected:
		case PlayerSecondaryRole.BothTeleporter:
		case PlayerSecondaryRole.BothEngineer:
		case PlayerSecondaryRole.BothPolitician:
		case PlayerSecondaryRole.BothMetabolic:
		case PlayerSecondaryRole.BothIllusionist:
		case PlayerSecondaryRole.BothGambler:
		case PlayerSecondaryRole.BothMedium:
		case PlayerSecondaryRole.BothAstral:
		case PlayerSecondaryRole.BothScavenger:
		case PlayerSecondaryRole.BothBlueMage:
		case PlayerSecondaryRole.BothActor:
		case PlayerSecondaryRole.BothScribe:
		case PlayerSecondaryRole.BothForger:
		case PlayerSecondaryRole.BothTinkerer:
			SecondaryRoleFirstRemainingUses = 1;
			break;
		default:
			SecondaryRoleFirstRemainingUses = 0;
			break;
		}
		if (role == PlayerSecondaryRole.BothBlueMage)
		{
			Effect val = CollectionsUtil.Grab<Effect>((from o in EffectManager.GetEffects()
				where o is AuditionEffect || o is NightVision || o is StinkingEffect || o is HauntedEffect || o is FlatulenceEffect || o is DeafnessEffect
				select o).ToList(), 1).First();
			SecondaryRoleUniqueInt = EffectManager.GetEffectIndex(val);
		}
		if (role == PlayerSecondaryRole.BothTinkerer && (Object)(object)Accessory == (Object)null)
		{
			List<Accessory> list = GameManagerCustom.SpawnableAccessories.Where((Accessory o) => Plugin.CustomConfig.AccessoriesAvailability[ItemUtility.ItemToTranslateKey((Item)(object)o)]).ToList();
			if (list.Count > 0)
			{
				Item prefab = ((IEnumerable<Item>)CollectionsUtil.Grab<Accessory>(list.ToList(), 1)).First();
				Item val2 = ItemUtility.SpawnItem(prefab, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
				val2.Rpc_ClaimItem(Ref);
			}
		}
		if (role == PlayerSecondaryRole.BothCarabineer)
		{
			PlayerController.IsGunLoaded = NetworkBool.op_Implicit(true);
		}
		if (role == PlayerSecondaryRole.BothImitator)
		{
			IsImitator = NetworkBool.op_Implicit(true);
		}
	}

	public void TriggerPrimaryRolePowerCooldown(NetworkRunner runner)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(runner, (float)PrimaryRolePowerCooldown.Value);
	}

	private void TriggerSecondaryRolePowerCooldown(NetworkRunner runner, bool secondCooldown = false)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		SecondaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(runner, (float)(secondCooldown ? SecondaryRolePowerSecondCooldown.Value : SecondaryRolePowerCooldown.Value));
	}

	public void UpdatePrimaryRole()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Invalid comparison between Unknown and I4
		PlayerController playerController = PlayerController;
		bool flag = false;
		if (NetworkBool.op_Implicit(GameManager.Instance.ShowAlly))
		{
			int num = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => (int)p.Role == 1)).Count();
			if ((int)playerController.Role == 1 && num > 1)
			{
				flag = true;
			}
			if (NewPrimaryRole == PlayerNewPrimaryRole.Traitor)
			{
				flag = true;
			}
		}
		if (flag)
		{
			GameManager.Instance.gameUI.UpdateAlly("");
		}
		else
		{
			GameManager.Instance.gameUI.UpdateRole(playerController.Role);
		}
		ItemCustom.UpdateAllItems();
	}

	public void InitForGameStart()
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Invalid comparison between Unknown and I4
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Expected O, but got Unknown
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		if (PowerMaterialsInfo != null)
		{
			PrimaryRolePowerRemainingUses = 0;
			PrimaryRolePowerCurrentMaterials = Mathf.FloorToInt((float)PowerMaterialsInfo.RequiredMaterials * PowerMaterialsInfo.StartingMaterialsPercentage);
		}
		if ((Object)(object)PlayerController.Item != (Object)null)
		{
			PlayerController.Item.Cancel();
			PlayerController.Item.DestroyItem();
		}
		switch (SecondaryRole)
		{
		case PlayerSecondaryRole.BothSherif:
			if ((int)PlayerController.Role != 1)
			{
				SecondaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 240f);
			}
			else
			{
				SecondaryRoleFirstRemainingUses = 1;
			}
			break;
		case PlayerSecondaryRole.BothScavenger:
		{
			Item[] value = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
			List<Item> list2 = value.Where((Item o) => (o is LockItem || o is PhasingDiamondItem || o is GrenadeItem) && Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)]).ToList();
			if (list2.Count == 0)
			{
				return;
			}
			Item prefab2 = CollectionsUtil.Grab<Item>(list2.ToList(), 1).First();
			Item val3 = ItemUtility.SpawnItem(prefab2, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
			val3.Rpc_ClaimItem(Ref);
			break;
		}
		case PlayerSecondaryRole.BothBlueMage:
		{
			Accessory accessory = GameManagerCustom.SpawnableAccessories.FirstOrDefault((Accessory o) => o is AccessorySpellbook);
			if ((Object)(object)accessory != (Object)null)
			{
				Item val2 = ItemUtility.SpawnItem((Item)(object)accessory, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
				val2.Rpc_ClaimItem(Ref);
			}
			break;
		}
		case PlayerSecondaryRole.BothScribe:
		{
			Item val4 = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value.FirstOrDefault((Item o) => o is MagicScrollItem);
			if ((Object)(object)val4 == (Object)null)
			{
				return;
			}
			Item val5 = ItemUtility.SpawnItem(val4, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
			val5.Rpc_ClaimItem(PlayerController.Ref);
			break;
		}
		case PlayerSecondaryRole.BothMerchant:
			MerchantCoin.CreateCoinsOnNewDay(((SimulationBehaviour)this).Runner, this);
			SecondaryRoleUniqueInt = 10;
			GenerateMerchantOffers();
			break;
		case PlayerSecondaryRole.BothTinkerer:
		{
			List<Accessory> list = GameManagerCustom.SpawnableAccessories.Where((Accessory o) => Plugin.CustomConfig.AccessoriesAvailability[ItemUtility.ItemToTranslateKey((Item)(object)o)]).ToList();
			if (list.Count > 0)
			{
				Item prefab = ((IEnumerable<Item>)CollectionsUtil.Grab<Accessory>(list.ToList(), 1)).First();
				Item val = ItemUtility.SpawnItem(prefab, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
				val.Rpc_ClaimItem(Ref);
			}
			break;
		}
		}
		switch (NewPrimaryRole)
		{
		case PlayerNewPrimaryRole.VillageIdiot:
			if (PlayerCustomRegistry.Any((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Warlock))
			{
				SoloRoleObjectiveTarget = 0;
			}
			else if (PlayerCustomRegistry.Any((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Bomber))
			{
				SoloRoleObjectiveTarget = 1;
			}
			else if (PlayerCustomRegistry.Any((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Saboteur))
			{
				SoloRoleObjectiveTarget = 2;
			}
			else
			{
				SoloRoleObjectiveTarget = Random.RandomRangeInt(0, 3);
			}
			break;
		case PlayerNewPrimaryRole.Mercenary:
		{
			SoloRoleObjectiveTarget = BalancingValues.MercenaryTotalObjective(GameManager.Instance.LootSpawnRate, (float)Plugin.CustomConfig.MercenaryPercentage * 0.01f, GameManager.Instance.DayDuration + GameManager.Instance.NightDuration, GameManagerCustom.Instance.SoloRoleDifficulty);
			int num = 2 * GameManager.Instance.DayDuration;
			PlayerController.IsGunLoaded = NetworkBool.op_Implicit(false);
			PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, (float)num);
			break;
		}
		case PlayerNewPrimaryRole.Kidnapper:
			PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 45f);
			PrimaryRolePowerRemainingUses = 0;
			break;
		}
		switch (PrimaryRolePower)
		{
		case PlayerPrimaryRolePower.Investigator:
			PrimaryRolePowerCurrentMaterials = 50;
			InvestigatorHint.CreateHintsOnNewDay(((SimulationBehaviour)this).Runner, this);
			InvestigatorGiveNewTarget();
			break;
		case PlayerPrimaryRolePower.Hermit:
			HermitHideout.CreateHideoutsOnNewDay(((SimulationBehaviour)this).Runner, this);
			break;
		case PlayerPrimaryRolePower.Sneak:
		{
			List<DiscipleAnchor> source = Object.FindObjectsOfType<DiscipleAnchor>().ToList();
			if (!source.Any((DiscipleAnchor o) => o.CreatorRef == Ref))
			{
				Teleporter val6 = CollectionsUtil.Grab<Teleporter>((from o in Object.FindObjectsOfType<Teleporter>()
					where o.MapID == GameManager.Instance.MapID
					select o).ToList(), 1).First();
				Vector3 position = ((Component)val6).transform.position;
				NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectDiscipleAnchor");
				NetworkObject val7 = ((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					//IL_0008: Unknown result type (might be due to invalid IL or missing references)
					((Component)no).transform.position = position;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				((Component)val7).transform.position = position;
				((Component)val7).GetComponent<DiscipleAnchor>().SetCreatorRef(Ref);
			}
			break;
		}
		}
	}

	public void InitStats()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Stats = new PlayerStats
			{
				PlayerRef = Ref,
				ID = ((object)PlayerController.PlayerData.ID/*cast due to constrained. prefix*/).ToString(),
				Username = ((object)PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString(),
				MainRoleInitial = TranslationManager.Instance.GetTranslationForStats(UpdateRoleUtility.GetNewPrimaryRoleKey(PlayerController, this)).Replace("{0}", "").Replace("{1}", "")
					.Replace("{2}", ""),
				Power = ((PrimaryRolePower != PlayerPrimaryRolePower.None) ? TranslationManager.Instance.GetTranslationForStats(UpdateRoleUtility.GetPrimaryRolePowerKey(PrimaryRolePower)) : null),
				SecondaryRole = ((SecondaryRole == PlayerSecondaryRole.None) ? null : (NetworkBool.op_Implicit(IsImitator) ? TranslationManager.Instance.GetTranslationForStats("NALES_ROLE_IMITATOR") : TranslationManager.Instance.GetTranslationForStats(UpdateRoleUtility.GetSecondaryRoleKey(SecondaryRole))))
			};
			switch (ColorIndex)
			{
			case 0:
				Stats.Color = "Bleu foncé";
				break;
			case 1:
				Stats.Color = "Vert foncé";
				break;
			case 2:
				Stats.Color = "Jaune";
				break;
			case 3:
				Stats.Color = "Rouge";
				break;
			case 4:
				Stats.Color = "Turquoise";
				break;
			case 5:
				Stats.Color = "Rose";
				break;
			case 6:
				Stats.Color = "Orange";
				break;
			case 7:
				Stats.Color = "Gris";
				break;
			case 8:
				Stats.Color = "Violet";
				break;
			case 9:
				Stats.Color = "Marron";
				break;
			case 10:
				Stats.Color = "Vert pomme";
				break;
			case 11:
				Stats.Color = "Bleu royal";
				break;
			default:
				Stats.Color = ColorIndex.ToString();
				break;
			}
			SessionStats.Stats.CurrentGame.AddPlayerStats(Stats);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("New player stats error: " + ex));
		}
	}

	public void AddSoloRoleProgress(int amount, int? goal)
	{
		SoloRoleObjectiveCount += amount;
		if (goal.HasValue)
		{
			SoloRoleHalfDayProgress += (float)amount / (float)goal.Value;
		}
	}

	public void UpdateVisibility()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Invalid comparison between Unknown and I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
		if ((Object)(object)povPlayer == (Object)null)
		{
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(povPlayer.Ref);
		if ((Object)(object)player == (Object)null)
		{
			return;
		}
		PlayerController playerController = PlayerController;
		if ((Object)(object)SummonedSpirit != (Object)null)
		{
			bool visible = true;
			if (SummonedSpirit.Ref == PlayerController.Local.Ref)
			{
				visible = false;
			}
			else
			{
				switch (PrimaryRolePower)
				{
				case PlayerPrimaryRolePower.Ghost:
					visible = NetworkBool.op_Implicit(povPlayer.IsDead) || (NetworkBool.op_Implicit(povPlayer.IsWolf) && (int)povPlayer.Role == 1);
					break;
				case PlayerPrimaryRolePower.Specter:
					visible = NetworkBool.op_Implicit(povPlayer.IsDead);
					break;
				}
				PlayerNewPrimaryRole newPrimaryRole = NewPrimaryRole;
				PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
				if (playerNewPrimaryRole == PlayerNewPrimaryRole.Cultist)
				{
					visible = true;
				}
			}
			SummonedSpirit.SetVisible(visible);
		}
		if ((Object)(object)playerController != (Object)(object)povPlayer)
		{
			if (NetworkBool.op_Implicit(Disappeared) || NetworkBool.op_Implicit(Phasing) || (NetworkBool.op_Implicit(Isolation) && !NetworkBool.op_Implicit(povPlayer.IsWolf) && player.NewPrimaryRole != PlayerNewPrimaryRole.Zombie))
			{
				UpdateVisible(visible: false);
			}
			else if (NetworkBool.op_Implicit(player.Isolation) && !NetworkBool.op_Implicit(PlayerController.IsWolf) && NewPrimaryRole != PlayerNewPrimaryRole.Zombie)
			{
				UpdateVisible(visible: false);
			}
			else if (NetworkBool.op_Implicit(player.Phasing) || (NetworkBool.op_Implicit(Local.Possessed) && !NetworkBool.op_Implicit(PlayerController.IsDead)))
			{
				UpdateVisible(visible: false);
			}
			else if (NetworkBool.op_Implicit(povPlayer.PlayerEffectManager.NightVision))
			{
				UpdateVisible(visible: true);
			}
			else if (NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(Sneaky))
			{
				UpdateVisible(visible: false);
			}
			else if (PrimaryRolePower == PlayerPrimaryRolePower.Peasant && NetworkBool.op_Implicit(NewPrimaryRoleUniqueBool) && NetworkBool.op_Implicit(povPlayer.IsWolf))
			{
				UpdateVisible(visible: false);
			}
			else if (NetworkBool.op_Implicit(Hidden) && NetworkBool.op_Implicit(povPlayer.IsWolf))
			{
				UpdateVisible(visible: false);
			}
			else
			{
				UpdateVisible(visible: true);
			}
		}
		else if ((Object)(object)AstralSpirit != (Object)null)
		{
			UpdateVisible(visible: true);
		}
		else
		{
			UpdateVisible(visible: false);
		}
	}

	private void UpdateVisible(bool visible)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Invalid comparison between Unknown and I4
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Invalid comparison between Unknown and I4
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_09be: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		Visible = visible;
		PlayerCustom povPlayerCustom = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		CamouflageLevelForPovPlayer = 0;
		if (NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf) && !NetworkBool.op_Implicit(povPlayerCustom.PlayerController.PlayerEffectManager.NightVision))
		{
			foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerPrimaryRolePower.Shadow && NetworkBool.op_Implicit(o.NewPrimaryRoleUniqueBool)))
			{
				if (Vector3.Distance(((Component)item.PlayerController).transform.position, ((Component)PlayerController).transform.position) <= 20f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
				{
					float num = Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)PlayerController).transform.position);
					if (num > 40f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
					{
						visible = false;
					}
					else if (num > 25f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
					{
						CamouflageLevelForPovPlayer = Math.Max(CamouflageLevelForPovPlayer, 3);
					}
					else if (num > 10f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
					{
						CamouflageLevelForPovPlayer = Math.Max(CamouflageLevelForPovPlayer, 2);
					}
					else
					{
						CamouflageLevelForPovPlayer = Math.Max(CamouflageLevelForPovPlayer, 1);
					}
				}
			}
		}
		PlayerController.ShowThirdPersonModels(visible);
		UpdateIconAbovePlayer(visible);
		ParticleSystem component = StinkingParticleSystem.GetComponent<ParticleSystem>();
		if (NetworkBool.op_Implicit(Stinking) && visible)
		{
			component.Play();
		}
		else
		{
			component.Stop();
		}
		ParticleSystem[] componentsInChildren = TruesightParticleSystem.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array = componentsInChildren;
		foreach (ParticleSystem val in array)
		{
			if (NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.NightVision) && !IsCurrentlyPlayedOrObserved && visible)
			{
				MainModule main = val.main;
				((MainModule)(ref main)).startColor = MinMaxGradient.op_Implicit(ColorManager.GetColor(ColorIndex));
				val.Play();
			}
			else
			{
				val.Stop();
			}
		}
		ParticleSystem[] componentsInChildren2 = MidasParticleSystemHandLeft.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array2 = componentsInChildren2;
		foreach (ParticleSystem val2 in array2)
		{
			if (NetworkBool.op_Implicit(Midas) && visible)
			{
				val2.Play();
			}
			else
			{
				val2.Stop();
			}
		}
		ParticleSystem[] componentsInChildren3 = MidasParticleSystemHandRight.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array3 = componentsInChildren3;
		foreach (ParticleSystem val3 in array3)
		{
			if (NetworkBool.op_Implicit(Midas) && visible)
			{
				val3.Play();
			}
			else
			{
				val3.Stop();
			}
		}
		ParticleSystem[] componentsInChildren4 = VampireParticleSystem.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array4 = componentsInChildren4;
		foreach (ParticleSystem val4 in array4)
		{
			if (PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is VampireEffect) && visible)
			{
				val4.Play();
			}
			else
			{
				val4.Stop();
			}
		}
		ParticleSystem component2 = SpeedParticleSystem.GetComponent<ParticleSystem>();
		if (NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.BonusSpeed) && visible)
		{
			component2.Play();
		}
		else
		{
			component2.Stop();
		}
		ParticleSystem[] componentsInChildren5 = HauntedParticleSystem.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array5 = componentsInChildren5;
		foreach (ParticleSystem val5 in array5)
		{
			if (PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is HauntedEffect) && visible)
			{
				val5.Play();
			}
			else
			{
				val5.Stop();
			}
		}
		ParticleSystem component3 = AsleepParticleSystem.GetComponent<ParticleSystem>();
		if (NetworkBool.op_Implicit(Asleep) && visible)
		{
			component3.Play();
		}
		else
		{
			component3.Stop();
		}
		ParticleSystem component4 = BanishedParticleSystem.GetComponent<ParticleSystem>();
		ParticleSystem[] componentsInChildren6 = ((Component)component4).GetComponentsInChildren<ParticleSystem>();
		if ((NetworkBool.op_Implicit(Banished) || NetworkBool.op_Implicit(CapturedByCultist)) && visible)
		{
			component4.Play();
			ParticleSystem[] array6 = componentsInChildren6;
			foreach (ParticleSystem val6 in array6)
			{
				val6.Play();
			}
		}
		else
		{
			component4.Stop();
			ParticleSystem[] array7 = componentsInChildren6;
			foreach (ParticleSystem val7 in array7)
			{
				val7.Stop();
			}
		}
		ParticleSystem component5 = MolotovBurnParticleSystem.GetComponent<ParticleSystem>();
		if (NetworkBool.op_Implicit(Burning) && visible)
		{
			component5.Play();
		}
		else
		{
			component5.Clear();
			component5.Stop();
		}
		ParticleSystem component6 = PurifierBurnParticleSystem.GetComponent<ParticleSystem>();
		if (NetworkBool.op_Implicit(PurifierBurn) && visible)
		{
			component6.Play();
		}
		else
		{
			component6.Clear();
			component6.Stop();
		}
		ParticleSystem[] componentsInChildren7 = ConfusedParticleSystem.GetComponentsInChildren<ParticleSystem>();
		if (NetworkBool.op_Implicit(Confused) && visible)
		{
			ParticleSystem[] array8 = componentsInChildren7;
			foreach (ParticleSystem val8 in array8)
			{
				val8.Play();
			}
		}
		else
		{
			ParticleSystem[] array9 = componentsInChildren7;
			foreach (ParticleSystem val9 in array9)
			{
				val9.Stop();
			}
		}
		bool flag = NetworkBool.op_Implicit(Tracked) || (NetworkBool.op_Implicit(povPlayerCustom.Spotter) && NetworkBool.op_Implicit(PlayerController.IsWolf)) || (NetworkBool.op_Implicit(PlayerController.IsWolf) && NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.Glowing)) || (NetworkBool.op_Implicit(PlayerController.IsWolf) && povPlayerCustom.PrimaryRolePower == PlayerPrimaryRolePower.Scout && ScoutRadar.AssociatedRadars.Any((ScoutRadar o) => o.CreatorRef == povPlayerCustom.Ref && o.WolvesInRange.Any((PlayerRef j) => j == Ref))) || (povPlayerCustom.PrimaryRolePower == PlayerPrimaryRolePower.Host && NetworkBool.op_Implicit(Parasite) && (int)GameManager.LocalGameState != 4 && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)PlayerController).transform.position) <= 20f);
		if (!flag && NetworkBool.op_Implicit(PlayerController.IsWolf) && NetworkBool.op_Implicit(PlayerController.IsWolf) && povPlayerCustom.PrimaryRolePower == PlayerPrimaryRolePower.Runemaster)
		{
			RunemasterRune runemasterRune = RunemasterRune.AssociatedRunes.FirstOrDefault((RunemasterRune o) => o.IsSelected);
			if ((Object)(object)runemasterRune != (Object)null && Vector3.Distance(((Component)runemasterRune).transform.position, ((Component)PlayerController).transform.position) <= 10f)
			{
				flag = true;
			}
		}
		if (!flag && NetworkBool.op_Implicit(PlayerController.IsWolf) && povPlayerCustom.Accessory is AccessoryCrystalBall && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)PlayerController).transform.position) <= 20f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID))
		{
			flag = true;
		}
		if (!flag && NetworkBool.op_Implicit(povPlayerCustom.Clairvoyance) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)PlayerController).transform.position) <= 40f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID))
		{
			flag = true;
		}
		List<MeshRenderer> list = new List<MeshRenderer>();
		int value = Traverse.Create((object)PlayerController).Property<int>("HatIndex", (object[])null).Value;
		if (value >= 0 && value < PlayerController.hats.transform.childCount)
		{
			GameObject gameObject = ((Component)PlayerController.hats.transform.GetChild(value)).gameObject;
			list.AddRange(gameObject.GetComponentsInChildren<MeshRenderer>().ToList());
		}
		if (flag && (int)GameManager.LocalGameState != 4)
		{
			((Component)PlayerController).GetComponent<PlayerHeartSeethroughComponent>().SetVisible(povPlayerCustom.Ref != Ref && !NetworkBool.op_Implicit(PlayerController.IsDead), NetworkBool.op_Implicit(PlayerController.IsWolf));
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).material.shader = SeeThroughShaderHuman;
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.shader = SeeThroughShaderWolf;
		}
		else
		{
			((Component)PlayerController).GetComponent<PlayerHeartSeethroughComponent>().SetVisible(visible: false, NetworkBool.op_Implicit(PlayerController.IsWolf));
			Shader shader = RegularVillagerShader;
			Shader shader2 = RegularWolfShader;
			if (!NetworkBool.op_Implicit(povPlayerCustom.PlayerController.PlayerEffectManager.NightVision))
			{
				if (PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is CamouflageEffect))
				{
					CamouflageLevelForPovPlayer = Mathf.Max(CamouflageLevelForPovPlayer, 2);
				}
				else if (NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf) && PrimaryRolePower == PlayerPrimaryRolePower.Spotter)
				{
					if (NetworkBool.op_Implicit(Spotter))
					{
						CamouflageLevelForPovPlayer = Mathf.Max(CamouflageLevelForPovPlayer, 2);
					}
					else
					{
						CamouflageLevelForPovPlayer = Mathf.Max(CamouflageLevelForPovPlayer, 1);
					}
				}
			}
			switch (CamouflageLevelForPovPlayer)
			{
			case 1:
				shader = CamouflageLevel1Shader;
				shader2 = CamouflageLevel2Shader;
				break;
			case 2:
				shader = CamouflageLevel2Shader;
				shader2 = CamouflageLevel3Shader;
				break;
			case 3:
				shader = CamouflageLevel3Shader;
				shader2 = CamouflageLevel3Shader;
				break;
			}
			((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).material.shader = shader;
			UpdateSkinColor();
			Material[] materials = ((Renderer)Traverse.Create((object)PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).materials;
			Material[] array10 = materials;
			foreach (Material val10 in array10)
			{
				val10.shader = shader2;
			}
			foreach (MeshRenderer item2 in list)
			{
				Material[] materials2 = ((Renderer)item2).materials;
				foreach (Material val11 in materials2)
				{
					val11.shader = shader;
				}
			}
		}
		if ((Object)(object)CurrentPet != (Object)null)
		{
			CurrentPet.UpdateVisibility();
		}
	}

	public void ClearAllParticleEffects()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Invalid comparison between Unknown and I4
		if ((int)GameManager.LocalGameState != 0 && (int)GameManager.LocalGameState != 1)
		{
			StinkingParticleSystem.GetComponent<ParticleSystem>().Clear();
			ParticleSystem[] componentsInChildren = TruesightParticleSystem.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array = componentsInChildren;
			foreach (ParticleSystem val in array)
			{
				val.Clear();
			}
			ParticleSystem[] componentsInChildren2 = MidasParticleSystemHandLeft.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array2 = componentsInChildren2;
			foreach (ParticleSystem val2 in array2)
			{
				val2.Clear();
			}
			ParticleSystem[] componentsInChildren3 = MidasParticleSystemHandRight.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array3 = componentsInChildren3;
			foreach (ParticleSystem val3 in array3)
			{
				val3.Clear();
			}
			ParticleSystem[] componentsInChildren4 = VampireParticleSystem.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array4 = componentsInChildren4;
			foreach (ParticleSystem val4 in array4)
			{
				val4.Clear();
			}
			ParticleSystem component = SpeedParticleSystem.GetComponent<ParticleSystem>();
			component.Clear();
			ParticleSystem[] componentsInChildren5 = HauntedParticleSystem.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array5 = componentsInChildren5;
			foreach (ParticleSystem val5 in array5)
			{
				val5.Clear();
			}
			ParticleSystem component2 = AsleepParticleSystem.GetComponent<ParticleSystem>();
			component2.Clear();
			ParticleSystem component3 = BanishedParticleSystem.GetComponent<ParticleSystem>();
			component3.Clear();
			ParticleSystem[] componentsInChildren6 = ((Component)component3).GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array6 = componentsInChildren6;
			foreach (ParticleSystem val6 in array6)
			{
				val6.Clear();
			}
			ParticleSystem component4 = MolotovBurnParticleSystem.GetComponent<ParticleSystem>();
			component4.Clear();
			ParticleSystem component5 = PurifierBurnParticleSystem.GetComponent<ParticleSystem>();
			component5.Clear();
			ChaosParticleSystem.GetComponent<ParticleSystem>().Clear();
			ParticleSystem[] componentsInChildren7 = ConfusedParticleSystem.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array7 = componentsInChildren7;
			foreach (ParticleSystem val7 in array7)
			{
				val7.Clear();
			}
			ParticleSystem[] componentsInChildren8 = DownedParticleSystem.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array8 = componentsInChildren8;
			foreach (ParticleSystem val8 in array8)
			{
				val8.Clear();
			}
			ParticleSystem component6 = ExorcisedParticleSystem.GetComponent<ParticleSystem>();
			component6.Clear();
			ParticleSystem[] componentsInChildren9 = PriestShieldParticleSystem.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array9 = componentsInChildren9;
			foreach (ParticleSystem val9 in array9)
			{
				val9.Clear();
			}
			ParticleSystem component7 = StinkingParticleSystem.GetComponent<ParticleSystem>();
			component7.Clear();
		}
	}

	public void UpdateAudition()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (!(Ref == PlayerController.Local.Ref))
		{
			return;
		}
		if (!NetworkBool.op_Implicit(Deaf) && !NetworkBool.op_Implicit(Phasing) && !NetworkBool.op_Implicit(Exorcised) && !NetworkBool.op_Implicit(Asleep))
		{
			if (PrimaryRolePower == PlayerPrimaryRolePower.Tracker)
			{
				TickTimer primaryRoleActionTimer = PrimaryRoleActionTimer;
				if (((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
				{
					goto IL_0070;
				}
			}
			AudioListener.volume = 1f;
			return;
		}
		goto IL_0070;
		IL_0070:
		AudioListener.volume = 0f;
	}

	public void UpdateScaleAndPitch()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		Transform value = Traverse.Create((object)PlayerController.PlayerEffectManager).Field<Transform>("villager").Value;
		Transform value2 = Traverse.Create((object)PlayerController.PlayerEffectManager).Field<Transform>("wolf").Value;
		if (NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.Giant))
		{
			value.localScale = new Vector3(8f, 8f, 8f);
			value2.localScale = new Vector3(3.2f, 3.2f, 3.2f);
		}
		else if (NetworkBool.op_Implicit(Tiny))
		{
			value.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			value2.localScale = new Vector3(0.15f, 0.15f, 0.15f);
		}
		else
		{
			value.localScale = new Vector3(1f, 1f, 1f);
			if (NewPrimaryRole == PlayerNewPrimaryRole.Beast)
			{
				value2.localScale = new Vector3(0.45f, 0.45f, 0.45f);
			}
			else
			{
				value2.localScale = new Vector3(0.4f, 0.4f, 0.4f);
			}
		}
		PlayerController.UpdateCameraAnchorOffset();
		PlayerController.EnableInteractCollider(!NetworkBool.op_Implicit(Tiny) && !NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.Giant));
		if (!NetworkBool.op_Implicit(PlayerController.IsDead))
		{
			PlayerController.EnablePlayerHitBox(!NetworkBool.op_Implicit(Tiny) && !NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.Giant));
		}
		VoiceSpeaker componentInChildren = ((Component)PlayerController).GetComponentInChildren<VoiceSpeaker>();
		if ((Object)(object)componentInChildren != (Object)null)
		{
			componentInChildren.UpdatePitch(VoiceChanges.GetVoicePitch(PlayerController, this));
		}
	}

	public IEnumerator WaitAndUpdateWolfColor(float waitTime)
	{
		yield return (object)new WaitForSeconds(waitTime);
		UpdateWolfColor();
	}

	public IEnumerator WaitAndPlaySuccessShot(float waitTime)
	{
		yield return (object)new WaitForSeconds(waitTime);
		AudioManager.Play("SUCCESS_SHOT", (MixerTarget)2, 1f, 1f);
	}

	public void UpdateCanMoveAnimation()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(CanMoveCustom());
	}

	private bool CanMoveCustom()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(Paralyzed) || NetworkBool.op_Implicit(Downed) || NetworkBool.op_Implicit(Dying) || NetworkBool.op_Implicit(Asleep) || NetworkBool.op_Implicit(Banished) || NetworkBool.op_Implicit(CapturedByCultist) || NetworkBool.op_Implicit(PlayerController.IsTrapped))
		{
			return false;
		}
		TickTimer val = PrimaryRoleActionTimer;
		if (((TickTimer)(ref val)).IsRunning && (NewPrimaryRole == PlayerNewPrimaryRole.Voodoo || NewPrimaryRole == PlayerNewPrimaryRole.Cultist || PrimaryRolePower == PlayerPrimaryRolePower.Warlock || PrimaryRolePower == PlayerPrimaryRolePower.Necromancer || PrimaryRolePower == PlayerPrimaryRolePower.Tracker || PrimaryRolePower == PlayerPrimaryRolePower.Exorcist || PrimaryRolePower == PlayerPrimaryRolePower.Survivalist || PrimaryRolePower == PlayerPrimaryRolePower.Scout || PrimaryRolePower == PlayerPrimaryRolePower.Magician || PrimaryRolePower == PlayerPrimaryRolePower.Mystic || PrimaryRolePower == PlayerPrimaryRolePower.Runemaster))
		{
			return false;
		}
		val = SecondaryRoleActionTimer;
		if (((TickTimer)(ref val)).IsRunning)
		{
			PlayerSecondaryRole secondaryRole = SecondaryRole;
			PlayerSecondaryRole playerSecondaryRole = secondaryRole;
			if (playerSecondaryRole == PlayerSecondaryRole.BothTeleporter || playerSecondaryRole == PlayerSecondaryRole.BothMedium || playerSecondaryRole == PlayerSecondaryRole.BothScavenger)
			{
				return false;
			}
		}
		if (NetworkBool.op_Implicit(SecondaryRolePowerActive) && (SecondaryRole == PlayerSecondaryRole.BothAstral || SecondaryRole == PlayerSecondaryRole.BothActor))
		{
			return false;
		}
		val = SabotageTimer;
		if (!((TickTimer)(ref val)).IsRunning)
		{
			val = SurvivalistSaveTimer;
			if (!((TickTimer)(ref val)).IsRunning)
			{
				val = TrapDisarmTimer;
				if (!((TickTimer)(ref val)).IsRunning)
				{
					val = LootCorpseTimer;
					if (!((TickTimer)(ref val)).IsRunning)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public void UpdateMoveSpeed()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController playerController = PlayerController;
			CharacterMovementHandler characterMovementHandler = playerController.CharacterMovementHandler;
			NetworkCharacterControllerPrototypeCustom value = Traverse.Create((object)characterMovementHandler).Field<NetworkCharacterControllerPrototypeCustom>("_networkCharacterControllerPrototypeCustom").Value;
			float num = 1.5f;
			float num2 = 0f;
			if (NetworkBool.op_Implicit(Empowered) && NetworkBool.op_Implicit(PlayerController.IsWolf))
			{
				num2 += 0.14999998f;
			}
			if (NetworkBool.op_Implicit(Portal))
			{
				num2 += 0.20000005f;
			}
			if (NetworkBool.op_Implicit(Predator))
			{
				num2 += 0.14999998f;
			}
			if (NetworkBool.op_Implicit(Sprinting))
			{
				num2 += 0.75f;
			}
			if (PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is ChasingEffect))
			{
				num2 += 0.5f;
			}
			if (NetworkBool.op_Implicit(Phasing))
			{
				num2 += (NetworkBool.op_Implicit(playerController.IsWolf) ? 0.25f : 0.5f);
			}
			if (NetworkBool.op_Implicit(BombActive))
			{
				num2 += 0.14999998f;
			}
			if (NetworkBool.op_Implicit(Fleeing))
			{
				num2 += 0.75f;
			}
			if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && NetworkBool.op_Implicit(PlayerController.IsWolf))
			{
				num2 += 0.25f;
			}
			if (NewPrimaryRole == PlayerNewPrimaryRole.Zombie)
			{
				num2 += 0.100000024f;
			}
			if (NetworkBool.op_Implicit(Sneaky))
			{
				num2 += 0.5f;
			}
			if (NetworkBool.op_Implicit(Escaping))
			{
				num2 += 0.100000024f;
			}
			if (NetworkBool.op_Implicit(Tenacity))
			{
				num2 += 0.20000005f;
			}
			if ((Object)(object)Accessory != (Object)null && Accessory is AccessoryRing { EffectActive: not false })
			{
				num2 = ((!NetworkBool.op_Implicit(PlayerController.IsWolf)) ? (num2 + 0.14999998f) : (num2 + 0.14999998f));
			}
			if (PrimaryRolePower == PlayerPrimaryRolePower.Spotter && NetworkBool.op_Implicit(Spotter))
			{
				num2 += 0.13f;
			}
			if (NetworkBool.op_Implicit(PurifierBurn) && !NetworkBool.op_Implicit(PlayerController.IsWolf))
			{
				num2 += 0.20000005f;
			}
			if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Haste)
			{
				num2 = ((!NetworkBool.op_Implicit(PlayerController.IsWolf)) ? (num2 + 0.35000002f) : (num2 + 0.25f));
			}
			if (NetworkBool.op_Implicit(TournamentWinner))
			{
				num2 = ((!NetworkBool.op_Implicit(PlayerController.IsWolf)) ? (num2 + 0.14999998f) : (num2 + 0.08000004f));
			}
			if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.FullMoon && NetworkBool.op_Implicit(PlayerController.IsWolf) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				num2 += 0.20000005f;
			}
			if (NetworkBool.op_Implicit(PlayerController.PlayerEffectManager.BonusSpeed))
			{
				num2 /= 2f;
			}
			if (num2 > 0f)
			{
				num *= 1f + num2;
			}
			if (NetworkBool.op_Implicit(Disease))
			{
				num *= 0.85f;
			}
			if (NetworkBool.op_Implicit(Wounded))
			{
				num *= 0.65f;
			}
			if (NetworkBool.op_Implicit(Disoriented))
			{
				num *= 0.5f;
			}
			if (NetworkBool.op_Implicit(Nauseated))
			{
				num *= 0.85f;
			}
			if (NetworkBool.op_Implicit(Stunned))
			{
				num *= 0.5f;
			}
			if (NetworkBool.op_Implicit(Panic))
			{
				num *= 0.65f;
			}
			if (NetworkBool.op_Implicit(Sleepy))
			{
				float num3 = Mathf.InverseLerp(0f, 1000f, (float)SleepStacks);
				num *= Mathf.Lerp(0.85f, 0.25f, num3);
			}
			if (NetworkBool.op_Implicit(Burning))
			{
				num *= 0.7f;
			}
			if (NetworkBool.op_Implicit(PurifierBurn) && NetworkBool.op_Implicit(PlayerController.IsWolf))
			{
				num *= 0.7f;
			}
			if (NetworkBool.op_Implicit(Repulsion))
			{
				float num4 = Mathf.InverseLerp(0f, 1000f, (float)RepulsionStacks);
				num *= Mathf.Lerp(0.9f, 0.25f, num4);
			}
			if (NetworkBool.op_Implicit(Hubris))
			{
				num *= 0.92f;
			}
			if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Eclipse && NetworkBool.op_Implicit(PlayerController.IsWolf) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				num *= 0.86f;
			}
			value.maxSpeed = num;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateMoveSpeed error: " + ex));
		}
	}

	public void TeleportToRandomTeleporter()
	{
		Teleporter val = FindRandomTeleporter();
		if ((Object)(object)val != (Object)null)
		{
			TeleportToTeleporter(val);
		}
	}

	public void TeleportToTeleporter(Teleporter teleporter)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = ((Component)teleporter).transform;
		PlayerController.CharacterMovementHandler.TeleportData = new NetworkTeleportData(transform.position, transform.rotation, true);
		PlayerController.IsClimbing = NetworkBool.op_Implicit(false);
		GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("TELEPORT_START"), ((Component)PlayerController).transform.position, 20f, 1f);
		NetworkRunner runner = ((SimulationBehaviour)this).Runner;
		NetworkString<_16> val = NetworkString<_16>.op_Implicit("TELEPORT_END");
		NetworkTeleportData teleportData = PlayerController.CharacterMovementHandler.TeleportData;
		GameManager.Rpc_BroadcastFollowSound(runner, val, ((NetworkTeleportData)(ref teleportData)).Position, 20f, 1f);
		ApplyEffectToPlayer(PlayerController, "LycansNewRoles.EffectTrapResistance", ((SimulationBehaviour)this).Runner);
	}

	public static Teleporter FindRandomTeleporter()
	{
		List<Teleporter> list = (from o in Object.FindObjectsOfType<Teleporter>()
			where o.MapID == GameManager.Instance.MapID
			select o).ToList();
		if (list.Any())
		{
			return CollectionsUtil.Grab<Teleporter>(list, 1).First();
		}
		return null;
	}

	public static void PlaySuccessSound()
	{
		AudioManager.Play("KILL_1", (MixerTarget)2, 0.1f, 1f);
	}

	public static void ApplyEffectToPlayer(PlayerController player, string effectName, NetworkRunner runner, float durationMultiplier = 1f, float? specificBaseDuration = null)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject(effectName);
		PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(player.Ref);
		if (NetworkBool.op_Implicit(player2.Immune))
		{
			GameObject prefabByPrefabId = NetworkObjectService.Instance.GetPrefabByPrefabId(networkObject);
			if (prefabByPrefabId.GetComponent<CustomEffect>().CanBeDispelled)
			{
				return;
			}
		}
		Effect val = player.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is CustomEffect && (o as CustomEffect).CustomEffectName == effectName);
		if ((Object)(object)val != (Object)null)
		{
			float num = (specificBaseDuration.HasValue ? specificBaseDuration.Value : Traverse.Create((object)val).Field<float>("duration").Value);
			num *= durationMultiplier;
			int num2 = Tick.op_Implicit(runner.Simulation.Tick) + (int)(num * (float)runner.Config.Simulation.TickRate);
			if (num2 <= Traverse.Create((object)val.EffectTimer).Field<int>("_target").Value)
			{
				return;
			}
		}
		runner.Spawn(networkObject, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
		{
			FinalizeEffect(runner, player, ((Component)no).GetComponent<Effect>(), durationMultiplier, specificBaseDuration);
		}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		if (effectName == "LycansNewRoles.EffectResilience")
		{
			networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.EffectSpiritResistance");
			runner.Spawn(networkObject, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
			{
				FinalizeEffect(runner, player, ((Component)no).GetComponent<Effect>(), 1f, specificBaseDuration * durationMultiplier * 1.5f);
			}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		}
	}

	public static void ApplyEffectToPlayer(PlayerController player, Effect effect, NetworkRunner runner, float durationMultiplier = 1f, float? specificBaseDuration = null)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)player == (Object)null)
		{
			return;
		}
		PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(player.Ref);
		if (NetworkBool.op_Implicit(player2.Immune) && ((Object)(object)((Component)effect).GetComponent<CustomEffect>() == (Object)null || ((Component)effect).GetComponent<CustomEffect>().CanBeDispelled))
		{
			return;
		}
		Effect val = player.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => ((NetworkBehaviour)o).Id == ((NetworkBehaviour)effect).Id);
		if ((Object)(object)val != (Object)null)
		{
			float num = (specificBaseDuration.HasValue ? specificBaseDuration.Value : Traverse.Create((object)val).Field<float>("duration").Value);
			int num2 = Tick.op_Implicit(runner.Simulation.Tick) + (int)(num * (float)runner.Config.Simulation.TickRate);
			if (num2 <= Traverse.Create((object)val.EffectTimer).Field<int>("_target").Value)
			{
				return;
			}
		}
		if (effect is CustomEffect customEffect)
		{
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject(customEffect.CustomEffectName);
			runner.Spawn(networkObject, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
			{
				FinalizeEffect(runner, player, ((Component)no).GetComponent<Effect>(), durationMultiplier, specificBaseDuration);
			}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		}
		else
		{
			runner.Spawn<Effect>(effect, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
			{
				FinalizeEffect(runner, player, ((Component)no).GetComponent<Effect>(), durationMultiplier, specificBaseDuration);
			}, (NetworkObjectPredictionKey?)null, true);
		}
	}

	private static void FinalizeEffect(NetworkRunner runner, PlayerController player, Effect effect, float durationMultiplier, float? specificBaseDuration = null)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Invalid comparison between Unknown and I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Invalid comparison between Unknown and I4
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Invalid comparison between Unknown and I4
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Invalid comparison between Unknown and I4
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Invalid comparison between Unknown and I4
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Invalid comparison between Unknown and I4
		PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(player.Ref);
		float num = (specificBaseDuration.HasValue ? specificBaseDuration.Value : Traverse.Create((object)effect).Field<float>("duration").Value);
		if (NetworkBool.op_Implicit(player.IsWolf) && !specificBaseDuration.HasValue)
		{
			float? durationOnTransformedWolf = BalancingValues.GetModifiedEffectData(effect).DurationOnTransformedWolf;
			if (durationOnTransformedWolf.HasValue)
			{
				num = durationOnTransformedWolf.Value;
			}
			if ((int)effect.GetEffectType() == 2 && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				num *= 0.5f;
			}
		}
		if (player2.NewPrimaryRole == PlayerNewPrimaryRole.Zombie)
		{
			float? durationOnZombie = BalancingValues.GetModifiedEffectData(effect).DurationOnZombie;
			if (durationOnZombie.HasValue)
			{
				num = durationOnZombie.Value;
			}
		}
		num *= durationMultiplier;
		if (!(effect is CustomEffect { DurationAffectedByModifiers: false }))
		{
			if (player2.SecondaryRole == PlayerSecondaryRole.BothMetabolic)
			{
				if ((int)effect.GetEffectType() == 0)
				{
					num *= 1.25f;
				}
				else if ((int)effect.GetEffectType() == 2)
				{
					num *= 0.8f;
				}
			}
			if (NetworkBool.op_Implicit(player2.Tenacity) && (int)effect.GetEffectType() == 2)
			{
				num *= 0.66f;
			}
			if (NetworkBool.op_Implicit(player2.Hubris) && (int)effect.GetEffectType() == 0)
			{
				num *= 0.75f;
			}
			if (player2.Accessory is AccessorySpellbook && (int)effect.GetEffectType() == 2)
			{
				num *= 0.7f;
			}
		}
		player.PlayerEffectManager.ApplyEffect(effect);
		Traverse.Create((object)effect).Field<PlayerRef>("_EffectPlayer").Value = player.Ref;
		if (runner.IsServer)
		{
			Traverse.Create((object)effect).Field<CustomTickTimer>("_EffectTimer").Value = CustomTickTimer.CreateFromTicks(runner, (int)(num * (float)runner.Config.Simulation.TickRate));
		}
		((NetworkBehaviour)effect).CopyBackingFieldsToState(true);
		Traverse.Create((object)effect).Method("ApplyEffectToPlayerSpecific", new List<Type> { typeof(PlayerRef) }.ToArray(), (object[])null).GetValue(new object[1] { player.Ref });
	}

	public Color GetHealthColor()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		float num = PlayerController.Hunger / (float)GameManager.Instance.MaxHunger;
		if (num >= 0.82f)
		{
			return HealthVeryHigh;
		}
		if (num >= 0.65f)
		{
			return HealthHigh;
		}
		if (num >= 0.45f)
		{
			return HealthMedium;
		}
		if (num >= 0.26f)
		{
			return HealthLow;
		}
		return HealthCritical;
	}

	public bool CanBeHeardByObservedPlayer()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerController.Local == (Object)null || (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer == (Object)null)
		{
			return false;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (PrimaryRolePower == PlayerPrimaryRolePower.Specter)
		{
			return false;
		}
		if (NetworkBool.op_Implicit(PlayerController.IsDead) && (!NetworkBool.op_Implicit(PlayerController.IsDeadChannel) || !NetworkBool.op_Implicit(PlayerController.Local.IsDead) || !NetworkBool.op_Implicit(PlayerController.Local.IsDeadChannel) || Local.PrimaryRolePower == PlayerPrimaryRolePower.Specter))
		{
			return false;
		}
		if (NetworkBool.op_Implicit(Kidnapped) && !NetworkBool.op_Implicit(player.Kidnapped) && player.NewPrimaryRole != PlayerNewPrimaryRole.Kidnapper)
		{
			return false;
		}
		if (NetworkBool.op_Implicit(PlayerController.Local.IsDead) && NetworkBool.op_Implicit(PlayerController.Local.IsDeadChannel) && !NetworkBool.op_Implicit(PlayerController.IsDead) && ExtraSettings.Instance.DisableLivingVoicesWhenInDeadChannel)
		{
			return false;
		}
		if (NetworkBool.op_Implicit(player.Isolation) && !NetworkBool.op_Implicit(PlayerController.IsWolf) && !NetworkBool.op_Implicit(PlayerController.IsDead))
		{
			return false;
		}
		if (NetworkBool.op_Implicit(Isolation) && !NetworkBool.op_Implicit(player.PlayerController.IsWolf) && player.NewPrimaryRole != PlayerNewPrimaryRole.Zombie && !NetworkBool.op_Implicit(PlayerController.Local.IsDead))
		{
			return false;
		}
		return true;
	}

	[Rpc]
	public unsafe static void Rpc_Play_Animation(NetworkRunner runner, int playerIndex, int emoteIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Play_Animation(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = emoteIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			string animationName = "";
			bool flag = true;
			bool flag2 = false;
			switch (emoteIndex)
			{
			case 1:
				animationName = "6-Threatening to kill-NR";
				break;
			case 2:
				animationName = "MCU_am_Stand_Emotion_ThisGuy_01";
				break;
			case 3:
				animationName = "HumanM@HeadShake02";
				break;
			case 4:
				animationName = "HumanM@Cheer02";
				break;
			case 5:
				animationName = "HumanM@HandClap01";
				break;
			case 6:
				animationName = "HumanM@Dance01 - Loop";
				flag2 = true;
				break;
			case 7:
				animationName = "HumanM@Dance05 - Loop";
				flag2 = true;
				break;
			case 8:
				animationName = "HumanM@Dance06 - Loop";
				flag2 = true;
				break;
			case 9:
				flag = false;
				animationName = "Unarmed-Knockback-Back1";
				break;
			}
			if (!flag || player.PlayerAnimations.CanDoEmote(animationName))
			{
				if (flag2)
				{
					player.PlayerAnimations.ToggleLoopEmote(animationName);
				}
				else
				{
					player.PlayerAnimations.PlayNonLoopEmote(animationName);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Play_Animation error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Play_Animation(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Play_Animation_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int emoteIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Play_Animation(runner, playerIndex, emoteIndex);
	}

	[Rpc]
	public unsafe static void Rpc_End_Game(NetworkRunner runner, int winnerIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Invalid comparison between Unknown and I4
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Invalid comparison between Unknown and I4
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Invalid comparison between Unknown and I4
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0919: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ace: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1021: Unknown result type (might be due to invalid IL or missing references)
		//IL_1027: Invalid comparison between Unknown and I4
		//IL_104b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1036: Unknown result type (might be due to invalid IL or missing references)
		//IL_100e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1060: Unknown result type (might be due to invalid IL or missing references)
		//IL_1243: Unknown result type (might be due to invalid IL or missing references)
		//IL_1249: Invalid comparison between Unknown and I4
		//IL_1224: Unknown result type (might be due to invalid IL or missing references)
		//IL_122a: Invalid comparison between Unknown and I4
		//IL_1297: Unknown result type (might be due to invalid IL or missing references)
		//IL_129d: Invalid comparison between Unknown and I4
		//IL_1260: Unknown result type (might be due to invalid IL or missing references)
		//IL_1266: Invalid comparison between Unknown and I4
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_End_Game(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = winnerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(winnerIndex);
			if (runner.IsServer)
			{
				GameManager.Instance.IsFinished = NetworkBool.op_Implicit(true);
				GameManager.LightingManager.IsTransition = NetworkBool.op_Implicit(false);
				PlayerRegistry.ForEach((Action<PlayerController>)delegate(PlayerController pObj)
				{
					//IL_0003: Unknown result type (might be due to invalid IL or missing references)
					//IL_000f: Unknown result type (might be due to invalid IL or missing references)
					//IL_0027: Unknown result type (might be due to invalid IL or missing references)
					//IL_0033: Unknown result type (might be due to invalid IL or missing references)
					//IL_003f: Unknown result type (might be due to invalid IL or missing references)
					//IL_004b: Unknown result type (might be due to invalid IL or missing references)
					//IL_0057: Unknown result type (might be due to invalid IL or missing references)
					//IL_0068: Unknown result type (might be due to invalid IL or missing references)
					pObj.CanMove = NetworkBool.op_Implicit(false);
					PlayerCustom player5 = PlayerCustomRegistry.GetPlayer(pObj.Ref);
					if ((Object)(object)player5 != (Object)null)
					{
						player5.PrimaryRoleActionTimer = TickTimer.None;
						player5.PrimaryRolePowerCooldownTimer = TickTimer.None;
						player5.SecondaryRolePowerCooldownTimer = TickTimer.None;
						player5.TrapDisarmTimer = TickTimer.None;
						if (NetworkBool.op_Implicit(player5.Dying))
						{
							player5.Dying = NetworkBool.op_Implicit(false);
						}
					}
				});
				GameManager.State.Server_DelaySetState((EGameState)5, 5f);
				if (player.NewPrimaryRole == PlayerNewPrimaryRole.Mercenary)
				{
					foreach (PlayerRef item3 in player.MercenaryTargetsAlreadyHit)
					{
						PlayerController player2 = PlayerRegistry.GetPlayer(item3);
						if (!NetworkBool.op_Implicit(player2.IsDead))
						{
							player2.Rpc_Kill(player.Ref);
							Rpc_Effect_On_Player(runner, player2.Index, 11);
						}
					}
				}
			}
			if (!runner.IsPlayer)
			{
				return;
			}
			PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.VillageIdiot);
			List<PlayerCustom> source = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Agent).ToList();
			PlayerCustom specificNewPrimaryRole2 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.Spy);
			PlayerCustom specificNewPrimaryRole3 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.Scientist);
			List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Lover).ToList();
			PlayerCustom specificNewPrimaryRole4 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.Beast);
			PlayerCustom specificNewPrimaryRole5 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.Mercenary);
			PlayerCustom specificNewPrimaryRole6 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.Voodoo);
			PlayerCustom specificNewPrimaryRole7 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.Kidnapper);
			PlayerCustom specificNewPrimaryRole8 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerNewPrimaryRole.Cultist);
			PlayerCustom specificPrimaryRolePower = PlayerCustomRegistry.GetSpecificPrimaryRolePower(PlayerPrimaryRolePower.Avatar);
			PlayerCustom specificPrimaryRolePower2 = PlayerCustomRegistry.GetSpecificPrimaryRolePower(PlayerPrimaryRolePower.Mole);
			PlayerController playerController = player.PlayerController;
			PlayerCustom player3 = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			PlayerController playerController2 = player3.PlayerController;
			bool flag = false;
			List<object> list2 = new List<object>();
			string text;
			switch (player.NewPrimaryRole)
			{
			case PlayerNewPrimaryRole.VillageIdiot:
				flag = player3.Index == winnerIndex;
				text = "NALES_UI_VILLAGE_IDIOT_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Agent:
				flag = player3.Index == winnerIndex;
				text = "NALES_UI_AGENT_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Spy:
				flag = player3.Index == winnerIndex;
				text = "NALES_UI_SPY_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Scientist:
				flag = player3.Index == winnerIndex;
				text = "NALES_UI_SCIENTIST_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Lover:
				flag = player3.NewPrimaryRole == PlayerNewPrimaryRole.Lover;
				text = "NALES_UI_LOVERS_WIN";
				list2.Add(list[0].PlayerController.PlayerData.Username);
				list2.Add(list[1].PlayerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Beast:
				flag = player3.Index == winnerIndex;
				text = "NALES_UI_BEAST_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Voodoo:
				flag = player3.NewPrimaryRole == PlayerNewPrimaryRole.Voodoo || player3.NewPrimaryRole == PlayerNewPrimaryRole.Zombie;
				text = "NALES_UI_VOODOO_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Mercenary:
				flag = player3.NewPrimaryRole == PlayerNewPrimaryRole.Mercenary;
				text = "NALES_UI_MERCENARY_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Kidnapper:
				flag = player3.Index == winnerIndex;
				text = "NALES_UI_KIDNAPPER_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.Cultist:
				flag = player3.Index == winnerIndex;
				text = "NALES_UI_CULTIST_WIN";
				list2.Add(playerController.PlayerData.Username);
				break;
			case PlayerNewPrimaryRole.None:
				if ((int)playerController.Role == 1)
				{
					text = "UI_WOLVES_WIN";
					flag = player3.NewPrimaryRole == PlayerNewPrimaryRole.None && (int)playerController2.Role == 1;
					if (player3.NewPrimaryRole == PlayerNewPrimaryRole.Traitor)
					{
						flag = true;
					}
				}
				else
				{
					text = "UI_VILLAGERS_WIN";
					flag = player3.NewPrimaryRole == PlayerNewPrimaryRole.None && (int)playerController2.Role != 1;
				}
				break;
			default:
				Plugin.Logger.LogError((object)("Incorrect winning player role: " + player.NewPrimaryRole));
				text = "";
				break;
			}
			Color val = (flag ? GameUI.VillagerColor : GameUI.WolfColor);
			string text2 = (flag ? "VICTORY" : "DEFEAT");
			string text3 = "";
			int num3 = 0;
			PlayerCustom playerCustom = PlayerCustomRegistry.AllPlayers.FirstOrDefault((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Lover && (int)o.PlayerController.Role == 1);
			PlayerRef item = (((Object)(object)playerCustom != (Object)null) ? playerCustom.Ref : PlayerRef.None);
			PlayerRef item2 = (((Object)(object)specificPrimaryRolePower2 != (Object)null) ? specificPrimaryRolePower2.Ref : PlayerRef.None);
			text3 = text3 + "<color=#" + ColorUtility.ToHtmlStringRGB(NewPrimaryRoleWolfRoleColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_WOLVES") + UpdateRoleUtility.ListWolvesAsString(new List<PlayerRef> { item, item2 }) + "</color>";
			num3++;
			if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Traitor) > 0)
			{
				List<PlayerCustom> source2 = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerNewPrimaryRole.Traitor).ToList();
				text3 = text3 + Environment.NewLine + "<color=#" + ColorUtility.ToHtmlStringRGB(NewPrimaryRoleTraitorColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_TRAITORS") + UpdateRoleUtility.ListPlayersAsString(source2.Select((PlayerCustom o) => PlayerRegistry.GetPlayer(o.Ref)).ToList()) + "</color>";
				num3++;
			}
			if ((Object)(object)specificNewPrimaryRole != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleVillageIdiotColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_VILLAGE_IDIOT"), specificNewPrimaryRole.PlayerController.PlayerData.Username);
				num3++;
			}
			if (source.Any())
			{
				text3 = text3 + Environment.NewLine + "<color=#" + ColorUtility.ToHtmlStringRGB(NewPrimaryRoleAgentColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_AGENTS") + UpdateRoleUtility.ListPlayersAsString(source.Select((PlayerCustom o) => o.PlayerController).ToList()) + "</color>";
				num3++;
			}
			if ((Object)(object)specificNewPrimaryRole2 != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleSpyColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_SPY"), specificNewPrimaryRole2.PlayerController.PlayerData.Username);
				num3++;
			}
			if ((Object)(object)specificNewPrimaryRole3 != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleScientistColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_SCIENTIST"), specificNewPrimaryRole3.PlayerController.PlayerData.Username);
				num3++;
			}
			if (list.Count >= 2)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2} & {3}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleLoverColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_LOVERS"), list[0].PlayerController.PlayerData.Username, PlayerRegistry.GetPlayer(list[1].Ref).PlayerData.Username);
				num3++;
			}
			if ((Object)(object)specificNewPrimaryRole4 != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleBeastColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_BEAST"), specificNewPrimaryRole4.PlayerController.PlayerData.Username);
				num3++;
			}
			if ((Object)(object)specificNewPrimaryRole5 != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleMercenaryColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_MERCENARY"), specificNewPrimaryRole5.PlayerController.PlayerData.Username);
				num3++;
			}
			if ((Object)(object)specificNewPrimaryRole6 != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleVoodooColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_VOODOO"), specificNewPrimaryRole6.PlayerController.PlayerData.Username);
				num3++;
			}
			if ((Object)(object)specificNewPrimaryRole7 != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleKidnapperColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_KIDNAPPER"), specificNewPrimaryRole7.PlayerController.PlayerData.Username);
				num3++;
			}
			if ((Object)(object)specificNewPrimaryRole8 != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(NewPrimaryRoleCultistColor), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_CULTIST"), specificNewPrimaryRole8.PlayerController.PlayerData.Username);
				num3++;
			}
			IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => IsPrimaryRolePowerForEliteVillagers(o.InitialPower));
			foreach (PlayerCustom item4 in enumerable)
			{
				string text4 = Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(GetPrimaryRolePowerColor(item4.InitialPower)), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_" + GetPrimaryRolePowerString(item4.InitialPower)), item4.PlayerController.PlayerData.Username);
				text3 += text4;
				num3++;
			}
			if ((Object)(object)specificPrimaryRolePower != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(Color.cyan), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_AVATAR"), specificPrimaryRolePower.PlayerController.PlayerData.Username);
				num3++;
			}
			if ((Object)(object)specificPrimaryRolePower2 != (Object)null)
			{
				text3 = text3 + Environment.NewLine + string.Format("<color=#{0}>{1}{2}</color>", ColorUtility.ToHtmlStringRGB(Color.cyan), TranslationManager.Instance.GetTranslation("NALES_UI_ROLES_RECAP_MOLE"), specificPrimaryRolePower2.PlayerController.PlayerData.Username);
				num3++;
			}
			AudioManager.Play(text2, (MixerTarget)2, 0.5f, 1f);
			LocalizeStringEvent value = Traverse.Create((object)GameManager.Instance.gameUI).Field<LocalizeStringEvent>("transition").Value;
			TextMeshProUGUI value2 = Traverse.Create((object)GameManager.Instance.gameUI).Field<TextMeshProUGUI>("transitionText").Value;
			TextMeshProUGUI value3 = Traverse.Create((object)GameManager.Instance.gameUI).Field<TextMeshProUGUI>("wolvesRecap").Value;
			value.StringReference.Arguments = list2;
			int num4 = Mathf.Max(0, num3 * 24 - 16);
			Vector3 position = default(Vector3);
			((Vector3)(ref position))._002Ector(((TMP_Text)value3).transform.position.x, ((TMP_Text)value3).transform.position.y + (float)num4, ((TMP_Text)value3).transform.position.z);
			((TMP_Text)value2).transform.position = position;
			((MonoBehaviour)GameManager.Instance.gameUI).StopCoroutine("WaitThenHideOverlay");
			GameManager.Instance.gameUI.UpdateTransitionText(text, val);
			((TMP_Text)value3).richText = true;
			GameManager.Instance.gameUI.UpdateWolvesRecap(text3);
			GameManager.Instance.gameUI.ShowWolvesRecap(true);
			GameManager.Instance.gameUI.StartFade(true);
			Dictionary<PlayerRef, PlayerDisplay> value4 = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
			foreach (KeyValuePair<PlayerRef, PlayerDisplay> item5 in value4)
			{
				TextMeshProUGUI value5 = Traverse.Create((object)item5.Value).Field<TextMeshProUGUI>("username").Value;
				PlayerController player4 = PlayerRegistry.GetPlayer(item5.Key);
				if ((Object)(object)player4 != (Object)null)
				{
					NetworkPlayerData playerData = player4.PlayerData;
					if (((NetworkPlayerData)(ref playerData)).IsValid)
					{
						playerData = player4.PlayerData;
						((TMP_Text)value5).text = ((object)playerData.Username/*cast due to constrained. prefix*/).ToString();
						((Graphic)value5).color = new Color(255f, 255f, 255f, 0.6f);
					}
				}
			}
			Local.PrimaryRolePowerPlayersList.Clear();
			Local.DetectiveIntelList.Clear();
			Local.MercenaryTargetsAlreadyHit.Clear();
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.Kidnapped = NetworkBool.op_Implicit(false);
			}
			GameManagerCustom.Rpc_New_Event(runner, 0);
			switch (player.NewPrimaryRole)
			{
			case PlayerNewPrimaryRole.Traitor:
				UIManager.LastGameSummaryPanel.AddWinner(UILastGameSummaryPanel.WinnerType.Wolves, PlayerRef.None);
				break;
			case PlayerNewPrimaryRole.Lover:
				UIManager.LastGameSummaryPanel.AddWinner(UILastGameSummaryPanel.WinnerType.Lovers, PlayerRef.None);
				break;
			case PlayerNewPrimaryRole.None:
				if ((int)player.PlayerController.Role == 1)
				{
					UIManager.LastGameSummaryPanel.AddWinner(UILastGameSummaryPanel.WinnerType.Wolves, PlayerRef.None);
				}
				else
				{
					UIManager.LastGameSummaryPanel.AddWinner(UILastGameSummaryPanel.WinnerType.Villagers, PlayerRef.None);
				}
				break;
			default:
				UIManager.LastGameSummaryPanel.AddWinner(UILastGameSummaryPanel.WinnerType.OtherSoloRole, player.Ref);
				break;
			}
			if (!runner.IsServer)
			{
				return;
			}
			try
			{
				SessionStats.Stats.CurrentGame.EndDate = LycansUtility.GetFormattedCurrentDateTimeUtc;
				SessionStats.Stats.CurrentGame.HarvestDone = GameManager.Instance.Score + GameManagerCustom.Instance.CollectedLoot;
				SessionStats.Stats.CurrentGame.EndTiming = GameStats.GetCurrentTiming();
				foreach (PlayerCustom allPlayer2 in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer2.Stats.OnTalkingChanged(talking: false);
					int childCount = allPlayer2.PlayerController.hats.transform.childCount;
					for (int num5 = 0; num5 < childCount; num5++)
					{
						Transform child = allPlayer2.PlayerController.hats.transform.GetChild(num5);
						if (((Component)child).gameObject.activeSelf)
						{
							allPlayer2.Stats.Hat = ((Object)((Component)child).gameObject).name;
						}
					}
					switch (player.NewPrimaryRole)
					{
					case PlayerNewPrimaryRole.VillageIdiot:
					case PlayerNewPrimaryRole.Agent:
					case PlayerNewPrimaryRole.Spy:
					case PlayerNewPrimaryRole.Scientist:
					case PlayerNewPrimaryRole.Beast:
					case PlayerNewPrimaryRole.Mercenary:
					case PlayerNewPrimaryRole.Kidnapper:
					case PlayerNewPrimaryRole.Cultist:
						allPlayer2.Stats.Victorious = allPlayer2.Index == winnerIndex;
						break;
					case PlayerNewPrimaryRole.Voodoo:
						allPlayer2.Stats.Victorious = allPlayer2.NewPrimaryRole == PlayerNewPrimaryRole.Voodoo || allPlayer2.NewPrimaryRole == PlayerNewPrimaryRole.Zombie;
						break;
					case PlayerNewPrimaryRole.Lover:
						allPlayer2.Stats.Victorious = allPlayer2.NewPrimaryRole == PlayerNewPrimaryRole.Lover;
						break;
					case PlayerNewPrimaryRole.Traitor:
						allPlayer2.Stats.Victorious = (int)allPlayer2.PlayerController.Role == 1 || allPlayer2.NewPrimaryRole == PlayerNewPrimaryRole.Traitor;
						break;
					case PlayerNewPrimaryRole.None:
						if ((int)playerController.Role == 1)
						{
							allPlayer2.Stats.Victorious = ((int)allPlayer2.PlayerController.Role == 1 && allPlayer2.NewPrimaryRole != PlayerNewPrimaryRole.Lover) || allPlayer2.NewPrimaryRole == PlayerNewPrimaryRole.Traitor;
						}
						else
						{
							allPlayer2.Stats.Victorious = (int)allPlayer2.PlayerController.Role != 1 && allPlayer2.NewPrimaryRole == PlayerNewPrimaryRole.None;
						}
						break;
					}
				}
				Plugin.Logger.LogInfo((object)("Session stats: " + JsonConvert.SerializeObject((object)SessionStats.Stats)));
				if (PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => o.PlayerData.ID == "76561198034021995")) || PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => o.PlayerData.ID == "76561198045789440")) || PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => o.PlayerData.ID == "76561199060053791")) || (PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => o.PlayerData.ID == "76561197973106144")) && !PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => PlayerRef.op_Implicit(o.Ref) >= 1000)) && PlayerRegistry.Count >= 8))
				{
					HttpClient httpClient = new HttpClient();
					StringContent content = new StringContent(JsonConvert.SerializeObject((object)SessionStats.Stats));
					httpClient.PostAsync("https://mjconxaygsuwux4lsilhzwauhi0yigbp.lambda-url.eu-west-1.on.aws/", content);
				}
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError((object)("End Game stats error: " + ex));
			}
		}
		catch (Exception ex2)
		{
			Plugin.Logger.LogError((object)("Rpc_End_Game error: " + ex2));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_End_Game(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_End_Game_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int winnerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_End_Game(runner, winnerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Change_Color(NetworkRunner runner, int playerIndex, int colorIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Change_Color(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = colorIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			player.ColorIndex = colorIndex;
			player.UpdateColor();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Change_Color error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Change_Color(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Change_Color_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int colorIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Change_Color(runner, playerIndex, colorIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Change_Pet(NetworkRunner runner, int playerIndex, int petIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Change_Pet(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = petIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			player.PetIndex = petIndex;
			player.UpdatePet();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Change_Pet error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Change_Pet(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Change_Pet_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int petIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Change_Pet(runner, playerIndex, petIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Set_No_Dead_Role(NetworkRunner runner, int playerIndex, int noDeadRoleValue)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Set_No_Dead_Role(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = noDeadRoleValue;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			player.NoDeadRole = NetworkBool.op_Implicit(noDeadRoleValue == 1);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Set_No_Dead_Role error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Set_No_Dead_Role(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Set_No_Dead_Role_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int noDeadRoleValue = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Set_No_Dead_Role(runner, playerIndex, noDeadRoleValue);
	}

	[Rpc]
	public unsafe static void Rpc_Kick(NetworkRunner runner, int playerIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Kick(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (PlayerController.Local.Index == playerIndex)
			{
				GameManager.Instance.LeaveGame();
			}
			else if (runner.IsServer)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
				if (!player.ModVersion.HasValue)
				{
					GameManager.Rpc_DeletePlayer(((SimulationBehaviour)GameManager.Instance).Runner, player.Ref);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Kick error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Kick(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Kick_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Kick(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Update_Options_Display(NetworkRunner runner, int wolvesCount, int traitorsCount, int wolfPupsCount, int soloRolesGiven, int soloRolesChecked, int jobsChance, int jobsChecked, int elitePowersGiven, int elitePowersChecked, int avatarChance, int moleChance, int wolfPowersGiven, int wolfPowersChecked, int secondaryRolesGiven, int secondaryRolesChecked, int potionsChecked, int itemsChecked)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 76;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Update_Options_Display(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = wolvesCount;
					num2 += 4;
					*(int*)(data + num2) = traitorsCount;
					num2 += 4;
					*(int*)(data + num2) = wolfPupsCount;
					num2 += 4;
					*(int*)(data + num2) = soloRolesGiven;
					num2 += 4;
					*(int*)(data + num2) = soloRolesChecked;
					num2 += 4;
					*(int*)(data + num2) = jobsChance;
					num2 += 4;
					*(int*)(data + num2) = jobsChecked;
					num2 += 4;
					*(int*)(data + num2) = elitePowersGiven;
					num2 += 4;
					*(int*)(data + num2) = elitePowersChecked;
					num2 += 4;
					*(int*)(data + num2) = avatarChance;
					num2 += 4;
					*(int*)(data + num2) = moleChance;
					num2 += 4;
					*(int*)(data + num2) = wolfPowersGiven;
					num2 += 4;
					*(int*)(data + num2) = wolfPowersChecked;
					num2 += 4;
					*(int*)(data + num2) = secondaryRolesGiven;
					num2 += 4;
					*(int*)(data + num2) = secondaryRolesChecked;
					num2 += 4;
					*(int*)(data + num2) = potionsChecked;
					num2 += 4;
					*(int*)(data + num2) = itemsChecked;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (!runner.IsServer)
			{
				Plugin.CustomConfig.TraitorsCount = traitorsCount;
				Plugin.CustomConfig.WolfPupsCount = wolfPupsCount;
				Plugin.CustomConfig.SoloRolesCount = soloRolesGiven;
				Plugin.CustomConfig.VillagerPowersChance = jobsChance;
				Plugin.CustomConfig.ElitesCount = elitePowersGiven;
				Plugin.CustomConfig.AvatarChance = avatarChance;
				Plugin.CustomConfig.MoleChance = moleChance;
				Plugin.CustomConfig.WolfPowersCount = wolfPowersGiven;
				Plugin.CustomConfig.SecondaryRolesCount = secondaryRolesGiven;
				UIManager.OptionsDisplayPanel.RefreshConfiguration(wolvesCount, traitorsCount, wolfPupsCount, soloRolesGiven, soloRolesChecked, jobsChance, jobsChecked, elitePowersGiven, elitePowersChecked, avatarChance, moleChance, wolfPowersGiven, wolfPowersChecked, secondaryRolesGiven, secondaryRolesChecked, potionsChecked, itemsChecked);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Update_Options_Display error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Update_Options_Display(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Update_Options_Display_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int wolvesCount = *(int*)(data + num);
		num += 4;
		int traitorsCount = *(int*)(data + num);
		num += 4;
		int wolfPupsCount = *(int*)(data + num);
		num += 4;
		int soloRolesGiven = *(int*)(data + num);
		num += 4;
		int soloRolesChecked = *(int*)(data + num);
		num += 4;
		int jobsChance = *(int*)(data + num);
		num += 4;
		int jobsChecked = *(int*)(data + num);
		num += 4;
		int elitePowersGiven = *(int*)(data + num);
		num += 4;
		int elitePowersChecked = *(int*)(data + num);
		num += 4;
		int avatarChance = *(int*)(data + num);
		num += 4;
		int moleChance = *(int*)(data + num);
		num += 4;
		int wolfPowersGiven = *(int*)(data + num);
		num += 4;
		int wolfPowersChecked = *(int*)(data + num);
		num += 4;
		int secondaryRolesGiven = *(int*)(data + num);
		num += 4;
		int secondaryRolesChecked = *(int*)(data + num);
		num += 4;
		int potionsChecked = *(int*)(data + num);
		num += 4;
		int itemsChecked = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Update_Options_Display(runner, wolvesCount, traitorsCount, wolfPupsCount, soloRolesGiven, soloRolesChecked, jobsChance, jobsChecked, elitePowersGiven, elitePowersChecked, avatarChance, moleChance, wolfPowersGiven, wolfPowersChecked, secondaryRolesGiven, secondaryRolesChecked, potionsChecked, itemsChecked);
	}

	[Rpc]
	public unsafe static void Rpc_Request_Unstuck(NetworkRunner runner, int playerIndex)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Request_Unstuck(Fusion.NetworkRunner,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (runner.IsServer)
			{
				Vector3 val = default(Vector3);
				((Vector3)(ref val))._002Ector(214.03f, 30.27f, 180.11f);
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
				if (Vector3.Distance(((Component)player.PlayerController).transform.position, val) <= 0.1f)
				{
					player.PlayerController.CharacterMovementHandler.TeleportData = new NetworkTeleportData(new Vector3(215.03f, 30.27f, 180.11f), Quaternion.identity, false);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Request_Unstuck error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Request_Unstuck(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Request_Unstuck_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Request_Unstuck(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Add_Game_Summary_Kill(NetworkRunner runner, int killerIndex, int victimIndex, int deathType)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Invalid comparison between Unknown and I4
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 32;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Add_Game_Summary_Kill(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = killerIndex;
					num2 += 4;
					*(int*)(data + num2) = victimIndex;
					num2 += 4;
					*(int*)(data + num2) = deathType;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 12;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom playerCustom = ((killerIndex != -1) ? PlayerCustomRegistry.GetPlayer(killerIndex) : null);
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(victimIndex);
			Color killerColor = (((Object)(object)playerCustom != (Object)null) ? UILastGameSummaryKill.GetColorForPlayer(playerCustom) : Color.black);
			Color colorForPlayer = UILastGameSummaryKill.GetColorForPlayer(player);
			LastGameSummaryKill.LastGameSummaryKillTiming timing = (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) ? LastGameSummaryKill.LastGameSummaryKillTiming.Night : (((int)GameManager.LocalGameState == 4) ? LastGameSummaryKill.LastGameSummaryKillTiming.Meeting : LastGameSummaryKill.LastGameSummaryKillTiming.Day));
			UIManager.LastGameSummaryPanel.AddPlayerKill(new LastGameSummaryKill(timing, GameManagerCustom.Instance.CurrentDay, ((Object)(object)playerCustom != (Object)null) ? ((object)playerCustom.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() : null, killerColor, PlayerStats.DeathTypeIntToString(deathType), ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString(), colorForPlayer));
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Add_Game_Summary_Kill error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Add_Game_Summary_Kill(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Add_Game_Summary_Kill_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int killerIndex = *(int*)(data + num);
		num += 4;
		int victimIndex = *(int*)(data + num);
		num += 4;
		int deathType = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Add_Game_Summary_Kill(runner, killerIndex, victimIndex, deathType);
	}
}
