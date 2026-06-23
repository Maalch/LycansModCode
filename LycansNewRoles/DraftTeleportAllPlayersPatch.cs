using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "TeleportAllPlayers")]
internal class DraftTeleportAllPlayersPatch
{
	private static bool Prefix()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(DraftManager.Instance.Active))
		{
			return false;
		}
		return true;
	}

	private static void Postfix()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(DraftManager.Instance.Active))
		{
			return;
		}
		foreach (PlayerController item in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))))
		{
			PlayerCustom.ApplyEffectToPlayer(item, "LycansNewRoles.EffectTrapResistance", ((SimulationBehaviour)item).Runner);
		}
	}
}
