using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Rpc_DeletePlayer")]
internal class DeletePlayerDeleteCustomPlayerPatch
{
	private static void Postfix(NetworkRunner runner, PlayerRef playerRef)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Invalid comparison between Unknown and I4
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if ((NetworkBehaviourUtils.InvokeRpc || (int)runner.Stage != 4) && runner.IsServer)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerRef);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustomRegistry.Server_Remove(runner, playerRef);
				runner.Despawn(((SimulationBehaviour)player).Object, false);
			}
			UIManager.ModInstallationPanel.RemovePlayer(playerRef);
		}
	}
}
