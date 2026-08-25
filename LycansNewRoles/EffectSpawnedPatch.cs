using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Effect), "Spawned")]
internal class EffectSpawnedPatch
{
	private static void Postfix(Effect __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.EffectPlayer);
		if (!(__instance is MidasEffect) && !(__instance is VampireEffect) && !(__instance is SpeedEffect) && !(__instance is HauntedEffect) && !(__instance is AsleepEffect) && !(__instance is SpiritResistanceEffect) && !(__instance is DetectedEffect) && !(__instance is ClairvoyanceEffect) && !(__instance is SneakyEffect) && !(__instance is PoisonEffect) && !(__instance is ConfusedEffect))
		{
			if (!(__instance is ResistanceEffect))
			{
				if (!(__instance is EnergizedEffect))
				{
					if (!(__instance is CapturedEffect) && !(__instance is BanishedEffect))
					{
						if (!(__instance is SpotterEffect))
						{
							if (!(__instance is MoleClockEffect))
							{
								if (__instance is JumpEffect)
								{
									((Component)player.PlayerController).gameObject.layer = 26;
								}
							}
							else if (player.IsCurrentlyPlayedOrObserved)
							{
								AudioManager.Play("BELL", (MixerTarget)2, 1f, 1f);
								ColorAdjustmentManager.FlashScreen(Color.red);
							}
						}
						else if (player.IsCurrentlyPlayedOrObserved)
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
						AudioManager.PlayPosition("Banish", ((Component)player.PlayerController).transform.position, (MixerTarget)2, 10f, 0.5f);
						if (player.IsCurrentlyPlayedOrObserved)
						{
							ColorAdjustmentManager.UpdateColorAdjustment();
						}
					}
				}
				else
				{
					PlayerCustom.PlayerPrimaryRolePower primaryRolePower = player.PrimaryRolePower;
					PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
					if (playerPrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Hunter && !NetworkBool.op_Implicit(player.PlayerController.IsGunLoaded) && ((SimulationBehaviour)__instance).Runner.IsServer)
					{
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)player.PlayerController).Runner, NetworkString<_16>.op_Implicit("RELOAD"), ((Component)player.PlayerController).transform.position, 8f, 0.5f);
						player.PlayerController.IsGunLoaded = NetworkBool.op_Implicit(true);
					}
				}
			}
			else
			{
				player.UpdateVisibility();
				player.PlayerAnimations.PlayNonLoopAnimation("Buff");
				if (NetworkBool.op_Implicit(player.PlayerController.IsWolf))
				{
					player.FlashPlayer(Color.green);
				}
			}
		}
		else
		{
			player.UpdateVisibility();
		}
		if (!(__instance is SprintEffect) && !(__instance is DisorientedEffect) && !(__instance is DiseasedEffect) && !(__instance is WoundedEffect) && !(__instance is EmpoweredEffect) && !(__instance is NauseatedEffect) && !(__instance is PanicEffect) && !(__instance is FleeingEffect) && !(__instance is SleepyEffect) && !(__instance is PredatorEffect) && !(__instance is PortalEffect) && !(__instance is EscapingEffect) && !(__instance is TenacityEffect) && !(__instance is HubrisEffect) && !(__instance is SneakyEffect) && !(__instance is StrenghtenedEffect))
		{
			if (!(__instance is BurningEffect) && !(__instance is PurifierBurnEffect))
			{
				if (__instance is ImmuneEffect)
				{
					PlayerEffectsManager playerEffectManager = player.PlayerController.PlayerEffectManager;
					foreach (NetworkId item2 in ((IEnumerable<NetworkId>)(object)playerEffectManager.ActiveEffects).ToList())
					{
						Effect val = ((SimulationBehaviour)playerEffectManager).Runner.TryGetNetworkedBehaviourFromNetworkedObjectRef<Effect>(item2);
						if ((Object)(object)val != (Object)null && (!(val is CustomEffect) || (val as CustomEffect).CanBeDispelled))
						{
							playerEffectManager.ActiveEffects.Remove(item2);
							val.RemoveEffectFromPlayer(player.Ref);
						}
					}
				}
			}
			else
			{
				player.UpdateMoveSpeed();
				player.UpdateVisibility();
			}
		}
		else
		{
			player.UpdateMoveSpeed();
		}
		if (player.Ref == PlayerController.Local.Ref)
		{
			if (__instance is MidasEffect || __instance is AssassinEffect || __instance is ClairvoyanceEffect || __instance is NightVision || __instance is AuditionEffect || __instance is EnergizedEffect)
			{
				player.CheckEffectTutorial();
			}
		}
	}
}
