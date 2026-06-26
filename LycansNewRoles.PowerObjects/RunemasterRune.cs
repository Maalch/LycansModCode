using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(3)]
public class RunemasterRune : NetworkBehaviour
{
	public static GameObject ActivationParticleSystemPrefab;

	private PlayerCustom _creatorCustom;

	private GameObject _effect;

	private GameObject _seethrough;

	private Stopwatch _nextCheckWatch = new Stopwatch();

	private Stopwatch _explosionWatch = new Stopwatch();

	public static List<RunemasterRune> AssociatedRunes = new List<RunemasterRune>();

	private int _runeIndex;

	private bool _isSelected = false;

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
				throw new InvalidOperationException("Error when accessing RunemasterRune.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing RunemasterRune.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "TriggeredChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool Triggered
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing RunemasterRune.Triggered. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing RunemasterRune.Triggered. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	public bool IsSelected => _isSelected;

	public void Awake()
	{
		_effect = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		_seethrough = ((Component)((Component)this).transform.Find("Icon")).gameObject;
		_explosionWatch.Stop();
	}

	private void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		if (!NetworkBool.op_Implicit(Triggered) || !((float)_explosionWatch.ElapsedMilliseconds >= 2500f))
		{
			return;
		}
		_explosionWatch.Reset();
		int num = Mathf.Max(0, PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead) && (int)o.Role == 1)) - 1);
		float num2 = 1f + (float)num * 0.5f;
		float num3 = 1f + (float)num * 0.25f;
		float num4 = (float)(1 + num) + 0.25f;
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (_creatorCustom.IsCurrentlyPlayedOrObserved || NetworkBool.op_Implicit(player.PlayerController.IsWolf))
		{
			GameObject val = Object.Instantiate<GameObject>(ActivationParticleSystemPrefab, new Vector3(((Component)this).transform.position.x, ((Component)this).transform.position.y + 0.5f, ((Component)this).transform.position.z), Quaternion.identity);
			val.SetActive(true);
			float num5 = 4.5f * num2;
			val.transform.localScale = new Vector3(num5, num5, num5);
			SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
			selfDestroyingObjectComponent.Init(2.5f);
			AudioManager.PlayPosition("RuneExplosion", ((Component)this).transform.position, (MixerTarget)2, 500f, 1f);
		}
		float num6 = 12f * num2;
		foreach (PlayerController item in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead))))
		{
			float num7 = Vector3.Distance(((Component)item).transform.position, ((Component)this).transform.position);
			if (!(num7 > num6))
			{
				float num8 = Mathf.InverseLerp(num6, 0f, num7);
				float num9 = Mathf.Lerp(0.35f, 1f, num8);
				if (num9 >= 0.5f)
				{
					((Component)item).GetComponent<ForcedRotationComponent>().Init(new Vector3(0f, 1f, 0f), 3000f * num3 * num9, 2000f);
				}
				float num10 = 12f * num4 * num8;
				num10 = Mathf.Min(num10, 10f);
				PlayerCustom.ApplyEffectToPlayer(item, "LycansNewRoles.EffectConfused", ((SimulationBehaviour)this).Runner, 1f, num10);
				PlayerCustom.ApplyEffectToPlayer(item, "LycansNewRoles.EffectResilience", ((SimulationBehaviour)this).Runner, 1f, num10);
			}
		}
		((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	public void SetSelected(bool selected)
	{
		_isSelected = selected;
		UpdateVisibility();
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<RunemasterRune> changed)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)changed.Behaviour).Runner.IsServer)
			{
				List<RunemasterRune> list = (from o in Object.FindObjectsOfType<RunemasterRune>()
					where o.CreatorRef == changed.Behaviour.CreatorRef
					select o).ToList();
				changed.Behaviour._runeIndex = ((!list.Any()) ? 1 : (list.Max((RunemasterRune o) => o._runeIndex) + 1));
				if (list.Count > 8)
				{
					int minRuneIndex = list.Min((RunemasterRune o) => o._runeIndex);
					RunemasterRune runemasterRune = list.First((RunemasterRune o) => o._runeIndex == minRuneIndex);
					((SimulationBehaviour)changed.Behaviour).Runner.Despawn(((Component)runemasterRune).GetComponent<NetworkObject>(), false);
				}
			}
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			if (changed.Behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
			{
				Plugin.Minimap.AddRunemasterRuneIcon(changed.Behaviour);
				if (!AssociatedRunes.Any())
				{
					changed.Behaviour.SetSelected(selected: true);
				}
				AssociatedRunes.Add(changed.Behaviour);
				changed.Behaviour._creatorCustom.UpdateDescriptionStatusIfNeeded();
			}
			changed.Behaviour._nextCheckWatch.Restart();
			changed.Behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreatorRefChanged error: " + ex));
		}
	}

	[Preserve]
	public static void TriggeredChanged(Changed<RunemasterRune> changed)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		changed.Behaviour.UpdateVisibility();
		if (!NetworkBool.op_Implicit(changed.Behaviour.Triggered))
		{
			return;
		}
		ParticleSystem[] componentsInChildren = changed.Behaviour._effect.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem val in componentsInChildren)
		{
			RotationOverLifetimeModule rotationOverLifetime = val.rotationOverLifetime;
			if (((RotationOverLifetimeModule)(ref rotationOverLifetime)).enabled)
			{
				RotationOverLifetimeModule rotationOverLifetime2 = val.rotationOverLifetime;
				((RotationOverLifetimeModule)(ref rotationOverLifetime2)).x = MinMaxCurve.op_Implicit(100f);
				((RotationOverLifetimeModule)(ref rotationOverLifetime2)).y = MinMaxCurve.op_Implicit(100f);
				((RotationOverLifetimeModule)(ref rotationOverLifetime2)).z = MinMaxCurve.op_Implicit(100f);
			}
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (changed.Behaviour._creatorCustom.IsCurrentlyPlayedOrObserved || NetworkBool.op_Implicit(player.PlayerController.IsWolf))
		{
			AudioManager.PlayPosition("RuneTrigger", ((Component)changed.Behaviour).transform.position, (MixerTarget)2, 15f, 0.8f);
		}
		changed.Behaviour._explosionWatch.Restart();
	}

	private void UpdateVisibility()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		_effect.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved || (NetworkBool.op_Implicit(Triggered) && NetworkBool.op_Implicit(player.PlayerController.IsWolf)));
		_seethrough.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved);
		if (_seethrough.activeSelf)
		{
			((Renderer)_seethrough.GetComponent<MeshRenderer>()).material.shader = (_isSelected ? PlayerCustom.SeeThroughShaderWolf : PlayerCustom.SeeThroughShaderHuman);
		}
	}

	public static void UpdateVisibilityForAllRunes()
	{
		RunemasterRune[] array = Object.FindObjectsOfType<RunemasterRune>();
		RunemasterRune[] array2 = array;
		foreach (RunemasterRune runemasterRune in array2)
		{
			runemasterRune.UpdateVisibility();
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
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
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
			if (!PlayerRegistry.HasPlayer(CreatorRef) || NetworkBool.op_Implicit(PlayerRegistry.GetPlayer(CreatorRef).IsDead))
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
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		AssociatedRunes.Remove(this);
		if ((Object)(object)_creatorCustom != (Object)null && _creatorCustom.Ref == PlayerController.Local.Ref)
		{
			PlayerCustom.Local.UpdateDescriptionStatusIfNeeded();
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	[Rpc]
	public unsafe static void Rpc_Request_Activate_Rune(NetworkRunner runner, int playerIndex, NetworkId runeId)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
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
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.RunemasterRune::Rpc_Request_Activate_Rune(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					Unsafe.Write(data + num2, runeId);
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (!runner.IsServer)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			if (player.PrimaryRolePowerRemainingUses != 0)
			{
				RunemasterRune component = ((Component)runner.FindObject(runeId)).GetComponent<RunemasterRune>();
				if ((Object)(object)component != (Object)null)
				{
					Rpc_Activate_Rune(runner, playerIndex, ((SimulationBehaviour)component).Object.Id);
					player.PrimaryRolePowerRemainingUses--;
					player.ReduceMaterialAfterPowerUse();
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Request_Activate_Rune error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.RunemasterRune::Rpc_Request_Activate_Rune(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")]
	[Preserve]
	protected unsafe static void Rpc_Request_Activate_Rune_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkId runeId = (NetworkId)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Request_Activate_Rune(runner, playerIndex, runeId);
	}

	[Rpc]
	public unsafe static void Rpc_Activate_Rune(NetworkRunner runner, int playerIndex, NetworkId runeId)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
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
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.RunemasterRune::Rpc_Activate_Rune(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					Unsafe.Write(data + num2, runeId);
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (runner.IsServer)
			{
				RunemasterRune component = ((Component)runner.FindObject(runeId)).GetComponent<RunemasterRune>();
				component.Triggered = NetworkBool.op_Implicit(true);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Activate_Rune error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.RunemasterRune::Rpc_Activate_Rune(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")]
	[Preserve]
	protected unsafe static void Rpc_Activate_Rune_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkId runeId = (NetworkId)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Activate_Rune(runner, playerIndex, runeId);
	}
}
