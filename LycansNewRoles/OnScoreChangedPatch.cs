using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "OnScoreChanged")]
internal class OnScoreChangedPatch
{
	private static bool Prefix(Changed<GameManager> changed)
	{
		try
		{
			GameManager.Instance.gameUI.UpdateScore();
			if (((SimulationBehaviour)changed.Behaviour).HasStateAuthority && GameManager.Instance.Score >= GameManager.Instance.MaxScore)
			{
				GameManager.Instance.CheckForEndGame();
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnScoreChangedPatch error: " + ex));
			return true;
		}
	}
}
