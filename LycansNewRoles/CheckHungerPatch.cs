using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems.Accessories;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "CheckHunger")]
internal class CheckHungerPatch
{
	private static bool Prefix(PlayerController __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Invalid comparison between Unknown and I4
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Invalid comparison between Unknown and I4
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Invalid comparison between Unknown and I4
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Invalid comparison between Unknown and I4
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0963: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_098b: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Invalid comparison between Unknown and I4
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aed: Invalid comparison between Unknown and I4
		//IL_0b04: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBool.op_Implicit(DraftManager.Instance.Active) && !NetworkBool.op_Implicit(GameManager.LightingManager.IsTransition) && !NetworkBool.op_Implicit(__instance.IsDead))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper && NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool) && !__instance.IsStarving())
				{
					float num = (((int)GameManager.LocalGameState == 4) ? 0.15f : 0.25f);
					__instance.Hunger -= num * ((SimulationBehaviour)__instance).Runner.DeltaTime;
				}
				if ((int)GameManager.State.Current != 2)
				{
					return false;
				}
				if (player.IsOutOfTheWorld)
				{
					return false;
				}
				if (NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
				{
					return false;
				}
				if (NetworkBool.op_Implicit(player.Sprinting))
				{
					__instance.Hunger -= 4f * ((SimulationBehaviour)__instance).Runner.DeltaTime;
				}
				if (NetworkBool.op_Implicit(__instance.PlayerEffectManager.Satiated) && (player.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover || (int)player.PlayerController.Role == 1))
				{
					return false;
				}
				if (NetworkBool.op_Implicit(player.Dying) || NetworkBool.op_Implicit(player.Petrified))
				{
					return false;
				}
				if (NetworkBool.op_Implicit(player.Burning))
				{
					if (NetworkBool.op_Implicit(player.PlayerController.IsWolf))
					{
						if (!player.PlayerController.IsStarving())
						{
							__instance.Hunger -= 1.5f * ((SimulationBehaviour)__instance).Runner.DeltaTime;
						}
					}
					else
					{
						__instance.Hunger -= 2.2f * ((SimulationBehaviour)__instance).Runner.DeltaTime;
					}
				}
				if (NetworkBool.op_Implicit(player.PurifierBurn))
				{
					if (NetworkBool.op_Implicit(player.PlayerController.IsWolf))
					{
						if (!player.PlayerController.IsStarving())
						{
							__instance.Hunger -= 0.8f * ((SimulationBehaviour)__instance).Runner.DeltaTime;
						}
					}
					else
					{
						player.IncreaseHealth(1.5f * ((SimulationBehaviour)__instance).Runner.DeltaTime);
					}
				}
				if (NetworkBool.op_Implicit(player.TournamentWinner))
				{
					if (NetworkBool.op_Implicit(player.PlayerController.IsWolf))
					{
						player.IncreaseHealth(0.6f * ((SimulationBehaviour)__instance).Runner.DeltaTime);
					}
					else
					{
						player.IncreaseHealth(0.8f * ((SimulationBehaviour)__instance).Runner.DeltaTime);
					}
				}
				if (NetworkBool.op_Implicit(player.Asleep) || NetworkBool.op_Implicit(player.Downed) || NetworkBool.op_Implicit(player.Paralyzed) || NetworkBool.op_Implicit(player.Banished) || NetworkBool.op_Implicit(player.CapturedByCultist))
				{
					return false;
				}
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
				{
					if (NetworkBool.op_Implicit(__instance.IsMoving) && __instance.MovementAction == 2)
					{
						__instance.Hunger -= 25f * ((SimulationBehaviour)__instance).Runner.DeltaTime;
					}
					else
					{
						player.IncreaseHealth(4f * ((SimulationBehaviour)__instance).Runner.DeltaTime);
					}
					return false;
				}
				if (NetworkBool.op_Implicit(player.Escaping))
				{
					player.IncreaseHealth(4f * ((SimulationBehaviour)__instance).Runner.DeltaTime);
					return false;
				}
				if (!NetworkBool.op_Implicit(__instance.IsWolf))
				{
					if ((int)__instance.Role != 1)
					{
						float num2 = 0f;
						if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
						{
							if (!NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
							{
								return false;
							}
							PlayerCustom playerCustom = player.FindLoverPartner();
							if ((Object)(object)playerCustom != (Object)null && (NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf) || NetworkBool.op_Implicit(playerCustom.Kidnapped)))
							{
								return false;
							}
							num2 = 0.5f + 0.02f * (float)PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead)));
							num2 = num2 * (float)Plugin.CustomConfig.LoverVillagerHungerSpeed * 0.01f;
						}
						else if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.VillageIdiot && NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
						{
							num2 = (float)player.PrimaryRolePowerCurrentMaterials * 0.0002f;
						}
						else
						{
							num2 = 0.2f;
							if (NetworkBool.op_Implicit(__instance.IsMoving))
							{
								num2 = 0.35f;
								if (__instance.MovementAction == 2)
								{
									num2 = 1.5f;
								}
								else if (__instance.MovementAction == 1)
								{
									num2 = 0.25f;
								}
								if (NetworkBool.op_Implicit(__instance.IsClimbing))
								{
									num2 *= 1.5f;
								}
							}
							if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Avatar)
							{
								num2 /= 2f;
							}
							if (NetworkBool.op_Implicit(__instance.IsTrapped))
							{
								num2 = 2f;
							}
							if (player.StarvationActive)
							{
								num2 += 0.8f;
							}
							if (NetworkBool.op_Implicit(player.Disease))
							{
								num2 += 0.3f;
							}
							if (NetworkBool.op_Implicit(player.Poison))
							{
								num2 += 1.2f;
							}
							if ((Object)(object)player.AstralSpirit != (Object)null)
							{
								num2 = num2 + 0.2f + 0.01f * Vector3.Distance(((Component)__instance).transform.position, ((Component)player.AstralSpirit).transform.position);
							}
						}
						__instance.Hunger -= num2 * ((SimulationBehaviour)__instance).Runner.DeltaTime;
						if (__instance.Hunger <= 0f)
						{
							__instance.Hunger = 0f;
							if (((SimulationBehaviour)__instance).Runner.IsServer)
							{
								player.Stats.UpdateDeathType("STARVATION");
							}
							__instance.Rpc_Kill(PlayerRef.None);
							return false;
						}
					}
				}
				else if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					__instance.Hunger -= 0.0115f * (float)GameManager.Instance.MaxHunger * ((SimulationBehaviour)__instance).Runner.DeltaTime;
					if (NetworkBool.op_Implicit(__instance.IsTrapped))
					{
						__instance.Hunger -= 0.025f * (float)GameManager.Instance.MaxHunger * ((SimulationBehaviour)__instance).Runner.DeltaTime;
					}
					if (__instance.Hunger <= 0f)
					{
						__instance.Hunger = 0f;
						player.Stats.UpdateDeathType("STARVATION_AS_BEAST");
						__instance.Rpc_Kill(PlayerRef.None);
						return false;
					}
				}
				else if (!__instance.IsStarving())
				{
					float num3 = 0.5f;
					if (NetworkBool.op_Implicit(__instance.IsTrapped))
					{
						num3 = 6f;
					}
					if (NetworkBool.op_Implicit(__instance.IsMoving))
					{
						num3 = 1f;
						if (__instance.MovementAction == 2)
						{
							num3 = 2.5f;
						}
						else if (__instance.MovementAction == 1)
						{
							num3 = 0.5f;
						}
						if (NetworkBool.op_Implicit(__instance.IsClimbing))
						{
							num3 *= 1.5f;
						}
					}
					if ((Object)(object)player.AstralSpirit != (Object)null)
					{
						num3 = 0f;
					}
					if (NetworkBool.op_Implicit(player.Empowered))
					{
						num3 *= 0.75f;
					}
					if (NetworkBool.op_Implicit(player.Resilience))
					{
						num3 *= 0.25f;
					}
					if (NetworkBool.op_Implicit(player.Tenacity))
					{
						num3 *= 0.7f;
					}
					if (NetworkBool.op_Implicit(player.Hubris))
					{
						num3 *= 1.08f;
					}
					if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothActor && NetworkBool.op_Implicit(player.SecondaryRolePowerActive))
					{
						num3 *= 0.25f;
					}
					if ((int)__instance.Role != 1)
					{
						num3 *= 0.05f;
					}
					__instance.Hunger -= num3 * ((SimulationBehaviour)__instance).Runner.DeltaTime;
				}
				if (NetworkBool.op_Implicit(player.Recuperating))
				{
					if (NetworkBool.op_Implicit(__instance.IsWolf))
					{
						if (!NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
						{
							float num4 = 2f * ((SimulationBehaviour)__instance).Runner.DeltaTime;
							if (NetworkBool.op_Implicit(player.Tenacity))
							{
								num4 *= 2f;
							}
							player.IncreaseHealth(num4);
						}
					}
					else
					{
						float amount = 1.2f * ((SimulationBehaviour)__instance).Runner.DeltaTime;
						player.IncreaseHealth(amount);
					}
				}
				if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.FullMoon && NetworkBool.op_Implicit(player.PlayerController.IsWolf) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					player.IncreaseHealth(0.7f * ((SimulationBehaviour)__instance).Runner.DeltaTime);
				}
				if (player.IsCurrentlyPlayedOrObserved && NetworkBool.op_Implicit(__instance.IsWolf) && __instance.IsStarving() && !PlayerPrefs.HasKey("NALES_TUTORIAL_WOLF_RECUPERATE"))
				{
					UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_EFFECT_WOLF_RECUPERATE", 0.4f, 4f);
					PlayerPrefs.SetInt("NALES_TUTORIAL_WOLF_RECUPERATE", 1);
				}
				if ((Object)(object)player.Accessory != (Object)null && player.Accessory is AccessoryHorn && !NetworkBool.op_Implicit(player.Dying) && (!NetworkBool.op_Implicit(player.PlayerController.IsWolf) || !player.PlayerController.IsStarving()))
				{
					float num5 = ((player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && (int)player.PlayerController.Role == 0) ? 0.15f : ((!NetworkBool.op_Implicit(__instance.IsWolf)) ? 0.3f : 0.2f));
					__instance.Hunger = Mathf.Min((float)GameManager.Instance.MaxHunger, __instance.Hunger + num5 * ((SimulationBehaviour)__instance).Runner.DeltaTime);
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleMetabolicLessHungerPatch error: " + ex));
			return true;
		}
	}
}
