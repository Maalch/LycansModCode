using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerEffectsManager), "ClearEffects")]
internal class ClearEffectsPatch
{
	private static void Prefix(PlayerEffectsManager __instance, out List<string> __state)
	{
		__state = new List<string>();
		if (__instance.GetActiveEffects().Any((Effect o) => o is MoleClockEffect))
		{
			__state.Add("LycansNewRoles.EffectMoleClock");
		}
	}

	private static void Postfix(PlayerEffectsManager __instance, List<string> __state)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController value = Traverse.Create((object)__instance).Field<PlayerController>("_playerController").Value;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(value.Ref);
			player.SecondaryRolePowerActive = NetworkBool.op_Implicit(false);
			if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
			{
				player.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
			}
			player.RepulsionStacks = 0;
			player.SleepStacks = 0;
			player.ClearAllParticleEffects();
			foreach (string item in __state)
			{
				PlayerCustom.ApplyEffectToPlayer(value, item, ((SimulationBehaviour)value).Runner, 1f, 3600f);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ClearEffectsPatch error: " + ex));
		}
	}
}
