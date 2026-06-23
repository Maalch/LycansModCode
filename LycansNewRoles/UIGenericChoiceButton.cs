using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UIGenericChoiceButton : MonoBehaviour
{
	private bool _selected = false;

	private int _index;

	private UnityAction _clickAction;

	public bool Selected => _selected;

	public int Index => _index;

	public UnityAction ClickAction => _clickAction;

	public void Init(int index, bool selected)
	{
		_index = index;
		ToggleSelected(selected);
	}

	public void SetAction(UnityAction action)
	{
		_clickAction = action;
		((UnityEvent)((Component)this).GetComponent<Button>().onClick).AddListener(action);
	}

	public void ToggleSelected(bool selected)
	{
		_selected = selected;
		((Component)((Component)this).transform.Find("SelectedIcon")).gameObject.SetActive(selected);
	}
}
