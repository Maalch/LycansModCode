using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "PlaceInCircle")]
internal class PlaceInCirclePatch
{
	private static void Postfix(GameManager __instance)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => (Object)(object)o.SummonedSpirit != (Object)null).ToList();
		Vector3 position = Traverse.Create((object)__instance).Field<Transform[]>("mapSpawns").Value[__instance.MapID - 1].position;
		foreach (PlayerCustom item in list)
		{
			((Component)item.SummonedSpirit).transform.position = position;
		}
	}
}
