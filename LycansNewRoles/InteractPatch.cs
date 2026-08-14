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
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Invalid comparison between Unknown and I4
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f95: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_104e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1054: Invalid comparison between Unknown and I4
		//IL_1061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1073: Unknown result type (might be due to invalid IL or missing references)
		//IL_1083: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_121b: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_1351: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_1376: Unknown result type (might be due to invalid IL or missing references)
		//IL_1245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_139d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Invalid comparison between Unknown and I4
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Invalid comparison between Unknown and I4
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0828: Unknown result type (might be due to invalid IL or missing references)
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_093d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e56: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_094f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Invalid comparison between Unknown and I4
		//IL_0a33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e64: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f49: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			if (NetworkBool.op_Implicit(__instance.IsAiming) && (int)__instance.Role != 2)
			{
				bool flag = false;
				if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary || player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothCarabineer)
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
					if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper && !isPrimary && player.PrimaryRolePowerRemainingUses > 0 && player.PrimaryRoleTargetRef == PlayerRef.None && !NetworkBool.op_Implicit(player3.Kidnapped) && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(player3.Dying) && !NetworkBool.op_Implicit(componentInParent2.IsWolf))
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
					if (NetworkBool.op_Implicit(player3.CapturedByCultist) && player.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie)
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
						if (player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(player3.BeastMark) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(player.PlayerController.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							PlayerCustom.PlaySuccessSound();
							return false;
						}
						break;
					case PlayerCustom.PlayerNewPrimaryRole.Voodoo:
						if (NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && player3.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie && player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(player3.Disappeared))
						{
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
							return false;
						}
						if (!NetworkBool.op_Implicit(componentInParent2.IsDead) && player3.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(player3.Strengthened))
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
					case PlayerCustom.PlayerPrimaryRolePower.Inventor:
						if (player.PrimaryRolePowerCurrentMaterials >= player.PowerMaterialsInfo.RequiredMaterials && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(player3.Dying))
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
