using System;
using HarmonyLib;
using TMPro;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "UpdatePlayerCount")]
internal class UpdatePlayerCountPatch
{
	private static bool Prefix(int count, GameUI __instance)
	{
		try
		{
			((TMP_Text)Traverse.Create((object)__instance).Field<TextMeshProUGUI>("playerCount").Value).text = count + "/15";
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdatePlayerCountPatch error: " + ex));
			return true;
		}
	}
}
