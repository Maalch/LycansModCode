using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LightingManager), "UpdateAmbient")]
internal class LightingManagerUpdateAmbientPatch
{
	private static void Postfix(LightingManager __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		LightingManagerFixedUpdateNetworkPatch.TimeOfDayAccurate = (NetworkBool.op_Implicit(__instance.IsNight) ? 20 : 8);
	}
}
