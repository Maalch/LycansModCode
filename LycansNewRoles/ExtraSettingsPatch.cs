using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(SettingsUI), "Start")]
internal class ExtraSettingsPatch
{
	private static void Postfix(SettingsUI __instance)
	{
		try
		{
			if (!PlayerPrefs.HasKey(ExtraSettings.PlayerPrefReduceWolfRef))
			{
				PlayerPrefs.SetInt(ExtraSettings.PlayerPrefReduceWolfRef, 0);
				ExtraSettings.Instance.ReduceWolfRed = false;
			}
			else
			{
				ExtraSettings.Instance.ReduceWolfRed = PlayerPrefs.GetInt(ExtraSettings.PlayerPrefReduceWolfRef) == 1;
			}
			if (!PlayerPrefs.HasKey(ExtraSettings.PlayerPrefDisableLivingVoicesWhenInDeadChannel))
			{
				PlayerPrefs.SetInt(ExtraSettings.PlayerPrefDisableLivingVoicesWhenInDeadChannel, 1);
				ExtraSettings.Instance.DisableLivingVoicesWhenInDeadChannel = true;
			}
			else
			{
				ExtraSettings.Instance.DisableLivingVoicesWhenInDeadChannel = PlayerPrefs.GetInt(ExtraSettings.PlayerPrefDisableLivingVoicesWhenInDeadChannel) == 1;
			}
			if (!PlayerPrefs.HasKey(ExtraSettings.PlayerPrefNoDeadRoleOnDeath))
			{
				PlayerPrefs.SetInt(ExtraSettings.PlayerPrefNoDeadRoleOnDeath, 0);
				ExtraSettings.Instance.NoDeadRoleOnDeath = false;
			}
			else
			{
				ExtraSettings.Instance.NoDeadRoleOnDeath = PlayerPrefs.GetInt(ExtraSettings.PlayerPrefNoDeadRoleOnDeath) == 1;
			}
			if (!PlayerPrefs.HasKey(ExtraSettings.PlayerPrefHidePets))
			{
				PlayerPrefs.SetInt(ExtraSettings.PlayerPrefHidePets, 0);
				ExtraSettings.Instance.HidePets = false;
			}
			else
			{
				ExtraSettings.Instance.HidePets = PlayerPrefs.GetInt(ExtraSettings.PlayerPrefHidePets) == 1;
			}
			Toggle checkbox = GetCheckbox(__instance, "NALES_SETTING_REDUCE_WOLF_RED", ExtraSettings.PlayerPrefReduceWolfRef);
			((UnityEvent<bool>)(object)checkbox.onValueChanged).AddListener((UnityAction<bool>)ExtraSettings.OnReduceWolfRedChanged);
			Toggle checkbox2 = GetCheckbox(__instance, "NALES_SETTING_DISABLE_LIVING_VOICES_WHEN_IN_DEAD_CHANNEL", ExtraSettings.PlayerPrefDisableLivingVoicesWhenInDeadChannel);
			((UnityEvent<bool>)(object)checkbox2.onValueChanged).AddListener((UnityAction<bool>)ExtraSettings.OnDisableLivingVoicesWhenInDeadChannelChanged);
			Toggle checkbox3 = GetCheckbox(__instance, "NALES_SETTING_NO_DEAD_ROLE_ON_DEATH", ExtraSettings.PlayerPrefNoDeadRoleOnDeath);
			((UnityEvent<bool>)(object)checkbox3.onValueChanged).AddListener((UnityAction<bool>)ExtraSettings.OnNoDeadRoleOnDeathChanged);
			Toggle checkbox4 = GetCheckbox(__instance, "NALES_SETTING_HIDE_PETS", ExtraSettings.PlayerPrefHidePets);
			((UnityEvent<bool>)(object)checkbox4.onValueChanged).AddListener((UnityAction<bool>)ExtraSettings.OnHidePetsChanged);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ExtraSettingsPatch error: " + ex));
		}
	}

	private static Toggle GetCheckbox(SettingsUI settingsUI, string textKey, string playerPref)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		Toggle value = Traverse.Create((object)settingsUI).Field<Toggle>("reverseYAxisToggle").Value;
		Transform parent = ((Component)value).transform.parent.parent.parent;
		Transform parent2 = ((Component)value).transform.parent.parent.parent.parent;
		Transform val = Object.Instantiate<Transform>(parent, parent2);
		LocalizeStringEvent val2 = ((Component)val).GetComponentsInChildren<LocalizeStringEvent>().First((LocalizeStringEvent o) => ((Object)o).name == "SettingNameText");
		((LocalizedReference)val2.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit(textKey));
		Toggle componentInChildren = ((Component)val).GetComponentInChildren<Toggle>();
		GameObject gameObject = ((Component)componentInChildren).gameObject;
		Object.DestroyImmediate((Object)(object)componentInChildren);
		componentInChildren = gameObject.AddComponent<Toggle>();
		componentInChildren.graphic = ((Component)((Component)componentInChildren).transform.Find("Background").Find("Checkmark")).GetComponent<Graphic>();
		componentInChildren.isOn = PlayerPrefs.GetInt(playerPref) == 1;
		return componentInChildren;
	}
}
