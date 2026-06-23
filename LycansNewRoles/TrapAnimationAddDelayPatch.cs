using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(TrapItem), "Spawned")]
internal class TrapAnimationAddDelayPatch
{
	private static bool Prefix(TrapItem __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBool.op_Implicit(Plugin.CustomConfig.TrapsModified))
			{
				return true;
			}
			Traverse.Create((object)__instance).Field("triggerDelay").SetValue((object)1f);
			Traverse.Create((object)__instance).Field("animationDuration").SetValue((object)1f);
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TrapAnimationAddDelayPatch error: " + ex));
			return true;
		}
	}
}
