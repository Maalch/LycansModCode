using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Effect), "Despawned")]
internal class EffectDespawnedPatch
{
	private static void Postfix(Effect __instance)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerCustomRegistry.Instance == (Object)null)
		{
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.EffectPlayer);
		if (!(__instance is MidasEffect) && !(__instance is VampireEffect) && !(__instance is SpeedEffect) && !(__instance is HauntedEffect) && !(__instance is AsleepEffect) && !(__instance is SpiritResistanceEffect) && !(__instance is ResistanceEffect) && !(__instance is SneakyEffect) && !(__instance is TrackedEffect) && !(__instance is ClairvoyanceEffect) && !(__instance is PoisonEffect) && !(__instance is ConfusedEffect))
		{
			if (!(__instance is RepulsionEffect))
			{
				if (!(__instance is CapturedEffect) && !(__instance is BanishedEffect))
				{
					if (__instance is SpotterEffect && player.IsCurrentlyPlayedOrObserved)
					{
						foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf)))
						{
							item.UpdateVisibility();
							((Component)item.PlayerController).GetComponent<PlayerSpotterLightComponent>().UpdateState();
						}
					}
				}
				else
				{
					player.UpdateVisibility();
					player.UpdateCanMoveAnimation();
					if (player.IsCurrentlyPlayedOrObserved)
					{
						ColorAdjustmentManager.UpdateColorAdjustment();
					}
				}
			}
			else if (player.IsCurrentlyPlayedOrObserved)
			{
				ColorAdjustmentManager.UpdateColorAdjustment();
				player.UpdateTargetArrowComponent();
			}
		}
		else
		{
			player.UpdateVisibility();
		}
		if (!(__instance is StunnedEffect) && !(__instance is SprintEffect) && !(__instance is DisorientedEffect) && !(__instance is DiseasedEffect) && !(__instance is WoundedEffect) && !(__instance is EmpoweredEffect) && !(__instance is NauseatedEffect) && !(__instance is PanicEffect) && !(__instance is FleeingEffect) && !(__instance is SleepyEffect) && !(__instance is PredatorEffect) && !(__instance is PortalEffect) && !(__instance is EscapingEffect) && !(__instance is TenacityEffect) && !(__instance is HubrisEffect) && !(__instance is SneakyEffect) && !(__instance is RepulsionEffect))
		{
			if (__instance is BurningEffect || __instance is PurifierBurnEffect)
			{
				player.UpdateMoveSpeed();
				player.UpdateVisibility();
			}
		}
		else
		{
			player.UpdateMoveSpeed();
		}
	}
}
