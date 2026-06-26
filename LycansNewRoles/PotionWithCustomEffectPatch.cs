using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using LycansNewRoles.Stats;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Potion), "ItemTriggered")]
public class PotionWithCustomEffectPatch
{
	private static bool Prefix(Potion __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected I4, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)__instance).Owner);
			ItemCustom componentInChildren = ((Component)__instance).GetComponentInChildren<ItemCustom>();
			if (NetworkBool.op_Implicit(componentInChildren.Sabotaged))
			{
				PlayerCustom.ApplyEffectToPlayer(player.PlayerController, "LycansNewRoles.EffectPoisoned", ((SimulationBehaviour)__instance).Runner);
				return false;
			}
			PlayerController playerController = player.PlayerController;
			Effect val = EffectManager.GetEffect(__instance.EffectIndex);
			Color[] value = Traverse.Create(typeof(Potion)).Field<Color[]>("PotionColors").Value;
			if (__instance.ColorIndex == value.Length - 1)
			{
				if (((SimulationBehaviour)__instance).Runner.IsServer && !NetworkBool.op_Implicit(playerController.IsDead) && !NetworkBool.op_Implicit(player.Dying))
				{
					playerController.Feed((int)(0.2f * (float)GameManager.Instance.MaxHunger));
					PlayerCustom.ApplyEffectToPlayer(playerController, val, ((SimulationBehaviour)__instance).Runner, 0.6f);
					if (player.Stats != null)
					{
						player.Stats.AddAction(new PlayerStats.PlayerAction
						{
							ActionType = "DrinkPotion",
							ActionName = "Blanche - " + TranslationManager.Instance.GetTranslation(val.GetTranslateKey())
						}, ((Component)player.PlayerController).transform.position);
					}
				}
				return false;
			}
			if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothAlcoholic)
			{
				if (NetworkBool.op_Implicit(playerController.IsWolf))
				{
					if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
					{
						playerController.Feed((int)(0.2f * (float)GameManager.Instance.MaxHunger));
					}
					else
					{
						playerController.Feed((int)(0.45f * (float)GameManager.Instance.MaxHunger));
					}
					return false;
				}
				EffectType val2 = ((val is CustomEffect customEffect) ? customEffect.CustomEffectType : val.GetEffectType());
				EffectType val3 = val2;
				EffectType val4 = val3;
				switch ((int)val4)
				{
				case 1:
					val = CollectionsUtil.Grab<Effect>((from o in EffectManager.GetEffects()
						where (int)o.GetEffectType() == 0
						select o).ToList(), 1).First();
					break;
				case 2:
					val = CollectionsUtil.Grab<Effect>((from o in EffectManager.GetEffects()
						where (int)o.GetEffectType() == 1
						select o).ToList(), 1).First();
					break;
				}
			}
			if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothBlueMage)
			{
				player.SecondaryRoleUniqueInt = __instance.EffectIndex;
			}
			if (player.Stats != null)
			{
				player.Stats.AddAction(new PlayerStats.PlayerAction
				{
					ActionType = "DrinkPotion",
					ActionName = TranslationManager.Instance.GetTranslation(val.GetTranslateKey())
				}, ((Component)player.PlayerController).transform.position);
			}
			PlayerCustom.ApplyEffectToPlayer(playerController, val, ((SimulationBehaviour)__instance).Runner);
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PotionWithCustomEffectPatch error: " + ex));
			return true;
		}
	}
}
