using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "ActionInput")]
internal class RifleChangesActionInputPatch
{
	private static bool Prefix(bool isPrimary, PlayerController __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
		if (player.IsOutOfTheWorld)
		{
			return false;
		}
		return true;
	}

	private static void Postfix(bool isPrimary, PlayerController __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Invalid comparison between Unknown and I4
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(GameManager.LightingManager.IsTransition) || (int)GameManager.State.Current != 2 || NetworkBool.op_Implicit(__instance.IsDead))
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (!isPrimary || NetworkBool.op_Implicit(__instance.IsClimbing) || !NetworkBool.op_Implicit(__instance.CanMoveAnimation) || NetworkBool.op_Implicit(__instance.IsZooming))
			{
				return;
			}
			PlayerCustom.PlayerPrimaryRolePower primaryRolePower = player.PrimaryRolePower;
			PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
			if (playerPrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Poacher)
			{
				__instance.IsAiming = NetworkBool.op_Implicit(!NetworkBool.op_Implicit(__instance.IsAiming));
				return;
			}
			PlayerCustom.PlayerNewPrimaryRole newPrimaryRole = player.NewPrimaryRole;
			PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
			if (playerNewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Mercenary)
			{
				return;
			}
			__instance.IsAiming = NetworkBool.op_Implicit(!NetworkBool.op_Implicit(__instance.IsAiming));
			if (((SimulationBehaviour)__instance).Runner.IsServer && NetworkBool.op_Implicit(__instance.IsAiming) && player.PrimaryRoleTargetRef != PlayerRef.None)
			{
				TickTimer primaryRolePowerCooldownTimer = player.PrimaryRolePowerCooldownTimer;
				if (!((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
				{
					PlayerController player2 = PlayerRegistry.GetPlayer(player.PrimaryRoleTargetRef);
					PlayerCustom.ApplyEffectToPlayer(player2, "LycansNewRoles.EffectParalyzed", ((SimulationBehaviour)__instance).Runner);
					player.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 60f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRolePoacherActionInputPatch: " + ex));
		}
	}
}
