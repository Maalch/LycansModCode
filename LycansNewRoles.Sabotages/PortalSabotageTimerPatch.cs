using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles.Sabotages;

[HarmonyPatch(typeof(Portal), "StartActivationTimer")]
[HarmonyPriority(800)]
internal class PortalSabotageTimerPatch
{
	private static bool Prefix(Portal __instance)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).HasStateAuthority)
			{
				if (!SabotageManager.Instance.IsSabotageActive(SabotageManager.SabotageIds.Portals))
				{
					return true;
				}
				Traverse<NetworkBool> val = Traverse.Create((object)__instance).Field<NetworkBool>("_Active");
				((NetworkBehaviour)__instance).CopyStateToBackingFields();
				if (NetworkBool.op_Implicit(val.Value))
				{
					__instance.ActivationTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 120f);
				}
				else
				{
					__instance.ActivationTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 10f);
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PortalSabotageTimerPatch error: " + ex));
			return true;
		}
	}
}
