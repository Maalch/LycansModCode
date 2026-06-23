using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.PowerObjects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LocalCameraHandler), "SwitchPov")]
internal class LocalCameraHandlerSwitchPoVPatch
{
	private static bool Prefix(LocalCameraHandler __instance, PlayerController spectatedPlayer, out PlayerRef __state)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)__instance.PovPlayer == (Object)null)
		{
			__state = PlayerRef.None;
			return true;
		}
		if (PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost || PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Specter)
		{
			__state = PlayerRef.None;
			return false;
		}
		if (NetworkBool.op_Implicit(PlayerCustom.Local.Kidnapped))
		{
			PlayerCustom playerCustom = PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper).FirstOrDefault();
			GameManager.Instance.gameUI.UpdateSpectatedUsername(((object)playerCustom.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString(), playerCustom.PlayerController.Role);
		}
		__state = __instance.PovPlayer.Ref;
		return true;
	}

	private static void Postfix(LocalCameraHandler __instance, PlayerRef __state)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)__instance.PovPlayer == (Object)null)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.PovPlayer.Ref);
			if ((Object)(object)player != (Object)null)
			{
				foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer.UpdateVisibility();
					allPlayer.UpdatePoacherMarkVisibility();
					((Component)allPlayer.PlayerController).GetComponent<PlayerSpotterLightComponent>().UpdateState();
				}
			}
			Plugin.Minimap.RefreshLegendsIfActive();
			PlayerCustom.UpdateTeleporterBeaconOnMinimap();
			if (__state != PlayerRef.None)
			{
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(__state);
				player2.UpdatePredatorComponent();
				player2.UpdateTargetArrowComponent();
			}
			player.UpdatePredatorComponent();
			player.UpdateTargetArrowComponent();
			ItemCustom.UpdateAllItems();
			ExorcistDetector.UpdateVisibilityForAllDetectors();
			ScoutRadar.UpdateVisibilityForAllRadars();
			MagicianBeacon.UpdateVisibilityForAllBeacons();
			MagicianIllusion.UpdateVisibilityForAllMagicianIllusions();
			InvestigatorHint.UpdateVisibilityForAllHints();
			SurvivalistHint.UpdateVisibilityForAllHints();
			DiscipleAnchor.UpdateVisibilityForAllAnchors();
			MysticRepulsor.UpdateVisibilityForAllRepulsors();
			HermitHideout.UpdateVisibilityForAllHideouts();
			RunemasterRune.UpdateVisibilityForAllRunes();
			if (UIManager.DetectivePanel.Active)
			{
				UIManager.DetectivePanel.Hide();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("LocalCameraHandlerSwitchPoVPatch error: " + ex));
		}
	}
}
