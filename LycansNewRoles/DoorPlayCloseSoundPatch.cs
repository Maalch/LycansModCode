using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Door), "PlayCloseSound")]
internal class DoorPlayCloseSoundPatch
{
	private static bool Prefix(Door __instance)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.State.Current == 2)
		{
			AudioManager.PlayPosition("DOOR_CLOSE", ((Component)__instance).transform.position, (MixerTarget)2, 20f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID), 0.2f);
		}
		return false;
	}
}
