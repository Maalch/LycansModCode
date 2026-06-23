using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(AudioManager), "PlayAndFollow")]
internal class AudioManagerFootstepsPatch
{
	private static void Prefix(string clip, ref float volume)
	{
		switch (clip)
		{
		case "WOOD_A":
		case "WOOD_B":
		case "FOOTSTEP_1":
		case "FOOTSTEP_2":
		case "FOOTSTEP_STONE":
		case "FOOTSTEP_WOOD":
			volume *= BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
			break;
		}
	}
}
