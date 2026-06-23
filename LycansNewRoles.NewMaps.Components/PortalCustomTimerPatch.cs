using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

[HarmonyPatch(typeof(Portal), "StartActivationTimer")]
[HarmonyPriority(0)]
internal class PortalCustomTimerPatch
{
	private static bool Prefix(Portal __instance)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).HasStateAuthority)
			{
				PortalCustom component = ((Component)__instance).GetComponent<PortalCustom>();
				if ((Object)(object)component == (Object)null)
				{
					return true;
				}
				Traverse<NetworkBool> val = Traverse.Create((object)__instance).Field<NetworkBool>("_Active");
				((NetworkBehaviour)__instance).CopyStateToBackingFields();
				if (NetworkBool.op_Implicit(val.Value))
				{
					__instance.ActivationTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, Random.Range(component.ActiveMinimumSeconds, component.ActiveMaximumSeconds));
				}
				else
				{
					__instance.ActivationTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, Random.Range(component.InactiveMinimumSeconds, component.InactiveMaximumSeconds));
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PortalCustomTimerPatch error: " + ex));
			return true;
		}
	}
}
