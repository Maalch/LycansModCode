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
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Invalid comparison between Unknown and I4
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Invalid comparison between Unknown and I4
		//IL_0982: Unknown result type (might be due to invalid IL or missing references)
		//IL_092d: Unknown result type (might be due to invalid IL or missing references)
		//IL_093e: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Invalid comparison between Unknown and I4
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Expected O, but got Unknown
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Invalid comparison between Unknown and I4
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Invalid comparison between Unknown and I4
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a19: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Expected O, but got Unknown
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Invalid comparison between Unknown and I4
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
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
						if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost) < Plugin.CustomConfig.GhostsCount)
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
								Vector3 position = Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value[GameManager.Instance.MapID - 1].position;
								NetworkObject val3 = ((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
								{
									//IL_0008: Unknown result type (might be due to invalid IL or missing references)
									((Component)no).transform.position = position;
								}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
								((Component)val3).transform.position = position;
								((Component)val3).GetComponent<PlayerSummonedSpiritComponent>().Init(playerCustom.Ref);
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
