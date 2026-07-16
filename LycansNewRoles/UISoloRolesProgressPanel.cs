using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UISoloRolesProgressPanel : MonoBehaviour
{
	private const float MinimumSoloRoleProgressToShow = 0.25f;

	private GameObject _panel;

	private TextMeshProUGUI _textTitle;

	private GameObject _soloRoles;

	private TextMeshProUGUI _textSpy;

	private TextMeshProUGUI _textScientist;

	private TextMeshProUGUI _textBeast;

	public bool Active = false;

	private Stopwatch _blinkStopwatch = new Stopwatch();

	private Stopwatch _blinkEndStopwatch = new Stopwatch();

	private bool _isAlternateColor = false;

	private static Color _defaultColor = Color.white;

	private static Color _alternateColor = Color.red;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_textTitle = ((Component)_panel.transform.Find("Title")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_textTitle).text = TranslationManager.Instance.GetTranslation("NALES_SOLO_ROLES_PROGRESS_TITLE");
		_soloRoles = ((Component)_panel.transform.Find("SoloRoles")).gameObject;
		_textSpy = ((Component)_soloRoles.transform.Find("Spy")).GetComponent<TextMeshProUGUI>();
		_textScientist = ((Component)_soloRoles.transform.Find("Scientist")).GetComponent<TextMeshProUGUI>();
		_textBeast = ((Component)_soloRoles.transform.Find("Beast")).GetComponent<TextMeshProUGUI>();
		_panel.SetActive(false);
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Invalid comparison between Unknown and I4
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState == 0)
		{
			return;
		}
		IEnumerable<PlayerCustom> source = PlayerCustomRegistry.Where((PlayerCustom o) => (o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Spy && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (float)o.SoloRoleObjectiveCount / (float)BalancingValues.SpyGoal(PlayerRegistry.Count) >= 0.25f) || (o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Scientist && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (float)o.SoloRoleObjectiveCount / (float)BalancingValues.ScientistGoal(PlayerRegistry.Count) >= 0.25f) || (o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && PlayerCustomRegistry.CountWhere((PlayerCustom playerCustom) => NetworkBool.op_Implicit(playerCustom.BeastMark)) >= 3));
		if ((Object)(object)Plugin.CustomConfig == (Object)null || (int)GameManager.LocalGameState != 4 || !source.Any() || GameManager.Instance.gameUI.IsSettingMenuOpen || GameManager.Instance.gameUI.IsGameSettingMenuOpen)
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
			return;
		}
		if (_blinkEndStopwatch.IsRunning && _blinkStopwatch.ElapsedMilliseconds >= 500)
		{
			_isAlternateColor = !_isAlternateColor;
			((Graphic)_textTitle).color = (_isAlternateColor ? _alternateColor : _defaultColor);
			_blinkStopwatch.Restart();
		}
		if (_blinkEndStopwatch.ElapsedMilliseconds >= 4000)
		{
			_blinkEndStopwatch.Reset();
			_blinkStopwatch.Reset();
			_isAlternateColor = false;
			((Graphic)_textTitle).color = _defaultColor;
		}
	}

	public void UpdateSoloRolesProgress()
	{
		IEnumerable<PlayerCustom> source = PlayerCustomRegistry.Where((PlayerCustom o) => (o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Spy && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (float)o.SoloRoleObjectiveCount / (float)BalancingValues.SpyGoal(PlayerRegistry.Count) >= 0.25f) || (o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Scientist && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (float)o.SoloRoleObjectiveCount / (float)BalancingValues.ScientistGoal(PlayerRegistry.Count) >= 0.25f) || (o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && PlayerCustomRegistry.CountWhere((PlayerCustom playerCustom) => NetworkBool.op_Implicit(playerCustom.BeastMark)) >= 3));
		PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Spy);
		if ((Object)(object)specificNewPrimaryRole != (Object)null && source.Contains(specificNewPrimaryRole) && specificNewPrimaryRole.SoloRoleObjectiveCount < BalancingValues.SpyGoal(PlayerRegistry.Count))
		{
			((Component)_textSpy).gameObject.SetActive(true);
			string text = Mathf.FloorToInt((float)(specificNewPrimaryRole.SoloRoleObjectiveCount * 100 / BalancingValues.SpyGoal(PlayerRegistry.Count))).ToString();
			((TMP_Text)_textSpy).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_SPY") + " : " + text + "%";
		}
		else
		{
			((Component)_textSpy).gameObject.SetActive(false);
		}
		PlayerCustom specificNewPrimaryRole2 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Scientist);
		if ((Object)(object)specificNewPrimaryRole2 != (Object)null && source.Contains(specificNewPrimaryRole2) && specificNewPrimaryRole2.SoloRoleObjectiveCount < BalancingValues.ScientistGoal(PlayerRegistry.Count))
		{
			((Component)_textScientist).gameObject.SetActive(true);
			string text2 = Mathf.FloorToInt((float)(specificNewPrimaryRole2.SoloRoleObjectiveCount * 100 / BalancingValues.ScientistGoal(PlayerRegistry.Count))).ToString();
			((TMP_Text)_textScientist).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_SCIENTIST") + " : " + text2 + "%";
		}
		else
		{
			((Component)_textScientist).gameObject.SetActive(false);
		}
		PlayerCustom specificNewPrimaryRole3 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Beast);
		if ((Object)(object)specificNewPrimaryRole3 != (Object)null && source.Contains(specificNewPrimaryRole3))
		{
			((Component)_textBeast).gameObject.SetActive(true);
			((TMP_Text)_textBeast).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_BEAST") + " : " + PlayerCustomRegistry.CountWhere((PlayerCustom o) => NetworkBool.op_Implicit(o.BeastMark) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)) + " / " + PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Beast && !NetworkBool.op_Implicit(o.PlayerController.IsDead));
		}
		else
		{
			((Component)_textBeast).gameObject.SetActive(false);
		}
	}

	public void Show()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		_panel.SetActive(true);
		Active = true;
		UpdateSoloRolesProgress();
		_blinkStopwatch.Restart();
		_blinkEndStopwatch.Restart();
		((Graphic)_textTitle).color = _defaultColor;
	}

	public void Hide()
	{
		_panel.SetActive(false);
		Active = false;
	}
}
