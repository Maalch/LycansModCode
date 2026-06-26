using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class UILastGameSummaryPanel : MonoBehaviour
{
	public enum WinnerType
	{
		None,
		Villagers,
		Wolves,
		Lovers,
		OtherSoloRole
	}

	public static GameObject PlayerKillPrefab;

	private GameObject _panel;

	public bool Active = false;

	private List<LastGameSummaryKill> _kills = new List<LastGameSummaryKill>();

	private WinnerType _winnerType;

	private PlayerRef _winnerRef = PlayerRef.None;

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
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState != 1 || (!_kills.Any() && _winnerType == WinnerType.None) || !NetworkBool.op_Implicit(Plugin.CustomConfig.ShowLastGameSummary) || UIManager.CustomizationComponent.Active || GameManager.Instance.gameUI.IsSettingMenuOpen || GameManager.Instance.gameUI.IsGameSettingMenuOpen)
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

	public void AddWinner(WinnerType winnerType, PlayerRef winnerRef)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		_winnerType = winnerType;
		_winnerRef = winnerRef;
	}

	public void Show()
	{
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
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
		if (_winnerType != WinnerType.None)
		{
			GameObject val2 = Object.Instantiate<GameObject>(PlayerKillPrefab);
			val2.SetActive(true);
			val2.transform.SetParent(_panel.transform);
			val2.GetComponent<UILastGameSummaryKill>().UpdateWithWinner(_winnerType, _winnerRef);
			_objects.Add(val2);
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		_kills.Clear();
		_winnerRef = PlayerRef.None;
		_winnerType = WinnerType.None;
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
