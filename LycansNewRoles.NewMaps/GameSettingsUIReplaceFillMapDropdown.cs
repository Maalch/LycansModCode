using System;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameSettingsUI), "FillMapDropdown")]
internal class GameSettingsUIReplaceFillMapDropdown
{
	private static bool Prefix(GameSettingsUI __instance)
	{
		try
		{
			TMP_Dropdown value = Traverse.Create((object)__instance).Field<TMP_Dropdown>("mapDropdown").Value;
			((UnityEventBase)value.onValueChanged).RemoveAllListeners();
			((UnityEvent<int>)(object)value.onValueChanged).AddListener((UnityAction<int>)delegate(int index)
			{
				GameManager.Instance.UpdateMap(index + 1);
				PlayerPrefs.SetInt("GAME_SETTINGS_MAP", index);
			});
			if (PlayerPrefs.HasKey("GAME_SETTINGS_MAP"))
			{
				int num = PlayerPrefs.GetInt("GAME_SETTINGS_MAP");
				if (num < value.options.Count)
				{
					value.SetValueWithoutNotify(num);
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameConfigAddNewMapPatch error: " + ex));
			return true;
		}
	}
}
