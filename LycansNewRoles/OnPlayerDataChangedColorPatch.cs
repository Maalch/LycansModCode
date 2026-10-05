using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnPlayerDataChanged")]
internal class OnPlayerDataChangedColorPatch
{
	private static bool Prefix(Changed<PlayerController> changed)
	{
		PlayerController behaviour = changed.Behaviour;
		SkinnedMeshRenderer value = Traverse.Create((object)behaviour).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value;
		GameManager.Instance.gameUI.AddPlayerDisplay(behaviour);
		if ((Object)(object)behaviour != (Object)(object)PlayerController.Local)
		{
			GameManager.Instance.gameUI.AddPlayerVolume(behaviour);
		}
		return false;
	}

	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
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
					int colorIndex = Mathf.Min(11, (int)behaviour.Index);
					PlayerCustom.Rpc_Change_Color(((SimulationBehaviour)behaviour).Runner, behaviour.Index, colorIndex);
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
				if (PlayerPrefs.HasKey("FavoriteSkin"))
				{
					int skinIndex = Mathf.Clamp(PlayerPrefs.GetInt("FavoriteSkin"), 0, SkinPicker.SkinsLength - 1);
					PlayerCustom.Rpc_Change_Skin(((SimulationBehaviour)behaviour).Runner, behaviour.Index, skinIndex);
				}
				else
				{
					PlayerCustom.Rpc_Change_Skin(((SimulationBehaviour)behaviour).Runner, behaviour.Index, 0);
				}
				if (PlayerPrefs.HasKey("FavoriteSkinColor"))
				{
					int skinColorIndex = Mathf.Clamp(PlayerPrefs.GetInt("FavoriteSkinColor"), 0, SkinColorPicker.SkinColorsLength - 1);
					PlayerCustom.Rpc_Change_Skin_Color(((SimulationBehaviour)behaviour).Runner, behaviour.Index, skinColorIndex);
				}
				else
				{
					PlayerCustom.Rpc_Change_Skin_Color(((SimulationBehaviour)behaviour).Runner, behaviour.Index, 0);
				}
				PlayerCustom.Rpc_Set_No_Dead_Role(((SimulationBehaviour)PlayerController.Local).Runner, PlayerController.Local.Index, ExtraSettings.Instance.NoDeadRoleOnDeath ? 1 : 0);
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(behaviour.Ref);
			if ((Object)(object)player != (Object)null)
			{
				player.UpdateModelIfNeeded();
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
