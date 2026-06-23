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
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!(__instance is Potion) && !(__instance is CustomItem) && !(__instance is LockItem) && !(__instance is KeyItem))
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
