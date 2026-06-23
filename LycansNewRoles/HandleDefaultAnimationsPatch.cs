using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateAnimation", new Type[]
{
	typeof(int),
	typeof(bool)
})]
internal class HandleDefaultAnimationsPatch
{
	private static void Prefix(PlayerController __instance, int id, bool value)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Invalid comparison between Unknown and I4
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerNewAnimationsComponent component = ((Component)__instance).GetComponent<PlayerNewAnimationsComponent>();
			if (!((Object)(object)component == (Object)null))
			{
				component.CancelLoopAnimation();
				if (id == Animator.StringToHash("Drinking"))
				{
					component.ResetNonLoopAnimation();
				}
				else if (id == Animator.StringToHash("RifleIdle"))
				{
					component.ResetLoopAnimation();
				}
				if (!value && (int)GameManager.LocalGameState == 4 && NetworkBool.op_Implicit(GameManager.Instance.CanVote) && __instance.IdVoted == -2)
				{
					component.SetLoopAnimation("HumanM@Question01", active: true);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("HandleDefaultAnimationsPatch error: " + ex));
		}
	}
}
