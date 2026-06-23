using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdateAlchemistsCountSetting")]
internal class SendRefreshToClientsPatch3
{
	private static void Postfix(GameManager __instance)
	{
		if (((SimulationBehaviour)__instance).Runner.IsServer)
		{
			UIOptionsDisplayPanel.SendRefreshToClients();
		}
	}
}
