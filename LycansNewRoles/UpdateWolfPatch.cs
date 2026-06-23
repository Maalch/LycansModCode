using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateWolf")]
internal class UpdateWolfPatch
{
	private static bool Prefix(PlayerController __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(__instance.IsWolf))
			{
				if ((Object)(object)__instance.Item != (Object)null)
				{
					__instance.Item.Cancel();
				}
				PlayerEffectsManager playerEffectManager = __instance.PlayerEffectManager;
				foreach (NetworkId item in ((IEnumerable<NetworkId>)(object)playerEffectManager.ActiveEffects).ToList())
				{
					Effect val = ((SimulationBehaviour)playerEffectManager).Runner.TryGetNetworkedBehaviourFromNetworkedObjectRef<Effect>(item);
					if ((Object)(object)val != (Object)null && !(val is CustomEffect { KeepOnWolfTransformation: not false }))
					{
						playerEffectManager.ActiveEffects.Remove(item);
						val.RemoveEffectFromPlayer(__instance.Ref);
					}
				}
			}
			PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
			if ((Object)(object)povPlayer == (Object)(object)__instance)
			{
				GameManager.LightingManager.DisplayWolfLight(NetworkBool.op_Implicit(__instance.IsWolf));
				GameManager.LightingManager.DisplayNightLight(NetworkBool.op_Implicit(__instance.IsWolf));
			}
			if (!NetworkBool.op_Implicit(povPlayer.PlayerEffectManager.Paranoia))
			{
				__instance.UpdateModel(NetworkBool.op_Implicit(__instance.IsWolf));
			}
			Traverse.Create((object)__instance).Method("UpdateCollider", Array.Empty<object>()).GetValue();
			__instance.UpdateCameraAnchorOffset();
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.UpdatePoacherMarkVisibility();
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateWolfPatch error: " + ex));
			return true;
		}
	}
}
