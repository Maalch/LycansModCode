using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using LycansNewRoles.Stats;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnDeadChanged")]
internal class OnDeadChangedPatch
{
	private static void Prefix(Changed<PlayerController> changed)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Invalid comparison between Unknown and I4
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Invalid comparison between Unknown and I4
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController deadPlayerController = changed.Behaviour;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(deadPlayerController.Ref);
			player.CurseDormant = NetworkBool.op_Implicit(false);
			player.CurseTimer = TickTimer.None;
			player.BombDormant = NetworkBool.op_Implicit(false);
			player.BombActive = NetworkBool.op_Implicit(false);
			player.Kidnapped = NetworkBool.op_Implicit(false);
			player.Parasite = NetworkBool.op_Implicit(false);
			PlayerController local = PlayerController.Local;
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(local.Ref);
			if (NetworkBool.op_Implicit(deadPlayerController.IsDead) && deadPlayerController.Ref == local.Ref)
			{
				UIManager.HideAllExtraUI();
			}
			if (NetworkBool.op_Implicit(deadPlayerController.IsDead) && !NetworkBool.op_Implicit(local.IsDead) && (Object)(object)local != (Object)(object)deadPlayerController && player2.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothMedium)
			{
				AudioManager.Play("BELL", (MixerTarget)2, 0.7f, 1f);
				if ((int)GameManager.LocalGameState != 1)
				{
					GameManager.Instance.gameUI.UpdateDeadPlayer(deadPlayerController.Ref);
				}
			}
			if (((SimulationBehaviour)deadPlayerController).Runner.IsServer)
			{
				PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Mercenary);
				if (NetworkBool.op_Implicit(deadPlayerController.IsDead) && (Object)(object)specificNewPrimaryRole != (Object)null && specificNewPrimaryRole.PrimaryRoleTargetRef == deadPlayerController.Ref && !NetworkBool.op_Implicit(specificNewPrimaryRole.PlayerController.IsDead))
				{
					if (specificNewPrimaryRole.Ref == deadPlayerController.Killer)
					{
						specificNewPrimaryRole.SoloRoleObjectiveCount += 200;
						specificNewPrimaryRole.PrimaryRoleTargetRef = PlayerRef.None;
						specificNewPrimaryRole.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)deadPlayerController).Runner, 90f);
					}
					else
					{
						specificNewPrimaryRole.MercenaryGiveNewTarget();
					}
				}
				if (NetworkBool.op_Implicit(deadPlayerController.IsDead) && player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Avatar && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && !NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
				{
					GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)changed.Behaviour).Runner, NetworkString<_16>.op_Implicit("WOLF"), ((Component)changed.Behaviour).transform.position, 500f, 0.6f);
				}
				if (NetworkBool.op_Implicit(deadPlayerController.IsDead) && (int)deadPlayerController.Role == 1 && !NetworkBool.op_Implicit(player.Resurrected) && !PlayerCustomRegistry.Any((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Necromancer && o.PrimaryRoleTargetRef == deadPlayerController.Ref))
				{
					foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.IsWolfPup)))
					{
						item.IsWolfPup = NetworkBool.op_Implicit(false);
						item.Stats.MainRoleChanges.Add(new PlayerStats.MainRoleChangeEvent(TranslationManager.Instance.GetTranslationForStats(UpdateRoleUtility.GetNewPrimaryRoleKey(item.PlayerController, item)).Replace("{0}", "").Replace("{1}", "")
							.Replace("{2}", "")));
					}
				}
				if (NetworkBool.op_Implicit(deadPlayerController.IsDead))
				{
					IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf) && (int)o.PlayerController.Role == 1);
					bool flag = BalancingValues.WolvesHaveTenacity();
					bool flag2 = BalancingValues.WolvesHaveHubris();
					foreach (PlayerCustom item2 in enumerable)
					{
						if (NetworkBool.op_Implicit(item2.Tenacity) && !flag)
						{
							Effect val = item2.PlayerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is TenacityEffect);
							if ((Object)(object)val != (Object)null)
							{
								item2.PlayerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
							}
						}
						if (NetworkBool.op_Implicit(item2.Hubris) && !flag2)
						{
							Effect val2 = item2.PlayerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is HubrisEffect);
							if ((Object)(object)val2 != (Object)null)
							{
								item2.PlayerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val2).Object.Id);
							}
						}
					}
				}
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper)
				{
					player.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
				}
				PlayerCustom playerCustom = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Possessor).FirstOrDefault();
				if ((Object)(object)playerCustom != (Object)null && playerCustom.PrimaryRoleTargetRef == player.Ref)
				{
					playerCustom.PrimaryRoleTargetRef = PlayerRef.None;
					playerCustom.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
				}
				if (player.Stats != null)
				{
					player.Stats.OnTalkingChanged(talking: false);
				}
			}
			if (!NetworkBool.op_Implicit(deadPlayerController.IsDead) && player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
			{
				player.HasZombieColor = true;
			}
			((Component)deadPlayerController).GetComponent<PlayerResurrectedComponent>().UpdateState();
			((Component)deadPlayerController).GetComponent<PlayerSpotterLightComponent>().UpdateState();
			player.UpdateVisibility();
			ColorAdjustmentManager.UpdateColorAdjustment();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnDeadChangedPatch error: " + ex));
		}
	}

	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Invalid comparison between Unknown and I4
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Invalid comparison between Unknown and I4
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController behaviour = changed.Behaviour;
			PlayerCustom deadPlayerCustom = PlayerCustomRegistry.GetPlayer(behaviour.Ref);
			if (NetworkBool.op_Implicit(behaviour.IsDead))
			{
				deadPlayerCustom.PoacherMark = NetworkBool.op_Implicit(false);
				if (deadPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Spy)
				{
					deadPlayerCustom.PrimaryRoleTargetRef = PlayerRef.None;
				}
				deadPlayerCustom.CustomAudio.UpdateReverbIfNeeded((AudioReverbPreset)0);
				if ((int)GameManager.LocalGameState == 4)
				{
					List<PlayerController> list = PlayerRegistry.Where((Predicate<PlayerController>)delegate(PlayerController o)
					{
						//IL_0001: Unknown result type (might be due to invalid IL or missing references)
						//IL_0019: Unknown result type (might be due to invalid IL or missing references)
						//IL_001e: Unknown result type (might be due to invalid IL or missing references)
						int result;
						if (!NetworkBool.op_Implicit(o.IsDead))
						{
							int idVoted = o.IdVoted;
							PlayerRef val2 = deadPlayerCustom.Ref;
							result = ((idVoted == ((PlayerRef)(ref val2)).PlayerId) ? 1 : 0);
						}
						else
						{
							result = 0;
						}
						return (byte)result != 0;
					}).ToList();
					foreach (PlayerController item in list)
					{
						item.IdVoted = -1;
					}
					behaviour.IdVoted = -1;
				}
				PlayerCustom playerCustom = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Mole && o.PlayerController.Killer == changed.Behaviour.Ref).FirstOrDefault();
				if ((Object)(object)playerCustom != (Object)null)
				{
					Effect val = playerCustom.PlayerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is MoleClockEffect);
					if ((Object)(object)val != (Object)null)
					{
						playerCustom.PlayerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
					}
				}
			}
			if ((Object)(object)behaviour == (Object)(object)PlayerController.Local && !NetworkBool.op_Implicit(behaviour.IsDead) && (int)GameManager.LocalGameState == 2)
			{
				GameManager.Instance.gameUI.ShowSpectateMenu(false);
				GameManager.Instance.gameUI.ShowGameMenu(true);
				PlayerController.Local.LocalCameraHandler.SwitchPov(behaviour);
				if (deadPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
				{
					GameManager.Instance.gameUI.UpdateRole(deadPlayerCustom.PlayerController.Role);
				}
				else
				{
					GameManager.Instance.gameUI.UpdateAlly("");
					UIManager.ShowRedCenterMessage("NALES_UI_RESURRECT_NOTIFICATION", 0.4f, 4f);
				}
			}
			if (deadPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Voodoo)
			{
				foreach (PlayerCustom item2 in PlayerCustomRegistry.Where((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie))
				{
					item2.PlayerController.Rpc_Kill(PlayerRef.None);
				}
			}
			behaviour.EnablePlayerHitBox(!NetworkBool.op_Implicit(behaviour.IsDead));
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewPrimaryRoleNecromancerOnResurrectedPatch error: " + ex));
		}
	}
}
