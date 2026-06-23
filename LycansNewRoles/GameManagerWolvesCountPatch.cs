using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdateWolvesCountSetting")]
internal class GameManagerWolvesCountPatch
{
	private static bool Prefix(GameManager __instance, int value)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		try
		{
			if (((SimulationBehaviour)__instance).Runner.IsServer && (int)GameManager.State.Current == 1)
			{
				Traverse.Create((object)__instance).Property<int>("WolvesCount", (object[])null).Value = value;
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameManagerWolvesCountPatch exception: " + ex));
			return true;
		}
	}
}
