using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(20)]
public class BeastManager : NetworkBehaviour
{
	private float DelayToNextHeartbeat;

	private const float DelaySoundSlow = 2.1f;

	private const float DelaySoundMid = 1.42f;

	private const float DelaySoundFast = 0.9f;

	[Networked(OnChanged = "BeastActiveChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe NetworkBool BeastActive
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BeastActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BeastActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "BeastKillsChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe int BeastKills
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BeastKills. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BeastKills. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe int BeastKillsObjective
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BeastKillsObjective. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[2];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.BeastKillsObjective. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[2] = value;
		}
	}

	public static BeastManager Instance { get; private set; }

	public void Reset()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		BeastActive = NetworkBool.op_Implicit(false);
		BeastKills = 0;
		BeastKillsObjective = 0;
	}

	public override void Spawned()
	{
		((NetworkBehaviour)this).Spawned();
		Instance = this;
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Instance = null;
	}

	private void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		if (!NetworkBool.op_Implicit(BeastActive))
		{
			return;
		}
		if (DelayToNextHeartbeat <= 0f)
		{
			float num = BalancingValues.BeastHeartbeatMaximumRange(GameManager.Instance.MapID);
			float num2 = num * 0.7f;
			float num3 = num * 0.4f;
			PlayerCustom povPlayerCustom = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast)
			{
				List<PlayerController> list = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => o.Ref != povPlayerCustom.Ref && !NetworkBool.op_Implicit(o.IsDead))).ToList();
				PlayerController val = null;
				float? num4 = null;
				foreach (PlayerController item in list)
				{
					float num5 = Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)item).transform.position);
					if (!num4.HasValue || num5 < num4.Value)
					{
						val = item;
						num4 = num5;
					}
				}
				if ((Object)(object)val != (Object)null && num4.Value <= num)
				{
					string text;
					float delayToNextHeartbeat;
					if (num4.Value > num2)
					{
						text = "BeastHeartBeatSlow";
						delayToNextHeartbeat = 2f;
					}
					else if (num4.Value > num3)
					{
						text = "BeastHeartBeatMid";
						delayToNextHeartbeat = 1.337f;
					}
					else
					{
						text = "BeastHeartBeatFast";
						delayToNextHeartbeat = 0.841f;
					}
					AudioManager.PlayAndFollow(text, ((Component)val).transform, (MixerTarget)2, num, 0.8f);
					DelayToNextHeartbeat = delayToNextHeartbeat;
				}
				else
				{
					DelayToNextHeartbeat = 0.5f;
				}
				return;
			}
			PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Beast);
			float num6 = Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)specificNewPrimaryRole.PlayerController).transform.position);
			if (num6 <= num)
			{
				string text2;
				float delayToNextHeartbeat2;
				if (num6 > num2)
				{
					text2 = "BeastHeartBeatSlow";
					delayToNextHeartbeat2 = 2f;
				}
				else if (num6 > num3)
				{
					text2 = "BeastHeartBeatMid";
					delayToNextHeartbeat2 = 1.337f;
				}
				else
				{
					text2 = "BeastHeartBeatFast";
					delayToNextHeartbeat2 = 0.841f;
				}
				AudioManager.Play(text2, (MixerTarget)2, 0.65f, 1f);
				DelayToNextHeartbeat = delayToNextHeartbeat2;
			}
			else
			{
				DelayToNextHeartbeat = 0.5f;
			}
		}
		else
		{
			DelayToNextHeartbeat -= Time.deltaTime;
		}
	}

	public void ActivateBeast()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		BeastActive = NetworkBool.op_Implicit(true);
		foreach (PlayerController item in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead))))
		{
			item.IsWolf = NetworkBool.op_Implicit(false);
			item.Hunger = GameManager.Instance.MaxHunger;
		}
	}

	[Preserve]
	public static void BeastActiveChanged(Changed<BeastManager> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			BeastManager behaviour = changed.Behaviour;
			if (NetworkBool.op_Implicit(behaviour.BeastActive))
			{
				int num = BalancingValues.BeastTargetAmount(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) - 1);
				PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Beast);
				if (((SimulationBehaviour)behaviour).Runner.IsServer)
				{
					behaviour.BeastKillsObjective = num;
					specificNewPrimaryRole.PlayerController.Hunger = GameManager.Instance.MaxHunger;
					Traverse.Create((object)specificNewPrimaryRole.PlayerController).Property("WolfDelay", (object[])null).SetValue((object)TickTimer.CreateFromSeconds(((SimulationBehaviour)behaviour).Runner, 7f));
					specificNewPrimaryRole.PlayerController.TransformedNight = NetworkBool.op_Implicit(true);
					specificNewPrimaryRole.PlayerController.IsWolf = NetworkBool.op_Implicit(true);
					specificNewPrimaryRole.PlayerController.MovementAction = 0;
					specificNewPrimaryRole.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
					specificNewPrimaryRole.PlayerController.IsZooming = NetworkBool.op_Implicit(false);
					GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)behaviour).Runner, NetworkString<_16>.op_Implicit("BeastAwakening"), ((Component)specificNewPrimaryRole.PlayerController).transform.position, 50f, 0.8f);
				}
				Traverse.Create((object)specificNewPrimaryRole.PlayerController).Field<ParticleSystem>("smokeParticleSystem").Value.Play();
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast)
				{
					UIManager.ShowRedCenterMessage("NALES_UI_BEAST_UNLEASHED_BEAST", 0.5f, 5f, new List<object> { num });
				}
				else
				{
					UIManager.ShowRedCenterMessage("NALES_UI_BEAST_UNLEASHED_OTHERS", 0.5f, 4f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ColorIndexChanged error: " + ex));
		}
	}

	[Preserve]
	public static void BeastKillsChanged(Changed<BeastManager> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			BeastManager behaviour = changed.Behaviour;
			if (NetworkBool.op_Implicit(behaviour.BeastActive) && ((SimulationBehaviour)behaviour).Runner.IsServer && behaviour.BeastKills >= behaviour.BeastKillsObjective)
			{
				PlayerCustom.Rpc_End_Game(((SimulationBehaviour)behaviour).Runner, PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Beast).Index);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ColorIndexChanged error: " + ex));
		}
	}
}
