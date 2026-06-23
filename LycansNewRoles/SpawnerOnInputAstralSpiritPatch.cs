using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Spawner), "OnInput")]
internal class SpawnerOnInputAstralSpiritPatch
{
	private static bool Prefix(NetworkRunner runner, NetworkInput input)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerCustom.Local != (Object)null && (Object)(object)PlayerCustom.Local.AstralSpirit != (Object)null)
		{
			((NetworkInput)(ref input)).Set<NetworkInputData>(((Component)PlayerCustom.Local.AstralSpirit).GetComponent<PlayerAstralSpiritInputHandler>().GetNetworkInput());
			((Component)PlayerCustom.Local.AstralSpirit).GetComponent<PlayerAstralSpiritInputHandler>().ClearInputs();
			return false;
		}
		return true;
	}
}
