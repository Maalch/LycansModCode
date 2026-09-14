using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewPrimaryRoles;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Rpc_Kill")]
internal class RpcKillPatch
{
	private static bool Prefix(PlayerRef killer, PlayerController __instance)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Invalid comparison between Unknown and I4
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Invalid comparison between Unknown and I4
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_094f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Invalid comparison between Unknown and I4
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Invalid comparison between Unknown and I4
		//IL_09be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09db: Invalid comparison between Unknown and I4
		//IL_07ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Expected O, but got Unknown
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Invalid comparison between Unknown and I4
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Invalid comparison between Unknown and I4
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Invalid comparison between Unknown and I4
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Invalid comparison between Unknown and I4
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Expected O, but got Unknown
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).HasStateAuthority && !NetworkBool.op_Implicit(__instance.IsDead))
			{
				PlayerCustom playerCustom = PlayerCustomRegistry.GetPlayer(__instance.Ref);
				if ((int)GameManager.LocalGameState == 4 && playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.VillageIdiot)
				{
					playerCustom.RoleDeathUniqueBool = NetworkBool.op_Implicit(true);
				}
				PlayerController val = null;
				if (!((PlayerRef)(ref killer)).IsNone)
				{
					val = PlayerRegistry.GetPlayer(killer);
				}
				if (playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
				{
					PlayerCustom playerCustom2 = playerCustom.FindLoverPartner();
					if ((Object)(object)playerCustom2 != (Object)null)
					{
						PlayerController playerController = playerCustom2.PlayerController;
						playerController.Killer = killer;
						playerCustom2.Stats.UpdateDeathType("LOVER_DEATH");
						playerCustom2.Stats.OnKilled(killer, ((Component)playerController).transform.position);
						playerController.IsDead = NetworkBool.op_Implicit(true);
					}
					else
					{
						Plugin.Logger.LogError((object)"Second lover not found or already dead!");
					}
				}
				if (playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper)
				{
					foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.Kidnapped)))
					{
						item.Kidnapped = NetworkBool.op_Implicit(false);
					}
				}
				if (NetworkBool.op_Implicit(__instance.IsWolf) && playerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Possessor)
				{
					PlayerRef primaryRoleTargetRef = playerCustom.PrimaryRoleTargetRef;
					if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef).Possessed))
					{
						PlayerController player = PlayerRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef);
						player.CharacterMovementHandler.TeleportData = new NetworkTeleportData(((Component)playerCustom.PlayerController).transform.position, ((Component)playerCustom.PlayerController).transform.rotation, true);
						Effect val2 = player.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is PossessedEffect);
						if ((Object)(object)val2 != (Object)null)
						{
							player.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val2).Object.Id);
						}
					}
				}
				if (!NetworkBool.op_Implicit(playerCustom.NoDeadRole) && !NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
				{
					if ((int)__instance.Role != 1 && playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None)
					{
						List<PlayerCustom.PlayerPrimaryRolePower> list = new List<PlayerCustom.PlayerPrimaryRolePower>();
						if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Angel) < Plugin.CustomConfig.GuardianAngelsCount)
						{
							list.Add(PlayerCustom.PlayerPrimaryRolePower.Angel);
						}
						if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost) + GameManagerCustom.Instance.GhostsDestroyed < Plugin.CustomConfig.GhostsCount && (int)GameManager.LocalGameState != 4 && (killer == PlayerRef.None || (float)PlayerCustomRegistry.GetPlayer(killer).TransformationTimer.ElapsedMilliseconds >= 15000f))
						{
							list.Add(PlayerCustom.PlayerPrimaryRolePower.Ghost);
						}
						if (list.Any())
						{
							PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list, 1).First();
							playerCustom.GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.None);
							playerCustom.GivePrimaryRolePower(playerPrimaryRolePower);
							switch (playerPrimaryRolePower)
							{
							case PlayerCustom.PlayerPrimaryRolePower.Angel:
								if ((int)GameManager.LocalGameState == 2)
								{
									playerCustom.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 15f);
								}
								else
								{
									playerCustom.PrimaryRolePowerRemainingUses = 1;
								}
								break;
							case PlayerCustom.PlayerPrimaryRolePower.Ghost:
							{
								NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectSummonedSpirit");
								Vector3 position = ((Component)playerCustom.PlayerController).transform.position;
								NetworkObject val3 = ((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
								{
									//IL_0008: Unknown result type (might be due to invalid IL or missing references)
									((Component)no).transform.position = position;
								}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
								((Component)val3).transform.position = position;
								((Component)val3).GetComponent<PlayerSummonedSpiritComponent>().Init(playerCustom.Ref);
								playerCustom.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
								break;
							}
							}
						}
					}
					else if (((int)__instance.Role == 1 || playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor) && playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover && !PlayerCustomRegistry.Any((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Necromancer && o.PrimaryRoleTargetRef == playerCustom.Ref) && PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Specter) < Plugin.CustomConfig.SpectersCount)
					{
						playerCustom.GivePrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Specter);
						playerCustom.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
						NetworkPrefabId networkObject2 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectSpecter");
						Vector3 position2 = Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value[GameManager.Instance.MapID - 1].position;
						NetworkObject val4 = ((SimulationBehaviour)__instance).Runner.Spawn(networkObject2, (Vector3?)position2, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							//IL_0008: Unknown result type (might be due to invalid IL or missing references)
							((Component)no).transform.position = position2;
						}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
						((Component)val4).transform.position = position2;
						((Component)val4).GetComponent<PlayerSummonedSpiritComponent>().Init(playerCustom.Ref);
					}
				}
				if (!((PlayerRef)(ref killer)).IsNone)
				{
					PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(killer);
					PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Scientist);
					if ((Object)(object)specificNewPrimaryRole != (Object)null && (int)GameManager.LocalGameState != 4 && NetworkBool.op_Implicit(player2.PlayerController.IsWolf))
					{
						PlayerController playerController2 = specificNewPrimaryRole.PlayerController;
						if (!NetworkBool.op_Implicit(playerController2.IsDead) && playerController2.Ref != __instance.Ref)
						{
							float num = Vector3.Distance(((Component)playerController2).transform.position, ((Component)val).transform.position);
							float num2 = 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
							if (num <= num2)
							{
								float num3 = ScientistUtility.GetBasePower(specificNewPrimaryRole, val, num, num2) * 5f;
								if (LycansUtility.CanPlayerSeeOtherPlayer(specificNewPrimaryRole, player2, num2))
								{
									num3 *= 4f;
								}
								int amount = Mathf.RoundToInt(num3 * BalancingValues.SoloRoleDiminishingReturnsMultiplier(playerCustom.SoloRoleHalfDayProgress, 7f));
								specificNewPrimaryRole.AddSoloRoleProgress(amount, BalancingValues.ScientistGoal(PlayerRegistry.Count));
							}
						}
					}
					if (player2.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Predator && player2.PrimaryRoleTargetRef == __instance.Ref)
					{
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("PredatorKill"), ((Component)__instance).transform.position, 100f, 1f);
						PlayerCustom.ApplyEffectToPlayer(val, "LycansNewRoles.EffectPredator", ((SimulationBehaviour)__instance).Runner);
						player2.PrimaryRoleTargetRef = PlayerRef.None;
					}
					if (playerCustom.InitialPower == PlayerCustom.PlayerPrimaryRolePower.Avatar && (int)player2.PlayerController.Role == 1 && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
					{
						player2.GainEmpoweredOnEachTransformation = true;
						if (NetworkBool.op_Implicit(player2.PlayerController.IsWolf))
						{
							PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, "LycansNewRoles.EffectEmpowered", ((SimulationBehaviour)__instance).Runner, 1f, 600f);
						}
					}
					if (playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.VillageIdiot && !NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf) && (int)player2.PlayerController.Role != 1 && player2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None)
					{
						player2.Stats.UpdateDeathType("KILLED_VILLAGE_IDIOT");
						player2.PlayerController.Rpc_Kill(PlayerRef.None);
					}
					__instance.Killer = killer;
					if (NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
					{
						val.IsGunLoaded = NetworkBool.op_Implicit(true);
					}
				}
				if (playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					BeastManager.Instance.BeastActive = NetworkBool.op_Implicit(false);
					GameManager.LightingManager.IsNight = NetworkBool.op_Implicit(true);
					GameManager.LightingManager.TimeOfDay = 6f;
					GameManager.Rpc_Transition(((SimulationBehaviour)__instance).Runner);
					GameManager.LightingManager.IsTransition = NetworkBool.op_Implicit(true);
				}
				if (NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf) && (int)playerCustom.PlayerController.Role != 1 && playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Beast)
				{
					playerCustom.PlayerController.IsWolf = NetworkBool.op_Implicit(false);
				}
				if (!NetworkBool.op_Implicit(playerCustom.ResurrectedByNecromancer) && playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie)
				{
					playerCustom.Stats.OnKilled(__instance.Killer, ((Component)__instance).transform.position);
				}
				__instance.IsDead = NetworkBool.op_Implicit(true);
				GameManager.Instance.CheckForEndGame();
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("RpcKillPatch error: " + ex));
			return true;
		}
	}
}
