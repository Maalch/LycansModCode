using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(1)]
public class GrenadeActive : NetworkBehaviour
{
	public static GameObject GrenadeExplosionParticleSystemPrefab;

	private Rigidbody _rigidbody;

	public bool CreatedByChaosEffect = false;

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe TickTimer ExplosionTimer
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

	private void Awake()
	{
		_rigidbody = ((Component)this).GetComponent<Rigidbody>();
	}

	public void Init(Vector3 velocity)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).GetComponent<Rigidbody>().velocity = velocity;
	}

	public override void Spawned()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Spawned();
		((Component)this).gameObject.layer = 26;
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			ExplosionTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 2f);
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		GameObject val = Object.Instantiate<GameObject>(GrenadeExplosionParticleSystemPrefab, ((Component)this).transform.position, Quaternion.identity);
		val.SetActive(true);
		SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
		MainModule main = val.GetComponent<ParticleSystem>().main;
		selfDestroyingObjectComponent.Init(((MainModule)(ref main)).duration);
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		if ((int)GameManager.LocalGameState != 2)
		{
			if (((SimulationBehaviour)this).HasStateAuthority)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
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
		TickTimer explosionTimer = ExplosionTimer;
		if (!((TickTimer)(ref explosionTimer)).Expired(((SimulationBehaviour)this).Runner) || !((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		ExplosionTimer = TickTimer.None;
		IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Phasing));
		foreach (PlayerCustom item in enumerable)
		{
			float num2 = Vector3.Distance(((Component)this).transform.position, ((Component)item.PlayerController).transform.position);
			if (num2 < 15f)
			{
				float num3 = 1f - num2 / 15f;
				if (item.PlayerController.MovementAction == 1)
				{
					num3 *= 0.5f;
				}
				if (item.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothEngineer)
				{
					num3 *= 0.6f;
				}
				else if (item.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					num3 *= 0.6f;
				}
				if (!LycansUtility.CanPositionSeePlayer(((Component)this).transform.position, item, num2))
				{
					num3 *= 0.6f;
				}
				if (CreatedByChaosEffect)
				{
					num3 *= 0.7f;
				}
				float num4 = Mathf.Lerp(0.4f, 1f, num3);
				float power = 20f * num4;
				Vector3 val2 = ((Component)this).transform.position - ((Component)item.PlayerController).transform.position;
				Vector3 val3 = -((Vector3)(ref val2)).normalized;
				((Component)item.PlayerController).GetComponent<KnockbackComponent>().Init(new Vector3(val3.x, 0f, val3.z), power, 9f);
				float num5 = Mathf.Lerp(0.4f, 1f, num3);
				float num6 = 8f * num5;
				if (item.PlayerController.MovementAction == 1)
				{
					num6 *= 0.5f;
				}
				PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectDisoriented", ((SimulationBehaviour)this).Runner, 1f, num6);
			}
		}
		GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("GrenadeEffect"), ((Component)this).transform.position, 30f, 0.4f);
		((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
	}
}
