using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(4)]
public class TrackerRadar : NetworkBehaviour
{
	private PlayerCustom _creatorCustom;

	private GameObject _effect;

	private Stopwatch _nextCheckWatch = new Stopwatch();

	private float _radius;

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
				throw new InvalidOperationException("Error when accessing ScoutRadar.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing ScoutRadar.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
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
				throw new InvalidOperationException("Error when accessing ScoutRadar.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing ScoutRadar.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	public void Awake()
	{
		_effect = ((Component)((Component)this).transform.Find("TrackerRadarEffect")).gameObject;
	}

	public void Update()
	{
		if (_nextCheckWatch.ElapsedMilliseconds < 1000)
		{
			return;
		}
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			List<PlayerController> list = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead) && Vector3.Distance(((Component)this).transform.position, ((Component)o).transform.position) <= _radius)).ToList();
			foreach (PlayerController item in list)
			{
				PlayerCustom.ApplyEffectToPlayer(item, "LycansNewRoles.EffectTracked", ((SimulationBehaviour)this).Runner, 1f, 3f);
			}
			RemainingDuration--;
			if (RemainingDuration <= 0)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
		_nextCheckWatch.Restart();
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	public void Init(int duration)
	{
		RemainingDuration = duration;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<TrackerRadar> changed)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		TrackerRadar behaviour = changed.Behaviour;
		behaviour._radius = 30f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID);
		behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(behaviour.CreatorRef);
		behaviour._nextCheckWatch.Restart();
		behaviour.UpdateVisibility();
	}

	private void UpdateVisibility()
	{
		_effect.SetActive(true);
	}

	public override void FixedUpdateNetwork()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Invalid comparison between Unknown and I4
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority && (Object)(object)_creatorCustom != (Object)null)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || val - 3 <= 2)
			{
				flag = true;
			}
			if (!PlayerRegistry.HasPlayer(CreatorRef) || NetworkBool.op_Implicit(_creatorCustom.PlayerController.IsDead))
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
