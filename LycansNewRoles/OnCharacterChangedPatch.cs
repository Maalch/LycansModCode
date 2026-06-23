using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnCharacterChanged")]
internal class OnCharacterChangedPatch
{
	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController behaviour = changed.Behaviour;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(behaviour.Ref);
			player.UpdateMoveSpeed();
			player.UpdateVisibility();
			if (!NetworkBool.op_Implicit(behaviour.IsWolf) && ((SimulationBehaviour)behaviour).Runner.IsServer)
			{
				Effect val = behaviour.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is RevertingEffect);
				if ((Object)(object)val != (Object)null)
				{
					behaviour.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
				}
				Effect val2 = behaviour.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is CursedEffect);
				if ((Object)(object)val2 != (Object)null)
				{
					behaviour.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val2).Object.Id);
				}
				Effect val3 = behaviour.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is ExorcismEffect);
				if ((Object)(object)val3 != (Object)null)
				{
					behaviour.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val3).Object.Id);
				}
				Effect val4 = behaviour.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is TenacityEffect);
				if ((Object)(object)val4 != (Object)null)
				{
					behaviour.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val4).Object.Id);
				}
				Effect val5 = behaviour.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is HubrisEffect);
				if ((Object)(object)val5 != (Object)null)
				{
					behaviour.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val5).Object.Id);
				}
			}
			if (player.IsCurrentlyPlayedOrObserved)
			{
				foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer.UpdateVisibility();
					allPlayer.UpdateIllusion();
				}
				MagicianIllusion.UpdateVisibilityForAllMagicianIllusions();
				MysticRepulsor.UpdateVisibilityForAllRepulsors();
			}
			player.UpdateScaleAndPitch();
			player.UpdateWolfColor();
			((Component)player.PlayerController).GetComponent<PlayerSpotterLightComponent>().UpdateState();
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (player2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Scientist)
			{
				player2.UpdateTargetArrowComponent();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnCharacterChangedPatch error: " + ex));
		}
	}
}
