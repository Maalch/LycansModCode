using System;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "ShowPregameShortcutSkin")]
internal class GameUIHideChangeHatText
{
	private static void Postfix(GameUI __instance)
	{
		try
		{
			((Behaviour)Traverse.Create((object)__instance).Field<TextMeshProUGUI>("pgmShortcutSkinText").Value).enabled = false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameUIHideChangeHatText error: " + ex));
		}
	}
}
