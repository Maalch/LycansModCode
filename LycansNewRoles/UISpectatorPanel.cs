using Fusion;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UISpectatorPanel : MonoBehaviour
{
	private GameObject _panel;

	private TextMeshProUGUI _textRole;

	public bool Active = false;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_textRole = ((Component)_panel.transform.Find("Text")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_textRole).text = TranslationManager.Instance.GetTranslation("NALES_SPECTATOR_TEXT").Replace("{0}", LycansUtility.GetInputDisplayCustom(InputManagerExtra.Instance.Actions["MAYORACTION"]).Replace(" -", ""));
		_panel.SetActive(false);
	}

	private void Update()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(PlayerController.Local.IsDead) && !GameManager.Instance.gameUI.IsSettingMenuOpen && !GameManager.Instance.gameUI.IsGameSettingMenuOpen && (Object)(object)PlayerCustom.Local.SummonedSpirit == (Object)null)
		{
			if (!Active)
			{
				Show();
			}
		}
		else if (Active)
		{
			Hide();
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
}
