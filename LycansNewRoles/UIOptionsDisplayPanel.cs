using System.Collections.Generic;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIOptionsDisplayPanel : MonoBehaviour
{
	private GameObject _panel;

	public bool Active = false;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		((TMP_Text)((Component)_panel.transform.Find("Title")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_TITLE");
		Transform val = _panel.transform.Find("Options");
		((TMP_Text)((Component)val.Find("Wolves").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_WOLVES");
		((TMP_Text)((Component)val.Find("Traitors").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_TRAITORS");
		((TMP_Text)((Component)val.Find("WolfPups").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_WOLF_PUPS");
		((TMP_Text)((Component)val.Find("ElitePowersGiven").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_ELITE_POWERS_GIVEN");
		((TMP_Text)((Component)val.Find("ElitePowersChecked").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_ELITE_POWERS_CHECKED");
		((TMP_Text)((Component)val.Find("SoloRolesGiven").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_SOLO_ROLES_GIVEN");
		((TMP_Text)((Component)val.Find("SoloRolesChecked").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_SOLO_ROLES_CHECKED");
		((TMP_Text)((Component)val.Find("JobsChance").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_JOBS_CHANCE");
		((TMP_Text)((Component)val.Find("JobsChecked").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_JOBS_CHECKED");
		((TMP_Text)((Component)val.Find("AvatarChance").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_AVATAR_CHANCE");
		((TMP_Text)((Component)val.Find("MoleChance").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_MOLE_CHANCE");
		((TMP_Text)((Component)val.Find("WolfPowersGiven").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_WOLF_POWERS_GIVEN");
		((TMP_Text)((Component)val.Find("WolfPowersChecked").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_WOLF_POWERS_CHECKED");
		((TMP_Text)((Component)val.Find("SecondaryRolesGiven").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_SECONDARY_ROLES_GIVEN");
		((TMP_Text)((Component)val.Find("SecondaryRolesChecked").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_SECONDARY_ROLES_CHECKED");
		((TMP_Text)((Component)val.Find("PotionsChecked").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_POTIONS_CHECKED");
		((TMP_Text)((Component)val.Find("ItemsChecked").Find("OptionTitle")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_OPTIONS_DISPLAY_ITEMS_CHECKED");
		_panel.SetActive(false);
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		if ((int)GameManager.LocalGameState != 1 || ((SimulationBehaviour)GameManager.Instance).Runner.IsServer || UIManager.CustomizationComponent.Active || GameManager.Instance.gameUI.IsSettingMenuOpen || GameManager.Instance.gameUI.IsGameSettingMenuOpen)
		{
			if (Active)
			{
				Hide();
			}
		}
		else if (!Active)
		{
			Show();
		}
	}

	public void Show()
	{
		_panel.SetActive(true);
		Active = true;
	}

	public void Hide()
	{
		_panel.SetActive(false);
		Active = false;
	}

	public void RefreshConfiguration(int wolvesCount, int traitorsCount, int wolfPupsCount, int soloRolesGiven, int soloRolesChecked, int jobsChance, int jobsChecked, int elitePowersGiven, int elitePowersChecked, int avatarChance, int moleChance, int wolfPowersGiven, int wolfPowersChecked, int secondaryRolesGiven, int secondaryRolesChecked, int potionsChecked, int itemsChecked)
	{
		Dictionary<PlayerCustom.PlayerPrimaryRolePower, bool> source = Plugin.CustomConfig.PrimaryRolePowerActive.Where((KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, bool> o) => o.Key != PlayerCustom.PlayerPrimaryRolePower.None).ToDictionary((KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, bool> o) => o.Key, (KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, bool> o) => o.Value);
		Transform val = _panel.transform.Find("Options");
		((TMP_Text)((Component)val.Find("Wolves").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = wolvesCount.ToString();
		((TMP_Text)((Component)val.Find("Traitors").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = traitorsCount.ToString();
		((TMP_Text)((Component)val.Find("WolfPups").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = wolfPupsCount.ToString();
		((TMP_Text)((Component)val.Find("SoloRolesGiven").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = soloRolesGiven.ToString();
		((TMP_Text)((Component)val.Find("SoloRolesChecked").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = soloRolesChecked + "/" + Plugin.CustomConfig.SoloRoleActive.Count;
		((TMP_Text)((Component)val.Find("JobsChance").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = jobsChance + "%";
		((TMP_Text)((Component)val.Find("JobsChecked").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = jobsChecked + "/" + source.Count((KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, bool> o) => PlayerCustom.IsPrimaryRolePowerForNormalVillagers(o.Key));
		((TMP_Text)((Component)val.Find("ElitePowersGiven").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = elitePowersGiven.ToString();
		((TMP_Text)((Component)val.Find("ElitePowersChecked").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = elitePowersChecked.ToString();
		((TMP_Text)((Component)val.Find("AvatarChance").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = avatarChance + "%";
		((TMP_Text)((Component)val.Find("MoleChance").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = moleChance + "%";
		((TMP_Text)((Component)val.Find("WolfPowersGiven").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = wolfPowersGiven.ToString();
		((TMP_Text)((Component)val.Find("WolfPowersChecked").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = wolfPowersChecked + "/" + source.Count((KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, bool> o) => PlayerCustom.IsPrimaryRolePowerForWolves(o.Key));
		((TMP_Text)((Component)val.Find("SecondaryRolesGiven").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = secondaryRolesGiven.ToString();
		((TMP_Text)((Component)val.Find("SecondaryRolesChecked").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = secondaryRolesChecked + "/" + Plugin.CustomConfig.SecondaryRoleActive.Count;
		((TMP_Text)((Component)val.Find("PotionsChecked").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = potionsChecked + "/" + Plugin.CustomConfig.PotionsAvailability.Count;
		((TMP_Text)((Component)val.Find("ItemsChecked").Find("OptionValue")).GetComponent<TextMeshProUGUI>()).text = itemsChecked + "/" + Plugin.CustomConfig.GadgetsAvailability.Count;
	}

	public static void SendRefreshToClients()
	{
		if (!((Object)(object)Plugin.CustomConfig == (Object)null))
		{
			PlayerCustom.Rpc_Update_Options_Display(((SimulationBehaviour)GameManager.Instance).Runner, GameManager.Instance.WolvesCount, Plugin.CustomConfig.TraitorsCount, Plugin.CustomConfig.WolfPupsCount, Plugin.CustomConfig.SoloRolesCount, Plugin.CustomConfig.SoloRoleActive.Count((KeyValuePair<PlayerCustom.PlayerNewPrimaryRole, bool> o) => o.Value), Plugin.CustomConfig.VillagerPowersChance, Plugin.CustomConfig.PrimaryRolePowerActive.Count((KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, bool> o) => PlayerCustom.IsPrimaryRolePowerForNormalVillagers(o.Key) && o.Value), Plugin.CustomConfig.ElitesCount, Plugin.CustomConfig.PrimaryRolePowerActive.Count((KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, bool> o) => PlayerCustom.IsPrimaryRolePowerForEliteVillagers(o.Key) && o.Value), Plugin.CustomConfig.AvatarChance, Plugin.CustomConfig.MoleChance, Plugin.CustomConfig.WolfPowersCount, Plugin.CustomConfig.PrimaryRolePowerActive.Count((KeyValuePair<PlayerCustom.PlayerPrimaryRolePower, bool> o) => PlayerCustom.IsPrimaryRolePowerForWolves(o.Key) && o.Value), Plugin.CustomConfig.SecondaryRolesCount, Plugin.CustomConfig.SecondaryRoleActive.Count((KeyValuePair<PlayerCustom.PlayerSecondaryRole, bool> o) => o.Value), Plugin.CustomConfig.PotionsAvailability.Count((KeyValuePair<string, bool> o) => o.Value), Plugin.CustomConfig.GadgetsAvailability.Count((KeyValuePair<string, bool> o) => o.Value));
		}
	}
}
