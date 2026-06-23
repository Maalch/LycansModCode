using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UIModInstallationPlayer : MonoBehaviour
{
	private TextMeshProUGUI _playerNameText;

	private TextMeshProUGUI _versionText;

	public void Awake()
	{
		_playerNameText = ((Component)((Component)this).gameObject.transform.Find("Name")).GetComponent<TextMeshProUGUI>();
		_versionText = ((Component)((Component)this).gameObject.transform.Find("Version")).GetComponent<TextMeshProUGUI>();
	}

	public void UpdateData(string playerName, float? version)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		((TMP_Text)_playerNameText).text = playerName;
		if (version.HasValue)
		{
			((TMP_Text)_versionText).text = "V" + version.Value.ToString("n3").Replace(",", ".");
			((Graphic)_versionText).color = ((version.Value == PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref).ModVersion.Value) ? Color.green : Color.red);
		}
		else
		{
			((TMP_Text)_versionText).text = "---";
			((Graphic)_versionText).color = Color.red;
		}
	}
}
