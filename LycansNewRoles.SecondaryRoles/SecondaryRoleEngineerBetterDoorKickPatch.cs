using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles.SecondaryRoles;

[HarmonyPatch(typeof(Door), "Rpc_Interact")]
internal class SecondaryRoleEngineerBetterDoorKickPatch
{
	private static void Postfix(PlayerRef actor, Door __instance)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Invalid comparison between Unknown and I4
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBehaviourUtils.InvokeRpc && (int)((SimulationBehaviour)__instance).Runner.Stage == 4)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(actor);
			if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothEngineer && NetworkBool.op_Implicit(__instance.IsLocked))
			{
				PlayerController player2 = PlayerRegistry.GetPlayer(actor);
				if (NetworkBool.op_Implicit(player2.IsKicking))
				{
					int num = (NetworkBool.op_Implicit(player2.IsWolf) ? 40 : 20);
					__instance.LockHp -= num;
					if (__instance.LockHp < 0)
					{
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("LOCK_BREAK"), ((Component)__instance).transform.position, 50f, 1f);
						__instance.IsLocked = NetworkBool.op_Implicit(false);
						__instance.IsMoving = NetworkBool.op_Implicit(true);
						__instance.IsOpen = NetworkBool.op_Implicit(true);
						Traverse.Create((object)__instance).Field<Animator>("_animator").Value.SetBool(Animator.StringToHash("Open"), true);
					}
				}
			}
			if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(__instance.IsLocked))
			{
				PlayerController player3 = PlayerRegistry.GetPlayer(actor);
				if (NetworkBool.op_Implicit(player3.IsKicking))
				{
					__instance.LockHp -= 50;
					if (__instance.LockHp < 0)
					{
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("LOCK_BREAK"), ((Component)__instance).transform.position, 50f, 1f);
						__instance.IsLocked = NetworkBool.op_Implicit(false);
						__instance.IsMoving = NetworkBool.op_Implicit(true);
						__instance.IsOpen = NetworkBool.op_Implicit(true);
						Traverse.Create((object)__instance).Field<Animator>("_animator").Value.SetBool(Animator.StringToHash("Open"), true);
					}
				}
			}
			Effect val = player.PlayerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is RecuperatingEffect);
			if ((Object)(object)val != (Object)null)
			{
				player.PlayerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
			}
			player.WolfRecuperateStopwatch.Restart();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleEngineerBetterDoorKickPatch error: " + ex));
		}
	}
}
