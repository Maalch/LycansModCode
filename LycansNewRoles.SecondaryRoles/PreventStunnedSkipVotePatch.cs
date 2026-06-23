using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.Stats;

namespace LycansNewRoles.SecondaryRoles;

[HarmonyPatch(typeof(PlayerController), "Rpc_SkipVote")]
internal class PreventStunnedSkipVotePatch
{
	private static bool Prefix(PlayerController __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.Stunned) || NetworkBool.op_Implicit(player.Downed) || NetworkBool.op_Implicit(player.Kidnapped))
			{
				return false;
			}
			if (player.PlayerController.IdVoted == -1 && ((SimulationBehaviour)player).Runner.IsServer)
			{
				player.Stats.Votes.Add(new PlayerStats.VoteStat("Passé", LycansUtility.GetFormattedCurrentDateTimeUtc));
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PreventStunnedSkipVotePatch error: " + ex));
			return true;
		}
	}
}
