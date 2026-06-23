using Fusion;
using HarmonyLib;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(CharacterInputHandler), "Update")]
internal class DraftPreventMovementPatch
{
	private static bool Prefix(CharacterInputHandler __instance)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)DraftManager.Instance != (Object)null && NetworkBool.op_Implicit(DraftManager.Instance.Active))
		{
			return false;
		}
		if ((int)GameManager.LocalGameState == 2 && NetworkBool.op_Implicit(PlayerCustom.Local.Petrified))
		{
			Traverse.Create((object)__instance).Field<LocalCameraHandler>("_localCameraHandler").Value.SetViewInputVector(new Vector2(0f, 0f));
			return false;
		}
		return true;
	}

	private static void Postfix(CharacterInputHandler __instance)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(PlayerCustom.Local.Confused))
		{
			NetworkInputData value = Traverse.Create((object)__instance).Field<NetworkInputData>("_networkInputData").Value;
			value.movementInput = new Vector2(PlayerCustom.Local.PersonalComponent.ConfusedSidesInverted ? (0f - InputManager.Instance.MoveInput.x) : InputManager.Instance.MoveInput.x, PlayerCustom.Local.PersonalComponent.ConfusedForwardInverted ? (0f - InputManager.Instance.MoveInput.y) : InputManager.Instance.MoveInput.y);
			Traverse.Create((object)__instance).Field("_networkInputData").SetValue((object)value);
			Traverse.Create((object)__instance).Field<LocalCameraHandler>("_localCameraHandler").Value.SetViewInputVector(new Vector2(PlayerCustom.Local.PersonalComponent.ConfusedRotationHorizontalInverted ? (0f - InputManager.Instance.LookInput.x) : InputManager.Instance.LookInput.x, PlayerCustom.Local.PersonalComponent.ConfusedRotationVerticalInverted ? (0f - InputManager.Instance.LookInput.y) : InputManager.Instance.LookInput.y));
		}
	}
}
