using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerVolume), "UpdateVolumeAndSave")]
internal class PlayerVolumeUpdateVolumeAndSavePatch
{
	private static void Postfix(PlayerVolume __instance)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		VoiceSpeaker value = Traverse.Create((object)__instance).Field<VoiceSpeaker>("_voiceSpeaker").Value;
		PlayerController value2 = Traverse.Create((object)value).Field<PlayerController>("_playerController").Value;
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(value2.Ref);
		player.CustomAudio.UpdateVolume();
	}
}
