using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIItemDetailsPanel : MonoBehaviour
{
	private TextMeshProUGUI _text;

	private void Awake()
	{
		_text = ((Component)this).GetComponent<TextMeshProUGUI>();
		Hide();
	}

	public void Show(string text)
	{
		((Behaviour)_text).enabled = true;
		((TMP_Text)_text).text = text;
	}

	public void Hide()
	{
		((Behaviour)_text).enabled = false;
	}
}
