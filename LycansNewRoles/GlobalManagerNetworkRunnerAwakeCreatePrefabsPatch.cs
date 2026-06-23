using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.NewMaps;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(NetworkRunner), "Awake")]
internal class GlobalManagerNetworkRunnerAwakeCreatePrefabsPatch
{
	private static bool _alreadyAddedPotionsColors;

	private static void Prefix(NetworkRunner __instance)
	{
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Plugin.CreatePrefabs();
			MapManager.UnloadCurrentMap();
			List<Item> list = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value.ToList();
			if (!list.Any((Item o) => o is MagicScrollItem))
			{
				MagicScrollItem item = Object.FindObjectOfType<MagicScrollItem>(true);
				list.Add((Item)(object)item);
			}
			if (!list.Any((Item o) => o is PhasingDiamondItem))
			{
				PhasingDiamondItem item2 = Object.FindObjectOfType<PhasingDiamondItem>(true);
				list.Add((Item)(object)item2);
			}
			if (!list.Any((Item o) => o is GrenadeItem))
			{
				GrenadeItem item3 = Object.FindObjectOfType<GrenadeItem>(true);
				list.Add((Item)(object)item3);
			}
			if (!list.Any((Item o) => o is SleepingGasItem))
			{
				SleepingGasItem item4 = Object.FindObjectOfType<SleepingGasItem>(true);
				list.Add((Item)(object)item4);
			}
			if (!list.Any((Item o) => o is MolotovItem))
			{
				MolotovItem item5 = Object.FindObjectOfType<MolotovItem>(true);
				list.Add((Item)(object)item5);
			}
			if (!list.Any((Item o) => o is RadarItem))
			{
				RadarItem item6 = Object.FindObjectOfType<RadarItem>(true);
				list.Add((Item)(object)item6);
			}
			Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value = list.ToArray();
			GameManagerCustom.SpawnableAccessories.Clear();
			AccessoryBoots item7 = Object.FindObjectOfType<AccessoryBoots>(true);
			GameManagerCustom.SpawnableAccessories.Add(item7);
			AccessoryHorn item8 = Object.FindObjectOfType<AccessoryHorn>(true);
			GameManagerCustom.SpawnableAccessories.Add(item8);
			AccessoryRing item9 = Object.FindObjectOfType<AccessoryRing>(true);
			GameManagerCustom.SpawnableAccessories.Add(item9);
			AccessoryMagnifier item10 = Object.FindObjectOfType<AccessoryMagnifier>(true);
			GameManagerCustom.SpawnableAccessories.Add(item10);
			AccessoryCrystalBall item11 = Object.FindObjectOfType<AccessoryCrystalBall>(true);
			GameManagerCustom.SpawnableAccessories.Add(item11);
			AccessoryBackpack item12 = Object.FindObjectOfType<AccessoryBackpack>(true);
			GameManagerCustom.SpawnableAccessories.Add(item12);
			AccessorySpellbook item13 = Object.FindObjectOfType<AccessorySpellbook>(true);
			GameManagerCustom.SpawnableAccessories.Add(item13);
			ItemManager value = Traverse.Create(typeof(ItemManager)).Field<ItemManager>("_instance").Value;
			List<Item> list2 = Traverse.Create((object)value).Field<Item[]>("items").Value.ToList();
			if (!list2.Any((Item o) => o is MagicScrollItem))
			{
				MagicScrollItem item14 = Object.FindObjectOfType<MagicScrollItem>(true);
				list2.Add((Item)(object)item14);
			}
			if (!list.Any((Item o) => o is PhasingDiamondItem))
			{
				PhasingDiamondItem item15 = Object.FindObjectOfType<PhasingDiamondItem>(true);
				list2.Add((Item)(object)item15);
			}
			if (!list2.Any((Item o) => o is GrenadeItem))
			{
				GrenadeItem item16 = Object.FindObjectOfType<GrenadeItem>(true);
				list2.Add((Item)(object)item16);
			}
			if (!list2.Any((Item o) => o is SleepingGasItem))
			{
				SleepingGasItem item17 = Object.FindObjectOfType<SleepingGasItem>(true);
				list2.Add((Item)(object)item17);
			}
			if (!list2.Any((Item o) => o is MolotovItem))
			{
				MolotovItem item18 = Object.FindObjectOfType<MolotovItem>(true);
				list2.Add((Item)(object)item18);
			}
			if (!list2.Any((Item o) => o is RadarItem))
			{
				RadarItem item19 = Object.FindObjectOfType<RadarItem>(true);
				list2.Add((Item)(object)item19);
			}
			if (!list2.Any((Item o) => o is AccessoryBoots))
			{
				AccessoryBoots item20 = Object.FindObjectOfType<AccessoryBoots>(true);
				list2.Add((Item)(object)item20);
			}
			if (!list2.Any((Item o) => o is AccessoryHorn))
			{
				AccessoryHorn item21 = Object.FindObjectOfType<AccessoryHorn>(true);
				list2.Add((Item)(object)item21);
			}
			if (!list2.Any((Item o) => o is AccessoryRing))
			{
				AccessoryRing item22 = Object.FindObjectOfType<AccessoryRing>(true);
				list2.Add((Item)(object)item22);
			}
			if (!list2.Any((Item o) => o is AccessoryMagnifier))
			{
				AccessoryMagnifier item23 = Object.FindObjectOfType<AccessoryMagnifier>(true);
				list2.Add((Item)(object)item23);
			}
			if (!list2.Any((Item o) => o is AccessoryCrystalBall))
			{
				AccessoryCrystalBall item24 = Object.FindObjectOfType<AccessoryCrystalBall>(true);
				list2.Add((Item)(object)item24);
			}
			if (!list2.Any((Item o) => o is AccessoryBackpack))
			{
				AccessoryBackpack item25 = Object.FindObjectOfType<AccessoryBackpack>(true);
				list2.Add((Item)(object)item25);
			}
			if (!list2.Any((Item o) => o is AccessorySpellbook))
			{
				AccessorySpellbook item26 = Object.FindObjectOfType<AccessorySpellbook>(true);
				list2.Add((Item)(object)item26);
			}
			Traverse.Create((object)value).Field<Item[]>("items").Value = list2.ToArray();
			if (!_alreadyAddedPotionsColors)
			{
				List<Color> list3 = Traverse.Create(typeof(Potion)).Field<Color[]>("PotionColors").Value.ToList();
				list3.Add(new Color(1f, 1f, 1f, 1f));
				Traverse.Create(typeof(Potion)).Field<Color[]>("PotionColors").Value = list3.ToArray();
				_alreadyAddedPotionsColors = true;
			}
			EffectManager value2 = Traverse.Create(typeof(EffectManager)).Field<EffectManager>("_instance").Value;
			List<Sprite> list4 = Traverse.Create((object)value2).Field<Sprite[]>("potionSprites").Value.ToList();
			list4.Add(Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("alchemistpotion"));
			Traverse.Create((object)value2).Field<Sprite[]>("potionSprites").Value = list4.ToArray();
			if (Harmony.HasAnyPatches("lycans.nalesnewmaps"))
			{
				GameManager.Instance.gameUI.DisplayMessage(TranslationManager.Instance.GetTranslation("NALES_NEW_MAPS_INSTALLED_CONFLICT"));
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NetworkRunner Awake error: " + ex));
		}
	}
}
