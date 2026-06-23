using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Feed")]
internal class SoloRolesMoreFeedPatch
{
	private static void Prefix(ref int value, PlayerController __instance)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Invalid comparison between Unknown and I4
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Invalid comparison between Unknown and I4
		if (((SimulationBehaviour)__instance).Object.HasStateAuthority)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Eclipse)
			{
				value = Mathf.RoundToInt((float)value * 2f);
			}
			else if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothMetabolic && (int)__instance.Role != 1)
			{
				value = Mathf.RoundToInt((float)value * 1.4f);
			}
			if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && (int)__instance.Role != 1)
			{
				value = 0;
			}
		}
	}
}
