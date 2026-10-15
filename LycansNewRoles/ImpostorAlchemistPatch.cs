using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Rpc_AlchemistEffects")]
internal class ImpostorAlchemistPatch
{
	private unsafe static bool Prefix(NetworkRunner runner, int[] effectIds)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Invalid comparison between Unknown and I4
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return false;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 8;
					num += (effectIds.Length * 4 + 4 + 3) & -4;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void GameManager::Rpc_AlchemistEffects(Fusion.NetworkRunner,System.Int32[])")), data);
					*(int*)(data + num2) = effectIds.Length;
					num2 += 4;
					num2 = ((Native.CopyFromArray<int>((void*)(data + num2), effectIds) + 3) & -4) + num2;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Alchemist)
			{
				List<string> list = new List<string>();
				foreach (int num3 in effectIds)
				{
					list.Add(EffectManager.GetEffect(num3).GetTranslateKey());
				}
				GameManager.Instance.gameUI.UpdateAlchemistEffects(list);
				GameManager.Instance.gameUI.ShowAlchemistEffects(true);
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ImpostorAlchemistPatch error: " + ex));
			return false;
		}
	}
}
