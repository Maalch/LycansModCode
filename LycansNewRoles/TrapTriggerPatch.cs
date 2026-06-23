using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Trap), "OnTriggerEnter")]
internal class TrapTriggerPatch
{
	private static bool Prefix(Collider other, Trap __instance)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
			if ((Object)(object)component != (Object)null)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(component.Ref);
				if (NetworkBool.op_Implicit(player.Phasing))
				{
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TrapTriggerPatch Prefix error: " + ex));
			return true;
		}
	}

	private static void Postfix(Collider other, Trap __instance)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
			if (!((Object)(object)component != (Object)null) || !NetworkBool.op_Implicit(component.IsTrapped))
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(component.Ref);
			if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothEngineer)
			{
				__instance.TrappedTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 4f);
			}
			else
			{
				if (NetworkBool.op_Implicit(component.IsWolf))
				{
					TickTimer value = Traverse.Create((object)component).Property<TickTimer>("WolfDelay", (object[])null).Value;
					if (((TickTimer)(ref value)).IsRunning)
					{
						__instance.TrappedTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 2f);
						goto IL_0145;
					}
				}
				if (NetworkBool.op_Implicit(component.IsWolf) && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					__instance.TrappedTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 5f);
				}
				else if (component.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is TrapResistanceEffect))
				{
					__instance.TrappedTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, 3f);
				}
			}
			goto IL_0145;
			IL_0145:
			player.Knockback.Stop();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleEngineerReduceTrapDurationPatch: " + ex));
		}
	}
}
