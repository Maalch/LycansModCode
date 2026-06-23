using System;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Loot), "Rpc_Collect")]
internal class RpcCollectPatch
{
	private unsafe static bool Prefix(PlayerRef actor, Loot __instance)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Invalid comparison between Unknown and I4
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Invalid comparison between Unknown and I4
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!((NetworkBehaviour)__instance).InvokeRpc)
			{
				NetworkBehaviourUtils.ThrowIfBehaviourNotInitialized((NetworkBehaviour)(object)__instance);
				if ((int)((SimulationBehaviour)__instance).Runner.Stage != 4)
				{
					int localAuthorityMask = ((SimulationBehaviour)__instance).Object.GetLocalAuthorityMask();
					if ((localAuthorityMask & 7) != 0)
					{
						if ((localAuthorityMask & 1) != 1)
						{
							if (((SimulationBehaviour)__instance).Runner.HasAnyActiveConnections())
							{
								int num = 8;
								num += 4;
								SimulationMessage* ptr = SimulationMessage.Allocate(((SimulationBehaviour)__instance).Runner.Simulation, num);
								byte* data = SimulationMessage.GetData(ptr);
								int num2 = RpcHeader.Write(RpcHeader.Create(((SimulationBehaviour)__instance).Object.Id, ((NetworkBehaviour)__instance).ObjectIndex, 1), data);
								Unsafe.Write(data + num2, actor);
								num2 += 4;
								((SimulationMessage)ptr).Offset = num2 * 8;
								((SimulationBehaviour)__instance).Runner.SendRpc(ptr);
							}
							if ((localAuthorityMask & 1) == 0)
							{
								return false;
							}
						}
						goto IL_011d;
					}
					NetworkBehaviourUtils.NotifyLocalSimulationNotAllowedToSendRpc("System.Void Loot::Rpc_Collect(Fusion.PlayerRef)", ((SimulationBehaviour)__instance).Object, 7);
				}
				return false;
			}
			((NetworkBehaviour)__instance).InvokeRpc = false;
			goto IL_011d;
			IL_011d:
			if (((SimulationBehaviour)__instance).HasStateAuthority && NetworkBool.op_Implicit(__instance.Available))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(actor);
				if ((int)player.PlayerController.Role != 1)
				{
					player.PlayerController.Feed((int)((double)__instance.HungerValue / 100.0 * (double)GameManager.Instance.MaxHunger));
				}
				__instance.Available = NetworkBool.op_Implicit(false);
				GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("LOOT"), ((Component)__instance).transform.position, 8f, 0.5f);
				if (player.PowerMaterialsInfo != null && player.PowerMaterialsInfo.GainsMaterialOnCollect)
				{
					int scoreValue = __instance.ScoreValue;
					scoreValue = Mathf.RoundToInt((float)(scoreValue * 2) / (float)GameManager.Instance.LootSpawnRate);
					if (NetworkBool.op_Implicit(player.Midas))
					{
						scoreValue += Mathf.RoundToInt((float)__instance.ScoreValue * 1f);
					}
					if (player.PowerMaterialsInfo.BonusMultiplierAtFullHealth > 0f && (__instance.HungerValue == 0 || player.PlayerController.Hunger >= (float)GameManager.Instance.MaxHunger))
					{
						scoreValue += Mathf.RoundToInt((float)__instance.ScoreValue * player.PowerMaterialsInfo.BonusMultiplierAtFullHealth);
					}
					if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Exorcist)
					{
						scoreValue *= Mathf.RoundToInt(BalancingValues.ExorcistChargeMultiplierByMap(GameManager.Instance.MapID));
					}
					player.AddMaterials(scoreValue);
				}
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.VillageIdiot && NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
				{
					player.PrimaryRolePowerCurrentMaterials = Mathf.Max(0, player.PrimaryRolePowerCurrentMaterials - 1700);
				}
				int num3 = __instance.ScoreValue;
				if (NetworkBool.op_Implicit(player.Midas) && !NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
				{
					num3 = Mathf.RoundToInt((float)num3 * 2f);
				}
				if ((int)player.PlayerController.Role != 1 && player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None)
				{
					GameManagerCustom.Instance.CollectedLoot += num3;
				}
				if (!NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
				{
					player.LootCollectedTodayDuringDay += num3;
				}
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary)
				{
					player.SoloRoleObjectiveCount += num3;
				}
				if (player.Stats != null)
				{
					player.Stats.TotalCollectedLoot += num3;
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewPrimaryRoleCollectPatch error: " + ex));
			return true;
		}
	}
}
