using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UISpectatorsListPanel : MonoBehaviour
{
	public static GameObject SpectatorsListPlayerPrefab;

	private GameObject _panel;

	private List<GameObject> _playerObjects = new List<GameObject>();

	private List<string> _spectators = new List<string>();

	public bool Active = false;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_panel.SetActive(false);
		((TMP_Text)_panel.GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_SPECTATORS_HEADER");
	}

	private void Update()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		if (Active)
		{
			CheckIfNeedsUpdate();
		}
		if ((int)GameManager.LocalGameState != 1 || !PlayerCustomRegistry.AllSpectators.Any() || UIManager.CustomizationComponent.Active || GameManager.Instance.gameUI.IsSettingMenuOpen || GameManager.Instance.gameUI.IsGameSettingMenuOpen)
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

	public void Show()
	{
		_panel.SetActive(true);
		UpdateSpectators();
		Active = true;
	}

	public void Hide()
	{
		DestroyPlayers();
		_panel.SetActive(false);
		Active = false;
	}

	private void DestroyPlayers()
	{
		foreach (GameObject playerObject in _playerObjects)
		{
			Object.Destroy((Object)(object)playerObject);
		}
		_playerObjects.Clear();
	}

	private void CheckIfNeedsUpdate()
	{
		List<string> spectators = PlayerCustomRegistry.AllSpectators.Select((PlayerCustom o) => ((object)o.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString()).ToList();
		if (_spectators.Any((string o) => !spectators.Contains(o)) || spectators.Any((string o) => !_spectators.Contains(o)))
		{
			UpdateSpectators();
		}
	}

	public void UpdateSpectators()
	{
		if (Active)
		{
			DestroyPlayers();
			_spectators = PlayerCustomRegistry.AllSpectators.Select((PlayerCustom o) => ((object)o.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString()).ToList();
			List<GameObject> playerObjects = GetPlayerObjects(_spectators);
			for (int num = 0; num < playerObjects.Count; num++)
			{
				playerObjects[num].transform.SetParent(_panel.transform);
				_playerObjects.Add(playerObjects[num]);
			}
		}
	}

	public static List<GameObject> GetPlayerObjects(List<string> players)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (string player in players)
		{
			GameObject val = Object.Instantiate<GameObject>(SpectatorsListPlayerPrefab);
			val.SetActive(true);
			((TMP_Text)val.GetComponentInChildren<TextMeshProUGUI>()).text = player.ToString();
			list.Add(val);
		}
		return list;
	}
}
