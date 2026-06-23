using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerInteract), "Update")]
internal class LongerRaycastPatch
{
	private static bool Prefix(PlayerInteract __instance)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Invalid comparison between Unknown and I4
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Camera value = Traverse.Create((object)__instance).Field<Camera>("_camera").Value;
			PlayerController value2 = Traverse.Create((object)__instance).Field<PlayerController>("_playerController").Value;
			LayerMask value3 = Traverse.Create((object)__instance).Field<LayerMask>("layerMask").Value;
			LayerMask value4 = Traverse.Create((object)__instance).Field<LayerMask>("gunLayerMask").Value;
			if (!((Behaviour)value).enabled)
			{
				return false;
			}
			Transform transform = ((Component)value).transform;
			Vector3 position = transform.position;
			Vector3 forward = transform.forward;
			Ray val = default(Ray);
			((Ray)(ref val))._002Ector(position, forward);
			Ray val2 = default(Ray);
			((Ray)(ref val2))._002Ector(position, forward);
			float num = 10f;
			if ((int)GameManager.LocalGameState == 4)
			{
				num = 10f;
			}
			bool flag = true;
			RaycastHit val3 = default(RaycastHit);
			if (NetworkBool.op_Implicit(value2.IsWolf) && Physics.Raycast(val, ref val3) && (Object)(object)((Component)((RaycastHit)(ref val3)).collider).gameObject.GetComponent<MagicianIllusion>() != (Object)null)
			{
				value2.UpdateRayCast(((Component)((RaycastHit)(ref val3)).collider).gameObject, ((RaycastHit)(ref val3)).distance);
				flag = false;
			}
			if (flag)
			{
				if (Physics.Raycast(val, ref val3, num, LayerMask.op_Implicit(value3)))
				{
					value2.UpdateRayCast(((Component)((RaycastHit)(ref val3)).collider).gameObject, ((RaycastHit)(ref val3)).distance);
				}
				else
				{
					value2.ClearRayCast();
				}
			}
			RaycastHit val4 = default(RaycastHit);
			if (Physics.Raycast(val2, ref val4, 500f, LayerMask.op_Implicit(value4)))
			{
				value2.UpdateGunTarget(((Component)((RaycastHit)(ref val4)).collider).gameObject);
			}
			else
			{
				value2.ClearGunTarget();
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("LongerRaycastPatch error: " + ex));
			return true;
		}
	}
}
