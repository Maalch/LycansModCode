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
		//IL_1879: Unknown result type (might be due to invalid IL or missing references)
		//IL_187e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1883: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_178d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1792: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1050: Unknown result type (might be due to invalid IL or missing references)
		//IL_1056: Invalid comparison between Unknown and I4
		//IL_123f: Unknown result type (might be due to invalid IL or missing references)
		//IL_179e: Unknown result type (might be due to invalid IL or missing references)
		//IL_180b: Unknown result type (might be due to invalid IL or missing references)
		//IL_105d: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a7: Invalid comparison between Unknown and I4
		//IL_124c: Unknown result type (might be due to invalid IL or missing references)
		//IL_140d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1710: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_116d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1104: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1214: Unknown result type (might be due to invalid IL or missing references)
		//IL_11aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1271: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_131b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1303: Unknown result type (might be due to invalid IL or missing references)
		//IL_1435: Unknown result type (might be due to invalid IL or missing references)
		//IL_172d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Invalid comparison between Unknown and I4
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_1111: Unknown result type (might be due to invalid IL or missing references)
		//IL_1073: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_139b: Unknown result type (might be due to invalid IL or missing references)
		//IL_15dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1505: Unknown result type (might be due to invalid IL or missing references)
		//IL_1648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Invalid comparison between Unknown and I4
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_111e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1085: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1340: Unknown result type (might be due to invalid IL or missing references)
		//IL_1346: Unknown result type (might be due to invalid IL or missing references)
		//IL_1512: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_1130: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_147e: Unknown result type (might be due to invalid IL or missing references)
		//IL_145b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_1150: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1544: Unknown result type (might be due to invalid IL or missing references)
		//IL_168d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1379: Unknown result type (might be due to invalid IL or missing references)
		//IL_1556: Unknown result type (might be due to invalid IL or missing references)
		//IL_155b: Unknown result type (might be due to invalid IL or missing references)
		//IL_16af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_156c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1571: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Invalid comparison between Unknown and I4
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e70: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e17: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0967: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0974: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ceb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
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
				if (NetworkBool.op_Implicit(targetPlayerCustom.CapturedByCultist) && distance <= 2.5f && playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie)
				{
					return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.green, "NALES_UI_ACTION_FREE_CULTIST_VICTIM", Array.Empty<object>());
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
					if (distance < 10f && NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && targetPlayerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && playerCustom.PrimaryRolePowerRemainingUses > 0)
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.magenta, "NALES_UI_ACTION_REANIMATE", Array.Empty<object>());
					}
					if (distance < 2.5f && !NetworkBool.op_Implicit(targetPlayer.IsDead) && targetPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayerCustom.Strengthened))
					{
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2.5f, Color.green, "NALES_UI_ACTION_VOODOO_STRENGTHEN_ZOMBIE", Array.Empty<object>());
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
					if (playerCustom.PrimaryRolePowerRemainingUses > 0 && playerCustom.PrimaryRoleTargetRef == PlayerRef.None && !NetworkBool.op_Implicit(targetPlayerCustom.Kidnapped) && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayerCustom.Dying) && distance < 5f)
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
				case PlayerCustom.PlayerPrimaryRolePower.Inventor:
					if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials && !NetworkBool.op_Implicit(playerController.IsWolf) && !NetworkBool.op_Implicit(targetPlayer.IsDead) && !NetworkBool.op_Implicit(targetPlayer.IsWolf) && !NetworkBool.op_Implicit(targetPlayerCustom.Dying) && distance < 2f)
					{
						if (targetPlayerCustom.InventorDeviceRef == playerCustom.Ref)
						{
							return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2f, Color.red, "NALES_UI_ACTION_PLACE_OBFUSCATOR_ALREADY_ACTIVE", Array.Empty<object>());
						}
						return new SingleInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, 2f, Color.green, "NALES_UI_ACTION_PLACE_OBFUSCATOR", Array.Empty<object>());
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
