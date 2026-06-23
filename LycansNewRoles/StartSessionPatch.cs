using System;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(NetworkRunnerHandler), "StartSession")]
internal class StartSessionPatch
{
	private static void Prefix(ref int playerCount)
	{
		try
		{
			playerCount = 15;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreateGamePatch error: " + ex));
		}
	}
}
