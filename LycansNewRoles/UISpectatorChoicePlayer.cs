using System.Diagnostics;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UISpectatorChoicePlayer : MonoBehaviour
{
	private Button _button;

	private TextMeshProUGUI _textPlayer;

	private TextMeshProUGUI _textRole;

	private Image _icon;

	private TextMeshProUGUI _soloRoleProgress;

	private PlayerCustom _playerCustom;

	private Stopwatch _stopwatch = new Stopwatch();

	private void Awake()
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		_button = ((Component)((Component)this).transform.Find("Button")).GetComponent<Button>();
		_textPlayer = ((Component)((Component)_button).transform.Find("PlayerName")).GetComponent<TextMeshProUGUI>();
		_textRole = ((Component)((Component)this).transform.Find("PlayerRole")).GetComponent<TextMeshProUGUI>();
		_icon = ((Component)((Component)this).transform.Find("LeftInfo").Find("Icon")).GetComponent<Image>();
		_soloRoleProgress = ((Component)((Component)this).transform.Find("LeftInfo").Find("SoloRoleProgress")).GetComponent<TextMeshProUGUI>();
		((UnityEvent)_button.onClick).AddListener(new UnityAction(OnClick));
	}

	public void Init(PlayerCustom playerCustom)
	{
		_playerCustom = playerCustom;
		_stopwatch.Restart();
		UpdateInfo();
	}

	private void Update()
	{
		if (_stopwatch.ElapsedMilliseconds >= 1000)
		{
			_stopwatch.Restart();
			UpdateInfo();
		}
	}

	private void UpdateInfo()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Invalid comparison between Unknown and I4
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Invalid comparison between Unknown and I4
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(_playerCustom.PlayerController.IsDead))
		{
			((TMP_Text)_textPlayer).text = "<s>" + ((object)_playerCustom.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</s>";
		}
		else
		{
			((TMP_Text)_textPlayer).text = ((object)_playerCustom.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
		}
		((Graphic)_textPlayer).color = ColorManager.GetColor(_playerCustom.ColorIndex);
		string text = TranslationManager.Instance.GetTranslation(UpdateRoleUtility.GetNewPrimaryRoleKey(_playerCustom.PlayerController, _playerCustom)).Replace("{0}", "").Replace("{1}", "")
			.Replace("{2}", "");
		if (_playerCustom.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None)
		{
			text = text + " " + TranslationManager.Instance.GetTranslation(UpdateRoleUtility.GetPrimaryRolePowerKey(_playerCustom.PrimaryRolePower));
		}
		((TMP_Text)_textRole).text = text;
		Color color = ((_playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None) ? PlayerCustom.GetNewPrimaryRoleColor(_playerCustom.NewPrimaryRole) : (((int)_playerCustom.PlayerController.Role == 1) ? GameUI.WolfColor : ((!PlayerCustom.IsPrimaryRolePowerForEliteVillagers(_playerCustom.PrimaryRolePower)) ? GameUI.VillagerColor : PlayerCustom.EliteRoleColor)));
		((Graphic)_textRole).color = color;
		if (NetworkBool.op_Implicit(_playerCustom.PlayerController.IsWolf) && (int)_playerCustom.PlayerController.Role == 1)
		{
			((Component)_soloRoleProgress).gameObject.SetActive(false);
			((Component)_icon).gameObject.SetActive(true);
			_icon.sprite = UILastGameSummaryKill.DeathTypeSpriteWolfKill;
		}
		else if (NetworkBool.op_Implicit(_playerCustom.Kidnapped))
		{
			((Component)_soloRoleProgress).gameObject.SetActive(false);
			((Component)_icon).gameObject.SetActive(true);
			_icon.sprite = UIManager.KidnappedPlayerIcon;
		}
		else if (NetworkBool.op_Implicit(_playerCustom.PlayerController.IsDead))
		{
			((Component)_soloRoleProgress).gameObject.SetActive(false);
			((Component)_icon).gameObject.SetActive(true);
			_icon.sprite = UILastGameSummaryKill.DeathTypeSpriteSurvivalistNotSaved;
		}
		else
		{
			((Component)_icon).gameObject.SetActive(false);
			string text2 = null;
			switch (_playerCustom.NewPrimaryRole)
			{
			case PlayerCustom.PlayerNewPrimaryRole.Spy:
				text2 = Mathf.FloorToInt((float)(_playerCustom.SoloRoleObjectiveCount * 100 / BalancingValues.SpyGoal(PlayerRegistry.Count))) + "%";
				break;
			case PlayerCustom.PlayerNewPrimaryRole.Scientist:
				text2 = Mathf.FloorToInt((float)(_playerCustom.SoloRoleObjectiveCount * 100 / BalancingValues.ScientistGoal(PlayerRegistry.Count))) + "%";
				break;
			case PlayerCustom.PlayerNewPrimaryRole.Beast:
				text2 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => NetworkBool.op_Implicit(o.BeastMark) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)) + "/" + PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Beast && !NetworkBool.op_Implicit(o.PlayerController.IsDead));
				break;
			case PlayerCustom.PlayerNewPrimaryRole.Cultist:
				text2 = Mathf.FloorToInt((float)(_playerCustom.SoloRoleObjectiveCount * 100 / 10000)) + "%";
				break;
			case PlayerCustom.PlayerNewPrimaryRole.Mercenary:
				text2 = Mathf.FloorToInt((float)(_playerCustom.SoloRoleObjectiveCount * 100 / _playerCustom.SoloRoleObjectiveTarget)) + "%";
				break;
			}
			if (text2 != null)
			{
				((Component)_soloRoleProgress).gameObject.SetActive(true);
				((TMP_Text)_soloRoleProgress).text = text2;
			}
			else
			{
				((Component)_soloRoleProgress).gameObject.SetActive(false);
			}
		}
		((Selectable)_button).interactable = !NetworkBool.op_Implicit(_playerCustom.PlayerController.IsDead) && !_playerCustom.IsOutOfTheWorld;
	}

	private void OnClick()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)_playerCustom == (Object)null) && PlayerCustomRegistry.HasPlayer(_playerCustom.Ref) && !NetworkBool.op_Implicit(_playerCustom.PlayerController.IsDead) && !_playerCustom.IsOutOfTheWorld)
		{
			PlayerController.Local.LocalCameraHandler.SwitchPov(_playerCustom.PlayerController);
		}
	}
}
