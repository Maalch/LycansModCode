using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdatePotions")]
internal class DraftUpdatePotionsPatch
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
