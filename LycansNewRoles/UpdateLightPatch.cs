using System;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LightingManager), "UpdateLight")]
internal class UpdateLightPatch
{
	private static bool Prefix(CustomLight sceneLight)
	{
		try
		{
			if (LanternCustom.LanternCustomsByLight.ContainsKey(sceneLight) && !LanternCustom.LanternCustomsByLight[sceneLight].On)
			{
				((Behaviour)sceneLight.lightComponent).enabled = false;
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CheckLanternLightPatch error: " + ex));
			return true;
		}
	}
}
