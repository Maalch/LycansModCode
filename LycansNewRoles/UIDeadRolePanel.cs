using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using Managers;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIDeadRolePanel : MonoBehaviour
{
	public enum PossibleAction
	{
		None,
		HealAndProtect,
		GiveRandomDebuff,
		GivePower
	}

	public static Color VillagerColor = new Color(0f, 0.5f, 1f, 1f);

	public static Color TraitorColor = new Color(1f, 0.5f, 0f, 1f);

	public static Color WolfColor = new Color(1f, 0f, 0f, 1f);

	public static Color SoloRoleColor = new Color(1f, 0f, 1f, 1f);

	private GameObject _panel;

	private TextMeshProUGUI _textRole;

	private GameObject _angelParentObject;

	private TextMeshProUGUI _textTargetTitle;

	private TextMeshProUGUI _textTargetValue;

	private TextMeshProUGUI _textAction;

	private TextMeshProUGUI _textCooldown;

	private GameObject _ghostParentObject;

	private TextMeshProUGUI _ghostAttackText;

	private TextMeshProUGUI _ghostSpellText;

	private GameObject _specterParentObject;

	private TextMeshProUGUI _specterAttackText;

	private TextMeshProUGUI _specterSpellText;

	public bool Active = false;

	private PossibleAction _currentPossibleAction;

	public PossibleAction CurrentPossibleAction => _currentPossibleAction;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_textRole = ((Component)_panel.transform.Find("RoleText")).GetComponent<TextMeshProUGUI>();
		_angelParentObject = ((Component)_panel.transform.Find("Angel")).gameObject;
		_textTargetTitle = ((Component)_angelParentObject.transform.Find("TargetTitleText")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_textTargetTitle).text = TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_TITLE");
		_textTargetValue = ((Component)_angelParentObject.transform.Find("TargetValueText")).GetComponent<TextMeshProUGUI>();
		_textAction = ((Component)_angelParentObject.transform.Find("ActionText")).GetComponent<TextMeshProUGUI>();
		_textCooldown = ((Component)_angelParentObject.transform.Find("CooldownText")).GetComponent<TextMeshProUGUI>();
		_ghostParentObject = ((Component)_panel.transform.Find("Ghost")).gameObject;
		_ghostAttackText = ((Component)_ghostParentObject.transform.Find("AttackText")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_ghostAttackText).text = TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_GHOST_ATTACK").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""));
		_ghostSpellText = ((Component)_ghostParentObject.transform.Find("SpellText")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_ghostSpellText).text = TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_GHOST_SPELL").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""));
		_specterParentObject = ((Component)_panel.transform.Find("Specter")).gameObject;
		_specterAttackText = ((Component)_specterParentObject.transform.Find("AttackText")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_specterAttackText).text = TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_SPECTER_ATTACK").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", ""));
		_specterSpellText = ((Component)_specterParentObject.transform.Find("SpellText")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_specterSpellText).text = TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_SPECTER_SPELL").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", ""));
		_panel.SetActive(false);
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState == 2 && NetworkBool.op_Implicit(PlayerController.Local.IsDead) && !GameManager.Instance.gameUI.IsSettingMenuOpen && !GameManager.Instance.gameUI.IsGameSettingMenuOpen && (PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Angel || PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost || PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Specter))
		{
			if (!Active)
			{
				Show();
			}
			Refresh();
		}
		else if (Active)
		{
			Hide();
		}
	}

	public void Show()
	{
		_panel.SetActive(true);
		_angelParentObject.SetActive(PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Angel);
		_ghostParentObject.SetActive(PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost);
		_specterParentObject.SetActive(PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Specter);
		Active = true;
	}

	public void Hide()
	{
		_panel.SetActive(false);
		Active = false;
	}

	public void Refresh()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Invalid comparison between Unknown and I4
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom povPlayerCustom = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		switch (PlayerCustom.Local.PrimaryRolePower)
		{
		case PlayerCustom.PlayerPrimaryRolePower.Angel:
			((TMP_Text)_textRole).text = "<color=#" + ColorUtility.ToHtmlStringRGB(VillagerColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_NAME_GUARDIAN_ANGEL") + "</color>";
			break;
		case PlayerCustom.PlayerPrimaryRolePower.Ghost:
			((TMP_Text)_textRole).text = "<color=#" + ColorUtility.ToHtmlStringRGB(VillagerColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_NAME_GHOST") + "</color>";
			break;
		case PlayerCustom.PlayerPrimaryRolePower.Specter:
			((TMP_Text)_textRole).text = "<color=#" + ColorUtility.ToHtmlStringRGB(WolfColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_NAME_SPECTER") + "</color>";
			break;
		}
		switch (PlayerCustom.Local.PrimaryRolePower)
		{
		case PlayerCustom.PlayerPrimaryRolePower.Angel:
		{
			((Component)_textTargetTitle).gameObject.SetActive(true);
			((Component)_textTargetValue).gameObject.SetActive(true);
			((Component)_textAction).gameObject.SetActive(true);
			switch (povPlayerCustom.NewPrimaryRole)
			{
			case PlayerCustom.PlayerNewPrimaryRole.Traitor:
				((TMP_Text)_textTargetValue).text = "<color=#" + ColorUtility.ToHtmlStringRGB(TraitorColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_VALUE_TRAITOR") + "</color>";
				_currentPossibleAction = PossibleAction.None;
				break;
			case PlayerCustom.PlayerNewPrimaryRole.None:
			{
				if ((int)povPlayerCustom.PlayerController.Role == 1)
				{
					if (NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf))
					{
						((TMP_Text)_textTargetValue).text = "<color=#" + ColorUtility.ToHtmlStringRGB(WolfColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_VALUE_WOLF_TRANSFORMED") + "</color>";
						_currentPossibleAction = ((PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Angel) ? PossibleAction.GiveRandomDebuff : PossibleAction.GivePower);
					}
					else
					{
						((TMP_Text)_textTargetValue).text = "<color=#" + ColorUtility.ToHtmlStringRGB(WolfColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_VALUE_WOLF_UNTRANSFORMED") + "</color>";
						_currentPossibleAction = PossibleAction.None;
					}
					break;
				}
				EffectManager effectManager = Traverse.Create(typeof(EffectManager)).Field<EffectManager>("_instance").Value;
				if (povPlayerCustom.AlreadyAngeledToday)
				{
					((TMP_Text)_textTargetValue).text = "<color=#" + ColorUtility.ToHtmlStringRGB(VillagerColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_VALUE_VILLAGER_ALREADY_PROTECTED") + "</color>";
					_currentPossibleAction = ((PlayerCustom.Local.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.Angel) ? PossibleAction.GiveRandomDebuff : PossibleAction.None);
				}
				else if (povPlayerCustom.PlayerController.Hunger <= (float)GameManager.Instance.MaxHunger * 0.25f || PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)o).transform.position) < Traverse.Create((object)effectManager).Method("WolfMusicDistance", new List<Type> { typeof(PlayerController) }.ToArray(), (object[])null).GetValue<float>(new object[1] { o }))))
				{
					((TMP_Text)_textTargetValue).text = "<color=#" + ColorUtility.ToHtmlStringRGB(VillagerColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_VALUE_VILLAGER_IN_DANGER") + "</color>";
					_currentPossibleAction = ((PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Angel) ? PossibleAction.HealAndProtect : PossibleAction.GiveRandomDebuff);
				}
				else
				{
					((TMP_Text)_textTargetValue).text = "<color=#" + ColorUtility.ToHtmlStringRGB(VillagerColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_VALUE_VILLAGER_SAFE") + "</color>";
					_currentPossibleAction = PossibleAction.None;
				}
				break;
			}
			default:
				((TMP_Text)_textTargetValue).text = "<color=#" + ColorUtility.ToHtmlStringRGB(SoloRoleColor) + ">" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_VALUE_SOLO_ROLE") + "</color>";
				_currentPossibleAction = PossibleAction.None;
				break;
			}
			switch (_currentPossibleAction)
			{
			case PossibleAction.None:
				((TMP_Text)_textAction).text = "<color=white>" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_ACTION_NONE") + "</color>";
				break;
			case PossibleAction.HealAndProtect:
				((TMP_Text)_textAction).text = "<color=green>" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_ACTION_HEAL_PROTECT") + "</color>";
				break;
			case PossibleAction.GiveRandomDebuff:
				((TMP_Text)_textAction).text = "<color=red>" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_ACTION_NEGATIVE_EFFECT") + "</color>";
				break;
			case PossibleAction.GivePower:
				((TMP_Text)_textAction).text = "<color=green>" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_ACTION_POWER") + "</color>";
				break;
			}
			TickTimer primaryRolePowerCooldownTimer = PlayerCustom.Local.PrimaryRolePowerCooldownTimer;
			if (!((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
			{
				((TMP_Text)_textCooldown).text = "<color=green>" + TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_AVAILABLE").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)11)) + "</color>";
				break;
			}
			TextMeshProUGUI textCooldown = _textCooldown;
			string translation = TranslationManager.Instance.GetTranslation("NALES_DEAD_ROLE_COOLDOWN");
			primaryRolePowerCooldownTimer = PlayerCustom.Local.PrimaryRolePowerCooldownTimer;
			((TMP_Text)textCooldown).text = "<color=red>" + translation.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)PlayerCustom.Local).Runner).Value).ToString()) + "</color>";
			break;
		}
		}
	}
}
