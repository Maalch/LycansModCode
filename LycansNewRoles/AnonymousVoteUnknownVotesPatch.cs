using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using TMPro;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "ResetPlayersVotesCount")]
internal class AnonymousVoteUnknownVotesPatch
{
	private static bool Prefix(GameUI __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBool.op_Implicit(Plugin.CustomConfig.AnonymousVotes))
			{
				return true;
			}
			Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)__instance).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
			foreach (PlayerDisplay value5 in value.Values)
			{
				TextMeshProUGUI value2 = Traverse.Create((object)value5).Field<TextMeshProUGUI>("votesCountText").Value;
				((TMP_Text)value2).text = "?";
			}
			PlayerDisplay value3 = Traverse.Create((object)__instance).Field<PlayerDisplay>("skippedVotes").Value;
			TextMeshProUGUI value4 = Traverse.Create((object)value3).Field<TextMeshProUGUI>("votesCountText").Value;
			((TMP_Text)value4).text = "?";
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("AnonymousVoteUnknownVotesPatch: " + ex));
			return true;
		}
	}
}
