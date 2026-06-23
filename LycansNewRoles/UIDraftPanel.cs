using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UIDraftPanel : MonoBehaviour
{
	public static GameObject DraftChoiceRolePrefab;

	private GameObject _panel;

	private TextMeshProUGUI _mainRoleTitle;

	private TextMeshProUGUI _mainRoleName;

	private TextMeshProUGUI _mainRoleDescription;

	private GameObject _draftChoicePanelPrimary;

	private TextMeshProUGUI _draftChoicePanelPrimaryGamepadText;

	private GameObject _draftChoicePanelSecondary;

	private TextMeshProUGUI _draftChoicePanelSecondaryGamepadText;

	private TextMeshProUGUI _waitingForPlayerText;

	private Button _confirmButton;

	private Button _randomButton;

	private List<DraftChoiceRoleComponent> _currentRoles = new List<DraftChoiceRoleComponent>();

	public GameObject _roleOptionAgent;

	public GameObject _roleOptionLover;

	private GameObject _timer;

	private Image _timerImageFill;

	public bool Active = false;

	private Stopwatch _showRolesDescriptionStopwatch = new Stopwatch();

	public List<DraftChoiceRoleComponent> CurrentRoles => _currentRoles;

	private void Start()
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_mainRoleTitle = ((Component)_panel.transform.Find("MainRoleTitle")).GetComponent<TextMeshProUGUI>();
		_mainRoleName = ((Component)_panel.transform.Find("MainRoleName")).GetComponent<TextMeshProUGUI>();
		_mainRoleDescription = ((Component)_panel.transform.Find("MainRoleDescription")).GetComponent<TextMeshProUGUI>();
		_draftChoicePanelPrimary = ((Component)_panel.transform.Find("DraftChoicePanelPrimary")).gameObject;
		_draftChoicePanelSecondary = ((Component)_panel.transform.Find("DraftChoicePanelSecondary")).gameObject;
		_waitingForPlayerText = ((Component)_panel.transform.Find("WaitingForPlayerText")).GetComponent<TextMeshProUGUI>();
		_confirmButton = ((Component)_panel.transform.Find("ConfirmButton")).GetComponent<Button>();
		((UnityEvent)_confirmButton.onClick).AddListener(new UnityAction(OnClickConfirm));
		_randomButton = ((Component)_panel.transform.Find("RandomButton")).GetComponent<Button>();
		((UnityEvent)_randomButton.onClick).AddListener(new UnityAction(OnClickRandom));
		_draftChoicePanelPrimaryGamepadText = ((Component)_panel.transform.Find("DraftChoicePanelPrimaryGamepadText")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_draftChoicePanelPrimaryGamepadText).text = TranslationManager.Instance.GetTranslation("NALES_DRAFT_CHANGE").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)11).Replace(" -", ""));
		_draftChoicePanelSecondaryGamepadText = ((Component)_panel.transform.Find("DraftChoicePanelSecondaryGamepadText")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_draftChoicePanelSecondaryGamepadText).text = TranslationManager.Instance.GetTranslation("NALES_DRAFT_CHANGE").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)6).Replace(" -", ""));
		_timer = ((Component)_panel.transform.Find("Timer")).gameObject;
		_timerImageFill = ((Component)_timer.transform.Find("Fill")).GetComponent<Image>();
		_roleOptionAgent = ((Component)_panel.transform.Find("RoleOptions").Find("OptionAgent")).gameObject;
		_roleOptionLover = ((Component)_panel.transform.Find("RoleOptions").Find("OptionLover")).gameObject;
		_roleOptionAgent.AddComponent<DraftRoleOptionComponent>();
		_roleOptionAgent.GetComponent<DraftRoleOptionComponent>().Init(0, "DRAFT_OPTION_AGENT", "NALES_DRAFT_ROLE_OPTION_AGENT");
		_roleOptionLover.AddComponent<DraftRoleOptionComponent>();
		_roleOptionLover.GetComponent<DraftRoleOptionComponent>().Init(1, "DRAFT_OPTION_LOVER", "NALES_DRAFT_ROLE_OPTION_LOVER");
		_panel.SetActive(false);
		_showRolesDescriptionStopwatch.Stop();
	}

	private void Update()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		if (!Active)
		{
			return;
		}
		TickTimer draftTimer = DraftManager.Instance.DraftTimer;
		if (((TickTimer)(ref draftTimer)).IsRunning)
		{
			Image timerImageFill = _timerImageFill;
			draftTimer = DraftManager.Instance.DraftTimer;
			timerImageFill.fillAmount = ((TickTimer)(ref draftTimer)).RemainingTime(((SimulationBehaviour)GameManager.Instance).Runner).Value / 30f;
			if (!Cursor.visible)
			{
				GameManager.Instance.gameUI.UpdateCursor(true);
			}
		}
		if (!DraftManager.Instance.MyData.SelectionDone)
		{
			if (InputManager.Instance.ItemJustPressed)
			{
				DraftChoiceRoleComponent item = _currentRoles.First((DraftChoiceRoleComponent o) => (o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SoloRole || o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.Power) & o.Selected);
				int currentChoiceIndex = _currentRoles.IndexOf(item);
				DraftChoiceRoleComponent draftChoiceRoleComponent = _currentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => (o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SoloRole || o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.Power) && _currentRoles.IndexOf(o) > currentChoiceIndex);
				if ((Object)(object)draftChoiceRoleComponent != (Object)null)
				{
					draftChoiceRoleComponent.Choose();
					return;
				}
				_currentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SoloRole || o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.Power).Choose();
			}
			else if (InputManager.Instance.SecondaryActionJustPressed)
			{
				DraftChoiceRoleComponent item2 = _currentRoles.First((DraftChoiceRoleComponent o) => (o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SecondaryRole) & o.Selected);
				int currentChoiceIndex2 = _currentRoles.IndexOf(item2);
				DraftChoiceRoleComponent draftChoiceRoleComponent2 = _currentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SecondaryRole && _currentRoles.IndexOf(o) > currentChoiceIndex2);
				if ((Object)(object)draftChoiceRoleComponent2 != (Object)null)
				{
					draftChoiceRoleComponent2.Choose();
					return;
				}
				_currentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SecondaryRole).Choose();
			}
			else if (InputManager.Instance.CrouchJustPressed)
			{
				OnClickConfirm();
			}
			else if (InputManager.Instance.SprintJustPressed)
			{
				OnClickRandom();
			}
		}
		else if (_showRolesDescriptionStopwatch.IsRunning && _showRolesDescriptionStopwatch.ElapsedMilliseconds >= 500)
		{
			_showRolesDescriptionStopwatch.Reset();
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			UIManager.RoleDescription.Show(player);
		}
	}

	public void Show()
	{
		_panel.SetActive(true);
		GameManager.Instance.gameUI.CloseSettings();
		GameManager.Instance.gameUI.UpdateCursor(true);
		GameManager.Instance.gameUI.ShowGameMenu(false);
		GameManager.Instance.gameUI.ShowRole(false);
		GameManager.Instance.previewCamera.DisableCamera();
		PlayerRegistry.ForEach((Action<PlayerController>)delegate(PlayerController p)
		{
			p.EnablePlayerInteraction(false);
		});
		Active = true;
	}

	public void Hide()
	{
		DestroyButtons();
		_panel.SetActive(false);
		Active = false;
	}

	public void UpdateInfo()
	{
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		if (!DraftManager.Instance.MyData.SelectionDone)
		{
			AudioManager.Play("KILL_5", (MixerTarget)2, 0.2f, 1f);
			((Component)_mainRoleTitle).gameObject.SetActive(true);
			((Component)_mainRoleName).gameObject.SetActive(true);
			((Component)_mainRoleDescription).gameObject.SetActive(true);
			_draftChoicePanelPrimary.gameObject.SetActive(true);
			_draftChoicePanelSecondary.gameObject.SetActive(true);
			((Component)_waitingForPlayerText).gameObject.SetActive(false);
			_roleOptionAgent.SetActive(true);
			_roleOptionLover.SetActive(true);
			((Component)_confirmButton).gameObject.SetActive(true);
			((Component)_randomButton).gameObject.SetActive(true);
			((Component)_draftChoicePanelPrimaryGamepadText).gameObject.SetActive(InputManager.Instance.IsGamepad());
			((Component)_draftChoicePanelSecondaryGamepadText).gameObject.SetActive(InputManager.Instance.IsGamepad());
			((TMP_Text)_mainRoleTitle).text = TranslationManager.Instance.GetTranslation("NALES_DRAFT_MAIN_ROLE_TITLE");
			((TMP_Text)_mainRoleName).text = TranslationManager.Instance.GetTranslation("NALES_DRAFT_MAIN_ROLE_NAME_" + GetMainRoleSuffix(DraftManager.Instance.MyData.MainRole));
			((Graphic)_mainRoleName).color = GetMainRoleColor(DraftManager.Instance.MyData.MainRole);
			((TMP_Text)_mainRoleDescription).text = TranslationManager.Instance.GetTranslation("NALES_DRAFT_MAIN_ROLE_DESCRIPTION_" + GetMainRoleSuffix(DraftManager.Instance.MyData.MainRole));
			string translation = TranslationManager.Instance.GetTranslation("NALES_DRAFT_BUTTON_CONFIRM");
			string translation2 = TranslationManager.Instance.GetTranslation("NALES_DRAFT_RANDOM");
			if (InputManager.Instance.IsGamepad())
			{
				translation = translation.Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)8).Replace(" -", ""));
				translation2 = translation2.Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)7).Replace(" -", ""));
			}
			else
			{
				translation = translation.Replace("{0} = ", "");
				translation2 = translation2.Replace("{0} = ", "");
			}
			((TMP_Text)((Component)_confirmButton).GetComponentInChildren<TextMeshProUGUI>()).text = translation;
			((TMP_Text)((Component)_randomButton).GetComponentInChildren<TextMeshProUGUI>()).text = translation2;
			DestroyButtons();
			DraftManager.DraftPlayerMainRole mainRole = DraftManager.Instance.MyData.MainRole;
			DraftManager.DraftPlayerMainRole draftPlayerMainRole = mainRole;
			if (draftPlayerMainRole == DraftManager.DraftPlayerMainRole.Solo)
			{
				foreach (PlayerCustom.PlayerNewPrimaryRole offeredSoloRole in DraftManager.Instance.MyData.OfferedSoloRoles)
				{
					GameObject val = Object.Instantiate<GameObject>(DraftChoiceRolePrefab, _draftChoicePanelPrimary.transform);
					val.SetActive(true);
					val.GetComponent<DraftChoiceRoleComponent>().Init(DraftChoiceRoleComponent.DraftChoiceRoleType.SoloRole, (int)offeredSoloRole, PlayerCustom.GetNewPrimaryRoleString(offeredSoloRole));
					_currentRoles.Add(val.GetComponent<DraftChoiceRoleComponent>());
				}
			}
			else
			{
				foreach (PlayerCustom.PlayerPrimaryRolePower offeredPower in DraftManager.Instance.MyData.OfferedPowers)
				{
					GameObject val2 = Object.Instantiate<GameObject>(DraftChoiceRolePrefab, _draftChoicePanelPrimary.transform);
					val2.SetActive(true);
					val2.GetComponent<DraftChoiceRoleComponent>().Init(DraftChoiceRoleComponent.DraftChoiceRoleType.Power, (int)offeredPower, PlayerCustom.GetPrimaryRolePowerString(offeredPower));
					_currentRoles.Add(val2.GetComponent<DraftChoiceRoleComponent>());
				}
			}
			foreach (PlayerCustom.PlayerSecondaryRole offeredSecondaryRole in DraftManager.Instance.MyData.OfferedSecondaryRoles)
			{
				GameObject val3 = Object.Instantiate<GameObject>(DraftChoiceRolePrefab, _draftChoicePanelSecondary.transform);
				val3.SetActive(true);
				string text = PlayerCustom.GetSecondaryRoleString(offeredSecondaryRole);
				if (DraftManager.Instance.MyData.MainRole == DraftManager.DraftPlayerMainRole.Wolf || DraftManager.Instance.MyData.MainRole == DraftManager.DraftPlayerMainRole.WolfPup)
				{
					text += "_WOLF";
				}
				val3.GetComponent<DraftChoiceRoleComponent>().Init(DraftChoiceRoleComponent.DraftChoiceRoleType.SecondaryRole, (int)offeredSecondaryRole, text);
				_currentRoles.Add(val3.GetComponent<DraftChoiceRoleComponent>());
			}
			_currentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SoloRole)?.Choose();
			_currentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.Power)?.Choose();
			ChooseDefaultSecondaryRoleIfNeeded();
		}
		else
		{
			HideDraftInfo();
		}
	}

	private void HideDraftInfo()
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		((Component)_mainRoleTitle).gameObject.SetActive(false);
		((Component)_mainRoleName).gameObject.SetActive(false);
		((Component)_mainRoleDescription).gameObject.SetActive(false);
		_draftChoicePanelPrimary.gameObject.SetActive(false);
		_draftChoicePanelSecondary.gameObject.SetActive(false);
		((Component)_draftChoicePanelPrimaryGamepadText).gameObject.SetActive(false);
		((Component)_draftChoicePanelSecondaryGamepadText).gameObject.SetActive(false);
		((Component)_waitingForPlayerText).gameObject.SetActive(true);
		_roleOptionAgent.SetActive(true);
		_roleOptionLover.SetActive(true);
		((Component)_confirmButton).gameObject.SetActive(false);
		((Component)_randomButton).gameObject.SetActive(false);
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		((TMP_Text)_waitingForPlayerText).text = TranslationManager.Instance.GetTranslation("NALES_DRAFT_WAITING_FOR_PLAYER_CHOICE");
		if (DraftManager.Instance.MyData.SelectionDone)
		{
			_showRolesDescriptionStopwatch.Start();
		}
	}

	public void ChooseDefaultSecondaryRoleIfNeeded()
	{
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		DraftChoiceRoleComponent draftChoiceRoleComponent = UIManager.DraftPanel.CurrentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SecondaryRole && o.Selected);
		if (!((Object)(object)draftChoiceRoleComponent == (Object)null) && draftChoiceRoleComponent.Available)
		{
			return;
		}
		DraftChoiceRoleComponent draftChoiceRoleComponent2 = _currentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SecondaryRole && o.Available);
		if ((Object)(object)draftChoiceRoleComponent2 != (Object)null)
		{
			draftChoiceRoleComponent2.Choose();
			return;
		}
		foreach (DraftChoiceRoleComponent item in _currentRoles.Where((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SecondaryRole))
		{
			((Graphic)item.Button.GetComponent<Image>()).color = Color.red;
			item.Selected = false;
		}
	}

	private void OnClickConfirm()
	{
		DraftChoiceRoleComponent draftChoiceRoleComponent = UIManager.DraftPanel.CurrentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => (o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.Power || o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SoloRole) && o.Selected);
		DraftChoiceRoleComponent draftChoiceRoleComponent2 = UIManager.DraftPanel.CurrentRoles.FirstOrDefault((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleComponent.DraftChoiceRoleType.SecondaryRole && o.Selected);
		int num = (((Object)(object)draftChoiceRoleComponent != (Object)null) ? draftChoiceRoleComponent.RoleIndex : (-1));
		int num2 = (((Object)(object)draftChoiceRoleComponent2 != (Object)null) ? draftChoiceRoleComponent2.RoleIndex : (-1));
		DraftManager.Rpc_Confirm(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index);
		DraftManager.Instance.MyData.SelectionDone = true;
		UpdateInfo();
	}

	private void OnClickRandom()
	{
		DraftManager.Rpc_Random(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index);
		DraftManager.Instance.MyData.SelectionDone = true;
		UpdateInfo();
	}

	private void DestroyButtons()
	{
		foreach (DraftChoiceRoleComponent currentRole in _currentRoles)
		{
			Object.Destroy((Object)(object)((Component)currentRole).gameObject);
		}
		_currentRoles.Clear();
	}

	public static string GetMainRoleSuffix(DraftManager.DraftPlayerMainRole mainRole)
	{
		return mainRole switch
		{
			DraftManager.DraftPlayerMainRole.NormalVillager => "VILLAGER", 
			DraftManager.DraftPlayerMainRole.EliteVillager => "ELITE", 
			DraftManager.DraftPlayerMainRole.Wolf => "WOLF", 
			DraftManager.DraftPlayerMainRole.Traitor => "TRAITOR", 
			DraftManager.DraftPlayerMainRole.WolfPup => "PUP", 
			DraftManager.DraftPlayerMainRole.Solo => "SOLO", 
			_ => "", 
		};
	}

	private static Color GetMainRoleColor(DraftManager.DraftPlayerMainRole mainRole)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(mainRole switch
		{
			DraftManager.DraftPlayerMainRole.NormalVillager => GameUI.VillagerColor, 
			DraftManager.DraftPlayerMainRole.EliteVillager => GameConfig.ColorElite, 
			DraftManager.DraftPlayerMainRole.Wolf => GameUI.WolfColor, 
			DraftManager.DraftPlayerMainRole.Traitor => PlayerCustom.NewPrimaryRoleTraitorColor, 
			DraftManager.DraftPlayerMainRole.WolfPup => GameConfig.ColorWolfPup, 
			DraftManager.DraftPlayerMainRole.Solo => PlayerCustom.NewPrimaryRoleAgentColor, 
			_ => Color.white, 
		});
	}
}
