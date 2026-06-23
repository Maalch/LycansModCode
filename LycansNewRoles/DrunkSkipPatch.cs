using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Rpc_SkipVote")]
internal class DrunkSkipPatch
{
	private static bool Prefix(PlayerController __instance)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).HasStateAuthority)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
				if (NetworkBool.op_Implicit(player.Confused) && Random.value < 0.65f)
				{
					PlayerController val = CollectionsUtil.Grab<PlayerController>(PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead) && o.Ref != __instance.Ref)).ToList(), 1).First();
					PlayerController obj = __instance;
					PlayerRef val2 = val.Ref;
					obj.IdVoted = ((PlayerRef)(ref val2)).PlayerId;
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DrunkSkipPatch error: " + ex));
			return true;
		}
	}
}
