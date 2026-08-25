using System;
using Fusion;
using HarmonyLib;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "Update")]
internal class DraftPreventShowMenuPatch
{
	private static bool Prefix(GameUI __instance)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Invalid comparison between Unknown and I4
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)DraftManager.Instance != (Object)null && NetworkBool.op_Implicit(DraftManager.Instance.Active))
		{
			Traverse.Create((object)__instance).Method("UpdateTalkingUI", Array.Empty<object>()).GetValue();
			return false;
		}
		if ((int)GameManager.LocalGameState == 2 && InputManager.Instance.SettingsJustPressed)
		{
			PlayerCustom local = PlayerCustom.Local;
			PlayerController playerController = local.PlayerController;
			TickTimer val = local.PrimaryRoleActionTimer;
			if (((TickTimer)(ref val)).IsRunning)
			{
				PlayerCustom.PlayerNewPrimaryRole newPrimaryRole = local.NewPrimaryRole;
				PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
				if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Voodoo || playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist)
				{
					PlayerCustom.Rpc_Cancel_Current_Action(((SimulationBehaviour)local).Runner, local.Index);
					return false;
				}
				switch (local.PrimaryRolePower)
				{
				case PlayerCustom.PlayerPrimaryRolePower.Necromancer:
				case PlayerCustom.PlayerPrimaryRolePower.Warlock:
				case PlayerCustom.PlayerPrimaryRolePower.Acrobat:
				case PlayerCustom.PlayerPrimaryRolePower.Exorcist:
				case PlayerCustom.PlayerPrimaryRolePower.Survivalist:
				case PlayerCustom.PlayerPrimaryRolePower.Scout:
				case PlayerCustom.PlayerPrimaryRolePower.Shadow:
				case PlayerCustom.PlayerPrimaryRolePower.Runemaster:
					PlayerCustom.Rpc_Cancel_Current_Action(((SimulationBehaviour)local).Runner, local.Index);
					return false;
				}
			}
			val = local.SecondaryRoleActionTimer;
			if (((TickTimer)(ref val)).IsRunning)
			{
				local.SecondaryRoleActionTimer = TickTimer.None;
				PlayerCustom.PlayerSecondaryRole secondaryRole = local.SecondaryRole;
				PlayerCustom.PlayerSecondaryRole playerSecondaryRole = secondaryRole;
				if (playerSecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTeleporter || playerSecondaryRole == PlayerCustom.PlayerSecondaryRole.BothMedium || playerSecondaryRole == PlayerCustom.PlayerSecondaryRole.BothScavenger)
				{
					PlayerCustom.Rpc_Cancel_Current_Action(((SimulationBehaviour)local).Runner, local.Index);
					return false;
				}
			}
		}
		return true;
	}
}
