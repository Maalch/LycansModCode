using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UseItem")]
internal class AllowItemsInWolfFormPatch
{
	private static void Postfix(PlayerController __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Invalid comparison between Unknown and I4
		try
		{
			if (!NetworkBool.op_Implicit(GameManager.LightingManager.IsTransition) && (int)GameManager.LocalGameState == 2 && NetworkBool.op_Implicit(__instance.IsWolf) && (Object)(object)__instance.Item != (Object)null && !NetworkBool.op_Implicit(__instance.IsClimbing) && NetworkBool.op_Implicit(__instance.CanMoveAnimation))
			{
				bool flag = false;
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
				if (__instance.Item is Potion && (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothAlcoholic || (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Alchemist && (int)player.PlayerController.Role == 1)))
				{
					flag = true;
				}
				else if (NetworkBool.op_Implicit(Plugin.CustomConfig.WolvesCanUseItems) && !(__instance.Item is Potion))
				{
					flag = true;
				}
				if (flag)
				{
					__instance.Item.Use();
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("AllowItemsInWolfFormPatch error: " + ex));
		}
	}
}
