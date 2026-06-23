using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Potion), "AnimationStarted")]
internal class PotionEnsureDrinkingAnimationPatch
{
	private static void Prefix(Potion __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)__instance).Owner);
		if (!((Object)(object)player == (Object)null))
		{
			player.PlayerAnimations.ResetNonLoopAnimation();
		}
	}
}
