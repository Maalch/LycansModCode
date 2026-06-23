using System.Linq;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Rpc_DisplayDeadPlayers")]
internal class KidnapperUpdateIconRpc
{
	private static void Postfix(GameManager __instance)
	{
		PlayerCustomRegistry.Where((PlayerCustom p) => NetworkBool.op_Implicit(p.Kidnapped)).ToList().ForEach(delegate(PlayerCustom p)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			GameManager.Instance.gameUI.UpdateDeadPlayer(p.Ref);
		});
	}
}
