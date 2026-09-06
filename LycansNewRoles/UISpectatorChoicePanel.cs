using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

public class UISpectatorChoicePanel : MonoBehaviour
{
	public static GameObject SpectatorChoiceButtonPrefab;

	private GameObject _panel;

	private List<GameObject> _buttons = new List<GameObject>();

	public bool Active = false;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_panel.SetActive(false);
	}

	public void Show(List<PlayerRef> players)
	{
		_panel.SetActive(true);
		DestroyButtons();
		List<GameObject> buttonsForPlayers = GetButtonsForPlayers(players);
		for (int i = 0; i < buttonsForPlayers.Count; i++)
		{
			buttonsForPlayers[i].transform.SetParent(_panel.transform);
			_buttons.Add(buttonsForPlayers[i]);
		}
		Active = true;
	}

	public void Hide()
	{
		DestroyButtons();
		_panel.SetActive(false);
		Active = false;
	}

	private void DestroyButtons()
	{
		foreach (GameObject button in _buttons)
		{
			Object.Destroy((Object)(object)button);
		}
		_buttons.Clear();
	}

	public static List<GameObject> GetButtonsForPlayers(List<PlayerRef> players)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		List<GameObject> list = new List<GameObject>();
		Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
		List<PlayerRef> list2 = (from o in value
			orderby ((Component)o.Value).gameObject.transform.position.y descending
			select o.Key).Where(players.Contains).ToList();
		foreach (PlayerRef item in list2)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(item);
			GameObject val = Object.Instantiate<GameObject>(SpectatorChoiceButtonPrefab);
			val.SetActive(true);
			UISpectatorChoicePlayer uISpectatorChoicePlayer = val.AddComponent<UISpectatorChoicePlayer>();
			uISpectatorChoicePlayer.Init(player);
			list.Add(val);
		}
		return list;
	}
}
