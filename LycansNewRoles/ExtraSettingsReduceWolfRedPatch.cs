using System;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LightingManager), "DisplayWolfLight")]
internal class ExtraSettingsReduceWolfRedPatch
{
	private static bool Prefix(bool active, LightingManager __instance)
	{
		try
		{
			ColorAdjustmentManager.UpdateColorAdjustment();
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ExtraSettingsReduceWolfRedPatch error: " + ex));
			return true;
		}
	}
}
