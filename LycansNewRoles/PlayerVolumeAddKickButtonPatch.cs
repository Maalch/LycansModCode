using System;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerVolume), "Init")]
internal class PlayerVolumeAddKickButtonPatch
{
	private static void Postfix(PlayerVolume __instance)
	{
		try
		{
			if (GameManager.Instance.IsHost && (Object)(object)((Component)__instance).GetComponent<PlayerKickComponent>() == (Object)null)
			{
				((Component)__instance).gameObject.AddComponent<PlayerKickComponent>();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PlayerVolumeAddKickButtonPatch error: " + ex));
		}
	}
}
