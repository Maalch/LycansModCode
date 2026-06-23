using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewMaps;
using LycansNewRoles.NewMaps.Components;
using LycansNewRoles.PowerObjects;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateRayCast")]
internal class LimitNonPlayerRaycastDistancePatch
{
	private static bool Prefix(GameObject raycastTargetObject, float distance, PlayerController __instance)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Invalid comparison between Unknown and I4
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Possessor)
			{
				PlayerRef primaryRoleTargetRef = PlayerCustom.Local.PrimaryRoleTargetRef;
				if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && PlayerCustom.Local.PrimaryRolePowerCurrentMaterials >= PlayerCustom.Local.PowerMaterialsInfo.RequiredMaterials && NetworkBool.op_Implicit(PlayerCustom.Local.NewPrimaryRoleUniqueBool))
				{
					return false;
				}
			}
			if (PlayerCustom.Local.IsOutOfTheWorld)
			{
				return false;
			}
			PlayerController componentInParent = raycastTargetObject.GetComponentInParent<PlayerController>();
			if ((Object)(object)componentInParent != (Object)null)
			{
				return true;
			}
			MagicianIllusion magicianIllusion = default(MagicianIllusion);
			if (raycastTargetObject.TryGetComponent<MagicianIllusion>(ref magicianIllusion) && NetworkBool.op_Implicit(__instance.IsWolf))
			{
				PlayerController player = PlayerRegistry.GetPlayer(magicianIllusion.TargetRef);
				string text = ((object)player.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
				GameManager.Instance.gameUI.UpdateUsername(text);
				GameManager.Instance.gameUI.ShowUsername(true);
				((Graphic)Traverse.Create((object)GameManager.Instance.gameUI).Field<TextMeshProUGUI>("usernameText").Value).color = Color.white;
				if (distance <= 1.75f)
				{
					GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_ATTACK_MAGICIAN_ILLUSION", Color.white, (InputActionName)3, Array.Empty<object>());
					Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = raycastTargetObject;
					GameManager.Instance.gameUI.ChangeCrossHairOpacity(1f);
				}
				return false;
			}
			bool flag = false;
			if (distance > 2.5f)
			{
				flag = true;
			}
			if (flag)
			{
				Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = null;
				GameManager.Instance.gameUI.ChangeCrossHairOpacity(0.1f);
				if (!NetworkBool.op_Implicit(__instance.IsAiming))
				{
					__instance.ClearRayCast();
					GameManager.Instance.gameUI.HideInteraction();
				}
				return false;
			}
			SleepingGasPlaced sleepingGasPlaced = default(SleepingGasPlaced);
			if (raycastTargetObject.TryGetComponent<SleepingGasPlaced>(ref sleepingGasPlaced) && ((SimulationBehaviour)__instance).HasInputAuthority && !NetworkBool.op_Implicit(__instance.IsAiming) && !NetworkBool.op_Implicit(__instance.IsZooming) && NetworkBool.op_Implicit(__instance.CanMoveAnimation) && (int)GameManager.LocalGameState == 2)
			{
				if (sleepingGasPlaced.CreatorRef == __instance.Ref)
				{
					GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_USE_TAKE_SLEEPING_GAS", Color.white, (InputActionName)3, Array.Empty<object>());
					Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = raycastTargetObject;
					GameManager.Instance.gameUI.ChangeCrossHairOpacity(1f);
				}
				return false;
			}
			CultistSkull cultistSkull = default(CultistSkull);
			if (raycastTargetObject.TryGetComponent<CultistSkull>(ref cultistSkull) && ((SimulationBehaviour)__instance).HasInputAuthority && !NetworkBool.op_Implicit(__instance.IsAiming) && !NetworkBool.op_Implicit(__instance.IsZooming) && NetworkBool.op_Implicit(__instance.CanMoveAnimation) && LycansUtility.GameActuallyInPlay)
			{
				GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_DESTROY_SKULL", Color.red, (InputActionName)3, Array.Empty<object>());
				Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = raycastTargetObject;
				GameManager.Instance.gameUI.ChangeCrossHairOpacity(1f);
				return false;
			}
			HostParasite hostParasite = default(HostParasite);
			if (raycastTargetObject.TryGetComponent<HostParasite>(ref hostParasite) && ((SimulationBehaviour)__instance).HasInputAuthority && NetworkBool.op_Implicit(hostParasite.Appeared) && !NetworkBool.op_Implicit(__instance.IsAiming) && !NetworkBool.op_Implicit(__instance.IsZooming) && NetworkBool.op_Implicit(__instance.CanMoveAnimation) && LycansUtility.GameActuallyInPlay)
			{
				GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_DESTROY_PARASITE", Color.red, (InputActionName)3, Array.Empty<object>());
				Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = raycastTargetObject;
				GameManager.Instance.gameUI.ChangeCrossHairOpacity(1f);
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("LimitNonPlayerRaycastDistancePatch error: " + ex));
			return true;
		}
	}

	private static void Postfix(GameObject raycastTargetObject, float distance, PlayerController __instance)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		AdminTable adminTable = default(AdminTable);
		if (raycastTargetObject.TryGetComponent<AdminTable>(ref adminTable) && !NetworkBool.op_Implicit(__instance.IsAiming) && !NetworkBool.op_Implicit(__instance.IsZooming) && NetworkBool.op_Implicit(__instance.CanMoveAnimation) && Plugin.Minimap.State == MinimapComponent.MinimapState.Inactive && distance <= 4f)
		{
			GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_USE_ADMIN_TABLE", Color.white, (InputActionName)3, Array.Empty<object>());
			Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = raycastTargetObject;
			GameManager.Instance.gameUI.ChangeCrossHairOpacity(1f);
		}
		MechanismButton mechanismButton = default(MechanismButton);
		if (raycastTargetObject.TryGetComponent<MechanismButton>(ref mechanismButton) && !NetworkBool.op_Implicit(__instance.IsAiming) && !NetworkBool.op_Implicit(__instance.IsZooming) && NetworkBool.op_Implicit(__instance.CanMoveAnimation) && distance <= 2f)
		{
			GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_USE_MECHANISM_BUTTON", Color.white, (InputActionName)3, Array.Empty<object>());
			Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = raycastTargetObject;
			GameManager.Instance.gameUI.ChangeCrossHairOpacity(1f);
		}
	}
}
