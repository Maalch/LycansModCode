using System;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PreviewCamera), "DisableCamera")]
internal class PreviewCameraDisableCameraPatch
{
	private static void Postfix(PreviewCamera __instance)
	{
		try
		{
			UIManager.CustomizationComponent.Hide();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PreviewCameraDisableCameraPatch error: " + ex));
		}
	}
}
