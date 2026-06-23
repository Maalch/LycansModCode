using System;
using DynamicSmokeSystem;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.SecondaryRoles;

[HarmonyPatch(typeof(Smoke), "Spawned")]
internal class SecondaryRoleEngineerReduceSmokeEffectPatch
{
	private static void Prefix(Smoke __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			if ((Object)(object)player != (Object)null && player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothEngineer)
			{
				DynamicSmokeSystemController value = Traverse.Create((object)__instance).Field<DynamicSmokeSystemController>("_dynamicSmokeSystem").Value;
				Traverse<int> val = Traverse.Create((object)value).Field<int>("amount");
				val.Value = (NetworkBool.op_Implicit(Plugin.CustomConfig.SmokeBoosted) ? 250 : 250);
			}
			else if (NetworkBool.op_Implicit(Plugin.CustomConfig.SmokeBoosted))
			{
				DynamicSmokeSystemController value2 = Traverse.Create((object)__instance).Field<DynamicSmokeSystemController>("_dynamicSmokeSystem").Value;
				Traverse<int> val2 = Traverse.Create((object)value2).Field<int>("amount");
				val2.Value = 4000;
				Traverse.Create((object)value2).Field<int>("amountMaxPerFrame").Value = 100;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleEngineerReduceSmokeEffectPatch: " + ex));
		}
	}
}
