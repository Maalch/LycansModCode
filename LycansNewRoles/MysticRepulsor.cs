using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(4)]
public class MysticRepulsor : NetworkBehaviour
{
	private PlayerCustom _creatorCustom;

	private GameObject _effect;

	private Stopwatch _nextCheckWatch = new Stopwatch();

	private Stopwatch _soundWatch = new Stopwatch();

	private float _radius;

	public static List<MysticRepulsor> AllRepulsors = new List<MysticRepulsor>();

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
				throw new InvalidOperationException("Error when accessing MysticRepulsor.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MysticRepulsor.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
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
				throw new InvalidOperationException("Error when accessing MysticRepulsor.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MysticRepulsor.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	public void Awake()
	{
		_effect = ((Component)((Component)this).transform.Find("MysticRepulsorEffect")).gameObject;
		_soundWatch.Start();
	}

	public void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState != 2)
		{
			return;
		}
		if (_soundWatch.ElapsedMilliseconds >= 1500)
		{
			PlaySoundIfNeeded();
			_soundWatch.Restart();
		}
		if (!((SimulationBehaviour)this).Runner.IsServer || _nextCheckWatch.ElapsedMilliseconds < 1000)
		{
			return;
		}
		List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf) && (int)o.PlayerController.Role == 1 && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Banished) && Vector3.Distance(((Component)this).transform.position, ((Component)o.PlayerController).transform.position) <= _radius).ToList();
		using (List<PlayerCustom>.Enumerator enumerator = list.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				PlayerCustom current = enumerator.Current;
				float num = Vector3.Distance(((Component)this).transform.position, ((Component)current.PlayerController).transform.position);
				int num2 = 175;
				float num3 = 1f - num / _radius;
				float num4 = Mathf.Lerp(1f, 0.35f, num3);
				num2 = Mathf.RoundToInt((float)num2 * num4);
				current.RepulsionStacks += num2;
				if (current.RepulsionStacks >= 1000)
				{
					current.RepulsionStacks = 0;
					PlayerCustom.ApplyEffectToPlayer(current.PlayerController, "LycansNewRoles.EffectBanished", ((SimulationBehaviour)this).Runner, 1f, 8f);
				}
				else
				{
					PlayerCustom.ApplyEffectToPlayer(current.PlayerController, "LycansNewRoles.EffectRepulsion", ((SimulationBehaviour)this).Runner);
				}
			}
		}
		RemainingDuration--;
		if (RemainingDuration <= 0)
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
		_nextCheckWatch.Restart();
	}

	private void PlaySoundIfNeeded()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
		if ((Object)(object)povPlayer != (Object)null && (povPlayer.Ref == CreatorRef || (NetworkBool.op_Implicit(povPlayer.IsWolf) && (int)povPlayer.Role == 1)))
		{
			AudioManager.PlayPosition("Repulsor", ((Component)this).transform.position, (MixerTarget)2, 15f, 0.5f);
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
	public static void CreatorRefChanged(Changed<MysticRepulsor> changed)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		MysticRepulsor behaviour = changed.Behaviour;
		behaviour._radius = 30f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID);
		changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(behaviour.CreatorRef);
		AllRepulsors.Add(behaviour);
		if (((SimulationBehaviour)changed.Behaviour).Runner.IsServer)
		{
			behaviour._nextCheckWatch.Restart();
		}
		behaviour.UpdateVisibility();
		changed.Behaviour.PlaySoundIfNeeded();
	}

	private void UpdateVisibility()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Invalid comparison between Unknown and I4
		if (!((Object)(object)((SimulationBehaviour)this).Runner == (Object)null))
		{
			_effect.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved || (NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.IsWolf) && (int)PlayerController.Local.LocalCameraHandler.PovPlayer.Role == 1));
		}
	}

	public static void UpdateVisibilityForAllRepulsors()
	{
		MysticRepulsor[] array = Object.FindObjectsOfType<MysticRepulsor>();
		MysticRepulsor[] array2 = array;
		foreach (MysticRepulsor mysticRepulsor in array2)
		{
			mysticRepulsor.UpdateVisibility();
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
		AllRepulsors.Remove(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
