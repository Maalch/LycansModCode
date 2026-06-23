using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BepInEx.Logging;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewMaps.Components;
using LycansNewRoles.Sabotages;
using UnityEngine;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameManager), "MapChanged")]
internal class MapChangedPatch
{
	private static bool _shouldReloadPrefabsOnNextChange;

	private static bool Prefix(Changed<GameManager> changed)
	{
		if (_shouldReloadPrefabsOnNextChange)
		{
			NetworkObjectService.Instance.Clear();
			NetworkProjectConfig.UnloadGlobal();
			Plugin.PlayerIllusionCreated = false;
			Plugin.CreatePrefabs();
		}
		_shouldReloadPrefabsOnNextChange = true;
		GameManager behaviour = changed.Behaviour;
		if (((SimulationBehaviour)behaviour).Runner.IsServer)
		{
			List<Loot> list = (from o in Object.FindObjectsOfType<Loot>(true)
				where o.GetMapID() >= 3
				select o).ToList();
			for (int num = list.Count - 1; num >= 0; num--)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)list[num]).GetComponent<NetworkObject>(), false);
			}
			List<DoorCustom> list2 = Object.FindObjectsOfType<DoorCustom>(false).ToList();
			for (int num2 = list2.Count - 1; num2 >= 0; num2--)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)list2[num2]).GetComponent<NetworkObject>(), false);
			}
			List<AutodoorCustom> list3 = Object.FindObjectsOfType<AutodoorCustom>(false).ToList();
			for (int num3 = list3.Count - 1; num3 >= 0; num3--)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)list3[num3]).GetComponent<NetworkObject>(), false);
			}
			List<PortalCustom> list4 = Object.FindObjectsOfType<PortalCustom>(false).ToList();
			for (int num4 = list4.Count - 1; num4 >= 0; num4--)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)list4[num4]).GetComponent<NetworkObject>(), false);
			}
			List<LadderCustom> list5 = Object.FindObjectsOfType<LadderCustom>(false).ToList();
			for (int num5 = list5.Count - 1; num5 >= 0; num5--)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)list5[num5]).GetComponent<NetworkObject>(), false);
			}
			List<MechanismObject> list6 = Object.FindObjectsOfType<MechanismObject>(false).ToList();
			for (int num6 = list6.Count - 1; num6 >= 0; num6--)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)list6[num6]).GetComponent<NetworkObject>(), false);
			}
			List<MechanismButton> list7 = Object.FindObjectsOfType<MechanismButton>(false).ToList();
			for (int num7 = list7.Count - 1; num7 >= 0; num7--)
			{
				((SimulationBehaviour)behaviour).Runner.Despawn(((Component)list7[num7]).GetComponent<NetworkObject>(), false);
			}
		}
		if (changed.Behaviour.MapID > 2)
		{
			return false;
		}
		return true;
	}

	private unsafe static void Postfix(Changed<GameManager> changed)
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_261b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_100f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fed: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1530: Unknown result type (might be due to invalid IL or missing references)
		//IL_1535: Unknown result type (might be due to invalid IL or missing references)
		//IL_153d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1546: Unknown result type (might be due to invalid IL or missing references)
		//IL_1557: Unknown result type (might be due to invalid IL or missing references)
		//IL_1712: Unknown result type (might be due to invalid IL or missing references)
		//IL_1717: Unknown result type (might be due to invalid IL or missing references)
		//IL_171f: Unknown result type (might be due to invalid IL or missing references)
		//IL_172d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1743: Unknown result type (might be due to invalid IL or missing references)
		//IL_175f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1775: Expected O, but got Unknown
		//IL_1919: Unknown result type (might be due to invalid IL or missing references)
		//IL_191e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1926: Unknown result type (might be due to invalid IL or missing references)
		//IL_1934: Unknown result type (might be due to invalid IL or missing references)
		//IL_194a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1966: Unknown result type (might be due to invalid IL or missing references)
		//IL_197c: Expected O, but got Unknown
		//IL_1afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b11: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f36: Expected O, but got Unknown
		//IL_20df: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_20fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_2110: Unknown result type (might be due to invalid IL or missing references)
		//IL_212c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2142: Expected O, but got Unknown
		//IL_21a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_2441: Unknown result type (might be due to invalid IL or missing references)
		//IL_2446: Unknown result type (might be due to invalid IL or missing references)
		//IL_2453: Unknown result type (might be due to invalid IL or missing references)
		//IL_245a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2467: Unknown result type (might be due to invalid IL or missing references)
		//IL_246e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2340: Unknown result type (might be due to invalid IL or missing references)
		//IL_2345: Unknown result type (might be due to invalid IL or missing references)
		//IL_234d: Unknown result type (might be due to invalid IL or missing references)
		//IL_235b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2371: Unknown result type (might be due to invalid IL or missing references)
		//IL_238d: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a3: Expected O, but got Unknown
		//IL_23f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_23fe: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			GameManager behaviour = changed.Behaviour;
			int mapID = behaviour.MapID;
			Plugin.Logger.LogInfo((object)("MapChanged -> " + mapID));
			MapManager.UnloadCurrentMap();
			GameObject val = null;
			switch (mapID)
			{
			case 1:
				val = Plugin.NewMapsCoreBundle.LoadAsset<GameObject>("minimap1");
				val.SetActive(true);
				if (!MapManager.NewMapsByIdInfo.ContainsKey(mapID))
				{
					MapManager.NewMapsByIdInfo[mapID] = new CustomMap(mapID, "map_1", val.transform.Find("OffsetMultiplier"), val.transform.Find("CameraOffset"), val.transform.Find("Rotation"), val.transform.localScale);
				}
				break;
			case 2:
				val = Plugin.NewMapsCoreBundle.LoadAsset<GameObject>("minimap2");
				val.SetActive(true);
				if (!MapManager.NewMapsByIdInfo.ContainsKey(mapID))
				{
					MapManager.NewMapsByIdInfo[mapID] = new CustomMap(mapID, "map_2", val.transform.Find("OffsetMultiplier"), val.transform.Find("CameraOffset"), val.transform.Find("Rotation"), val.transform.localScale);
				}
				break;
			}
			if (mapID > 2)
			{
				string text = Plugin.NewMapPathById[mapID];
				MapManager.CurrentNewMapBundle = AssetBundle.LoadFromFile(Path.GetDirectoryName(Plugin.Location) + "/resources/" + text);
				Plugin.Logger.LogInfo((object)("Bundle " + text + " loaded"));
				string[] allAssetNames = MapManager.CurrentNewMapBundle.GetAllAssetNames();
				string text2 = allAssetNames.FirstOrDefault((string o) => o.Split('/').Last() == "map.prefab");
				string text3 = allAssetNames.FirstOrDefault((string o) => o.Split('/').Last() == "minimap.prefab");
				GameObject val2 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(text2);
				val2.SetActive(false);
				val = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(text3);
				val.SetActive(true);
				if (!MapManager.NewMapsByIdInfo.ContainsKey(mapID))
				{
					MapManager.NewMapsByIdInfo[mapID] = new CustomMap(mapID, ((Object)MapManager.CurrentNewMapBundle).name, val.transform.Find("OffsetMultiplier"), val.transform.Find("CameraOffset"), val.transform.Find("Rotation"), val2.transform.localScale);
				}
				foreach (string item in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("food_") && o.Contains(".prefab")
					select o)
				{
					GameObject val3 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(item);
					GameObject val4 = Object.Instantiate<GameObject>(val3);
					val4.AddComponent<NetworkObject>();
					val4.AddComponent<NetworkTransform>();
					Loot val5 = val4.AddComponent<Loot>();
					Traverse.Create((object)val5).Field<NetworkBool>("_Available").Value = NetworkBool.op_Implicit(false);
					int value = 0;
					if (item.Contains("rarity0"))
					{
						value = 0;
					}
					else if (item.Contains("rarity1"))
					{
						value = 1;
					}
					else if (item.Contains("rarity2"))
					{
						value = 2;
					}
					else if (item.Contains("rarity3"))
					{
						value = 3;
					}
					Traverse.Create((object)val5).Field<int>("rarity").Value = value;
					if (item.Contains("notfood"))
					{
						Traverse.Create((object)val5).Field<bool>("notFood").Value = true;
					}
					val4.AddComponent<LootCustom>();
					val4.SetActive(false);
					NetworkPrefabId val6 = NetworkObjectService.Instance.RegisterNetworkObject(val4.gameObject, ((Object)val3).name);
					MapManager.CurrentNewMapNetworkUniqueKeys.Add(((Object)val3).name);
					ManualLogSource logger = Plugin.Logger;
					string name = ((Object)val3).name;
					NetworkPrefabId val7 = val6;
					logger.LogInfo((object)("Added loot: " + name + " with id " + ((object)(*(NetworkPrefabId*)(&val7))/*cast due to constrained. prefix*/).ToString()));
				}
				foreach (string item2 in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("/door_") && o.Contains(".prefab")
					select o)
				{
					GameObject val8 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(item2);
					GameObject val9 = Object.Instantiate<GameObject>(val8);
					Door val10 = val9.AddComponent<Door>();
					Traverse.Create((object)val10).Field<GameObject>("locks").Value = ((Component)val9.transform.Find("Lock")).gameObject;
					val9.AddComponent<DoorCustom>();
					val9.SetActive(false);
					NetworkObjectService.Instance.RegisterNetworkObject(val9.gameObject, ((Object)val8).name);
					MapManager.CurrentNewMapNetworkUniqueKeys.Add(((Object)val8).name);
					Plugin.Logger.LogInfo((object)("Added door: " + ((Object)val8).name));
				}
				foreach (string item3 in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("/autodoor_") && o.Contains(".prefab")
					select o)
				{
					GameObject val11 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(item3);
					GameObject val12 = Object.Instantiate<GameObject>(val11);
					AutodoorCustom autodoorCustom = val12.AddComponent<AutodoorCustom>();
					Transform val13 = val12.transform.Find("Detection");
					if ((Object)(object)val13 == (Object)null)
					{
						Plugin.Logger.LogError((object)("Autodoor " + ((object)val12)?.ToString() + " is lacking Detection object!"));
						continue;
					}
					AutodoorDetection autodoorDetection = ((Component)val13).gameObject.AddComponent<AutodoorDetection>();
					Transform val14 = val12.transform.Find("SoundOpen");
					if ((Object)(object)val14 != (Object)null)
					{
						autodoorCustom.SoundOpen = ((Object)((Component)val14.GetChild(0)).gameObject).name;
					}
					Transform val15 = val12.transform.Find("SoundClose");
					if ((Object)(object)val15 != (Object)null)
					{
						autodoorCustom.SoundClose = ((Object)((Component)val15.GetChild(0)).gameObject).name;
					}
					val12.SetActive(false);
					NetworkObjectService.Instance.RegisterNetworkObject(val12.gameObject, ((Object)val11).name);
					MapManager.CurrentNewMapNetworkUniqueKeys.Add(((Object)val11).name);
					Plugin.Logger.LogInfo((object)("Added autodoor: " + ((Object)val11).name));
				}
				Portal[] array = Object.FindObjectsOfType<Portal>(true);
				Light val16 = null;
				Portal[] array2 = array;
				foreach (Portal val17 in array2)
				{
					val16 = Traverse.Create((object)val17).Field<Light>("teleportLight").Value;
					if ((Object)(object)val16 != (Object)null)
					{
						break;
					}
				}
				foreach (string item4 in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("portal_") && o.Contains(".prefab")
					select o)
				{
					GameObject val18 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(item4);
					GameObject val19 = Object.Instantiate<GameObject>(val18);
					Portal val20 = val19.AddComponent<Portal>();
					Light value2 = Object.Instantiate<Light>(val16, val19.transform);
					Traverse.Create((object)val20).Field<Light>("teleportLight").Value = value2;
					Transform value3 = val19.transform.Find("PortalTeleportPoint");
					Traverse.Create((object)val20).Field<Transform>("teleportPoint").Value = value3;
					Transform val21 = val19.transform.Find("PortalEffect");
					Traverse.Create((object)val20).Field<GameObject>("portalEffects").Value = ((Component)val21).gameObject;
					PortalCustom portalCustom = val19.AddComponent<PortalCustom>();
					string[] array3 = ((Object)((Component)val18.transform.Find("Config").GetChild(0)).gameObject).name.Split(';');
					portalCustom.ActiveMinimumSeconds = int.Parse(array3[0]);
					portalCustom.ActiveMaximumSeconds = int.Parse(array3[1]);
					portalCustom.InactiveMinimumSeconds = int.Parse(array3[2]);
					portalCustom.InactiveMaximumSeconds = int.Parse(array3[3]);
					val19.SetActive(false);
					NetworkObjectService.Instance.RegisterNetworkObject(val19.gameObject, ((Object)val18).name);
					MapManager.CurrentNewMapNetworkUniqueKeys.Add(((Object)val18).name);
					Plugin.Logger.LogInfo((object)("Added portal: " + ((Object)val18).name));
				}
				foreach (string item5 in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("ladder_") && o.Contains(".prefab")
					select o)
				{
					GameObject val22 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(item5);
					GameObject val23 = Object.Instantiate<GameObject>(val22);
					Ladder val24 = val23.AddComponent<Ladder>();
					Transform val25 = val23.transform.Find("LadderStartTrigger");
					((Component)val25).gameObject.AddComponent<LadderStartTrigger>();
					Transform val26 = val23.transform.Find("LadderEndTrigger");
					LadderEndTrigger val27 = ((Component)val26).gameObject.AddComponent<LadderEndTrigger>();
					Transform value4 = val23.transform.Find("LadderUpClimbingPoint");
					Traverse.Create((object)val27).Field<Transform>("upClimbingPoint").Value = value4;
					Transform value5 = val23.transform.Find("LadderStartPoint");
					Traverse.Create((object)val24).Field<Transform>("startClimbingPoint").Value = value5;
					Transform value6 = val23.transform.Find("LadderEndPoint");
					Traverse.Create((object)val24).Field<Transform>("fallingPoint").Value = value6;
					Traverse.Create((object)val24).Field<NetworkBool>("_CanClimb").Value = NetworkBool.op_Implicit(true);
					val23.AddComponent<LadderCustom>();
					val23.SetActive(false);
					NetworkObjectService.Instance.RegisterNetworkObject(val23.gameObject, ((Object)val22).name);
					MapManager.CurrentNewMapNetworkUniqueKeys.Add(((Object)val22).name);
					Plugin.Logger.LogInfo((object)("Added ladder: " + ((Object)val22).name));
				}
				foreach (string item6 in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("admintable") && o.Contains(".prefab")
					select o)
				{
					GameObject val28 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(item6);
					GameObject val29 = Object.Instantiate<GameObject>(val28);
					val29.AddComponent<NetworkObject>();
					val29.AddComponent<NetworkTransform>();
					val29.AddComponent<AdminTable>();
					val29.SetActive(false);
					NetworkObjectService.Instance.RegisterNetworkObject(val29.gameObject, ((Object)val28).name);
					MapManager.CurrentNewMapNetworkUniqueKeys.Add(((Object)val28).name);
					Plugin.Logger.LogInfo((object)("Added admin table: " + ((Object)val28).name));
				}
				foreach (string item7 in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("mechanismbutton") && o.Contains(".prefab")
					select o)
				{
					GameObject val30 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(item7);
					GameObject val31 = Object.Instantiate<GameObject>(val30);
					MechanismButton mechanismButton = val31.AddComponent<MechanismButton>();
					mechanismButton.MechanismId = ((Object)val31).name.Split('_')[1];
					val31.SetActive(false);
					NetworkObjectService.Instance.RegisterNetworkObject(val31.gameObject, ((Object)val30).name);
					MapManager.CurrentNewMapNetworkUniqueKeys.Add(((Object)val30).name);
					Plugin.Logger.LogInfo((object)("Added mechanism button: " + ((Object)val30).name + ", prefab name: " + ((object)val31)?.ToString() + ", id: " + mechanismButton.MechanismId));
				}
				foreach (string item8 in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("mechanismobject") && o.Contains(".prefab")
					select o)
				{
					GameObject val32 = MapManager.CurrentNewMapBundle.LoadAsset<GameObject>(item8);
					GameObject val33 = Object.Instantiate<GameObject>(val32);
					MechanismObject mechanismObject = val33.AddComponent<MechanismObject>();
					mechanismObject.MechanismId = ((Object)val33).name.Split('_')[1];
					mechanismObject.Duration = int.Parse(((Object)val33.transform.Find("Duration").GetChild(0)).name);
					mechanismObject.Cooldown = int.Parse(((Object)val33.transform.Find("Cooldown").GetChild(0)).name);
					mechanismObject.AutoActivation = (Object)(object)val33.transform.Find("AutoActivation") != (Object)null;
					if ((Object)(object)val33.transform.Find("SoundOnActivation") != (Object)null)
					{
						mechanismObject.SoundOnActivation = ((Object)((Component)val33.transform.Find("SoundOnActivation").GetChild(0)).gameObject).name.ToLower();
					}
					if ((Object)(object)val33.transform.Find("SoundOnDeactivation") != (Object)null)
					{
						mechanismObject.SoundOnDeactivation = ((Object)((Component)val33.transform.Find("SoundOnDeactivation").GetChild(0)).gameObject).name.ToLower();
					}
					if ((Object)(object)val33.transform.Find("PlayerShiftOnActivation") != (Object)null)
					{
						string[] array4 = ((Object)((Component)val33.transform.Find("PlayerShiftOnActivation").GetChild(0)).gameObject).name.Split(';');
						mechanismObject.PlayerShiftOnActivation = new Vector3(float.Parse(array4[0].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture), float.Parse(array4[1].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture), float.Parse(array4[2].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture));
					}
					if ((Object)(object)val33.transform.Find("PlayerShiftOnDeactivation") != (Object)null)
					{
						string[] array5 = ((Object)((Component)val33.transform.Find("PlayerShiftOnDeactivation").GetChild(0)).gameObject).name.Split(';');
						mechanismObject.PlayerShiftOnDeactivation = new Vector3(float.Parse(array5[0].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture), float.Parse(array5[1].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture), float.Parse(array5[2].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture));
					}
					val33.SetActive(false);
					NetworkObjectService.Instance.RegisterNetworkObject(val33.gameObject, ((Object)val32).name);
					MapManager.CurrentNewMapNetworkUniqueKeys.Add(((Object)val32).name);
					Plugin.Logger.LogInfo((object)("Added mechanism object: " + ((Object)val32).name + ", id: " + mechanismObject.MechanismId + ", sound on activation: " + mechanismObject.SoundOnActivation + ", deactivation: " + mechanismObject.SoundOnDeactivation));
				}
				AudioBank value7 = Traverse.Create((object)AudioManager.Instance).Field<AudioBank>("soundBank").Value;
				Dictionary<string, AudioClip> value8 = Traverse.Create((object)value7).Field<Dictionary<string, AudioClip>>("dictionary").Value;
				foreach (string item9 in from o in MapManager.CurrentNewMapBundle.GetAllAssetNames()
					where o.Contains("/sound")
					select o)
				{
					string text4 = item9.Split('/').Last().Split('.')
						.First();
					if (!value8.ContainsKey(text4))
					{
						AudioClip value9 = MapManager.CurrentNewMapBundle.LoadAsset<AudioClip>(item9);
						value8.Add(text4, value9);
						Plugin.Logger.LogInfo((object)("Added map sound: " + text4));
					}
				}
				MapManager.CurrentNewMapObject = Object.Instantiate<GameObject>(val2, new Vector3(0f, 0f, 0f), Quaternion.identity, GameObject.Find("World").transform);
				Plugin.Logger.LogInfo((object)("Created map " + ((object)MapManager.CurrentNewMapObject)?.ToString() + " at position " + ((object)MapManager.CurrentNewMapObject.transform.position/*cast due to constrained. prefix*/).ToString()));
				MapManager.CurrentNewMapObject.SetActive(true);
				List<GameObject> source = (from o in MapManager.CurrentNewMapObject.GetComponentsInChildren<Transform>(true)
					select ((Component)o).gameObject).ToList();
				List<GameObject> list = source.Where((GameObject obj) => ((Object)obj).name.ToLower() == "teleporter").ToList();
				foreach (GameObject item10 in list)
				{
					if (!((Object)(object)item10.GetComponent<Teleporter>() != (Object)null))
					{
						item10.gameObject.RemoveAllChildren();
						Teleporter val34 = item10.gameObject.AddComponent<Teleporter>();
						Traverse.Create((object)val34).Field<int>("mapID").Value = mapID;
					}
				}
				List<GameObject> list2 = source.Where((GameObject obj) => ((Object)obj).name == "ItemSpawn").ToList();
				foreach (GameObject item11 in list2)
				{
					if (!((Object)(object)item11.GetComponent<ItemSpawner>() != (Object)null))
					{
						item11.gameObject.RemoveAllChildren();
						ItemSpawner val35 = item11.gameObject.AddComponent<ItemSpawner>();
						Traverse.Create((object)val35).Field<int>("mapID").Value = mapID;
						Traverse.Create((object)GameManager.Instance).Field<Dictionary<ItemSpawner, bool>>("_spawnedItems").Value.Add(val35, value: false);
					}
				}
				List<Transform> list3 = Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value.ToList();
				Transform value10 = MapManager.CurrentNewMapObject.transform.Find("MapSpawn");
				list3[mapID - 1] = value10;
				Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value = list3.ToArray();
				List<GameObject> list4 = source.Where((GameObject obj) => ((Object)obj).name == "FoodSpawn").ToList();
				foreach (GameObject item12 in list4)
				{
					if (!item12.gameObject.activeSelf)
					{
						continue;
					}
					CustomSpawnComponent component = item12.GetComponent<CustomSpawnComponent>();
					string text5;
					if ((Object)(object)component != (Object)null)
					{
						text5 = component.PrefabName;
					}
					else
					{
						Transform child = item12.transform.GetChild(0);
						if ((Object)(object)child == (Object)null)
						{
							Plugin.Logger.LogError((object)("Missing food name for: " + ((Object)item12).name));
						}
						text5 = ((Object)((Component)child).gameObject).name;
						component = item12.AddComponent<CustomSpawnComponent>();
						component.PrefabName = text5;
					}
					item12.gameObject.RemoveAllChildren();
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject(text5);
						NetworkObject val36 = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject, (Vector3?)item12.transform.position, (Quaternion?)item12.transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val36).transform.parent = item12.transform.parent;
						Loot component2 = ((Component)val36).GetComponent<Loot>();
						Traverse.Create((object)component2).Field<int>("map").Value = behaviour.MapID;
					}
				}
				List<GameObject> list5 = source.Where((GameObject obj) => ((Object)obj).name.StartsWith("DoorSpawn:")).ToList();
				foreach (GameObject doorSpawn in list5)
				{
					if (!doorSpawn.gameObject.activeSelf)
					{
						continue;
					}
					CustomSpawnComponent component3 = doorSpawn.GetComponent<CustomSpawnComponent>();
					string text6;
					if ((Object)(object)component3 != (Object)null)
					{
						text6 = component3.PrefabName;
					}
					else
					{
						Transform child2 = doorSpawn.transform.GetChild(0);
						if ((Object)(object)child2 == (Object)null)
						{
							Plugin.Logger.LogError((object)("Missing door name for: " + ((Object)doorSpawn).name));
						}
						text6 = ((Object)((Component)child2).gameObject).name;
						component3 = doorSpawn.AddComponent<CustomSpawnComponent>();
						component3.PrefabName = text6;
					}
					doorSpawn.gameObject.RemoveAllChildren();
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject2 = NetworkObjectService.Instance.GetNetworkObject(text6);
						NetworkObject val37 = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject2, (Vector3?)doorSpawn.transform.position, (Quaternion?)doorSpawn.transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							((Component)no).transform.SetParent(doorSpawn.transform);
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val37).transform.parent = doorSpawn.transform;
						((Component)val37).GetComponent<DoorCustom>().MapID = behaviour.MapID;
						Plugin.Logger.LogInfo((object)("Created door: " + (object)val37));
					}
				}
				List<GameObject> list6 = source.Where((GameObject obj) => ((Object)obj).name.StartsWith("AutodoorSpawn:")).ToList();
				foreach (GameObject autodoorSpawn in list6)
				{
					if (!autodoorSpawn.gameObject.activeSelf)
					{
						continue;
					}
					CustomSpawnComponent component4 = autodoorSpawn.GetComponent<CustomSpawnComponent>();
					string text7;
					if ((Object)(object)component4 != (Object)null)
					{
						text7 = component4.PrefabName;
					}
					else
					{
						Transform child3 = autodoorSpawn.transform.GetChild(0);
						if ((Object)(object)child3 == (Object)null)
						{
							Plugin.Logger.LogError((object)("Missing autodoor name for: " + ((Object)autodoorSpawn).name));
						}
						text7 = ((Object)((Component)child3).gameObject).name;
						component4 = autodoorSpawn.AddComponent<CustomSpawnComponent>();
						component4.PrefabName = text7;
					}
					autodoorSpawn.gameObject.RemoveAllChildren();
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject3 = NetworkObjectService.Instance.GetNetworkObject(text7);
						NetworkObject val38 = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject3, (Vector3?)autodoorSpawn.transform.position, (Quaternion?)autodoorSpawn.transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							((Component)no).transform.SetParent(autodoorSpawn.transform.parent);
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val38).transform.parent = autodoorSpawn.transform.parent;
						((Component)val38).GetComponent<AutodoorCustom>().MapID = behaviour.MapID;
						Plugin.Logger.LogInfo((object)("Created autodoor: " + (object)val38));
					}
				}
				List<GameObject> list7 = source.Where((GameObject obj) => ((Object)obj).name.Contains("PortalSpawn:")).ToList();
				foreach (GameObject item13 in list7)
				{
					if (!item13.gameObject.activeSelf)
					{
						continue;
					}
					CustomSpawnComponent component5 = item13.GetComponent<CustomSpawnComponent>();
					string text8;
					if ((Object)(object)component5 != (Object)null)
					{
						text8 = component5.PrefabName;
					}
					else
					{
						Transform child4 = item13.transform.GetChild(0);
						if ((Object)(object)child4 == (Object)null)
						{
							Plugin.Logger.LogError((object)("Missing portal name for: " + ((Object)item13).name));
						}
						text8 = ((Object)((Component)child4).gameObject).name;
						component5 = item13.AddComponent<CustomSpawnComponent>();
						component5.PrefabName = text8;
					}
					item13.gameObject.RemoveAllChildren();
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject4 = NetworkObjectService.Instance.GetNetworkObject(text8);
						NetworkObject val39 = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject4, (Vector3?)item13.transform.position, (Quaternion?)item13.transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val39).transform.SetParent(item13.transform.parent);
						((Component)val39).GetComponent<PortalCustom>().MapID = behaviour.MapID;
						Plugin.Logger.LogInfo((object)("Created portal: " + ((object)val39)?.ToString() + " at position " + ((object)((Component)val39).transform.position/*cast due to constrained. prefix*/).ToString()));
					}
				}
				List<GameObject> list8 = source.Where((GameObject obj) => ((Object)obj).name.Contains("LadderSpawn:")).ToList();
				foreach (GameObject item14 in list8)
				{
					if (!item14.gameObject.activeSelf)
					{
						continue;
					}
					CustomSpawnComponent component6 = item14.GetComponent<CustomSpawnComponent>();
					string text9;
					if ((Object)(object)component6 != (Object)null)
					{
						text9 = component6.PrefabName;
					}
					else
					{
						Transform child5 = item14.transform.GetChild(0);
						if ((Object)(object)child5 == (Object)null)
						{
							Plugin.Logger.LogError((object)("Missing ladder name for: " + ((Object)item14).name));
						}
						text9 = ((Object)((Component)child5).gameObject).name;
						component6 = item14.AddComponent<CustomSpawnComponent>();
						component6.PrefabName = text9;
					}
					item14.gameObject.RemoveAllChildren();
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject5 = NetworkObjectService.Instance.GetNetworkObject(text9);
						NetworkObject val40 = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject5, (Vector3?)item14.transform.position, (Quaternion?)item14.transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val40).transform.parent = item14.transform.parent;
						((Component)val40).GetComponent<LadderCustom>().MapID = behaviour.MapID;
						Plugin.Logger.LogInfo((object)("Created ladder: " + (object)val40));
					}
				}
				List<GameObject> list9 = source.Where((GameObject obj) => ((Object)obj).name.Contains("AdminTableSpawn:")).ToList();
				foreach (GameObject adminTableSpawn in list9)
				{
					if (!adminTableSpawn.gameObject.activeSelf)
					{
						continue;
					}
					CustomSpawnComponent component7 = adminTableSpawn.GetComponent<CustomSpawnComponent>();
					string text10;
					if ((Object)(object)component7 != (Object)null)
					{
						text10 = component7.PrefabName;
					}
					else
					{
						Transform child6 = adminTableSpawn.transform.GetChild(0);
						if ((Object)(object)child6 == (Object)null)
						{
							Plugin.Logger.LogError((object)("Missing admin table name for: " + ((Object)adminTableSpawn).name));
						}
						text10 = ((Object)((Component)child6).gameObject).name;
						component7 = adminTableSpawn.AddComponent<CustomSpawnComponent>();
						component7.PrefabName = text10;
					}
					adminTableSpawn.gameObject.RemoveAllChildren();
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject6 = NetworkObjectService.Instance.GetNetworkObject(text10);
						NetworkObject val41 = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject6, (Vector3?)adminTableSpawn.transform.position, (Quaternion?)adminTableSpawn.transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							((Component)no).transform.SetParent(adminTableSpawn.transform.parent);
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val41).transform.parent = adminTableSpawn.transform.parent;
						((Component)val41).GetComponent<AdminTable>().MapID = behaviour.MapID;
						Plugin.Logger.LogInfo((object)("Created admin table: " + (object)val41));
					}
				}
				List<GameObject> list10 = source.Where((GameObject obj) => ((Object)obj).name.StartsWith("MechanismObjectSpawn:")).ToList();
				foreach (GameObject mechanismObjectSpawn in list10)
				{
					if (!mechanismObjectSpawn.gameObject.activeSelf)
					{
						continue;
					}
					CustomSpawnComponent component8 = mechanismObjectSpawn.GetComponent<CustomSpawnComponent>();
					string text11;
					if ((Object)(object)component8 != (Object)null)
					{
						text11 = component8.PrefabName;
					}
					else
					{
						Transform child7 = mechanismObjectSpawn.transform.GetChild(0);
						if ((Object)(object)child7 == (Object)null)
						{
							Plugin.Logger.LogError((object)("Missing mechanismObject name for: " + ((Object)mechanismObjectSpawn).name));
						}
						text11 = ((Object)((Component)child7).gameObject).name;
						component8 = mechanismObjectSpawn.AddComponent<CustomSpawnComponent>();
						component8.PrefabName = text11;
					}
					mechanismObjectSpawn.gameObject.RemoveAllChildren();
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject7 = NetworkObjectService.Instance.GetNetworkObject(text11);
						NetworkObject val42 = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject7, (Vector3?)mechanismObjectSpawn.transform.position, (Quaternion?)mechanismObjectSpawn.transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							((Component)no).transform.SetParent(mechanismObjectSpawn.transform.parent);
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val42).transform.parent = mechanismObjectSpawn.transform.parent;
						((Component)val42).GetComponent<MechanismObject>().MapID = behaviour.MapID;
						Plugin.Logger.LogInfo((object)("Created mechanismObject: " + ((object)val42)?.ToString() + " at position " + ((object)((Component)val42).transform.position/*cast due to constrained. prefix*/).ToString() + ", parent: " + (object)((Component)val42).transform.parent));
					}
				}
				List<GameObject> list11 = source.Where((GameObject obj) => ((Object)obj).name.StartsWith("MechanismButtonSpawn:")).ToList();
				foreach (GameObject mechanismButtonSpawn in list11)
				{
					if (!mechanismButtonSpawn.gameObject.activeSelf)
					{
						continue;
					}
					CustomSpawnComponent component9 = mechanismButtonSpawn.GetComponent<CustomSpawnComponent>();
					string text12;
					if ((Object)(object)component9 != (Object)null)
					{
						text12 = component9.PrefabName;
					}
					else
					{
						Transform child8 = mechanismButtonSpawn.transform.GetChild(0);
						if ((Object)(object)child8 == (Object)null)
						{
							Plugin.Logger.LogError((object)("Missing mechanismButton name for: " + ((Object)mechanismButtonSpawn).name));
						}
						text12 = ((Object)((Component)child8).gameObject).name;
						component9 = mechanismButtonSpawn.AddComponent<CustomSpawnComponent>();
						component9.PrefabName = text12;
					}
					mechanismButtonSpawn.gameObject.RemoveAllChildren();
					if (((SimulationBehaviour)behaviour).Runner.IsServer)
					{
						NetworkPrefabId networkObject8 = NetworkObjectService.Instance.GetNetworkObject(text12);
						NetworkObject val43 = ((SimulationBehaviour)behaviour).Runner.Spawn(networkObject8, (Vector3?)mechanismButtonSpawn.transform.position, (Quaternion?)mechanismButtonSpawn.transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							((Component)no).transform.SetParent(mechanismButtonSpawn.transform.parent);
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val43).transform.parent = mechanismButtonSpawn.transform.parent;
						((Component)val43).GetComponent<MechanismButton>().MapID = behaviour.MapID;
						Plugin.Logger.LogInfo((object)("Created mechanismButton: " + ((object)val43)?.ToString() + " at position " + ((object)((Component)val43).transform.position/*cast due to constrained. prefix*/).ToString()));
					}
				}
				Vector3 position = list3[mapID - 1].position;
				((Component)behaviour.previewCamera).transform.position = new Vector3(position.x, position.y + 1.12f, position.z);
				List<GameObject> list12 = Traverse.Create((object)behaviour).Field<GameObject[]>("maps").Value.ToList();
				foreach (GameObject item15 in list12)
				{
					item15.gameObject.SetActive(false);
				}
				if (((SimulationBehaviour)behaviour).HasStateAuthority)
				{
					behaviour.PlaceInCircle();
				}
			}
			Plugin.Minimap.Init(Object.Instantiate<GameObject>(val));
			((Behaviour)Traverse.Create((object)GameManager.LightingManager).Field<Light>("directionalLight").Value).enabled = behaviour.MapID != MapManager.FindMapIdByName("map_dungeon");
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.Reset();
			}
			SabotageManager.Instance.ClearCachedObjects();
			DungeonMap.Lights.Clear();
			Light[] source2 = Resources.FindObjectsOfTypeAll<Light>();
			DungeonMap.Lights.AddRange(source2.Where((Light o) => ((Object)((Component)o).gameObject).name == "RoomCenterLight"));
			if (behaviour.MapID == MapManager.FindMapIdByName("map_dungeon"))
			{
				foreach (Light light in DungeonMap.Lights)
				{
					((Component)light).gameObject.SetActive(true);
					light.color = DungeonMap.TorchColorDaytime;
					light.intensity = DungeonMap.TorchIntensityDaytime;
				}
				return;
			}
			AudioManager.PlayMusic("DAY", 1f);
			foreach (Light light2 in DungeonMap.Lights)
			{
				((Component)light2).gameObject.SetActive(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MapChangedPatch error: " + ex));
		}
	}
}
