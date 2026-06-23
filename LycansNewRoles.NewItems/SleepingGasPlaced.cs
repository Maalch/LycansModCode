using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(4)]
public class SleepingGasPlaced : NetworkBehaviour
{
	public static GameObject SleepingGasParticleSystemPrefab;

	private Rigidbody _rigidbody;

	private ParticleSystem _particleSystem;

	private bool _detonated = false;

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe TickTimer SleepyTimer
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasPlaced.SleepyTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasPlaced.SleepyTimer. Networked properties can only be accessed when Spawned() has been called.");
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
				throw new InvalidOperationException("Error when accessing SleepingGasPlaced.DisappearTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasPlaced.DisappearTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe int Type
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasPlaced.Type. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[5];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasPlaced.Type. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[5] = value;
		}
	}

	[Networked(OnChanged = "CreatorRefChanged")]
	[NetworkedWeaved(3, 1)]
	public unsafe PlayerRef CreatorRef
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasPlaced.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[3];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasPlaced.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 3, value);
		}
	}

	public bool Detonated => _detonated;

	private float CurrentScalingRatio
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			TickTimer disappearTimer = DisappearTimer;
			if (!((TickTimer)(ref disappearTimer)).IsRunning)
			{
				return 0f;
			}
			float duration = Duration;
			disappearTimer = DisappearTimer;
			float num = Mathf.InverseLerp(0f, 4f, duration - ((TickTimer)(ref disappearTimer)).RemainingTime(((SimulationBehaviour)this).Runner).Value);
			return Mathf.Lerp(0.2f, 1f, num);
		}
	}

	private float Duration => (SleepingGasItem.RemoteGadgetType)Type switch
	{
		SleepingGasItem.RemoteGadgetType.SleepingGas => 20f, 
		SleepingGasItem.RemoteGadgetType.Camouflage => 20f, 
		SleepingGasItem.RemoteGadgetType.StinkingGas => 50f, 
		_ => 0f, 
	};

	private float Radius => (SleepingGasItem.RemoteGadgetType)Type switch
	{
		SleepingGasItem.RemoteGadgetType.SleepingGas => 20f, 
		SleepingGasItem.RemoteGadgetType.Camouflage => 20f, 
		SleepingGasItem.RemoteGadgetType.StinkingGas => 30f, 
		_ => 0f, 
	};

	private void Awake()
	{
		_rigidbody = ((Component)this).GetComponent<Rigidbody>();
		_particleSystem = ((Component)this).GetComponentInChildren<ParticleSystem>();
		_particleSystem.Clear();
		_particleSystem.Stop();
	}

	public void Init(SleepingGasItem.RemoteGadgetType type, PlayerRef creatorRef)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		Type = (int)type;
		CreatorRef = creatorRef;
	}

	public override void Spawned()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Spawned();
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			SleepyTimer = TickTimer.None;
			DisappearTimer = TickTimer.None;
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		if ((int)GameManager.LocalGameState != 0)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(CreatorRef);
			player.PlacedSleepingGas = null;
		}
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<SleepingGasPlaced> changed)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (changed.Behaviour.CreatorRef == PlayerController.Local.Ref)
		{
			Plugin.Minimap.AddSleepingGasIcon(changed.Behaviour);
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
		player.PlacedSleepingGas = changed.Behaviour;
	}

	public static SleepingGasPlaced FindPlayerPlacedSleepingGas(PlayerRef playerRef)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		return Object.FindObjectsOfType<SleepingGasPlaced>().FirstOrDefault((SleepingGasPlaced o) => o.CreatorRef == playerRef && !o._detonated);
	}

	public void Detonate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("SleepingGasBreak"), ((Component)this).transform.position, 30f, 1f);
		SleepyTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 1f);
		DisappearTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, Duration);
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Invalid comparison between Unknown and I4
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Invalid comparison between Unknown and I4
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Invalid comparison between Unknown and I4
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Invalid comparison between Unknown and I4
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Invalid comparison between Unknown and I4
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		TickTimer val = DisappearTimer;
		if (((TickTimer)(ref val)).IsRunning && !_detonated)
		{
			float num = 0.175f * Radius * BalancingValues.SleepingGasRadiusByMap(GameManager.Instance.MapID);
			((Component)_particleSystem).transform.localScale = new Vector3(num, num, num);
			Color val2 = Color.white;
			switch ((SleepingGasItem.RemoteGadgetType)Type)
			{
			case SleepingGasItem.RemoteGadgetType.SleepingGas:
				val2 = BalancingValues.SleepingGasColorForSleepingGas;
				break;
			case SleepingGasItem.RemoteGadgetType.Camouflage:
				val2 = BalancingValues.SleepingGasColorForCamouflage;
				break;
			case SleepingGasItem.RemoteGadgetType.StinkingGas:
				val2 = BalancingValues.SleepingGasColorForPoisonGas;
				break;
			}
			MainModule main = _particleSystem.main;
			((MainModule)(ref main)).startColor = MinMaxGradient.op_Implicit(val2);
			_particleSystem.Play();
			_detonated = true;
		}
		if (_detonated && _particleSystem.isPlaying)
		{
			val = DisappearTimer;
			if (((TickTimer)(ref val)).IsRunning)
			{
				val = DisappearTimer;
				if (((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)this).Runner) <= 4f)
				{
					_particleSystem.Stop();
				}
			}
		}
		if ((int)GameManager.LocalGameState == 5 || (int)GameManager.LocalGameState == 1 || (int)GameManager.LocalGameState == 0)
		{
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
		else
		{
			if (!_detonated)
			{
				return;
			}
			float currentScalingRatio = CurrentScalingRatio;
			ShapeModule shape = _particleSystem.shape;
			((ShapeModule)(ref shape)).scale = new Vector3(currentScalingRatio, currentScalingRatio, currentScalingRatio);
			if (!((SimulationBehaviour)this).HasStateAuthority)
			{
				return;
			}
			if (_detonated && (int)GameManager.LocalGameState != 2 && (int)GameManager.LocalGameState != 3)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				return;
			}
			val = SleepyTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				SleepyTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 1f);
				if (LycansUtility.GameActuallyInPlay)
				{
					IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Phasing) && !NetworkBool.op_Implicit(o.Asleep));
					foreach (PlayerCustom item in enumerable)
					{
						float num2 = Vector3.Distance(((Component)this).transform.position, ((Component)item.PlayerController).transform.position);
						float num3 = Radius * currentScalingRatio * BalancingValues.SleepingGasRadiusByMap(GameManager.Instance.MapID);
						if (!(num2 <= num3))
						{
							continue;
						}
						switch ((SleepingGasItem.RemoteGadgetType)Type)
						{
						case SleepingGasItem.RemoteGadgetType.SleepingGas:
						{
							int num4 = (NetworkBool.op_Implicit(item.PlayerController.IsWolf) ? 150 : 150);
							float num5 = 1f - num2 / num3;
							float num6 = Mathf.Lerp(1.7f, 0.6f, num5);
							num4 = Mathf.RoundToInt((float)num4 * num6);
							if (item.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothEngineer)
							{
								num4 = Mathf.RoundToInt((float)num4 * 0.4f);
							}
							if (item.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
							{
								num4 = Mathf.RoundToInt((float)num4 * 0.5f);
							}
							item.SleepStacks += num4;
							if (item.SleepStacks >= 1000)
							{
								item.SleepStacks = 0;
								PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectAsleep", ((SimulationBehaviour)this).Runner, 1f, NetworkBool.op_Implicit(item.PlayerController.IsWolf) ? 12f : 30f);
							}
							else
							{
								PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectSleepy", ((SimulationBehaviour)this).Runner);
							}
							break;
						}
						case SleepingGasItem.RemoteGadgetType.Camouflage:
							PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectStealthing", ((SimulationBehaviour)this).Runner, 1f, 2f);
							if (NetworkBool.op_Implicit(item.PlayerController.IsWolf))
							{
								PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectUndetected", ((SimulationBehaviour)this).Runner, 1f, 2f);
							}
							break;
						case SleepingGasItem.RemoteGadgetType.StinkingGas:
							val = item.PlayerController.PlayerEffectManager.FartTimer;
							if (!((TickTimer)(ref val)).IsRunning)
							{
								Effect effect = EffectManager.GetEffects().First((Effect o) => o is FlatulenceEffect);
								PlayerCustom.ApplyEffectToPlayer(item.PlayerController, effect, ((SimulationBehaviour)this).Runner, 1f, 5.5f);
							}
							if (NetworkBool.op_Implicit(item.PlayerController.IsWolf))
							{
								PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectNauseated", ((SimulationBehaviour)this).Runner, 1f, 5.5f);
							}
							break;
						}
					}
				}
			}
			val = DisappearTimer;
			if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	[Rpc]
	public unsafe static void Rpc_Take_Sleeping_Gas(NetworkRunner runner, PlayerRef playerRef)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 20;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.SleepingGasPlaced::Rpc_Take_Sleeping_Gas(Fusion.NetworkRunner,Fusion.PlayerRef)")), data);
					Unsafe.Write(data + num2, playerRef);
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (((PlayerRef)(ref playerRef)).IsNone)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerRef);
			PlayerController playerController = player.PlayerController;
			if (runner.IsServer)
			{
				SleepingGasPlaced sleepingGasPlaced = FindPlayerPlacedSleepingGas(playerRef);
				if (!((Object)(object)sleepingGasPlaced == (Object)null))
				{
					Item item = ItemManager.GetItem(typeof(SleepingGasItem));
					Item val = ItemUtility.SpawnItem(item, Vector3.zero, Quaternion.identity, runner);
					(val as SleepingGasItem).Type = sleepingGasPlaced.Type;
					runner.Despawn(((Component)sleepingGasPlaced).GetComponent<NetworkObject>(), false);
					val.Rpc_ClaimItem(playerRef);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Take_Sleeping_Gas error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.SleepingGasPlaced::Rpc_Take_Sleeping_Gas(Fusion.NetworkRunner,Fusion.PlayerRef)")]
	[Preserve]
	protected unsafe static void Rpc_Take_Sleeping_Gas_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		PlayerRef playerRef = (PlayerRef)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Take_Sleeping_Gas(runner, playerRef);
	}
}
