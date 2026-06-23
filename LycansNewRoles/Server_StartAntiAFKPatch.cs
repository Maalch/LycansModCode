using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Server_StartAntiAFK")]
public class Server_StartAntiAFKPatch
{
	private static bool Prefix(GameManager __instance)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)__instance).Runner.IsServer)
		{
			if ((int)GameManager.LocalGameState != 1)
			{
				__instance.AntiAFK = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 5400f);
			}
			else
			{
				__instance.AntiAFK = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 1800f);
			}
			return false;
		}
		return true;
	}
}
