using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UseItem")]
internal class UseItemPatch
{
	private static bool Prefix(PlayerController __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(__instance.IsDead))
		{
			return false;
		}
		return true;
	}
}
