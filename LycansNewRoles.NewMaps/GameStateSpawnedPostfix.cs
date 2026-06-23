using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles.NewMaps;

[HarmonyPatch(typeof(GameState), "Spawned")]
internal class GameStateSpawnedPostfix
{
	private static void Postfix(GameState __instance)
	{
		try
		{
			StateMachine<EGameState> value = Traverse.Create((object)__instance).Field<StateMachine<EGameState>>("StateMachine").Value;
			StateHooks<EGameState> obj = value[(EGameState)2];
			obj.onEnter = (Action<EGameState>)Delegate.Combine(obj.onEnter, (Action<EGameState>)delegate(EGameState state)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0003: Invalid comparison between Unknown and I4
				if ((int)state == 1)
				{
					MapManager.UpdateMapByPlayersAmount();
				}
			});
			StateHooks<EGameState> obj2 = value[(EGameState)3];
			obj2.onEnter = (Action<EGameState>)Delegate.Combine(obj2.onEnter, (Action<EGameState>)delegate(EGameState state)
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0012: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Invalid comparison between Unknown and I4
				if (!NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) || (int)state == 4)
				{
					MapManager.UpdateMapByPlayersAmount();
				}
			});
			StateHooks<EGameState> obj3 = value[(EGameState)2];
			obj3.onEnter = (Action<EGameState>)Delegate.Combine(obj3.onEnter, (Action<EGameState>)delegate(EGameState state)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0003: Invalid comparison between Unknown and I4
				if ((int)state == 4)
				{
					MapManager.UpdateMapByPlayersAmount();
				}
			});
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameManagerStartGamePatch error: " + ex));
		}
	}
}
