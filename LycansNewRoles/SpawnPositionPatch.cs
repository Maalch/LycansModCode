using System;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "GetSpawnPosition")]
internal class SpawnPositionPatch
{
	private static bool Prefix(PlayerController playerController, GameManager __instance, ref Vector3 __result)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			float num = 24f;
			float num2 = (float)(int)playerController.Index * num;
			Vector3 val = Quaternion.Euler(0f, num2, 0f) * Vector3.forward * Traverse.Create((object)__instance).Field<float>("radius").Value;
			__result = Traverse.Create((object)__instance).Field<Transform[]>("mapSpawns").Value[__instance.MapID - 1].position + val;
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SpawnPositionPatch error: " + ex));
			return true;
		}
	}
}
