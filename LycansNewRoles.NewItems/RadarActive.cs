using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(2)]
public class RadarActive : NetworkBehaviour
{
	public static GameObject GrenadeExplosionParticleSystemPrefab;

	private Rigidbody _rigidbody;

	private Stopwatch _pulseWatch = new Stopwatch();

	private bool _detectsWolves = false;

	public bool CreatedByChaosEffect = false;

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
				throw new InvalidOperationException("Error when accessing GrenadeActive.ExplosionTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GrenadeActive.ExplosionTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe TickTimer DisappearTimer
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GrenadeActive.DisappearTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GrenadeActive.DisappearTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	private void Awake()
	{
		_rigidbody = ((Component)this).GetComponent<Rigidbody>();
	}

	public void Init(Vector3 velocity, bool detectsWolves)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).GetComponent<Rigidbody>().velocity = velocity;
		_detectsWolves = detectsWolves;
	}

	public override void Spawned()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Spawned();
		((Component)this).gameObject.layer = 26;
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			ActivationTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 2f);
			DisappearTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 23f);
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		if ((int)GameManager.LocalGameState != 2)
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			return;
		}
		Vector3 velocity = ((Component)this).GetComponent<Rigidbody>().velocity;
		velocity.x *= 1f - 0.5f * ((SimulationBehaviour)this).Runner.DeltaTime;
		velocity.z *= 1f - 0.5f * ((SimulationBehaviour)this).Runner.DeltaTime;
		RaycastHit val = default(RaycastHit);
		if (_rigidbody.velocity.y <= 0f && _rigidbody.velocity.y > -0.1f && Physics.Raycast(((Component)this).transform.position, Vector3.down, ref val, 0.1f))
		{
			velocity.x *= 1f - 60f * ((SimulationBehaviour)this).Runner.DeltaTime;
			velocity.z *= 1f - 60f * ((SimulationBehaviour)this).Runner.DeltaTime;
		}
		_rigidbody.velocity = velocity;
		float num = Mathf.Abs(_rigidbody.velocity.x) + Mathf.Abs(_rigidbody.velocity.z);
		if (num >= 1f)
		{
			((Component)this).transform.Rotate(0f, 0f, Time.deltaTime * num * 10f);
		}
		TickTimer val2 = ActivationTimer;
		if (((TickTimer)(ref val2)).Expired(((SimulationBehaviour)this).Runner) && ((SimulationBehaviour)this).HasStateAuthority)
		{
			ActivationTimer = TickTimer.None;
			Pulse();
			_pulseWatch.Restart();
		}
		val2 = DisappearTimer;
		if (((TickTimer)(ref val2)).Expired(((SimulationBehaviour)this).Runner))
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
		if (_pulseWatch.ElapsedMilliseconds >= 2000)
		{
			Pulse();
			_pulseWatch.Restart();
		}
	}

	private void Pulse()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("Radar"), ((Component)this).transform.position, 40f, 0.8f);
		IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Phasing) && ((_detectsWolves && NetworkBool.op_Implicit(o.PlayerController.IsWolf)) || (!_detectsWolves && !NetworkBool.op_Implicit(o.PlayerController.IsWolf))));
		float num = (_detectsWolves ? (40f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID)) : (15f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID)));
		foreach (PlayerCustom item in enumerable)
		{
			float num2 = Vector3.Distance(((Component)this).transform.position, ((Component)item.PlayerController).transform.position);
			if (num2 < num && item.SecondaryRole != PlayerCustom.PlayerSecondaryRole.BothEngineer)
			{
				PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectTracked", ((SimulationBehaviour)this).Runner, 1f, 3f);
			}
		}
		GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("GrenadeEffect"), ((Component)this).transform.position, 30f, 0.4f);
	}
}
