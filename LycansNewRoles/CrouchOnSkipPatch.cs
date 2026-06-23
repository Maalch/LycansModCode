using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnVoteChanged")]
internal class CrouchOnSkipPatch
{
	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBool.op_Implicit(Plugin.CustomConfig.AnonymousVotes))
			{
				PlayerController behaviour = changed.Behaviour;
				if (behaviour.IdVoted == -2)
				{
					PlayerCustom player = PlayerCustomRegistry.GetPlayer(behaviour.Ref);
					player.PlayerAnimations.SetLoopAnimation("HumanM@Question01", active: true);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CrouchOnSkipPatch: " + ex));
		}
	}
}
