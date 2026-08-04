using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewMaps;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(CharacterMovementHandler), "FixedUpdateNetwork")]
internal class MovementSpeedChangesPatch
{
	internal enum Buttons
	{
		PrimaryInteract,
		SecondaryInteract,
		PrimaryAction,
		SecondaryAction,
		Crouch,
		Item
	}

	private static bool Prefix(CharacterMovementHandler __instance)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_096e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_12eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1271: Unknown result type (might be due to invalid IL or missing references)
		//IL_1276: Unknown result type (might be due to invalid IL or missing references)
		//IL_127a: Unknown result type (might be due to invalid IL or missing references)
		//IL_128c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1291: Unknown result type (might be due to invalid IL or missing references)
		//IL_1295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d23: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_101e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1023: Unknown result type (might be due to invalid IL or missing references)
		//IL_102e: Unknown result type (might be due to invalid IL or missing references)
		//IL_103a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1044: Unknown result type (might be due to invalid IL or missing references)
		//IL_1055: Unknown result type (might be due to invalid IL or missing references)
		//IL_100a: Unknown result type (might be due to invalid IL or missing references)
		//IL_100c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1114: Unknown result type (might be due to invalid IL or missing references)
		//IL_124b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1252: Unknown result type (might be due to invalid IL or missing references)
		//IL_1131: Unknown result type (might be due to invalid IL or missing references)
		//IL_1138: Unknown result type (might be due to invalid IL or missing references)
		//IL_1143: Unknown result type (might be due to invalid IL or missing references)
		//IL_114a: Unknown result type (might be due to invalid IL or missing references)
		//IL_114f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1154: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController value = Traverse.Create((object)__instance).Field<PlayerController>("_playerController").Value;
			if ((Object)(object)value == (Object)null)
			{
				return false;
			}
			if ((Object)(object)DraftManager.Instance == (Object)null)
			{
				return false;
			}
			if ((Object)(object)((SimulationBehaviour)__instance).Runner == (Object)null)
			{
				return false;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(value.Ref);
			if (NetworkBool.op_Implicit(DraftManager.Instance.Active))
			{
				return false;
			}
			if (NetworkBool.op_Implicit(player.Kidnapped))
			{
				return false;
			}
			Traverse<float> val = Traverse.Create((object)__instance).Field<float>("_xVelocity");
			Traverse<float> val2 = Traverse.Create((object)__instance).Field<float>("_yVelocity");
			NetworkCharacterControllerPrototypeCustom value2 = Traverse.Create((object)__instance).Field<NetworkCharacterControllerPrototypeCustom>("_networkCharacterControllerPrototypeCustom").Value;
			NetworkTeleportData teleportData;
			if (NetworkBool.op_Implicit(player.Petrified))
			{
				value.IsClimbing = NetworkBool.op_Implicit(false);
				value.UpdateIsMoving(false);
				val.Value = 0f;
				val2.Value = 0f;
				value.UpdateAnimation(Animator.StringToHash("X_Velocity"), 0f);
				value.UpdateAnimation(Animator.StringToHash("Y_Velocity"), 0f);
				teleportData = value.CharacterMovementHandler.TeleportData;
				if (!((NetworkTeleportData)(ref teleportData)).IsNone)
				{
					Transform transform = ((Component)value).transform;
					teleportData = value.CharacterMovementHandler.TeleportData;
					transform.position = ((NetworkTeleportData)(ref teleportData)).Position;
					Transform transform2 = ((Component)value).transform;
					teleportData = value.CharacterMovementHandler.TeleportData;
					transform2.rotation = ((NetworkTeleportData)(ref teleportData)).Rotation;
					if (((SimulationBehaviour)__instance).HasStateAuthority)
					{
						teleportData = value.CharacterMovementHandler.TeleportData;
						if (((NetworkTeleportData)(ref teleportData)).ResetLook)
						{
							value.Rpc_UpdateRotation();
						}
					}
					value.CharacterMovementHandler.TeleportData = NetworkTeleportData.None;
				}
				return false;
			}
			NetworkInputData val3 = default(NetworkInputData);
			if ((Object)(object)player.Knockback != (Object)null && player.Knockback.Knockback.HasValue)
			{
				float num = Mathf.Abs(player.Knockback.Knockback.Value.x);
				float num2 = Mathf.Abs(player.Knockback.Knockback.Value.z);
				float num3 = Mathf.Abs(player.Knockback.Knockback.Value.y);
				value.IsClimbing = NetworkBool.op_Implicit(false);
				value.UpdateIsMoving(false);
				Vector3 value3 = player.Knockback.Knockback.Value;
				val.Value = 0f;
				val2.Value = 0f;
				if (NetworkBool.op_Implicit(player.Jump))
				{
					float num4 = num + num2 + num3;
					float deltaTime = ((SimulationBehaviour)player).Runner.DeltaTime;
					Vector3 position = ((Component)value).transform.position;
					Vector3 velocity = value2.Velocity;
					value3.y += value2.gravity;
					value2.Controller.Move(value3 * deltaTime);
				}
				else
				{
					((Vector3)(ref value3)).Normalize();
					value2.Move(value3, num + num2);
					value.UpdateAnimation(Animator.StringToHash("X_Velocity"), val.Value);
					value.UpdateAnimation(Animator.StringToHash("Y_Velocity"), val2.Value);
					if (((NetworkBehaviour)__instance).GetInput<NetworkInputData>(ref val3))
					{
						if (val3.aimForwardVector != Vector3.zero)
						{
							((Component)__instance).transform.forward = val3.aimForwardVector;
						}
						Quaternion rotation = ((Component)__instance).transform.rotation;
						((Quaternion)(ref rotation)).eulerAngles = new Vector3(0f, ((Quaternion)(ref rotation)).eulerAngles.y, ((Quaternion)(ref rotation)).eulerAngles.z);
						((Component)__instance).transform.rotation = rotation;
					}
				}
				return false;
			}
			if (((SimulationBehaviour)__instance).Runner.IsServer && !NetworkBool.op_Implicit(value.IsDead))
			{
				teleportData = __instance.TeleportData;
				if (!((NetworkTeleportData)(ref teleportData)).IsNone)
				{
					((Component)value).GetComponent<GravityComponent>().ResetGrounded();
				}
			}
			if ((Object)(object)player.AstralSpirit != (Object)null)
			{
				value.UpdateAnimation(Animator.StringToHash("X_Velocity"), 0f);
				value.UpdateAnimation(Animator.StringToHash("Y_Velocity"), 0f);
				teleportData = value.CharacterMovementHandler.TeleportData;
				if (!((NetworkTeleportData)(ref teleportData)).IsNone)
				{
					Transform transform3 = ((Component)value).transform;
					teleportData = value.CharacterMovementHandler.TeleportData;
					transform3.position = ((NetworkTeleportData)(ref teleportData)).Position;
					Transform transform4 = ((Component)value).transform;
					teleportData = value.CharacterMovementHandler.TeleportData;
					transform4.rotation = ((NetworkTeleportData)(ref teleportData)).Rotation;
					if (((SimulationBehaviour)__instance).HasStateAuthority)
					{
						teleportData = value.CharacterMovementHandler.TeleportData;
						if (((NetworkTeleportData)(ref teleportData)).ResetLook)
						{
							value.Rpc_UpdateRotation();
						}
					}
					value.CharacterMovementHandler.TeleportData = NetworkTeleportData.None;
				}
				if (!player.AstralSpirit.Movable)
				{
					return false;
				}
				if (((NetworkBehaviour)__instance).GetInput<NetworkInputData>(ref val3))
				{
					PlayerAstralSpiritNetworkCharacterController component = ((Component)player.AstralSpirit).GetComponent<PlayerAstralSpiritNetworkCharacterController>();
					float x = val3.movementInput.x;
					float y = val3.movementInput.y;
					if (val3.aimForwardVector != Vector3.zero)
					{
						((Component)component).transform.forward = val3.aimForwardVector;
					}
					Quaternion rotation2 = ((Component)component).transform.rotation;
					((Quaternion)(ref rotation2)).eulerAngles = new Vector3(0f, ((Quaternion)(ref rotation2)).eulerAngles.y, ((Quaternion)(ref rotation2)).eulerAngles.z);
					((Component)component).transform.rotation = rotation2;
					float num5 = (NetworkBool.op_Implicit(value.IsWolf) ? 9f : 3f);
					Vector3 val4 = ((Component)component).transform.forward * y + ((Component)component).transform.right * x;
					((Vector3)(ref val4)).Normalize();
					val.Value = Maths.Lerp(val.Value, x * ((NetworkCharacterControllerPrototypeCustom)component).maxSpeed * num5 * 8f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
					val2.Value = Maths.Lerp(val2.Value, y * ((NetworkCharacterControllerPrototypeCustom)component).maxSpeed * num5 * 8f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
					if (val4 != Vector3.zero)
					{
						((NetworkCharacterControllerPrototypeCustom)component).Move(val4, num5);
					}
				}
				return false;
			}
			if (PlayerController.Local.Ref == player.Ref && (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Angel || player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost || player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Specter) && LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(player.PlayerController.IsDead) && ((NetworkBehaviour)__instance).GetInput<NetworkInputData>(ref val3))
			{
				NetworkButtons pressed = ((NetworkButtons)(ref val3.buttons)).GetPressed(__instance.ButtonsPrevious);
				if (((NetworkButtons)(ref pressed)).IsSet<Buttons>(Buttons.Item))
				{
					PlayerCustom.PlayerPrimaryRolePower primaryRolePower = player.PrimaryRolePower;
					PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
					if (playerPrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Angel)
					{
						PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
						if (player.PrimaryRolePowerRemainingUses > 0 && UIManager.DeadRolePanel.CurrentPossibleAction != UIDeadRolePanel.PossibleAction.None)
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, player2.Index);
						}
					}
				}
			}
			if ((Object)(object)player.SummonedSpirit != (Object)null && NetworkBool.op_Implicit(player.SummonedSpirit.HasFocus))
			{
				value.UpdateAnimation(Animator.StringToHash("X_Velocity"), 0f);
				value.UpdateAnimation(Animator.StringToHash("Y_Velocity"), 0f);
				teleportData = __instance.TeleportData;
				if (!((NetworkTeleportData)(ref teleportData)).IsNone)
				{
					Transform transform5 = ((Component)player.SummonedSpirit).transform;
					teleportData = __instance.TeleportData;
					transform5.position = ((NetworkTeleportData)(ref teleportData)).Position;
					__instance.TeleportData = NetworkTeleportData.None;
					return false;
				}
				if (!player.SummonedSpirit.Movable)
				{
					return false;
				}
				if (((NetworkBehaviour)__instance).GetInput<NetworkInputData>(ref val3))
				{
					PlayerSummonedSpiritNetworkCharacterController component2 = ((Component)player.SummonedSpirit).GetComponent<PlayerSummonedSpiritNetworkCharacterController>();
					float x2 = val3.movementInput.x;
					float y2 = val3.movementInput.y;
					if (val3.aimForwardVector != Vector3.zero)
					{
						((Component)component2).transform.forward = val3.aimForwardVector;
					}
					Quaternion rotation3 = ((Component)component2).transform.rotation;
					((Quaternion)(ref rotation3)).eulerAngles = new Vector3(0f, ((Quaternion)(ref rotation3)).eulerAngles.y, ((Quaternion)(ref rotation3)).eulerAngles.z);
					((Component)component2).transform.rotation = rotation3;
					float num6 = 3.5f;
					Vector3 val5 = ((Component)component2).transform.forward * y2 + ((Component)component2).transform.right * x2;
					((Vector3)(ref val5)).Normalize();
					val.Value = Maths.Lerp(val.Value, x2 * ((NetworkCharacterControllerPrototypeCustom)component2).maxSpeed * num6, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
					val2.Value = Maths.Lerp(val2.Value, y2 * ((NetworkCharacterControllerPrototypeCustom)component2).maxSpeed * num6, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
					if (val5 != Vector3.zero)
					{
						((NetworkCharacterControllerPrototypeCustom)component2).Move(val5, num6);
					}
				}
				return false;
			}
			if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Possessor)
			{
				PlayerRef primaryRoleTargetRef = player.PrimaryRoleTargetRef;
				if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials && NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool))
				{
					value.UpdateAnimation(Animator.StringToHash("X_Velocity"), 0f);
					value.UpdateAnimation(Animator.StringToHash("Y_Velocity"), 0f);
					teleportData = value.CharacterMovementHandler.TeleportData;
					if (!((NetworkTeleportData)(ref teleportData)).IsNone)
					{
						Transform transform6 = ((Component)value).transform;
						teleportData = value.CharacterMovementHandler.TeleportData;
						transform6.position = ((NetworkTeleportData)(ref teleportData)).Position;
						Transform transform7 = ((Component)value).transform;
						teleportData = value.CharacterMovementHandler.TeleportData;
						transform7.rotation = ((NetworkTeleportData)(ref teleportData)).Rotation;
						if (((SimulationBehaviour)__instance).HasStateAuthority)
						{
							teleportData = value.CharacterMovementHandler.TeleportData;
							if (((NetworkTeleportData)(ref teleportData)).ResetLook)
							{
								value.Rpc_UpdateRotation();
							}
						}
						value.CharacterMovementHandler.TeleportData = NetworkTeleportData.None;
					}
					return false;
				}
			}
			if (NetworkBool.op_Implicit(player.Tiny))
			{
				if (((NetworkBehaviour)__instance).GetInput<NetworkInputData>(ref val3))
				{
					if (!NetworkBool.op_Implicit(value.IsDead))
					{
						teleportData = value.CharacterMovementHandler.TeleportData;
						if (((NetworkTeleportData)(ref teleportData)).IsNone)
						{
							float num7 = val3.movementInput.x;
							float num8 = val3.movementInput.y;
							if (!value.IsCanMove())
							{
								num7 = 0f;
								num8 = 0f;
							}
							if (val3.aimForwardVector != Vector3.zero)
							{
								((Component)__instance).transform.forward = val3.aimForwardVector;
							}
							Quaternion rotation4 = ((Component)__instance).transform.rotation;
							((Quaternion)(ref rotation4)).eulerAngles = new Vector3(0f, ((Quaternion)(ref rotation4)).eulerAngles.y, ((Quaternion)(ref rotation4)).eulerAngles.z);
							((Component)__instance).transform.rotation = rotation4;
							value.UpdateIsMoving(num7 != 0f || num8 != 0f);
							float num9 = 0.6f;
							float num10 = (NetworkBool.op_Implicit(value.PlayerEffectManager.BonusSpeed) ? 1.75f : 1f);
							num9 *= num10;
							Vector3 val6 = ((Component)__instance).transform.forward * num8 + ((Component)__instance).transform.right * num7;
							((Vector3)(ref val6)).Normalize();
							val.Value = Maths.Lerp(val.Value, num7 * value2.maxSpeed * num9 * 8f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
							val2.Value = Maths.Lerp(val2.Value, num8 * value2.maxSpeed * num9 * 8f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
							value2.Move(val6, num9);
							value.UpdateAnimation(Animator.StringToHash("X_Velocity"), val.Value);
							value.UpdateAnimation(Animator.StringToHash("Y_Velocity"), val2.Value);
						}
						else
						{
							Transform transform8 = ((Component)value).transform;
							teleportData = value.CharacterMovementHandler.TeleportData;
							transform8.position = ((NetworkTeleportData)(ref teleportData)).Position;
							Transform transform9 = ((Component)value).transform;
							teleportData = value.CharacterMovementHandler.TeleportData;
							transform9.rotation = ((NetworkTeleportData)(ref teleportData)).Rotation;
							if (((SimulationBehaviour)__instance).HasStateAuthority)
							{
								teleportData = value.CharacterMovementHandler.TeleportData;
								if (((NetworkTeleportData)(ref teleportData)).ResetLook)
								{
									value.Rpc_UpdateRotation();
								}
							}
							value.CharacterMovementHandler.TeleportData = NetworkTeleportData.None;
						}
						value.UpdateAnchorRotation(val3.aimForwardVector);
						return false;
					}
					value2.Move(Vector3.zero, 1f);
				}
				return false;
			}
			if (((NetworkBehaviour)__instance).GetInput<NetworkInputData>(ref val3))
			{
				NetworkButtons pressed2 = ((NetworkButtons)(ref val3.buttons)).GetPressed(__instance.ButtonsPrevious);
				bool flag = ((NetworkButtons)(ref pressed2)).IsSet<Buttons>(Buttons.PrimaryInteract);
				bool flag2 = ((NetworkButtons)(ref pressed2)).IsSet<Buttons>(Buttons.SecondaryInteract);
				bool flag3 = ((NetworkButtons)(ref pressed2)).IsSet<Buttons>(Buttons.PrimaryAction);
				bool flag4 = ((NetworkButtons)(ref pressed2)).IsSet<Buttons>(Buttons.SecondaryAction);
				bool flag5 = ((NetworkButtons)(ref pressed2)).IsSet<Buttons>(Buttons.Item);
				Traverse.Create((object)__instance).Field<bool>("_sprinting").Value = NetworkBool.op_Implicit(val3.sprinting);
				bool flag6 = ((NetworkButtons)(ref val3.buttons)).WasPressed<Buttons>(__instance.ButtonsPrevious, Buttons.Crouch);
				if (flag || flag2)
				{
					value.InteractInput(flag);
				}
				if (flag3 || flag4)
				{
					value.ActionInput(flag3);
				}
				if (flag5)
				{
					value.UseItem();
				}
				__instance.ButtonsPrevious = val3.buttons;
				if (!NetworkBool.op_Implicit(value.IsDead))
				{
					teleportData = __instance.TeleportData;
					if (((NetworkTeleportData)(ref teleportData)).IsNone)
					{
						float num11 = val3.movementInput.x;
						float num12 = val3.movementInput.y;
						if (!value.IsCanMove())
						{
							num11 = 0f;
							num12 = 0f;
						}
						else
						{
							Traverse.Create((object)__instance).Method("UpdateMovementAction", new List<Type> { typeof(bool) }.ToArray(), (object[])null).GetValue(new object[1] { flag6 });
						}
						if (NetworkBool.op_Implicit(value.IsClimbing))
						{
							num11 = 0f;
						}
						if ((!NetworkBool.op_Implicit(value.IsVoting) || NetworkBool.op_Implicit(Plugin.CustomConfig.AnonymousVotes)) && !NetworkBool.op_Implicit(value.IsClimbing) && val3.aimForwardVector != Vector3.zero)
						{
							((Component)__instance).transform.forward = val3.aimForwardVector;
						}
						Quaternion rotation5 = ((Component)__instance).transform.rotation;
						((Quaternion)(ref rotation5)).eulerAngles = new Vector3(0f, ((Quaternion)(ref rotation5)).eulerAngles.y, ((Quaternion)(ref rotation5)).eulerAngles.z);
						((Component)__instance).transform.rotation = rotation5;
						value.UpdateIsMoving(num11 != 0f || num12 != 0f);
						bool flag7 = value.MovementAction == 2;
						float num13 = 2f;
						if (flag7)
						{
							num13 = 3.5f;
						}
						else if (value.MovementAction == 1)
						{
							num13 = 1f;
						}
						if (NetworkBool.op_Implicit(value.IsWolf))
						{
							float num14 = 1f + (float)GameManager.Instance.WolfSpeed / 100f;
							num13 *= num14;
						}
						float num15 = (NetworkBool.op_Implicit(value.PlayerEffectManager.BonusSpeed) ? 1.75f : 1f);
						num13 *= num15;
						if (!NetworkBool.op_Implicit(value.IsClimbing))
						{
							Vector3 val7 = ((Component)__instance).transform.forward * num12 + ((Component)__instance).transform.right * num11;
							((Vector3)(ref val7)).Normalize();
							val.Value = Maths.Lerp(val.Value, num11 * value2.maxSpeed * num13, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
							val2.Value = Maths.Lerp(val2.Value, num12 * value2.maxSpeed * num13, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
							value2.Move(val7, num13);
							value.UpdateAnimation(Animator.StringToHash("X_Velocity"), val.Value);
							value.UpdateAnimation(Animator.StringToHash("Y_Velocity"), val2.Value);
						}
						else
						{
							val2.Value = Maths.Lerp(val2.Value, num12 * value2.maxSpeed * num13 * 0.25f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
							value.UpdateAnimation(Animator.StringToHash("Y_Velocity"), val2.Value);
							value2.Climb(Vector3.up * num12, num13 * 0.25f);
						}
					}
					else
					{
						Transform transform10 = ((Component)value).transform;
						teleportData = __instance.TeleportData;
						transform10.position = ((NetworkTeleportData)(ref teleportData)).Position;
						Transform transform11 = ((Component)value).transform;
						teleportData = __instance.TeleportData;
						transform11.rotation = ((NetworkTeleportData)(ref teleportData)).Rotation;
						if (((SimulationBehaviour)__instance).HasStateAuthority)
						{
							teleportData = __instance.TeleportData;
							if (((NetworkTeleportData)(ref teleportData)).ResetLook)
							{
								value.Rpc_UpdateRotation();
							}
						}
						__instance.TeleportData = NetworkTeleportData.None;
					}
					value.UpdateAnchorRotation(val3.aimForwardVector);
					return false;
				}
				value2.Move(Vector3.zero, 1f);
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MovementActionTinyChangePatch error: " + ex));
			return true;
		}
	}
}
