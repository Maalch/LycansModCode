using Fusion;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIMayorPanelForOthers : MonoBehaviour
{
	private GameObject _panel;

	private TextMeshProUGUI _textMayor;

	private TextMeshProUGUI _textCurrentVote;

	private TextMeshProUGUI _textDestitutionCount;

	private TextMeshProUGUI _textDifferentCount;

	public bool Active = false;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		((TMP_Text)((Component)_panel.transform.Find("VoteText")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_MAYOR_VOTE").Replace("{0}", LycansUtility.GetInputDisplayCustom(InputManagerExtra.Instance.GetAction("SECONDARYROLEPOWER")).Replace(" -", ""));
		_textMayor = ((Component)_panel.transform.Find("MayorText")).GetComponent<TextMeshProUGUI>();
		_textCurrentVote = ((Component)_panel.transform.Find("CurrentVoteText")).GetComponent<TextMeshProUGUI>();
		_textDestitutionCount = ((Component)_panel.transform.Find("DestitutionCountText")).GetComponent<TextMeshProUGUI>();
		_textDifferentCount = ((Component)_panel.transform.Find("DifferentCountText")).GetComponent<TextMeshProUGUI>();
		_panel.SetActive(false);
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
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState == 0)
		{
			return;
		}
		if ((Object)(object)Plugin.CustomConfig == (Object)null || !NetworkBool.op_Implicit(Plugin.CustomConfig.AllowMayor) || (int)GameManager.LocalGameState != 4 || GameManagerCustom.Instance.CurrentMayor == PlayerController.Local.Ref || NetworkBool.op_Implicit(PlayerController.Local.IsDead) || GameManager.Instance.gameUI.IsSettingMenuOpen || GameManager.Instance.gameUI.IsGameSettingMenuOpen)
		{
			if (Active)
			{
				Hide();
			}
		}
		else if (!Active)
		{
			Show();
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

	public void UpdateCurrentVote()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		PlayerRef mayorVoteTarget = PlayerCustom.Local.MayorVoteTarget;
		if (((PlayerRef)(ref mayorVoteTarget)).IsNone)
		{
			((TMP_Text)_textCurrentVote).text = TranslationManager.Instance.GetTranslation("NALES_MAYOR_CURRENT_VOTE_NONE");
		}
		else if (mayorVoteTarget == GameManagerCustom.Instance.CurrentMayor)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(mayorVoteTarget);
			((TMP_Text)_textCurrentVote).text = TranslationManager.Instance.GetTranslation("NALES_MAYOR_CURRENT_VOTE_DESTITUTE_MAYOR").Replace("{0}", "<color=#" + ColorUtility.ToHtmlStringRGB(ColorManager.GetColor(player.ColorIndex)) + ">" + ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</color>");
		}
		else
		{
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(mayorVoteTarget);
			((TMP_Text)_textCurrentVote).text = TranslationManager.Instance.GetTranslation("NALES_MAYOR_CURRENT_VOTE_NEW_MAYOR").Replace("{0}", "<color=#" + ColorUtility.ToHtmlStringRGB(ColorManager.GetColor(player2.ColorIndex)) + ">" + ((object)player2.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</color>");
		}
	}

	public void UpdateDestitutionCount(int count, int required)
	{
		((TMP_Text)_textDestitutionCount).text = TranslationManager.Instance.GetTranslation("NALES_MAYOR_COUNT_DESTITUTION").Replace("{0}", count.ToString()).Replace("{1}", required.ToString());
	}

	public void UpdateDifferentCount(int count, int required, PlayerRef differentMayorRef)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (count > 0 && !((PlayerRef)(ref differentMayorRef)).IsNone)
		{
			string newValue = ((object)PlayerRegistry.GetPlayer(differentMayorRef).PlayerData.Username/*cast due to constrained. prefix*/).ToString();
			((TMP_Text)_textDifferentCount).text = TranslationManager.Instance.GetTranslation("NALES_MAYOR_COUNT_DIFFERENT").Replace("{0}", count.ToString()).Replace("{1}", required.ToString())
				.Replace("{2}", newValue);
		}
		else
		{
			((TMP_Text)_textDifferentCount).text = "";
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
