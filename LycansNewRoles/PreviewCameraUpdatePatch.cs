using System;
using System.Collections.Generic;
using HarmonyLib;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PreviewCamera), "Update")]
internal class PreviewCameraUpdatePatch
{
	private static bool Prefix(PreviewCamera __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)GameManager.LocalGameState == 1 && !GameManager.Instance.gameUI.IsGameSettingMenuOpen && !GameManager.Instance.gameUI.IsSettingMenuOpen)
			{
				Vector3 val = ((Component)PlayerController.Local).transform.position - ((Component)__instance).transform.position;
				val.y = 0f;
				Quaternion rotation = Quaternion.LookRotation(val, Vector3.up);
				((Component)__instance).transform.rotation = rotation;
				if (InputManager.Instance.CrouchJustPressed)
				{
					if (!__instance.IsPreviewCameraActive)
					{
						Traverse.Create((object)__instance).Property<bool>("IsPreviewCameraActive", (object[])null).Value = true;
						Traverse.Create((object)__instance).Method("DisplayPreviewCamera", new List<Type> { typeof(bool) }.ToArray(), (object[])null).GetValue(new object[1] { true });
						UIManager.CustomizationComponent.Show();
					}
					else
					{
						Traverse.Create((object)__instance).Property<bool>("IsPreviewCameraActive", (object[])null).Value = false;
						Traverse.Create((object)__instance).Method("DisplayPreviewCamera", new List<Type> { typeof(bool) }.ToArray(), (object[])null).GetValue(new object[1] { false });
						UIManager.CustomizationComponent.Hide();
					}
				}
				if (__instance.IsPreviewCameraActive)
				{
					if (InputManager.Instance.ItemJustPressed)
					{
						UIManager.CustomizationComponent.CycleType();
					}
					else if (InputManager.Instance.PrimaryInteractJustPressed)
					{
						UIManager.CustomizationComponent.ChooseNext();
					}
					else if (InputManager.Instance.PrimaryActionJustPressed)
					{
						UIManager.CustomizationComponent.ChoosePrevious();
					}
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PreviewCameraUpdatePatch error: " + ex));
			return false;
		}
	}
}
