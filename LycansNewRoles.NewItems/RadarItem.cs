using System;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using LycansNewRoles.Stats;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(6)]
public class RadarItem : CustomItem
{
	private static Changed<RadarItem> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<RadarItem> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<RadarItem> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	public override string PrefabName => "LycansNewRoles.ItemRadar";

	protected override void ItemCollected()
	{
	}

	protected override bool CanUseItem()
	{
		return true;
	}

	protected override void ItemTriggered()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)this).Owner);
		Rpc_Use_Radar(((SimulationBehaviour)this).Runner, ((Item)this).Owner, ((Component)player.PlayerController.LocalCameraHandler.LocalCamera).transform.forward.y);
	}

	[Rpc]
	public unsafe static void Rpc_Use_Radar(NetworkRunner runner, PlayerRef playerRef, float angleY)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
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
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.RadarItem::Rpc_Use_Radar(Fusion.NetworkRunner,Fusion.PlayerRef,System.Single)")), data);
					Unsafe.Write(data + num2, playerRef);
					num2 += 4;
					*(float*)(data + num2) = angleY;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerRef);
			if (!runner.IsServer)
			{
				return;
			}
			Item item = player.PlayerController.Item;
			if (!(item is RadarItem radarItem))
			{
				return;
			}
			TickTimer itemTimer = ((Item)radarItem).ItemTimer;
			if (!((TickTimer)(ref itemTimer)).IsRunning)
			{
				((Item)radarItem).ItemQuantity = ((Item)radarItem).ItemQuantity - 1;
				if (((Item)radarItem).ItemQuantity > 0)
				{
					((Item)radarItem).StartDelayTimer(Traverse.Create((object)radarItem).Field<float>("resetDelay").Value);
				}
				else
				{
					((Item)radarItem).DestroyItem();
				}
				NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemRadarActive");
				Vector3 value = default(Vector3);
				((Vector3)(ref value))._002Ector(((Component)player).transform.position.x + ((Component)player).transform.forward.x / 2f, ((Component)player).transform.position.y + 1f, ((Component)player).transform.position.z + ((Component)player).transform.forward.z / 2f);
				NetworkObject val = runner.Spawn(networkObject, (Vector3?)value, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				Vector3 forward = ((Component)player.PlayerController.LocalCameraHandler.LocalCamera).transform.forward;
				forward.y = Mathf.Min(1f, angleY + 0.1f);
				((Component)val).GetComponent<RadarActive>().Init(forward * 25f, !NetworkBool.op_Implicit(player.PlayerController.IsWolf));
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("GrenadeThrow"), ((Component)player.PlayerController).transform.position, 15f, 0.8f);
				player.PlayerController.UpdateAnimation(Animator.StringToHash("Attacking"), true);
				((MonoBehaviour)player.PlayerController).StartCoroutine("WaitAndResetAttackAnimation");
				if (player.Stats != null)
				{
					player.Stats.AddAction(new PlayerStats.PlayerAction
					{
						ActionType = "UseGadget",
						ActionName = TranslationManager.Instance.GetTranslation("NALES_ITEM_RADAR")
					}, ((Component)player.PlayerController).transform.position);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Use_Radar error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.RadarItem::Rpc_Use_Radar(Fusion.NetworkRunner,Fusion.PlayerRef,System.Single)")]
	[Preserve]
	protected unsafe static void Rpc_Use_Radar_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		PlayerRef playerRef = (PlayerRef)data[num];
		num += 4;
		float angleY = *(float*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Use_Radar(runner, playerRef, angleY);
	}

	protected override void ItemCancelled()
	{
	}

	protected override void AnimationStarted()
	{
	}

	protected override void AnimationCancelled()
	{
	}

	protected override void AnimationEnded()
	{
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
