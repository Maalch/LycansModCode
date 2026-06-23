using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnGunLoadedChanged")]
internal class OnGunLoadedChangedPatch
{
	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Invalid comparison between Unknown and I4
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
			if (NetworkBool.op_Implicit(changed.Behaviour.IsGunLoaded) && player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothCarabineer)
			{
				player.SecondaryRolePowerCooldownTimer = TickTimer.None;
				if (player.IsCurrentlyPlayedOrObserved)
				{
					AudioManager.Play("RELOAD", (MixerTarget)2, 0.35f, 1f);
					player.UpdateDescriptionStatusIfNeeded();
				}
			}
			if ((int)player.PlayerController.Role == 2 && player.IsCurrentlyPlayedOrObserved)
			{
				player.UpdateDescriptionStatusIfNeeded();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnGunLoadedChangedPatch error: " + ex));
		}
	}
}
