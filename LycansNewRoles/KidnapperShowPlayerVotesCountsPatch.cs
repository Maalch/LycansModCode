using System.Collections.Generic;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "ShowPlayerVotesCounts")]
internal class KidnapperShowPlayerVotesCountsPatch
{
	private static void Postfix(GameUI __instance, bool active)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (!active)
		{
			return;
		}
		Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)__instance).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
		foreach (KeyValuePair<PlayerRef, PlayerDisplay> item in value)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(item.Key);
			if (NetworkBool.op_Implicit(player.Kidnapped))
			{
				item.Value.ShowVotesCount(false);
			}
		}
	}
}
