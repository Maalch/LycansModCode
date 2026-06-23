using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(3)]
public class HostParasite : NetworkBehaviour
{
	public static GameObject ParasiteExplosionParticleSystemPrefab;

	private PlayerCustom _creatorCustom;

	private GameObject _visual;

	private Stopwatch _appearStopwatch = new Stopwatch();

	private Shader _initialShader;

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
				throw new InvalidOperationException("Error when accessing HostParasite.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing HostParasite.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "AppearedChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool Appeared
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing HostParasite.Appeared. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing HostParasite.Appeared. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	public void Awake()
	{
		_visual = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		_initialShader = ((Renderer)_visual.GetComponent<MeshRenderer>()).material.shader;
		_visual.SetActive(false);
	}

	public override void Spawned()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		Appeared = NetworkBool.op_Implicit(false);
		UpdateVisibility();
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			_appearStopwatch.Start();
		}
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<HostParasite> changed)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			if (changed.Behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
			{
				Plugin.Minimap.AddHostParasiteIcon(changed.Behaviour);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreatorRefChanged error: " + ex));
		}
	}

	[Preserve]
	public static void AppearedChanged(Changed<HostParasite> changed)
	{
		changed.Behaviour.UpdateVisibility();
	}

	private void UpdateVisibility()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Invalid comparison between Unknown and I4
		if (!NetworkBool.op_Implicit(Appeared))
		{
			_visual.SetActive(false);
			return;
		}
		_visual.SetActive(true);
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Host)
		{
			((Renderer)_visual.GetComponent<MeshRenderer>()).material.shader = PlayerCustom.SeeThroughShaderWolf;
		}
		else if ((int)player.PlayerController.Role == 1 || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor)
		{
			((Renderer)_visual.GetComponent<MeshRenderer>()).material.shader = _initialShader;
		}
		else
		{
			((Renderer)_visual.GetComponent<MeshRenderer>()).material.shader = PlayerCustom.CamouflageLevel2Shader;
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority && (Object)(object)_creatorCustom != (Object)null)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || (int)val == 5)
			{
				flag = true;
			}
			if (!PlayerRegistry.HasPlayer(CreatorRef))
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				return;
			}
		}
		if (!NetworkBool.op_Implicit(Appeared) && ((SimulationBehaviour)this).Runner.IsServer && _appearStopwatch.ElapsedMilliseconds >= 15000)
		{
			Appeared = NetworkBool.op_Implicit(true);
			_appearStopwatch.Stop();
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)((SimulationBehaviour)this).Runner == (Object)null) && NetworkBool.op_Implicit(Appeared) && ((SimulationBehaviour)this).Runner.IsServer)
		{
			PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
			if ((Object)(object)component != (Object)null && !NetworkBool.op_Implicit(component.IsWolf) && !NetworkBool.op_Implicit(component.IsDead))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(component.Ref);
				player.Parasite = NetworkBool.op_Implicit(true);
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	[Rpc]
	public unsafe static void Rpc_Destroy_Parasite(NetworkRunner runner, int playerIndex, NetworkId parasiteId)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
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
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.HostParasite::Rpc_Destroy_Parasite(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					Unsafe.Write(data + num2, parasiteId);
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			HostParasite component = ((Component)runner.FindObject(parasiteId)).GetComponent<HostParasite>();
			((SimulationBehaviour)component).Runner.Despawn(((Component)component).GetComponent<NetworkObject>(), false);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Destroy_Parasite error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.HostParasite::Rpc_Destroy_Parasite(Fusion.NetworkRunner,System.Int32,Fusion.NetworkId)")]
	[Preserve]
	protected unsafe static void Rpc_Destroy_Parasite_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkId parasiteId = (NetworkId)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Destroy_Parasite(runner, playerIndex, parasiteId);
	}
}
