using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public static class VoiceChanges
{
	public static float GetVoicePitch(PlayerController playerController, PlayerCustom playerCustom)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
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
		return 1f;
	}
}
