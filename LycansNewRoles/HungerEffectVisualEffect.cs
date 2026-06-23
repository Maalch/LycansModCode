using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "UpdateHungerBar")]
internal class HungerEffectVisualEffect
{
	private static void Postfix(GameUI __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)GameManager.LocalGameState == 2 && (Object)(object)PlayerController.Local != (Object)null)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
				if (NetworkBool.op_Implicit(player.Stunned) || NetworkBool.op_Implicit(player.Phasing))
				{
					GameManager.LightingManager.UpdateHungry(0.8f);
				}
				if (NetworkBool.op_Implicit(player.Possessed) || NetworkBool.op_Implicit(player.Hidden) || (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Peasant && NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool)) || (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Shadow && NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool)))
				{
					GameManager.LightingManager.UpdateHungry(0.5f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("StunnedEffectVisualEffect error: " + ex));
		}
	}
}
