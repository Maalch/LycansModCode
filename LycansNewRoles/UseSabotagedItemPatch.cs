using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Item), "Use")]
internal class UseSabotagedItemPatch
{
	private static bool Prefix(Item __instance)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (__instance.ItemQuantity > 0)
			{
				TickTimer val = __instance.ItemTimer;
				if (!((TickTimer)(ref val)).IsRunning)
				{
					val = __instance.AnimationTimer;
					if (!((TickTimer)(ref val)).IsRunning)
					{
						val = __instance.TriggerTimer;
						if (!((TickTimer)(ref val)).IsRunning && (bool)Traverse.Create((object)__instance).Method("CanUseItem", Array.Empty<object>()).GetValue())
						{
							ItemCustom componentInChildren = ((Component)__instance).GetComponentInChildren<ItemCustom>();
							if (NetworkBool.op_Implicit(componentInChildren.Sabotaged) && !(__instance is Potion))
							{
								if (!((SimulationBehaviour)__instance).HasInputAuthority)
								{
									return false;
								}
								PlayerCustom.Rpc_Use_Sabotaged_Item(((SimulationBehaviour)__instance).Runner, PlayerCustomRegistry.GetPlayer(__instance.Owner).Index);
								return false;
							}
						}
					}
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UseSabotagedItemPatch prefix error: " + ex));
			return true;
		}
	}
}
