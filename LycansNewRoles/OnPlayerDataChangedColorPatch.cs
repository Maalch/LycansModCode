using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnPlayerDataChanged")]
internal class OnPlayerDataChangedColorPatch
{
	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController behaviour = changed.Behaviour;
			if (((SimulationBehaviour)behaviour).HasInputAuthority)
			{
				if (PlayerPrefs.HasKey("FavoriteColor"))
				{
					PlayerCustom.Rpc_Change_Color(((SimulationBehaviour)behaviour).Runner, behaviour.Index, PlayerPrefs.GetInt("FavoriteColor"));
				}
				else
				{
					int num = Mathf.Min(11, (int)behaviour.Index);
					PlayerCustom.Rpc_Change_Color(((SimulationBehaviour)behaviour).Runner, behaviour.Index, behaviour.Index);
				}
				if (PlayerPrefs.HasKey("FavoritePet"))
				{
					int petIndex = Mathf.Min(PetPicker.PetsLength - 1, PlayerPrefs.GetInt("FavoritePet"));
					PlayerCustom.Rpc_Change_Pet(((SimulationBehaviour)behaviour).Runner, behaviour.Index, petIndex);
				}
				else
				{
					PlayerCustom.Rpc_Change_Pet(((SimulationBehaviour)behaviour).Runner, behaviour.Index, 0);
				}
				PlayerCustom.Rpc_Set_No_Dead_Role(((SimulationBehaviour)PlayerController.Local).Runner, PlayerController.Local.Index, ExtraSettings.Instance.NoDeadRoleOnDeath ? 1 : 0);
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(behaviour.Ref);
			if ((Object)(object)player != (Object)null)
			{
				player.UpdateColor();
			}
			if (((SimulationBehaviour)behaviour).Runner.IsServer)
			{
				UIManager.ModInstallationPanel.AddOrUpdatePlayer(behaviour.Ref);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnPlayerDataChangedColorPatch error: " + ex));
		}
	}
}
