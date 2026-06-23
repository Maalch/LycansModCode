using System;
using HarmonyLib;

namespace LycansNewRoles.NewEffects;

[HarmonyPatch(typeof(Effect), "GetEffectType")]
internal class CustomEffectTypePatch
{
	private static bool Prefix(ref Effect __instance, ref EffectType __result)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected I4, but got Unknown
		try
		{
			if (__instance is CustomEffect customEffect)
			{
				__result = (EffectType)(int)customEffect.CustomEffectType;
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CustomEffectTypePatch error: " + ex));
			return true;
		}
	}
}
