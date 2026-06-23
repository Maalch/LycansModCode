using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameState), "Spawned")]
internal class UpdateDescriptionAfterTransitionPatch
{
	private static void Postfix(GameState __instance)
	{
		try
		{
			if (((SimulationBehaviour)__instance).HasInputAuthority)
			{
				StateMachine<EGameState> value = Traverse.Create((object)__instance).Field<StateMachine<EGameState>>("StateMachine").Value;
				StateHooks<EGameState> obj = value[(EGameState)2];
				obj.onEnter = (Action<EGameState>)Delegate.Combine(obj.onEnter, (Action<EGameState>)delegate
				{
					ShowRoleDescriptionPatch.NeedsUpdate = true;
				});
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateDescriptionAfterTransitionPatch error: " + ex));
		}
	}
}
