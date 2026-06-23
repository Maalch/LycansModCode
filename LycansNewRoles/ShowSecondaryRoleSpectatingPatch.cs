using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "UpdateSpectatedUsername")]
internal class ShowSecondaryRoleSpectatingPatch
{
	private static bool Prefix(string username, PlayerRole playerRole, GameUI __instance)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)ShowRoleClass.TraverseSpectatedUsername == (Object)null)
			{
				ShowRoleClass.TraverseSpectatedUsername = Traverse.Create((object)__instance).Field<TextMeshProUGUI>("spectatedUsername").Value;
			}
			PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
			if (!PlayerCustomRegistry.HasPlayer(povPlayer.Ref))
			{
				return true;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(povPlayer.Ref);
			PlayerCustom.PlayerNewPrimaryRole newPrimaryRole = player.NewPrimaryRole;
			PlayerCustom.PlayerPrimaryRolePower primaryRolePower = player.PrimaryRolePower;
			PlayerCustom.PlayerSecondaryRole secondaryRole = player.SecondaryRole;
			Color primaryRoleColor = UpdateRoleUtility.GetPrimaryRoleColor(playerRole, newPrimaryRole, primaryRolePower);
			List<object> list = new List<object>();
			((Graphic)ShowRoleClass.TraverseSpectatedUsername).color = primaryRoleColor;
			if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Angel || player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost || player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Specter)
			{
				string text = TranslationManager.Instance.GetTranslation(UpdateRoleUtility.GetPrimaryRolePowerKey(primaryRolePower));
				if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Specter)
				{
					PlayerCustom playerCustom = PlayerCustomRegistry.AllPlayers.FirstOrDefault((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && (int)o.PlayerController.Role == 1);
					PlayerRef item = (((Object)(object)playerCustom != (Object)null) ? playerCustom.Ref : PlayerRef.None);
					string text2 = TranslationManager.Instance.GetTranslation("NALES_ROLE_TRAITOR_WOLF") + UpdateRoleUtility.ListWolvesAsString(new List<PlayerRef> { player.Ref, item });
					text = text + "<br>" + text2;
				}
				((TMP_Text)ShowRoleClass.TraverseSpectatedUsername).text = text;
				return false;
			}
			string text3 = username + " (";
			text3 += TranslationManager.Instance.GetTranslation(UpdateRoleUtility.GetNewPrimaryRoleKey(povPlayer, player)).Replace("{0}", "").Replace("{1}", "")
				.Replace("{2}", "");
			if (primaryRolePower != PlayerCustom.PlayerPrimaryRolePower.None)
			{
				text3 = text3 + " " + TranslationManager.Instance.GetTranslation(UpdateRoleUtility.GetPrimaryRolePowerKey(primaryRolePower));
			}
			if (secondaryRole != PlayerCustom.PlayerSecondaryRole.None)
			{
				text3 = text3 + " - " + TranslationManager.Instance.GetTranslation(UpdateRoleUtility.GetSecondaryRoleKey(secondaryRole));
			}
			text3 += ")";
			string primaryRoleDetails = UpdateRoleUtility.GetPrimaryRoleDetails(player, forSpectator: true);
			if (primaryRoleDetails != null)
			{
				text3 = text3 + "<br>" + primaryRoleDetails;
			}
			((TMP_Text)ShowRoleClass.TraverseSpectatedUsername).text = text3;
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Postfix ShowSecondaryRoleSpectatingPatch error: " + ex));
			return true;
		}
	}
}
