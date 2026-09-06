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

	private PlayerCustom _playerCustom;

	private void Awake()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		_button = ((Component)((Component)this).transform.Find("Button")).GetComponent<Button>();
		_textPlayer = ((Component)((Component)_button).transform.Find("PlayerName")).GetComponent<TextMeshProUGUI>();
		_textRole = ((Component)((Component)this).transform.Find("PlayerRole")).GetComponent<TextMeshProUGUI>();
		((UnityEvent)_button.onClick).AddListener(new UnityAction(OnClick));
	}

	public void Init(PlayerCustom playerCustom)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Invalid comparison between Unknown and I4
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		((TMP_Text)_textPlayer).text = ((object)playerCustom.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
		((Graphic)_textPlayer).color = ColorManager.GetColor(playerCustom.ColorIndex);
		string text = TranslationManager.Instance.GetTranslation(UpdateRoleUtility.GetNewPrimaryRoleKey(playerCustom.PlayerController, playerCustom)).Replace("{0}", "").Replace("{1}", "")
			.Replace("{2}", "");
		if (playerCustom.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None)
		{
			text = text + " " + TranslationManager.Instance.GetTranslation(UpdateRoleUtility.GetPrimaryRolePowerKey(playerCustom.PrimaryRolePower));
		}
		((TMP_Text)_textRole).text = text;
		Color color = ((playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None) ? PlayerCustom.GetNewPrimaryRoleColor(playerCustom.NewPrimaryRole) : (((int)playerCustom.PlayerController.Role == 1) ? GameUI.WolfColor : ((!PlayerCustom.IsPrimaryRolePowerForEliteVillagers(playerCustom.PrimaryRolePower)) ? GameUI.VillagerColor : PlayerCustom.EliteRoleColor)));
		((Graphic)_textRole).color = color;
		_playerCustom = playerCustom;
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
