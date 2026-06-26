using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using Helpers.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(4)]
public class HermitHideout : NetworkBehaviour
{
	public Teleporter AssociatedTeleporter;

	private PlayerCustom _creatorCustom;

	private GameObject _infoGreen;

	private GameObject _infoYellow;

	private GameObject _infoRed;

	private GameObject _effect;

	private Stopwatch _nextCheckWatch = new Stopwatch();

	private Stopwatch _hideWatch = new Stopwatch();

	private Collider _collider;

	private float _radius;

	public static List<HermitHideout> AllHideouts = new List<HermitHideout>();

	[Networked(OnChanged = "CreatorRefChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe PlayerRef CreatorRef
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing HermitHideout.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing HermitHideout.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe int RemainingDuration
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing HermitHideout.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing HermitHideout.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	public void Awake()
	{
		_collider = (Collider)(object)((Component)this).GetComponent<BoxCollider>();
		_effect = ((Component)((Component)this).transform.Find("HermitHideoutEffect")).gameObject;
		_infoGreen = ((Component)((Component)this).transform.Find("InfoGreen")).gameObject;
		_infoYellow = ((Component)((Component)this).transform.Find("InfoYellow")).gameObject;
		_infoRed = ((Component)((Component)this).transform.Find("InfoRed")).gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)((SimulationBehaviour)this).Runner == (Object)null) && ((SimulationBehaviour)this).Runner.IsServer && LycansUtility.WolvesCanTransform)
		{
			PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
			if ((Object)(object)component != (Object)null && component.Ref == CreatorRef)
			{
				PlayerCustom.ApplyEffectToPlayer(component, "LycansNewRoles.EffectHidden", ((SimulationBehaviour)this).Runner, 1f, 2f);
				_hideWatch.Restart();
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)((SimulationBehaviour)this).Runner == (Object)null) && ((SimulationBehaviour)this).Runner.IsServer && LycansUtility.WolvesCanTransform && (!_hideWatch.IsRunning || _hideWatch.ElapsedMilliseconds >= 1000))
		{
			PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
			if ((Object)(object)component != (Object)null && component.Ref == CreatorRef)
			{
				PlayerCustom.ApplyEffectToPlayer(component, "LycansNewRoles.EffectHidden", ((SimulationBehaviour)this).Runner, 1f, 2f);
				_hideWatch.Restart();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
			if ((Object)(object)component != (Object)null && component.Ref == CreatorRef)
			{
				_hideWatch.Reset();
			}
		}
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	public void Init(PlayerRef playerRef, Teleporter teleporter, int duration)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
		AssociatedTeleporter = teleporter;
		RemainingDuration = duration;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<HermitHideout> changed)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		HermitHideout behaviour = changed.Behaviour;
		AllHideouts.Add(behaviour);
		behaviour._radius = 12f;
		behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(behaviour.CreatorRef);
		if (behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
		{
			Plugin.Minimap.AddHermitHideoutIcon(behaviour);
		}
		behaviour._nextCheckWatch.Restart();
		behaviour.UpdateVisibility();
	}

	private void UpdateVisibility()
	{
		if (!((Object)(object)((SimulationBehaviour)this).Runner == (Object)null))
		{
			_effect.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved);
		}
	}

	public static void UpdateVisibilityForAllHideouts()
	{
		foreach (HermitHideout allHideout in AllHideouts)
		{
			allHideout.UpdateVisibility();
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Invalid comparison between Unknown and I4
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Invalid comparison between Unknown and I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Invalid comparison between Unknown and I4
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)((SimulationBehaviour)this).Runner == (Object)null)
		{
			LycansUtility.AddLogOnlyForMe("HermitHideout: this.Runner is null");
		}
		if (((SimulationBehaviour)this).HasStateAuthority && (Object)(object)_creatorCustom != (Object)null)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || val - 4 <= 1)
			{
				flag = true;
			}
			if ((Object)(object)_creatorCustom.PlayerController == (Object)null)
			{
				LycansUtility.AddLogOnlyForMe("HermitHideout: _creatorCustom.PlayerController is null");
			}
			if (!PlayerRegistry.HasPlayer(CreatorRef) || NetworkBool.op_Implicit(_creatorCustom.PlayerController.IsDead))
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				return;
			}
		}
		if ((Object)(object)_creatorCustom == (Object)null || (int)GameManager.LocalGameState != 2)
		{
			return;
		}
		if (_nextCheckWatch.ElapsedMilliseconds >= 1000)
		{
			if (((SimulationBehaviour)this).Runner.IsServer && LycansUtility.GameActuallyInPlay)
			{
				if (_hideWatch.IsRunning)
				{
					RemainingDuration -= 3;
				}
				else
				{
					RemainingDuration--;
				}
				if (RemainingDuration <= 0)
				{
					((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				}
			}
			_nextCheckWatch.Restart();
		}
		if (_creatorCustom.IsCurrentlyPlayedOrObserved)
		{
			if (RemainingDuration < 30)
			{
				_infoGreen.SetActive(false);
				_infoYellow.SetActive(false);
				_infoRed.SetActive(true);
			}
			else if (RemainingDuration < 60)
			{
				_infoGreen.SetActive(false);
				_infoYellow.SetActive(true);
				_infoRed.SetActive(false);
			}
			else
			{
				_infoGreen.SetActive(true);
				_infoYellow.SetActive(false);
				_infoRed.SetActive(false);
			}
		}
		else
		{
			_infoGreen.SetActive(false);
			_infoYellow.SetActive(false);
			_infoRed.SetActive(false);
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		_creatorCustom = null;
		AllHideouts.Remove(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public static Teleporter CreateNewHideout(NetworkRunner runner, PlayerCustom playerCustom, List<Teleporter> teleportersToExclude)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		teleportersToExclude.AddRange(from o in AllHideouts
			where o.CreatorRef == playerCustom.Ref
			select o.AssociatedTeleporter);
		try
		{
			float minimumDistanceFromOtherHideouts = 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectHermitHideout");
			Vector3 playerPosition = playerCustom.ActualPositionIncludingTeleport();
			List<Teleporter> list = (from o in Object.FindObjectsOfType<Teleporter>()
				where o.MapID == GameManager.Instance.MapID && Vector3.Distance(((Component)o).transform.position, playerPosition) >= 15f && !teleportersToExclude.Any((Teleporter j) => (Object)(object)j == (Object)(object)o || Vector3.Distance(((Component)j).transform.position, ((Component)o).transform.position) < minimumDistanceFromOtherHideouts)
				select o).ToList();
			if (list.Count == 0)
			{
				list = (from o in Object.FindObjectsOfType<Teleporter>()
					where o.MapID == GameManager.Instance.MapID && !teleportersToExclude.Any((Teleporter j) => (Object)(object)j == (Object)(object)o)
					select o).ToList();
			}
			if (list.Count > 0)
			{
				Teleporter val = CollectionsUtil.Grab<Teleporter>(list, 1).First();
				Vector3 position = new Vector3(((Component)val).transform.position.x, ((Component)val).transform.position.y + 0.25f, ((Component)val).transform.position.z);
				NetworkObject val2 = runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					//IL_0008: Unknown result type (might be due to invalid IL or missing references)
					((Component)no).transform.position = position;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				((Component)val2).transform.position = position;
				((Component)val2).GetComponent<HermitHideout>().Init(playerCustom.Ref, val, 80);
				return val;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Exception in CreateNewHideouts: " + ex));
			if (teleportersToExclude.Any((Teleporter o) => (Object)(object)o == (Object)null))
			{
				Plugin.Logger.LogError((object)"-> Some teleporters are null");
			}
			if (teleportersToExclude.Any((Teleporter o) => (Object)(object)((Component)o).transform == (Object)null))
			{
				Plugin.Logger.LogError((object)"-> Some teleporter transforms are null");
				bool flag = teleportersToExclude.Any((Teleporter o) => (Object)(object)((Component)o).transform == (Object)null && AllHideouts.Any((HermitHideout j) => (Object)(object)j == (Object)(object)o));
				Plugin.Logger.LogError((object)("-> From hideouts: " + flag));
			}
		}
		return null;
	}

	public static void CreateHideoutsOnNewDay(NetworkRunner runner, PlayerCustom playerCustom)
	{
		List<Teleporter> list = new List<Teleporter>();
		for (int i = 0; i < 4; i++)
		{
			Teleporter val = CreateNewHideout(runner, playerCustom, list);
			if ((Object)(object)val != (Object)null)
			{
				list.Add(val);
			}
		}
		playerCustom.TriggerPrimaryRolePowerCooldown(runner);
	}
}
