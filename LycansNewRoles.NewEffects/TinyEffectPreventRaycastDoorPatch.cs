using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles.NewEffects;

[HarmonyPatch(typeof(PlayerController), "CheckDoorRayCast")]
internal class TinyEffectPreventRaycastDoorPatch
{
	private static bool Prefix(PlayerController __instance, ref bool __result)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.Tiny) || NetworkBool.op_Implicit(player.Phasing))
			{
				__result = false;
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TinyEffectPreventRaycastLootPatch error: " + ex));
			return true;
		}
	}
}
