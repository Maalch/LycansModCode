using System;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "CloseMessage")]
internal class GameUICloseGameIfNewMapsInstalledPatch
{
	private static bool Prefix()
	{
		try
		{
			if (Harmony.HasAnyPatches("lycans.nalesnewmaps"))
			{
				Application.Quit();
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameUICloseGameIfNewMapsInstalledPatch error: " + ex));
			return true;
		}
	}
}
