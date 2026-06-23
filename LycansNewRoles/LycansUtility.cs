using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LycansNewRoles;

public static class LycansUtility
{
	public static bool AddDebugLogs = false;

	public static Dictionary<string, int> GameObjectsCount = new Dictionary<string, int>();

	public static bool GameActuallyInPlay => (int)GameManager.LocalGameState == 2 && !NetworkBool.op_Implicit(GameManager.Instance.IsFinished) && !NetworkBool.op_Implicit(GameManager.LightingManager.IsTransition) && !NetworkBool.op_Implicit(DraftManager.Instance.Active);

	public static bool WolvesCanTransform => NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) || GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Eclipse;

	public static string GetFormattedCurrentDateTimeUtc => DateTime.UtcNow.ToString("o");

	public static string GetCurrentDateTimeUtcForId => DateTime.UtcNow.ToString("s").Replace("-", "").Replace("T", "")
		.Replace(":", "");

	public static string ReplaceWithActionNameText(string text, string key, InputActionName inputActionName)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (text.Contains(key))
		{
			return text.Replace(key, GetInputDisplayCustom(inputActionName));
		}
		return text;
	}

	public static string ReplaceWithActionNameText(string text, string key, InputAction inputAction)
	{
		if (text.Contains(key))
		{
			return text.Replace(key, GetInputDisplayCustom(inputAction));
		}
		return text;
	}

	public static string GetInputDisplayCustom(InputActionName inputActionName)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return GetInputDisplayCustom(InputManager.Instance.GetInputAction(inputActionName));
	}

	public static string GetInputDisplayCustom(InputAction inputAction)
	{
		int num = (InputManager.Instance.IsGamepad() ? 1 : 0);
		if (inputAction.name == "Item")
		{
			num = ((num != 1) ? 1 : 0);
		}
		string text2 = default(string);
		string text3 = default(string);
		string text = InputActionRebindingExtensions.GetBindingDisplayString(inputAction, num, ref text2, ref text3, (DisplayStringOptions)0);
		string text4 = null;
		if (InputSystem.IsFirstLayoutBasedOnSecond(text2, "DualShockGamepad"))
		{
			text4 = "PS4" + text3;
		}
		else if (InputSystem.IsFirstLayoutBasedOnSecond(text2, "Gamepad"))
		{
			text4 = "XBOX" + text3;
		}
		else if (InputSystem.IsFirstLayoutBasedOnSecond(text2, "Mouse") && (text3 == "leftButton" || text3 == "rightButton" || text3 == "middleButton"))
		{
			text4 = text3;
		}
		if (text4 != null)
		{
			return "<sprite name=\"" + text4 + "\">";
		}
		if (!InputManager.Instance.IsGamepad())
		{
			text = "[" + text + "]";
		}
		return text;
	}

	public static bool CanPlayerSeeOtherPlayer(PlayerCustom player1, PlayerCustom player2, float range)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = ((Component)Traverse.Create((object)((Component)player1.PlayerController).GetComponent<PlayerInteract>()).Field<Camera>("_camera").Value).transform;
		return CanPositionSeePlayer(transform.position, player2, range);
	}

	public static bool CanPositionSeePlayer(Vector3 position, PlayerCustom player, float range)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			player.PlayerCustomCollider.SetActive(true);
			Transform transform = player.PlayerCustomCollider.transform;
			Vector3 val = transform.position - position;
			LayerMask value = Traverse.Create((object)((Component)player.PlayerController).GetComponent<PlayerInteract>()).Field<LayerMask>("gunLayerMask").Value;
			bool result = false;
			RaycastHit val2 = default(RaycastHit);
			if (Physics.Raycast(position, val, ref val2, range, LayerMask.op_Implicit(value)))
			{
				PlayerController componentInParent = ((Component)((RaycastHit)(ref val2)).collider).gameObject.GetComponentInParent<PlayerController>();
				if ((Object)(object)componentInParent != (Object)null && componentInParent.Ref == player.Ref)
				{
					result = true;
				}
			}
			player.PlayerCustomCollider.SetActive(false);
			return result;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CanPositionSeePlayer error: " + ex));
			player.PlayerCustomCollider.SetActive(false);
			return false;
		}
	}

	public static void DebugLog(string log)
	{
		if (AddDebugLogs)
		{
			Plugin.Logger.LogInfo((object)("DEBUG: " + log));
		}
	}

	public static void AddLogOnlyForMe(string log)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (PlayerController.Local.PlayerData.ID == "76561197973106144")
		{
			Plugin.Logger.LogInfo((object)log);
		}
	}
}
