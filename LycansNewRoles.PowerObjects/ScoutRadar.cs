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
public class ScoutRadar : NetworkBehaviour
{
	private PlayerCustom _creatorCustom;

	private GameObject _effect;

	private Stopwatch _nextCheckWatch = new Stopwatch();

	private float _radius;

	public static List<ScoutRadar> AssociatedRadars = new List<ScoutRadar>();

	private List<PlayerRef> _wolvesInRange = new List<PlayerRef>();

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

	public List<PlayerRef> WolvesInRange => _wolvesInRange;

	public void Awake()
	{
		_effect = ((Component)((Component)this).transform.Find("ScoutRadarEffect")).gameObject;
	}

	public void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Invalid comparison between Unknown and I4
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState != 2)
		{
			_wolvesInRange.Clear();
		}
		else
		{
			if (_nextCheckWatch.ElapsedMilliseconds < 1000)
			{
				return;
			}
			if (_creatorCustom.IsCurrentlyPlayedOrObserved)
			{
				List<PlayerController> list = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead) && Vector3.Distance(((Component)this).transform.position, ((Component)o).transform.position) <= _radius)).ToList();
				foreach (PlayerController wolf in list)
				{
					if (!_wolvesInRange.Any((PlayerRef o) => o == wolf.Ref))
					{
						AudioManager.PlayPosition("ScoutAlert", ((Component)wolf).transform.position, (MixerTarget)2, 500f, 1f);
					}
				}
				_wolvesInRange = list.Select((PlayerController o) => o.Ref).ToList();
				_creatorCustom.UpdateTargetArrowComponent();
				PlayerCustomRegistry.AllPlayers.ForEach(delegate(PlayerCustom o)
				{
					o.UpdateVisibility();
				});
			}
			if (((SimulationBehaviour)this).Runner.IsServer && LycansUtility.WolvesCanTransform && (int)GameManager.LocalGameState == 2)
			{
				RemainingDuration--;
				if (RemainingDuration <= 0)
				{
					((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				}
			}
			_nextCheckWatch.Restart();
		}
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
	public static void CreatorRefChanged(Changed<ScoutRadar> changed)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		ScoutRadar behaviour = changed.Behaviour;
		behaviour._radius = 36f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID);
		behaviour._wolvesInRange.Clear();
		behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(behaviour.CreatorRef);
		if (behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
		{
			AssociatedRadars.Add(behaviour);
			Plugin.Minimap.AddScoutRadarIcon(behaviour);
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

	public static void UpdateVisibilityForAllRadars()
	{
		ScoutRadar[] array = Object.FindObjectsOfType<ScoutRadar>();
		ScoutRadar[] array2 = array;
		foreach (ScoutRadar scoutRadar in array2)
		{
			scoutRadar.UpdateVisibility();
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority && (Object)(object)_creatorCustom != (Object)null)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || (int)val == 5)
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		AssociatedRadars.Remove(this);
		if (CreatorRef == PlayerCustom.Local.Ref)
		{
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.UpdateVisibility();
			}
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
