using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewMaps;
using LycansNewRoles.NewMaps.Components;
using LycansNewRoles.PowerObjects;
using LycansNewRoles.Sabotages;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Interact")]
internal class InteractPatch
{
	private static bool Prefix(bool isPrimary, PlayerController __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Invalid comparison between Unknown and I4
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f80: Invalid comparison between Unknown and I4
		//IL_0f8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0faf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_1213: Unknown result type (might be due to invalid IL or missing references)
		//IL_10dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1147: Unknown result type (might be due to invalid IL or missing references)
		//IL_1012: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_127d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1024: Unknown result type (might be due to invalid IL or missing references)
		//IL_1034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1171: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Invalid comparison between Unknown and I4
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_0967: Invalid comparison between Unknown and I4
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Invalid comparison between Unknown and I4
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e67: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0900: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Invalid comparison between Unknown and I4
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c99: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			if (NetworkBool.op_Implicit(__instance.IsAiming) && (int)__instance.Role != 2)
			{
				bool flag = false;
				if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Poacher || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary || player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothCarabineer)
				{
					flag = NetworkBool.op_Implicit(__instance.IsGunLoaded);
				}
				if (!flag)
				{
					return false;
				}
				PlayerRef val = PlayerRef.None;
				GameObject value = Traverse.Create((object)__instance).Field<GameObject>("_gunTargetObject").Value;
				if ((Object)(object)value != (Object)null)
				{
					PlayerController componentInParent = value.GetComponentInParent<PlayerController>();
					if ((Object)(object)componentInParent != (Object)null)
					{
						val = componentInParent.Ref;
					}
				}
				if (((SimulationBehaviour)__instance).HasInputAuthority)
				{
					if (val != PlayerRef.None)
					{
						PlayerController player2 = PlayerRegistry.GetPlayer(val);
						PlayerCustom.Rpc_Custom_Shot_With_Target(((SimulationBehaviour)__instance).Runner, __instance.Index, player2.Index);
					}
					else
					{
						PlayerCustom.Rpc_Custom_Shot_Without_Target(((SimulationBehaviour)__instance).Runner, __instance.Index);
					}
				}
				return false;
			}
			if ((int)GameManager.LocalGameState == 2 && (Object)(object)__instance.targetObject != (Object)null && !NetworkBool.op_Implicit(__instance.IsAiming))
			{
				if (NetworkBool.op_Implicit(player.Dying) || NetworkBool.op_Implicit(player.Petrified) || NetworkBool.op_Implicit(player.Asleep) || NetworkBool.op_Implicit(player.Banished) || NetworkBool.op_Implicit(player.CapturedByCultist))
				{
					return false;
				}
				PlayerController componentInParent2 = __instance.targetObject.GetComponentInParent<PlayerController>();
				if ((Object)(object)componentInParent2 != (Object)null)
				{
					PlayerCustom player3 = PlayerCustomRegistry.GetPlayer(componentInParent2.Ref);
					if (!isPrimary && NetworkBool.op_Implicit(componentInParent2.IsDead) && (Object)(object)componentInParent2.Item != (Object)null)
					{
						PlayerCustom.Rpc_Loot_Corpse(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
						return false;
					}
					if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper && !isPrimary && player.PrimaryRolePowerRemainingUses > 0 && player.PrimaryRoleTargetRef == PlayerRef.None && !NetworkBool.op_Implicit(player3.Kidnapped) && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(player3.Downed) && !NetworkBool.op_Implicit(componentInParent2.IsWolf))
					{
						PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
						PlayerCustom.PlaySuccessSound();
						return false;
					}
					if (!InteractionsManager.NormalInteractionAvailable)
					{
						return false;
					}
					if (NetworkBool.op_Implicit(player3.Dying) && !NetworkBool.op_Implicit(__instance.IsWolf) && player.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie)
					{
						PlayerCustom.Rpc_Save(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
						return false;
					}
					if (NetworkBool.op_Implicit(player.Assassin) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(componentInParent2.PlayerEffectManager.Invisible))
					{
						if (!NetworkBool.op_Implicit(componentInParent2.IsWolf) && GameManagerCustom.Instance.CurrentDay == 1 && (int)player.PlayerController.Role != 1 && player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None)
						{
							return false;
						}
						PlayerCustom.Rpc_Assassinate(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
						return false;
					}
					if (NetworkBool.op_Implicit(player.Midas) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
					{
						PlayerCustom.Rpc_Petrify(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
						return false;
					}
					if (player3.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary && NetworkBool.op_Implicit(player3.NewPrimaryRoleUniqueBool) && !NetworkBool.op_Implicit(player3.PlayerController.IsDead))
					{
						PlayerCustom.Rpc_Assassinate(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
						return false;
					}
					if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent && player3.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent && NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && componentInParent2.Ref != __instance.Ref && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(componentInParent2.PlayerEffectManager.Invisible) && !NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
					{
						PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
						return false;
					}
					if (NetworkBool.op_Implicit(player.BombActive) && !NetworkBool.op_Implicit(player.Panic) && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
					{
						PlayerCustom.Rpc_Give_Bomb(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
						return false;
					}
					if (NetworkBool.op_Implicit(__instance.IsWolf))
					{
						if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
						{
							if (__instance.IsCanMove() && !NetworkBool.op_Implicit(componentInParent2.IsDead))
							{
								if (!NetworkBool.op_Implicit(player3.Dying) && !NetworkBool.op_Implicit(player3.Petrified) && !NetworkBool.op_Implicit(player3.Angel))
								{
									PlayerCustom.Rpc_Wolf_Attack(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
								}
								return false;
							}
						}
						else if (NetworkBool.op_Implicit(__instance.CanMove) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && (int)__instance.Role == 1)
						{
							if (!NetworkBool.op_Implicit(player3.Dying) && !NetworkBool.op_Implicit(player3.Petrified) && !NetworkBool.op_Implicit(player3.Angel) && ((int)componentInParent2.Role != 1 || (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && NetworkBool.op_Implicit(componentInParent2.IsWolf))))
							{
								PlayerCustom.Rpc_Wolf_Attack(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							}
							return false;
						}
					}
					switch (player.NewPrimaryRole)
					{
					case PlayerCustom.PlayerNewPrimaryRole.VillageIdiot:
						if (player.PrimaryRolePowerRemainingUses <= 0 || NetworkBool.op_Implicit(__instance.IsWolf) || NetworkBool.op_Implicit(componentInParent2.IsDead) || NetworkBool.op_Implicit(componentInParent2.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
						{
							break;
						}
						switch (player.SoloRoleObjectiveTarget)
						{
						case 0:
							if (!NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
							{
								PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
								PlayerCustom.PlaySuccessSound();
								return false;
							}
							break;
						case 1:
							if (!NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
							{
								PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
								PlayerCustom.PlaySuccessSound();
								return false;
							}
							break;
						case 2:
						{
							if (!((Object)(object)componentInParent2.Item != (Object)null) || NetworkBool.op_Implicit(((Component)componentInParent2.Item).GetComponentInChildren<ItemCustom>().Sabotaged))
							{
								break;
							}
							TickTimer val2 = componentInParent2.Item.TriggerTimer;
							if (!((TickTimer)(ref val2)).IsRunning)
							{
								val2 = componentInParent2.Item.AnimationTimer;
								if (!((TickTimer)(ref val2)).IsRunning && !NetworkBool.op_Implicit(componentInParent2.IsZooming))
								{
									PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
									PlayerCustom.PlaySuccessSound();
									return false;
								}
							}
							break;
						}
						}
						break;
					case PlayerCustom.PlayerNewPrimaryRole.Beast:
						if (player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(player3.BeastMark) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					case PlayerCustom.PlayerNewPrimaryRole.Voodoo:
						if (NetworkBool.op_Implicit(componentInParent2.IsDead) && (int)componentInParent2.Role != 1 && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && player3.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(player3.Disappeared))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							return false;
						}
						break;
					case PlayerCustom.PlayerNewPrimaryRole.Zombie:
						if (__instance.IsCanMove() && !NetworkBool.op_Implicit(componentInParent2.IsDead) && player3.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Voodoo && player3.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && !NetworkBool.op_Implicit(player3.Dying) && !NetworkBool.op_Implicit(player3.Petrified))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							return false;
						}
						break;
					}
					switch (player.PrimaryRolePower)
					{
					case PlayerCustom.PlayerPrimaryRolePower.Necromancer:
						if (NetworkBool.op_Implicit(__instance.CanMove) && !NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool) && player3.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && NetworkBool.op_Implicit(componentInParent2.IsDead))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Deceiver:
						if (player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(player3.DeceiverTrickAllTime) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Warlock:
						if (player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(player3.CurseDormant) && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Possessor:
						if (LycansUtility.GameActuallyInPlay && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !player3.AlreadyPossessed)
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Saboteur:
						if (player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && (Object)(object)componentInParent2.Item != (Object)null && !NetworkBool.op_Implicit(componentInParent2.IsZooming) && !NetworkBool.op_Implicit(((Component)componentInParent2.Item).GetComponentInChildren<ItemCustom>().Sabotaged))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							AudioManager.Play("KILL_2", (MixerTarget)2, 0.4f, 1f);
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Bomber:
						if (player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Avenger:
						if (player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials && !NetworkBool.op_Implicit(componentInParent2.IsDead))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Survivalist:
						if (player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(componentInParent2.IsWolf))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Priest:
						if (player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(componentInParent2.IsWolf))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Investigator:
						if (player.PrimaryRoleTargetRef == componentInParent2.Ref && !NetworkBool.op_Implicit(componentInParent2.IsWolf))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					}
				}
				else
				{
					if (NetworkBool.op_Implicit(Plugin.CustomConfig.TrapsModified))
					{
						Trap component = __instance.targetObject.GetComponent<Trap>();
						if ((Object)(object)component != (Object)null && component.TrapCanBeDisarmed())
						{
							PlayerCustom.Rpc_Disarm_Trap(((SimulationBehaviour)__instance).Runner, __instance.Index, ((SimulationBehaviour)component).Object.Id);
							return false;
						}
					}
					if (NetworkBool.op_Implicit(Plugin.CustomConfig.SabotagesAvailable))
					{
						SabotageComponent component2 = __instance.targetObject.GetComponent<SabotageComponent>();
						if ((Object)(object)component2 != (Object)null)
						{
							SabotageInfo sabotageInfo = SabotageManager.SabotageObjectsInfo[((Object)__instance.targetObject).name];
							float range = sabotageInfo.Range;
							if (((int)__instance.Role == 1 || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor) && !NetworkBool.op_Implicit(__instance.IsWolf) && Vector3.Distance(((Component)__instance).transform.position, __instance.targetObject.transform.position) <= range && !NetworkBool.op_Implicit(component2.SabotageObject.Completed))
							{
								SabotageManager.Rpc_Sabotage(((SimulationBehaviour)__instance).Runner, player.Index, component2.SabotageObject.SabotageObjectIndex, (!isPrimary) ? 1 : 0);
								return false;
							}
							if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.VillageIdiot && !NetworkBool.op_Implicit(__instance.IsWolf) && Vector3.Distance(((Component)__instance).transform.position, __instance.targetObject.transform.position) <= range)
							{
								SabotageManager.Rpc_Sabotage(((SimulationBehaviour)__instance).Runner, player.Index, component2.SabotageObject.SabotageObjectIndex, 0);
								return false;
							}
						}
					}
					Loot component3 = __instance.targetObject.GetComponent<Loot>();
					if ((Object)(object)component3 != (Object)null && player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Alchemist && !isPrimary)
					{
						if (player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials)
						{
							PlayerCustom.Rpc_Manipulate_Item(((SimulationBehaviour)__instance).Runner, player.Index, ((SimulationBehaviour)component3).Object.Id);
							PlayerCustom.PlaySuccessSound();
						}
						return false;
					}
					Item component4 = __instance.targetObject.GetComponent<Item>();
					if ((Object)(object)component4 != (Object)null && player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Saboteur && !isPrimary)
					{
						if (player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials && !NetworkBool.op_Implicit(((Component)component4).GetComponentInChildren<ItemCustom>().Sabotaged))
						{
							PlayerCustom.Rpc_Manipulate_Item(((SimulationBehaviour)__instance).Runner, player.Index, ((SimulationBehaviour)component4).Object.Id);
							AudioManager.Play("KILL_2", (MixerTarget)2, 0.4f, 1f);
						}
						return false;
					}
					AdminTable component5 = __instance.targetObject.GetComponent<AdminTable>();
					if ((Object)(object)component5 != (Object)null)
					{
						Plugin.Minimap.SetState(MinimapComponent.MinimapState.Admin);
					}
					MechanismButton component6 = __instance.targetObject.GetComponent<MechanismButton>();
					if ((Object)(object)component6 != (Object)null)
					{
						component6.Rpc_Activate(__instance.Ref);
					}
					SleepingGasPlaced component7 = __instance.targetObject.GetComponent<SleepingGasPlaced>();
					if ((Object)(object)component7 != (Object)null)
					{
						SleepingGasPlaced.Rpc_Take_Sleeping_Gas(((SimulationBehaviour)__instance).Runner, __instance.Ref);
					}
					MagicianIllusion component8 = __instance.targetObject.GetComponent<MagicianIllusion>();
					if ((Object)(object)component8 != (Object)null)
					{
						PlayerCustom.Rpc_Attack_Magician_Illusion(((SimulationBehaviour)__instance).Runner, player.Index);
					}
					CultistSkull component9 = __instance.targetObject.GetComponent<CultistSkull>();
					if ((Object)(object)component9 != (Object)null)
					{
						CultistSkull.Rpc_Destroy_Skull(((SimulationBehaviour)__instance).Runner, player.Index, ((SimulationBehaviour)component9).Object.Id);
					}
					HostParasite component10 = __instance.targetObject.GetComponent<HostParasite>();
					if ((Object)(object)component10 != (Object)null && NetworkBool.op_Implicit(component10.Appeared))
					{
						HostParasite.Rpc_Destroy_Parasite(((SimulationBehaviour)__instance).Runner, player.Index, ((SimulationBehaviour)component10).Object.Id);
					}
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SecondaryRoleDetectiveInspectPatch error: " + ex));
			return true;
		}
	}
}
