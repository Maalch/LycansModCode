using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameManager), "Awake")]
internal class GameManagerAwakePatch
{
	private static void Prefix(GameManager __instance)
	{
		try
		{
			List<GameObject> list = Traverse.Create((object)__instance).Field<GameObject[]>("maps").Value.ToList();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameManagerAwakePatch error: " + ex));
		}
	}
}
