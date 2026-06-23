using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerEffectsManager), "ParanoiaChanged")]
internal class OnParanoiaChangedPatch
{
	private static void Postfix(Changed<PlayerEffectsManager> changed)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController value = Traverse.Create((object)changed.Behaviour).Field<PlayerController>("_playerController").Value;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(value.Ref);
			if (!player.IsCurrentlyPlayedOrObserved)
			{
				return;
			}
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.UpdateVisibility();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnParanoiaChangedPatch error: " + ex));
		}
	}
}
