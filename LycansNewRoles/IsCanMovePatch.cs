using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "IsCanMove")]
internal class IsCanMovePatch
{
	private static void Postfix(PlayerController __instance, ref bool __result)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.Downed))
			{
				__result = false;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("IsCanMovePatch error: " + ex));
		}
	}
}
