using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(ParanoiaEffect), "ApplyEffectToPlayerSpecific")]
internal class PreventParanoiaIfTruesightPatch
{
	private static bool Prefix(ParanoiaEffect __instance, PlayerRef targetPlayer)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).HasStateAuthority)
			{
				PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
				if ((Object)(object)player != (Object)null && NetworkBool.op_Implicit(player.PlayerEffectManager.NightVision))
				{
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ApplyEffectToPlayerSpecific error: " + ex));
			return true;
		}
	}
}
