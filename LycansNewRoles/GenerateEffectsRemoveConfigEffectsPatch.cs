using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "GenerateEffects")]
public class GenerateEffectsRemoveConfigEffectsPatch
{
	private static bool Prefix(GameManager __instance)
	{
		try
		{
			List<Effect> list = (from o in EffectManager.GetEffects()
				where Plugin.CustomConfig.PotionsAvailability[o.GetTranslateKey()]
				select o).ToList();
			List<Effect> tempEffectList = new List<Effect>();
			int[] array = new int[5];
			Effect val = CollectionsUtil.Grab<Effect>(list.Where((Effect e) => (int)e.GetEffectType() == 0).ToList(), 1).First();
			if ((Object)(object)val != (Object)null)
			{
				tempEffectList.Add(val);
				list.Remove(val);
			}
			if (__instance.PotionsCount > 0)
			{
				Effect val2 = CollectionsUtil.Grab<Effect>(list.Where((Effect e) => (int)e.GetEffectType() > 0).ToList(), 1).First();
				if ((Object)(object)val2 != (Object)null)
				{
					tempEffectList.Add(val2);
					list.Remove(val2);
				}
				CollectionsUtil.Grab<Effect>(list, __instance.PotionsCount - 2).ToList().ForEach(delegate(Effect e)
				{
					tempEffectList.Add(e);
				});
			}
			List<Effect> list2 = tempEffectList.OrderBy((Effect _) => Guid.NewGuid()).ToList();
			for (int num = 0; num < list2.Count; num++)
			{
				array[num] = EffectManager.GetEffectIndex(list2[num]);
			}
			List<Effect> value = Traverse.Create((object)__instance).Field<List<Effect>>("_potionEffects").Value;
			value.AddRange(list2);
			__instance.GiveAlchemistPotions();
			Traverse.Create(typeof(GameManager)).Method("Rpc_AlchemistEffects", new List<Type>
			{
				typeof(NetworkRunner),
				typeof(int[])
			}.ToArray(), (object[])null).GetValue(new object[2]
			{
				((SimulationBehaviour)__instance).Runner,
				array
			});
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PotionsEffectWithConfigPatch error: " + ex));
			StackTrace stackTrace = new StackTrace();
			Plugin.Logger.LogError((object)("StackTrace: " + stackTrace));
			return true;
		}
	}
}
