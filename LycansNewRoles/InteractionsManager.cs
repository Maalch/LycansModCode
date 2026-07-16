using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BepInEx.Logging;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace LycansNewRoles;

public static class InteractionsManager
{
	private static TextMeshProUGUI _interactionText;

	private static Shortcut _interactionShortcut;

	private static LocalizeStringEvent _localizeStringEvent;

	private static string _previousKey;

	private static object[] _previousItems = new object[0];

	private static List<string> _previousActions;

	public static bool NormalInteractionAvailable = false;

	public static void UpdateWithInteractions(List<SingleInteraction> interactions)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			NormalInteractionAvailable = interactions.Any((SingleInteraction o) => o.ActionType == SingleInteraction.SingleInteractionType.NormalInteraction);
			SingleInteraction singleInteraction = interactions.First();
			string translationKey = singleInteraction.TranslationKey;
			object[] textArguments = singleInteraction.TextArguments;
			string item = "";
			switch (singleInteraction.ActionType)
			{
			case SingleInteraction.SingleInteractionType.NormalInteraction:
				item = ((object)(InputActionName)3/*cast due to constrained. prefix*/).ToString();
				break;
			case SingleInteraction.SingleInteractionType.SecondaryInteraction:
				item = ((object)(InputActionName)4/*cast due to constrained. prefix*/).ToString();
				break;
			case SingleInteraction.SingleInteractionType.SecondaryRoleInteraction:
				item = "SECONDARYROLEPOWER";
				break;
			case SingleInteraction.SingleInteractionType.ItemInteraction:
				item = ((object)(InputActionName)11/*cast due to constrained. prefix*/).ToString();
				break;
			case SingleInteraction.SingleInteractionType.SecondaryItemInteraction:
				item = "ITEMSECONDARY";
				break;
			case SingleInteraction.SingleInteractionType.AccessoryInteraction:
				item = "ACCESSORYACTION";
				break;
			}
			UpdateInteraction(translationKey, singleInteraction.ActionColor, new List<string> { item }, textArguments);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateWithInteractions error: " + ex));
		}
	}

	public static void UpdateInteraction(string key, Color color, List<string> actions, object[] items)
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_interactionText == (Object)null)
		{
			_interactionText = Traverse.Create((object)GameManager.Instance.gameUI).Field<TextMeshProUGUI>("interactionText").Value;
		}
		if ((Object)(object)_interactionShortcut == (Object)null)
		{
			_interactionShortcut = Traverse.Create((object)GameManager.Instance.gameUI).Field<Shortcut>("interactionShortcut").Value;
		}
		if ((Object)(object)_localizeStringEvent == (Object)null)
		{
			_localizeStringEvent = Traverse.Create((object)_interactionShortcut).Field<LocalizeStringEvent>("_localizeStringEvent").Value;
		}
		((Graphic)_interactionText).color = color;
		bool flag = key != _previousKey;
		bool flag2 = !Enumerable.SequenceEqual(_previousItems, items);
		if (flag || flag2)
		{
			_previousKey = key;
			_previousActions = actions;
			_previousItems = items;
			List<object> list = new List<object>();
			foreach (string action in actions)
			{
				string item = "";
				if (action != "None")
				{
					item = LycansUtility.GetInputDisplayCustom(InputManagerExtra.Instance.GetAction(action));
				}
				list.Add(item);
			}
			foreach (object item2 in items)
			{
				list.Add(item2);
			}
			_localizeStringEvent.StringReference.Arguments = list.ToArray();
			if (flag)
			{
				if (key != "")
				{
					((LocalizedReference)_localizeStringEvent.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit(key));
					_localizeStringEvent.StringReference.RefreshString();
				}
				else
				{
					_localizeStringEvent.StringReference.Clear();
				}
			}
		}
		((Component)_interactionShortcut).gameObject.SetActive(true);
	}

	public static SingleInteraction GetInteraction(SingleInteraction.SingleInteractionType type, PlayerController playerController, PlayerCustom playerCustom, PlayerController targetPlayer, PlayerCustom targetPlayerCustom, float distance)
	{
		//IL_16f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1703: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_1663: Unknown result type (might be due to invalid IL or missing references)
		//IL_1566: Unknown result type (might be due to invalid IL or missing references)
		//IL_160d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed6: Invalid comparison between Unknown and I4
		//IL_10bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_161e: Unknown result type (might be due to invalid IL or missing references)
		//IL_168b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1021: Unknown result type (might be due to invalid IL or missing references)
		//IL_1027: Invalid comparison between Unknown and I4
		//IL_10cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_128d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1590: Unknown result type (might be due to invalid IL or missing references)
		//IL_163b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1094: Unknown result type (might be due to invalid IL or missing references)
		//IL_102a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1147: Unknown result type (might be due to invalid IL or missing references)
		//IL_112d: Unknown result type (might be due to invalid IL or missing references)
		//IL_119b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1183: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Invalid comparison between Unknown and I4
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1037: Unknown result type (might be due to invalid IL or missing references)
		//IL_121b: Unknown result type (might be due to invalid IL or missing references)
		//IL_145c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1420: Unknown result type (might be due to invalid IL or missing references)
		//IL_1385: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Invalid comparison between Unknown and I4
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1049: Unknown result type (might be due to invalid IL or missing references)
		//IL_1258: Unknown result type (might be due to invalid IL or missing references)
		//IL_1235: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1392: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_1069: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_12db: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1077: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_150d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13db: Unknown result type (might be due to invalid IL or missing references)
		//IL_152f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Invalid comparison between Unknown and I4
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Invalid comparison between Unknown and I4
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0904: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			TickTimer val;
			switch (type)
			{
			case SingleInteraction.SingleInteractionType.NormalInteraction:
				if (NetworkBool.op_Implicit(playerController.IsWolf))
				{
					if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
					{
						if (distance < 3f && playerController.IsCanMove() && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayerCustom.Dying) && !NetworkBool.op_Implicit(targetPlayerCustom.Petrified))
						{
							if (NetworkBool.op_Implicit(targetPlayerCustom.Angel))
							{
								return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 3f, Color.red, "NALES_UI_ACTION_TARGET_PROTECTED", Array.Empty<object>());
							}
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 3f, Color.white, "UI_KILL", Array.Empty<object>());
						}
					}
					else if (distance < 1.75f && playerController.IsCanMove() && !NetworkBool.op_Implicit(targetPlayer.IsDead) && (int)playerController.Role == 1 && ((int)targetPlayer.Role != 1 || (playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && NetworkBool.op_Implicit(targetPlayer.IsWolf))) && !NetworkBool.op_Implicit(targetPlayerCustom.Dying) && !NetworkBool.op_Implicit(targetPlayerCustom.Petrified))
					{
						if (NetworkBool.op_Implicit(targetPlayerCustom.Angel))
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 1.75f, Color.red, "NALES_UI_ACTION_TARGET_PROTECTED", Array.Empty<object>());
						}
						if (targetPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Mole)
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 1.75f, Color.white, "NALES_UI_ACTION_ATTACK_MOLE", Array.Empty<object>());
						}
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 1.75f, Color.white, "UI_KILL", Array.Empty<object>());
					}
				}
				if (targetPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary && NetworkBool.op_Implicit(targetPlayerCustom.NewPrimaryRoleUniqueBool) && distance <= 2.5f && !NetworkBool.op_Implicit(targetPlayerCustom.PlayerController.IsDead))
				{
					return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.red, "NALES_UI_ACTION_ELIMINATE_AGENT", Array.Empty<object>());
				}
				if (NetworkBool.op_Implicit(targetPlayerCustom.Dying) && distance <= 2.5f && !NetworkBool.op_Implicit(playerController.IsWolf) && playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie)
				{
					return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.green, "NALES_UI_ACTION_SURVIVALIST_SAVE", Array.Empty<object>());
				}
				if (NetworkBool.op_Implicit(playerCustom.Assassin) && distance <= 3.5f && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible))
				{
					if (!NetworkBool.op_Implicit(targetPlayer.IsWolf) && GameManagerCustom.Instance.CurrentDay == 1 && (int)playerCustom.PlayerController.Role != 1 && playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None)
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 3.5f, Color.red, "NALES_UI_ACTION_ASSASSINATE_T1", Array.Empty<object>(), actionAvailable: false);
					}
					return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 3.5f, Color.red, "NALES_UI_ACTION_ASSASSINATE", Array.Empty<object>());
				}
				if (NetworkBool.op_Implicit(playerCustom.Midas) && distance <= 3f && !NetworkBool.op_Implicit(targetPlayer.IsDead))
				{
					return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 3f, Color.yellow, "NALES_UI_ACTION_MIDAS_PETRIFY", Array.Empty<object>());
				}
				if (playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent && targetPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent && NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && distance <= 2.5f && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) && !NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
				{
					return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.red, "NALES_UI_ACTION_ELIMINATE_AGENT", Array.Empty<object>());
				}
				if (NetworkBool.op_Implicit(playerCustom.BombActive) && !NetworkBool.op_Implicit(playerCustom.Panic) && distance < 2.5f && !NetworkBool.op_Implicit(targetPlayerCustom.BombActive) && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
				{
					return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.red, "NALES_UI_ACTION_GIVE_BOMB", Array.Empty<object>());
				}
				switch (playerCustom.NewPrimaryRole)
				{
				case PlayerCustom.PlayerNewPrimaryRole.VillageIdiot:
					if (playerCustom.PrimaryRolePowerRemainingUses <= 0 || NetworkBool.op_Implicit(playerController.IsWolf) || NetworkBool.op_Implicit(targetPlayer.IsDead) || NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
					{
						break;
					}
					switch (playerCustom.SoloRoleObjectiveTarget)
					{
					case 0:
						if (distance < 10f && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 10f, Color.red, "NALES_UI_ACTION_CURSE", Array.Empty<object>());
						}
						break;
					case 1:
						if (distance < 6f && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 6f, Color.red, "NALES_UI_ACTION_CREATE_BOMB", Array.Empty<object>());
						}
						break;
					case 2:
						if (!(distance < 6f) || !((Object)(object)targetPlayer.Item != (Object)null) || NetworkBool.op_Implicit(((Component)targetPlayer.Item).GetComponentInChildren<ItemCustom>().Sabotaged))
						{
							break;
						}
						val = targetPlayer.Item.TriggerTimer;
						if (!((TickTimer)(ref val)).IsRunning)
						{
							val = targetPlayer.Item.AnimationTimer;
							if (!((TickTimer)(ref val)).IsRunning && !NetworkBool.op_Implicit(targetPlayer.IsZooming))
							{
								return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 6f, Color.red, "NALES_UI_ACTION_ITEM_TRAP_HELD_ITEM", Array.Empty<object>());
							}
						}
						break;
					}
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Beast:
					if (playerCustom.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(targetPlayerCustom.BeastMark) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && distance < 7f)
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 7f, Color.magenta, "NALES_UI_ACTION_BEAST_MARK", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Voodoo:
					if (distance < 10f && NetworkBool.op_Implicit(targetPlayer.IsDead) && (int)targetPlayer.Role != 1 && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && targetPlayerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && playerCustom.PrimaryRolePowerRemainingUses > 0)
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.magenta, "NALES_UI_ACTION_REANIMATE", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Zombie:
					if (distance < 2.5f && playerController.IsCanMove() && !NetworkBool.op_Implicit(targetPlayer.IsDead) && targetPlayerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Voodoo && targetPlayerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && !NetworkBool.op_Implicit(targetPlayerCustom.Dying) && !NetworkBool.op_Implicit(targetPlayerCustom.Petrified))
					{
						if (NetworkBool.op_Implicit(targetPlayer.IsWolf))
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.red, "NALES_UI_ACTION_ZOMBIE_STUN", Array.Empty<object>());
						}
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.red, "UI_KILL", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Kidnapper:
					if (playerCustom.PrimaryRolePowerRemainingUses > 0 && playerCustom.PrimaryRoleTargetRef == PlayerRef.None && !NetworkBool.op_Implicit(targetPlayerCustom.Kidnapped) && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayerCustom.Downed) && distance < 5f)
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryInteraction, 5f, Color.red, "NALES_UI_ACTION_KIDNAPPER_ABDUCT", Array.Empty<object>());
					}
					break;
				}
				switch (playerCustom.PrimaryRolePower)
				{
				case PlayerCustom.PlayerPrimaryRolePower.Necromancer:
					if (!NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool) && distance < 2.5f && targetPlayerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && playerController.IsCanMove() && NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.red, "NALES_UI_ACTION_RESURRECT", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Deceiver:
					if (playerCustom.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(targetPlayerCustom.DeceiverTrickAllTime) && distance < 10f && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 10f, Color.red, "NALES_UI_ACTION_DECEIVER_TRICK", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Warlock:
					if (playerCustom.PrimaryRolePowerRemainingUses > 0 && distance < 10f && !NetworkBool.op_Implicit(targetPlayerCustom.CurseDormant) && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 10f, Color.red, "NALES_UI_ACTION_CURSE", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Possessor:
				{
					if (!(distance < 5f))
					{
						break;
					}
					PlayerRef primaryRoleTargetRef = playerCustom.PrimaryRoleTargetRef;
					if ((((PlayerRef)(ref primaryRoleTargetRef)).IsNone || playerCustom.PrimaryRoleTargetRef != targetPlayer.Ref) && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						if (targetPlayerCustom.AlreadyPossessed)
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 5f, Color.red, "NALES_UI_ACTION_POSSESSOR_IMPOSSIBLE", Array.Empty<object>());
						}
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 5f, Color.yellow, "NALES_UI_ACTION_POSSESSOR_MARK", Array.Empty<object>());
					}
					break;
				}
				case PlayerCustom.PlayerPrimaryRolePower.Saboteur:
					if (playerCustom.PrimaryRolePowerCurrentMaterials < playerCustom.PowerMaterialsInfo.RequiredMaterials || !(distance < 6f) || NetworkBool.op_Implicit(playerController.IsWolf) || NetworkBool.op_Implicit(targetPlayer.IsDead) || NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia) || !((Object)(object)targetPlayer.Item != (Object)null) || NetworkBool.op_Implicit(((Component)targetPlayer.Item).GetComponentInChildren<ItemCustom>().Sabotaged))
					{
						break;
					}
					val = targetPlayer.Item.TriggerTimer;
					if (!((TickTimer)(ref val)).IsRunning)
					{
						val = targetPlayer.Item.AnimationTimer;
						if (!((TickTimer)(ref val)).IsRunning && !NetworkBool.op_Implicit(targetPlayer.IsZooming))
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 6f, Color.red, "NALES_UI_ACTION_ITEM_TRAP_HELD_ITEM", Array.Empty<object>());
						}
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Bomber:
					if (playerCustom.PrimaryRolePowerRemainingUses > 0 && distance < 6f && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 6f, Color.red, "NALES_UI_ACTION_CREATE_BOMB", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Avenger:
					if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials && distance < 3.5f && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 3.5f, Color.red, "NALES_UI_ACTION_AVENGER_ATTACK", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Survivalist:
					if (playerCustom.PrimaryRolePowerRemainingUses > 0 && distance < 2.5f && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.green, "NALES_UI_ACTION_SURVIVALIST_BUFF", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Priest:
					if (playerCustom.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && distance < 2.5f && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.green, "NALES_UI_ACTION_PROTECT", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Investigator:
					if (distance < 3f && playerCustom.PrimaryRoleTargetRef == targetPlayer.Ref && !NetworkBool.op_Implicit(targetPlayer.IsWolf))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 3f, Color.yellow, "NALES_UI_ACTION_INVESTIGATE", Array.Empty<object>());
					}
					break;
				}
				break;
			case SingleInteraction.SingleInteractionType.SecondaryRoleInteraction:
				switch (playerCustom.SecondaryRole)
				{
				case PlayerCustom.PlayerSecondaryRole.BothMetabolic:
					if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && (int)playerController.Role == 1 && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && distance < 10f && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) && !NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 10f, StarvationActiveEffect.EffectColor, "NALES_UI_ACTION_STARVE", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothPolitician:
					if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && !NetworkBool.op_Implicit(targetPlayerCustom.PoliticianVictimAlltime) && distance < 10f && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) && !NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 10f, Color.magenta, "NALES_UI_ACTION_DENY_VOTE", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothSherif:
					if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && distance < 3f && (int)playerController.Role != 1 && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) && !NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia) && !NetworkBool.op_Implicit(targetPlayerCustom.Petrified))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 3f, Color.red, "NALES_UI_ACTION_SHERIF_KILL", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothGambler:
					if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && distance < 5f)
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 5f, Color.white, "NALES_UI_ACTION_TELEPORT_OTHER", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothMedium:
					if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && distance < 2.5f && playerController.IsCanMove() && NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 2.5f, Color.white, "NALES_UI_ACTION_CONSULT", Array.Empty<object>());
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothScavenger:
					if (playerCustom.SecondaryRoleFirstRemainingUses <= 0 || !(distance < 2.5f) || !playerController.IsCanMove() || !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						break;
					}
					if (NetworkBool.op_Implicit(playerController.IsWolf))
					{
						PlayerCustom specificPrimaryRolePower = PlayerCustomRegistry.GetSpecificPrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Necromancer);
						if (((Object)(object)specificPrimaryRolePower != (Object)null && specificPrimaryRolePower.PrimaryRoleTargetRef == targetPlayer.Ref) || targetPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
						{
							break;
						}
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 2.5f, Color.green, "NALES_UI_ACTION_SCAVENGER_EAT", Array.Empty<object>());
					}
					if (!NetworkBool.op_Implicit(targetPlayerCustom.Scavenged))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 2.5f, Color.white, "NALES_UI_ACTION_SCAVENGER_SEARCH", Array.Empty<object>());
					}
					return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 2.5f, Color.white, "NALES_UI_ACTION_SCAVENGER_UNAVAILABLE", Array.Empty<object>(), actionAvailable: false);
				case PlayerCustom.PlayerSecondaryRole.BothBlueMage:
					if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && distance < 7f && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						Effect effect = EffectManager.GetEffect(playerCustom.SecondaryRoleUniqueInt);
						if (NetworkBool.op_Implicit(targetPlayer.IsWolf) && effect is AssassinEffect)
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 7f, Color.cyan, "NALES_UI_ACTION_BLUE_MAGE_INVALID", Array.Empty<object>(), actionAvailable: false);
						}
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 7f, Color.cyan, "NALES_UI_ACTION_BLUE_MAGE_CAST", new object[1] { TranslationManager.Instance.GetTranslation("NALES_UI_ACTION_BLUE_MAGE_CAST_SPELL").Replace("#EFFECT", TranslationManager.Instance.GetTranslation(effect.GetTranslateKey())) });
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothForger:
					if (playerCustom.SecondaryRoleFirstRemainingUses <= 0 || !((Object)(object)targetPlayer.Item != (Object)null) || !PlayerHeldItemComponent.CanSeeItem(playerCustom, targetPlayer.Item) || !(distance < 10f) || NetworkBool.op_Implicit(targetPlayer.IsWolf) || NetworkBool.op_Implicit(targetPlayer.IsDead) || NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
					{
						break;
					}
					val = targetPlayer.Item.TriggerTimer;
					if (((TickTimer)(ref val)).IsRunning)
					{
						break;
					}
					val = targetPlayer.Item.AnimationTimer;
					if (!((TickTimer)(ref val)).IsRunning)
					{
						if (distance < 3f)
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 10f, Color.magenta, "NALES_UI_ACTION_STEAL_ITEM", new object[1] { TranslationManager.Instance.GetTranslation(ItemUtility.ItemToTranslateKey(targetPlayer.Item)) });
						}
						return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 3f, Color.magenta, "NALES_UI_ACTION_COPY_ITEM", new object[1] { TranslationManager.Instance.GetTranslation(ItemUtility.ItemToTranslateKey(targetPlayer.Item)) });
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothTinkerer:
					if ((playerCustom.Accessory is AccessoryRing || playerCustom.Accessory is AccessoryMagnifier) && playerCustom.SecondaryRoleFirstRemainingUses > 0 && !NetworkBool.op_Implicit(targetPlayer.IsDead) && distance < 10f)
					{
						Accessory accessory = playerCustom.Accessory;
						Accessory accessory2 = accessory;
						if (accessory2 is AccessoryRing)
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 10f, Color.white, "NALES_UI_ACTION_TINKERER_RING", Array.Empty<object>());
						}
						if (accessory2 is AccessoryMagnifier)
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, 10f, Color.red, "NALES_UI_ACTION_TINKERER_MAGNIFIER", Array.Empty<object>());
						}
					}
					break;
				}
				break;
			case SingleInteraction.SingleInteractionType.ItemInteraction:
				if ((!NetworkBool.op_Implicit(playerController.IsWolf) || NetworkBool.op_Implicit(Plugin.CustomConfig.WolvesCanUseItems)) && (Object)(object)playerController.Item != (Object)null && playerController.Item is MagicScrollItem magicScrollItem && !NetworkBool.op_Implicit(targetPlayer.IsDead))
				{
					return new SingleInteraction(SingleInteraction.SingleInteractionType.ItemInteraction, 10f, Color.cyan, "NALES_UI_ACTION_USE_SCROLL", new object[1] { TranslationManager.Instance.GetTranslation("NALES_ITEM_SCROLL").Replace("#EFFECT", TranslationManager.Instance.GetTranslation(magicScrollItem.Effect.GetTranslateKey())) });
				}
				break;
			case SingleInteraction.SingleInteractionType.AccessoryInteraction:
				if (playerCustom.Accessory is AccessorySpellbook accessorySpellbook)
				{
					val = ((Item)accessorySpellbook).ItemTimer;
					if (!((TickTimer)(ref val)).IsRunning && !NetworkBool.op_Implicit(targetPlayer.IsDead))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.AccessoryInteraction, 10f, Color.cyan, "NALES_UI_ACTION_USE_SPELLBOOK", Array.Empty<object>());
					}
				}
				break;
			}
			if (distance <= 2.5f && NetworkBool.op_Implicit(targetPlayer.IsDead) && (Object)(object)targetPlayer.Item != (Object)null)
			{
				return new SingleInteraction(SingleInteraction.SingleInteractionType.SecondaryInteraction, 2.5f, Color.yellow, "NALES_UI_ACTION_LOOT_CORPSE", new object[1] { TranslationManager.Instance.GetTranslation(ItemUtility.ItemToTranslateKey(targetPlayer.Item)) });
			}
			return null;
		}
		catch (Exception ex)
		{
			ManualLogSource logger = Plugin.Logger;
			string[] obj = new string[8]
			{
				"Error getting interaction ",
				type.ToString(),
				", for player ",
				null,
				null,
				null,
				null,
				null
			};
			NetworkString<_32> username = playerController.PlayerData.Username;
			obj[3] = ((object)username/*cast due to constrained. prefix*/).ToString();
			obj[4] = ", error: ";
			obj[5] = ex?.ToString();
			obj[6] = ", stacktrace: ";
			obj[7] = new StackTrace()?.ToString();
			logger.LogError((object)string.Concat(obj));
			return null;
		}
	}
}
