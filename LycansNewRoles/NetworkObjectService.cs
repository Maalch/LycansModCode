using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

internal class NetworkObjectService
{
	private static NetworkObjectService? _instance;

	private Dictionary<string, NetworkPrefabId> _prefabIds;

	private Dictionary<NetworkPrefabId, GameObject> _prefabsByNetworkPrefabId;

	public static NetworkObjectService Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new NetworkObjectService();
			}
			return _instance;
		}
	}

	public GameObject GetPrefabByPrefabId(NetworkPrefabId prefabId)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return _prefabsByNetworkPrefabId[prefabId];
	}

	private NetworkObjectService()
	{
		_prefabIds = new Dictionary<string, NetworkPrefabId>();
		_prefabsByNetworkPrefabId = new Dictionary<NetworkPrefabId, GameObject>();
	}

	public NetworkPrefabId RegisterNetworkObject(GameObject prefab, string uniqueKey)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		if (_prefabIds.ContainsKey(uniqueKey))
		{
			throw new InvalidOperationException("Key '" + uniqueKey + "' already exists");
		}
		NetworkObject component = prefab.GetComponent<NetworkObject>();
		if ((Object)(object)component == (Object)null)
		{
			throw new MissingComponentException("Prefab is missing 'NetworkObject' component!");
		}
		component.NetworkedBehaviours = prefab.GetComponents<NetworkBehaviour>();
		component.NetworkGuid = new NetworkObjectGuid(uniqueKey.ToGuid().ToString());
		NetworkPrefabSourceUnityStatic val = ScriptableObject.CreateInstance<NetworkPrefabSourceUnityStatic>();
		val.PrefabReference = ((Component)component).gameObject;
		Traverse.Create((object)NetworkProjectConfig.Global.PrefabTable).Field<Dictionary<NetworkObjectGuid, NetworkPrefabId>>("_guidToId").Value.Remove(component.NetworkGuid);
		NetworkPrefabId val2 = default(NetworkPrefabId);
		if (!NetworkProjectConfig.Global.PrefabTable.TryAdd(component.NetworkGuid, (INetworkPrefabSource)(object)val, ref val2))
		{
			throw new ApplicationException("Could not add prefab to registered prefabs, was it already present?");
		}
		_prefabIds.Add(uniqueKey, val2);
		_prefabsByNetworkPrefabId[val2] = ((Component)component).gameObject;
		return val2;
	}

	public NetworkPrefabId GetNetworkObject(string uniqueKey)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (_prefabIds.TryGetValue(uniqueKey, out var value))
		{
			return value;
		}
		throw new KeyNotFoundException("No prefab registered with '" + uniqueKey + "' as identifier!");
	}

	public void UnregisterNetworkObject(string uniqueKey)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			NetworkPrefabId networkObject = GetNetworkObject(uniqueKey);
			_prefabsByNetworkPrefabId.Remove(_prefabIds[uniqueKey]);
			NetworkProjectConfig.Global.PrefabTable.Unload(networkObject);
			_prefabIds.Remove(uniqueKey);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogInfo((object)("UnregisterNetworkObject: " + uniqueKey + " does not exist, or error: " + ex));
		}
	}

	public void Clear()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<string, NetworkPrefabId> prefabId in _prefabIds)
		{
			NetworkProjectConfig.Global.PrefabTable.Unload(prefabId.Value);
		}
		_prefabIds.Clear();
		_prefabsByNetworkPrefabId.Clear();
	}
}
