using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles.NewPrimaryRoles;

[HarmonyPatch(typeof(GameManager), "Rpc_Transition")]
internal class NewPrimaryRoleNecromancerKillAtDawnPatch
{
	private static void Postfix(NetworkRunner runner)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer != (Object)null)
			{
				Plugin.Minimap.UpdatePositionBeforeMeeting(((Component)PlayerController.Local.LocalCameraHandler.PovPlayer).transform.position, ((Component)PlayerController.Local.LocalCameraHandler.PovPlayer).transform.rotation);
			}
			if (!runner.IsServer || !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
			{
				return;
			}
			foreach (PlayerController item in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => !NetworkBool.op_Implicit(p.IsDead))).ToList())
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(item.Ref);
				if (NetworkBool.op_Implicit(player.Resurrected) || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
				{
					item.IsDead = NetworkBool.op_Implicit(true);
				}
				else if (NetworkBool.op_Implicit(player.Dying))
				{
					Effect val = player.PlayerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is DyingEffect);
					if ((Object)(object)val != (Object)null)
					{
						player.PlayerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
					}
				}
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist && NetworkBool.op_Implicit(CultistManager.Instance.CultistActive) && !NetworkBool.op_Implicit(GameManager.Instance.IsFinished))
				{
					player.Stats.UpdateDeathType("CULTIST_FAILED");
					player.PlayerController.Rpc_Kill(PlayerRef.None);
					CultistManager.Instance.CultistActive = NetworkBool.op_Implicit(false);
					runner.Despawn(((Component)player.SummonedSpirit).GetComponent<NetworkObject>(), false);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewPrimaryRoleNecromancerKillAtDawnPatch: " + ex));
		}
	}
}
