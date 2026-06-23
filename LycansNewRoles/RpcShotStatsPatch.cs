using BepInEx.Logging;
using Fusion;
using HarmonyLib;
using LycansNewRoles.Stats;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Rpc_Shot")]
public class RpcShotStatsPatch
{
	private static bool Prefix(PlayerController __instance, PlayerRef targetPlayerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(__instance.IsAiming) && NetworkBool.op_Implicit(__instance.IsGunLoaded))
		{
			if (__instance.Ref == targetPlayerRef)
			{
				ManualLogSource logger = Plugin.Logger;
				NetworkString<_32> username = __instance.PlayerData.Username;
				logger.LogError((object)("Player " + ((object)username/*cast due to constrained. prefix*/).ToString() + " shooting himself!"));
				return false;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (player.Stats != null)
			{
				player.Stats.AddAction(new PlayerStats.PlayerAction
				{
					ActionType = "HunterShoot",
					ActionTarget = (((PlayerRef)(ref targetPlayerRef)).IsNone ? null : ((object)PlayerRegistry.GetPlayer(targetPlayerRef).PlayerData.Username/*cast due to constrained. prefix*/).ToString())
				}, ((Component)player.PlayerController).transform.position);
			}
		}
		return true;
	}
}
