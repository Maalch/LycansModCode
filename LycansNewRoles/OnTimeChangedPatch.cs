using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LightingManager), "OnTimeChanged")]
internal class OnTimeChangedPatch
{
	private static void Postfix(Changed<LightingManager> changed)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Necromancer || player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Sneak)
			{
				player.UpdateDescriptionStatusIfNeeded();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnTimeChangedPatch error: " + ex));
		}
	}
}
