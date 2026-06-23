using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "Update")]
internal class DraftPreventShowMenuPatch
{
	private static bool Prefix(GameUI __instance)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)DraftManager.Instance != (Object)null && NetworkBool.op_Implicit(DraftManager.Instance.Active))
		{
			Traverse.Create((object)__instance).Method("UpdateTalkingUI", Array.Empty<object>()).GetValue();
			return false;
		}
		return true;
	}
}
