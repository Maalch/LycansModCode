using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Effect), "Init")]
internal class BeastReducedNegativeEffectsDurationPatch
{
	private static bool Prefix(PlayerRef targetPlayer, Effect __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			PlayerCustom.ApplyEffectToPlayer(player, __instance, ((SimulationBehaviour)__instance).Runner);
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("BeastReducedNegativeEffectsDurationPatch error: " + ex));
			return true;
		}
	}
}
