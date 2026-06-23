using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(AudioManager), "Play")]
internal class DraftAudioManagerPatch
{
	private static bool Prefix(string clip)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (clip == "WOLF" || clip == "VILLAGER")
		{
			if (NetworkBool.op_Implicit(DraftManager.Instance.Active))
			{
				return false;
			}
			return true;
		}
		return true;
	}
}
