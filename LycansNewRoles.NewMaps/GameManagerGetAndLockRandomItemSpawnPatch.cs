using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Helpers.Collections;
using UnityEngine;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameManager), "GetAndLockRandomItemSpawn")]
internal class GameManagerGetAndLockRandomItemSpawnPatch
{
	private static bool Prefix(GameManager __instance, ref ItemSpawner __result)
	{
		try
		{
			Dictionary<ItemSpawner, bool> value = Traverse.Create((object)__instance).Field<Dictionary<ItemSpawner, bool>>("_spawnedItems").Value;
			List<ItemSpawner> list = value.Where(delegate(KeyValuePair<ItemSpawner, bool> sp)
			{
				KeyValuePair<ItemSpawner, bool> keyValuePair = sp;
				int result;
				if (((Component)keyValuePair.Key).gameObject.activeSelf)
				{
					keyValuePair = sp;
					if (!keyValuePair.Value)
					{
						keyValuePair = sp;
						result = ((keyValuePair.Key.MapID == __instance.MapID) ? 1 : 0);
						goto IL_003e;
					}
				}
				result = 0;
				goto IL_003e;
				IL_003e:
				return (byte)result != 0;
			}).ToDictionary((KeyValuePair<ItemSpawner, bool> sp) => sp.Key, (KeyValuePair<ItemSpawner, bool> sp) => sp.Value).Keys.ToList();
			if (list.Any())
			{
				ItemSpawner val = CollectionsUtil.Grab<ItemSpawner>(list, 1).First();
				value[val] = true;
				__result = val;
				return false;
			}
			__result = null;
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameManagerGetAndLockRandomItemSpawnPatch error: " + ex));
			return true;
		}
	}
}
