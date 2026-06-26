using System;
using System.Collections.Generic;
using Fusion;
using LycansNewRoles.NewItems;
using LycansNewRoles.PowerObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapComponent : MonoBehaviour
{
	public enum MinimapState
	{
		Inactive,
		Regular,
		Admin
	}

	private MinimapState _state;

	private GameObject _panel;

	private GameObject _minimapObject;

	private Dictionary<GameObject, GameObject> _sabotageIconsByGameObject = new Dictionary<GameObject, GameObject>();

	private GameObject _investigatorTargetGameObject = null;

	private GameObject _teleporterBeaconGameObject = null;

	private List<MinimapConditionalObject> _conditionalObjects = new List<MinimapConditionalObject>();

	public Vector3 PlayerPositionBeforeMeeting;

	public Quaternion PlayerRotationBeforeMeeting;

	private GameObject _legendMe;

	private GameObject _legendHuman;

	private GameObject _legendWolf;

	private GameObject _legendPlayer;

	private GameObject _legendLover;

	private GameObject _legendSabotage;

	private GameObject _legendTarget;

	private GameObject _legendCorpse;

	private GameObject _legendBomb;

	private GameObject _legendDetector;

	private GameObject _legendInvestigatorTarget;

	private GameObject _legendTeleporterBeacon;

	private GameObject _legendMerchantCoin;

	private GameObject _legendRadar;

	private GameObject _legendHideout;

	private GameObject _legendRune;

	public MinimapState State => _state;

	private void Awake()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		Transform val = _panel.transform.Find("Legends");
		for (int i = 0; i < val.childCount; i++)
		{
			TextMeshProUGUI componentInChildren = ((Component)val.GetChild(i)).GetComponentInChildren<TextMeshProUGUI>();
			((TMP_Text)componentInChildren).text = TranslationManager.Instance.GetTranslation(((Object)componentInChildren).name);
		}
		_legendMe = ((Component)val.Find("MinimapLegendMe")).gameObject;
		_legendHuman = ((Component)val.Find("MinimapLegendHuman")).gameObject;
		_legendWolf = ((Component)val.Find("MinimapLegendWolf")).gameObject;
		_legendLover = ((Component)val.Find("MinimapLegendLover")).gameObject;
		_legendPlayer = ((Component)val.Find("MinimapLegendPlayer")).gameObject;
		_legendSabotage = ((Component)val.Find("MinimapLegendSabotage")).gameObject;
		_legendTarget = ((Component)val.Find("MinimapLegendTarget")).gameObject;
		_legendCorpse = ((Component)val.Find("MinimapLegendCorpse")).gameObject;
		_legendBomb = ((Component)val.Find("MinimapLegendBomb")).gameObject;
		_legendDetector = ((Component)val.Find("MinimapLegendDetector")).gameObject;
		_legendInvestigatorTarget = ((Component)val.Find("MinimapLegendInvestigatorPlace")).gameObject;
		_legendTeleporterBeacon = ((Component)val.Find("MinimapLegendTeleporterBeacon")).gameObject;
		_legendMerchantCoin = ((Component)val.Find("MinimapLegendMerchantCoin")).gameObject;
		_legendRadar = ((Component)val.Find("MinimapLegendRadar")).gameObject;
		_legendHideout = ((Component)val.Find("MinimapLegendHideout")).gameObject;
		_legendRune = ((Component)val.Find("MinimapLegendRune")).gameObject;
	}

	public void Init(GameObject minimap)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < _panel.transform.childCount; i++)
		{
			Transform child = _panel.transform.GetChild(i);
			if (((Object)child).name.Contains("Minimap") && !((Object)child).name.StartsWith("MinimapPlayer"))
			{
				list.Add(((Component)_panel.transform.GetChild(i)).gameObject);
			}
		}
		foreach (GameObject item in list)
		{
			Object.Destroy((Object)(object)item);
		}
		_minimapObject = minimap;
		_minimapObject.transform.SetParent(_panel.transform, false);
		_minimapObject.transform.SetAsFirstSibling();
		_conditionalObjects.Clear();
		Transform val = _minimapObject.transform.Find("Texts");
		if ((Object)(object)val != (Object)null)
		{
			UpdateAllTextLocalizations(((Component)val).gameObject);
		}
		Transform val2 = _minimapObject.transform.Find("Conditionals");
		if ((Object)(object)val2 != (Object)null)
		{
			for (int j = 0; j < val2.childCount; j++)
			{
				AddConditionalObject(((Component)val2.GetChild(j)).gameObject);
				UpdateAllTextLocalizations(((Component)val2.GetChild(j)).gameObject);
			}
		}
		SetState(MinimapState.Inactive);
	}

	private void AddConditionalObject(GameObject conditionalObject)
	{
		MinimapConditionalObject minimapConditionalObject = new MinimapConditionalObject
		{
			ConditionObject = conditionalObject.gameObject,
			Active = false
		};
		if (((Object)conditionalObject).name.StartsWith("Y<"))
		{
			minimapConditionalObject.ConditionType = MinimapConditionalObject.MinimapConditionType.PlayerBelowVerticalPosition;
			minimapConditionalObject.ConditionValue = float.Parse(((Object)conditionalObject).name.Replace("Y<", ""));
		}
		else if (((Object)conditionalObject).name.StartsWith("Y>="))
		{
			minimapConditionalObject.ConditionType = MinimapConditionalObject.MinimapConditionType.PlayerAtOrAboveVerticalPosition;
			minimapConditionalObject.ConditionValue = float.Parse(((Object)conditionalObject).name.Replace("Y>=", ""));
		}
		minimapConditionalObject.SetActive(active: false);
		_conditionalObjects.Add(minimapConditionalObject);
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		EGameState localGameState = GameManager.LocalGameState;
		if ((int)localGameState == 0 || (int)localGameState == 3 || (int)localGameState == 5)
		{
			if (_state != MinimapState.Inactive)
			{
				SetState(MinimapState.Inactive);
			}
			return;
		}
		if (InputManagerExtra.Instance.ShowMinimapJustPressed)
		{
			if (_state == MinimapState.Inactive)
			{
				SetState(MinimapState.Regular);
			}
			else
			{
				SetState(MinimapState.Inactive);
			}
		}
		else if (_state == MinimapState.Admin && NetworkBool.op_Implicit(PlayerController.Local.IsMoving))
		{
			SetState(MinimapState.Inactive);
		}
		if (_state == MinimapState.Inactive)
		{
			return;
		}
		foreach (MinimapConditionalObject conditionalObject in _conditionalObjects)
		{
			bool flag = conditionalObject.IsConditionMet(((Component)PlayerController.Local.LocalCameraHandler.PovPlayer).transform.position);
			if (conditionalObject.Active && !flag)
			{
				conditionalObject.SetActive(active: false);
			}
			else if (!conditionalObject.Active && flag)
			{
				conditionalObject.SetActive(active: true);
			}
		}
	}

	public void SetState(MinimapState state)
	{
		_state = state;
		_panel.SetActive(state != MinimapState.Inactive);
		RefreshLegendsIfActive();
	}

	public void RefreshLegendsIfActive()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Invalid comparison between Unknown and I4
		if (_state != MinimapState.Inactive)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			_legendWolf.SetActive(_state != MinimapState.Admin);
			_legendPlayer.SetActive(NetworkBool.op_Implicit(player.Clairvoyance));
			_legendLover.SetActive(player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover);
			_legendTarget.SetActive(player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Spy || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary);
			_legendCorpse.SetActive(false);
			_legendBomb.SetActive((int)player.PlayerController.Role == 1 || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor);
			_legendDetector.SetActive(player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Exorcist);
			_legendInvestigatorTarget.SetActive(player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Investigator);
			_legendTeleporterBeacon.SetActive(player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTeleporter);
			_legendMerchantCoin.SetActive(player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothMerchant);
			_legendRadar.SetActive(player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Scout);
			_legendHideout.SetActive(player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Hermit);
			_legendRune.SetActive(player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Runemaster);
		}
	}

	public void UpdatePositionBeforeMeeting(Vector3 position, Quaternion rotation)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		PlayerPositionBeforeMeeting = position;
		PlayerRotationBeforeMeeting = rotation;
	}

	public void AddSabotageIcon(GameObject sabotageObject, Color color)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		Vector3 position = sabotageObject.transform.position;
		position -= customMap.MinimapCameraOffset;
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapSabotageComponent.MinimapSabotagePrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		val2.GetComponent<MinimapSabotageComponent>().Init(sabotageObject, color);
		_sabotageIconsByGameObject[sabotageObject] = val2;
	}

	public void RemoveSabotageIcon(GameObject sabotageObject)
	{
		if (_sabotageIconsByGameObject.ContainsKey(sabotageObject))
		{
			Object.Destroy((Object)(object)_sabotageIconsByGameObject[sabotageObject]);
			_sabotageIconsByGameObject.Remove(sabotageObject);
		}
	}

	public void AddInvestigatorTargetIcon(InvestigatorHint hint)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)hint).transform.position;
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapInvestigatorTargetComponent.MinimapInvestigatorTargetPrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		val2.GetComponent<MinimapInvestigatorTargetComponent>().Init(hint);
	}

	public void AddExorcistDetectorIcon(ExorcistDetector detector)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)detector).transform.position;
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapExorcistDetectorComponent.MinimapExorcistDetectorPrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		val2.GetComponent<MinimapExorcistDetectorComponent>().Init(detector);
	}

	public void AddTeleporterBeaconIcon(Vector3 position)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapTeleporterBeaconComponent.MinimapTeleporterBeaconPrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		val2.GetComponent<MinimapTeleporterBeaconComponent>().Init(position);
		_teleporterBeaconGameObject = val2;
	}

	public void RemoveTeleporterBeaconIcon()
	{
		if ((Object)(object)_teleporterBeaconGameObject != (Object)null)
		{
			Object.Destroy((Object)(object)_teleporterBeaconGameObject);
			_teleporterBeaconGameObject = null;
		}
	}

	public void AddMerchantCoinIcon(MerchantCoin coin)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)coin).transform.position;
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapMerchantCoinComponent.MinimapMerchantCoinPrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		val2.GetComponent<MinimapMerchantCoinComponent>().Init(coin);
	}

	public void AddDetectivePositionIcon(PlayerCustom targetPlayerCustom, Vector3 position)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapDetectivePositionComponent.MinimapDetectivePositionPrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		Color color = ColorManager.GetColor(targetPlayerCustom.ColorIndex);
		color.a = 0.7f;
		((Graphic)val2.GetComponent<Image>()).color = color;
		val2.GetComponent<MinimapDetectivePositionComponent>().Init(position);
	}

	public void AddDeathPositionIcon(Vector3 position)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
			position = new Vector3(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector(0f - position.z, position.x);
			float minimapRotation = customMap.MinimapRotation;
			float num = minimapRotation;
			if (num == 270f)
			{
				((Vector2)(ref val))._002Ector(position.x, position.z);
			}
			val *= customMap.MinimapOffsetMultiplier;
			LycansUtility.AddLogOnlyForMe("MinimapDeathPositionComponent.MinimapDeathPositionPrefab: " + (object)MinimapDeathPositionComponent.MinimapDeathPositionPrefab);
			GameObject val2 = Object.Instantiate<GameObject>(MinimapDeathPositionComponent.MinimapDeathPositionPrefab);
			val2.gameObject.SetActive(true);
			val2.transform.SetParent(_panel.transform);
			val2.transform.localPosition = Vector2.op_Implicit(val);
			val2.transform.SetAsLastSibling();
			val2.GetComponent<MinimapDeathPositionComponent>().Init(position);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogInfo((object)("AddDeathPositionIcon exception: " + ex));
		}
	}

	public void AddSleepingGasIcon(SleepingGasPlaced sleepingGasPlaced)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		Vector3 position = ((Component)sleepingGasPlaced).transform.position;
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapSleepingGasComponent.MinimapSleepingGasPrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		val2.GetComponent<MinimapSleepingGasComponent>().Init(sleepingGasPlaced);
	}

	public void AddScoutRadarIcon(ScoutRadar radar)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)radar).transform.position;
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapScoutRadarComponent.MinimapScoutRadarPrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		float radiusScale = customMap.MinimapOffsetMultiplier / 5.45f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID);
		val2.GetComponent<MinimapScoutRadarComponent>().Init(radar, radiusScale);
	}

	public void AddHermitHideoutIcon(HermitHideout hideout)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)hideout).transform.position;
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapHermitHideoutComponent.MinimapHermitHideoutPrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		float radiusScale = customMap.MinimapOffsetMultiplier / 5.45f;
		val2.GetComponent<MinimapHermitHideoutComponent>().Init(hideout, radiusScale);
	}

	public void AddHostParasiteIcon(HostParasite detector)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)detector).transform.position;
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapHostParasiteComponent.MinimapHostParasitePrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		val2.GetComponent<MinimapHostParasiteComponent>().Init(detector);
	}

	public void AddRunemasterRuneIcon(RunemasterRune rune)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)rune).transform.position;
		CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		((Vector3)(ref position))._002Ector(position.x - customMap.MinimapCameraOffset.x, position.y, position.z - customMap.MinimapCameraOffset.z);
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f - position.z, position.x);
		float minimapRotation = customMap.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val))._002Ector(position.x, position.z);
		}
		val *= customMap.MinimapOffsetMultiplier;
		GameObject val2 = Object.Instantiate<GameObject>(MinimapRunemasterRuneComponent.MinimapRunemasterRunePrefab);
		val2.gameObject.SetActive(true);
		val2.transform.SetParent(_panel.transform);
		val2.transform.localPosition = Vector2.op_Implicit(val);
		val2.transform.SetAsLastSibling();
		val2.GetComponent<MinimapRunemasterRuneComponent>().Init(rune);
	}

	private void UpdateAllTextLocalizations(GameObject parentObject)
	{
		for (int num = parentObject.transform.childCount - 1; num >= 0; num--)
		{
			Transform child = parentObject.transform.GetChild(num);
			if (((Object)child).name.StartsWith("ROOM_"))
			{
				((TMP_Text)((Component)child).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation(((Object)child).name);
			}
		}
	}
}
