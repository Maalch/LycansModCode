using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Rpc_Damage")]
internal class RpcDamagePatch
{
	private unsafe static bool Prefix(PlayerRef attacker, PlayerController __instance)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!((NetworkBehaviour)__instance).InvokeRpc)
			{
				NetworkBehaviourUtils.ThrowIfBehaviourNotInitialized((NetworkBehaviour)(object)__instance);
				if ((int)((SimulationBehaviour)__instance).Runner.Stage != 4)
				{
					int localAuthorityMask = ((SimulationBehaviour)__instance).Object.GetLocalAuthorityMask();
					if ((localAuthorityMask & 1) == 0)
					{
						NetworkBehaviourUtils.NotifyLocalSimulationNotAllowedToSendRpc("System.Void PlayerController::Rpc_Damage(Fusion.PlayerRef)", ((SimulationBehaviour)__instance).Object, 1);
					}
					else
					{
						if (((SimulationBehaviour)__instance).Runner.HasAnyActiveConnections())
						{
							int num = 8;
							num += 4;
							SimulationMessage* ptr = SimulationMessage.Allocate(((SimulationBehaviour)__instance).Runner.Simulation, num);
							byte* data = SimulationMessage.GetData(ptr);
							int num2 = RpcHeader.Write(RpcHeader.Create(((SimulationBehaviour)__instance).Object.Id, ((NetworkBehaviour)__instance).ObjectIndex, 6), data);
							Unsafe.Write(data + num2, attacker);
							num2 += 4;
							((SimulationMessage)ptr).Offset = num2 * 8;
							((SimulationBehaviour)__instance).Runner.SendRpc(ptr);
						}
						if ((localAuthorityMask & 7) != 0)
						{
							goto IL_0108;
						}
					}
				}
				return false;
			}
			((NetworkBehaviour)__instance).InvokeRpc = false;
			goto IL_0108;
			IL_0108:
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.Phasing) || NetworkBool.op_Implicit(player.Petrified))
			{
				return false;
			}
			if (((SimulationBehaviour)__instance).Object.HasStateAuthority && !NetworkBool.op_Implicit(__instance.IsDead))
			{
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(attacker);
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					PlayerController playerController = player.PlayerController;
					playerController.Hunger -= 0.1f * (float)GameManager.Instance.MaxHunger;
					PlayerCustom.ApplyEffectToPlayer(player.PlayerController, "LycansNewRoles.EffectWounded", ((SimulationBehaviour)__instance).Runner);
				}
				else if (NetworkBool.op_Implicit(player.Resurrected))
				{
					__instance.Rpc_Kill(attacker);
				}
				else if (player.PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is ResistanceEffect))
				{
					__instance.Hunger = Mathf.Min(__instance.Hunger, (float)GameManager.Instance.MaxHunger * 0.6f);
					player.PlayerAnimations.PlayNonLoopAnimation("GetHit");
				}
				else if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Predator && player.PrimaryRoleTargetRef == player2.Ref && !__instance.IsStarving())
				{
					__instance.Hunger = Mathf.Min(__instance.Hunger, (float)GameManager.Instance.MaxHunger * 0.5f);
					player.PlayerAnimations.PlayNonLoopAnimation("GetHit");
				}
				else if (!NetworkBool.op_Implicit(__instance.IsWolf))
				{
					player.Stats.UpdateDeathType("BULLET_HUMAN");
					__instance.Rpc_Kill(attacker);
				}
				else if (!__instance.IsStarving())
				{
					__instance.Hunger = (float)GameManager.Instance.MaxHunger * 0.125f;
					player.PlayerAnimations.PlayNonLoopAnimation("GetHit");
				}
				else
				{
					player.Stats.UpdateDeathType("BULLET_WOLF");
					__instance.Rpc_Kill(attacker);
				}
			}
			if (((SimulationBehaviour)__instance).Object.HasInputAuthority)
			{
				AudioManager.Play("BULLET_HIT", (MixerTarget)2, 1f, 1f);
				return false;
			}
			((Renderer)Traverse.Create((object)__instance).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = Color.red;
			AudioManager.PlayAndFollow("BULLET_HIT", ((Component)__instance).transform, (MixerTarget)2, 15f, 0.8f);
			if (PlayerController.Local.Ref == attacker)
			{
				GameManager.Instance.gameUI.HighlightHunterCrosshair(NetworkBool.op_Implicit(__instance.IsDead));
				if (!NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
				{
					((MonoBehaviour)player).StartCoroutine(player.WaitAndPlaySuccessShot(0.2f));
				}
			}
			((MonoBehaviour)player).StartCoroutine(player.WaitAndUpdateWolfColor(0.2f));
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("RpcDamagePatch error: " + ex));
			return true;
		}
	}
}
