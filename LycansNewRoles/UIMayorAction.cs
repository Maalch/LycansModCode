using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIMayorAction : MonoBehaviour
{
	public int Index;

	private string _translateKey;

	private TextMeshProUGUI _text;

	public void Awake()
	{
		_text = ((Component)((Component)this).gameObject.transform.Find("Text")).GetComponent<TextMeshProUGUI>();
	}

	public void Init(int index, string translateKey)
	{
		Index = index;
		_translateKey = translateKey;
		UpdateAction(selected: false);
	}

	public void UpdateAction(bool selected)
	{
		string text = (selected ? "green" : "white");
		((TMP_Text)_text).text = "<color=" + text + ">" + TranslationManager.Instance.GetTranslation(_translateKey) + "</color>";
	}
}
