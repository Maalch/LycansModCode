using System;
using System.Net.Http;
using Fusion;
using HarmonyLib;
using LycansNewRoles.Stats;
using Newtonsoft.Json;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Rpc_EndGameBattleRoyale")]
internal class BattleRoyaleStats
{
	private static void Postfix(GameManager __instance)
	{
		Plugin.Logger.LogInfo((object)("BR Session stats: " + JsonConvert.SerializeObject((object)SessionStats.Stats)));
		if (PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => o.PlayerData.ID == "76561198034021995")) || PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => o.PlayerData.ID == "76561198045789440")) || PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => o.PlayerData.ID == "76561199060053791")) || (PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => o.PlayerData.ID == "76561197973106144")) && !PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => PlayerRef.op_Implicit(o.Ref) >= 1000)) && PlayerRegistry.Count >= 8))
		{
			HttpClient httpClient = new HttpClient();
			StringContent content = new StringContent(JsonConvert.SerializeObject((object)SessionStats.Stats));
			httpClient.PostAsync("https://mjconxaygsuwux4lsilhzwauhi0yigbp.lambda-url.eu-west-1.on.aws/", content);
		}
	}
}
