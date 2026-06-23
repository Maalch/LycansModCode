using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnPlayerDataChanged")]
internal class DraftSendRoleOptionsPatch
{
	private static void Postfix(Changed<PlayerController> changed)
	{
		try
		{
			PlayerController behaviour = changed.Behaviour;
			if (((SimulationBehaviour)behaviour).HasInputAuthority)
			{
				if (PlayerPrefs.HasKey("DRAFT_OPTION_AGENT") && PlayerPrefs.HasKey("DRAFT_OPTION_LOVER"))
				{
					DraftManager.Rpc_Role_Option(((SimulationBehaviour)behaviour).Runner, PlayerController.Local.Index, 0, PlayerPrefs.GetInt("DRAFT_OPTION_AGENT") == 1);
					DraftManager.Rpc_Role_Option(((SimulationBehaviour)behaviour).Runner, PlayerController.Local.Index, 1, PlayerPrefs.GetInt("DRAFT_OPTION_LOVER") == 1);
				}
				else
				{
					DraftManager.Rpc_Role_Option(((SimulationBehaviour)behaviour).Runner, PlayerController.Local.Index, 0, active: true);
					DraftManager.Rpc_Role_Option(((SimulationBehaviour)behaviour).Runner, PlayerController.Local.Index, 1, active: true);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DraftSendRoleOptionsPatch error: " + ex));
		}
	}
}
