using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(TrapItem), "AnimationEnded")]
internal class TrapAnimationEndedPatch
{
	private static bool Prefix(TrapItem __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
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
			player.MovementAction = 0;
			player.UpdateAnimation(Animator.StringToHash("Crouching"), false);
			player.CanMoveAnimation = NetworkBool.op_Implicit(true);
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TrapAnimationEndedPatch error: " + ex));
			return true;
		}
	}
}
