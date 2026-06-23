using System.Collections.Generic;
using Fusion;
using Managers;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIMayorPanelForMayor : MonoBehaviour
{
	public static GameObject MayorActionPrefab;

	private GameObject _panel;

	private TextMeshProUGUI _textMayor;

	private TextMeshProUGUI _textCooldown;

	private GameObject _actionsContainer;

	private List<UIMayorAction> _actions = new List<UIMayorAction>();

	public bool Active = false;

	public List<UIMayorAction> Actions => _actions;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_textMayor = ((Component)_panel.transform.Find("MayorText")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)((Component)_panel.transform.Find("ChangeActionText")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_MAYOR_CHANGE_ACTION").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)11).Replace(" -", ""));
		_actionsContainer = ((Component)_panel.transform.Find("ActionsContainer")).gameObject;
		_textCooldown = ((Component)_panel.transform.Find("CooldownText")).GetComponent<TextMeshProUGUI>();
		_panel.SetActive(false);
		AddAction(0, "NALES_MAYOR_CHOICE_STUN");
		AddAction(1, "NALES_MAYOR_CHOICE_LISTEN");
		AddAction(2, "NALES_MAYOR_CHOICE_SPEECH");
		AddAction(3, "NALES_MAYOR_CHOICE_NEW_MAYOR");
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Invalid comparison between Unknown and I4
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState == 0)
		{
			return;
		}
		if ((Object)(object)Plugin.CustomConfig == (Object)null || !NetworkBool.op_Implicit(Plugin.CustomConfig.AllowMayor) || (int)GameManager.LocalGameState != 4 || GameManagerCustom.Instance.CurrentMayor != PlayerController.Local.Ref || GameManager.Instance.gameUI.IsSettingMenuOpen || GameManager.Instance.gameUI.IsGameSettingMenuOpen)
		{
			if (Active)
			{
				Hide();
			}
			return;
		}
		if (!Active)
		{
			Show();
		}
		if (Active)
		{
			UpdateCooldown();
		}
	}

	public void UpdateMayor(PlayerRef mayorRef)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(mayorRef);
		((TMP_Text)_textMayor).text = TranslationManager.Instance.GetTranslation("NALES_MAYOR_NAME").Replace("{0}", "<color=#" + ColorUtility.ToHtmlStringRGB(ColorManager.GetColor(player.ColorIndex)) + ">" + ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</color>");
	}

	public void AddAction(int actionIndex, string translationKey)
	{
		GameObject val = Object.Instantiate<GameObject>(MayorActionPrefab);
		val.SetActive(true);
		val.transform.SetParent(_actionsContainer.transform);
		UIMayorAction component = val.GetComponent<UIMayorAction>();
		component.Init(actionIndex, TranslationManager.Instance.GetTranslation(translationKey));
		_actions.Add(component);
	}

	public void SetActiveAction(int actionIndex)
	{
		for (int i = 0; i < _actions.Count; i++)
		{
			_actions[i].UpdateAction(i == actionIndex);
		}
	}

	public void UpdateCooldown()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		TickTimer mayorActionCooldownTimer = GameManagerCustom.Instance.MayorActionCooldownTimer;
		if (!((TickTimer)(ref mayorActionCooldownTimer)).IsRunning)
		{
			((TMP_Text)_textCooldown).text = "<color=green>" + TranslationManager.Instance.GetTranslation("NALES_MAYOR_AVAILABLE") + "</color>";
			return;
		}
		TextMeshProUGUI textCooldown = _textCooldown;
		string translation = TranslationManager.Instance.GetTranslation("NALES_MAYOR_COOLDOWN");
		mayorActionCooldownTimer = GameManagerCustom.Instance.MayorActionCooldownTimer;
		((TMP_Text)textCooldown).text = "<color=red>" + translation.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref mayorActionCooldownTimer)).RemainingTime(((SimulationBehaviour)GameManagerCustom.Instance).Runner).Value).ToString()) + "</color>";
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
}
