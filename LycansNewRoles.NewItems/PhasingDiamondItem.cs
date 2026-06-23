using System;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using LycansNewRoles.Stats;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(6)]
public class PhasingDiamondItem : CustomItem
{
	private static Changed<PhasingDiamondItem> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<PhasingDiamondItem> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<PhasingDiamondItem> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	public override string PrefabName => "LycansNewRoles.ItemPhasingDiamond";

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
		Rpc_Use_Phasing_Diamond(((SimulationBehaviour)this).Runner, ((Item)this).Owner);
	}

	[Rpc]
	public unsafe static void Rpc_Use_Phasing_Diamond(NetworkRunner runner, PlayerRef playerRef)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
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
					int num = 12;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PhasingDiamondItem::Rpc_Use_Phasing_Diamond(Fusion.NetworkRunner,Fusion.PlayerRef)")), data);
					Unsafe.Write(data + num2, playerRef);
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
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerRef);
			Item item = player.PlayerController.Item;
			if (!(item is PhasingDiamondItem phasingDiamondItem))
			{
				return;
			}
			TickTimer itemTimer = ((Item)phasingDiamondItem).ItemTimer;
			if (!((TickTimer)(ref itemTimer)).IsRunning)
			{
				((Item)phasingDiamondItem).ItemQuantity = ((Item)phasingDiamondItem).ItemQuantity - 1;
				if (((Item)phasingDiamondItem).ItemQuantity > 0)
				{
					((Item)phasingDiamondItem).StartDelayTimer(Traverse.Create((object)phasingDiamondItem).Field<float>("resetDelay").Value);
				}
				else
				{
					((Item)phasingDiamondItem).DestroyItem();
				}
				PlayerCustom.ApplyEffectToPlayer(player.PlayerController, "LycansNewRoles.EffectPhasing", runner);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("DiamondEffect"), ((Component)player).transform.position, 20f, 0.2f);
				if (player.Stats != null)
				{
					player.Stats.AddAction(new PlayerStats.PlayerAction
					{
						ActionType = "UseGadget",
						ActionName = TranslationManager.Instance.GetTranslation("NALES_ITEM_DIAMOND")
					}, ((Component)player.PlayerController).transform.position);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Use_Phasing_Diamond error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PhasingDiamondItem::Rpc_Use_Phasing_Diamond(Fusion.NetworkRunner,Fusion.PlayerRef)")]
	[Preserve]
	protected unsafe static void Rpc_Use_Phasing_Diamond_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		PlayerRef playerRef = (PlayerRef)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Use_Phasing_Diamond(runner, playerRef);
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
