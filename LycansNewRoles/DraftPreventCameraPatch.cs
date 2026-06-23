using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LocalCameraHandler), "LateUpdate")]
internal class DraftPreventCameraPatch
{
	private static bool Prefix()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)DraftManager.Instance != (Object)null && NetworkBool.op_Implicit(DraftManager.Instance.Active))
		{
			return false;
		}
		return true;
	}
}
