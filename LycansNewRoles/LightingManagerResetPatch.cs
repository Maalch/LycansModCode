using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LightingManager), "Reset")]
internal class LightingManagerResetPatch
{
	private static void Postfix(LightingManager __instance)
	{
		LightingManagerFixedUpdateNetworkPatch.TimeOfDayAccurate = 8f;
	}
}
