using System;
using HarmonyLib;
using TMPro;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameSettingsUI), "SetGameSettings")]
internal class GameSettingsSetGameSettings
{
	private static bool Prefix(GameSettingsUI __instance)
	{
		try
		{
			TMP_Dropdown value = Traverse.Create((object)__instance).Field<TMP_Dropdown>("farmDropdown").Value;
			TMP_Dropdown value2 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("voteTimeDropdown").Value;
			TMP_Dropdown value3 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("dayDurationDropdown").Value;
			TMP_Dropdown value4 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("nightDurationDropdown").Value;
			TMP_Dropdown value5 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("wolvesCountDropdown").Value;
			TMP_Dropdown value6 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("wolfSpeedDropdown").Value;
			TMP_Dropdown value7 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("transformationTimeDropdown").Value;
			TMP_Dropdown value8 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("lootSpawnRateDropdown").Value;
			TMP_Dropdown value9 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("hungerDropdown").Value;
			TMP_Dropdown value10 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("potionsCountDropdown").Value;
			TMP_Dropdown value11 = Traverse.Create((object)__instance).Field<TMP_Dropdown>("itemsSpawnRateDropdown").Value;
			Toggle value12 = Traverse.Create((object)__instance).Field<Toggle>("showAllyToggle").Value;
			Toggle value13 = Traverse.Create((object)__instance).Field<Toggle>("teleportToggle").Value;
			Toggle value14 = Traverse.Create((object)__instance).Field<Toggle>("wolfVoiceToggle").Value;
			Toggle value15 = Traverse.Create((object)__instance).Field<Toggle>("wolfRevertToggle").Value;
			Toggle value16 = Traverse.Create((object)__instance).Field<Toggle>("battleRoyaleToggle").Value;
			int num = Traverse.Create((object)__instance).Field<TMP_Dropdown>("mapDropdown").Value.value + 1;
			GameManager.Instance.UpdateMaxScoreSetting(int.Parse(value.options[value.value].text));
			GameManager.Instance.UpdateVoteTimeSetting(int.Parse(value2.options[value2.value].text));
			GameManager.Instance.UpdateDayDurationSetting(int.Parse(value3.options[value3.value].text));
			GameManager.Instance.UpdateNightDurationSetting(int.Parse(value4.options[value4.value].text));
			GameManager.Instance.UpdateWolvesCountSetting(int.Parse(value5.options[value5.value].text));
			GameManager.Instance.UpdateWolfSpeedSetting(int.Parse(value6.options[value6.value].text));
			GameManager.Instance.UpdateTransformationTimeSetting(int.Parse(value7.options[value7.value].text));
			GameManager.Instance.UpdateLootSpawnRateSetting(int.Parse(value8.options[value8.value].text));
			GameManager.Instance.UpdateHungerSetting(int.Parse(value9.options[value9.value].text));
			GameManager.Instance.UpdatePotionsCountSetting(int.Parse(value10.options[value10.value].text));
			GameManager.Instance.UpdateItemsSpawnRate(int.Parse(value11.options[value11.value].text));
			GameManager.Instance.UpdateShowAllySetting(value12.isOn);
			GameManager.Instance.UpdateTeleportPlayersSetting(value13.isOn);
			GameManager.Instance.UpdateWolfVoiceSetting(value14.isOn);
			GameManager.Instance.UpdateWolfRevertSetting(value15.isOn);
			GameManager.Instance.UpdateBattleRoyale(value16.isOn);
			GameManager.Instance.UpdateMap(num);
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameSettingsSetGameSettings error: " + ex));
			return true;
		}
	}
}
