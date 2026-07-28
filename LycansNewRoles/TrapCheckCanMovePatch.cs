using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Trap), "FixedUpdateNetwork")]
internal class TrapCheckCanMovePatch
{
	private static bool Prefix(Trap __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		TickTimer trappedTimer = __instance.TrappedTimer;
		if (((TickTimer)(ref trappedTimer)).Expired(((SimulationBehaviour)__instance).Runner))
		{
			__instance.TrappedTimer = TickTimer.None;
			((NetworkBehaviour)__instance).CopyStateToBackingFields();
			PlayerRef value = Traverse.Create((object)__instance).Field<PlayerRef>("_TrappedPlayer").Value;
			if (value != PlayerRef.None && PlayerCustomRegistry.HasPlayer(value))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(value);
				AudioManager.PlayAndFollow("TRAP_BREAKING", ((Component)__instance).transform, (MixerTarget)2, 20f, 1f);
				player.PlayerController.IsTrapped = NetworkBool.op_Implicit(false);
				Traverse.Create((object)__instance).Field<Animator>("animator").Value.SetBool(Animator.StringToHash("Breaking"), true);
				player.UpdateCanMoveAnimation();
			}
		}
		return false;
	}
}
