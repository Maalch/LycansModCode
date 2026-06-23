using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LycansNewRoles;

public class DraftRoleOptionComponent : MonoBehaviour
{
	public bool Active;

	private int _optionIndex;

	private string _playerPrefKey;

	private Image _imageChecked;

	private void Awake()
	{
		_imageChecked = ((Component)((Component)this).transform.Find("Checkbox").Find("Active")).GetComponent<Image>();
	}

	public void Init(int optionIndex, string playerPrefKey, string translateKey)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		_optionIndex = optionIndex;
		_playerPrefKey = playerPrefKey;
		((TMP_Text)((Component)this).GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation(translateKey);
		((UnityEventBase)((Component)this).GetComponentInChildren<Button>().onClick).RemoveAllListeners();
		((UnityEvent)((Component)this).GetComponentInChildren<Button>().onClick).AddListener(new UnityAction(OnClick));
		if (!PlayerPrefs.HasKey(_playerPrefKey))
		{
			PlayerPrefs.SetInt(_playerPrefKey, 1);
		}
		Toggle(PlayerPrefs.GetInt(_playerPrefKey) == 1);
	}

	private void OnClick()
	{
		Toggle(!Active);
		DraftManager.Rpc_Role_Option(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index, _optionIndex, Active);
	}

	private void Toggle(bool active)
	{
		Active = active;
		((Behaviour)_imageChecked).enabled = active;
		PlayerPrefs.SetInt(_playerPrefKey, active ? 1 : 0);
	}
}
