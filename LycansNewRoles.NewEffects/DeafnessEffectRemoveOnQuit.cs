using System;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[HarmonyPatch(typeof(GameManager), "QuitGame")]
internal class DeafnessEffectRemoveOnQuit
{
	private static void Prefix()
	{
		try
		{
			AudioListener.volume = 1f;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DeafnessEffectRemoveOnQuit error: " + ex));
		}
	}
}
