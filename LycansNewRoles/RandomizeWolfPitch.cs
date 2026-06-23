using System;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateModel")]
public class RandomizeWolfPitch
{
	private static void Postfix(bool isWolf, PlayerController __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if ((Object)(object)PlayerController.Local != (Object)null && (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer != (Object)null)
			{
				VoiceSpeaker componentInChildren = ((Component)__instance).GetComponentInChildren<VoiceSpeaker>();
				if ((Object)(object)componentInChildren != (Object)null)
				{
					componentInChildren.UpdatePitch(VoiceChanges.GetVoicePitch(__instance, player));
				}
			}
			Traverse.Create((object)__instance).Field<GameObject>("gunContainer").Value.SetActive(true);
			if (isWolf)
			{
				PlayerCustomRegistry.GetPlayer(__instance.Ref).UpdateWolfColor();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("RandomizeWolfPitch error: " + ex));
		}
	}
}
