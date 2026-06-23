using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewPrimaryRoles;

[HarmonyPatch(typeof(SatiatedEffect), "ApplyEffectToPlayerSpecific")]
internal class SatiatedEffectOnLoverPatch
{
	private static bool Prefix(SatiatedEffect __instance, PlayerRef targetPlayer)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Invalid comparison between Unknown and I4
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).HasStateAuthority)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(targetPlayer);
				if ((Object)(object)player != (Object)null && player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && (int)player.PlayerController.Role == 0)
				{
					player.PlayerController.PlayerEffectManager.Satiated = NetworkBool.op_Implicit(true);
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SatiatedEffectOnLoverPatch error: " + ex));
			return true;
		}
	}
}
