using System;
using System.Collections.Generic;
using HarmonyLib;
using TMPro;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameSettingsUI), "Start")]
internal class GameConfigAddNewMapPatch
{
	private static void Postfix(GameSettingsUI __instance)
	{
		try
		{
			foreach (string value in Plugin.NewMapPathById.Values)
			{
				string item = value;
				switch (value)
				{
				case "map_dungeon":
					item = "Le Donjon (Nales)";
					break;
				case "map_haddoncans":
					item = "HaddonCans (Alefa)";
					break;
				case "map_apartcan":
					item = "Ashfang Woods (Hornicoo)";
					break;
				case "map_laboratory":
					item = "Lupus Labs (Nales)";
					break;
				case "map_got":
					item = "Wisterio Lane (Hornicoo)";
					break;
				}
				Traverse.Create((object)__instance).Field<TMP_Dropdown>("mapDropdown").Value.AddOptions(new List<string> { item });
			}
			Traverse.Create((object)__instance).Method("FillMapDropdown", Array.Empty<object>()).GetValue();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameConfigAddNewMapPatch error: " + ex));
		}
	}
}
