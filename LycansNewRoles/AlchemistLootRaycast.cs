using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using Managers;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "CheckLootRayCast")]
internal class AlchemistLootRaycast
{
	private static bool Prefix(PlayerController __instance, Loot targetLoot, ref bool __result)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.Tiny) || NetworkBool.op_Implicit(player.Phasing))
			{
				__result = false;
				return false;
			}
			if (NetworkBool.op_Implicit(targetLoot.Available) && !NetworkBool.op_Implicit(__instance.IsWolf) && player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Alchemist && player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials)
			{
				GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_ALCHEMIZE_LOOT", targetLoot.RarityColor, new List<InputActionName>
				{
					(InputActionName)3,
					(InputActionName)4
				}, new object[1] { targetLoot.ScoreValue });
				__result = true;
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TinyEffectPreventRaycastLootPatch error: " + ex));
			return true;
		}
	}
}
