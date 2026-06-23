using System;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(6)]
public class SleepingGasItem : CustomItem
{
	public enum RemoteGadgetType
	{
		SleepingGas,
		Camouflage,
		StinkingGas
	}

	private static Changed<SleepingGasItem> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<SleepingGasItem> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<SleepingGasItem> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	[Networked]
	[NetworkedWeaved(5, 1)]
	public unsafe int Type
	{
		get
		{
			if (((NetworkBehaviour)this).Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasItem.Type. Networked properties can only be accessed when Spawned() has been called.");
			}
			return ((NetworkBehaviour)this).Ptr[5];
		}
		set
		{
			if (((NetworkBehaviour)this).Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SleepingGasItem.Type. Networked properties can only be accessed when Spawned() has been called.");
			}
			((NetworkBehaviour)this).Ptr[5] = value;
		}
	}

	public override string PrefabName => "LycansNewRoles.ItemSleepingGas";

	public override void Spawned()
	{
		((Item)this).Spawned();
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			RandomizeEffect();
		}
	}

	public void RandomizeEffect()
	{
		Type = Random.Range(0, 3);
	}

	protected override void ItemCollected()
	{
	}

	protected override bool CanUseItem()
	{
		return true;
	}

	protected override void ItemTriggered()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		Rpc_Use_Sleeping_Gas_Item(((SimulationBehaviour)this).Runner, ((Item)this).Owner);
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		((Item)this).FixedUpdateNetwork();
		TickTimer val;
		if (((SimulationBehaviour)this).HasInputAuthority)
		{
			val = ((Item)this).AnimationTimer;
			if (((TickTimer)(ref val)).IsRunning && !UIManager.Timer.IsActive)
			{
				UIManager.UpdateTimer(((Item)this).AnimationTimer, ((SimulationBehaviour)this).Runner, "NALES_UI_ACTION_PLACE_SLEEPING_GAS");
			}
		}
		val = ((Item)this).AnimationTimer;
		if (((TickTimer)(ref val)).IsRunning)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)this).Owner);
			if ((Object)(object)player != (Object)null && player.PlayerAnimations.CurrentLoopAnimation != "HumanM@Gathering02")
			{
				player.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: true);
			}
		}
		if (((Item)this).ItemQuantity != 0)
		{
			val = ((Item)this).AnimationTimer;
			if (!((TickTimer)(ref val)).IsRunning)
			{
				val = ((Item)this).ItemTimer;
				if (!((TickTimer)(ref val)).IsRunning)
				{
					val = ((Item)this).TriggerTimer;
					if (!((TickTimer)(ref val)).IsRunning)
					{
						return;
					}
				}
			}
		}
		MeshRenderer[] value = Traverse.Create((object)this).Field<MeshRenderer[]>("meshRenderers").Value;
		MeshRenderer[] array = value;
		foreach (MeshRenderer val2 in array)
		{
			((Renderer)val2).enabled = false;
		}
	}

	[Rpc]
	public unsafe static void Rpc_Use_Sleeping_Gas_Item(NetworkRunner runner, PlayerRef playerRef)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
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
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.SleepingGasItem::Rpc_Use_Sleeping_Gas_Item(Fusion.NetworkRunner,Fusion.PlayerRef)")), data);
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
			if (playerController.Item is SleepingGasItem)
			{
				playerController.CanMoveAnimation = NetworkBool.op_Implicit(true);
				player.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
				if (runner.IsServer)
				{
					SleepingGasItem sleepingGasItem = playerController.Item as SleepingGasItem;
					NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemSleepingGasPlaced");
					Vector3 value = default(Vector3);
					((Vector3)(ref value))._002Ector(((Component)playerController).transform.position.x + ((Component)playerController).transform.forward.x / 2f, ((Component)playerController).transform.position.y, ((Component)playerController).transform.position.z + ((Component)playerController).transform.forward.z / 2f);
					NetworkObject val = runner.Spawn(networkObject, (Vector3?)value, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
					((Component)val).GetComponent<SleepingGasPlaced>().Init((RemoteGadgetType)sleepingGasItem.Type, player.Ref);
					((Item)sleepingGasItem).DestroyItem();
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Use_Sleeping_Gas_Item error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.SleepingGasItem::Rpc_Use_Sleeping_Gas_Item(Fusion.NetworkRunner,Fusion.PlayerRef)")]
	[Preserve]
	protected unsafe static void Rpc_Use_Sleeping_Gas_Item_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		PlayerRef playerRef = (PlayerRef)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Use_Sleeping_Gas_Item(runner, playerRef);
	}

	protected override void ItemCancelled()
	{
	}

	protected override void AnimationStarted()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)this).Owner);
		if ((Object)(object)player != (Object)null)
		{
			player.PlayerController.MovementAction = 0;
			player.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(false);
			player.PlayerController.IsAiming = NetworkBool.op_Implicit(false);
		}
	}

	protected override void AnimationCancelled()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (!(((Item)this).Owner == PlayerRef.None))
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)this).Owner);
			if ((Object)(object)player != (Object)null)
			{
				player.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(true);
				player.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
			}
		}
	}

	protected override void AnimationEnded()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (!(((Item)this).Owner == PlayerRef.None))
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)this).Owner);
			if ((Object)(object)player != (Object)null)
			{
				player.PlayerController.CanMoveAnimation = NetworkBool.op_Implicit(true);
				player.PlayerAnimations.SetLoopAnimation("HumanM@Gathering02", active: false);
			}
		}
	}

	public override void CopyBackingFieldsToState(bool A_1)
	{
		((Item)this).CopyBackingFieldsToState(A_1);
	}

	public override void CopyStateToBackingFields()
	{
		((Item)this).CopyStateToBackingFields();
	}
}
