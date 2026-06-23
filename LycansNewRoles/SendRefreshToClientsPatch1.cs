using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdateWolvesCountSetting")]
internal class SendRefreshToClientsPatch1
{
	private static void Postfix(GameManager __instance)
	{
		if (((SimulationBehaviour)__instance).Runner.IsServer)
		{
			UIOptionsDisplayPanel.SendRefreshToClients();
		}
	}
}
