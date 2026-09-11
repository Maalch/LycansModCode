using System;
using System.Collections.Generic;
using System.Diagnostics;
using BepInEx.Logging;
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

	public static SkinnedMeshRenderer UpdateVillagerSkin(SkinnedMeshRenderer villagerMeshRenderer, int skinIndex, PlayerController playerController)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Plugin.Logger.LogInfo((object)("Update villager skin with skin index " + skinIndex + " with stacktrace " + new StackTrace()));
			if ((Object)(object)playerController != (Object)null)
			{
				ManualLogSource logger = Plugin.Logger;
				NetworkString<_32> username = playerController.PlayerData.Username;
				logger.LogInfo((object)("For player: " + ((object)username/*cast due to constrained. prefix*/).ToString()));
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogInfo((object)("Error logging skin update: " + ex));
		}
		bool activeSelf = ((Component)villagerMeshRenderer).gameObject.activeSelf;
		GameObject gameObject = ((Component)((Component)villagerMeshRenderer).transform.parent.Find("metarig")).gameObject;
		CharacterSkin characterSkin = Plugin.Skins[skinIndex];
		GameObject val = Object.Instantiate<GameObject>(characterSkin.SkinMeshRenderer, ((Component)villagerMeshRenderer).transform.parent);
		val.SetActive(true);
		SkinnedMeshRenderer component = val.GetComponent<SkinnedMeshRenderer>();
		Transform[] bones = component.bones;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>(false);
		Dictionary<string, Transform> dictionary = new Dictionary<string, Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform val2 in array)
		{
			if (!dictionary.ContainsKey(((Object)val2).name))
			{
				dictionary[((Object)val2).name] = ((Component)val2).transform;
			}
		}
		Transform[] array2 = (Transform[])(object)new Transform[bones.Length];
		for (int j = 0; j < bones.Length; j++)
		{
			array2[j] = dictionary[((Object)bones[j]).name];
		}
		component.bones = array2;
		component.rootBone = dictionary[((Object)component.rootBone).name];
		Object.DestroyImmediate((Object)(object)((Component)villagerMeshRenderer).gameObject);
		if ((Object)(object)playerController != (Object)null)
		{
			Traverse.Create((object)playerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value = val.GetComponent<SkinnedMeshRenderer>();
			Traverse.Create((object)playerController.PlayerEffectManager).Field<SkinnedMeshRenderer>("skinnedMeshRenderer").Value = val.GetComponent<SkinnedMeshRenderer>();
			playerController.ShowThirdPersonModels(activeSelf);
		}
		Plugin.Logger.LogInfo((object)("Skin updated, returning SkinnedMeshRenderer: " + (object)val));
		return val.GetComponent<SkinnedMeshRenderer>();
	}

	public static void UpdateVillagerSkinColor(SkinnedMeshRenderer villagerMeshRenderer, int skinIndex, int skinColorIndex, int playerColorIndex, PlayerCustom playerCustom)
	{
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		if (skinIndex == 0)
		{
			((Renderer)villagerMeshRenderer).material.mainTexture = ColorManager.GetTexture(playerColorIndex);
			if ((Object)(object)playerCustom != (Object)null)
			{
				if (NetworkBool.op_Implicit(playerCustom.Petrified))
				{
					((Renderer)villagerMeshRenderer).material.color = PlayerCustom.SkinColorPetrified;
					((Renderer)Traverse.Create((object)playerCustom.PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = PlayerCustom.SkinColorPetrified;
				}
				else if (playerCustom.HasZombieColor)
				{
					((Renderer)villagerMeshRenderer).material.color = PlayerCustom.SkinColorZombieHuman;
					((Renderer)Traverse.Create((object)playerCustom.PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = PlayerCustom.SkinColorZombieWolf;
				}
				else if (NetworkBool.op_Implicit(playerCustom.Poison))
				{
					((Renderer)villagerMeshRenderer).material.color = PlayerCustom.SkinColorPoison;
					((Renderer)Traverse.Create((object)playerCustom.PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = PlayerCustom.SkinColorPoison;
				}
				else
				{
					((Renderer)villagerMeshRenderer).material.color = Color.white;
					((Renderer)Traverse.Create((object)playerCustom.PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = playerCustom.WolfColor;
				}
			}
			return;
		}
		skinColorIndex = Mathf.Min(skinColorIndex, Plugin.Skins[skinIndex].SkinTextures.Count - 1);
		((Renderer)villagerMeshRenderer).material.SetTexture("_SkinTexture", Plugin.Skins[skinIndex].SkinTextures[skinColorIndex]);
		int index = Mathf.Min(playerColorIndex, Plugin.Skins[skinIndex].TopTextures.Count - 1);
		((Renderer)villagerMeshRenderer).material.SetTexture("_TopTexture", Plugin.Skins[skinIndex].TopTextures[index]);
		if ((Object)(object)playerCustom != (Object)null)
		{
			if (NetworkBool.op_Implicit(playerCustom.Petrified))
			{
				((Renderer)villagerMeshRenderer).material.SetTexture("_SkinTexture", Plugin.Skins[skinIndex].SkinPetrified);
				((Renderer)Traverse.Create((object)playerCustom.PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = PlayerCustom.SkinColorPetrified;
			}
			else if (playerCustom.HasZombieColor)
			{
				((Renderer)villagerMeshRenderer).material.SetTexture("_SkinTexture", Plugin.Skins[skinIndex].SkinZombified);
				((Renderer)Traverse.Create((object)playerCustom.PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = PlayerCustom.SkinColorZombieWolf;
			}
			else if (NetworkBool.op_Implicit(playerCustom.Poison))
			{
				((Renderer)villagerMeshRenderer).material.SetTexture("_SkinTexture", Plugin.Skins[skinIndex].SkinPoisoned);
				((Renderer)Traverse.Create((object)playerCustom.PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = PlayerCustom.SkinColorPoison;
			}
			else
			{
				((Renderer)Traverse.Create((object)playerCustom.PlayerController).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.color = playerCustom.WolfColor;
			}
		}
	}

	public static void UpdateVillagerHat(SkinnedMeshRenderer villagerMeshRenderer, int hatIndex)
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ((Component)villagerMeshRenderer).transform.parent.Find("metarig").Find("spine").Find("spine.001")
			.Find("spine.002")
			.Find("spine.003")
			.Find("spine.004")
			.Find("spine.005")
			.Find("spine.006")
			.Find("HatsContainer")
			.Find("Hats");
		int childCount = ((Component)val).transform.childCount;
		foreach (object? item in ((Component)val).transform)
		{
			((Component)(Transform)item).gameObject.SetActive(false);
		}
		if (hatIndex >= 0 && hatIndex < childCount)
		{
			((Component)((Component)val).transform.GetChild(hatIndex)).gameObject.SetActive(true);
			((Component)val).gameObject.SetActive(true);
			((Component)val.parent).gameObject.SetActive(true);
		}
	}
}
