using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdateLoot")]
internal class DraftUpdateLootPatch
{
	private static bool Prefix()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(DraftManager.Instance.Active))
		{
			return false;
		}
		return true;
	}
}
