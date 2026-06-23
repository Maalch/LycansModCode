using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(TrapItem), "AnimationStarted")]
internal class TrapAnimationStartedPatch
{
	private static bool Prefix(TrapItem __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBool.op_Implicit(Plugin.CustomConfig.TrapsModified))
			{
				return true;
			}
			PlayerController player = PlayerRegistry.GetPlayer(((Item)__instance).Owner);
			if ((Object)(object)player == (Object)null)
			{
				return false;
			}
			player.MovementAction = 1;
			player.CanMoveAnimation = NetworkBool.op_Implicit(false);
			player.IsAiming = NetworkBool.op_Implicit(false);
			player.UpdateAnimation(Animator.StringToHash("Crouching"), true);
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TrapAnimationStartedPatch error: " + ex));
			return true;
		}
	}
}
