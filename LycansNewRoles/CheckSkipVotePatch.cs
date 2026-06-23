using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "CheckSkipVote")]
internal class CheckSkipVotePatch
{
	private static bool Prefix(GameManager __instance)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).Runner.IsServer)
			{
				List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (!NetworkBool.op_Implicit(o.Downed) || !NetworkBool.op_Implicit(o.PoliticianVictimAlltime)) && !NetworkBool.op_Implicit(o.Kidnapped)).ToList();
				int num = list.Count((PlayerCustom o) => o.PlayerController.IdVoted != -1);
				if (list.Count == num && NetworkBool.op_Implicit(__instance.CanVote))
				{
					__instance.VoteTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, NetworkBool.op_Implicit(Plugin.CustomConfig.AnonymousVotes) ? 10f : 4f);
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CheckSkipVotePatch: " + ex));
			return true;
		}
	}
}
