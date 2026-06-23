using System.Collections.Generic;
using UnityEngine;

namespace LycansNewRoles.NewMaps;

public static class DungeonMap
{
	public static Color TorchColorDaytime = new Color(1f, 0.9f, 0.8f, 1f);

	public static Color TorchColorMostlyDay = new Color(1f, 0.75f, 0.6f, 1f);

	public static Color TorchColorMostlyNight = new Color(1f, 0.6f, 0.4f, 1f);

	public static Color TorchColorNight = new Color(1f, 0.5f, 0.3f, 1f);

	public static float TorchIntensityDaytime = 1f;

	public static float TorchIntensityMostlyDay = 0.8f;

	public static float TorchIntensityMostlyNight = 0.65f;

	public static float TorchIntensityNight = 0.5f;

	public static Color CurrentTorchColor = TorchColorDaytime;

	public static List<Light> Lights = new List<Light>();
}
