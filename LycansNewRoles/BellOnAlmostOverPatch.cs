using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "FixedUpdateNetwork")]
internal class BellOnAlmostOverPatch
{
	public static bool BellSounded;

	private static void Postfix(GameManager __instance)
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			TickTimer voteTimer;
			if (((SimulationBehaviour)__instance).Runner.IsServer)
			{
				voteTimer = __instance.VoteTimer;
				if (((TickTimer)(ref voteTimer)).IsRunning)
				{
					voteTimer = __instance.VoteTimer;
					if (!((TickTimer)(ref voteTimer)).Expired(((SimulationBehaviour)__instance).Runner))
					{
						voteTimer = __instance.VoteTimer;
						if (((TickTimer)(ref voteTimer)).RemainingTime(((SimulationBehaviour)__instance).Runner) <= 10f && !BellSounded)
						{
							BellSounded = true;
							GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("BELL"), ((Component)PlayerController.Local.LocalCameraHandler.PovPlayer).transform.position, 100f, 1f);
						}
					}
				}
			}
			voteTimer = __instance.VoteTimer;
			if (((TickTimer)(ref voteTimer)).IsRunning)
			{
				voteTimer = __instance.VoteTimer;
				if (!((TickTimer)(ref voteTimer)).Expired(((SimulationBehaviour)__instance).Runner))
				{
					GameUI gameUI = GameManager.Instance.gameUI;
					voteTimer = __instance.VoteTimer;
					gameUI.UpdateTimer((int)((TickTimer)(ref voteTimer)).RemainingTime(((SimulationBehaviour)__instance).Runner).Value);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("BellOnAlmostOverPatch: " + ex));
		}
	}
}
