using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.NewMaps;
using LycansNewRoles.PowerObjects;
using LycansNewRoles.Sabotages;
using LycansNewRoles.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameState), "Spawned")]
internal class GiveNewRolesPatch
{
	private static void Postfix(GameState __instance)
	{
		StateMachine<EGameState> value = Traverse.Create((object)__instance).Field<StateMachine<EGameState>>("StateMachine").Value;
		GameState gameState = __instance;
		StateHooks<EGameState> obj = value[(EGameState)2];
		obj.onEnter = (Action<EGameState>)Delegate.Combine(obj.onEnter, (Action<EGameState>)delegate(EGameState state)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0003: Invalid comparison between Unknown and I4
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Invalid comparison between Unknown and I4
			//IL_1303: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_16e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_16e2: Invalid comparison between Unknown and I4
			//IL_13b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_13b5: Invalid comparison between Unknown and I4
			//IL_16e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_16e6: Invalid comparison between Unknown and I4
			//IL_13ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_019a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_159e: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_15bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1607: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0456: Unknown result type (might be due to invalid IL or missing references)
			//IL_061c: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0851: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e4b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ec5: Unknown result type (might be due to invalid IL or missing references)
			//IL_119d: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_11c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_11e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_11e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_126d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1272: Unknown result type (might be due to invalid IL or missing references)
			if ((int)state == 1)
			{
				GameManagerCustom.Instance.NewGame();
			}
			if (((SimulationBehaviour)gameState).Runner.IsServer && (int)state == 1)
			{
				try
				{
					if (!NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
					{
						SessionStats.Stats.NewGame();
					}
					if (BalancingValues.AcrobatSpotsByMapId.ContainsKey(GameManager.Instance.MapID))
					{
						NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectAcrobatSpot");
						foreach (BalancingValues.AcrobatSpotData item in BalancingValues.AcrobatSpotsByMapId[GameManager.Instance.MapID])
						{
							NetworkObject val = ((SimulationBehaviour)gameState).Runner.Spawn(networkObject, (Vector3?)item.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
							((Component)val).GetComponent<AcrobatSpot>().Init(item.KnockbackPower, item.GravityDuringJump, item.Rotation, item.JumpDuration * 1.4f, item.FallSpeedDuringJump);
						}
					}
					Plugin.Logger.LogInfo((object)"Game start: init");
					foreach (PlayerCustom allPlayersIncludingSpectator in PlayerCustomRegistry.AllPlayersIncludingSpectators)
					{
						allPlayersIncludingSpectator.Reset();
					}
					if (!NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale) && !NetworkBool.op_Implicit(Plugin.CustomConfig.DraftMode))
					{
						Plugin.Logger.LogInfo((object)"Game start: determine if impostor present");
						bool flag = Random.value * 100f < (float)Plugin.CustomConfig.ImpostorChance;
						foreach (PlayerCustom allPlayersIncludingSpectator2 in PlayerCustomRegistry.AllPlayersIncludingSpectators)
						{
							allPlayersIncludingSpectator2.PlayerController.Role = (PlayerRole)0;
						}
						for (int i = 0; i < GameManager.Instance.WolvesCount; i++)
						{
							List<PlayerCustom> list = PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => (int)o.PlayerController.Role == 0).ToList();
							if (list.Any())
							{
								PlayerCustom playerCustom = CollectionsUtil.Grab<PlayerCustom>(list, 1).First();
								playerCustom.PlayerController.Role = (PlayerRole)1;
							}
						}
						foreach (PlayerCustom allSpectator in PlayerCustomRegistry.AllSpectators)
						{
							allSpectator.PlayerController.IsDead = NetworkBool.op_Implicit(true);
							allSpectator.TeleportToVoid();
						}
						Plugin.Logger.LogInfo((object)"Game start: solo roles");
						List<PlayerCustom.PlayerNewPrimaryRole> list2 = (from o in Plugin.CustomConfig.SoloRoleActive
							where o.Key != PlayerCustom.PlayerNewPrimaryRole.None && o.Key != PlayerCustom.PlayerNewPrimaryRole.Traitor && o.Value && !PlayerCustom.IsNewPrimaryRoleDisabled(o.Key)
							select o.Key).ToList();
						List<PlayerCustom> list3 = PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => (int)o.PlayerController.Role == 0 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).ToList();
						for (int num = 0; num < Plugin.CustomConfig.SoloRolesCount; num++)
						{
							if (list2.Any() && list3.Any())
							{
								PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = CollectionsUtil.Grab<PlayerCustom.PlayerNewPrimaryRole>(list2, 1).First();
								PlayerCustom playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(list3, 1).First();
								playerCustom2.GiveNewPrimaryRole(playerNewPrimaryRole);
								list2.Remove(playerNewPrimaryRole);
								list3.Remove(playerCustom2);
								if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent && list3.Any())
								{
									playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(list3, 1).First();
									playerCustom2.GiveNewPrimaryRole(playerNewPrimaryRole);
									list3.Remove(playerCustom2);
									list2.Remove(PlayerCustom.PlayerNewPrimaryRole.Lover);
								}
								if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
								{
									if (NetworkBool.op_Implicit(Plugin.CustomConfig.LoverWolfReplacesVillager))
									{
										if (list3.Any())
										{
											playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(list3, 1).First();
											playerCustom2.PlayerController.Role = (PlayerRole)1;
											playerCustom2.GiveNewPrimaryRole(playerNewPrimaryRole);
											list3.Remove(playerCustom2);
										}
									}
									else
									{
										playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(PlayerCustomRegistry.Where((PlayerCustom o) => (int)o.PlayerController.Role == 1).ToList(), 1).First();
										playerCustom2.GiveNewPrimaryRole(playerNewPrimaryRole);
									}
									list2.Remove(PlayerCustom.PlayerNewPrimaryRole.Agent);
								}
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: traitor");
						List<PlayerCustom> list4 = PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => (int)o.PlayerController.Role == 0 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).ToList();
						for (int num2 = 0; num2 < Plugin.CustomConfig.TraitorsCount; num2++)
						{
							if (list4.Any())
							{
								PlayerCustom playerCustom3 = CollectionsUtil.Grab<PlayerCustom>(list4, 1).First();
								playerCustom3.GiveNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Traitor);
								list4.Remove(playerCustom3);
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: wolf pup");
						List<PlayerCustom> list5 = PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => (int)o.PlayerController.Role == 0 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).ToList();
						for (int num3 = 0; num3 < Plugin.CustomConfig.WolfPupsCount; num3++)
						{
							if (list5.Any())
							{
								PlayerCustom playerCustom4 = CollectionsUtil.Grab<PlayerCustom>(list5, 1).First();
								playerCustom4.PlayerController.Role = (PlayerRole)1;
								playerCustom4.IsWolfPup = NetworkBool.op_Implicit(true);
								list5.Remove(playerCustom4);
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: wolves and traitor powers");
						List<PlayerCustom.PlayerPrimaryRolePower> list6 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
							where o.Value && PlayerCustom.IsPrimaryRolePowerForWolves(o.Key) && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key) && PlayerCustom.IsPrimaryRolePowerAvailableForCurrentMap(o.Key)
							select o.Key).ToList();
						if (Random.value < 0.25f)
						{
							if (!list6.Contains(PlayerCustom.PlayerPrimaryRolePower.Fartmaster))
							{
								list6.Add(PlayerCustom.PlayerPrimaryRolePower.Fartmaster);
							}
						}
						else
						{
							list6.RemoveAll((PlayerCustom.PlayerPrimaryRolePower o) => o == PlayerCustom.PlayerPrimaryRolePower.Fartmaster);
						}
						List<PlayerCustom> list7 = PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => ((int)o.PlayerController.Role == 1 || o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor) && o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.None && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover).ToList();
						int num4 = Plugin.CustomConfig.WolfPowersCount;
						if (flag && list7.Any((PlayerCustom o) => o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Traitor))
						{
							PlayerCustom playerCustom5 = CollectionsUtil.Grab<PlayerCustom>(list7.Where((PlayerCustom o) => o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Traitor).ToList(), 1).First();
							PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerCustom5.Ref);
							player.GivePrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Impostor);
							num4--;
							list7.Remove(playerCustom5);
						}
						for (int num5 = 0; num5 < num4; num5++)
						{
							if (list7.Any() && list6.Any())
							{
								PlayerCustom customPlayer = CollectionsUtil.Grab<PlayerCustom>(list7, 1).First();
								List<PlayerCustom.PlayerPrimaryRolePower> list8 = list6.Where((PlayerCustom.PlayerPrimaryRolePower o) => (int)customPlayer.PlayerController.Role == 1 || PlayerCustom.IsWolfPowerAvailableForTraitor(o)).ToList();
								if (NetworkBool.op_Implicit(customPlayer.IsWolfPup))
								{
									list8.RemoveAll((PlayerCustom.PlayerPrimaryRolePower o) => !PlayerCustom.IsWolfPowerAvailableForWolfPup(o));
								}
								if (list8.Any())
								{
									PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list8, 1).First();
									customPlayer.GivePrimaryRolePower(playerPrimaryRolePower);
									list6.Remove(playerPrimaryRolePower);
									list7.Remove(customPlayer);
								}
								else
								{
									list7.Remove(customPlayer);
								}
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: elites");
						List<PlayerCustom> list9 = PlayerCustomRegistry.Where((PlayerCustom o) => (int)o.PlayerController.Role == 0 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).ToList();
						List<PlayerCustom.PlayerPrimaryRolePower> list10 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
							where PlayerCustom.IsPrimaryRolePowerForEliteVillagers(o.Key) && o.Value && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key) && PlayerCustom.IsPrimaryRolePowerAvailableForCurrentMap(o.Key)
							select o.Key).ToList();
						int num6 = Plugin.CustomConfig.ElitesCount;
						if (flag)
						{
							num6--;
						}
						for (int num7 = 0; num7 < num6; num7++)
						{
							if (list9.Any() && list10.Any())
							{
								PlayerCustom playerCustom6 = CollectionsUtil.Grab<PlayerCustom>(list9, 1).First();
								PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower2 = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list10, 1).First();
								playerCustom6.GivePrimaryRolePower(playerPrimaryRolePower2);
								list10.Remove(playerPrimaryRolePower2);
								list9.Remove(playerCustom6);
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: villager powers");
						List<PlayerCustom.PlayerPrimaryRolePower> list11 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
							where o.Value && PlayerCustom.IsPrimaryRolePowerForNormalVillagers(o.Key) && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key) && PlayerCustom.IsPrimaryRolePowerAvailableForCurrentMap(o.Key)
							select o.Key).ToList();
						List<PlayerCustom.PlayerPrimaryRolePower> list12 = new List<PlayerCustom.PlayerPrimaryRolePower>();
						foreach (PlayerCustom.PlayerPrimaryRolePower item2 in list11)
						{
							int villagerJobChancePonderation = BalancingValues.GetVillagerJobChancePonderation(item2);
							for (int num8 = 0; num8 < villagerJobChancePonderation; num8++)
							{
								list12.Add(item2);
							}
						}
						foreach (PlayerCustom villagerCustom in list9)
						{
							if (Random.value * 100f < (float)Plugin.CustomConfig.VillagerPowersChance && list12.Count > 0)
							{
								PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower3 = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list12.Where((PlayerCustom.PlayerPrimaryRolePower o) => o != villagerCustom.NonDraftLastGamePower).ToList(), 1).First();
								villagerCustom.GivePrimaryRolePower(playerPrimaryRolePower3);
								villagerCustom.NonDraftLastGamePower = playerPrimaryRolePower3;
								list12.Remove(playerPrimaryRolePower3);
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: avatar");
						if (Random.value * 100f < (float)Plugin.CustomConfig.AvatarChance && list9.Any())
						{
							PlayerCustom playerCustom7 = CollectionsUtil.Grab<PlayerCustom>(list9, 1).FirstOrDefault();
							playerCustom7.GivePrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Avatar);
						}
						Plugin.Logger.LogInfo((object)"Game start: mole");
						if (Random.value * 100f < (float)Plugin.CustomConfig.MoleChance && list9.Any())
						{
							PlayerCustom playerCustom8 = CollectionsUtil.Grab<PlayerCustom>(list9, 1).FirstOrDefault();
							playerCustom8.GivePrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Mole);
						}
						Plugin.Logger.LogInfo((object)"Game start: secondary role");
						List<PlayerCustom.PlayerSecondaryRole> availableSecondaryRoles = (from o in Plugin.CustomConfig.SecondaryRoleActive
							where o.Value && !PlayerCustom.IsSecondaryRoleDisabled(o.Key)
							select o.Key).ToList();
						List<PlayerController> list13 = (from t in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => true))
							select (t)).ToList();
						Plugin.Logger.LogInfo((object)"Game start: telepath");
						if (Plugin.CustomConfig.SecondaryRoleActive[PlayerCustom.PlayerSecondaryRole.BothTelepath])
						{
							float num9 = Mathf.Min(1f, (float)Plugin.CustomConfig.SecondaryRolesCount / (float)list13.Count) * 0.5f;
							if (Random.value < num9)
							{
								List<PlayerCustom> source = PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => PlayerCustom.GetAvailableSecondaryRoles(o.PlayerController.Role, o.NewPrimaryRole, o.PrimaryRolePower).Contains(PlayerCustom.PlayerSecondaryRole.BothTelepath)).ToList();
								List<PlayerCustom> list14 = source.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor || (int)o.PlayerController.Role == 1).ToList();
								List<PlayerCustom> list15 = source.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None && (int)o.PlayerController.Role != 1).ToList();
								if (list14.Count >= 2 && Random.value < 0.33f)
								{
									List<PlayerCustom> list16 = CollectionsUtil.Grab<PlayerCustom>(list14, 2).ToList();
									foreach (PlayerCustom item3 in list16)
									{
										PlayerCustomRegistry.GetPlayer(item3.Ref).GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.BothTelepath);
										list13.Remove(item3.PlayerController);
									}
								}
								else if (list15.Count >= 2)
								{
									List<PlayerCustom> list17 = CollectionsUtil.Grab<PlayerCustom>(list15, 2).ToList();
									foreach (PlayerCustom item4 in list17)
									{
										PlayerCustomRegistry.GetPlayer(item4.Ref).GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.BothTelepath);
										list13.Remove(item4.PlayerController);
									}
								}
							}
						}
						availableSecondaryRoles.Remove(PlayerCustom.PlayerSecondaryRole.BothTelepath);
						for (int num10 = 0; num10 < Plugin.CustomConfig.SecondaryRolesCount; num10++)
						{
							if (availableSecondaryRoles.Any() && list13.Any())
							{
								PlayerController val2 = CollectionsUtil.Grab<PlayerController>(list13, 1).First();
								PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(val2.Ref);
								List<PlayerCustom.PlayerSecondaryRole> availableSecondaryRoles2 = PlayerCustom.GetAvailableSecondaryRoles(val2.Role, player2.NewPrimaryRole, player2.PrimaryRolePower);
								if (player2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor)
								{
									availableSecondaryRoles2.RemoveAll((PlayerCustom.PlayerSecondaryRole o) => PlayerCustom.IsSecondaryRoleDisabledForTraitor(o));
								}
								availableSecondaryRoles2 = availableSecondaryRoles2.Where((PlayerCustom.PlayerSecondaryRole o) => availableSecondaryRoles.Contains(o)).ToList();
								if (availableSecondaryRoles2.Any())
								{
									PlayerCustom.PlayerSecondaryRole role = CollectionsUtil.Grab<PlayerCustom.PlayerSecondaryRole>(availableSecondaryRoles2, 1).First();
									player2.GiveSecondaryRole(role);
									list13.Remove(val2);
								}
								else
								{
									list13.Remove(val2);
								}
							}
						}
						foreach (PlayerCustom item5 in PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.None))
						{
							item5.GivePrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.None);
						}
						foreach (PlayerCustom item6 in PlayerCustomRegistry.Where((PlayerCustom o) => o.SecondaryRole == PlayerCustom.PlayerSecondaryRole.None))
						{
							item6.GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.None);
						}
						SabotageManager.Instance.Init();
						BeastManager.Instance.Reset();
						CultistManager.Instance.Reset();
						VoodooManager.Instance.Reset();
						GameManager.Instance.UpdateLoot(true);
						GameManager.Instance.ClearSpawnedItems();
						GameManager.Instance.SpawnRandomItems();
						Plugin.Logger.LogInfo((object)"Game start: stats and others");
						try
						{
							foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
							{
								PlayerController playerController = allPlayer.PlayerController;
								allPlayer.InitForGameStart();
								allPlayer.InitStats();
								string[] obj8 = new string[14]
								{
									"Player ", null, null, null, null, null, null, null, null, null,
									null, null, null, null
								};
								NetworkString<_32> username = playerController.PlayerData.Username;
								obj8[1] = ((object)username/*cast due to constrained. prefix*/).ToString();
								obj8[2] = " with ref ";
								obj8[3] = ((object)playerController.Ref/*cast due to constrained. prefix*/).ToString();
								obj8[4] = " has base role ";
								obj8[5] = ((object)playerController.Role/*cast due to constrained. prefix*/).ToString();
								obj8[6] = ", primary role ";
								obj8[7] = allPlayer.NewPrimaryRole.ToString();
								obj8[8] = ", secondary role ";
								obj8[9] = allPlayer.SecondaryRole.ToString();
								obj8[10] = ", power ";
								obj8[11] = allPlayer.PrimaryRolePower.ToString();
								obj8[12] = ", wolf pup: ";
								obj8[13] = ((object)allPlayer.IsWolfPup/*cast due to constrained. prefix*/).ToString();
								LycansUtility.DebugLog(string.Concat(obj8));
							}
							GameManager.Instance.GiveAlchemistPotions();
							GameManagerCustom.Instance.NewDay();
						}
						catch (Exception ex)
						{
							Plugin.Logger.LogError((object)("New game stats error: " + ex));
						}
						GameManagerCustom.Instance.EventsManager.RollEvent();
					}
					if (NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
					{
						foreach (PlayerCustom allPlayer2 in PlayerCustomRegistry.AllPlayers)
						{
							allPlayer2.PrimaryRolePower = PlayerCustom.PlayerPrimaryRolePower.Hunter;
						}
					}
				}
				catch (Exception ex2)
				{
					Plugin.Logger.LogError((object)("Roles spawned error: " + ex2));
				}
			}
			if (((SimulationBehaviour)__instance).Runner.IsPlayer)
			{
				PlayerController local = PlayerController.Local;
				if ((Object)(object)local != (Object)null && (int)state == 1)
				{
					LycansUtility.AddLogOnlyForMe("Game start personal stuff");
					PlayerCustom player3 = PlayerCustomRegistry.GetPlayer(local.Ref);
					if (!PlayerPrefs.HasKey("NALES_TUTORIAL_SHOW_DESCRIPTION") && (player3.SecondaryRole != PlayerCustom.PlayerSecondaryRole.None || player3.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None || player3.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None))
					{
						UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_SHOW_DESCRIPTION", 0.4f, 6f);
						PlayerPrefs.SetInt("NALES_TUTORIAL_SHOW_DESCRIPTION", 1);
					}
					PlayerCustom.PlayersWithSpecificColor.Clear();
					UIManager.ItemSecondaryPanel.Hide();
					RenderSettings.fogStartDistance = 0f;
					FogChangesPatch.FogEndDistanceDaytime = BalancingValues.FogEndDistanceDaytimeByFogConfigPercentage[Plugin.CustomConfig.NightFog];
					FogChangesPatch.FogEndDistanceNight = BalancingValues.FogEndDistanceNightByFogConfigPercentage[Plugin.CustomConfig.NightFog] * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					FogChangesPatch.FogDensityNight = 0.25f + (float)Plugin.CustomConfig.NightFog / 200f;
					foreach (PlayerCustom allPlayer3 in PlayerCustomRegistry.AllPlayers)
					{
						allPlayer3.PrimaryRolePowerPlayersList.Clear();
						allPlayer3.DetectiveIntelList.Clear();
						allPlayer3.MercenaryTargetsAlreadyHit.Clear();
						allPlayer3.InitialPower = allPlayer3.PrimaryRolePower;
						allPlayer3.AlreadyPossessed = false;
						allPlayer3.UpdateDisplayColor();
						allPlayer3.UpdatePet();
						allPlayer3.ResetGravity();
						allPlayer3.PlacedSleepingGas = null;
					}
					Dictionary<PlayerRef, PlayerDisplay> value2 = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
					foreach (KeyValuePair<PlayerRef, PlayerDisplay> item7 in value2)
					{
						TextMeshProUGUI value3 = Traverse.Create((object)item7.Value).Field<TextMeshProUGUI>("username").Value;
						PlayerController player4 = PlayerRegistry.GetPlayer(item7.Key);
						if ((Object)(object)player4 != (Object)null)
						{
							NetworkPlayerData playerData = player4.PlayerData;
							if (((NetworkPlayerData)(ref playerData)).IsValid)
							{
								playerData = player4.PlayerData;
								((TMP_Text)value3).text = ((object)playerData.Username/*cast due to constrained. prefix*/).ToString();
								((Graphic)value3).color = new Color(255f, 255f, 255f, 0.6f);
							}
						}
					}
					List<GameObject> list18 = (from o in Object.FindObjectsOfType<MinimapDeathPositionComponent>(true)
						select ((Component)o).gameObject).ToList();
					list18.AddRange(from o in Object.FindObjectsOfType<MinimapDetectivePositionComponent>(true)
						select ((Component)o).gameObject);
					foreach (GameObject item8 in list18)
					{
						Object.Destroy((Object)(object)item8);
					}
					UIManager.LastGameSummaryPanel.Clear();
					Plugin.CreatePlayerIllusionIfNeeded();
				}
				if ((int)state == 1 || (int)state == 4)
				{
					foreach (PlayerCustom item9 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (int)o.PlayerController.Role == 1 && !NetworkBool.op_Implicit(o.IsWolfPup) && !o.IsOutOfTheWorld))
					{
						item9.WolfDaysWithoutTransformation++;
					}
				}
			}
			if (((SimulationBehaviour)__instance).Runner.IsServer)
			{
				LycansUtility.AddLogOnlyForMe("Stats: NewPhase, timing: " + GameStats.GetCurrentTiming());
				SessionStats.Stats.CurrentGame.AddEvent(GameEvent.GameEventType.NewPhase, GameStats.GetCurrentTiming());
			}
		});
		StateHooks<EGameState> obj2 = value[(EGameState)4];
		obj2.onEnter = (Action<EGameState>)Delegate.Combine(obj2.onEnter, (Action<EGameState>)delegate
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0552: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_0432: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0494: Unknown result type (might be due to invalid IL or missing references)
			//IL_0443: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Unknown result type (might be due to invalid IL or missing references)
			//IL_0459: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_046a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0479: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0408: Unknown result type (might be due to invalid IL or missing references)
			if (((SimulationBehaviour)gameState).Runner.IsServer && !NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
			{
				try
				{
					GameManager.Instance.IncrementScore(GameManagerCustom.Instance.CollectedLoot);
					GameManagerCustom.Instance.CollectedLoot = 0;
					BellOnAlmostOverPatch.BellSounded = false;
					foreach (PlayerCustom allPlayer4 in PlayerCustomRegistry.AllPlayers)
					{
						TickTimer val = allPlayer4.PrimaryRolePowerCooldownTimer;
						if (((TickTimer)(ref val)).IsRunning)
						{
							val = allPlayer4.PrimaryRolePowerCooldownTimer;
							allPlayer4.PrimaryRolePowerCooldownTimerTicksBeforeMeeting = ((TickTimer)(ref val)).RemainingTicks(((SimulationBehaviour)gameState).Runner).Value;
							allPlayer4.PrimaryRolePowerCooldownTimer = TickTimer.None;
						}
						val = allPlayer4.SecondaryRolePowerCooldownTimer;
						if (((TickTimer)(ref val)).IsRunning)
						{
							val = allPlayer4.SecondaryRolePowerCooldownTimer;
							allPlayer4.SecondaryRolePowerCooldownTimerTicksBeforeMeeting = ((TickTimer)(ref val)).RemainingTicks(((SimulationBehaviour)gameState).Runner).Value;
							allPlayer4.SecondaryRolePowerCooldownTimer = TickTimer.None;
						}
						switch (allPlayer4.PrimaryRolePower)
						{
						case PlayerCustom.PlayerPrimaryRolePower.Warlock:
							allPlayer4.PrimaryRoleTargetRef = allPlayer4.Ref;
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Predator:
							allPlayer4.PrimaryRoleTargetRef = PlayerRef.None;
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Tracker:
							allPlayer4.PrimaryRolePowerRemainingUses = 1;
							break;
						}
						if (allPlayer4.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTinkerer)
						{
							allPlayer4.SecondaryRoleUniqueInt = 1;
						}
						if (allPlayer4.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
						{
							PlayerCustom playerCustom = allPlayer4.FindLoverPartner();
							if ((Object)(object)playerCustom == (Object)null || NetworkBool.op_Implicit(playerCustom.PlayerController.IsDead))
							{
								allPlayer4.Stats.UpdateDeathType("LOVER_DEATH");
								allPlayer4.Stats.OnKilled(PlayerRef.None, ((Component)allPlayer4.PlayerController).transform.position);
								allPlayer4.PlayerController.IsDead = NetworkBool.op_Implicit(true);
							}
						}
						allPlayer4.MayorVoteTarget = PlayerRef.None;
					}
					if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Tournament)
					{
						PlayerCustom playerCustom2 = (from o in PlayerCustomRegistry
							where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld
							orderby o.LootCollectedTodayDuringDay descending
							select o).FirstOrDefault();
						if ((Object)(object)playerCustom2 != (Object)null)
						{
							GameManagerCustom.Instance.CurrentMayor = playerCustom2.Ref;
							PlayerCustom.ApplyEffectToPlayer(playerCustom2.PlayerController, "LycansNewRoles.EffectTournamentWinner", ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
							playerCustom2.Stats.AddAction(new PlayerStats.PlayerAction
							{
								ActionType = "TournamentWon"
							}, ((Component)playerCustom2.PlayerController).transform.position);
						}
						PlayerCustom playerCustom3 = (from o in PlayerCustomRegistry
							where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld
							orderby o.LootCollectedTodayDuringDay
							select o).FirstOrDefault();
						if ((Object)(object)playerCustom3 != (Object)null)
						{
							PlayerCustom.ApplyEffectToPlayer(playerCustom3.PlayerController, "LycansNewRoles.EffectTournamentLoser", ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
							PlayerCustom.ApplyEffectToPlayer(playerCustom3.PlayerController, EffectManager.GetEffects().First((Effect o) => o is GlowingEffect), ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
							playerCustom3.Stats.AddAction(new PlayerStats.PlayerAction
							{
								ActionType = "TournamentLost"
							}, ((Component)playerCustom3.PlayerController).transform.position);
						}
						GameManagerCustom.Rpc_New_Event(((SimulationBehaviour)__instance).Runner, 0);
					}
					else if ((NetworkBool.op_Implicit(Plugin.CustomConfig.AllowMayor) && (GameManagerCustom.Instance.CurrentMayor == PlayerRef.None || !PlayerCustomRegistry.HasPlayer(GameManagerCustom.Instance.CurrentMayor) || NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(GameManagerCustom.Instance.CurrentMayor).PlayerController.IsDead))) || NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(GameManagerCustom.Instance.CurrentMayor).Kidnapped))
					{
						GameManagerCustom.Instance.PickRandomMayor();
					}
				}
				catch (Exception ex)
				{
					Plugin.Logger.LogError((object)("Roles spawned error (Meeting): " + ex));
				}
			}
			if (NetworkBool.op_Implicit(Plugin.CustomConfig.AllowMayor))
			{
				LycansUtility.AddLogOnlyForMe("Mayor stuff");
				int required = Mathf.CeilToInt((float)(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) / 2));
				UIManager.MayorPanelForOthers.UpdateCurrentVote();
				UIManager.MayorPanelForOthers.UpdateDestitutionCount(0, required);
				UIManager.MayorPanelForOthers.UpdateDifferentCount(0, required, PlayerRef.None);
			}
			UIManager.SoloRolesProgressPanel.UpdateSoloRoles();
			LycansUtility.AddLogOnlyForMe("Player stuff");
			foreach (PlayerCustom allPlayer5 in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer5.UpdateVisibility();
				allPlayer5.UpdateIllusion();
				allPlayer5.AlreadyAngeledToday = false;
				if ((Object)(object)allPlayer5.Accessory != (Object)null && allPlayer5.Accessory is AccessoryCrystalBall accessoryCrystalBall)
				{
					accessoryCrystalBall.Available = true;
				}
			}
			if (((SimulationBehaviour)__instance).Runner.IsServer)
			{
				LycansUtility.AddLogOnlyForMe("Stats: NewPhase, timing: " + GameStats.GetCurrentTiming());
				SessionStats.Stats.CurrentGame.AddEvent(GameEvent.GameEventType.NewPhase, GameStats.GetCurrentTiming());
			}
		});
		StateHooks<EGameState> obj3 = value[(EGameState)4];
		obj3.onExit = (Action<EGameState>)Delegate.Combine(obj3.onExit, (Action<EGameState>)delegate(EGameState state)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0809: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0786: Unknown result type (might be due to invalid IL or missing references)
			//IL_0788: Invalid comparison between Unknown and I4
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_079e: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a4: Invalid comparison between Unknown and I4
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_0180: Unknown result type (might be due to invalid IL or missing references)
			//IL_0185: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Unknown result type (might be due to invalid IL or missing references)
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_021e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Unknown result type (might be due to invalid IL or missing references)
			//IL_052f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0531: Invalid comparison between Unknown and I4
			//IL_0539: Unknown result type (might be due to invalid IL or missing references)
			//IL_0571: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e8: Invalid comparison between Unknown and I4
			//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_051a: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_06bb: Invalid comparison between Unknown and I4
			//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0683: Unknown result type (might be due to invalid IL or missing references)
			//IL_0685: Invalid comparison between Unknown and I4
			//IL_0650: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_068d: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0742: Unknown result type (might be due to invalid IL or missing references)
			//IL_075d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0724: Unknown result type (might be due to invalid IL or missing references)
			//IL_0735: Unknown result type (might be due to invalid IL or missing references)
			if (((SimulationBehaviour)gameState).Runner.IsServer && !NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
			{
				try
				{
					foreach (PlayerCustom allPlayer6 in PlayerCustomRegistry.AllPlayers)
					{
						if (allPlayer6.PrimaryRolePowerCooldownTimerTicksBeforeMeeting.HasValue)
						{
							int value2 = allPlayer6.PrimaryRolePowerCooldownTimerTicksBeforeMeeting.Value;
							value2 += (int)(5f * (float)((SimulationBehaviour)gameState).Runner.Config.Simulation.TickRate);
							allPlayer6.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromTicks(((SimulationBehaviour)gameState).Runner, value2);
							allPlayer6.PrimaryRolePowerCooldownTimerTicksBeforeMeeting = null;
						}
						if (allPlayer6.SecondaryRolePowerCooldownTimerTicksBeforeMeeting.HasValue)
						{
							int value3 = allPlayer6.SecondaryRolePowerCooldownTimerTicksBeforeMeeting.Value;
							value3 += (int)(5f * (float)((SimulationBehaviour)gameState).Runner.Config.Simulation.TickRate);
							allPlayer6.SecondaryRolePowerCooldownTimer = TickTimer.CreateFromTicks(((SimulationBehaviour)gameState).Runner, value3);
							allPlayer6.SecondaryRolePowerCooldownTimerTicksBeforeMeeting = null;
						}
						if (NetworkBool.op_Implicit(allPlayer6.IsImitator) && allPlayer6.SecondaryRole != PlayerCustom.PlayerSecondaryRole.BothImitator)
						{
							allPlayer6.GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.BothImitator);
							allPlayer6.SecondaryRolePowerCooldownTimer = TickTimer.None;
							allPlayer6.SecondaryRoleFirstRemainingUses = 0;
						}
						allPlayer6.DeceiverTrickThisMeeting = NetworkBool.op_Implicit(false);
						allPlayer6.ProtectedPriest = NetworkBool.op_Implicit(false);
						NetworkBool weakened = allPlayer6.Weakened;
						NetworkBool empowered = allPlayer6.Empowered;
						NetworkBool tournamentWinner = allPlayer6.TournamentWinner;
						bool flag = allPlayer6.PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is TournamentLoser);
						allPlayer6.PlayerController.PlayerEffectManager.ClearEffects();
						allPlayer6.SleepStacks = 0;
						if (NetworkBool.op_Implicit(weakened))
						{
							PlayerCustom.ApplyEffectToPlayer(allPlayer6.PlayerController, "LycansNewRoles.EffectWeakened", ((SimulationBehaviour)__instance).Runner, 1f, 3600f);
						}
						if (NetworkBool.op_Implicit(empowered))
						{
							PlayerCustom.ApplyEffectToPlayer(allPlayer6.PlayerController, "LycansNewRoles.EffectEmpowered", ((SimulationBehaviour)__instance).Runner, 1f, 3600f);
						}
						if (NetworkBool.op_Implicit(tournamentWinner))
						{
							PlayerCustom.ApplyEffectToPlayer(allPlayer6.PlayerController, "LycansNewRoles.EffectTournamentWinner", ((SimulationBehaviour)__instance).Runner, 1f, 3600f);
						}
						if (flag)
						{
							List<string> list = new List<string> { "LycansNewRoles.EffectNearsighted", "LycansNewRoles.EffectMute", "Paranoia", "Glowing", "Flatulences" };
							string text = CollectionsUtil.Grab<string>(list, 1).First();
							switch (text)
							{
							case "LycansNewRoles.EffectNearsighted":
							case "LycansNewRoles.EffectMute":
								PlayerCustom.ApplyEffectToPlayer(allPlayer6.PlayerController, text, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
								break;
							case "Paranoia":
							{
								Effect val2 = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is ParanoiaEffect);
								if ((Object)(object)val2 != (Object)null)
								{
									PlayerCustom.ApplyEffectToPlayer(allPlayer6.PlayerController, val2, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
								}
								break;
							}
							case "Glowing":
							{
								Effect val3 = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is GlowingEffect);
								if ((Object)(object)val3 != (Object)null)
								{
									PlayerCustom.ApplyEffectToPlayer(allPlayer6.PlayerController, val3, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
								}
								break;
							}
							case "Flatulences":
							{
								Effect val = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is FlatulenceEffect);
								if ((Object)(object)val != (Object)null)
								{
									PlayerCustom.ApplyEffectToPlayer(allPlayer6.PlayerController, val, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
								}
								break;
							}
							}
						}
						switch (allPlayer6.PrimaryRolePower)
						{
						case PlayerCustom.PlayerPrimaryRolePower.Investigator:
							if ((int)state == 2 && !NetworkBool.op_Implicit(allPlayer6.PlayerController.IsDead))
							{
								InvestigatorHint.CreateHintsOnNewDay(((SimulationBehaviour)gameState).Runner, allPlayer6);
								allPlayer6.PrimaryRoleTargetRef = PlayerRef.None;
								allPlayer6.InvestigatorGiveNewTarget();
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Hermit:
							if ((int)state == 2 && !NetworkBool.op_Implicit(allPlayer6.PlayerController.IsDead))
							{
								HermitHideout.CreateHideoutsOnNewDay(((SimulationBehaviour)gameState).Runner, allPlayer6);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Predator:
							allPlayer6.PrimaryRolePowerRemainingUses = 1;
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Ghost:
							allPlayer6.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
							break;
						}
						switch (allPlayer6.NewPrimaryRole)
						{
						case PlayerCustom.PlayerNewPrimaryRole.Mercenary:
							if (!NetworkBool.op_Implicit(allPlayer6.PlayerController.IsDead) && !NetworkBool.op_Implicit(allPlayer6.PlayerController.IsGunLoaded))
							{
								allPlayer6.PlayerController.IsGunLoaded = NetworkBool.op_Implicit(true);
							}
							break;
						case PlayerCustom.PlayerNewPrimaryRole.Kidnapper:
						{
							TickTimer primaryRolePowerCooldownTimer = allPlayer6.PrimaryRolePowerCooldownTimer;
							if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
							{
								primaryRolePowerCooldownTimer = allPlayer6.PrimaryRolePowerCooldownTimer;
								if (!(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)gameState).Runner) < 15f))
								{
									break;
								}
							}
							allPlayer6.PrimaryRolePowerRemainingUses = 0;
							allPlayer6.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)gameState).Runner, 15f);
							break;
						}
						}
						switch (allPlayer6.SecondaryRole)
						{
						case PlayerCustom.PlayerSecondaryRole.BothPolitician:
							allPlayer6.SecondaryRoleFirstRemainingUses = 1;
							break;
						case PlayerCustom.PlayerSecondaryRole.BothMerchant:
							if ((int)state == 2 && !NetworkBool.op_Implicit(allPlayer6.PlayerController.IsDead))
							{
								MerchantCoin.CreateCoinsOnNewDay(((SimulationBehaviour)gameState).Runner, allPlayer6);
							}
							break;
						}
						if ((int)state == 2 && !NetworkBool.op_Implicit(allPlayer6.PlayerController.IsDead) && NetworkBool.op_Implicit(allPlayer6.BombDormant))
						{
							allPlayer6.BombTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)gameState).Runner, Random.Range(5f, 10f));
						}
						if ((Object)(object)allPlayer6.SummonedSpirit != (Object)null)
						{
							allPlayer6.SummonedSpirit.AttackCooldown = TickTimer.None;
							allPlayer6.SummonedSpirit.SpellCooldown = TickTimer.None;
						}
						allPlayer6.MayorVoteTarget = PlayerRef.None;
						allPlayer6.LootCollectedTodayDuringDay = 0;
						allPlayer6.RecuperateDisabled = false;
						allPlayer6.ForcedTransformation = NetworkBool.op_Implicit(false);
					}
					if ((int)state == 2)
					{
						ManageSoloRolesHunt(__instance);
					}
					if ((int)GameManager.LocalGameState != 5)
					{
						GameManagerCustom.Instance.NewDay();
						GameManagerCustom.Instance.EventsManager.RollEvent();
					}
				}
				catch (Exception ex)
				{
					Plugin.Logger.LogError((object)("Meeting onExit error: " + ex));
				}
			}
			LycansUtility.AddLogOnlyForMe("Other stuff after meeting");
			if (!NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
			{
				foreach (PlayerCustom allPlayer7 in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer7.AskForSpeechTimer.Reset();
					allPlayer7.AskForSpeechUsedThisMeeting = false;
					allPlayer7.WolfForcedTransformationWarningIssued = false;
					allPlayer7.UpdateIconAbovePlayer(visible: true);
				}
			}
			if (UIManager.GenericChoicePanel.Active)
			{
				UIManager.GenericChoicePanel.Hide();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
			ShowRoleDescriptionPatch.NeedsUpdate = true;
		});
		StateHooks<EGameState> obj4 = value[(EGameState)5];
		obj4.onEnter = (Action<EGameState>)Delegate.Combine(obj4.onEnter, (Action<EGameState>)delegate
		{
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				SabotageManager.Instance.Clean();
				BeastManager.Instance.Reset();
				CultistManager.Instance.Reset();
				VoodooManager.Instance.Reset();
				GameManagerCustom.Instance.Reset();
				foreach (PlayerCustom allPlayer8 in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer8.ResurrectedByNecromancer = NetworkBool.op_Implicit(false);
					allPlayer8.HasZombieColor = false;
					allPlayer8.UpdateModelIfNeeded();
					allPlayer8.UpdateVisibility();
					allPlayer8.UpdateScaleAndPitch();
					allPlayer8.CustomAudio.UpdateVolume();
				}
				if (((SimulationBehaviour)gameState).Runner.IsServer)
				{
					List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None).ToList();
					{
						foreach (PlayerCustom item10 in list)
						{
							item10.PrimaryRoleTargetRef = PlayerRef.None;
						}
						return;
					}
				}
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError((object)("Meeting onExit error: " + ex));
			}
		});
		StateHooks<EGameState> obj5 = value[(EGameState)1];
		obj5.onEnter = (Action<EGameState>)Delegate.Combine(obj5.onEnter, (Action<EGameState>)delegate
		{
			LycansUtility.AddLogOnlyForMe("Pregame Enter");
			if (((SimulationBehaviour)gameState).Runner.IsServer)
			{
				try
				{
					foreach (PlayerCustom allPlayer9 in PlayerCustomRegistry.AllPlayers)
					{
						allPlayer9.Reset();
					}
					BeastManager.Instance.Reset();
					CultistManager.Instance.Reset();
					VoodooManager.Instance.Reset();
					AcrobatSpot[] array = Object.FindObjectsOfType<AcrobatSpot>();
					AcrobatSpot[] array2 = array;
					foreach (AcrobatSpot acrobatSpot in array2)
					{
						((SimulationBehaviour)gameState).Runner.Despawn(((Component)acrobatSpot).GetComponent<NetworkObject>(), false);
					}
				}
				catch (Exception ex)
				{
					Plugin.Logger.LogError((object)("Pregame onEnter error: " + ex));
				}
			}
			foreach (PlayerCustom allPlayer10 in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer10.UpdateVisibility();
				((Component)allPlayer10.PlayerController).GetComponent<PlayerResurrectedComponent>()?.UpdateState();
				((Component)allPlayer10.PlayerController).GetComponent<PlayerSpotterLightComponent>().UpdateState();
			}
		});
		StateHooks<EGameState> obj6 = value[(EGameState)3];
		obj6.onEnter = (Action<EGameState>)Delegate.Combine(obj6.onEnter, (Action<EGameState>)delegate
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Unknown result type (might be due to invalid IL or missing references)
			//IL_020e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_017e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0183: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Unknown result type (might be due to invalid IL or missing references)
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Expected O, but got Unknown
			//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
			LycansUtility.AddLogOnlyForMe("Transition Enter");
			UIManager.HideAllExtraUI();
			if (!NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
			{
				if (GameManagerCustom.Instance.EventsManager.CurrentEvent != EventsManager.EventType.Tournament && GameManagerCustom.Instance.EventsManager.CurrentEvent != EventsManager.EventType.Vengeance)
				{
					GameManagerCustom.Rpc_New_Event(((SimulationBehaviour)gameState).Runner, 0);
				}
				foreach (PlayerCustom allPlayer11 in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer11.UpdateVisibility();
				}
			}
			if (((SimulationBehaviour)gameState).Runner.IsServer && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
			{
				List<PlayerCustom> specificSecondaryRoles = PlayerCustomRegistry.GetSpecificSecondaryRoles(PlayerCustom.PlayerSecondaryRole.BothPolitician);
				foreach (PlayerCustom item11 in specificSecondaryRoles)
				{
					if (item11.SecondaryRoleTargetRef != PlayerRef.None)
					{
						PlayerCustom politicianTargetCustom = PlayerCustomRegistry.GetPlayer(item11.SecondaryRoleTargetRef);
						if (!NetworkBool.op_Implicit(politicianTargetCustom.PlayerController.IsDead) && !politicianTargetCustom.IsOutOfTheWorld)
						{
							politicianTargetCustom.PoliticianVictimAlltime = NetworkBool.op_Implicit(true);
							NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.EffectDowned");
							((SimulationBehaviour)politicianTargetCustom).Runner.Spawn(networkObject, (Vector3?)Vector3.zero, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
							{
								((Component)no).GetComponent<CustomEffect>().InitWithSpecificDuration(politicianTargetCustom.PlayerController, 300f);
							}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
							GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)gameState).Runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)politicianTargetCustom.PlayerController).transform.position, 30f, 1f);
						}
						item11.SecondaryRoleTargetRef = PlayerRef.None;
					}
				}
				foreach (PlayerCustom item12 in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.DeceiverTrickThisMeeting) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)))
				{
					List<string> list = new List<string> { "LycansNewRoles.EffectConfused", "Paranoia", "Mute" };
					string text = CollectionsUtil.Grab<string>(list, 1).First();
					switch (text)
					{
					case "LycansNewRoles.EffectConfused":
						PlayerCustom.ApplyEffectToPlayer(item12.PlayerController, text, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
						break;
					case "Paranoia":
					{
						Effect val2 = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is ParanoiaEffect);
						if ((Object)(object)val2 != (Object)null)
						{
							PlayerCustom.ApplyEffectToPlayer(item12.PlayerController, val2, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
						}
						break;
					}
					case "Mute":
					{
						Effect val = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is MuteEffect);
						if ((Object)(object)val != (Object)null)
						{
							PlayerCustom.ApplyEffectToPlayer(item12.PlayerController, val, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
						}
						break;
					}
					}
					if (Random.value < 0.2f && NetworkBool.op_Implicit(Plugin.CustomConfig.DeceiverHasFlatulenceChance))
					{
						Effect val3 = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is FlatulenceEffect);
						if ((Object)(object)val3 != (Object)null)
						{
							PlayerCustom.ApplyEffectToPlayer(item12.PlayerController, val3, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
						}
					}
				}
			}
		});
		StateHooks<EGameState> obj7 = value[(EGameState)3];
		obj7.onExit = (Action<EGameState>)Delegate.Combine(obj7.onExit, (Action<EGameState>)delegate
		{
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_061f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0630: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_064f: Unknown result type (might be due to invalid IL or missing references)
			//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0813: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Unknown result type (might be due to invalid IL or missing references)
			//IL_034c: Unknown result type (might be due to invalid IL or missing references)
			//IL_074d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_037c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0442: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0453: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Unknown result type (might be due to invalid IL or missing references)
			//IL_0546: Unknown result type (might be due to invalid IL or missing references)
			//IL_0557: Unknown result type (might be due to invalid IL or missing references)
			//IL_0568: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02de: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
			LycansUtility.AddLogOnlyForMe("Transition Exit, players count: " + GameManager.Instance.PlayerCount);
			if (((SimulationBehaviour)gameState).Runner.IsServer)
			{
				if (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
				{
					LycansUtility.AddLogOnlyForMe("Check for mercenary win");
					PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Mercenary);
					if ((Object)(object)specificNewPrimaryRole != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole.PlayerController.IsDead) && NetworkBool.op_Implicit(specificNewPrimaryRole.NewPrimaryRoleUniqueBool))
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole.Index);
						return;
					}
					foreach (PlayerController item13 in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => !NetworkBool.op_Implicit(p.IsDead))).ToList())
					{
						PlayerCustom player = PlayerCustomRegistry.GetPlayer(item13.Ref);
						LycansUtility.AddLogOnlyForMe("Starvation effect");
						if (player.StarvationDormant)
						{
							player.StarvationDormant = false;
							PlayerCustom.ApplyEffectToPlayer(player.PlayerController, "LycansNewRoles.EffectStarvationActive", ((SimulationBehaviour)__instance).Runner);
						}
						LycansUtility.AddLogOnlyForMe("Curse timer");
						if (NetworkBool.op_Implicit(player.CurseDormant))
						{
							player.CurseTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)gameState).Runner, Random.Range(1f, 20f));
						}
						LycansUtility.AddLogOnlyForMe("Bomb timer");
						if (NetworkBool.op_Implicit(player.BombDormant))
						{
							player.BombTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)gameState).Runner, Random.Range(5f, 10f));
						}
						LycansUtility.AddLogOnlyForMe("Avatar protection");
						if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Avatar && !NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool))
						{
							player.ProtectedPriest = NetworkBool.op_Implicit(true);
						}
						LycansUtility.AddLogOnlyForMe("Village idiot boredom");
						if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.VillageIdiot)
						{
							player.PrimaryRolePowerCurrentMaterials = 2500;
						}
						LycansUtility.AddLogOnlyForMe("Spotter radar");
						if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Spotter && (Object)(object)player.PlayerController.Item == (Object)null)
						{
							Item[] value2 = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
							Item val = value2.FirstOrDefault((Item o) => o is RadarItem);
							if ((Object)(object)val == (Object)null)
							{
								return;
							}
							Item val2 = ItemUtility.SpawnItem(val, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)gameState).Runner);
							val2.Rpc_ClaimItem(player.Ref);
						}
					}
					LycansUtility.AddLogOnlyForMe("Deceiver illusion timer");
					PlayerCustom specificPrimaryRolePower = PlayerCustomRegistry.GetSpecificPrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Deceiver);
					if ((Object)(object)specificPrimaryRolePower != (Object)null && !NetworkBool.op_Implicit(specificPrimaryRolePower.PlayerController.IsDead))
					{
						specificPrimaryRolePower.PrimaryRoleActionTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)gameState).Runner, Random.Range(2f, 5f));
					}
					LycansUtility.AddLogOnlyForMe("Beast");
					PlayerCustom beastCustom = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Beast);
					if (Object.op_Implicit((Object)(object)beastCustom) && !NetworkBool.op_Implicit(beastCustom.PlayerController.IsDead) && !NetworkBool.op_Implicit(beastCustom.Kidnapped) && !NetworkBool.op_Implicit(CultistManager.Instance.CultistActive) && PlayerCustomRegistry.AllPlayers.All((PlayerCustom o) => o.Ref == beastCustom.Ref || NetworkBool.op_Implicit(o.PlayerController.IsDead) || o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie || NetworkBool.op_Implicit(o.BeastMark) || NetworkBool.op_Implicit(o.Kidnapped)))
					{
						BeastManager.Instance.ActivateBeast();
					}
					LycansUtility.AddLogOnlyForMe("Zombie rez");
					PlayerCustom specificNewPrimaryRole2 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Voodoo);
					if ((Object)(object)specificNewPrimaryRole2 != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole2.PlayerController.IsDead) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
					{
						foreach (PlayerCustom item14 in PlayerCustomRegistry.Where((PlayerCustom playerCustom) => playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie).ToList())
						{
							item14.PlayerController.IsDead = NetworkBool.op_Implicit(false);
							item14.PlayerController.IsAiming = NetworkBool.op_Implicit(false);
							item14.PlayerController.IsDeadChannel = NetworkBool.op_Implicit(false);
							item14.PlayerController.Hunger = GameManager.Instance.MaxHunger;
						}
					}
					if (GameManagerCustom.Instance.EventsManager.CurrentEvent != EventsManager.EventType.None)
					{
						GameManagerCustom.Instance.EventsManager.OnNightStarted();
					}
					if (NetworkBool.op_Implicit(Plugin.CustomConfig.ForceWolfTransformation) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && !NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
					{
						foreach (PlayerCustom item15 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (int)o.PlayerController.Role == 1 && o.WolfDaysWithoutTransformation >= 3))
						{
							item15.ForcedTransformation = NetworkBool.op_Implicit(true);
							PlayerCustom.ApplyEffectToPlayer(item15.PlayerController, "LycansNewRoles.EffectTransformation", ((SimulationBehaviour)gameState).Runner, 1f, 20f);
						}
					}
				}
				LycansUtility.AddLogOnlyForMe("Sabotages");
				if (NetworkBool.op_Implicit(Plugin.CustomConfig.SabotagesAvailable) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					if (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
					{
						List<SabotageSingle> list = SabotageManager.Instance.Sabotages.Values.Where((SabotageSingle o) => o.CanActivate || o.Completed).ToList();
						List<SabotageSingle> list2 = new List<SabotageSingle>();
						foreach (SabotageSingle item16 in list)
						{
							if (item16.Completed)
							{
								list2.Add(item16);
							}
							else
							{
								float num = (float)item16.AmountCurrent / (float)item16.AmountRequired;
								if (Random.value < num)
								{
									list2.Add(item16);
								}
								else
								{
									item16.CanActivate = false;
								}
							}
						}
						if (list2.Any())
						{
							SabotageSingle sabotageSingle = CollectionsUtil.Grab<SabotageSingle>(list2, 1).First();
							sabotageSingle.Active = NetworkBool.op_Implicit(true);
						}
					}
					else
					{
						List<SabotageSingle> list3 = SabotageManager.Instance.Sabotages.Values.Where((SabotageSingle o) => NetworkBool.op_Implicit(o.Active)).ToList();
						foreach (SabotageSingle item17 in list3)
						{
							item17.Active = NetworkBool.op_Implicit(false);
						}
					}
				}
				LycansUtility.AddLogOnlyForMe("Spy timer");
				PlayerCustom specificNewPrimaryRole3 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Spy);
				if ((Object)(object)specificNewPrimaryRole3 != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole3.PlayerController.IsDead))
				{
					specificNewPrimaryRole3.PrimaryRoleTargetRef = PlayerRef.None;
				}
			}
			foreach (PlayerCustom item18 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead)))
			{
				item18.UpdateVisibility();
				item18.SoloRoleHalfDayProgress = 0f;
			}
			HostParasite.UpdateVisibilityForAllParasites();
		});
	}

	private static void ManageSoloRolesHunt(GameState gameState)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Mercenary);
		if ((Object)(object)specificNewPrimaryRole != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole.PlayerController.IsDead))
		{
			if (specificNewPrimaryRole.SoloRoleObjectiveCount >= specificNewPrimaryRole.SoloRoleObjectiveTarget && !NetworkBool.op_Implicit(specificNewPrimaryRole.Kidnapped))
			{
				specificNewPrimaryRole.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
				PlayerCustom.ApplyEffectToPlayer(specificNewPrimaryRole.PlayerController, "LycansNewRoles.EffectEscaping", ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
				PlayerCustom.ApplyEffectToPlayer(specificNewPrimaryRole.PlayerController, "LycansNewRoles.EffectSneaky", ((SimulationBehaviour)gameState).Runner, 1f, 10f);
				return;
			}
			specificNewPrimaryRole.SoloRoleObjectiveTarget = BalancingValues.MercenaryTotalObjective(GameManager.Instance.LootSpawnRate, (float)Plugin.CustomConfig.MercenaryPercentage * 0.01f, GameManager.Instance.DayDuration + GameManager.Instance.NightDuration, GameManagerCustom.Instance.SoloRoleDifficulty);
		}
		PlayerCustom specificNewPrimaryRole2 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Cultist);
		if ((Object)(object)specificNewPrimaryRole2 != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole2.PlayerController.IsDead) && specificNewPrimaryRole2.SoloRoleObjectiveCount >= 10000)
		{
			specificNewPrimaryRole2.SoloRoleObjectiveCount = 0;
			CultistManager.Instance.ActivateCultist();
			specificNewPrimaryRole2.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
			specificNewPrimaryRole2.TeleportToVoid();
			specificNewPrimaryRole2.CurseDormant = NetworkBool.op_Implicit(false);
			specificNewPrimaryRole2.BombDormant = NetworkBool.op_Implicit(false);
			specificNewPrimaryRole2.PlayerController.PlayerEffectManager.ClearEffects();
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectCultistSpirit");
			Vector3 position = Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value[GameManager.Instance.MapID - 1].position;
			NetworkObject val = ((SimulationBehaviour)gameState).Runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
			{
				//IL_0008: Unknown result type (might be due to invalid IL or missing references)
				((Component)no).transform.position = position;
			}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			((Component)val).transform.position = position;
			((Component)val).GetComponent<PlayerSummonedSpiritComponent>().Init(specificNewPrimaryRole2.Ref);
			PlayerCustom.ApplyEffectToPlayer(specificNewPrimaryRole2.PlayerController, "LycansNewRoles.EffectParalyzed", ((SimulationBehaviour)gameState).Runner, 1f, 8f);
		}
	}
}
