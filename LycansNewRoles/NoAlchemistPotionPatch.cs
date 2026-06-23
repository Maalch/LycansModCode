using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "GiveAlchemistPotions")]
public class NoAlchemistPotionPatch
{
	private static bool Prefix(GameManager __instance)
	{
		int[] array = new int[5];
		List<Effect> value = Traverse.Create((object)__instance).Field<List<Effect>>("_potionEffects").Value;
		for (int i = 0; i < value.Count; i++)
		{
			array[i] = EffectManager.GetEffectIndex(value[i]);
		}
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
}
