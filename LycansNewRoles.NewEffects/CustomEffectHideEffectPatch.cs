using System;
using HarmonyLib;

namespace LycansNewRoles.NewEffects;

[HarmonyPatch(typeof(GameUI), "AddEffectDisplay")]
internal class CustomEffectHideEffectPatch
{
	private static bool Prefix(Effect effect)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Invalid comparison between Unknown and I4
		try
		{
			if (effect is CustomEffect customEffect)
			{
				switch (customEffect.DisplayType)
				{
				case CustomEffect.DisplayPerRoleType.Everyone:
					return true;
				case CustomEffect.DisplayPerRoleType.WolvesOnly:
					return (int)PlayerController.Local.LocalCameraHandler.PovPlayer.Role == 1;
				case CustomEffect.DisplayPerRoleType.Nobody:
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CustomEffectHideEffectPatch error: " + ex));
			return true;
		}
	}
}
