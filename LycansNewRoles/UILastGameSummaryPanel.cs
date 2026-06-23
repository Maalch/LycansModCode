using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class UILastGameSummaryPanel : MonoBehaviour
{
	public static GameObject PlayerKillPrefab;

	private GameObject _panel;

	public bool Active = false;

	private List<LastGameSummaryKill> _kills = new List<LastGameSummaryKill>();

	private List<GameObject> _objects = new List<GameObject>();

	private void Awake()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
	}

	private void Start()
	{
		_panel.SetActive(false);
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState != 1 || !_kills.Any() || !NetworkBool.op_Implicit(Plugin.CustomConfig.ShowLastGameSummary) || UIManager.CustomizationComponent.Active || GameManager.Instance.gameUI.IsSettingMenuOpen || GameManager.Instance.gameUI.IsGameSettingMenuOpen)
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

	public void AddPlayerKill(LastGameSummaryKill data)
	{
		_kills.Add(data);
	}

	public void Show()
	{
		_panel.SetActive(true);
		Active = true;
		foreach (LastGameSummaryKill kill in _kills)
		{
			GameObject val = Object.Instantiate<GameObject>(PlayerKillPrefab);
			val.SetActive(true);
			val.transform.SetParent(_panel.transform);
			val.GetComponent<UILastGameSummaryKill>().UpdateData(kill);
			_objects.Add(val);
		}
	}

	public void Hide()
	{
		ClearObjects();
		_panel.SetActive(false);
		Active = false;
	}

	public void Clear()
	{
		_kills.Clear();
		ClearObjects();
	}

	public void ClearObjects()
	{
		foreach (GameObject @object in _objects)
		{
			Object.Destroy((Object)(object)@object.gameObject);
		}
		_objects.Clear();
	}
}
