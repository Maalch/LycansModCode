using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UIItemSecondaryPanel : MonoBehaviour
{
	private TextMeshProUGUI _key;

	private Image _image;

	private void Awake()
	{
		_key = ((Component)this).GetComponentInChildren<TextMeshProUGUI>();
		_image = ((Component)this).GetComponentInChildren<Image>();
		Hide();
	}

	public void Show(string text)
	{
		((Behaviour)_key).enabled = true;
		((TMP_Text)_key).text = text;
		((Behaviour)_image).enabled = true;
	}

	public void Hide()
	{
		((Behaviour)_key).enabled = false;
		((Behaviour)_image).enabled = false;
	}
}
