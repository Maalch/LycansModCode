using System;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameSettingsUI), "ResetSettings")]
internal class GameConfigResetSettingsPatch
{
	private static void Postfix(GameSettingsUI __instance)
	{
		try
		{
			Plugin.CustomConfig.ResetToDefault();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameConfigResetSettingsPatch error: " + ex));
		}
	}
}
