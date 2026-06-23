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
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Invalid comparison between Unknown and I4
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Invalid comparison between Unknown and I4
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Invalid comparison between Unknown and I4
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Invalid comparison between Unknown and I4
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Invalid comparison between Unknown and I4
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Invalid comparison between Unknown and I4
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Invalid comparison between Unknown and I4
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Invalid comparison between Unknown and I4
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Invalid comparison between Unknown and I4
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Invalid comparison between Unknown and I4
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Invalid comparison between Unknown and I4
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(__instance.IsStarted) && !NetworkBool.op_Implicit(__instance.IsFinished))
			{
				if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) || NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
				{
					return false;
				}
				PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.VillageIdiot);
				if ((Object)(object)specificNewPrimaryRole != (Object)null)
				{
					PlayerController playerController = specificNewPrimaryRole.PlayerController;
					if (NetworkBool.op_Implicit(playerController.IsDead) && NetworkBool.op_Implicit(specificNewPrimaryRole.RoleDeathUniqueBool))
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole.Index);
						return false;
					}
				}
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
				if ((int)GameManager.LocalGameState == 4)
				{
					PlayerCustom specificNewPrimaryRole2 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Spy);
					if ((Object)(object)specificNewPrimaryRole2 != (Object)null && specificNewPrimaryRole2.SoloRoleObjectiveCount >= BalancingValues.SpyGoal(PlayerRegistry.Count) && !NetworkBool.op_Implicit(specificNewPrimaryRole2.PlayerController.IsDead) && !NetworkBool.op_Implicit(specificNewPrimaryRole2.Kidnapped))
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole2.Index);
						return false;
					}
				}
				if ((int)GameManager.LocalGameState == 4)
				{
					PlayerCustom specificNewPrimaryRole3 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Scientist);
					if ((Object)(object)specificNewPrimaryRole3 != (Object)null && specificNewPrimaryRole3.SoloRoleObjectiveCount >= BalancingValues.ScientistGoal(PlayerRegistry.Count) && !NetworkBool.op_Implicit(specificNewPrimaryRole3.PlayerController.IsDead) && !NetworkBool.op_Implicit(specificNewPrimaryRole3.Kidnapped))
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole3.Index);
						return false;
					}
				}
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
				if ((int)GameManager.LocalGameState == 4)
				{
					PlayerCustom specificNewPrimaryRole4 = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Voodoo);
					if ((Object)(object)specificNewPrimaryRole4 != (Object)null && !NetworkBool.op_Implicit(specificNewPrimaryRole4.PlayerController.IsDead) && !NetworkBool.op_Implicit(specificNewPrimaryRole4.Kidnapped))
					{
						int num2 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Voodoo || o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie);
						int num3 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Voodoo && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && !NetworkBool.op_Implicit(o.PlayerController.IsDead));
						int num4 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => ((int)o.PlayerController.Role == 1 || o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor) && !NetworkBool.op_Implicit(o.PlayerController.IsDead));
						if (num2 >= num3 && num2 > num4)
						{
							PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, specificNewPrimaryRole4.Index);
							return false;
						}
					}
				}
				PlayerCustom kidnapper = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Kidnapper);
				if ((Object)(object)kidnapper != (Object)null && !NetworkBool.op_Implicit(kidnapper.PlayerController.IsDead))
				{
					if ((int)GameManager.LocalGameState == 4)
					{
						if (kidnapper.SoloRoleObjectiveCount >= BalancingValues.KidnapperTargetAmount(PlayerRegistry.Count))
						{
							PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, kidnapper.Index);
							return false;
						}
						if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.Ref != kidnapper.Ref && !NetworkBool.op_Implicit(o.Kidnapped) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)) <= 1)
						{
							PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, kidnapper.Index);
							return false;
						}
					}
					if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.Ref != kidnapper.Ref && !NetworkBool.op_Implicit(o.Kidnapped) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)) == 0)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, kidnapper.Index);
						return false;
					}
				}
				List<PlayerCustom> list2 = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
				if (!NetworkBool.op_Implicit(__instance.BattleRoyale))
				{
					int num5 = list2.Count((PlayerCustom o) => (int)o.PlayerController.Role == 1);
					int num6 = list2.Count - num5;
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
					if (num5 == 0)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, PlayerCustomRegistry.GetAnyVillager().Index);
						return false;
					}
					if (num6 == 0)
					{
						PlayerCustom.Rpc_End_Game(((SimulationBehaviour)__instance).Runner, PlayerCustomRegistry.Where((PlayerCustom o) => (int)o.PlayerController.Role == 1 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).First().Index);
						return false;
					}
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
