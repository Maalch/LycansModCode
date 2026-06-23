using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace LycansNewRoles;

public static class UIManager
{
	[HarmonyPatch(typeof(GameUI), "ShowSettingsMenu")]
	private class ShowSettingsMenuHideExtraUIPatch
	{
		private static void Prefix(bool active)
		{
			try
			{
				if (active)
				{
					HideAllExtraUI();
				}
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError((object)("ShowSettingsMenuHideExtraUIPatch error: " + ex));
			}
		}
	}

	[HarmonyPatch(typeof(GameUI), "ShowGameSettingsMenu")]
	private class ShowGameSettingsMenuHideExtraUIPatch
	{
		private static void Prefix()
		{
			try
			{
				HideAllExtraUI();
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError((object)("ShowGameSettingsMenuHideExtraUIPatch error: " + ex));
			}
		}
	}

	[HarmonyPatch(typeof(GameManager), "LeaveGame")]
	private class LeaveGameHideExtraUIPatch
	{
		private static void Prefix()
		{
			try
			{
				HideAllExtraUI();
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError((object)("LeaveGameHideExtraUIPatch error: " + ex));
			}
		}
	}

	public static UITimer Timer;

	public static UIRoleDescription RoleDescription;

	public static UIGenericChoicePanel GenericChoicePanel;

	public static UIDraftPanel DraftPanel;

	public static UICustomizationComponent CustomizationComponent;

	public static UIModInstallationPanel ModInstallationPanel;

	public static UISpiritPanel SpiritPanel;

	public static UIOptionsDisplayPanel OptionsDisplayPanel;

	public static UIItemDetailsPanel ItemDetailsPanel;

	public static UIItemSecondaryPanel ItemSecondaryPanel;

	public static UIAccessoryPanel AccessoryPanel;

	public static UIDetectivePanel DetectivePanel;

	public static UIMayorPanelForMayor MayorPanelForMayor;

	public static UIMayorPanelForOthers MayorPanelForOthers;

	public static UIDeadRolePanel DeadRolePanel;

	public static UILastGameSummaryPanel LastGameSummaryPanel;

	public static UISecondItemPanel SecondItemPanel;

	public static Sprite DefaultDeadPlayerIcon;

	public static Sprite KidnappedPlayerIcon;

	public static Color KidnappedPlayerColor = new Color(1f, 0f, 1f, 1f);

	public static void ShowRedCenterMessage(string key, float alpha, float duration, List<object> arguments = null)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBool.op_Implicit(GameManager.Instance.IsFinished))
			{
				Traverse.Create((object)GameManager.Instance.gameUI).Field<LocalizeStringEvent>("transition").Value.StringReference.Arguments = ((arguments != null) ? ((IList<object>)arguments.ToArray()) : ((IList<object>)new object[0]));
				GameManager.Instance.gameUI.UpdateTransitionText(key, Color.red);
				CanvasGroup value = Traverse.Create((object)GameManager.Instance.gameUI).Field<CanvasGroup>("transitionOverlay").Value;
				value.alpha = alpha;
				((MonoBehaviour)GameManager.Instance.gameUI).StopCoroutine("WaitThenHideOverlay");
				((MonoBehaviour)GameManager.Instance.gameUI).StartCoroutine(WaitThenHideOverlay(duration));
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ShowRedCenterMessage error: " + ex));
		}
	}

	private static IEnumerator WaitThenHideOverlay(float duration)
	{
		yield return (object)new WaitForSeconds(duration);
		try
		{
			CanvasGroup transitionOverlay = Traverse.Create((object)GameManager.Instance.gameUI).Field<CanvasGroup>("transitionOverlay").Value;
			transitionOverlay.alpha = 0f;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("WaitThenHideOverlay error: " + ex));
		}
	}

	public static void UpdateTimer(TickTimer timer, NetworkRunner runner, string translateKey)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Timer.UpdateTimer(timer, runner, translateKey);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UIManager InitTimer error: " + ex));
		}
	}

	public static void HideTimer()
	{
		try
		{
			Timer.HideTimer();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UIManager HideTimer error: " + ex));
		}
	}

	public static void HideAllExtraUI()
	{
		HideTimer();
		GenericChoicePanel.Hide();
		DetectivePanel.Hide();
		DraftPanel.Hide();
		CustomizationComponent.Hide();
		MayorPanelForMayor.Hide();
		MayorPanelForOthers.Hide();
		DeadRolePanel.Hide();
		if (Cursor.visible)
		{
			GameManager.Instance.gameUI.UpdateCursor(false);
		}
	}
}
