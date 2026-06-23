using System;
using System.Diagnostics;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(NetworkRunner), "Disconnect", new Type[] { typeof(PlayerRef) })]
internal class DisconnectTest
{
	private unsafe static void Prefix(PlayerRef player, NetworkRunner __instance)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (__instance.IsServer)
			{
				StackTrace stackTrace = new StackTrace();
				PlayerRef val = player;
				LycansUtility.DebugLog("Disconnecting player: " + ((object)(*(PlayerRef*)(&val))/*cast due to constrained. prefix*/).ToString() + ", stacktrace: " + stackTrace);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DisconnectTest error: " + ex));
		}
	}
}
