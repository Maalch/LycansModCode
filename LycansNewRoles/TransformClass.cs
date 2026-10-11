using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewPrimaryRoles;
using LycansNewRoles.Sabotages;
using LycansNewRoles.Stats;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

public class TransformClass
{
	public static bool TransformPrefix(PlayerController __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
		if ((Object)(object)player != (Object)null && NetworkBool.op_Implicit(player.IsWolfPup))
		{
			if (((SimulationBehaviour)__instance).Object.HasInputAuthority)
			{
				UIManager.ShowRedCenterMessage("NALES_UI_WOLF_PUP_NO_TRANSFORM", 0.5f, 4f);
			}
			return false;
		}
		if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
		{
			if (((SimulationBehaviour)__instance).Object.HasInputAuthority)
			{
				UIManager.ShowRedCenterMessage("NALES_UI_ACTION_CANNOT_TRANSFORM_BEAST", 0.5f, 4f);
			}
			return false;
		}
		if (NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
		{
			if (((SimulationBehaviour)__instance).Object.HasInputAuthority)
			{
				UIManager.ShowRedCenterMessage("NALES_UI_ACTION_CANNOT_TRANSFORM_CULTIST", 0.5f, 4f);
			}
			return false;
		}
		if (NetworkBool.op_Implicit(VoodooManager.Instance.VoodooActive))
		{
			if (((SimulationBehaviour)__instance).Object.HasInputAuthority)
			{
				UIManager.ShowRedCenterMessage("NALES_UI_ACTION_CANNOT_TRANSFORM_VOODOO", 0.5f, 4f);
			}
			return false;
		}
		if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Possessor)
		{
			PlayerRef primaryRoleTargetRef = player.PrimaryRoleTargetRef;
			if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials && NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool))
			{
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(player.PrimaryRoleTargetRef);
				if (NetworkBool.op_Implicit(player2.PlayerController.IsWolf) || NetworkBool.op_Implicit(player2.PlayerController.IsDead))
				{
					return false;
				}
				Traverse.Create((object)__instance).Property("WolfDelay", (object[])null).SetValue((object)TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, (float)GameManager.Instance.TransformationTime));
				AudioManager.PlayPosition("WOLF_TRANSFORM", ((Component)player2).transform.position, (MixerTarget)2, 30f, 1f);
				__instance.CharacterMovementHandler.TeleportData = new NetworkTeleportData(((Component)player2).transform.position, ((Component)player2).transform.rotation, true);
				__instance.IsClimbing = NetworkBool.op_Implicit(false);
				player.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
				player2.TeleportToVoid();
				player2.PlayerController.PlayerEffectManager.ClearEffects();
				PlayerCustom.ApplyEffectToPlayer(player2.PlayerController, "LycansNewRoles.EffectPossessed", ((SimulationBehaviour)__instance).Runner);
				goto IL_0355;
			}
		}
		bool flag = player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Sneak && NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool);
		if (((SimulationBehaviour)__instance).Object.HasStateAuthority)
		{
			float num = GameManager.Instance.TransformationTime;
			if (flag)
			{
				num *= 2f;
			}
			if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Eclipse)
			{
				num += 2f;
			}
			Traverse.Create((object)__instance).Property("WolfDelay", (object[])null).SetValue((object)TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, num));
		}
		if (!flag)
		{
			AudioManager.PlayAndFollow("WOLF_TRANSFORM", ((Component)__instance).transform, (MixerTarget)2, 30f, 1f);
		}
		goto IL_0355;
		IL_0355:
		Traverse.Create((object)__instance).Field<ParticleSystem>("smokeParticleSystem").Value.Play();
		__instance.TransformedNight = NetworkBool.op_Implicit(true);
		__instance.IsWolf = NetworkBool.op_Implicit(true);
		__instance.MovementAction = 0;
		__instance.CanMoveAnimation = NetworkBool.op_Implicit(false);
		__instance.IsZooming = NetworkBool.op_Implicit(false);
		if (((SimulationBehaviour)__instance).Object.HasInputAuthority)
		{
			if (NetworkBool.op_Implicit(GameManager.Instance.WolfRevert))
			{
				GameManager.Instance.gameUI.UpdateAction("UI_ACTION_TRANSFORM_BACK", (InputActionName)6, Array.Empty<object>());
			}
			else
			{
				GameManager.Instance.gameUI.HideAction();
			}
		}
		return false;
	}

	public static void TransformPostfix(PlayerController __instance)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Invalid comparison between Unknown and I4
		try
		{
			if ((Object)(object)__instance == (Object)(object)PlayerController.Local && ((SimulationBehaviour)__instance).Object.HasInputAuthority)
			{
				ShowRoleDescriptionPatch.NeedsUpdate = true;
			}
			if (!((SimulationBehaviour)__instance).Object.HasStateAuthority)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			__instance.IsAiming = NetworkBool.op_Implicit(false);
			__instance.Hunger = GameManager.Instance.MaxHunger;
			if (!NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				if (SabotageManager.Instance.IsSabotageActive(SabotageManager.SabotageIds.WolvesRitual))
				{
					PlayerCustom.ApplyEffectToPlayer(__instance, "LycansNewRoles.EffectEmpowered", ((SimulationBehaviour)__instance).Runner);
				}
				if (player.GainEmpoweredOnEachTransformation)
				{
					PlayerCustom.ApplyEffectToPlayer(__instance, "LycansNewRoles.EffectEmpowered", ((SimulationBehaviour)__instance).Runner);
				}
				if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Rage)
				{
					if (GameManagerCustom.Instance.EventsManager.CurrentEventUniqueBool)
					{
						PlayerCustom.ApplyEffectToPlayer(__instance, "LycansNewRoles.EffectWeakened", ((SimulationBehaviour)__instance).Runner, 1f, 3600f);
					}
					else
					{
						PlayerCustom.ApplyEffectToPlayer(__instance, "LycansNewRoles.EffectEmpowered", ((SimulationBehaviour)__instance).Runner, 1f, 3600f);
						GameManagerCustom.Instance.EventsManager.EventUniqueMethod();
					}
				}
				if (NetworkBool.op_Implicit(Plugin.CustomConfig.TenacityHubris) && (int)__instance.Role == 1)
				{
					if (BalancingValues.WolvesHaveTenacity())
					{
						PlayerCustom.ApplyEffectToPlayer(__instance, "LycansNewRoles.EffectTenacity", ((SimulationBehaviour)__instance).Runner);
					}
					else if (BalancingValues.WolvesHaveHubris())
					{
						PlayerCustom.ApplyEffectToPlayer(__instance, "LycansNewRoles.EffectHubris", ((SimulationBehaviour)__instance).Runner);
					}
				}
			}
			PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Scientist);
			if ((Object)(object)specificNewPrimaryRole != (Object)null)
			{
				PlayerController playerController = specificNewPrimaryRole.PlayerController;
				if (!NetworkBool.op_Implicit(playerController.IsDead))
				{
					float num = Vector3.Distance(((Component)playerController).transform.position, ((Component)__instance).transform.position);
					float num2 = 40f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
					if (num <= num2)
					{
						float num3 = ScientistUtility.GetBasePower(specificNewPrimaryRole, __instance, num, num2) * 5f;
						if (LycansUtility.CanPlayerSeeOtherPlayer(specificNewPrimaryRole, player, num2))
						{
							num3 *= 4f;
						}
						int amount = Mathf.RoundToInt(num3 * BalancingValues.SoloRoleDiminishingReturnsMultiplier(specificNewPrimaryRole.SoloRoleHalfDayProgress, 7f));
						specificNewPrimaryRole.AddSoloRoleProgress(amount, BalancingValues.ScientistGoal(PlayerCustomRegistry.Count));
					}
				}
			}
			PlayerCustom.ApplyEffectToPlayer(player.PlayerController, "LycansNewRoles.EffectSpiritResistance", ((SimulationBehaviour)__instance).Runner, 1f, 6f);
			if (NetworkBool.op_Implicit(player.PlayerController.IsWolf))
			{
				string text = DateTime.UtcNow.ToString();
				NetworkString<_32> username = __instance.PlayerData.Username;
				LycansUtility.AddLogOnlyForMe("Adding transformation from regular wolf transformation at date: " + text + ", player: " + ((object)username/*cast due to constrained. prefix*/).ToString());
				GameManagerCustom.Instance.AddTransformation();
				player.Stats.AddAction(new PlayerStats.PlayerAction
				{
					ActionType = "Transform"
				}, ((Component)player.PlayerController).transform.position);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TransformPatch error: " + ex));
		}
	}
}
