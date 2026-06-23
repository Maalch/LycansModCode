using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerEffectsManager), "CheckFart")]
internal class MegaFartPatch
{
	private static bool Prefix(PlayerEffectsManager __instance)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)__instance).HasStateAuthority)
		{
			TickTimer fartTimer = __instance.FartTimer;
			if (((TickTimer)(ref fartTimer)).Expired(((SimulationBehaviour)__instance).Runner) && LycansUtility.GameActuallyInPlay)
			{
				PlayerController value = Traverse.Create((object)__instance).Field<PlayerController>("_playerController").Value;
				if (!((Component)value).GetComponent<KnockbackComponent>().Knockback.HasValue && Random.value < 0.0035f)
				{
					PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)__instance).Runner, value.Index, 4);
				}
			}
		}
		return true;
	}
}
