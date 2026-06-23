using System;
using System.Collections;
using Fusion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace LycansNewRoles;

public static class ColorAdjustmentManager
{
	private static Color ReducedWolfColor = new Color(1f, 0.8f, 0.8f, 1f);

	private static Color LoverWolfColor = new Color(1f, 0.4f, 1f, 1f);

	private static Color LoverReducedWolfColor = new Color(1f, 0.8f, 1f, 1f);

	private static Color MysticCloseColor = new Color(0f, 1f, 1f, 1f);

	private static Color? FlashColor = null;

	public static void UpdateColorAdjustment()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerController.Local == (Object)null || (Object)(object)PlayerController.Local.LocalCameraHandler == (Object)null || (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer == (Object)null)
		{
			return;
		}
		PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(povPlayer.Ref);
		if ((Object)(object)player == (Object)null)
		{
			return;
		}
		ColorAdjustments value = Traverse.Create((object)GameManager.LightingManager).Field<ColorAdjustments>("_colorAdjustments").Value;
		if (FlashColor.HasValue)
		{
			((VolumeParameter<Color>)(object)value.colorFilter).value = FlashColor.Value;
			return;
		}
		if (NetworkBool.op_Implicit(player.Blind) || NetworkBool.op_Implicit(player.Dying) || NetworkBool.op_Implicit(player.Paralyzed) || NetworkBool.op_Implicit(player.Asleep) || NetworkBool.op_Implicit(player.Downed) || NetworkBool.op_Implicit(player.Banished) || (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothActor && NetworkBool.op_Implicit(player.SecondaryRolePowerActive)))
		{
			((VolumeParameter<Color>)(object)value.colorFilter).value = Color.black;
			return;
		}
		if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Tracker)
		{
			TickTimer primaryRoleActionTimer = player.PrimaryRoleActionTimer;
			if (((TickTimer)(ref primaryRoleActionTimer)).IsRunning)
			{
				((VolumeParameter<Color>)(object)value.colorFilter).value = Color.black;
				return;
			}
		}
		if (NetworkBool.op_Implicit(povPlayer.IsWolf))
		{
			if (NetworkBool.op_Implicit(player.Repulsion) || NetworkBool.op_Implicit(player.CapturedByCultist))
			{
				((VolumeParameter<Color>)(object)value.colorFilter).value = MysticCloseColor;
			}
			else if (PlayerCustomRegistry.Any((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && !NetworkBool.op_Implicit(o.PlayerController.IsDead)))
			{
				if (ExtraSettings.Instance.ReduceWolfRed)
				{
					((VolumeParameter<Color>)(object)value.colorFilter).value = LoverReducedWolfColor;
				}
				else
				{
					((VolumeParameter<Color>)(object)value.colorFilter).value = LoverWolfColor;
				}
			}
			else if (ExtraSettings.Instance.ReduceWolfRed)
			{
				((VolumeParameter<Color>)(object)value.colorFilter).value = ReducedWolfColor;
			}
			else
			{
				((VolumeParameter<Color>)(object)value.colorFilter).value = Color.red;
			}
		}
		else
		{
			((VolumeParameter<Color>)(object)value.colorFilter).value = Color.white;
		}
	}

	public static void FlashScreen(Color color)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			FlashColor = color;
			UpdateColorAdjustment();
			((MonoBehaviour)GameManager.Instance.gameUI).StartCoroutine(WaitThenRemoveFlash(0.1f));
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("FlashScreen error: " + ex));
		}
	}

	private static IEnumerator WaitThenRemoveFlash(float duration)
	{
		yield return (object)new WaitForSeconds(duration);
		try
		{
			FlashColor = null;
			UpdateColorAdjustment();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("WaitThenRemoveFlash error: " + ex));
		}
	}
}
