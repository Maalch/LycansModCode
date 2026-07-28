using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "UpdatePotions")]
internal class DraftUpdatePotionsPatch
{
	private static bool Prefix(GameManager __instance)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		if (NetworkBool.op_Implicit(DraftManager.Instance.Active))
		{
			return false;
		}
		if (__instance.AlchemistsCount > 0 || __instance.PotionsCount > 0)
		{
			List<Effect> value = Traverse.Create((object)__instance).Field<List<Effect>>("_potionEffects").Value;
			if (!value.Any())
			{
				Traverse.Create((object)__instance).Method("GenerateEffects", Array.Empty<object>()).GetValue();
			}
			if (__instance.PotionsCount > 0)
			{
				int num = 4 * __instance.ItemsSpawnRate;
				for (int i = 0; i < num; i++)
				{
					int randomEffectIndex = Random.Range(0, __instance.PotionsCount);
					Effect randomEffect = value[randomEffectIndex];
					if (randomEffect is AssassinEffect && GameManagerCustom.Instance.CurrentDay <= 1)
					{
						continue;
					}
					Traverse val = Traverse.Create((object)__instance).Method("GetAndLockRandomItemSpawn", Array.Empty<object>());
					Potion value2 = Traverse.Create((object)__instance).Field<Potion>("potionPrefab").Value;
					ItemSpawner val2 = (ItemSpawner)val.GetValue();
					if ((Object)(object)val2 != (Object)null)
					{
						((SimulationBehaviour)__instance).Runner.Spawn<Potion>(value2, (Vector3?)((Component)val2).transform.position, (Quaternion?)((Component)val2).transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
						{
							((Component)no).GetComponent<Potion>().Init(randomEffectIndex, EffectManager.GetEffectIndex(randomEffect));
						}, (NetworkObjectPredictionKey?)null, true);
					}
				}
			}
		}
		return true;
	}
}
