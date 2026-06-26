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
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Invalid comparison between Unknown and I4
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Invalid comparison between Unknown and I4
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Invalid comparison between Unknown and I4
		__state = new List<string>();
		if (__instance.GetActiveEffects().Any((Effect o) => o is MoleClockEffect) && ((int)GameManager.LocalGameState == 2 || (int)GameManager.LocalGameState == 4 || (int)GameManager.LocalGameState == 3))
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
