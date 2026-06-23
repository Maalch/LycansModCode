using System;
using HarmonyLib;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace LycansNewRoles.NewEffects;

[HarmonyPatch(typeof(EffectDisplay), "Init")]
internal class CustomEffectDisplayInit
{
	private static void Postfix(Effect effect, EffectDisplay __instance)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (effect is CustomEffect customEffect)
			{
				Image val = (Image)Traverse.Create((object)__instance).Field("effectFill").GetValue();
				((Graphic)val).color = customEffect.Color;
				LocalizeStringEvent val2 = (LocalizeStringEvent)Traverse.Create((object)__instance).Field("effectStringEvent").GetValue();
				((LocalizedReference)val2.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit(customEffect.TranslateKey));
			}
			else if (effect is NightVision || effect is AuditionEffect)
			{
				LocalizeStringEvent val3 = (LocalizeStringEvent)Traverse.Create((object)__instance).Field("effectStringEvent").GetValue();
				((LocalizedReference)val3.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit(effect.GetTranslateKey()));
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CustomEffectDisplayInit error: " + ex));
		}
	}
}
