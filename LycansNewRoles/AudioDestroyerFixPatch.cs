using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(AudioDestroyer), "Update")]
internal class AudioDestroyerFixPatch
{
	private static bool Prefix(AudioDestroyer __instance)
	{
		Traverse<AudioSource> val = Traverse.Create((object)__instance).Field<AudioSource>("src");
		if ((Object)(object)val.Value == (Object)null)
		{
			val.Value = ((Component)__instance).GetComponent<AudioSource>();
			if ((Object)(object)val.Value == (Object)null)
			{
				Plugin.Logger.LogInfo((object)"AudioDestroyer: source missing");
				Object.Destroy((Object)(object)((Component)__instance).gameObject);
				return false;
			}
		}
		if ((Object)(object)val.Value.clip == (Object)null)
		{
			Plugin.Logger.LogInfo((object)"AudioDestroyer: source clip missing");
			Object.Destroy((Object)(object)((Component)__instance).gameObject);
			return false;
		}
		if (val.Value.timeSamples == val.Value.clip.samples || !val.Value.isPlaying)
		{
			Object.Destroy((Object)(object)((Component)__instance).gameObject);
		}
		return false;
	}
}
