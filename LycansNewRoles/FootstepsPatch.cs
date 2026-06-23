using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems.Accessories;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "PlayFootstepSound")]
internal class FootstepsPatch
{
	private static bool Prefix(PlayerController __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)GameManager.LocalGameState != 2)
			{
				return false;
			}
			if (NetworkBool.op_Implicit(__instance.IsMoving) && (Object)(object)PlayerController.Local != (Object)null)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				if (NetworkBool.op_Implicit(player.Tiny) || NetworkBool.op_Implicit(player.Phasing) || NetworkBool.op_Implicit(player.Sneaky) || player.CamouflageLevelForPovPlayer > 0 || (NetworkBool.op_Implicit(player.Isolation) && !NetworkBool.op_Implicit(__instance.IsWolf) && !player.IsCurrentlyPlayedOrObserved && player2.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie) || (NetworkBool.op_Implicit(player2.Isolation) && !NetworkBool.op_Implicit(player.PlayerController.IsWolf) && !player.IsCurrentlyPlayedOrObserved) || (player.Accessory is AccessoryBoots && !NetworkBool.op_Implicit(player.PlayerController.IsWolf)) || (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Spotter && NetworkBool.op_Implicit(player2.PlayerController.IsWolf)) || (NetworkBool.op_Implicit(player.Hidden) && NetworkBool.op_Implicit(player2.PlayerController.IsWolf)))
				{
					return false;
				}
			}
			Traverse<bool> val = Traverse.Create((object)__instance).Field<bool>("_isPlayingFootstep");
			if (!val.Value && NetworkBool.op_Implicit(__instance.IsMoving) && (Object)(object)PlayerController.Local != (Object)null)
			{
				PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
				float num = Vector3.Distance(((Component)povPlayer).transform.position, ((Component)__instance).transform.position);
				if (povPlayer.Ref == PlayerCustom.Local.Ref && (Object)(object)PlayerCustom.Local.AstralSpirit != (Object)null)
				{
					num = Vector3.Distance(((Component)PlayerCustom.Local.AstralSpirit).transform.position, ((Component)__instance).transform.position);
				}
				if (povPlayer.Ref == PlayerCustom.Local.Ref && (Object)(object)PlayerCustom.Local.SummonedSpirit != (Object)null && NetworkBool.op_Implicit(PlayerCustom.Local.SummonedSpirit.HasFocus))
				{
					num = Vector3.Distance(((Component)PlayerCustom.Local.SummonedSpirit).transform.position, ((Component)__instance).transform.position);
				}
				if (NetworkBool.op_Implicit(__instance.IsClimbing))
				{
					float num2 = 16f;
					float num3 = 0.1f;
					float num4 = 0.5f;
					if (__instance.MovementAction == 2)
					{
						num2 = 20f;
						num3 = 0.2f;
						num4 = 0.3f;
					}
					if (NetworkBool.op_Implicit(__instance.PlayerEffectManager.BonusSpeed))
					{
						num4 /= 1.25f;
					}
					if (num < num2)
					{
						Traverse<bool> val2 = Traverse.Create((object)__instance).Field<bool>("_footstepRhythm");
						AudioManager.PlayAndFollow(val2.Value ? "WOOD_A" : "WOOD_B", ((Component)__instance).transform, (MixerTarget)2, num2, num3);
						val.Value = true;
						val2.Value = !val2.Value;
						((MonoBehaviour)__instance).StartCoroutine("ResetFootstepStatus", (object)num4);
						return false;
					}
				}
				else if (__instance.MovementAction != 1)
				{
					float num5 = 24f;
					float num6 = 0.4f;
					float num7 = 0.3f;
					if (__instance.MovementAction == 0)
					{
						num5 = 12f;
						num6 = 0.4f;
						num7 = 0.5f;
					}
					if (NetworkBool.op_Implicit(__instance.PlayerEffectManager.BonusSpeed))
					{
						num7 /= 1.25f;
					}
					if ((Object)(object)povPlayer == (Object)(object)__instance)
					{
						num6 /= 6f;
					}
					if (num < num5)
					{
						Traverse<bool> val3 = Traverse.Create((object)__instance).Field<bool>("_footstepRhythm");
						Traverse<byte> val4 = Traverse.Create((object)__instance).Field<byte>("_footstepSound");
						string text = "FOOTSTEP";
						if (val4.Value == 2)
						{
							text += "_STONE";
						}
						else if (val4.Value == 1)
						{
							text += "_WOOD";
						}
						text = ((!val3.Value) ? (text + "_2") : (text + "_1"));
						AudioManager.PlayAndFollow(text, ((Component)__instance).transform, (MixerTarget)2, num5, num6);
						val.Value = true;
						val3.Value = !val3.Value;
						((MonoBehaviour)__instance).StartCoroutine("ResetFootstepStatus", (object)num7);
					}
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("FootstepsPatch error: " + ex));
			return true;
		}
	}
}
