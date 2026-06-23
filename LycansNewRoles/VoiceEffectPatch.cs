using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(VoiceSpeaker), "Update")]
internal class VoiceEffectPatch
{
	[HarmonyPatch(typeof(GameUI), "UpdatePlayerTalking")]
	private class PreventTalkingDisplayPatch
	{
		private static void Prefix(PlayerRef playerRef, ref bool talking)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Invalid comparison between Unknown and I4
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			if ((int)GameManager.LocalGameState == 0 || (int)GameManager.LocalGameState == 1)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerRef);
			if (talking)
			{
				if (NetworkBool.op_Implicit(player.Downed) || NetworkBool.op_Implicit(player.Mute) || NetworkBool.op_Implicit(player.Petrified))
				{
					talking = false;
				}
				if (playerRef != PlayerController.Local.Ref && (NetworkBool.op_Implicit(player.PlayerController.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(player.Camouflage) || !player.CanBeHeardByObservedPlayer() || NetworkBool.op_Implicit(player.PlayerController.IsWolf)))
				{
					talking = false;
				}
			}
		}
	}

	[HarmonyPatch(typeof(PlayerController), "Rpc_UpdateTalking")]
	private class TalkingStatsPatch
	{
		private static void Postfix(PlayerController __instance, NetworkBool talking)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			if (((SimulationBehaviour)__instance).HasStateAuthority)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
				if (player.Stats != null)
				{
					player.Stats.OnTalkingChanged(NetworkBool.op_Implicit(talking) && !NetworkBool.op_Implicit(player.PlayerController.IsDead));
				}
			}
		}
	}

	[HarmonyPatch(typeof(VoiceManager), "JoinDeadChannel")]
	private class VoiceManagerJoinDeadChannelPatch
	{
		private static bool Prefix()
		{
			GameManager.Instance.gameUI.ShowJoinDeadChannel(false);
			return false;
		}
	}

	[HarmonyPatch(typeof(VoiceManager), "LeaveDeadChannel")]
	private class VoiceManagerLeaveDeadChannelPatch
	{
		private static bool Prefix()
		{
			GameManager.Instance.gameUI.ShowJoinDeadChannel(true);
			return false;
		}
	}

	private static void Postfix(VoiceSpeaker __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)GameManager.LocalGameState == 0)
			{
				return;
			}
			PlayerController value = Traverse.Create((object)__instance).Field<PlayerController>("_playerController").Value;
			if ((Object)(object)value == (Object)(object)PlayerController.Local)
			{
				return;
			}
			AudioSource value2 = Traverse.Create((object)__instance).Field<AudioSource>("_audioSource").Value;
			if ((Object)(object)DraftManager.Instance != (Object)null && NetworkBool.op_Implicit(DraftManager.Instance.Active))
			{
				value2.spatialBlend = 0f;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(value.Ref);
			if (!player.CanBeHeardByObservedPlayer())
			{
				value2.maxDistance = 0f;
				value2.mute = true;
				return;
			}
			PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
			if (!((Object)(object)player != (Object)null) || !((Object)(object)povPlayer != (Object)null))
			{
				return;
			}
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(povPlayer.Ref);
			if (NetworkBool.op_Implicit(PlayerCustom.Local.Possessed))
			{
				value2.maxDistance = 0f;
				value2.mute = true;
				return;
			}
			AudioReverbPreset reverb = (AudioReverbPreset)0;
			if (NetworkBool.op_Implicit(player.Spotter))
			{
				reverb = (AudioReverbPreset)19;
				if (NetworkBool.op_Implicit(povPlayer.IsWolf))
				{
					value2.spatialBlend = 0f;
				}
			}
			if (NetworkBool.op_Implicit(player.Tiny))
			{
				float num = 7f;
				if ((Object)(object)povPlayer != (Object)null && NetworkBool.op_Implicit(povPlayer.PlayerEffectManager.Audition))
				{
					num *= 3f;
				}
				value2.maxDistance = num;
			}
			else if (NetworkBool.op_Implicit(player.Spotter))
			{
				float num2 = 70f;
				if (player.PlayerController.MovementAction == 1)
				{
					num2 = 5f;
				}
				if ((Object)(object)povPlayer != (Object)null && NetworkBool.op_Implicit(povPlayer.PlayerEffectManager.Audition))
				{
					num2 *= 3f;
				}
				value2.maxDistance = num2;
			}
			else if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && NetworkBool.op_Implicit(player.PlayerController.IsWolf))
			{
				float num3 = 40f;
				if ((Object)(object)povPlayer != (Object)null && NetworkBool.op_Implicit(povPlayer.PlayerEffectManager.Audition))
				{
					num3 *= 3f;
				}
				value2.maxDistance = num3;
			}
			if (NetworkBool.op_Implicit(value.IsDead))
			{
				return;
			}
			if (NetworkBool.op_Implicit(player.Kidnapped))
			{
				if (NetworkBool.op_Implicit(player2.Kidnapped))
				{
					value2.maxDistance = 500f;
					value2.mute = false;
					value2.spatialBlend = 0f;
					reverb = (AudioReverbPreset)23;
					player.CustomAudio.UpdateReverbIfNeeded(reverb);
				}
				else if (player2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper)
				{
					value2.maxDistance = 2000f;
					value2.mute = false;
					value2.spatialBlend = 0f;
					reverb = (AudioReverbPreset)23;
					player.CustomAudio.UpdateReverbIfNeeded(reverb);
				}
				else
				{
					value2.maxDistance = 0f;
					value2.mute = true;
				}
				return;
			}
			if (NetworkBool.op_Implicit(player.Resurrected) || NetworkBool.op_Implicit(player.Dying) || NetworkBool.op_Implicit(player.Asleep) || NetworkBool.op_Implicit(player.Downed) || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
			{
				value2.maxDistance = 0f;
				value2.mute = true;
				return;
			}
			if (NetworkBool.op_Implicit(player.Mute) || NetworkBool.op_Implicit(player.Phasing) || NetworkBool.op_Implicit(player.Petrified) || (NetworkBool.op_Implicit(player.Isolation) && !NetworkBool.op_Implicit(povPlayer.IsWolf) && player2.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie) || (NetworkBool.op_Implicit(player2.Isolation) && !NetworkBool.op_Implicit(value.IsWolf)))
			{
				value2.maxDistance = 0f;
				value2.mute = true;
			}
			if (player2.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTelepath && NetworkBool.op_Implicit(player2.SecondaryRolePowerActive))
			{
				value2.maxDistance = 0f;
				value2.mute = true;
			}
			if (player2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && NetworkBool.op_Implicit(player2.NewPrimaryRoleUniqueBool))
			{
				value2.maxDistance = 0f;
				value2.mute = true;
			}
			if ((Object)(object)player2.AstralSpirit != (Object)null)
			{
				reverb = (AudioReverbPreset)23;
			}
			else if (NetworkBool.op_Implicit(player.PlayerController.PlayerEffectManager.Giant))
			{
				reverb = (AudioReverbPreset)19;
			}
			else if (NetworkBool.op_Implicit(player.PlayerController.IsWolf))
			{
				reverb = (AudioReverbPreset)1;
			}
			if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTelepath && NetworkBool.op_Implicit(player.SecondaryRolePowerActive))
			{
				if (player2.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTelepath)
				{
					value2.maxDistance = 500f;
					value2.mute = false;
					reverb = (AudioReverbPreset)23;
				}
				else
				{
					value2.maxDistance = 0f;
					value2.mute = true;
				}
			}
			if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool))
			{
				if (player2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
				{
					value2.maxDistance = 500f;
					value2.mute = false;
					reverb = (AudioReverbPreset)23;
				}
				else
				{
					value2.maxDistance = 0f;
					value2.mute = true;
				}
			}
			if (value2.maxDistance > 0f)
			{
				value2.maxDistance *= BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
			}
			player.CustomAudio.UpdateReverbIfNeeded(reverb);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("VoiceEffectPatch error: " + ex));
		}
	}
}
