using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(ColorManager), "Awake")]
internal class ColorManagerPatch
{
	private static void Postfix(ColorManager __instance)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			List<Texture> list = Traverse.Create((object)__instance).Field<Texture[]>("villagerTextures").Value.ToList();
			List<Color> list2 = Traverse.Create((object)__instance).Field<Color[]>("colors").Value.ToList();
			list.Add(list[0]);
			list.Add(list[1]);
			list.Add(list[2]);
			list2.Add(list2[0]);
			list2.Add(list2[1]);
			list2.Add(list2[2]);
			Traverse.Create((object)__instance).Field("villagerTextures").SetValue((object)list.ToArray());
			Traverse.Create((object)__instance).Field("colors").SetValue((object)list2.ToArray());
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ColorManagerPatch error: " + ex));
		}
	}
}
