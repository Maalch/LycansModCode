using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Rpc_EndGame")]
internal class EndgameChangesPatch
{
	private unsafe static bool Prefix(NetworkRunner runner, bool wolfWin)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Invalid comparison between Unknown and I4
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
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
					num += 4;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void GameManager::Rpc_EndGame(Fusion.NetworkRunner,System.Boolean)")), data);
					ReadWriteUtilsForWeaver.WriteBoolean((int*)(data + num2), wolfWin);
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			Plugin.Logger.LogError((object)"ERROR: should never go into Rpc_EndGame method!");
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("EndgameChangesPatch error: " + ex));
			return true;
		}
	}
}
