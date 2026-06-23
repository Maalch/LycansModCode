using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.Stats;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Rpc_VoteForPlayer")]
internal class DrunkVoteForPlayerPatch
{
	private static bool Prefix(PlayerRef voted, PlayerController __instance)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (((SimulationBehaviour)__instance).HasStateAuthority)
			{
				if (player.Stats != null)
				{
					PlayerController player2 = PlayerRegistry.GetPlayer(voted);
					if ((Object)(object)player2 != (Object)null && !NetworkBool.op_Implicit(player2.IsDead) && __instance.IdVoted == -1)
					{
						player.Stats.Votes.Add(new PlayerStats.VoteStat(((object)player2.PlayerData.Username/*cast due to constrained. prefix*/).ToString(), LycansUtility.GetFormattedCurrentDateTimeUtc));
					}
				}
				if (NetworkBool.op_Implicit(player.Confused) && Random.value < 0.65f && __instance.IdVoted == -1)
				{
					PlayerCustom playerCustom = CollectionsUtil.Grab<PlayerCustom>(PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld && o.Ref != __instance.Ref).ToList(), 1).First();
					PlayerController obj = __instance;
					PlayerRef val = playerCustom.Ref;
					obj.IdVoted = ((PlayerRef)(ref val)).PlayerId;
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DrunkVoteForPlayerPatch error: " + ex));
			return true;
		}
	}
}
