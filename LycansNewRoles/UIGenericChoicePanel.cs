using System.Collections.Generic;
using System.Linq;
using BepInEx.Logging;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UIGenericChoicePanel : MonoBehaviour
{
	public static GameObject GenericChoiceButtonPrefab;

	private GameObject _headerPanel;

	private GameObject _panel;

	private GameObject _keysPanel;

	private List<GameObject> _buttons = new List<GameObject>();

	private PlayerRef _currentTarget = PlayerRef.None;

	public bool Active = false;

	private void Start()
	{
		_headerPanel = ((Component)((Component)this).transform.Find("HeaderPanel")).gameObject;
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_keysPanel = ((Component)((Component)this).transform.Find("KeysPanel")).gameObject;
		((TMP_Text)((Component)_keysPanel.transform.Find("TextNext")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_UI_PANEL_NEXT").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)11).Replace(" -", ""));
		((TMP_Text)((Component)_keysPanel.transform.Find("TextChoose")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_UI_PANEL_CHOOSE").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)8).Replace(" -", ""));
		_headerPanel.SetActive(false);
		_panel.SetActive(false);
		_keysPanel.SetActive(false);
	}

	private void Update()
	{
		if (Active)
		{
			if (InputManager.Instance.ItemJustPressed)
			{
				NextButton();
			}
			else if (InputManager.Instance.CrouchJustPressed)
			{
				Confirm();
			}
		}
	}

	public void Show(List<GameObject> buttons, string headerTextKey)
	{
		_headerPanel.SetActive(true);
		((TMP_Text)((Component)_headerPanel.transform.Find("Text")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation(headerTextKey);
		_panel.SetActive(true);
		_panel.SetActive(true);
		_keysPanel.SetActive(InputManager.Instance.IsGamepad());
		DestroyButtons();
		for (int i = 0; i < buttons.Count; i++)
		{
			buttons[i].transform.SetParent(_panel.transform);
			buttons[i].GetComponent<UIGenericChoiceButton>().Init(i, i == 0);
			_buttons.Add(buttons[i]);
		}
		Active = true;
	}

	public void Hide()
	{
		DestroyButtons();
		_headerPanel.SetActive(false);
		_panel.SetActive(false);
		_keysPanel.SetActive(false);
		Active = false;
	}

	private void NextButton()
	{
		GameObject val = _buttons.FirstOrDefault((GameObject o) => o.GetComponent<UIGenericChoiceButton>().Selected);
		int index = val.GetComponent<UIGenericChoiceButton>().Index;
		index = ((index != _buttons.Count - 1) ? (index + 1) : 0);
		foreach (GameObject button in _buttons)
		{
			UIGenericChoiceButton component = button.GetComponent<UIGenericChoiceButton>();
			component.ToggleSelected(component.Index == index);
		}
	}

	private void Confirm()
	{
		_buttons.First((GameObject o) => o.GetComponent<UIGenericChoiceButton>().Selected).GetComponent<UIGenericChoiceButton>().ClickAction.Invoke();
	}

	private void DestroyButtons()
	{
		foreach (GameObject button in _buttons)
		{
			Object.Destroy((Object)(object)button);
		}
		_buttons.Clear();
	}

	public static List<GameObject> GetButtonsForPlayers(List<PlayerRef> players)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		List<GameObject> list = new List<GameObject>();
		Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
		List<PlayerRef> list2 = (from o in value
			orderby ((Component)o.Value).gameObject.transform.position.y descending
			select o.Key).Where(players.Contains).ToList();
		foreach (PlayerRef item in list2)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(item);
			GameObject val = Object.Instantiate<GameObject>(GenericChoiceButtonPrefab);
			val.SetActive(true);
			((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
			((Graphic)val.GetComponent<Image>()).color = ColorManager.GetColor(player.ColorIndex);
			((Behaviour)val.GetComponent<Button>()).enabled = true;
			UIGenericChoiceButtonPlayer uIGenericChoiceButtonPlayer = val.AddComponent<UIGenericChoiceButtonPlayer>();
			uIGenericChoiceButtonPlayer.PlayerRef = item;
			list.Add(val);
		}
		return list;
	}

	public static List<GameObject> GetButtonsForCrystalBallGuess()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (PlayerCustom.PlayerPrimaryRolePower item2 in PlayerCustom.AllVillagerJobs.Where((PlayerCustom.PlayerPrimaryRolePower o) => !PlayerCustom.IsPrimaryRolePowerDisabled(o) && o != PlayerCustom.PlayerPrimaryRolePower.Avatar && o != PlayerCustom.PlayerPrimaryRolePower.Mole))
		{
			GameObject item = CreateButtonForCrystalBallGuess(item2);
			list.Add(item);
		}
		return list;
	}

	private static GameObject CreateButtonForCrystalBallGuess(PlayerCustom.PlayerPrimaryRolePower power)
	{
		GameObject val = Object.Instantiate<GameObject>(GenericChoiceButtonPrefab);
		val.SetActive(true);
		((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetPrimaryRolePowerString(power));
		Button component = val.GetComponent<Button>();
		((Behaviour)component).enabled = true;
		((Selectable)component).interactable = true;
		UIGenericChoiceButtonCrystalBallGuessRole uIGenericChoiceButtonCrystalBallGuessRole = val.AddComponent<UIGenericChoiceButtonCrystalBallGuessRole>();
		uIGenericChoiceButtonCrystalBallGuessRole.VillagerJob = power;
		return val;
	}

	public static List<GameObject> GetButtonsForRitualist()
	{
		List<GameObject> list = new List<GameObject>();
		EffectManager value = Traverse.Create(typeof(EffectManager)).Field<EffectManager>("_instance").Value;
		Effect[] value2 = Traverse.Create((object)value).Field<Effect[]>("effects").Value;
		Effect val = value2.FirstOrDefault((Effect o) => o is MuteEffect);
		if ((Object)(object)val != (Object)null)
		{
			list.Add(GetRitualistEffectButton(val, 55f));
		}
		Effect val2 = value2.FirstOrDefault((Effect o) => o is DeafnessEffect);
		if ((Object)(object)val2 != (Object)null)
		{
			list.Add(GetRitualistEffectButton(val2, 20f));
		}
		Effect val3 = value2.FirstOrDefault((Effect o) => o is ParanoiaEffect);
		if ((Object)(object)val3 != (Object)null)
		{
			list.Add(GetRitualistEffectButton(val3, 12f));
		}
		Effect val4 = value2.FirstOrDefault((Effect o) => o is FlatulenceEffect);
		if ((Object)(object)val4 != (Object)null)
		{
			list.Add(GetRitualistEffectButton(val4, 20f));
		}
		Effect val5 = value2.FirstOrDefault((Effect o) => o is NearsightedEffect);
		if ((Object)(object)val5 != (Object)null)
		{
			list.Add(GetRitualistEffectButton(val5, 25f));
		}
		Effect val6 = value2.FirstOrDefault((Effect o) => o is ConfusedEffect);
		if ((Object)(object)val6 != (Object)null)
		{
			list.Add(GetRitualistEffectButton(val6, 15f));
		}
		return list;
	}

	private static GameObject GetRitualistEffectButton(Effect effect, float duration)
	{
		GameObject val = Object.Instantiate<GameObject>(GenericChoiceButtonPrefab);
		val.SetActive(true);
		((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_UI_RITUALIST_PANEL_OPTION").Replace("{0}", TranslationManager.Instance.GetTranslation(effect.GetTranslateKey())).Replace("{1}", duration.ToString());
		Button component = val.GetComponent<Button>();
		((Behaviour)component).enabled = true;
		((Selectable)component).interactable = true;
		UIGenericChoiceButtonRitualistEffect uIGenericChoiceButtonRitualistEffect = val.AddComponent<UIGenericChoiceButtonRitualistEffect>();
		uIGenericChoiceButtonRitualistEffect.Effect = effect;
		uIGenericChoiceButtonRitualistEffect.Duration = duration;
		return val;
	}

	public static List<GameObject> GetButtonsForImitator(PlayerCustom playerCustom)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		List<GameObject> list = new List<GameObject>();
		List<PlayerCustom.PlayerSecondaryRole> list2 = new List<PlayerCustom.PlayerSecondaryRole>();
		if (playerCustom.ImitatorChoicesForToday.Any())
		{
			list2 = playerCustom.ImitatorChoicesForToday;
		}
		else
		{
			List<PlayerCustom.PlayerSecondaryRole> availableSecondaryRoles = PlayerCustom.GetAvailableSecondaryRoles(playerCustom.PlayerController.Role, playerCustom.NewPrimaryRole, playerCustom.PrimaryRolePower);
			availableSecondaryRoles.RemoveAll((PlayerCustom.PlayerSecondaryRole o) => PlayerCustom.IsSecondaryRoleDisabled(o) || o == PlayerCustom.PlayerSecondaryRole.BothImitator || o == PlayerCustom.PlayerSecondaryRole.BothMedium || o == PlayerCustom.PlayerSecondaryRole.BothMerchant || o == PlayerCustom.PlayerSecondaryRole.BothTelepath);
			list2 = CollectionsUtil.Grab<PlayerCustom.PlayerSecondaryRole>(availableSecondaryRoles, 4).ToList();
			playerCustom.ImitatorChoicesForToday.AddRange(list2);
		}
		foreach (PlayerCustom.PlayerSecondaryRole item in list2)
		{
			string translation = TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetSecondaryRoleString(item));
			GameObject val = Object.Instantiate<GameObject>(GenericChoiceButtonPrefab);
			val.SetActive(true);
			((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = translation;
			Button component = val.GetComponent<Button>();
			((Behaviour)component).enabled = true;
			((Selectable)component).interactable = true;
			UIGenericChoiceButtonImitatorRole uIGenericChoiceButtonImitatorRole = val.AddComponent<UIGenericChoiceButtonImitatorRole>();
			uIGenericChoiceButtonImitatorRole.Role = item;
			list.Add(val);
		}
		return list;
	}

	public static List<GameObject> GetButtonsForMerchant(List<MerchantOffer> offers)
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		List<GameObject> list = new List<GameObject>();
		foreach (MerchantOffer offer in offers.OrderBy((MerchantOffer o) => o.Index))
		{
			GameObject val = Object.Instantiate<GameObject>(GenericChoiceButtonPrefab);
			val.SetActive(true);
			string text = "(" + offer.Price + "$) ";
			switch (offer.Type)
			{
			case MerchantOffer.MerchantOfferType.Scroll:
			{
				Effect effect2 = EffectManager.GetEffect(offer.TypeIndex.Value);
				((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = text + TranslationManager.Instance.GetTranslation("NALES_UI_MERCHANT_OFFER_SCROLL").Replace("{0}", TranslationManager.Instance.GetTranslation(effect2.GetTranslateKey()));
				((Graphic)val.GetComponent<Image>()).color = Color.white;
				break;
			}
			case MerchantOffer.MerchantOfferType.OtherGadget:
			{
				string key = "";
				switch (offer.TypeIndex.Value)
				{
				case 0:
					key = "NALES_GADGET_LOCK";
					break;
				case 1:
					key = "NALES_GADGET_TRAP";
					break;
				case 2:
					key = "NALES_GADGET_SMOKE";
					break;
				case 3:
					key = "NALES_GADGET_SPYGLASS";
					break;
				case 4:
					key = "NALES_GADGET_DIAMOND";
					break;
				case 5:
					key = "NALES_GADGET_GRENADE";
					break;
				case 6:
					key = "NALES_GADGET_SLEEPING_GAS";
					break;
				case 7:
					key = "NALES_GADGET_MOLOTOV";
					break;
				case 8:
					key = "NALES_GADGET_RADAR";
					break;
				}
				((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = text + TranslationManager.Instance.GetTranslation("NALES_UI_MERCHANT_OFFER_GADGET").Replace("{0}", TranslationManager.Instance.GetTranslation(key));
				((Graphic)val.GetComponent<Image>()).color = Color.gray;
				break;
			}
			case MerchantOffer.MerchantOfferType.Potion:
			{
				Effect val2 = EffectManager.GetEffects().FirstOrDefault((Effect o) => BalancingValues.GetModifiedEffectData(o).RealIndex == offer.TypeIndex.Value);
				if ((Object)(object)val2 != (Object)null)
				{
					((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = text + TranslationManager.Instance.GetTranslation("NALES_UI_MERCHANT_OFFER_POTION").Replace("{0}", TranslationManager.Instance.GetTranslation(val2.GetTranslateKey()));
				}
				else
				{
					ManualLogSource logger = Plugin.Logger;
					int? typeIndex = offer.TypeIndex;
					logger.LogError((object)("BUG5: merchant potion not found, offer.TypeIndex: " + typeIndex));
					foreach (Effect effect3 in EffectManager.GetEffects())
					{
						Plugin.Logger.LogError((object)("Effect in list: " + (object)effect3));
					}
					((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = text + TranslationManager.Instance.GetTranslation("NALES_UI_MERCHANT_OFFER_POTION").Replace("{0}", "???");
				}
				((Graphic)val.GetComponent<Image>()).color = Color.yellow;
				break;
			}
			case MerchantOffer.MerchantOfferType.ImmediateEffect:
			{
				Effect effect = EffectManager.GetEffect(offer.TypeIndex.Value);
				((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = text + TranslationManager.Instance.GetTranslation("NALES_UI_MERCHANT_OFFER_EFFECT").Replace("{0}", TranslationManager.Instance.GetTranslation(effect.GetTranslateKey()));
				((Graphic)val.GetComponent<Image>()).color = Color.magenta;
				break;
			}
			case MerchantOffer.MerchantOfferType.PriestProtection:
				((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = text + TranslationManager.Instance.GetTranslation("NALES_UI_MERCHANT_OFFER_PROTECTION");
				((Graphic)val.GetComponent<Image>()).color = Color.cyan;
				break;
			case MerchantOffer.MerchantOfferType.Heal:
				((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = text + TranslationManager.Instance.GetTranslation("NALES_UI_MERCHANT_OFFER_HEAL").Replace("{0}", Mathf.FloorToInt(50f).ToString());
				((Graphic)val.GetComponent<Image>()).color = Color.green;
				break;
			}
			((Behaviour)val.GetComponent<Button>()).enabled = PlayerCustom.Local.SecondaryRoleUniqueInt >= offer.Price;
			UIGenericChoiceButtonMerchantOffer uIGenericChoiceButtonMerchantOffer = val.AddComponent<UIGenericChoiceButtonMerchantOffer>();
			uIGenericChoiceButtonMerchantOffer.Offer = offer;
			list.Add(val);
		}
		GameObject val3 = Object.Instantiate<GameObject>(GenericChoiceButtonPrefab);
		val3.SetActive(true);
		((TMP_Text)val3.GetComponentInChildren<TextMeshProUGUI>()).text = "(" + 3 + "$) " + TranslationManager.Instance.GetTranslation("NALES_UI_MERCHANT_REROLL");
		((Graphic)val3.GetComponent<Image>()).color = Color.red;
		((Behaviour)val3.GetComponent<Button>()).enabled = PlayerCustom.Local.SecondaryRoleUniqueInt >= 3;
		UIGenericChoiceButtonMerchantOffer uIGenericChoiceButtonMerchantOffer2 = val3.AddComponent<UIGenericChoiceButtonMerchantOffer>();
		uIGenericChoiceButtonMerchantOffer2.Offer = new MerchantOffer
		{
			Index = -1
		};
		list.Add(val3);
		return list;
	}

	public void RefreshMerchantOffers(List<MerchantOffer> offers)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		List<GameObject> buttonsForMerchant = GetButtonsForMerchant(offers);
		foreach (GameObject playerButton in buttonsForMerchant)
		{
			playerButton.GetComponent<UIGenericChoiceButtonMerchantOffer>().SetAction((UnityAction)delegate
			{
				OnClickMerchantOffer(playerButton.GetComponent<UIGenericChoiceButtonMerchantOffer>().Offer);
			});
		}
		Show(buttonsForMerchant, "NALES_UI_CHOICE_PANEL_HEADER_MERCHANT");
	}

	public void OnClickCrystalBallGuessTarget(PlayerRef target)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Hide();
		List<GameObject> buttonsForCrystalBallGuess = GetButtonsForCrystalBallGuess();
		foreach (GameObject jobButton in buttonsForCrystalBallGuess)
		{
			jobButton.GetComponent<UIGenericChoiceButtonCrystalBallGuessRole>().SetAction((UnityAction)delegate
			{
				UIManager.GenericChoicePanel.OnClickCrystalBallGuessRole(jobButton.GetComponent<UIGenericChoiceButtonCrystalBallGuessRole>().VillagerJob);
			});
		}
		_currentTarget = target;
		Show(buttonsForCrystalBallGuess, "NALES_UI_CHOICE_PANEL_HEADER_SEER_JOB");
	}

	public void OnClickCrystalBallGuessRole(PlayerCustom.PlayerPrimaryRolePower power)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		int powerIndex = PlayerCustom.AllJobs.IndexOf(power);
		PlayerCustom.Rpc_Try_Guess_Power(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index, PlayerRegistry.GetPlayer(_currentTarget).Index, powerIndex);
	}

	public void OnClickShapeshiftingTarget(PlayerRef target)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Hide();
		GameManager.Instance.gameUI.UpdateCursor(false);
		PlayerCustom.Rpc_Shapeshift(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index, PlayerRegistry.GetPlayer(target).Index);
		PlayerCustom.PlaySuccessSound();
	}

	public void OnClickRitualistEffect(Effect effect)
	{
		Hide();
		GameManager.Instance.gameUI.UpdateCursor(false);
		int effectIndex = EffectManager.GetEffectIndex(effect);
		PlayerCustom.Rpc_Ritualist_Ritual(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index, effectIndex);
	}

	public void OnClickPredatorTarget(PlayerRef target)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Hide();
		GameManager.Instance.gameUI.UpdateCursor(false);
		PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index, PlayerRegistry.GetPlayer(target).Index);
		PlayerCustom.PlaySuccessSound();
	}

	public void OnClickImitatorRole(PlayerCustom.PlayerSecondaryRole secondaryRole)
	{
		Hide();
		GameManager.Instance.gameUI.UpdateCursor(false);
		PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index, (int)secondaryRole);
	}

	public void OnClickMerchantOffer(MerchantOffer offer)
	{
		if (offer.Index > -1)
		{
			PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index, offer.Index);
			Hide();
			GameManager.Instance.gameUI.UpdateCursor(false);
		}
		else
		{
			PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index);
		}
	}
}
