using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameManager), "EndVote")]
internal class GameManagerEndVotePatch
{
	private static void Postfix(GameManager __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBool.op_Implicit(__instance.IsFinished))
			{
				MapManager.UpdateMapByPlayersAmount();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameManagerEndVotePatch error: " + ex));
		}
	}
}
