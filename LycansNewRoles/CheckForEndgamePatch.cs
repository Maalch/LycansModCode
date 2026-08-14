using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "CheckForEndGame")]
internal class CheckForEndgamePatch
{
	private static bool Prefix(GameManager __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Invalid comparison between Unknown and I4
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Invalid comparison between Unknown and I4
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Invalid comparison between Unknown and I4
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Invalid comparison between Unknown and I4
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Invalid comparison between Unknown and I4
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Invalid comparison between Unknown and I4
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0870: Invalid comparison between Unknown and I4
		//IL_0949: Unknown result type (might be due to invalid IL or missing references)
		//IL_094f: Invalid comparison between Unknown and I4
		//IL_0a74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Invalid comparison between Unknown and I4
		//IL_09a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Invalid comparison between Unknown and I4
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1b: Invalid comparison between Unknown and I4
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac2: Invalid comparison between Unknown and I4
		//IL_0aa9: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(__instance.IsStarted) && !NetworkBool.op_Implicit(__instance.IsFinished))
			{
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: voodoo hunt");
				if (NetworkBool.op_Implicit(VoodooManager.Instance.VoodooTriggered))
				{
					PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Voodoo);
					if ((Object)(object)specificNewPrimaryRole != (Object)null)
					{
						if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Voodoo && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie) == 0)
						{
							PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole.Index);
							return false;
						}
						if (!NetworkBool.op_Implicit(VoodooManager.Instance.VoodooActive))
						{
							PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Voodoo && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie).First().Index);
						}
						return false;
					}
				}
				if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) || NetworkBool.op_Implicit(CultistManager.Instance.CultistActive) || NetworkBool.op_Implicit(VoodooManager.Instance.VoodooActive))
				{
					return false;
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: village idiot");
				PlayerCustom specificNewPrimaryRole2 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.VillageIdiot);
				if ((Object)(object)specificNewPrimaryRole2 != (Object)null)
				{
					PlayerController playerController = specificNewPrimaryRole2.PlayerController;
					if (NetworkBool.op_Implicit(playerController.IsDead) && NetworkBool.op_Implicit(specificNewPrimaryRole2.RoleDeathUniqueBool))
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole2.Index);
						return false;
					}
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: agent");
				if ((int)GameManager.LocalGameState == 4 && PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) <= BalancingValues.AgentMaxSurvivorsToWin(PlayerRegistry.Count))
				{
					IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent);
					if (enumerable.Any())
					{
						int num = 0;
						foreach (PlayerCustom item in enumerable)
						{
							PlayerController playerController2 = item.PlayerController;
							if (!NetworkBool.op_Implicit(playerController2.IsDead))
							{
								num++;
							}
						}
						if (num == 1)
						{
							PlayerCustom playerCustom = enumerable.First((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead));
							if (!NetworkBool.op_Implicit(playerCustom.Kidnapped))
							{
								PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								return false;
							}
						}
					}
				}
				bool flag = false;
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: spy");
				PlayerCustom specificNewPrimaryRole3 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Spy);
				if ((Object)(object)specificNewPrimaryRole3 != (Object)null && specificNewPrimaryRole3.SoloRoleObjectiveCount >= BalancingValues.SpyGoal(PlayerRegistry.Count) && !NetworkBool.op_Implicit(specificNewPrimaryRole3.PlayerController.IsDead) && !NetworkBool.op_Implicit(specificNewPrimaryRole3.Kidnapped))
				{
					flag = true;
					if ((int)GameManager.LocalGameState == 4)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole3.Index);
						return false;
					}
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: scientist");
				PlayerCustom specificNewPrimaryRole4 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Scientist);
				if ((Object)(object)specificNewPrimaryRole4 != (Object)null && specificNewPrimaryRole4.SoloRoleObjectiveCount >= BalancingValues.ScientistGoal(PlayerRegistry.Count) && !NetworkBool.op_Implicit(specificNewPrimaryRole4.PlayerController.IsDead) && !NetworkBool.op_Implicit(specificNewPrimaryRole4.Kidnapped))
				{
					flag = true;
					if ((int)GameManager.LocalGameState == 4)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole4.Index);
						return false;
					}
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: kidnapper");
				PlayerCustom kidnapper = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Kidnapper);
				if ((Object)(object)kidnapper != (Object)null && !NetworkBool.op_Implicit(kidnapper.PlayerController.IsDead))
				{
					if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.Ref != kidnapper.Ref && !NetworkBool.op_Implicit(o.Kidnapped) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)) == 0)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, kidnapper.Index);
						return false;
					}
					if ((int)GameManager.LocalGameState == 4 && PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.Ref != kidnapper.Ref && !NetworkBool.op_Implicit(o.Kidnapped) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)) <= 1)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, kidnapper.Index);
						return false;
					}
					if (kidnapper.SoloRoleObjectiveCount >= BalancingValues.KidnapperTargetAmount(PlayerRegistry.Count))
					{
						flag = true;
						if ((int)GameManager.LocalGameState == 4 && kidnapper.SoloRoleObjectiveCount >= BalancingValues.KidnapperTargetAmount(PlayerRegistry.Count))
						{
							PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, kidnapper.Index);
							return false;
						}
					}
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: voodoo");
				PlayerCustom specificNewPrimaryRole5 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Voodoo);
				if ((Object)(object)specificNewPrimaryRole5 != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole5.PlayerController.IsDead) && !NetworkBool.op_Implicit(specificNewPrimaryRole5.Kidnapped))
				{
					int num2 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Voodoo || o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie);
					int num3 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Voodoo && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && !NetworkBool.op_Implicit(o.PlayerController.IsDead));
					int num4 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => ((int)o.PlayerController.Role == 1 || o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor) && !NetworkBool.op_Implicit(o.PlayerController.IsDead));
					if (num2 >= num3 && num2 > num4)
					{
						flag = true;
						if ((int)GameManager.LocalGameState == 4)
						{
							PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole5.Index);
							return false;
						}
					}
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: beast");
				PlayerCustom beast = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Beast);
				if ((Object)(object)beast != (Object)null && !NetworkBool.op_Implicit(beast.PlayerController.IsDead) && !NetworkBool.op_Implicit(beast.Kidnapped) && PlayerCustomRegistry.AllPlayers.All((PlayerCustom o) => o.Ref == beast.Ref || NetworkBool.op_Implicit(o.PlayerController.IsDead) || o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie || NetworkBool.op_Implicit(o.BeastMark) || NetworkBool.op_Implicit(o.Kidnapped) || NetworkBool.op_Implicit(o.ResurrectedByNecromancer)))
				{
					flag = true;
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: mercenary");
				PlayerCustom specificNewPrimaryRole6 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Mercenary);
				if ((Object)(object)specificNewPrimaryRole6 != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole6.PlayerController.IsDead) && !NetworkBool.op_Implicit(specificNewPrimaryRole6.Kidnapped) && specificNewPrimaryRole6.SoloRoleObjectiveCount >= specificNewPrimaryRole6.SoloRoleObjectiveTarget)
				{
					flag = true;
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: cultist");
				PlayerCustom specificNewPrimaryRole7 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Cultist);
				if ((Object)(object)specificNewPrimaryRole7 != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole7.PlayerController.IsDead) && !NetworkBool.op_Implicit(specificNewPrimaryRole7.Kidnapped) && specificNewPrimaryRole7.SoloRoleObjectiveCount >= 10000)
				{
					flag = true;
				}
				LycansUtility.AddLogOnlyForMe("CheckForEndGame: lovers");
				List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover).ToList();
				if (list.Count == 2 && !NetworkBool.op_Implicit(PlayerRegistry.GetPlayer(list.First().Ref).IsDead) && !list.Any((PlayerCustom o) => NetworkBool.op_Implicit(o.Kidnapped)))
				{
					if (PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) == 2)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, list.First().Index);
						return false;
					}
					if ((int)GameManager.LocalGameState == 4 && PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) <= 4)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, list.First().Index);
						return false;
					}
				}
				List<PlayerCustom> list2 = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
				if (!NetworkBool.op_Implicit(__instance.BattleRoyale))
				{
					int num5 = list2.Count((PlayerCustom o) => (int)o.PlayerController.Role == 1);
					int num6 = list2.Count - num5;
					LycansUtility.AddLogOnlyForMe("CheckForEndGame: default case 1");
					if ((int)GameManager.LocalGameState == 4 && num5 > 0)
					{
						foreach (PlayerCustom item2 in list2)
						{
							if (item2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor)
							{
								num5++;
								num6--;
							}
							if (item2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && (int)item2.PlayerController.Role == 1)
							{
								num6 += 2;
							}
						}
					}
					LycansUtility.AddLogOnlyForMe("CheckForEndGame: default case 2");
					if (num5 == 0 && !flag)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, PlayerCustomRegistry.GetAnyVillager().Index);
						return false;
					}
					if (num6 == 0)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, PlayerCustomRegistry.Where((PlayerCustom o) => (int)o.PlayerController.Role == 1 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).First().Index);
						return false;
					}
					LycansUtility.AddLogOnlyForMe("CheckForEndGame: default case 3");
					if ((int)GameManager.State.Current == 4)
					{
						foreach (PlayerCustom item3 in list2)
						{
							if ((NetworkBool.op_Implicit(item3.Downed) || NetworkBool.op_Implicit(item3.Kidnapped)) && ((int)item3.PlayerController.Role == 1 || item3.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor))
							{
								num5--;
							}
						}
					}
					LycansUtility.AddLogOnlyForMe("CheckForEndGame: default case 4");
					if (num5 >= num6 && (int)GameManager.State.Current == 4)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, PlayerCustomRegistry.Where((PlayerCustom o) => (int)o.PlayerController.Role == 1 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).First().Index);
						return false;
					}
					if (GameManager.Instance.Score >= GameManager.Instance.MaxScore)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, PlayerCustomRegistry.GetAnyVillager().Index);
						return false;
					}
				}
				else if (list2.Count <= 1)
				{
					GameManager.Rpc_EndGameBattleRoyale(((SimulationBehaviour)__instance).Runner);
					return false;
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CheckForEndgamePatch: " + ex));
			StackTrace stackTrace = new StackTrace();
			Plugin.Logger.LogError((object)("StackTrace: " + stackTrace));
			return true;
		}
	}
}
