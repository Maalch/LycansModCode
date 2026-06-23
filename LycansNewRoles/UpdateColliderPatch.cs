using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "UpdateCollider")]
internal class UpdateColliderPatch
{
	private static bool Prefix(PlayerController __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.Phasing))
			{
				Traverse val = Traverse.Create((object)__instance).Method("UpdatePlayerCollider", new List<Type>
				{
					typeof(PlayerController),
					typeof(ColliderData)
				}.ToArray(), (object[])null);
				ColliderData value = Traverse.Create(typeof(PlayerController)).Field<ColliderData>("DeadVillagerColliderData").Value;
				val.GetValue(new object[2] { __instance, value });
				return false;
			}
			if (NetworkBool.op_Implicit(player.Dying) || NetworkBool.op_Implicit(player.Asleep))
			{
				Traverse val2 = Traverse.Create((object)__instance).Method("UpdatePlayerCollider", new List<Type>
				{
					typeof(PlayerController),
					typeof(ColliderData)
				}.ToArray(), (object[])null);
				ColliderData val3 = (NetworkBool.op_Implicit(__instance.IsWolf) ? Traverse.Create(typeof(PlayerController)).Field<ColliderData>("DeadWolfColliderData").Value : Traverse.Create(typeof(PlayerController)).Field<ColliderData>("DeadVillagerColliderData").Value);
				val2.GetValue(new object[2] { __instance, val3 });
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateColliderPatch error: " + ex));
			return true;
		}
	}
}
