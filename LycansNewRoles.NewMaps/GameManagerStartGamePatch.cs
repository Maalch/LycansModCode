using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameManager), "StartGame")]
internal class GameManagerStartGamePatch
{
	private static void Prefix(GameManager __instance)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		try
		{
			if (((SimulationBehaviour)__instance).Runner.IsServer && (int)GameManager.State.Current == 1)
			{
				MapManager.UpdateMapByPlayersAmount();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameManagerStartGamePatch error: " + ex));
		}
	}
}
