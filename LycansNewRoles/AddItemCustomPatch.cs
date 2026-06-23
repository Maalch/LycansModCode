using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems.Accessories;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Item), "Spawned")]
internal class AddItemCustomPatch
{
	private static void Postfix(Item __instance)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		try
		{
			if (((SimulationBehaviour)__instance).HasStateAuthority && !(__instance is Accessory))
			{
				NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemCustom");
				NetworkObject val = ((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)new Vector3(((Component)__instance).transform.position.x, ((Component)__instance).transform.position.y, ((Component)__instance).transform.position.z), (Quaternion?)null, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					//IL_0012: Unknown result type (might be due to invalid IL or missing references)
					((Component)no).GetComponent<ItemCustom>().ItemNetworkId = ((SimulationBehaviour)__instance).Object.Id;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				((Component)val).transform.parent = ((Component)__instance).transform;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("AddItemCustomPatch error: " + ex));
		}
	}
}
