using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Rpc_UpdateTimer")]
internal class PreventRpcUpdateTimerPatch
{
	private static bool Prefix()
	{
		return false;
	}
}
