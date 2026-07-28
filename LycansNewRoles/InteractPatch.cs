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
		//IL_0fee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1053: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ad: Invalid comparison between Unknown and I4
		//IL_10ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_103a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1311: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_1340: Unknown result type (might be due to invalid IL or missing references)
		//IL_1209: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1274: Unknown result type (might be due to invalid IL or missing references)
		//IL_113f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_13aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1151: Unknown result type (might be due to invalid IL or missing references)
		//IL_1161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_129e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Invalid comparison between Unknown and I4
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Invalid comparison between Unknown and I4
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f16: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Invalid comparison between Unknown and I4
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f86: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa2: Unknown result type (might be due to invalid IL or missing references)
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
						if (player.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(player3.BeastMark) && !NetworkBool.op_Implicit(componentInParent2.IsWolf) && !NetworkBool.op_Implicit(componentInParent2.IsDead))
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
