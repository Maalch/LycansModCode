using System;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "ShowSkipVote")]
internal class GameUIHideSkipVoteText
{
	private static bool Prefix(GameUI __instance)
	{
		try
		{
			((Behaviour)Traverse.Create((object)__instance).Field<TextMeshProUGUI>("skipVoteText").Value).enabled = false;
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameUIHideSkipVoteText error: " + ex));
			return true;
		}
	}
}
