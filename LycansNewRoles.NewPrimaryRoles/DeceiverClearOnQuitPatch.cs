using System;
using HarmonyLib;
using LycansNewRoles.PowerObjects;

namespace LycansNewRoles.NewPrimaryRoles;

[HarmonyPatch(typeof(GameManager), "QuitGame")]
internal class DeceiverClearOnQuitPatch
{
	private static void Prefix()
	{
		try
		{
			DeceiverIllusionComponent.Illusions.Clear();
			MerchantCoin.AllCoins.Clear();
			InvestigatorHint.AllHints.Clear();
			SurvivalistHint.AllHints.Clear();
			HermitHideout.AllHideouts.Clear();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DeceiverClearOnQuitPatch error: " + ex));
		}
	}
}
