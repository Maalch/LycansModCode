using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(BulletItem), "CanUseItem")]
internal class RifleChangesReloadPatch
{
	private static bool Prefix(BulletItem __instance, ref bool __result)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)__instance).Owner);
			if ((Object)(object)player != (Object)null && (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Poacher || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary))
			{
				__result = !NetworkBool.op_Implicit(player.PlayerController.IsGunLoaded);
				return false;
			}
			if (PlayerCustom.IsPrimaryRolePowerForEliteVillagers(player.PrimaryRolePower) && player.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.Hunter)
			{
				__result = (float)player.PrimaryRolePowerCurrentMaterials < (float)player.PowerMaterialsInfo.RequiredMaterials * player.PowerMaterialsInfo.MaximumMaterialsPercentage;
				return false;
			}
			if (player.IsOutOfTheWorld)
			{
				__result = false;
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PrimaryRolePoacherRpcShotPatch: " + ex));
			return true;
		}
	}
}
