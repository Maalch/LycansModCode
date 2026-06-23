using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Rpc_TransformWolf")]
internal class TransformPatch
{
	private unsafe static bool Prefix(PlayerController __instance)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Invalid comparison between Unknown and I4
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		if (!((NetworkBehaviour)__instance).InvokeRpc)
		{
			NetworkBehaviourUtils.ThrowIfBehaviourNotInitialized((NetworkBehaviour)(object)__instance);
			if ((int)((SimulationBehaviour)__instance).Runner.Stage != 4)
			{
				int localAuthorityMask = ((SimulationBehaviour)__instance).Object.GetLocalAuthorityMask();
				if ((localAuthorityMask & 2) == 0)
				{
					NetworkBehaviourUtils.NotifyLocalSimulationNotAllowedToSendRpc("System.Void PlayerController::Rpc_TransformWolf()", ((SimulationBehaviour)__instance).Object, 2);
				}
				else
				{
					if (((SimulationBehaviour)__instance).Runner.HasAnyActiveConnections())
					{
						int num = 8;
						SimulationMessage* ptr = SimulationMessage.Allocate(((SimulationBehaviour)__instance).Runner.Simulation, num);
						byte* data = SimulationMessage.GetData(ptr);
						int num2 = RpcHeader.Write(RpcHeader.Create(((SimulationBehaviour)__instance).Object.Id, ((NetworkBehaviour)__instance).ObjectIndex, 13), data);
						((SimulationMessage)ptr).Offset = num2 * 8;
						((SimulationBehaviour)__instance).Runner.SendRpc(ptr);
					}
					if ((localAuthorityMask & 7) != 0)
					{
						goto IL_00e9;
					}
				}
			}
			return false;
		}
		((NetworkBehaviour)__instance).InvokeRpc = false;
		goto IL_00e9;
		IL_00e9:
		return TransformClass.TransformPrefix(__instance);
	}

	private static void Postfix(PlayerController __instance)
	{
		TransformClass.TransformPostfix(__instance);
	}
}
