using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LocalCameraHandler), "NextPov")]
internal class LocalCameraHandlerNextPoVPatch
{
	private static bool Prefix(LocalCameraHandler __instance, bool forward)
	{
		if (PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost || PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Specter)
		{
			return false;
		}
		List<PlayerController> list = (from o in PlayerCustomRegistry
			where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.PlayerController.IsDisconnected) && !o.IsOutOfTheWorld
			select o.PlayerController).ToList();
		if (!list.Any())
		{
			__instance.SwitchPov(PlayerController.Local);
			return false;
		}
		int num = list.IndexOf(__instance.PovPlayer);
		if (num > -1)
		{
			PlayerController val;
			if (forward)
			{
				int num2 = num + 1;
				val = ((num2 >= list.Count) ? list[0] : list[num2]);
			}
			else
			{
				int num3 = num - 1;
				if (num3 >= 0)
				{
					val = list[num3];
				}
				else
				{
					List<PlayerController> list2 = list;
					val = list2[list2.Count - 1];
				}
			}
			__instance.SwitchPov(val);
			return false;
		}
		__instance.SwitchPov(list[0]);
		return false;
	}
}
