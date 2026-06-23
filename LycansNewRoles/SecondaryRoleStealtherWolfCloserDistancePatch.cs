using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems.Accessories;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(EffectManager), "WolfMusicDistance")]
internal class SecondaryRoleStealtherWolfCloserDistancePatch
{
	private static void Postfix(PlayerController wolfPlayer, ref float __result)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(wolfPlayer.Ref);
			if (NetworkBool.op_Implicit(player.Undetected) || NetworkBool.op_Implicit(player.Phasing) || (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothActor && NetworkBool.op_Implicit(player.SecondaryRolePowerActive)))
			{
				__result = 0f;
				return;
			}
			if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Predator)
			{
				if (player.PlayerController.MovementAction == 1)
				{
					__result = 5f;
				}
				else
				{
					__result = 30f;
				}
				if (NetworkBool.op_Implicit(player.Predator))
				{
					__result *= 0.75f;
				}
			}
			if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothAstral && NetworkBool.op_Implicit(player.SecondaryRolePowerActive))
			{
				__result *= 0.35f;
			}
			if ((Object)(object)player.Accessory != (Object)null && player.Accessory is AccessoryBoots)
			{
				__result *= 0.85f;
			}
			switch (player.CamouflageLevelForPovPlayer)
			{
			case 1:
				__result *= 0.9f;
				break;
			case 2:
				__result *= 0.8f;
				break;
			case 3:
				__result *= 0.7f;
				break;
			}
			__result *= BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleStealtherWolfCloserDistancePatch error: " + ex));
		}
	}
}
