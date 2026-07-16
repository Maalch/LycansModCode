using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles.SecondaryRoles;

[HarmonyPatch(typeof(GameManager), "EndVote")]
internal class SecondaryRolePoliticianVillagerPatch
{
	private static bool Prefix(GameManager __instance)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Invalid comparison between Unknown and I4
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
			dictionary[-2] = 0;
			foreach (PlayerCustom item in list)
			{
				PlayerRef val = item.Ref;
				dictionary[((PlayerRef)(ref val)).PlayerId] = 0;
			}
			foreach (PlayerCustom item2 in list.Where((PlayerCustom o) => o.PlayerController.IdVoted != -1))
			{
				dictionary[item2.PlayerController.IdVoted]++;
			}
			int mostVotedPlayerId = -1;
			int maxVotes = dictionary.Values.Max();
			if (dictionary.Values.Count((int o) => o == maxVotes) > 1)
			{
				mostVotedPlayerId = -3;
			}
			else
			{
				mostVotedPlayerId = dictionary.First((KeyValuePair<int, int> o) => o.Value == maxVotes).Key;
			}
			if (mostVotedPlayerId >= 0)
			{
				PlayerController playerController = PlayerRegistry.Where((Predicate<PlayerController>)delegate(PlayerController p)
				{
					//IL_0001: Unknown result type (might be due to invalid IL or missing references)
					//IL_0006: Unknown result type (might be due to invalid IL or missing references)
					PlayerRef val3 = p.Ref;
					return ((PlayerRef)(ref val3)).PlayerId == mostVotedPlayerId;
				}).FirstOrDefault();
				if ((Object)(object)playerController != (Object)null)
				{
					PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerController.Ref);
					if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothPolitician)
					{
						if ((int)playerController.Role != 1 && player.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Traitor)
						{
							playerController = CollectionsUtil.Grab<PlayerCustom>(list.Where((PlayerCustom o) => o.Ref != playerController.Ref && !NetworkBool.op_Implicit(o.Kidnapped)).ToList(), 1).First().PlayerController;
						}
						else
						{
							int num = 0;
							foreach (PlayerCustom item3 in list)
							{
								num++;
							}
						}
					}
					if ((Object)(object)playerController != (Object)null)
					{
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerController).transform.position, 50f, 0.8f);
						PlayerCustomRegistry.GetPlayer(playerController.Ref).Stats.UpdateDeathType("VOTED");
						playerController.Rpc_Kill(PlayerRef.None);
						GameManager.Rpc_DisplayDeadPlayers(((SimulationBehaviour)__instance).Runner);
					}
				}
			}
			PlayerCustom playerCustom = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Mole && o.PlayerController.PlayerEffectManager.GetActiveEffects().Any((Effect val3) => val3 is MoleClockEffect) && o.PlayerController.Killer != PlayerRef.None).FirstOrDefault();
			if ((Object)(object)playerCustom != (Object)null && !NetworkBool.op_Implicit(PlayerRegistry.GetPlayer(playerCustom.PlayerController.Killer).IsDead))
			{
				Effect val2 = playerCustom.PlayerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is MoleClockEffect);
				playerCustom.PlayerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val2).Object.Id);
				GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerCustom.PlayerController).transform.position, 50f, 0.8f);
				PlayerCustomRegistry.GetPlayer(playerCustom.PlayerController.Ref).Stats.UpdateDeathType("MOLE");
				playerCustom.PlayerController.Rpc_Kill(playerCustom.PlayerController.Killer);
				GameManager.Rpc_DisplayDeadPlayers(((SimulationBehaviour)__instance).Runner);
			}
			LycansUtility.AddLogOnlyForMe("EndVote event: " + GameManagerCustom.Instance.EventsManager.CurrentEvent);
			if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Vengeance)
			{
				LycansUtility.AddLogOnlyForMe("EndVote mostVotedPlayerId: " + mostVotedPlayerId + ", UniqueBool: " + GameManagerCustom.Instance.EventsManager.CurrentEventUniqueBool);
				if (mostVotedPlayerId < 0 && GameManagerCustom.Instance.EventsManager.CurrentEventUniqueBool)
				{
					List<PlayerCustom> list2 = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (int)o.PlayerController.Role != 1 && o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None && !o.IsOutOfTheWorld).ToList();
					LycansUtility.AddLogOnlyForMe("Valid villagers count: " + list2.Count);
					if (list2.Any())
					{
						PlayerCustom playerCustom2 = CollectionsUtil.Grab<PlayerCustom>(list2, 1).First();
						NetworkString<_32> username = playerCustom2.PlayerController.PlayerData.Username;
						LycansUtility.AddLogOnlyForMe("Victim: " + ((object)username/*cast due to constrained. prefix*/).ToString());
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)playerCustom2.PlayerController).transform.position, 50f, 0.8f);
						PlayerCustomRegistry.GetPlayer(playerCustom2.PlayerController.Ref).Stats.UpdateDeathType("VENGEANCE");
						playerCustom2.PlayerController.Rpc_Kill(PlayerRef.None);
						GameManager.Rpc_DisplayDeadPlayers(((SimulationBehaviour)__instance).Runner);
					}
				}
				GameManagerCustom.Rpc_New_Event(((SimulationBehaviour)__instance).Runner, 0);
			}
			if (!NetworkBool.op_Implicit(__instance.IsFinished))
			{
				GameManager.State.Server_DelaySetState((EGameState)2, 1f);
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRolePoliticianVillagerPatch error: " + ex));
			return true;
		}
	}
}
