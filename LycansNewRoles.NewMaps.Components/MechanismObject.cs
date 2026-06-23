using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

[NetworkBehaviourWeaved(2)]
public class MechanismObject : NetworkBehaviour
{
	public string MechanismId;

	public int Duration;

	public int Cooldown;

	public bool AutoActivation;

	public string SoundOnActivation;

	public string SoundOnDeactivation;

	public Vector3 PlayerShiftOnActivation;

	public Vector3 PlayerShiftOnDeactivation;

	public int MapID;

	private Animator _animator;

	private List<PlayerRef> _playersToMoveAlong = new List<PlayerRef>();

	private List<GameObject> _otherObjectsToMoveAlong = new List<GameObject>();

	private Vector3 _previousPosition;

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe TickTimer ActivationTimer
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MechanismObject.ActivationTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MechanismObject.ActivationTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe TickTimer CooldownTimer
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MechanismObject.CooldownTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MechanismObject.CooldownTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	private void Start()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)GameManager.Instance == (Object)null)
			{
				return;
			}
			float? num = null;
			IEnumerable<GameObject> enumerable = from o in Object.FindObjectsOfType<GameObject>()
				where ((Object)o).name == "MechanismObjects"
				select o;
			Vector3 val = default(Vector3);
			((Vector3)(ref val))._002Ector(((Component)this).transform.position.x, ((Component)this).transform.position.y, ((Component)this).transform.position.z);
			Transform parent = null;
			foreach (GameObject item in enumerable)
			{
				for (int num2 = 0; num2 < item.transform.childCount; num2++)
				{
					Transform child = item.transform.GetChild(num2);
					float num3 = Vector3.Distance(val, ((Component)child).transform.position);
					if (!num.HasValue || num3 < num.Value)
					{
						num = num3;
						parent = child;
					}
				}
			}
			((Component)this).gameObject.transform.parent = parent;
			MapManager.RescaleSpawnedObject(((Component)this).gameObject, ((Component)((Component)this).transform.parent).gameObject, MapManager.NewMapsByIdInfo[GameManager.Instance.MapID]);
			_animator = ((Component)this).GetComponent<Animator>();
			Transform val2 = ((Component)this).transform.Find("MovementCollider");
			if ((Object)(object)val2 != (Object)null)
			{
				((Component)val2).gameObject.AddComponent<MechanismObjectMovementCollider>();
			}
			if (((SimulationBehaviour)this).Runner.IsServer)
			{
				Transform val3 = ((Component)this).transform.Find("TeleportOnDeactivate");
				if ((Object)(object)val3 != (Object)null)
				{
					((Component)val3).gameObject.AddComponent<MechanismObjectTeleportOnDeactivateComponent>();
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MechanismObject start error: " + ex));
		}
	}

	public void Activate()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		TickTimer val = ActivationTimer;
		if (((TickTimer)(ref val)).IsRunning)
		{
			return;
		}
		val = CooldownTimer;
		if (((TickTimer)(ref val)).IsRunning)
		{
			return;
		}
		if (!string.IsNullOrEmpty(SoundOnActivation))
		{
			GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit(SoundOnActivation), ((Component)this).transform.position, 10f, 1f);
		}
		_animator.SetBool("Active", true);
		ActivationTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, (float)Duration);
		CooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, (float)Cooldown);
		_ = PlayerShiftOnActivation;
		if (1 == 0)
		{
			return;
		}
		foreach (PlayerRef item in _playersToMoveAlong)
		{
			MovePlayer(item, PlayerShiftOnActivation);
		}
		foreach (GameObject item2 in _otherObjectsToMoveAlong)
		{
			MoveObject(item2, PlayerShiftOnActivation);
		}
	}

	public void Deactivate()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (!string.IsNullOrEmpty(SoundOnDeactivation))
		{
			GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit(SoundOnDeactivation), ((Component)this).transform.position, 10f, 1f);
		}
		_animator.SetBool("Active", false);
		_ = PlayerShiftOnDeactivation;
		if (1 == 0)
		{
			return;
		}
		foreach (PlayerRef item in _playersToMoveAlong)
		{
			MovePlayer(item, PlayerShiftOnDeactivation);
		}
		foreach (GameObject item2 in _otherObjectsToMoveAlong)
		{
			MoveObject(item2, PlayerShiftOnDeactivation);
		}
	}

	public void AddPlayerInDetection(PlayerRef playerRef)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (!_playersToMoveAlong.Contains(playerRef))
		{
			_playersToMoveAlong.Add(playerRef);
		}
	}

	public void RemovePlayerFromDetection(PlayerRef playerRef)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_playersToMoveAlong.Remove(playerRef);
	}

	public void AddObjectInDetection(GameObject obj)
	{
		if (!_otherObjectsToMoveAlong.Contains(obj))
		{
			_otherObjectsToMoveAlong.Add(obj);
		}
	}

	public void RemoveObjectFromDetection(GameObject obj)
	{
		_otherObjectsToMoveAlong.Remove(obj);
	}

	private void Update()
	{
		MovePlayersAlong();
	}

	private void MovePlayersAlong()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)this).transform.position;
		if (_previousPosition != ((Component)this).transform.position)
		{
			Vector3 delta = position - _previousPosition;
			foreach (PlayerRef item in _playersToMoveAlong)
			{
				MovePlayer(item, delta);
			}
			foreach (GameObject item2 in _otherObjectsToMoveAlong)
			{
				MoveObject(item2, delta);
			}
		}
		_previousPosition = position;
	}

	private void MovePlayer(PlayerRef playerRef, Vector3 delta)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		PlayerController player = PlayerRegistry.GetPlayer(playerRef);
		if ((Object)(object)player == (Object)null)
		{
			_playersToMoveAlong.Remove(playerRef);
			return;
		}
		NetworkCharacterControllerPrototypeCustom value = Traverse.Create((object)player.CharacterMovementHandler).Field<NetworkCharacterControllerPrototypeCustom>("_networkCharacterControllerPrototypeCustom").Value;
		CharacterController controller = value.Controller;
		controller.Move(delta);
	}

	private void MoveObject(GameObject obj, Vector3 delta)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)obj == (Object)null)
		{
			_otherObjectsToMoveAlong.Remove(obj);
		}
		Transform transform = obj.transform;
		transform.position += delta;
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).Runner.IsServer)
		{
			return;
		}
		TickTimer val = ActivationTimer;
		if (((TickTimer)(ref val)).IsRunning)
		{
			val = ActivationTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				Deactivate();
				ActivationTimer = TickTimer.None;
			}
		}
		val = CooldownTimer;
		if (((TickTimer)(ref val)).IsRunning)
		{
			val = CooldownTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				CooldownTimer = TickTimer.None;
			}
		}
		val = CooldownTimer;
		if (!((TickTimer)(ref val)).IsRunning && AutoActivation)
		{
			Activate();
		}
	}
}
