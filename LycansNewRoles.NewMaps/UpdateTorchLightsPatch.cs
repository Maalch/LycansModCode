using System;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(LightingManager), "UpdateLighting")]
public class UpdateTorchLightsPatch
{
	private static void Postfix(LightingManager __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)GameManager.LocalGameState != 2 || GameManager.Instance.MapID != MapManager.FindMapIdByName("map_dungeon"))
			{
				return;
			}
			float value = Traverse.Create((object)__instance).Field<float>("_localTimeOfDay").Value;
			Color val = DungeonMap.TorchColorDaytime;
			float intensity = DungeonMap.TorchIntensityDaytime;
			if (value >= 6f && value <= 15f)
			{
				val = DungeonMap.TorchColorDaytime;
				intensity = DungeonMap.TorchIntensityDaytime;
			}
			else if (value > 15f && value <= 17f)
			{
				val = DungeonMap.TorchColorMostlyDay;
				intensity = DungeonMap.TorchIntensityMostlyDay;
			}
			else if (value > 17f && value <= 18f)
			{
				val = DungeonMap.TorchColorMostlyNight;
				intensity = DungeonMap.TorchIntensityMostlyNight;
			}
			else if (value > 18f || value <= 3f)
			{
				val = DungeonMap.TorchColorNight;
				intensity = DungeonMap.TorchIntensityNight;
			}
			else if (value > 3f && value <= 5f)
			{
				val = DungeonMap.TorchColorMostlyNight;
				intensity = DungeonMap.TorchIntensityMostlyNight;
			}
			else if (value > 5f && value <= 6f)
			{
				val = DungeonMap.TorchColorMostlyDay;
				intensity = DungeonMap.TorchIntensityMostlyDay;
			}
			if (!(DungeonMap.CurrentTorchColor != val))
			{
				return;
			}
			DungeonMap.CurrentTorchColor = val;
			foreach (Light light in DungeonMap.Lights)
			{
				light.color = val;
				light.intensity = intensity;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateTorchLightsPatch error: " + ex));
		}
	}
}
