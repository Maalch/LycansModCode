using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Spawner), "OnInput")]
internal class SpawnerOnInputSummonedSpiritPatch
{
	private static bool Prefix(NetworkRunner runner, NetworkInput input)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Invalid comparison between Unknown and I4
		if ((Object)(object)PlayerCustom.Local != (Object)null && (Object)(object)PlayerCustom.Local.SummonedSpirit != (Object)null)
		{
			if (NetworkBool.op_Implicit(PlayerCustom.Local.Paralyzed) || (int)GameManager.LocalGameState == 3 || (!LycansUtility.GameActuallyInPlay && (int)GameManager.LocalGameState != 4))
			{
				return false;
			}
			((NetworkInput)(ref input)).Set<NetworkInputData>(((Component)PlayerCustom.Local.SummonedSpirit).GetComponent<PlayerSummonedSpiritInputHandler>().GetNetworkInput());
			((Component)PlayerCustom.Local.SummonedSpirit).GetComponent<PlayerSummonedSpiritInputHandler>().ClearInputs();
			return false;
		}
		return true;
	}
}
