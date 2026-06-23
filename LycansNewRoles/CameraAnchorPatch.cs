using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateCameraAnchorOffset")]
internal class CameraAnchorPatch
{
	private static void Postfix(PlayerController __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.Tiny))
			{
				float num = ((__instance.MovementAction == 1) ? 0.09f : 0.12f);
				Transform value = Traverse.Create((object)__instance).Field<Transform>("cameraAnchorPoint").Value;
				Vector3 localPosition = value.localPosition;
				((Vector3)(ref localPosition))._002Ector(localPosition.x, num, localPosition.z);
				value.localPosition = localPosition;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CameraAnchorPatch error: " + ex));
		}
	}
}
