using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnAimingChanged")]
internal class OnAimingChangedPatch
{
	private static void Prefix(Changed<PlayerController> changed)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
		player.PlayerAnimations.ResetLoopAnimation();
	}

	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
			if ((player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary || player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Poacher) && player.Ref == PlayerController.Local.Ref)
			{
				Traverse.Create((object)PlayerController.Local.LocalCameraHandler).Field<bool>("zoomed").Value = NetworkBool.op_Implicit(changed.Behaviour.IsAiming);
				PlayerController.Local.LocalCameraHandler.LocalCamera.fieldOfView = (NetworkBool.op_Implicit(changed.Behaviour.IsAiming) ? 10f : 60f);
				GameManager.Instance.gameUI.ShowSpyglassOverlay(NetworkBool.op_Implicit(changed.Behaviour.IsAiming));
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnAimingChangedPatch error: " + ex));
		}
	}
}
