using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "EndVote")]
internal class StandOnEndVotePatch
{
	private static void Postfix(GameManager __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(__instance.IsFinished))
			{
				return;
			}
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.PlayerAnimations.SetLoopAnimation("HumanM@Question01", active: false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("StandOnEndVotePatch: " + ex));
		}
	}
}
