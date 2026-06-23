using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewItems.Accessories;

[NetworkBehaviourWeaved(7)]
public class AccessoryBackpack : Accessory
{
	private static Changed<AccessoryBackpack> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<AccessoryBackpack> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<AccessoryBackpack> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	public override string PrefabName => "LycansNewRoles.AccessoryBackpack";

	public override string DescriptionTranslateKey => "NALES_ACCESSORY_BACKPACK_DESCRIPTION";

	public override string TinkererDescriptionTranslateKey => "NALES_ACCESSORY_BACKPACK_TINKERER";

	public override float CooldownAfterUse => 2f;

	public override int TinkererPowerCooldown => 90;

	public override bool TinkererPowerRequiresPlayerTarget => false;

	[Networked]
	[NetworkedWeaved(5, 2)]
	public unsafe Item ItemInside
	{
		get
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			if (((NetworkBehaviour)this).Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AccessoryBackpack.Item. Networked properties can only be accessed when Spawned() has been called.");
			}
			NetworkBehaviour val = null;
			ReadWriteUtilsForWeaver.VerifyRawNetworkUnwrap<Item>(NetworkBehaviour.NetworkDeserialize(((SimulationBehaviour)this).Runner, (byte*)((NetworkBehaviour)this).Ptr + (nint)5 * (nint)4, ref val), 8);
			return (Item)val;
		}
		set
		{
			if (((NetworkBehaviour)this).Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AccessoryBackpack.Item. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.VerifyRawNetworkWrap<Item>(NetworkBehaviour.NetworkSerialize(((SimulationBehaviour)this).Runner, (NetworkBehaviour)(object)value, (byte*)((NetworkBehaviour)this).Ptr + (nint)5 * (nint)4), 8);
		}
	}

	public override void Spawned()
	{
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		((Item)this).Spawned();
		if (!((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		if (Random.value > 0.4f)
		{
			Potion value = Traverse.Create((object)GameManager.Instance).Field<Potion>("potionPrefab").Value;
			List<Effect> value2 = Traverse.Create((object)GameManager.Instance).Field<List<Effect>>("_potionEffects").Value;
			if (value2.Count == 0)
			{
				return;
			}
			Effect randomEffect = CollectionsUtil.Grab<Effect>(value2.Select((Effect e) => e).ToList(), 1).First();
			Effect item = value2.First((Effect o) => ((object)o).GetType() == ((object)randomEffect).GetType());
			int localEffectIndex = value2.IndexOf(item);
			if ((Object)(object)randomEffect != (Object)null)
			{
				Potion itemInside = ((SimulationBehaviour)this).Runner.Spawn<Potion>(value, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<Potion>().Init(localEffectIndex, EffectManager.GetEffectIndex(randomEffect));
				}, (NetworkObjectPredictionKey?)null, true);
				ItemInside = (Item)(object)itemInside;
			}
			return;
		}
		Item[] value3 = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
		List<Item> list = value3.Where((Item o) => Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)]).ToList();
		if (list.Count != 0)
		{
			Item prefab = CollectionsUtil.Grab<Item>(list.Where((Item o) => o is LockItem || o is TrapItem || o is SmokeItem || o is SpyglassItem || o is MagicScrollItem || o is PhasingDiamondItem || o is GrenadeItem || o is SleepingGasItem || o is MolotovItem || o is RadarItem).ToList(), 1).First();
			Item itemInside2 = ItemUtility.SpawnItem(prefab, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
			ItemInside = itemInside2;
		}
	}

	public void TransferItemToRegularSlotIfNeeded()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		_ = ((Item)this).Owner;
		if (0 == 0)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)this).Owner);
			if ((Object)(object)player.PlayerController.Item == (Object)null && (Object)(object)ItemInside != (Object)null)
			{
				((NetworkBehaviour)ItemInside).CopyStateToBackingFields();
				Traverse.Create((object)ItemInside).Field("_Owner").SetValue((object)((Item)this).Owner);
				((NetworkBehaviour)ItemInside).CopyBackingFieldsToState(true);
				((SimulationBehaviour)ItemInside).Object.AssignInputAuthority(ItemInside.Owner);
				player.PlayerController.Item = ItemInside;
				ItemInside = null;
				player.PlayerController.Item.StartDelayTimer(1f);
			}
		}
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		_ = ((Item)this).Owner;
		TickTimer itemTimer = ((Item)this).ItemTimer;
		if (!((TickTimer)(ref itemTimer)).IsRunning)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)this).Owner);
			Rpc_Switch_Backpack_Items(((SimulationBehaviour)this).Runner, ((Item)this).Owner);
		}
	}

	[Rpc]
	public unsafe static void Rpc_Switch_Backpack_Items(NetworkRunner runner, PlayerRef playerRef)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
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
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.AccessoryBackpack::Rpc_Switch_Backpack_Items(Fusion.NetworkRunner,Fusion.PlayerRef)")), data);
					Unsafe.Write(data + num2, playerRef);
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			if (runner.IsServer)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerRef);
				if (player.Accessory is AccessoryBackpack accessoryBackpack && (Object)(object)player.PlayerController.Item != (Object)null && (Object)(object)accessoryBackpack.ItemInside != (Object)null)
				{
					Item itemInside = accessoryBackpack.ItemInside;
					accessoryBackpack.ItemInside = player.PlayerController.Item;
					player.PlayerController.Item = itemInside;
					((Item)accessoryBackpack).StartDelayTimer(accessoryBackpack.CooldownAfterUse);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Switch_Backpack_Items error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.AccessoryBackpack::Rpc_Switch_Backpack_Items(Fusion.NetworkRunner,Fusion.PlayerRef)")]
	[Preserve]
	protected unsafe static void Rpc_Switch_Backpack_Items_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		PlayerRef playerRef = (PlayerRef)data[num];
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Switch_Backpack_Items(runner, playerRef);
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
