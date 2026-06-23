using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "Awake")]
internal class MoreKeysGameUIAwakePatch
{
	private static void Postfix(GameUI __instance)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			InputManagerExtra.AdaptUI(__instance);
			InputManagerExtra.AddActionToUI(__instance, "SECONDARYROLEPOWER");
			InputManagerExtra.AddActionToUI(__instance, "SHOWMINIMAP");
			InputManagerExtra.AddActionToUI(__instance, "ITEMSECONDARY");
			InputManagerExtra.AddActionToUI(__instance, "MAYORACTION");
			InputManagerExtra.AddActionToUI(__instance, "ACCESSORYACTION");
			InputManagerExtra.AddActionToUI(__instance, "EMOTE1");
			InputManagerExtra.AddActionToUI(__instance, "EMOTE2");
			InputManagerExtra.AddActionToUI(__instance, "EMOTE3");
			InputManagerExtra.AddActionToUI(__instance, "EMOTE4");
			InputManagerExtra.AddActionToUI(__instance, "EMOTE5");
			InputManagerExtra.AddActionToUI(__instance, "EMOTE6");
			InputManagerExtra.AddActionToUI(__instance, "EMOTE7");
			InputManagerExtra.AddActionToUI(__instance, "EMOTE8");
			Traverse.Create((object)GameManager.Instance.gameUI).Field<GameObject>("settingsMenuKeyboard").Value.GetComponent<GridLayoutGroup>().cellSize = new Vector2(700f, 50f);
			Traverse.Create((object)GameManager.Instance.gameUI).Field<GameObject>("settingsMenuGamepad").Value.GetComponent<GridLayoutGroup>().cellSize = new Vector2(700f, 60f);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MoreKeysGameUIAwakePatch error: " + ex));
		}
	}
}
