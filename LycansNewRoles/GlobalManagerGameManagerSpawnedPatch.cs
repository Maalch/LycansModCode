using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Spawned")]
internal class GlobalManagerGameManagerSpawnedPatch
{
	private static void Postfix(GameManager __instance)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!((SimulationBehaviour)__instance).Runner.SessionInfo.IsOpen)
			{
				return;
			}
			LanternCustom.LanternCustomsByLight.Clear();
			if (!((SimulationBehaviour)__instance).Runner.IsServer)
			{
				return;
			}
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GlobalManager");
			Plugin.NetworkObject = ((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)null, (Quaternion?)null, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.PlayerCustomRegistry");
			((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)null, (Quaternion?)null, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.SabotageManager");
			((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)null, (Quaternion?)null, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.BeastManager");
			((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)null, (Quaternion?)null, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.CultistManager");
			((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)null, (Quaternion?)null, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.DraftManager");
			((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)null, (Quaternion?)null, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameManagerCustom");
			((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)null, (Quaternion?)null, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			Lantern[] array = Object.FindObjectsOfType<Lantern>();
			List<Vector3> list = new List<Vector3>();
			Lantern[] array2 = array;
			foreach (Lantern val in array2)
			{
				if (!list.Contains(((Component)val).transform.position))
				{
					((Component)val).gameObject.AddComponent<LanternCustom>();
					list.Add(((Component)val).transform.position);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Error: " + ex));
		}
	}
}
