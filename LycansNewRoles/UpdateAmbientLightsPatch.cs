using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LightingManager), "UpdateAmbientLights")]
internal class UpdateAmbientLightsPatch
{
	private static void Postfix(LightingManager __instance)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		foreach (LanternCustom value in LanternCustom.LanternCustomsByLight.Values)
		{
			value.On = NetworkBool.op_Implicit(__instance.IsNight);
		}
	}
}
