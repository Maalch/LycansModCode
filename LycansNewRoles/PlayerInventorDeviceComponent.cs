using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using LycansNewRoles.PowerObjects;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerInventorDeviceComponent : MonoBehaviour
{
	private bool _gaveError = false;

	private string _username;

	private PlayerCustom _playerCustom;

	private Stopwatch _destructionWatch = new Stopwatch();

	private Stopwatch _nextSmokeWatch = new Stopwatch();

	private Stopwatch _alertCooldownWatch = new Stopwatch();

	private Stopwatch _nextDeviceCheckWatch = new Stopwatch();

	private static Dictionary<int, float> AlertRangesAndCooldowns = new Dictionary<int, float>
	{
		{ 15, 2f },
		{ 10, 1f },
		{ 5, 0.5f }
	};

	public void Init(PlayerCustom playerCustom)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		_playerCustom = playerCustom;
		Remove();
		_username = ((object)_playerCustom.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
	}

	public void PlaceDevice(PlayerRef inventorRef)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_playerCustom.InventorDeviceRef = inventorRef;
		_destructionWatch.Reset();
		_nextSmokeWatch.Reset();
	}

	public bool CanActivate()
	{
		return !_destructionWatch.IsRunning;
	}

	public void Activate(PlayerCustom wolf)
	{
		_destructionWatch.Start();
		_nextSmokeWatch.Start();
		AddSmoke();
		PlayerCustom.ApplyEffectToPlayer(wolf.PlayerController, "LycansNewRoles.EffectDisoriented", ((SimulationBehaviour)wolf).Runner, 1f, 1.5f);
		PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)wolf).Runner, wolf.Index, 18);
	}

	private void Update()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_playerCustom == (Object)null)
		{
			if (!_gaveError)
			{
				LycansUtility.AddLogOnlyForMe("InventorDevice: _playerCustom is null, player: " + _username);
				_gaveError = true;
			}
			return;
		}
		if (((SimulationBehaviour)_playerCustom).Runner.IsServer && !NetworkBool.op_Implicit(_playerCustom.PlayerController.IsWolf) && _playerCustom.InventorDeviceRef != PlayerRef.None && _playerCustom.InventorDeviceInfo.CanActivate() && (!_nextDeviceCheckWatch.IsRunning || _nextDeviceCheckWatch.ElapsedMilliseconds >= 200))
		{
			_nextDeviceCheckWatch.Restart();
			PlayerCustom playerCustom = PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf) && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != _playerCustom.Ref && Vector3.Distance(((Component)_playerCustom.PlayerController).transform.position, ((Component)o.PlayerController).transform.position) <= 12f && LycansUtility.CanPlayerSeeOtherPlayer(o, _playerCustom, 12f)).FirstOrDefault();
			if ((Object)(object)playerCustom != (Object)null)
			{
				_playerCustom.InventorDeviceInfo.Activate(playerCustom);
				PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)_playerCustom).Runner, _playerCustom.Index, 17);
				_nextDeviceCheckWatch.Reset();
				return;
			}
		}
		if (_nextSmokeWatch.IsRunning && (float)_nextSmokeWatch.ElapsedMilliseconds >= 1000f)
		{
			AddSmoke();
			_nextSmokeWatch.Restart();
		}
		if ((float)_destructionWatch.ElapsedMilliseconds >= 11000f)
		{
			Remove();
		}
	}

	public void Remove()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		_destructionWatch.Reset();
		_nextSmokeWatch.Reset();
		_playerCustom.InventorDeviceRef = PlayerRef.None;
	}

	private void AddSmoke()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectInventorSmoke");
		Vector3 inventorSmokePosition = ((Component)_playerCustom.PlayerController).transform.position;
		NetworkObject val = ((SimulationBehaviour)_playerCustom.PlayerController).Runner.Spawn(networkObject, (Vector3?)((Component)_playerCustom.PlayerController).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			((Component)no).transform.position = inventorSmokePosition;
		}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		((Component)val).transform.position = inventorSmokePosition;
		((Component)val).GetComponent<InventorSmoke>().SetCreatorRef(_playerCustom.InventorDeviceRef);
	}

	public void CheckAlert()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		float? num = null;
		foreach (InventorScrap item in InventorScrap.AllScraps.Where((InventorScrap o) => o.CreatorRef == _playerCustom.Ref))
		{
			float num2 = Vector3.Distance(((Component)_playerCustom.PlayerController).transform.position, ((Component)item).transform.position);
			foreach (KeyValuePair<int, float> item2 in AlertRangesAndCooldowns.OrderByDescending((KeyValuePair<int, float> o) => o.Key))
			{
				if (num2 <= (float)item2.Key)
				{
					num = item2.Value;
				}
			}
		}
		if (num.HasValue && (!_alertCooldownWatch.IsRunning || (float)_alertCooldownWatch.ElapsedMilliseconds >= num.Value * 1000f))
		{
			AudioManager.Play("InventorScrapAlert", (MixerTarget)2, 0.45f, 1f);
			_alertCooldownWatch.Restart();
		}
	}
}
