using System;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.Stats;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Item), "Rpc_ClaimItem")]
internal class ClaimItemPatch
{
	private unsafe static bool Prefix(PlayerRef claimer, Item __instance)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!((NetworkBehaviour)__instance).InvokeRpc)
			{
				NetworkBehaviourUtils.ThrowIfBehaviourNotInitialized((NetworkBehaviour)(object)__instance);
				if ((int)((SimulationBehaviour)__instance).Runner.Stage != 4)
				{
					int localAuthorityMask = ((SimulationBehaviour)__instance).Object.GetLocalAuthorityMask();
					if ((localAuthorityMask & 7) != 0)
					{
						if ((localAuthorityMask & 1) != 1)
						{
							if (((SimulationBehaviour)__instance).Runner.HasAnyActiveConnections())
							{
								int num = 8;
								num += 4;
								SimulationMessage* ptr = SimulationMessage.Allocate(((SimulationBehaviour)__instance).Runner.Simulation, num);
								byte* data = SimulationMessage.GetData(ptr);
								int num2 = RpcHeader.Write(RpcHeader.Create(((SimulationBehaviour)__instance).Object.Id, ((NetworkBehaviour)__instance).ObjectIndex, 1), data);
								Unsafe.Write(data + num2, claimer);
								num2 += 4;
								((SimulationMessage)ptr).Offset = num2 * 8;
								((SimulationBehaviour)__instance).Runner.SendRpc(ptr);
							}
							if ((localAuthorityMask & 1) == 0)
							{
								return false;
							}
						}
						goto IL_0125;
					}
					NetworkBehaviourUtils.NotifyLocalSimulationNotAllowedToSendRpc("System.Void Item::Rpc_ClaimItem(Fusion.PlayerRef)", ((SimulationBehaviour)__instance).Object, 7);
				}
				return false;
			}
			((NetworkBehaviour)__instance).InvokeRpc = false;
			goto IL_0125;
			IL_0125:
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(claimer);
			if (__instance is Accessory accessory)
			{
				if (__instance.Owner == PlayerRef.None && __instance.Owner != claimer)
				{
					((NetworkBehaviour)__instance).CopyStateToBackingFields();
					Traverse.Create((object)__instance).Field("_Owner").SetValue((object)claimer);
					((NetworkBehaviour)__instance).CopyBackingFieldsToState(true);
					((SimulationBehaviour)__instance).Object.AssignInputAuthority(__instance.Owner);
					PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(__instance.Owner);
					if ((Object)(object)player2 != (Object)null)
					{
						Accessory accessory2 = player2.Accessory;
						if ((Object)(object)accessory2 != (Object)null)
						{
							((NetworkBehaviour)accessory2).CopyStateToBackingFields();
							Traverse.Create((object)accessory2).Field("_Owner").SetValue((object)PlayerRef.None);
							((NetworkBehaviour)accessory2).CopyBackingFieldsToState(true);
							((Component)accessory2).transform.position = ((Component)__instance).transform.position;
							((Component)accessory2).transform.rotation = ((Component)__instance).transform.rotation;
						}
						player2.Accessory = accessory;
						if (player2.Stats != null)
						{
							LycansUtility.AddLogOnlyForMe("Stats: TakeAccessory, timing: " + GameStats.GetCurrentTiming());
							player2.Stats.AddAction(new PlayerStats.PlayerAction
							{
								ActionType = "TakeAccessory",
								ActionName = TranslationManager.Instance.GetTranslation(ItemUtility.ItemToTranslateKey((Item)(object)accessory))
							}, ((Component)player2.PlayerController).transform.position);
						}
					}
				}
				return false;
			}
			if ((Object)(object)player.PlayerController.Item != (Object)null && player.Accessory is AccessoryBackpack accessoryBackpack && (Object)(object)accessoryBackpack.ItemInside == (Object)null)
			{
				accessoryBackpack.ItemInside = __instance;
				((NetworkBehaviour)__instance).CopyStateToBackingFields();
				Traverse.Create((object)__instance).Field("_Owner").SetValue((object)claimer);
				((NetworkBehaviour)__instance).CopyBackingFieldsToState(true);
				((SimulationBehaviour)__instance).Object.AssignInputAuthority(__instance.Owner);
				return false;
			}
			if (!NetworkBool.op_Implicit(Plugin.CustomConfig.DropItemsAvailable) || !((SimulationBehaviour)__instance).Object.HasStateAuthority)
			{
				return true;
			}
			if ((Object)(object)player.PlayerController.Item != (Object)null && player.PlayerController.Item is BulletItem)
			{
				player.PlayerController.Item.DestroyItem();
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ClaimItemPatch error: " + ex));
			return true;
		}
	}
}
