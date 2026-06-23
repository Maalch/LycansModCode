using System;
using Fusion;
using HarmonyLib;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateRayCast")]
internal class TrapDisarmTooltipPatch
{
	private static void Postfix(GameObject raycastTargetObject, float distance, PlayerController __instance)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Invalid comparison between Unknown and I4
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		Trap trap = default(Trap);
		if (((SimulationBehaviour)__instance).HasInputAuthority && !NetworkBool.op_Implicit(__instance.IsAiming) && !NetworkBool.op_Implicit(__instance.IsZooming) && NetworkBool.op_Implicit(__instance.CanMoveAnimation) && distance < 2.5f && (int)GameManager.LocalGameState == 2 && NetworkBool.op_Implicit(Plugin.CustomConfig.TrapsModified) && raycastTargetObject.TryGetComponent<Trap>(ref trap) && trap.TrapCanBeDisarmed())
		{
			GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_DISARM", Color.white, (InputActionName)3, Array.Empty<object>());
			Traverse.Create((object)__instance).Field("_targetObject").SetValue((object)raycastTargetObject);
			GameManager.Instance.gameUI.ChangeCrossHairOpacity(NetworkBool.op_Implicit(__instance.IsAiming) ? 0.1f : 1f);
		}
	}
}
