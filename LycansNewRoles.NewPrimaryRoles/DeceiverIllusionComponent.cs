using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewPrimaryRoles;

[NetworkBehaviourWeaved(3)]
public class DeceiverIllusionComponent : NetworkBehaviour
{
	public static List<DeceiverIllusionComponent> Illusions = new List<DeceiverIllusionComponent>();

	private ParticleSystem _smoke;

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe TickTimer DisappearTimer
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing DeceiverIllusionComponent.DisappearTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing DeceiverIllusionComponent.DisappearTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "CreatorRefChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe PlayerRef CreatorRef
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing DeceiverIllusionComponent.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[1];
		}
		private set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing DeceiverIllusionComponent.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe TickTimer StopSmokeTimer
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing DeceiverIllusionComponent.StopSmokeTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[2];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing DeceiverIllusionComponent.StopSmokeTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 2, value);
		}
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<DeceiverIllusionComponent> changed)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		Illusions.Add(changed.Behaviour);
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
		switch (player.PrimaryRolePower)
		{
		case PlayerCustom.PlayerPrimaryRolePower.Deceiver:
		{
			AudioManager.PlayAndFollow("WOLF_TRANSFORM", ((Component)changed.Behaviour).transform, (MixerTarget)2, 30f, 1f);
			GameObject val2 = Object.Instantiate<GameObject>(((Component)Traverse.Create((object)player.PlayerController).Field<ParticleSystem>("smokeParticleSystem").Value).gameObject, ((Component)changed.Behaviour).transform);
			changed.Behaviour._smoke = val2.GetComponent<ParticleSystem>();
			changed.Behaviour._smoke.Play();
			break;
		}
		case PlayerCustom.PlayerPrimaryRolePower.Specter:
			if (!NetworkBool.op_Implicit(PlayerController.Local.IsWolf))
			{
				AudioManager.PlayAndFollow("WOLF_TRANSFORM", ((Component)changed.Behaviour).transform, (MixerTarget)2, 30f, 1f);
				GameObject val = Object.Instantiate<GameObject>(((Component)Traverse.Create((object)player.PlayerController).Field<ParticleSystem>("smokeParticleSystem").Value).gameObject, ((Component)changed.Behaviour).transform);
				changed.Behaviour._smoke = val.GetComponent<ParticleSystem>();
				changed.Behaviour._smoke.Play();
			}
			break;
		}
		if (((SimulationBehaviour)changed.Behaviour).HasStateAuthority)
		{
			changed.Behaviour.DisappearTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)changed.Behaviour).Runner, Random.Range(8f, 15f));
			if (player.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.Sneak)
			{
				changed.Behaviour.StopSmokeTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)changed.Behaviour).Runner, (float)GameManager.Instance.TransformationTime);
			}
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState != 2)
		{
			if (((SimulationBehaviour)this).HasStateAuthority)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
			return;
		}
		TickTimer val = StopSmokeTimer;
		if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
		{
			StopSmokeTimer = TickTimer.None;
			if ((Object)(object)_smoke != (Object)null)
			{
				_smoke.Stop();
			}
		}
		val = DisappearTimer;
		if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner) && ((SimulationBehaviour)this).HasStateAuthority)
		{
			DisappearTimer = TickTimer.None;
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Illusions.Remove(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
