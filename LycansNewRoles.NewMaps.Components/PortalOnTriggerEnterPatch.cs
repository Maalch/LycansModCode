using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.Sabotages;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

[HarmonyPatch(typeof(Portal), "OnTriggerEnter")]
internal class PortalOnTriggerEnterPatch
{
	private static bool Prefix(Collider other, Portal __instance)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			NetworkBool value = Traverse.Create((object)__instance).Property<NetworkBool>("Active", (object[])null).Value;
			if (NetworkBool.op_Implicit(value))
			{
				PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
				if ((Object)(object)component != (Object)null && SabotageManager.Instance.IsSabotageActive(SabotageManager.SabotageIds.Portals) && !NetworkBool.op_Implicit(component.IsWolf))
				{
					return false;
				}
				if ((Object)(object)component != (Object)null && !NetworkBool.op_Implicit(component.IsDead) && ((SimulationBehaviour)__instance).HasStateAuthority)
				{
					PortalCustom portalCustomComponent = ((Component)__instance).GetComponent<PortalCustom>();
					List<Portal> list = ((!((Object)(object)portalCustomComponent != (Object)null)) ? (from p in Object.FindObjectsOfType<Portal>()
						where (Object)(object)p != (Object)(object)__instance && (Object)(object)((Component)p).GetComponent<PortalCustom>() == (Object)null
						select p).ToList() : (from p in Object.FindObjectsOfType<Portal>()
						where (Object)(object)p != (Object)(object)__instance && (Object)(object)((Component)p).GetComponent<PortalCustom>() != (Object)null && ((Component)p).GetComponent<PortalCustom>().MapID == portalCustomComponent.MapID
						select p).ToList());
					if (list.Any())
					{
						__instance.ResetPortal();
						Portal val = CollectionsUtil.Grab<Portal>(list, 1).First();
						val.ResetPortal();
						Transform value2 = Traverse.Create((object)val).Field<Transform>("teleportPoint").Value;
						Vector3 position = ((Component)val).transform.position;
						Traverse.Create((object)__instance).Method("Rpc_DisplayLight", Array.Empty<object>()).GetValue();
						Traverse.Create((object)val).Method("Rpc_DisplayLight", Array.Empty<object>()).GetValue();
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("TELEPORT_START"), ((Component)__instance).transform.position, 30f, 1f);
						component.CharacterMovementHandler.TeleportData = new NetworkTeleportData(value2.position, value2.rotation, true);
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("TELEPORT_END"), position, 30f, 1f);
						PlayerCustom.ApplyEffectToPlayer(component, "LycansNewRoles.EffectTrapResistance", ((SimulationBehaviour)__instance).Runner);
						if (SabotageManager.Instance.IsSabotageActive(SabotageManager.SabotageIds.Portals) && NetworkBool.op_Implicit(component.IsWolf))
						{
							component.Hunger = Mathf.Min((float)GameManager.Instance.MaxHunger, component.Hunger + 0.35f * (float)GameManager.Instance.MaxHunger);
							PlayerCustom.ApplyEffectToPlayer(component, "LycansNewRoles.EffectPortal", ((SimulationBehaviour)__instance).Runner, 1f, 8f);
						}
					}
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PortalCustomTimerPatch error: " + ex));
			return true;
		}
	}
}
