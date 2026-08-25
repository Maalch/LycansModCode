using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateDead")]
internal class PlayerControllerUpdateDeadPatch
{
	private static void Postfix(PlayerController __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(__instance.IsDead) && PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Possessor && PlayerCustom.Local.PrimaryRoleTargetRef == PlayerRef.None && NetworkBool.op_Implicit(PlayerCustom.Local.NewPrimaryRoleUniqueBool))
		{
			PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerController.Local);
		}
	}
}
