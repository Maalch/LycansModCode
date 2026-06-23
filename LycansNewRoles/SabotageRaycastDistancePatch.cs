using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using LycansNewRoles.Sabotages;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateRayCast")]
internal class SabotageRaycastDistancePatch
{
	private static void Postfix(GameObject raycastTargetObject, float distance, PlayerController __instance)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Invalid comparison between Unknown and I4
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Invalid comparison between Unknown and I4
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!((SimulationBehaviour)__instance).HasInputAuthority || !NetworkBool.op_Implicit(Plugin.CustomConfig.SabotagesAvailable) || (int)GameManager.LocalGameState != 2 || NetworkBool.op_Implicit(__instance.IsAiming) || NetworkBool.op_Implicit(__instance.IsZooming))
			{
				return;
			}
			SabotageComponent component = raycastTargetObject.GetComponent<SabotageComponent>();
			if (!((Object)(object)component != (Object)null))
			{
				return;
			}
			float num = Vector3.Distance(((Component)__instance).transform.position, raycastTargetObject.transform.position);
			if (num > 7f)
			{
				return;
			}
			SabotageInfo sabotageInfo = SabotageManager.SabotageObjectsInfo[((Object)raycastTargetObject).name];
			string text = TranslationManager.Instance.GetTranslation(sabotageInfo.DescriptionKey);
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			TickTimer sabotageTimer;
			if ((int)__instance.Role == 1 || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor)
			{
				SabotageSingle sabotageSingle = SabotageManager.Instance.Sabotages[component.SabotageId];
				text += $" ({sabotageSingle.AmountCurrent}/{sabotageSingle.AmountRequired})";
				sabotageTimer = player.SabotageTimer;
				if (!((TickTimer)(ref sabotageTimer)).IsRunning && !sabotageSingle.Completed)
				{
					if (NetworkBool.op_Implicit(component.SabotageObject.Completed))
					{
						GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_SABOTAGE_COMPLETED", Color.red, (InputActionName)0, Array.Empty<object>());
					}
					else
					{
						float range = sabotageInfo.Range;
						if (!NetworkBool.op_Implicit(__instance.IsWolf) && NetworkBool.op_Implicit(__instance.CanMoveAnimation) && num <= range)
						{
							GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_SABOTAGE_GENERIC", Color.red, new List<InputActionName>
							{
								(InputActionName)3,
								(InputActionName)4
							}, Array.Empty<object>());
							Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = raycastTargetObject;
							GameManager.Instance.gameUI.ChangeCrossHairOpacity(1f);
						}
					}
				}
			}
			else if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.VillageIdiot)
			{
				sabotageTimer = player.SabotageTimer;
				if (!((TickTimer)(ref sabotageTimer)).IsRunning)
				{
					float range2 = sabotageInfo.Range;
					if (!NetworkBool.op_Implicit(__instance.IsWolf) && NetworkBool.op_Implicit(__instance.CanMoveAnimation) && num <= range2)
					{
						GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_SABOTAGE_VILLAGE_IDIOT", Color.red, (InputActionName)3, Array.Empty<object>());
						Traverse.Create((object)__instance).Field<GameObject>("_targetObject").Value = raycastTargetObject;
						GameManager.Instance.gameUI.ChangeCrossHairOpacity(1f);
					}
				}
			}
			GameManager.Instance.gameUI.UpdateUsername(text);
			GameManager.Instance.gameUI.ShowUsername(true);
			((Graphic)Traverse.Create((object)GameManager.Instance.gameUI).Field<TextMeshProUGUI>("usernameText").Value).color = Color.white;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SabotageRaycastDistancePatch error: " + ex));
		}
	}
}
