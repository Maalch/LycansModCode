using System;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "GiveAlchemistPotions")]
public class GiveItemPatch
{
	private static void Postfix(GameManager __instance)
	{
		try
		{
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Give item error: " + ex));
		}
	}
}
