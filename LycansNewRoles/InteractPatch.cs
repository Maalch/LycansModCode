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
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Invalid comparison between Unknown and I4
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1092: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_114b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1151: Invalid comparison between Unknown and I4
		//IL_115e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10de: Unknown result type (might be due to invalid IL or missing references)
		//IL_1170: Unknown result type (might be due to invalid IL or missing references)
		//IL_1180: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_1318: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_144e: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_1473: Unknown result type (might be due to invalid IL or missing references)
		//IL_1342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_149a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Invalid comparison between Unknown and I4
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_076b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Invalid comparison between Unknown and I4
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0960: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f53: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0980: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Invalid comparison between Unknown and I4
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_101c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_102a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1038: Unknown result type (might be due to invalid IL or missing references)
		//IL_1046: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			if (NetworkBool.op_Implicit(__instance.IsAiming) && player.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.Hunter)
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
								if (!NetworkBool.op_Implicit(player3.Dying) && !NetworkBool.op_Implicit(player3.Petrified) && !NetworkBool.op_Implicit(player3.Angel) && !NetworkBool.op_Implicit(player3.Sanctuary))
								{
									PlayerCustom.Rpc_Wolf_Attack(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
								}
								return false;
							}
						}
						else if (NetworkBool.op_Implicit(__instance.CanMove) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && (int)__instance.Role == 1)
						{
							if (!NetworkBool.op_Implicit(player3.Dying) && !NetworkBool.op_Implicit(player3.Petrified) && !NetworkBool.op_Implicit(player3.Angel) && !NetworkBool.op_Implicit(player3.Sanctuary) && ((int)componentInParent2.Role != 1 || (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && NetworkBool.op_Implicit(componentInParent2.IsWolf))))
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
					case PlayerCustom.PlayerPrimaryRolePower.Tracker:
						if (!NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead) && !NetworkBool.op_Implicit(player3.Dying))
						{
							if (NetworkBool.op_Implicit(player3.Tracked))
							{
								PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
								PlayerCustom.PlaySuccessSound();
							}
							else if (PlayerCustomRegistry.CountWhere((PlayerCustom o) => NetworkBool.op_Implicit(o.Tracked)) < 2)
							{
								PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, player.Index, componentInParent2.Index);
								PlayerCustom.PlaySuccessSound();
							}
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
