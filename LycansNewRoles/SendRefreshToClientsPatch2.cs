using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdateHuntersCountSetting")]
internal class SendRefreshToClientsPatch2
{
	private static void Postfix(GameManager __instance)
	{
		if (((SimulationBehaviour)__instance).Runner.IsServer)
		{
			UIOptionsDisplayPanel.SendRefreshToClients();
		}
	}
}
