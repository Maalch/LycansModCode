using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "UpdateAlly")]
internal class ShowSecondaryRoleAllyPatch
{
	private static bool Prefix(ref string username, GameUI __instance)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)ShowRoleClass.TraverseRole == (Object)null)
			{
				ShowRoleClass.TraverseRole = Traverse.Create((object)__instance).Field<LocalizeStringEvent>("role").Value;
			}
			if (NetworkBool.op_Implicit(PlayerCustom.Local.Possessed))
			{
				((LocalizedReference)ShowRoleClass.TraverseRole.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_UI_POSSESSED_NAME"));
				ShowRoleClass.TraverseRole.RefreshString();
				return false;
			}
			if (NetworkBool.op_Implicit(PlayerCustom.Local.Kidnapped))
			{
				PlayerCustom playerCustom = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper).FirstOrDefault();
				((TMP_Text)ShowRoleClass.TraverseRoleText).text = ((object)playerCustom.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
				((Graphic)ShowRoleClass.TraverseRoleText).color = PlayerCustom.NewPrimaryRoleKidnapperColor;
				return false;
			}
			PlayerController local = PlayerController.Local;
			if (!PlayerCustomRegistry.HasPlayer(local.Ref))
			{
				return true;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(local.Ref);
			RoleDescription fullRoleDescription = UpdateRoleUtility.GetFullRoleDescription(local, player, withAlly: true);
			ShowRoleClass.TraverseRole.StringReference.Arguments = fullRoleDescription.Arguments;
			((LocalizedReference)ShowRoleClass.TraverseRole.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit(fullRoleDescription.Key));
			ShowRoleClass.TraverseRole.RefreshString();
			string primaryRoleDetails = UpdateRoleUtility.GetPrimaryRoleDetails(player, forSpectator: false);
			if (primaryRoleDetails != null)
			{
				TextMeshProUGUI component = ((Component)ShowRoleClass.TraverseRole).GetComponent<TextMeshProUGUI>();
				((TMP_Text)component).text = ((TMP_Text)component).text + "<br>" + primaryRoleDetails;
			}
			if ((Object)(object)ShowRoleClass.TraverseRoleText == (Object)null)
			{
				ShowRoleClass.TraverseRoleText = Traverse.Create((object)__instance).Field<TextMeshProUGUI>("roleText").Value;
			}
			((Graphic)ShowRoleClass.TraverseRoleText).color = fullRoleDescription.Color;
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Postfix UpdateRoleAlly error: " + ex));
			return true;
		}
	}
}
