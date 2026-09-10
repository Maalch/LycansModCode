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
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0940: Unknown result type (might be due to invalid IL or missing references)
		//IL_0956: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_126c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1271: Unknown result type (might be due to invalid IL or missing references)
		//IL_1275: Unknown result type (might be due to invalid IL or missing references)
		//IL_1287: Unknown result type (might be due to invalid IL or missing references)
		//IL_128c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1019: Unknown result type (might be due to invalid IL or missing references)
		//IL_101e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1029: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_103f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1050: Unknown result type (might be due to invalid IL or missing references)
		//IL_1005: Unknown result type (might be due to invalid IL or missing references)
		//IL_1007: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_110f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1246: Unknown result type (might be due to invalid IL or missing references)
		//IL_124d: Unknown result type (might be due to invalid IL or missing references)
		//IL_112c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1133: Unknown result type (might be due to invalid IL or missing references)
		//IL_113e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1145: Unknown result type (might be due to invalid IL or missing references)
		//IL_114a: Unknown result type (might be due to invalid IL or missing references)
		//IL_114f: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bb: Unknown result type (might be due to invalid IL or missing references)
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
			if (player.IsOutOfTheWorld)
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
