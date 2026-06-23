using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(NetworkRunner), "OnDestroy")]
internal class GlobalManagerNetworkRunnerOnDestroyUnloadPrefabsPatch
{
	private static void Prefix(NetworkRunner __instance)
	{
		try
		{
			NetworkObjectService.Instance.Clear();
			NetworkProjectConfig.UnloadGlobal();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GlobalManagerNetworkRunnerOnDestroyUnloadPrefabsPatch Awake error: " + ex));
		}
	}
}
