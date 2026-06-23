using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdateLoot")]
internal class UpdateLootPatch
{
	private static bool Prefix(GameManager __instance, bool reset)
	{
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			List<Loot> list = (from l in Object.FindObjectsOfType<Loot>()
				where l.GetMapID() == GameManager.Instance.MapID
				select l).ToList();
			int num = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => !NetworkBool.op_Implicit(p.IsDead))).Count();
			int num2 = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => (int)p.Role == 1 && !NetworkBool.op_Implicit(p.IsDead))).Count();
			int num3 = PlayerCustomRegistry.CountWhere((PlayerCustom p) => p.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor);
			int num4 = PlayerRegistry.Count - num + num2 * 4 + num3 * 2;
			if (num4 < 0)
			{
				num4 = 0;
			}
			float num5 = (float)__instance.LootSpawnRate * 25f * BalancingValues.LootGenerationMultiplier(__instance.MapID);
			float num6 = (float)num4 * num5;
			int num7 = 0;
			if (reset)
			{
				list.ForEach(delegate(Loot loot)
				{
					//IL_0003: Unknown result type (might be due to invalid IL or missing references)
					loot.Available = NetworkBool.op_Implicit(false);
				});
			}
			else
			{
				num6 *= 0.75f;
			}
			int num8 = list.Count((Loot l) => !NetworkBool.op_Implicit(l.Available));
			while ((float)num7 < num6 && num8 > 0)
			{
				Loot val = CollectionsUtil.Grab<Loot>(list.Where((Loot l) => !NetworkBool.op_Implicit(l.Available)).ToList(), 1).ToList()[0];
				num7 += val.ScoreValue;
				val.Available = NetworkBool.op_Implicit(true);
				num8--;
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateLootPatch error: " + ex));
			return true;
		}
	}
}
