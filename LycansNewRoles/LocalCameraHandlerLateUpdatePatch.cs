using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LocalCameraHandler), "LateUpdate")]
internal class LocalCameraHandlerLateUpdatePatch
{
	private static bool Prefix(LocalCameraHandler __instance)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Invalid comparison between Unknown and I4
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!((Behaviour)__instance.LocalCamera).enabled || (int)GameManager.LocalGameState == 0)
			{
				return false;
			}
			if (NetworkBool.op_Implicit(PlayerCustom.Local.Dying) || NetworkBool.op_Implicit(PlayerCustom.Local.Downed) || NetworkBool.op_Implicit(PlayerCustom.Local.Asleep) || NetworkBool.op_Implicit(PlayerCustom.Local.Banished) || NetworkBool.op_Implicit(PlayerCustom.Local.CapturedByCultist) || (PlayerCustom.Local.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothActor && NetworkBool.op_Implicit(PlayerCustom.Local.SecondaryRolePowerActive)))
			{
				return false;
			}
			if (UIManager.GenericChoicePanel.Active)
			{
				if ((Object)(object)__instance.PovPlayer != (Object)null && __instance.PovPlayer.Ref == PlayerController.Local.Ref)
				{
					((Component)__instance.LocalCamera).transform.position = Traverse.Create((object)__instance).Field<Transform>("localCameraAnchorPoint").Value.position;
				}
				return false;
			}
			if ((Object)(object)__instance.PovPlayer == (Object)null)
			{
				return true;
			}
			if (__instance.PovPlayer.Ref == PlayerController.Local.Ref)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.PovPlayer.Ref);
				if (player.ForcedRotationY != 0f)
				{
					Traverse<float> obj = Traverse.Create((object)__instance).Field<float>("_cameraRotationY");
					obj.Value += player.ForcedRotationY * Time.deltaTime;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("LocalCameraHandlerLateUpdatePatch error: " + ex));
			return true;
		}
	}
}
