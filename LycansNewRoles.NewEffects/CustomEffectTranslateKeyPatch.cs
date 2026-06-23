using System;
using HarmonyLib;

namespace LycansNewRoles.NewEffects;

[HarmonyPatch(typeof(Effect), "GetTranslateKey")]
internal class CustomEffectTranslateKeyPatch
{
	private static bool Prefix(Effect __instance, ref string __result)
	{
		try
		{
			if (__instance is CustomEffect customEffect)
			{
				__result = customEffect.TranslateKey;
				return false;
			}
			if (__instance is NightVision)
			{
				__result = "EFFECT_TRUESIGHT";
				return false;
			}
			if (__instance is AuditionEffect)
			{
				__result = "EFFECT_AUDITION_PLUS";
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CustomEffectTranslateKeyPatch error: " + ex));
			return true;
		}
	}
}
