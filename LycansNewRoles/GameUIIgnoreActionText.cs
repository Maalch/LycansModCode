using System;
using HarmonyLib;
using Managers;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "UpdateAction", new Type[]
{
	typeof(string),
	typeof(InputActionName),
	typeof(object[])
})]
internal class GameUIIgnoreActionText
{
	private static bool Prefix(GameUI __instance)
	{
		try
		{
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameUIIgnoreActionText error: " + ex));
			return true;
		}
	}
}
