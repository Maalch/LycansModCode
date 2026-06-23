using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewPrimaryRoles;

[HarmonyPatch(typeof(EffectManager), "ClosestWolf")]
internal class NewPrimaryRoleDeceiverGetIllusions
{
	private static bool Prefix(ref PlayerController __result)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerCustom.Local.SummonedSpirit != (Object)null && NetworkBool.op_Implicit(PlayerCustom.Local.SummonedSpirit.HasFocus))
		{
			PlayerSummonedSpiritComponent ghost = PlayerCustom.Local.SummonedSpirit;
			__result = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => p.Ref != ghost.Ref && NetworkBool.op_Implicit(p.IsWolf) && !NetworkBool.op_Implicit(p.IsDead) && Vector3.Distance(((Component)ghost).transform.position, ((Component)p).transform.position) < 30f)).FirstOrDefault();
			return false;
		}
		return true;
	}

	private static void Postfix(ref PlayerController __result)
	{
		try
		{
			if ((Object)(object)__result == (Object)null)
			{
				PlayerController playerPov = PlayerController.Local.LocalCameraHandler.PovPlayer;
				if (DeceiverIllusionComponent.Illusions.Any((DeceiverIllusionComponent o) => Vector3.Distance(((Component)playerPov).transform.position, ((Component)o).transform.position) < 40f))
				{
					__result = playerPov;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewPrimaryRoleDeceiverGetIllusions error: " + ex));
		}
	}
}
