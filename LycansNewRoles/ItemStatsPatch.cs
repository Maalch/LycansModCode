using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems;
using LycansNewRoles.Stats;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Item), "Use")]
public class ItemStatsPatch
{
	private static bool Prefix(Item __instance)
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!(__instance is Potion) && !(__instance is CustomItem) && !(__instance is LockItem) && !(__instance is KeyItem) && !(__instance is SpyglassItem))
			{
				PlayerRef owner = __instance.Owner;
				if (!((PlayerRef)(ref owner)).IsNone && PlayerCustomRegistry.HasPlayer(__instance.Owner) && __instance.ItemQuantity > 0)
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
								PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Owner);
								if (player.Stats != null)
								{
									LycansUtility.AddLogOnlyForMe("Add use item stat from ItemStatsPatch: " + ItemUtility.ItemToTranslateKey(__instance));
									player.Stats.AddAction(new PlayerStats.PlayerAction
									{
										ActionType = "UseGadget",
										ActionName = TranslationManager.Instance.GetTranslation(ItemUtility.ItemToTranslateKey(__instance))
									}, ((Component)player.PlayerController).transform.position);
								}
							}
						}
					}
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogInfo((object)("ItemStatsPatch error: " + ex));
			return true;
		}
	}
}
