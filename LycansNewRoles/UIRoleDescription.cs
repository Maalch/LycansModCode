using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Managers;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIRoleDescription : MonoBehaviour
{
	private GameObject _panelPrimary;

	private GameObject _panelPrimaryWarning;

	private GameObject _panelSecondary;

	private GameObject _panelAccessory;

	private GameObject _panelEvent;

	private TextMeshProUGUI _descriptionPrimary;

	private TextMeshProUGUI _descriptionSecondary;

	private TextMeshProUGUI _descriptionAccessory;

	private TextMeshProUGUI _descriptionEvent;

	private void Awake()
	{
		_panelPrimary = ((Component)((Component)this).transform.Find("PanelPrimary")).gameObject;
		_panelPrimaryWarning = ((Component)((Component)this).transform.Find("PanelPrimaryWarning")).gameObject;
		_panelSecondary = ((Component)((Component)this).transform.Find("PanelSecondary")).gameObject;
		_panelAccessory = ((Component)((Component)this).transform.Find("PanelAccessory")).gameObject;
		_panelEvent = ((Component)((Component)this).transform.Find("PanelEvent")).gameObject;
		_descriptionPrimary = ((Component)((Component)this).transform.Find("PanelPrimary").Find("Description")).GetComponent<TextMeshProUGUI>();
		_descriptionSecondary = ((Component)((Component)this).transform.Find("PanelSecondary").Find("Description")).GetComponent<TextMeshProUGUI>();
		_descriptionAccessory = ((Component)((Component)this).transform.Find("PanelAccessory").Find("Description")).GetComponent<TextMeshProUGUI>();
		_descriptionEvent = ((Component)((Component)this).transform.Find("PanelEvent").Find("Description")).GetComponent<TextMeshProUGUI>();
		Hide();
	}

	public void Show(PlayerCustom playerCustom)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Unknown result type (might be due to invalid IL or missing references)
		if (PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Possessor)
		{
			PlayerRef primaryRoleTargetRef = PlayerCustom.Local.PrimaryRoleTargetRef;
			if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && PlayerCustom.Local.PrimaryRolePowerCurrentMaterials >= PlayerCustom.Local.PowerMaterialsInfo.RequiredMaterials && NetworkBool.op_Implicit(PlayerCustom.Local.NewPrimaryRoleUniqueBool))
			{
				return;
			}
		}
		if (ShowRoleDescriptionPatch.ShowingExplanation)
		{
			return;
		}
		string text = "";
		string text2 = "";
		if (playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None || playerCustom.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None)
		{
			List<string> list = new List<string>();
			if (playerCustom.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None)
			{
				text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_" + PlayerCustom.GetPrimaryRolePowerString(playerCustom.PrimaryRolePower));
				switch (playerCustom.PrimaryRolePower)
				{
				case PlayerCustom.PlayerPrimaryRolePower.Necromancer:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Saboteur:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)4).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Deceiver:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""),
						playerCustom.PrimaryRolePowerCooldown.Value.ToString()
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Tracker:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						6f.ToString(),
						30.ToString()
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Warlock:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""),
						playerCustom.PrimaryRolePowerCooldown.Value.ToString()
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Possessor:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)6).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Bomber:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Ritualist:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Predator:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Sneak:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Host:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						15.ToString(),
						LycansUtility.GetInputDisplayCustom((InputActionName)4).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Peasant:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Avenger:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Exorcist:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						3f.ToString(),
						15f.ToString()
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Investigator:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Survivalist:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Priest:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Scout:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Magician:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)6).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Mystic:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						15.ToString()
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Shadow:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Hermit:
					list = new List<string>
					{
						80.ToString(),
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						20.ToString()
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Runemaster:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						8.ToString(),
						LycansUtility.GetInputDisplayCustom((InputActionName)6).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Avatar:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Hunter:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)11).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Alchemist:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)4).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Spotter:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						20f.ToString()
					};
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Purifier:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				}
			}
			else
			{
				text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_" + PlayerCustom.GetNewPrimaryRoleString(playerCustom));
				switch (playerCustom.NewPrimaryRole)
				{
				case PlayerCustom.PlayerNewPrimaryRole.VillageIdiot:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Agent:
					list = new List<string>
					{
						BalancingValues.AgentMaxSurvivorsToWin(PlayerRegistry.Count).ToString(),
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Scientist:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""),
						60f.ToString()
					};
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Lover:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Beast:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""),
						BalancingValues.BeastMarkCooldown.ToString()
					};
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Mercenary:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Voodoo:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "") };
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Kidnapper:
					list = new List<string>
					{
						BalancingValues.KidnapperTargetAmount(PlayerRegistry.Count).ToString(),
						LycansUtility.GetInputDisplayCustom((InputActionName)4).Replace(" -", ""),
						LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Cultist:
					list = new List<string>
					{
						LycansUtility.GetInputDisplayCustom((InputActionName)6).Replace(" -", ""),
						1f.ToString(),
						LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "")
					};
					break;
				case PlayerCustom.PlayerNewPrimaryRole.Zombie:
					list = new List<string> { LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "") };
					break;
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				text = text.Replace("{" + i + "}", list[i]);
			}
		}
		if (playerCustom.SecondaryRole != PlayerCustom.PlayerSecondaryRole.None)
		{
			text2 = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_" + PlayerCustom.GetSecondaryRoleString(playerCustom.SecondaryRole));
			text2 = LycansUtility.ReplaceWithActionNameText(text2, "#PRIMARYACTION", (InputActionName)5);
			text2 = LycansUtility.ReplaceWithActionNameText(text2, "#PRIMARYINTERACT", (InputActionName)3);
			text2 = LycansUtility.ReplaceWithActionNameText(text2, "#SECONDARYROLEPOWER", InputManagerExtra.Instance.Actions["SECONDARYROLEPOWER"]);
			text2 = LycansUtility.ReplaceWithActionNameText(text2, "#ITEMSECONDARY", InputManagerExtra.Instance.Actions["ITEMSECONDARY"]);
			if (playerCustom.SecondaryRolePowerCooldown.HasValue)
			{
				text2 = text2.Replace("#COOLDOWN", playerCustom.SecondaryRolePowerCooldown.Value + "s");
			}
			if (playerCustom.SecondaryRolePowerSecondCooldown.HasValue)
			{
				text2 = text2.Replace("#SECONDCOOLDOWN", playerCustom.SecondaryRolePowerSecondCooldown.Value + "s");
			}
			if (playerCustom.SecondaryRolePowerCastTime.HasValue)
			{
				text2 = text2.Replace("#CAST", playerCustom.SecondaryRolePowerCastTime.Value + "s");
			}
			if (text2.Contains("#PARTNER"))
			{
				PlayerCustom playerCustom2 = PlayerCustomRegistry.AllPlayers.First((PlayerCustom o) => o.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTelepath && o.Ref != PlayerController.Local.Ref);
				PlayerController playerController = playerCustom2.PlayerController;
				text2 = text2.Replace("#PARTNER", ((object)playerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString());
			}
			if (text2.Contains("#HUNTER"))
			{
				PlayerController val = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => (int)o.Role == 2)).FirstOrDefault();
				text2 = ((!((Object)(object)val != (Object)null)) ? text2.Replace("#HUNTER", "---") : text2.Replace("#HUNTER", ((object)val.PlayerData.Username/*cast due to constrained. prefix*/).ToString()));
			}
			PlayerCustom.PlayerSecondaryRole secondaryRole = playerCustom.SecondaryRole;
			PlayerCustom.PlayerSecondaryRole playerSecondaryRole = secondaryRole;
			if (playerSecondaryRole == PlayerCustom.PlayerSecondaryRole.BothMerchant)
			{
				text2 = text2.Replace("#DELAY", 5f.ToString());
			}
		}
		if ((Object)(object)playerCustom.Accessory != (Object)null)
		{
			string text3 = TranslationManager.Instance.GetTranslation(playerCustom.Accessory.DescriptionTranslateKey);
			if (playerCustom.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTinkerer)
			{
				text3 = text3 + " " + TranslationManager.Instance.GetTranslation(playerCustom.Accessory.TinkererDescriptionTranslateKey).Replace("#COOLDOWN", playerCustom.Accessory.TinkererPowerCooldown.ToString());
			}
			text3 = LycansUtility.ReplaceWithActionNameText(text3, "#SECONDARYROLEPOWER", InputManagerExtra.Instance.Actions["SECONDARYROLEPOWER"]);
			text3 = LycansUtility.ReplaceWithActionNameText(text3, "#ITEMSECONDARY", InputManagerExtra.Instance.Actions["ITEMSECONDARY"]);
			text3 = LycansUtility.ReplaceWithActionNameText(text3, "#ACCESSORYACTION", InputManagerExtra.Instance.Actions["ACCESSORYACTION"]);
			text3 = text3.Replace("#COOLDOWN", playerCustom.Accessory.CooldownAfterUse + "s");
			_panelAccessory.SetActive(true);
			((TMP_Text)_descriptionAccessory).text = text3;
		}
		else
		{
			_panelAccessory.SetActive(false);
		}
		if (GameManagerCustom.Instance.EventsManager.CurrentEvent != EventsManager.EventType.None)
		{
			string translation = TranslationManager.Instance.GetTranslation("NALES_EVENT_" + GameManagerCustom.Instance.EventsManager.CurrentEvent.ToString().ToUpper() + "_DESCRIPTION");
			_panelEvent.SetActive(true);
			((TMP_Text)_descriptionEvent).text = translation;
		}
		else
		{
			_panelEvent.SetActive(false);
		}
		if (!string.IsNullOrEmpty(text))
		{
			_panelPrimary.SetActive(true);
			((TMP_Text)_descriptionPrimary).text = text;
			if (text.Contains("<color=purple>"))
			{
				_panelPrimaryWarning.gameObject.SetActive(true);
				((TMP_Text)_panelPrimaryWarning.GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_ACTION_WARNING");
			}
			else if (PlayerCustom.IsPrimaryRolePowerForEliteVillagers(playerCustom.PrimaryRolePower))
			{
				_panelPrimaryWarning.gameObject.SetActive(true);
				((TMP_Text)_panelPrimaryWarning.GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_ELITE_BULLET");
			}
			else
			{
				_panelPrimaryWarning.gameObject.SetActive(false);
			}
		}
		else
		{
			_panelPrimary.SetActive(false);
			_panelPrimaryWarning.SetActive(false);
		}
		if (!string.IsNullOrEmpty(text2))
		{
			_panelSecondary.SetActive(true);
			((TMP_Text)_descriptionSecondary).text = text2;
		}
		else
		{
			_panelSecondary.SetActive(false);
		}
		ShowRoleDescriptionPatch.ShowingExplanation = true;
	}

	public void Hide()
	{
		_panelPrimary.SetActive(false);
		_panelPrimaryWarning.SetActive(false);
		_panelSecondary.SetActive(false);
		_panelAccessory.SetActive(false);
		_panelEvent.SetActive(false);
		ShowRoleDescriptionPatch.ShowingExplanation = false;
	}
}
