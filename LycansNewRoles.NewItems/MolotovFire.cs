using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(2)]
public class MolotovFire : NetworkBehaviour
{
	private ParticleSystem _particleSystem;

	private Stopwatch _applyBurnWatch = new Stopwatch();

	private Stopwatch _disappearWatch = new Stopwatch();

	private float _burnDuration;

	private const int ApplyBurnIntervalMilliseconds = 1000;

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe int DurationMilliseconds
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MolotovFire.DurationMilliseconds. Networked properties can only be accessed when Spawned() has been called.");
			}
			return *base.Ptr;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MolotovFire.DurationMilliseconds. Networked properties can only be accessed when Spawned() has been called.");
			}
			*base.Ptr = value;
		}
	}

	private void Awake()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		_particleSystem = ((Component)this).GetComponentInChildren<ParticleSystem>();
		_applyBurnWatch.Start();
		_disappearWatch.Start();
		((Component)_particleSystem).transform.localScale = Vector3.zero;
	}

	public void Init(int durationMilliseconds, float burnDuration)
	{
		DurationMilliseconds = durationMilliseconds;
		_burnDuration = burnDuration;
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		((NetworkBehaviour)this).Despawned(runner, hasState);
		if ((int)GameManager.LocalGameState != 0)
		{
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Invalid comparison between Unknown and I4
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Invalid comparison between Unknown and I4
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Invalid comparison between Unknown and I4
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState == 5 || (int)GameManager.LocalGameState == 1 || (int)GameManager.LocalGameState == 0)
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			return;
		}
		float num = ((_disappearWatch.ElapsedMilliseconds <= DurationMilliseconds - 1000) ? Mathf.InverseLerp(0f, 1000f, (float)_disappearWatch.ElapsedMilliseconds) : Mathf.InverseLerp((float)DurationMilliseconds, (float)(DurationMilliseconds - 1000), (float)_disappearWatch.ElapsedMilliseconds));
		((Component)_particleSystem).transform.localScale = new Vector3(num, num, num);
		if (!((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		if ((int)GameManager.LocalGameState != 2 && (int)GameManager.LocalGameState != 3)
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			return;
		}
		if (_applyBurnWatch.ElapsedMilliseconds >= 1000)
		{
			if (LycansUtility.GameActuallyInPlay)
			{
				IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Phasing));
				foreach (PlayerCustom item in enumerable)
				{
					float num2 = Vector3.Distance(((Component)this).transform.position, ((Component)item.PlayerController).transform.position);
					if (num2 <= 2f)
					{
						PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectBurning", ((SimulationBehaviour)this).Runner, 1f, _burnDuration);
					}
				}
			}
			_applyBurnWatch.Restart();
		}
		if (_disappearWatch.ElapsedMilliseconds >= DurationMilliseconds)
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
	}
}
