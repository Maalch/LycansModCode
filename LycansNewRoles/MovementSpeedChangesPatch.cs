using System;
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
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_082e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0851: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_089a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a68: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c94: Unknown result type (might be due to invalid IL or missing references)
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
				value.IsClimbing = NetworkBool.op_Implicit(false);
				value.UpdateIsMoving(false);
				Vector3 value3 = player.Knockback.Knockback.Value;
				((Vector3)(ref value3)).Normalize();
				val.Value = 0f;
				val2.Value = 0f;
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
					float num3 = (NetworkBool.op_Implicit(value.IsWolf) ? 9f : 3f);
					Vector3 val4 = ((Component)component).transform.forward * y + ((Component)component).transform.right * x;
					((Vector3)(ref val4)).Normalize();
					val.Value = Maths.Lerp(val.Value, x * ((NetworkCharacterControllerPrototypeCustom)component).maxSpeed * num3 * 8f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
					val2.Value = Maths.Lerp(val2.Value, y * ((NetworkCharacterControllerPrototypeCustom)component).maxSpeed * num3 * 8f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
					if (val4 != Vector3.zero)
					{
						((NetworkCharacterControllerPrototypeCustom)component).Move(val4, num3);
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
					float num4 = 3.5f;
					Vector3 val5 = ((Component)component2).transform.forward * y2 + ((Component)component2).transform.right * x2;
					((Vector3)(ref val5)).Normalize();
					val.Value = Maths.Lerp(val.Value, x2 * ((NetworkCharacterControllerPrototypeCustom)component2).maxSpeed * num4, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
					val2.Value = Maths.Lerp(val2.Value, y2 * ((NetworkCharacterControllerPrototypeCustom)component2).maxSpeed * num4, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
					if (val5 != Vector3.zero)
					{
						((NetworkCharacterControllerPrototypeCustom)component2).Move(val5, num4);
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
							float num5 = val3.movementInput.x;
							float num6 = val3.movementInput.y;
							if (!value.IsCanMove())
							{
								num5 = 0f;
								num6 = 0f;
							}
							if (val3.aimForwardVector != Vector3.zero)
							{
								((Component)__instance).transform.forward = val3.aimForwardVector;
							}
							Quaternion rotation4 = ((Component)__instance).transform.rotation;
							((Quaternion)(ref rotation4)).eulerAngles = new Vector3(0f, ((Quaternion)(ref rotation4)).eulerAngles.y, ((Quaternion)(ref rotation4)).eulerAngles.z);
							((Component)__instance).transform.rotation = rotation4;
							value.UpdateIsMoving(num5 != 0f || num6 != 0f);
							float num7 = 0.6f;
							float num8 = (NetworkBool.op_Implicit(value.PlayerEffectManager.BonusSpeed) ? 1.75f : 1f);
							num7 *= num8;
							Vector3 val6 = ((Component)__instance).transform.forward * num6 + ((Component)__instance).transform.right * num5;
							((Vector3)(ref val6)).Normalize();
							val.Value = Maths.Lerp(val.Value, num5 * value2.maxSpeed * num7 * 8f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
							val2.Value = Maths.Lerp(val2.Value, num6 * value2.maxSpeed * num7 * 8f, ((SimulationBehaviour)__instance).Runner.DeltaTime * 8.9f);
							value2.Move(val6, num7);
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
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MovementActionTinyChangePatch error: " + ex));
			return true;
		}
	}
}
