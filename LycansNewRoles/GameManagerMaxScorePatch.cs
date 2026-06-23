using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdateMaxScoreSetting")]
internal class GameManagerMaxScorePatch
{
	private static bool Prefix(GameManager __instance, int value)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		try
		{
			if (((SimulationBehaviour)__instance).Runner.IsServer && (int)GameManager.State.Current == 1)
			{
				Traverse.Create((object)__instance).Property<int>("MaxScore", (object[])null).Value = value;
				Traverse.Create(typeof(GameManager)).Method("Rpc_UpdateScoreSettings", new List<Type> { typeof(NetworkRunner) }.ToArray(), (object[])null).GetValue(new object[1] { ((SimulationBehaviour)__instance).Runner });
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameManagerMaxScorePatch exception: " + ex));
			return true;
		}
	}
}
