using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewMaps;

public static class MapManager
{
	public const string MapNameDefault1 = "map_1";

	public const string MapNameDefault2 = "map_2";

	public const string MapNameDungeon = "map_dungeon";

	public const string MapNameHaddoncans = "map_haddoncans";

	public const string MapNameApartcan = "map_apartcan";

	public const string MapNameLaboratory = "map_laboratory";

	public const string MapNameGot = "map_got";

	public static Camera TestCamera;

	public static Dictionary<int, CustomMap> NewMapsByIdInfo = new Dictionary<int, CustomMap>();

	public static GameObject CurrentNewMapObject = null;

	public static AssetBundle CurrentNewMapBundle = null;

	public static List<string> CurrentNewMapNetworkUniqueKeys = new List<string>();

	public static void UpdateMapByPlayersAmount()
	{
		try
		{
			if (GameManager.Instance.MapID <= 2)
			{
				return;
			}
			int num = PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead)));
			GameObject[] source = Resources.FindObjectsOfTypeAll<GameObject>();
			foreach (GameObject item in source.Where((GameObject o) => ((Object)o).name.ToLower().Contains("minimumplayers")))
			{
				int num2 = int.Parse(((Object)item).name.ToLower().Replace("minimumplayers", ""));
				item.SetActive(num >= num2);
			}
			foreach (GameObject item2 in source.Where((GameObject o) => ((Object)o).name.ToLower().Contains("maximumplayers")))
			{
				int num3 = int.Parse(((Object)item2).name.ToLower().Replace("maximumplayers", ""));
				item2.SetActive(num <= num3);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateMapByPlayersAmount error: " + ex));
		}
	}

	public static int? FindMapIdByName(string mapName)
	{
		if (NewMapsByIdInfo.Any((KeyValuePair<int, CustomMap> o) => o.Value.MapName == mapName))
		{
			return NewMapsByIdInfo.First((KeyValuePair<int, CustomMap> o) => o.Value.MapName == mapName).Key;
		}
		return null;
	}

	public static string FindMapNameById(int mapId)
	{
		if (NewMapsByIdInfo.ContainsKey(mapId))
		{
			return NewMapsByIdInfo[mapId].MapName;
		}
		return null;
	}

	public static void RescaleSpawnedObject(GameObject gameObject, GameObject spawn, CustomMap mapInfo)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * (((Object)(object)spawn != (Object)null) ? spawn.transform.localScale.x : 1f) * mapInfo.MapResize.x, gameObject.transform.localScale.y * (((Object)(object)spawn != (Object)null) ? spawn.transform.localScale.y : 1f) * mapInfo.MapResize.y, gameObject.transform.localScale.z * (((Object)(object)spawn != (Object)null) ? spawn.transform.localScale.z : 1f) * mapInfo.MapResize.z);
	}

	public static void UnloadCurrentMap()
	{
		if ((Object)(object)CurrentNewMapObject != (Object)null)
		{
			Object.Destroy((Object)(object)CurrentNewMapObject);
			CurrentNewMapObject = null;
		}
		if ((Object)(object)CurrentNewMapBundle != (Object)null)
		{
			CurrentNewMapBundle.Unload(true);
			CurrentNewMapBundle = null;
		}
		Dictionary<ItemSpawner, bool> value = Traverse.Create((object)GameManager.Instance).Field<Dictionary<ItemSpawner, bool>>("_spawnedItems").Value;
		value = value.Where((KeyValuePair<ItemSpawner, bool> o) => o.Key.MapID <= 2).ToDictionary((KeyValuePair<ItemSpawner, bool> o) => o.Key, (KeyValuePair<ItemSpawner, bool> o) => o.Value);
		Traverse.Create((object)GameManager.Instance).Field<Dictionary<ItemSpawner, bool>>("_spawnedItems").Value = value;
	}
}
