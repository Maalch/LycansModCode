using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "CheckItemRayCast")]
internal class CheckItemPatch
{
	private static bool Prefix(Item targetItem, PlayerController __instance, ref bool __result)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.Tiny) || NetworkBool.op_Implicit(player.Phasing))
			{
				__result = false;
				return false;
			}
			if (targetItem is MagicScrollItem magicScrollItem)
			{
				GameManager.Instance.gameUI.UpdateUsername(TranslationManager.Instance.GetTranslation("NALES_ITEM_SCROLL").Replace("#EFFECT", TranslationManager.Instance.GetTranslation(magicScrollItem.Effect.GetTranslateKey())));
				GameManager.Instance.gameUI.ShowUsername(true);
			}
			else if (targetItem is PhasingDiamondItem)
			{
				GameManager.Instance.gameUI.UpdateUsername(TranslationManager.Instance.GetTranslation("NALES_ITEM_DIAMOND"));
				GameManager.Instance.gameUI.ShowUsername(true);
			}
			if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Saboteur && !NetworkBool.op_Implicit(__instance.IsWolf))
			{
				ItemCustom componentInChildren = ((Component)targetItem).GetComponentInChildren<ItemCustom>();
				if (NetworkBool.op_Implicit(componentInChildren.Sabotaged))
				{
					GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_ITEM_ALREADY_SABOTAGED", Color.white, (InputActionName)0, Array.Empty<object>());
					__result = true;
					return false;
				}
				if (targetItem is Potion)
				{
					GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_ITEM_POISON", Color.green, (InputActionName)0, Array.Empty<object>());
				}
				else
				{
					GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_ITEM_TRAP", Color.red, (InputActionName)0, Array.Empty<object>());
				}
				__result = true;
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CheckItemPatch prefix error: " + ex));
			return true;
		}
	}

	private static void Postfix(Item targetItem, ref bool __result, PlayerController __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (targetItem.Owner == PlayerRef.None && targetItem is Potion && NetworkBool.op_Implicit(__instance.IsWolf) && PlayerCustomRegistry.GetPlayer(__instance.Ref).SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothAlcoholic)
			{
				GameManager.Instance.gameUI.UpdateInteraction("UI_LOOT_ITEM", Color.white, (InputActionName)3, Array.Empty<object>());
				__result = true;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CheckItemPatch postfix error: " + ex));
		}
	}
}
