using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIModInstallationPanel : MonoBehaviour
{
	public static GameObject PlayerPanelPrefab;

	private GameObject _panel;

	private GameObject _playersContainer;

	private Dictionary<PlayerRef, UIModInstallationPlayer> _players = new Dictionary<PlayerRef, UIModInstallationPlayer>();

	public bool Active = false;

	private void Awake()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_playersContainer = ((Component)_panel.transform.Find("PlayersContainer")).gameObject;
	}

	private void Start()
	{
		((TMP_Text)((Component)_panel.transform.Find("Title")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_MOD_INSTALLATION_TITLE");
		_panel.SetActive(false);
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		if ((int)GameManager.LocalGameState != 1 || !((SimulationBehaviour)GameManager.Instance).Runner.IsServer || UIManager.CustomizationComponent.Active || GameManager.Instance.gameUI.IsSettingMenuOpen || GameManager.Instance.gameUI.IsGameSettingMenuOpen)
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

	public void AddOrUpdatePlayer(PlayerRef playerRef)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (!_players.ContainsKey(playerRef))
		{
			GameObject val = Object.Instantiate<GameObject>(PlayerPanelPrefab);
			val.SetActive(true);
			val.transform.SetParent(_playersContainer.transform);
			_players[playerRef] = val.GetComponent<UIModInstallationPlayer>();
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerRef);
		if ((Object)(object)player != (Object)null)
		{
			_players[playerRef].UpdateData(((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString(), player.ModVersion);
		}
	}

	public void RemovePlayer(PlayerRef playerRef)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (_players.ContainsKey(playerRef))
		{
			Object.Destroy((Object)(object)((Component)_players[playerRef]).gameObject);
			_players.Remove(playerRef);
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
