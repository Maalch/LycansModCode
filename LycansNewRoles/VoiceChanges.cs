using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public static class VoiceChanges
{
	public static float GetVoicePitch(PlayerController playerController, PlayerCustom playerCustom)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerController.Local == (Object)null || (Object)(object)PlayerController.Local.LocalCameraHandler == (Object)null || (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer == (Object)null)
		{
			return 1f;
		}
		if (((SimulationBehaviour)playerController).HasInputAuthority || NetworkBool.op_Implicit(playerController.IsDead))
		{
			return 1f;
		}
		PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
		if (NetworkBool.op_Implicit(playerController.IsWolf) && (Object)(object)BeastManager.Instance != (Object)null && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
		{
			return 0.65f;
		}
		if (NetworkBool.op_Implicit(playerCustom.Confused))
		{
			return 0.8f;
		}
		if (NetworkBool.op_Implicit(playerController.IsWolf) || NetworkBool.op_Implicit(povPlayer.PlayerEffectManager.Paranoia))
		{
			return (Random.value > 0.5f) ? Random.Range(1.15f, 1.2f) : Random.Range(0.65f, 0.8f);
		}
		if (NetworkBool.op_Implicit(playerCustom.Tiny))
		{
			return 1.2f;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (NetworkBool.op_Implicit(player.Confused))
		{
			return 1.12f;
		}
		if (playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie && NetworkBool.op_Implicit(VoodooManager.Instance.VoodooActive))
		{
			return 0.9f;
		}
		return 1f;
	}
}
