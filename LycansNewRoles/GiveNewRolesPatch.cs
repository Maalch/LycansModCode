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
			//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ab: Invalid comparison between Unknown and I4
			//IL_11c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_13cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_13e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_13e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1400: Unknown result type (might be due to invalid IL or missing references)
			//IL_1405: Unknown result type (might be due to invalid IL or missing references)
			//IL_1435: Unknown result type (might be due to invalid IL or missing references)
			//IL_027b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0464: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_030a: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0343: Unknown result type (might be due to invalid IL or missing references)
			//IL_0524: Unknown result type (might be due to invalid IL or missing references)
			//IL_053c: Unknown result type (might be due to invalid IL or missing references)
			//IL_066d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d48: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c44: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f90: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f95: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fd2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_105b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1060: Unknown result type (might be due to invalid IL or missing references)
			if ((int)state == 1)
			{
				GameManagerCustom.Instance.NewGame();
			}
			if (((SimulationBehaviour)gameState).Runner.IsServer && (int)state == 1)
			{
				try
				{
					SessionStats.Stats.NewGame();
					Plugin.Logger.LogInfo((object)"Game start: init");
					List<PlayerCustom> list = (from t in PlayerCustomRegistry.Where(delegate(PlayerCustom p)
						{
							//IL_0001: Unknown result type (might be due to invalid IL or missing references)
							//IL_0006: Unknown result type (might be due to invalid IL or missing references)
							PlayerRef val5 = p.Ref;
							return ((PlayerRef)(ref val5)).IsValid;
						})
						select (t)).ToList();
					foreach (PlayerCustom item in list)
					{
						item.Reset();
					}
					if (!NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale) && !NetworkBool.op_Implicit(Plugin.CustomConfig.DraftMode))
					{
						Plugin.Logger.LogInfo((object)"Game start: remove hunter and alchemist");
						IEnumerable<PlayerController> enumerable = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => (int)o.Role == 2 || (int)o.Role == 3));
						foreach (PlayerController item2 in enumerable)
						{
							item2.Role = (PlayerRole)0;
						}
						Plugin.Logger.LogInfo((object)"Game start: solo roles");
						List<PlayerCustom.PlayerNewPrimaryRole> list2 = (from o in Plugin.CustomConfig.SoloRoleActive
							where o.Key != PlayerCustom.PlayerNewPrimaryRole.None && o.Key != PlayerCustom.PlayerNewPrimaryRole.Traitor && o.Value && !PlayerCustom.IsNewPrimaryRoleDisabled(o.Key)
							select o.Key).ToList();
						List<PlayerController> list3 = (from t in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => (int)p.Role == 0 && PlayerCustomRegistry.GetPlayer(p.Ref).NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None))
							select (t)).ToList();
						for (int num = 0; num < Plugin.CustomConfig.SoloRolesCount; num++)
						{
							if (list2.Any() && list3.Any())
							{
								PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = CollectionsUtil.Grab<PlayerCustom.PlayerNewPrimaryRole>(list2, 1).First();
								PlayerController val = CollectionsUtil.Grab<PlayerController>(list3, 1).First();
								PlayerCustom player = PlayerCustomRegistry.GetPlayer(val.Ref);
								player.GiveNewPrimaryRole(playerNewPrimaryRole);
								list2.Remove(playerNewPrimaryRole);
								list3.Remove(val);
								if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent && list3.Any())
								{
									val = CollectionsUtil.Grab<PlayerController>(list3, 1).First();
									player = PlayerCustomRegistry.GetPlayer(val.Ref);
									player.GiveNewPrimaryRole(playerNewPrimaryRole);
									list3.Remove(val);
									list2.Remove(PlayerCustom.PlayerNewPrimaryRole.Lover);
								}
								if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
								{
									if (NetworkBool.op_Implicit(Plugin.CustomConfig.LoverWolfReplacesVillager))
									{
										if (list3.Any())
										{
											val = CollectionsUtil.Grab<PlayerController>(list3, 1).First();
											val.Role = (PlayerRole)1;
											player = PlayerCustomRegistry.GetPlayer(val.Ref);
											player.GiveNewPrimaryRole(playerNewPrimaryRole);
											list3.Remove(val);
										}
									}
									else
									{
										val = CollectionsUtil.Grab<PlayerController>(PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => (int)o.Role == 1)).ToList(), 1).First();
										player = PlayerCustomRegistry.GetPlayer(val.Ref);
										player.GiveNewPrimaryRole(playerNewPrimaryRole);
									}
									list2.Remove(PlayerCustom.PlayerNewPrimaryRole.Agent);
								}
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: traitor");
						List<PlayerController> list4 = (from t in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => (int)p.Role == 0 && PlayerCustomRegistry.GetPlayer(p.Ref).NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None))
							select (t)).ToList();
						for (int num2 = 0; num2 < Plugin.CustomConfig.TraitorsCount; num2++)
						{
							if (list4.Any())
							{
								PlayerController val2 = CollectionsUtil.Grab<PlayerController>(list4, 1).First();
								PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(val2.Ref);
								player2.GiveNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Traitor);
								list4.Remove(val2);
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: wolf pup");
						List<PlayerController> list5 = (from t in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => (int)p.Role == 0 && PlayerCustomRegistry.GetPlayer(p.Ref).NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None))
							select (t)).ToList();
						for (int num3 = 0; num3 < Plugin.CustomConfig.WolfPupsCount; num3++)
						{
							if (list5.Any())
							{
								PlayerController val3 = CollectionsUtil.Grab<PlayerController>(list5, 1).First();
								PlayerCustom player3 = PlayerCustomRegistry.GetPlayer(val3.Ref);
								val3.Role = (PlayerRole)1;
								player3.IsWolfPup = NetworkBool.op_Implicit(true);
								list5.Remove(val3);
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: wolves and traitor powers");
						List<PlayerCustom.PlayerPrimaryRolePower> list6 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
							where o.Value && PlayerCustom.IsPrimaryRolePowerForWolves(o.Key) && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key)
							select o.Key).ToList();
						List<PlayerController> list7 = (from t in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => ((int)p.Role == 1 || PlayerCustomRegistry.GetPlayer(p.Ref).NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor) && PlayerCustomRegistry.GetPlayer(p.Ref).PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.None && PlayerCustomRegistry.GetPlayer(p.Ref).NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover))
							select (t)).ToList();
						for (int num4 = 0; num4 < Plugin.CustomConfig.WolfPowersCount; num4++)
						{
							if (list7.Any() && list6.Any())
							{
								PlayerController player4 = CollectionsUtil.Grab<PlayerController>(list7, 1).First();
								PlayerCustom player5 = PlayerCustomRegistry.GetPlayer(player4.Ref);
								List<PlayerCustom.PlayerPrimaryRolePower> list8 = list6.Where((PlayerCustom.PlayerPrimaryRolePower o) => (int)player4.Role == 1 || PlayerCustom.IsWolfPowerAvailableForTraitor(o)).ToList();
								if (list8.Any())
								{
									PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list8, 1).First();
									player5.GivePrimaryRolePower(playerPrimaryRolePower);
									list6.Remove(playerPrimaryRolePower);
									list7.Remove(player4);
								}
								else
								{
									list7.Remove(player4);
								}
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: elites");
						List<PlayerCustom> list9 = PlayerCustomRegistry.Where((PlayerCustom o) => (int)o.PlayerController.Role == 0 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).ToList();
						List<PlayerCustom.PlayerPrimaryRolePower> list10 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
							where PlayerCustom.IsPrimaryRolePowerForEliteVillagers(o.Key) && o.Value && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key)
							select o.Key).ToList();
						for (int num5 = 0; num5 < Plugin.CustomConfig.ElitesCount; num5++)
						{
							if (list9.Any() && list10.Any())
							{
								PlayerCustom playerCustom = CollectionsUtil.Grab<PlayerCustom>(list9, 1).First();
								PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower2 = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list10, 1).First();
								playerCustom.GivePrimaryRolePower(playerPrimaryRolePower2);
								list10.Remove(playerPrimaryRolePower2);
								list9.Remove(playerCustom);
							}
						}
						Plugin.Logger.LogInfo((object)"Game start: villager powers");
						List<PlayerCustom.PlayerPrimaryRolePower> list11 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
							where o.Value && PlayerCustom.IsPrimaryRolePowerForNormalVillagers(o.Key) && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key)
							select o.Key).ToList();
						List<PlayerCustom.PlayerPrimaryRolePower> list12 = new List<PlayerCustom.PlayerPrimaryRolePower>();
						foreach (PlayerCustom.PlayerPrimaryRolePower item3 in list11)
						{
							int villagerJobChancePonderation = BalancingValues.GetVillagerJobChancePonderation(item3);
							for (int num6 = 0; num6 < villagerJobChancePonderation; num6++)
							{
								list12.Add(item3);
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
							PlayerCustom playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(list9, 1).FirstOrDefault();
							playerCustom2.GivePrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Avatar);
						}
						Plugin.Logger.LogInfo((object)"Game start: mole");
						if (Random.value * 100f < (float)Plugin.CustomConfig.MoleChance && list9.Any())
						{
							PlayerCustom playerCustom3 = CollectionsUtil.Grab<PlayerCustom>(list9, 1).FirstOrDefault();
							playerCustom3.GivePrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.Mole);
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
							float num7 = Mathf.Min(1f, (float)Plugin.CustomConfig.SecondaryRolesCount / (float)list13.Count) * 0.5f;
							if (Random.value < num7)
							{
								List<PlayerCustom> source = PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => PlayerCustom.GetAvailableSecondaryRoles(o.PlayerController.Role, o.NewPrimaryRole, o.PrimaryRolePower).Contains(PlayerCustom.PlayerSecondaryRole.BothTelepath)).ToList();
								List<PlayerCustom> list14 = source.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor || (int)o.PlayerController.Role == 1).ToList();
								List<PlayerCustom> list15 = source.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None && (int)o.PlayerController.Role != 1).ToList();
								if (list14.Count >= 2 && Random.value < 0.33f)
								{
									List<PlayerCustom> list16 = CollectionsUtil.Grab<PlayerCustom>(list14, 2).ToList();
									foreach (PlayerCustom item4 in list16)
									{
										PlayerCustomRegistry.GetPlayer(item4.Ref).GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.BothTelepath);
										list13.Remove(item4.PlayerController);
									}
								}
								else if (list15.Count >= 2)
								{
									List<PlayerCustom> list17 = CollectionsUtil.Grab<PlayerCustom>(list15, 2).ToList();
									foreach (PlayerCustom item5 in list17)
									{
										PlayerCustomRegistry.GetPlayer(item5.Ref).GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.BothTelepath);
										list13.Remove(item5.PlayerController);
									}
								}
							}
						}
						availableSecondaryRoles.Remove(PlayerCustom.PlayerSecondaryRole.BothTelepath);
						for (int num8 = 0; num8 < Plugin.CustomConfig.SecondaryRolesCount; num8++)
						{
							if (availableSecondaryRoles.Any() && list13.Any())
							{
								PlayerController val4 = CollectionsUtil.Grab<PlayerController>(list13, 1).First();
								PlayerCustom player6 = PlayerCustomRegistry.GetPlayer(val4.Ref);
								List<PlayerCustom.PlayerSecondaryRole> availableSecondaryRoles2 = PlayerCustom.GetAvailableSecondaryRoles(val4.Role, player6.NewPrimaryRole, player6.PrimaryRolePower);
								if (player6.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor)
								{
									availableSecondaryRoles2.RemoveAll((PlayerCustom.PlayerSecondaryRole o) => PlayerCustom.IsSecondaryRoleDisabledForTraitor(o));
								}
								availableSecondaryRoles2 = availableSecondaryRoles2.Where((PlayerCustom.PlayerSecondaryRole o) => availableSecondaryRoles.Contains(o)).ToList();
								if (availableSecondaryRoles2.Any())
								{
									PlayerCustom.PlayerSecondaryRole role = CollectionsUtil.Grab<PlayerCustom.PlayerSecondaryRole>(availableSecondaryRoles2, 1).First();
									player6.GiveSecondaryRole(role);
									list13.Remove(val4);
								}
								else
								{
									list13.Remove(val4);
								}
							}
						}
						foreach (PlayerCustom item6 in PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.None))
						{
							item6.GivePrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower.None);
						}
						foreach (PlayerCustom item7 in PlayerCustomRegistry.Where((PlayerCustom o) => o.SecondaryRole == PlayerCustom.PlayerSecondaryRole.None))
						{
							item7.GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.None);
						}
						SabotageManager.Instance.Init();
						BeastManager.Instance.Reset();
						CultistManager.Instance.Reset();
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
							allPlayer2.InitStats();
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
					PlayerCustom player7 = PlayerCustomRegistry.GetPlayer(local.Ref);
					if (!PlayerPrefs.HasKey("NALES_TUTORIAL_SHOW_DESCRIPTION") && (player7.SecondaryRole != PlayerCustom.PlayerSecondaryRole.None || player7.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None || player7.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None))
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
						allPlayer3.UpdateColor();
						allPlayer3.UpdatePet();
						Traverse.Create((object)allPlayer3.PlayerController.CharacterMovementHandler).Field<NetworkCharacterControllerPrototypeCustom>("_networkCharacterControllerPrototypeCustom").Value.gravity = -40f * BalancingValues.GravityMultiplier(GameManager.Instance.MapID);
						allPlayer3.PlacedSleepingGas = null;
					}
					Dictionary<PlayerRef, PlayerDisplay> value2 = Traverse.Create((object)GameManager.Instance.gameUI).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
					foreach (KeyValuePair<PlayerRef, PlayerDisplay> item8 in value2)
					{
						TextMeshProUGUI value3 = Traverse.Create((object)item8.Value).Field<TextMeshProUGUI>("username").Value;
						PlayerController player8 = PlayerRegistry.GetPlayer(item8.Key);
						if ((Object)(object)player8 != (Object)null)
						{
							NetworkPlayerData playerData = player8.PlayerData;
							if (((NetworkPlayerData)(ref playerData)).IsValid)
							{
								playerData = player8.PlayerData;
								((TMP_Text)value3).text = ((object)playerData.Username/*cast due to constrained. prefix*/).ToString();
								((Graphic)value3).color = new Color(255f, 255f, 255f, 0.6f);
							}
						}
					}
					List<GameObject> list18 = (from o in Object.FindObjectsOfType<MinimapDeathPositionComponent>(true)
						select ((Component)o).gameObject).ToList();
					list18.AddRange(from o in Object.FindObjectsOfType<MinimapDetectivePositionComponent>(true)
						select ((Component)o).gameObject);
					foreach (GameObject item9 in list18)
					{
						Object.Destroy((Object)(object)item9);
					}
					UIManager.LastGameSummaryPanel.Clear();
					Plugin.CreatePlayerIllusionIfNeeded();
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
			//IL_0461: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
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
			//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0404: Unknown result type (might be due to invalid IL or missing references)
			//IL_040e: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0291: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
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
						}
						PlayerCustom playerCustom3 = (from o in PlayerCustomRegistry
							where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld
							orderby o.LootCollectedTodayDuringDay
							select o).FirstOrDefault();
						if ((Object)(object)playerCustom3 != (Object)null)
						{
							PlayerCustom.ApplyEffectToPlayer(playerCustom3.PlayerController, "LycansNewRoles.EffectTournamentLoser", ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
							PlayerCustom.ApplyEffectToPlayer(playerCustom3.PlayerController, EffectManager.GetEffects().First((Effect o) => o is GlowingEffect), ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
						}
						GameManagerCustom.Instance.EventsManager.ClearEvent();
					}
					else if ((NetworkBool.op_Implicit(Plugin.CustomConfig.AllowMayor) && (GameManagerCustom.Instance.CurrentMayor == PlayerRef.None || NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(GameManagerCustom.Instance.CurrentMayor).PlayerController.IsDead))) || NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(GameManagerCustom.Instance.CurrentMayor).Kidnapped))
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
			LycansUtility.AddLogOnlyForMe("Player stuff");
			foreach (PlayerCustom allPlayer5 in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer5.UpdateVisibility();
				allPlayer5.UpdateIllusion();
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
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Invalid comparison between Unknown and I4
			//IL_0116: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Invalid comparison between Unknown and I4
			if (((SimulationBehaviour)gameState).Runner.IsServer && !NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
			{
				try
				{
					PlayerRegistry.ForEach((Action<PlayerController>)delegate(PlayerController player)
					{
						//IL_0011: Unknown result type (might be due to invalid IL or missing references)
						//IL_007a: Unknown result type (might be due to invalid IL or missing references)
						//IL_0109: Unknown result type (might be due to invalid IL or missing references)
						//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
						//IL_014c: Unknown result type (might be due to invalid IL or missing references)
						//IL_0159: Unknown result type (might be due to invalid IL or missing references)
						//IL_0165: Unknown result type (might be due to invalid IL or missing references)
						//IL_016a: Unknown result type (might be due to invalid IL or missing references)
						//IL_016c: Unknown result type (might be due to invalid IL or missing references)
						//IL_0171: Unknown result type (might be due to invalid IL or missing references)
						//IL_0173: Unknown result type (might be due to invalid IL or missing references)
						//IL_0178: Unknown result type (might be due to invalid IL or missing references)
						//IL_0136: Unknown result type (might be due to invalid IL or missing references)
						//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
						//IL_0216: Unknown result type (might be due to invalid IL or missing references)
						//IL_0254: Unknown result type (might be due to invalid IL or missing references)
						//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
						//IL_0502: Invalid comparison between Unknown and I4
						//IL_058f: Unknown result type (might be due to invalid IL or missing references)
						//IL_0505: Unknown result type (might be due to invalid IL or missing references)
						//IL_059c: Unknown result type (might be due to invalid IL or missing references)
						//IL_054a: Unknown result type (might be due to invalid IL or missing references)
						//IL_0550: Invalid comparison between Unknown and I4
						//IL_0534: Unknown result type (might be due to invalid IL or missing references)
						//IL_0553: Unknown result type (might be due to invalid IL or missing references)
						//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
						//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
						//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
						//IL_060d: Unknown result type (might be due to invalid IL or missing references)
						//IL_0620: Unknown result type (might be due to invalid IL or missing references)
						//IL_0625: Unknown result type (might be due to invalid IL or missing references)
						//IL_0631: Unknown result type (might be due to invalid IL or missing references)
						//IL_0636: Unknown result type (might be due to invalid IL or missing references)
						//IL_0702: Unknown result type (might be due to invalid IL or missing references)
						//IL_0708: Invalid comparison between Unknown and I4
						//IL_070b: Unknown result type (might be due to invalid IL or missing references)
						//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
						//IL_06cd: Invalid comparison between Unknown and I4
						//IL_0693: Unknown result type (might be due to invalid IL or missing references)
						//IL_0718: Unknown result type (might be due to invalid IL or missing references)
						//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
						//IL_074c: Unknown result type (might be due to invalid IL or missing references)
						//IL_078f: Unknown result type (might be due to invalid IL or missing references)
						//IL_0771: Unknown result type (might be due to invalid IL or missing references)
						//IL_0782: Unknown result type (might be due to invalid IL or missing references)
						if ((Object)(object)player != (Object)null)
						{
							PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(player.Ref);
							if (player2.PrimaryRolePowerCooldownTimerTicksBeforeMeeting.HasValue)
							{
								int value2 = player2.PrimaryRolePowerCooldownTimerTicksBeforeMeeting.Value;
								value2 += (int)(5f * (float)((SimulationBehaviour)gameState).Runner.Config.Simulation.TickRate);
								player2.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromTicks(((SimulationBehaviour)gameState).Runner, value2);
								player2.PrimaryRolePowerCooldownTimerTicksBeforeMeeting = null;
							}
							if (player2.SecondaryRolePowerCooldownTimerTicksBeforeMeeting.HasValue)
							{
								int value3 = player2.SecondaryRolePowerCooldownTimerTicksBeforeMeeting.Value;
								value3 += (int)(5f * (float)((SimulationBehaviour)gameState).Runner.Config.Simulation.TickRate);
								player2.SecondaryRolePowerCooldownTimer = TickTimer.CreateFromTicks(((SimulationBehaviour)gameState).Runner, value3);
								player2.SecondaryRolePowerCooldownTimerTicksBeforeMeeting = null;
							}
							if (NetworkBool.op_Implicit(player2.IsImitator) && player2.SecondaryRole != PlayerCustom.PlayerSecondaryRole.BothImitator)
							{
								player2.GiveSecondaryRole(PlayerCustom.PlayerSecondaryRole.BothImitator);
								player2.SecondaryRolePowerCooldownTimer = TickTimer.None;
								player2.SecondaryRoleFirstRemainingUses = 0;
							}
							player2.DeceiverTrickThisMeeting = NetworkBool.op_Implicit(false);
							player2.ProtectedPriest = NetworkBool.op_Implicit(false);
							NetworkBool weakened = player2.Weakened;
							NetworkBool empowered = player2.Empowered;
							NetworkBool tournamentWinner = player2.TournamentWinner;
							bool flag = player2.PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is TournamentLoser);
							player2.PlayerController.PlayerEffectManager.ClearEffects();
							player2.SleepStacks = 0;
							player2.RepulsionStacks = 0;
							player2.AlreadyAngeledToday = false;
							if (NetworkBool.op_Implicit(weakened))
							{
								PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, "LycansNewRoles.EffectWeakened", ((SimulationBehaviour)__instance).Runner, 1f, 3600f);
							}
							if (NetworkBool.op_Implicit(empowered))
							{
								PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, "LycansNewRoles.EffectEmpowered", ((SimulationBehaviour)__instance).Runner, 1f, 3600f);
							}
							if (NetworkBool.op_Implicit(tournamentWinner))
							{
								PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, "LycansNewRoles.EffectTournamentWinner", ((SimulationBehaviour)__instance).Runner, 1f, 3600f);
							}
							if (flag)
							{
								List<string> list = new List<string> { "LycansNewRoles.EffectNearsighted", "LycansNewRoles.EffectMute", "Paranoia", "Glowing", "Flatulences" };
								string text = CollectionsUtil.Grab<string>(list, 1).First();
								switch (text)
								{
								case "LycansNewRoles.EffectNearsighted":
								case "LycansNewRoles.EffectMute":
									PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, text, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
									break;
								case "Paranoia":
								{
									Effect val2 = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is ParanoiaEffect);
									if ((Object)(object)val2 != (Object)null)
									{
										PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, val2, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
									}
									break;
								}
								case "Glowing":
								{
									Effect val3 = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is GlowingEffect);
									if ((Object)(object)val3 != (Object)null)
									{
										PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, val3, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
									}
									break;
								}
								case "Flatulences":
								{
									Effect val = EffectManager.GetEffects().FirstOrDefault((Effect o) => o is FlatulenceEffect);
									if ((Object)(object)val != (Object)null)
									{
										PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, val, ((SimulationBehaviour)gameState).Runner, 1f, 3600f);
									}
									break;
								}
								}
							}
							switch (player2.PrimaryRolePower)
							{
							case PlayerCustom.PlayerPrimaryRolePower.Investigator:
								if ((int)state == 2 && !NetworkBool.op_Implicit(player.IsDead))
								{
									InvestigatorHint.CreateHintsOnNewDay(((SimulationBehaviour)gameState).Runner, player2);
									player2.PrimaryRoleTargetRef = PlayerRef.None;
									player2.InvestigatorGiveNewTarget();
								}
								break;
							case PlayerCustom.PlayerPrimaryRolePower.Hermit:
								if ((int)state == 2 && !NetworkBool.op_Implicit(player.IsDead))
								{
									HermitHideout.CreateHideoutsOnNewDay(((SimulationBehaviour)gameState).Runner, player2);
								}
								break;
							case PlayerCustom.PlayerPrimaryRolePower.Predator:
								player2.PrimaryRolePowerRemainingUses = 1;
								break;
							case PlayerCustom.PlayerPrimaryRolePower.Poacher:
								if (!NetworkBool.op_Implicit(player.IsDead) && !NetworkBool.op_Implicit(player.IsGunLoaded))
								{
									player.IsGunLoaded = NetworkBool.op_Implicit(true);
								}
								break;
							}
							switch (player2.NewPrimaryRole)
							{
							case PlayerCustom.PlayerNewPrimaryRole.Mercenary:
								if (!NetworkBool.op_Implicit(player.IsDead) && !NetworkBool.op_Implicit(player.IsGunLoaded))
								{
									player.IsGunLoaded = NetworkBool.op_Implicit(true);
								}
								break;
							case PlayerCustom.PlayerNewPrimaryRole.Kidnapper:
							{
								TickTimer primaryRolePowerCooldownTimer = player2.PrimaryRolePowerCooldownTimer;
								if (((TickTimer)(ref primaryRolePowerCooldownTimer)).IsRunning)
								{
									primaryRolePowerCooldownTimer = player2.PrimaryRolePowerCooldownTimer;
									if (!(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)gameState).Runner) < 15f))
									{
										break;
									}
								}
								player2.PrimaryRolePowerRemainingUses = 0;
								player2.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)gameState).Runner, 15f);
								break;
							}
							}
							switch (player2.SecondaryRole)
							{
							case PlayerCustom.PlayerSecondaryRole.BothPolitician:
								player2.SecondaryRoleFirstRemainingUses = 1;
								break;
							case PlayerCustom.PlayerSecondaryRole.BothMerchant:
								if ((int)state == 2 && !NetworkBool.op_Implicit(player.IsDead))
								{
									MerchantCoin.CreateCoinsOnNewDay(((SimulationBehaviour)gameState).Runner, player2);
								}
								break;
							}
							if ((int)state == 2 && !NetworkBool.op_Implicit(player.IsDead) && NetworkBool.op_Implicit(player2.BombDormant))
							{
								player2.BombTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)gameState).Runner, Random.Range(5f, 10f));
							}
							if ((Object)(object)player2.SummonedSpirit != (Object)null)
							{
								player2.SummonedSpirit.AttackCooldown = TickTimer.None;
								player2.SummonedSpirit.SpellCooldown = TickTimer.None;
							}
							player2.MayorVoteTarget = PlayerRef.None;
							player2.LootCollectedTodayDuringDay = 0;
							player2.SecondsTransformedOrNearTransformedWolfToday = 0;
						}
					});
					if ((int)state == 2)
					{
						ManageSoloRolesHunt(__instance);
					}
					if ((int)GameManager.LocalGameState != 5)
					{
						GameManagerCustom.Instance.EventsManager.RollEvent();
						GameManagerCustom.Instance.NewDay();
					}
				}
				catch (Exception ex)
				{
					Plugin.Logger.LogError((object)("Meeting onExit error: " + ex));
				}
			}
			LycansUtility.AddLogOnlyForMe("Other stuff after meeting");
			if (UIManager.GenericChoicePanel.Active)
			{
				UIManager.GenericChoicePanel.Hide();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
			if (PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Poacher && !NetworkBool.op_Implicit(PlayerController.Local.IsDead))
			{
				AudioManager.Play("RELOAD", (MixerTarget)2, 0.35f, 1f);
			}
			ShowRoleDescriptionPatch.NeedsUpdate = true;
		});
		StateHooks<EGameState> obj4 = value[(EGameState)5];
		obj4.onEnter = (Action<EGameState>)Delegate.Combine(obj4.onEnter, (Action<EGameState>)delegate
		{
			//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				SabotageManager.Instance.Clean();
				BeastManager.Instance.Reset();
				CultistManager.Instance.Reset();
				foreach (PlayerCustom allPlayer6 in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer6.HasZombieColor = false;
					allPlayer6.UpdateSkinColor();
					allPlayer6.UpdateVisibility();
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
					foreach (PlayerCustom allPlayer7 in PlayerCustomRegistry.AllPlayers)
					{
						allPlayer7.Reset();
					}
					BeastManager.Instance.Reset();
					CultistManager.Instance.Reset();
				}
				catch (Exception ex)
				{
					Plugin.Logger.LogError((object)("Pregame onEnter error: " + ex));
				}
			}
			foreach (PlayerCustom allPlayer8 in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer8.UpdateVisibility();
				((Component)allPlayer8.PlayerController).GetComponent<PlayerResurrectedComponent>()?.UpdateState();
				((Component)allPlayer8.PlayerController).GetComponent<PlayerSpotterLightComponent>().UpdateState();
			}
		});
		StateHooks<EGameState> obj6 = value[(EGameState)3];
		obj6.onEnter = (Action<EGameState>)Delegate.Combine(obj6.onEnter, (Action<EGameState>)delegate
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_017f: Expected O, but got Unknown
			//IL_0190: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
			LycansUtility.AddLogOnlyForMe("Transition Enter");
			UIManager.HideAllExtraUI();
			if (!NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && GameManagerCustom.Instance.EventsManager.CurrentEvent != EventsManager.EventType.Tournament && GameManagerCustom.Instance.EventsManager.CurrentEvent != EventsManager.EventType.Vengeance)
			{
				GameManagerCustom.Instance.EventsManager.ClearEvent();
			}
			if (((SimulationBehaviour)gameState).Runner.IsServer && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
			{
				List<PlayerCustom> specificSecondaryRoles = PlayerCustomRegistry.GetSpecificSecondaryRoles(PlayerCustom.PlayerSecondaryRole.BothPolitician);
				foreach (PlayerCustom item11 in specificSecondaryRoles)
				{
					if (item11.SecondaryRoleTargetRef != PlayerRef.None)
					{
						PlayerCustom politicianTargetCustom = PlayerCustomRegistry.GetPlayer(item11.SecondaryRoleTargetRef);
						if (!NetworkBool.op_Implicit(politicianTargetCustom.PlayerController.IsDead))
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
			//IL_0541: Unknown result type (might be due to invalid IL or missing references)
			//IL_0552: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0571: Unknown result type (might be due to invalid IL or missing references)
			//IL_068d: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_063f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Unknown result type (might be due to invalid IL or missing references)
			//IL_0347: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0434: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0464: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02de: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
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
					LycansUtility.AddLogOnlyForMe("Zombie rez");
					PlayerCustom specificNewPrimaryRole2 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Voodoo);
					if ((Object)(object)specificNewPrimaryRole2 != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole2.PlayerController.IsDead))
					{
						foreach (PlayerCustom item14 in PlayerCustomRegistry.Where((PlayerCustom playerCustom) => playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie).ToList())
						{
							item14.PlayerController.IsDead = NetworkBool.op_Implicit(false);
							item14.PlayerController.IsAiming = NetworkBool.op_Implicit(false);
							item14.PlayerController.IsDeadChannel = NetworkBool.op_Implicit(false);
							item14.PlayerController.Hunger = GameManager.Instance.MaxHunger;
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
					if (GameManagerCustom.Instance.EventsManager.CurrentEvent != EventsManager.EventType.None)
					{
						GameManagerCustom.Instance.EventsManager.OnNightStarted();
					}
				}
				LycansUtility.AddLogOnlyForMe("Sabotages");
				if (NetworkBool.op_Implicit(Plugin.CustomConfig.SabotagesAvailable) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					if (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
					{
						List<SabotageSingle> list = SabotageManager.Instance.Sabotages.Values.Where((SabotageSingle o) => o.Completed).ToList();
						if (list.Any())
						{
							SabotageSingle sabotageSingle = CollectionsUtil.Grab<SabotageSingle>(list, 1).First();
							sabotageSingle.Active = NetworkBool.op_Implicit(true);
						}
					}
					else
					{
						List<SabotageSingle> list2 = SabotageManager.Instance.Sabotages.Values.Where((SabotageSingle o) => NetworkBool.op_Implicit(o.Active)).ToList();
						foreach (SabotageSingle item15 in list2)
						{
							item15.Active = NetworkBool.op_Implicit(false);
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
			foreach (PlayerCustom item16 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead)))
			{
				item16.UpdateVisibility();
				item16.SoloRoleHalfDayProgress = 0f;
			}
		});
	}

	private static void ManageSoloRolesHunt(GameState gameState)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
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
			((Component)specificNewPrimaryRole2.PlayerController).transform.position = new Vector3(999f, 999f, 999f);
			specificNewPrimaryRole2.PlayerController.IsClimbing = NetworkBool.op_Implicit(false);
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
			PlayerCustom.ApplyEffectToPlayer(specificNewPrimaryRole2.PlayerController, "LycansNewRoles.EffectParalyzed", ((SimulationBehaviour)gameState).Runner, 1f, 6f);
		}
	}
}
