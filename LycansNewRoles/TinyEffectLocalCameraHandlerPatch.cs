using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LocalCameraHandler), "UpdateAnchorOffset")]
internal class TinyEffectLocalCameraHandlerPatch
{
	private static void Postfix(LocalCameraHandler __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)GameManager.LocalGameState == 0 || !((Object)(object)__instance.PovPlayer != (Object)null) || NetworkBool.op_Implicit(__instance.PovPlayer.IsDead))
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.PovPlayer.Ref);
			Traverse<bool> val = Traverse.Create((object)__instance).Field<bool>("_giant");
			if (val.Value != NetworkBool.op_Implicit(player.Tiny))
			{
				Transform value = Traverse.Create((object)__instance).Field<Transform>("localCameraAnchorPoint").Value;
				int value2 = Traverse.Create((object)__instance).Field<int>("_movementStatus").Value;
				Traverse<bool> val2 = Traverse.Create((object)__instance).Field<bool>("_isWolf");
				float value3 = Traverse.Create((object)__instance).Field<float>("_baseCameraOffset").Value;
				int movementAction = __instance.PovPlayer.MovementAction;
				bool flag = NetworkBool.op_Implicit(__instance.PovPlayer.IsWolf);
				bool flag2 = NetworkBool.op_Implicit(__instance.PovPlayer.PlayerEffectManager.Giant);
				bool flag3 = NetworkBool.op_Implicit(player.Tiny);
				float num = ((movementAction == 1) ? (value3 - 0.5f) : value3);
				if (flag)
				{
					num = (flag3 ? ((movementAction == 1) ? 0.35f : 0.5f) : ((!flag2) ? (num + 0.25f) : ((movementAction == 1) ? 7.2f : 10.2f)));
				}
				else if (flag2)
				{
					num = ((movementAction == 1) ? 7.2f : 10.2f);
				}
				else if (flag3)
				{
					num = ((movementAction == 1) ? 0.09f : 0.12f);
				}
				Vector3 localPosition = value.localPosition;
				((Vector3)(ref localPosition))._002Ector(localPosition.x, num, localPosition.z);
				value.localPosition = localPosition;
				value2 = movementAction;
				val2.Value = flag;
				val.Value = flag2 || flag3;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TinyEffectLocalCameraHandlerPatch error: " + ex));
		}
	}
}
