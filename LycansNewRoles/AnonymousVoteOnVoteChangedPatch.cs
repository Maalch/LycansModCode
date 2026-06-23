using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnVoteChanged")]
internal class AnonymousVoteOnVoteChangedPatch
{
	private static bool Prefix(Changed<PlayerController> changed)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)Plugin.CustomConfig == (Object)null || !NetworkBool.op_Implicit(Plugin.CustomConfig.AnonymousVotes))
			{
				return true;
			}
			PlayerController behaviour = changed.Behaviour;
			if (((SimulationBehaviour)behaviour).HasInputAuthority)
			{
				GameManager.Instance.gameUI.ShowSkipVote(behaviour.IdVoted == -1);
			}
			((NetworkBehaviour)behaviour).CopyStateToBackingFields();
			Traverse<NetworkBool> val = Traverse.Create((object)behaviour).Field<NetworkBool>("_IsVoting");
			if (behaviour.IdVoted != -1)
			{
				if (behaviour.IdVoted >= 0)
				{
					val.Value = NetworkBool.op_Implicit(true);
					((NetworkBehaviour)behaviour).CopyBackingFieldsToState(true);
				}
				if (((SimulationBehaviour)behaviour).HasStateAuthority)
				{
					GameManager.Instance.CheckSkipVote();
				}
				List<PlayerController> list = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => !NetworkBool.op_Implicit(p.IsDead))).ToList();
				int num = list.Count((PlayerController p) => p.IdVoted != -1);
				if (list.Count == num)
				{
					PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => !NetworkBool.op_Implicit(p.IsDead))).ToList().ForEach(delegate(PlayerController p)
					{
						//IL_003a: Unknown result type (might be due to invalid IL or missing references)
						int count2 = PlayerRegistry.Where((Predicate<PlayerController>)delegate(PlayerController v)
						{
							//IL_000c: Unknown result type (might be due to invalid IL or missing references)
							//IL_0011: Unknown result type (might be due to invalid IL or missing references)
							int idVoted = v.IdVoted;
							PlayerRef val2 = p.Ref;
							return idVoted == ((PlayerRef)(ref val2)).PlayerId;
						}).ToList().Count;
						GameManager.Instance.gameUI.UpdatePlayerVotesCount(p.Ref, count2);
					});
					int count = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => !NetworkBool.op_Implicit(p.IsDead) && p.IdVoted == -2)).ToList().Count;
					GameManager.Instance.gameUI.UpdateSkippedVotesCount(count);
				}
				return false;
			}
			val.Value = NetworkBool.op_Implicit(false);
			((NetworkBehaviour)behaviour).CopyBackingFieldsToState(true);
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("AnonymousVoteOnVoteChangedPatch: " + ex));
			return true;
		}
	}
}
