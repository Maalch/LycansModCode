using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Door), "PlayOpenSound")]
internal class DoorPlayOpenSoundPatch
{
	private static bool Prefix(Door __instance)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		AudioManager.PlayPosition("DOOR_OPEN", ((Component)__instance).transform.position, (MixerTarget)2, 20f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID), 0.2f);
		return false;
	}
}
