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
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			List<Texture> list = Traverse.Create((object)__instance).Field<Texture[]>("villagerTextures").Value.ToList();
			List<Color> list2 = Traverse.Create((object)__instance).Field<Color[]>("colors").Value.ToList();
			list.Add(list[0]);
			list.Add(list[1]);
			list.Add(list[2]);
			list.Add(list[3]);
			list.Add(list[4]);
			list.Add(list[5]);
			list2.Add(list2[0]);
			list2.Add(list2[1]);
			list2.Add(list2[2]);
			list2.Add(list2[3]);
			list2.Add(list2[4]);
			list2.Add(list2[5]);
			Traverse.Create((object)__instance).Field("villagerTextures").SetValue((object)list.ToArray());
			Traverse.Create((object)__instance).Field("colors").SetValue((object)list2.ToArray());
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ColorManagerPatch error: " + ex));
		}
	}
}
