using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.PowerObjects;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "CheckLocalInputs")]
internal class LocalInputPatches
{
	public static Stopwatch? PrimaryInteractionHoldTimer = new Stopwatch();

	public static Stopwatch? CrouchHoldTimer = new Stopwatch();

	private static void AdditionalInputFunctions(PlayerController playerController, PlayerCustom playerCustom)
	{
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Invalid comparison between Unknown and I4
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Invalid comparison between Unknown and I4
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Invalid comparison between Unknown and I4
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Invalid comparison between Unknown and I4
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Invalid comparison between Unknown and I4
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Invalid comparison between Unknown and I4
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Invalid comparison between Unknown and I4
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Expected O, but got Unknown
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		if (InputManager.Instance.CrouchJustPressed)
		{
			CrouchHoldTimer.Restart();
		}
		if (CrouchHoldTimer.IsRunning && !InputManager.Instance.CrouchHeld)
		{
			CrouchHoldTimer.Reset();
		}
		if ((Object)(object)playerCustom.AstralSpirit != (Object)null && playerCustom.AstralSpirit.CanShift && (InputManager.Instance.PrimaryInteractJustPressed || InputManager.Instance.SecondaryInteractJustPressed))
		{
			PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, 7);
		}
		if ((Object)(object)playerCustom.SummonedSpirit != (Object)null && playerCustom.SummonedSpirit.CanShift && (InputManager.Instance.PrimaryInteractJustPressed || InputManager.Instance.SecondaryInteractJustPressed))
		{
			PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, 7);
		}
		TickTimer val;
		if ((Object)(object)playerCustom.SummonedSpirit != (Object)null && (Object)(object)playerCustom.SummonedSpirit.TooltipTarget != (Object)null && (playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist || !playerCustom.SummonedSpirit.TooltipTarget.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is SpiritResistanceEffect)))
		{
			val = playerCustom.SummonedSpirit.AttackCooldown;
			if (!((TickTimer)(ref val)).IsRunning && (InputManager.Instance.PrimaryInteractJustPressed || InputManager.Instance.SecondaryInteractJustPressed))
			{
				PlayerCustom.Rpc_Spirit_Attack(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, playerCustom.SummonedSpirit.TooltipTarget.Index);
			}
		}
		if ((Object)(object)playerCustom.SummonedSpirit != (Object)null)
		{
			val = playerCustom.SummonedSpirit.SpellCooldown;
			if (!((TickTimer)(ref val)).IsRunning && InputManager.Instance.PrimaryActionJustPressed)
			{
				PlayerCustom.Rpc_Spirit_Spell(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index);
			}
		}
		if ((int)GameManager.LocalGameState == 2)
		{
			if (NetworkBool.op_Implicit(Plugin.CustomConfig.DropItemsAvailable) && (Object)(object)playerController.Item != (Object)null && !(playerController.Item is BulletItem) && !NetworkBool.op_Implicit(playerCustom.Tiny) && !NetworkBool.op_Implicit(playerCustom.PlayerController.PlayerEffectManager.Giant))
			{
				if (InputManager.Instance.PrimaryInteractJustPressed || InputManager.Instance.SecondaryInteractJustPressed)
				{
					PrimaryInteractionHoldTimer.Restart();
				}
				if (PrimaryInteractionHoldTimer.IsRunning && !InputManager.Instance.PrimaryInteractHeld && !InputManager.Instance.SecondaryInteractHeld)
				{
					PrimaryInteractionHoldTimer.Reset();
				}
				if (PrimaryInteractionHoldTimer.IsRunning && PrimaryInteractionHoldTimer.ElapsedMilliseconds >= 1000 && NetworkBool.op_Implicit(playerController.CanMoveAnimation) && !NetworkBool.op_Implicit(playerController.IsClimbing))
				{
					PlayerCustom.Rpc_Drop_Item(((SimulationBehaviour)playerController).Runner, playerCustom.Index);
					PrimaryInteractionHoldTimer.Reset();
				}
			}
			else if (PrimaryInteractionHoldTimer.IsRunning)
			{
				PrimaryInteractionHoldTimer.Reset();
			}
		}
		if (!playerCustom.CanPerformActions)
		{
			return;
		}
		if (InputManagerExtra.Instance.ItemSecondaryJustPressed)
		{
			SleepingGasPlaced sleepingGasPlaced = SleepingGasPlaced.FindPlayerPlacedSleepingGas(PlayerController.Local.Ref);
			if ((int)GameManager.LocalGameState == 2 && (Object)(object)sleepingGasPlaced != (Object)null)
			{
				PlayerCustom.Rpc_Activate_Item_Secondary(((SimulationBehaviour)playerController).Runner, playerCustom.Index);
			}
		}
		if (InputManagerExtra.Instance.AccessoryActionJustPressed)
		{
			if ((int)GameManager.LocalGameState == 4 && playerCustom.Accessory is AccessoryCrystalBall accessoryCrystalBall && ((int)playerCustom.PlayerController.Role == 1 || playerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor) && accessoryCrystalBall.Available)
			{
				if (UIManager.GenericChoicePanel.Active)
				{
					UIManager.GenericChoicePanel.Hide();
					GameManager.Instance.gameUI.UpdateCursor(false);
				}
				else
				{
					List<GameObject> buttonsForPlayers = UIGenericChoicePanel.GetButtonsForPlayers((from o in PlayerCustomRegistry
						where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Kidnapped) && o.Ref != PlayerController.Local.Ref
						select o.Ref).ToList());
					foreach (GameObject crystalBallGuessPlayerButton in buttonsForPlayers)
					{
						crystalBallGuessPlayerButton.GetComponent<UIGenericChoiceButtonPlayer>().SetAction((UnityAction)delegate
						{
							//IL_0010: Unknown result type (might be due to invalid IL or missing references)
							UIManager.GenericChoicePanel.OnClickCrystalBallGuessTarget(crystalBallGuessPlayerButton.GetComponent<UIGenericChoiceButtonPlayer>().PlayerRef);
						});
					}
					UIManager.GenericChoicePanel.Show(buttonsForPlayers, "NALES_UI_CHOICE_PANEL_HEADER_SEER_PLAYER");
					GameManager.Instance.gameUI.UpdateCursor(true);
				}
			}
			if ((int)GameManager.LocalGameState == 2 && playerCustom.Accessory is AccessorySpellbook accessorySpellbook)
			{
				val = ((Item)accessorySpellbook).ItemTimer;
				if (!((TickTimer)(ref val)).IsRunning && (bool)Traverse.Create((object)accessorySpellbook).Method("CanUseItem", Array.Empty<object>()).GetValue())
				{
					Traverse.Create((object)accessorySpellbook).Method("ItemTriggered", Array.Empty<object>()).GetValue();
				}
			}
			if ((int)GameManager.LocalGameState == 2 && playerCustom.Accessory is AccessoryBackpack accessoryBackpack && (Object)(object)playerCustom.PlayerController.Item != (Object)null && (Object)(object)accessoryBackpack.ItemInside != (Object)null)
			{
				val = ((Item)accessoryBackpack).ItemTimer;
				if (!((TickTimer)(ref val)).IsRunning && (bool)Traverse.Create((object)accessoryBackpack).Method("CanUseItem", Array.Empty<object>()).GetValue())
				{
					Traverse.Create((object)accessoryBackpack).Method("ItemTriggered", Array.Empty<object>()).GetValue();
				}
			}
		}
		if (InputManagerExtra.Instance.SecondaryRoleActionJustPressed && playerCustom.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTinkerer && (int)GameManager.LocalGameState == 4 && playerCustom.SecondaryRoleUniqueInt > 0)
		{
			PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)playerController).Runner, playerCustom.Index);
			PlayerCustom.PlaySuccessSound();
		}
		if (InputManager.Instance.SecondaryInteractJustPressed && playerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Host && !NetworkBool.op_Implicit(playerController.IsDead) && LycansUtility.GameActuallyInPlay && PlayerCustomRegistry.Any((PlayerCustom o) => NetworkBool.op_Implicit(o.Parasite)))
		{
			PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)playerController).Runner, playerCustom.Index, playerCustom.Index);
			PlayerCustom.PlaySuccessSound();
		}
	}

	private static void Postfix(PlayerController __instance)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Invalid comparison between Unknown and I4
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d5: Invalid comparison between Unknown and I4
		//IL_1eaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_10dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec1: Invalid comparison between Unknown and I4
		//IL_2539: Unknown result type (might be due to invalid IL or missing references)
		//IL_253f: Invalid comparison between Unknown and I4
		//IL_24ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected I4, but got Unknown
		//IL_1ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2546: Unknown result type (might be due to invalid IL or missing references)
		//IL_2550: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a9: Invalid comparison between Unknown and I4
		//IL_2222: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2294: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_23bc: Invalid comparison between Unknown and I4
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f03: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1733: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f30: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Invalid comparison between Unknown and I4
		//IL_12fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1555: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_179d: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2421: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_145e: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1235: Unknown result type (might be due to invalid IL or missing references)
		//IL_123a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1963: Unknown result type (might be due to invalid IL or missing references)
		//IL_2465: Unknown result type (might be due to invalid IL or missing references)
		//IL_23eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1250: Unknown result type (might be due to invalid IL or missing references)
		//IL_1255: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e8: Invalid comparison between Unknown and I4
		//IL_1b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Expected O, but got Unknown
		//IL_1a65: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b22: Invalid comparison between Unknown and I4
		//IL_1b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Expected O, but got Unknown
		//IL_1a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_16dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e7: Expected O, but got Unknown
		//IL_1a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c65: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1867: Unknown result type (might be due to invalid IL or missing references)
		//IL_1871: Expected O, but got Unknown
		//IL_0d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9e: Expected O, but got Unknown
		//IL_1ab4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1daa: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState == 0 || !((SimulationBehaviour)__instance).Object.HasInputAuthority || GameManager.Instance.gameUI.IsSettingMenuOpen)
		{
			return;
		}
		PlayerCustom playerCustom = PlayerCustomRegistry.GetPlayer(__instance.Ref);
		try
		{
			if (playerCustom.CanPerformActions)
			{
				try
				{
					bool flag = true;
					EGameState localGameState = GameManager.LocalGameState;
					EGameState val = localGameState;
					switch ((int)val)
					{
					case 1:
					case 5:
						flag = true;
						break;
					case 0:
						flag = false;
						break;
					case 2:
						flag = NetworkBool.op_Implicit(__instance.CanMoveAnimation) && !NetworkBool.op_Implicit(__instance.IsMoving) && !NetworkBool.op_Implicit(__instance.IsAiming) && !NetworkBool.op_Implicit(__instance.IsClimbing);
						break;
					case 3:
						flag = false;
						break;
					case 4:
						flag = __instance.IdVoted == -1;
						break;
					}
					if (flag)
					{
						if (InputManagerExtra.Instance.Emote1JustPressed)
						{
							PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)__instance).Runner, playerCustom.Index, 1);
						}
						if (InputManagerExtra.Instance.Emote2JustPressed)
						{
							PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)__instance).Runner, playerCustom.Index, 2);
						}
						if (InputManagerExtra.Instance.Emote3JustPressed)
						{
							PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)__instance).Runner, playerCustom.Index, 3);
						}
						if (InputManagerExtra.Instance.Emote4JustPressed)
						{
							PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)__instance).Runner, playerCustom.Index, 4);
						}
						if (InputManagerExtra.Instance.Emote5JustPressed)
						{
							PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)__instance).Runner, playerCustom.Index, 5);
						}
						if (InputManagerExtra.Instance.Emote6JustPressed)
						{
							PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)__instance).Runner, playerCustom.Index, 6);
						}
						if (InputManagerExtra.Instance.Emote7JustPressed)
						{
							PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)__instance).Runner, playerCustom.Index, 7);
						}
						if (InputManagerExtra.Instance.Emote8JustPressed)
						{
							PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)__instance).Runner, playerCustom.Index, 8);
						}
					}
				}
				catch (Exception ex)
				{
					Plugin.Logger.LogInfo((object)("CanEmote exception: " + ex));
				}
				TickTimer val2;
				try
				{
					if (InputManager.Instance.PrimaryActionJustPressed)
					{
						if (NetworkBool.op_Implicit(__instance.IsWolf))
						{
							TickTimer value = Traverse.Create((object)__instance).Property<TickTimer>("WolfDelay", (object[])null).Value;
							if (((TickTimer)(ref value)).IsRunning)
							{
								return;
							}
						}
						switch (playerCustom.PrimaryRolePower)
						{
						case PlayerCustom.PlayerPrimaryRolePower.Avatar:
							if (!LycansUtility.GameActuallyInPlay)
							{
								break;
							}
							if (!((Object)(object)__instance.Item == (Object)null))
							{
								val2 = __instance.Item.AnimationTimer;
								if (((TickTimer)(ref val2)).IsRunning)
								{
									break;
								}
								val2 = __instance.Item.TriggerTimer;
								if (((TickTimer)(ref val2)).IsRunning)
								{
									break;
								}
							}
							if (playerCustom.PlayerController.Hunger >= 0.5f * (float)GameManager.Instance.MaxHunger)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Exorcist:
							if (LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(__instance.CanMove) && playerCustom.PrimaryRolePowerRemainingUses > 0)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Survivalist:
							if (LycansUtility.GameActuallyInPlay && playerCustom.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(playerCustom.SurvivalistBuff))
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Investigator:
							if (UIManager.DetectivePanel.Active)
							{
								UIManager.DetectivePanel.Hide();
								GameManager.Instance.gameUI.UpdateCursor(false);
							}
							else
							{
								UIManager.DetectivePanel.Show();
								GameManager.Instance.gameUI.UpdateCursor(false);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Scout:
							if (LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(__instance.CanMove) && playerCustom.PrimaryRolePowerRemainingUses > 0)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Magician:
							if (LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(__instance.CanMove) && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Shadow:
							if (!LycansUtility.GameActuallyInPlay || !LycansUtility.WolvesCanTransform)
							{
								break;
							}
							if (!NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
							{
								if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
								{
									PlayerCustom.PlaySuccessSound();
									PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								}
							}
							else
							{
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Hermit:
							if (LycansUtility.GameActuallyInPlay && LycansUtility.WolvesCanTransform && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials && playerCustom.CanPerformActions)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Runemaster:
							if (LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(__instance.CanMove))
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Spotter:
							if (LycansUtility.GameActuallyInPlay && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Purifier:
							if (LycansUtility.GameActuallyInPlay && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Necromancer:
							if (LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(__instance.CanMove) && NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool) && NetworkBool.op_Implicit(PlayerRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef).IsDead) && playerCustom.PrimaryRolePowerCurrentMaterials >= 10000 && LycansUtility.WolvesCanTransform && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Warlock:
						{
							if (!LycansUtility.GameActuallyInPlay)
							{
								break;
							}
							if (UIManager.GenericChoicePanel.Active)
							{
								UIManager.GenericChoicePanel.Hide();
								GameManager.Instance.gameUI.UpdateCursor(false);
								break;
							}
							if (__instance.MovementAction == 1)
							{
								__instance.MovementAction = 0;
							}
							__instance.UpdateIsMoving(false);
							List<GameObject> buttonsForPlayers2 = UIGenericChoicePanel.GetButtonsForPlayers((from o in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead)))
								select o.Ref).ToList());
							foreach (GameObject playerButton2 in buttonsForPlayers2)
							{
								playerButton2.GetComponent<UIGenericChoiceButtonPlayer>().SetAction((UnityAction)delegate
								{
									//IL_0010: Unknown result type (might be due to invalid IL or missing references)
									UIManager.GenericChoicePanel.OnClickShapeshiftingTarget(playerButton2.GetComponent<UIGenericChoiceButtonPlayer>().PlayerRef);
								});
							}
							UIManager.GenericChoicePanel.Show(buttonsForPlayers2, "NALES_UI_CHOICE_PANEL_HEADER_SHAPESHIFT");
							GameManager.Instance.gameUI.UpdateCursor(true);
							break;
						}
						case PlayerCustom.PlayerPrimaryRolePower.Possessor:
						{
							if (!LycansUtility.GameActuallyInPlay)
							{
								break;
							}
							PlayerRef primaryRoleTargetRef = playerCustom.PrimaryRoleTargetRef;
							if (((PlayerRef)(ref primaryRoleTargetRef)).IsNone || playerCustom.PrimaryRolePowerCurrentMaterials < playerCustom.PowerMaterialsInfo.RequiredMaterials)
							{
								break;
							}
							if (!NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf) || !NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef).Possessed))
							{
								if (!NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
								{
									PlayerCustom.PlaySuccessSound();
								}
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							return;
						}
						case PlayerCustom.PlayerPrimaryRolePower.Ritualist:
							if (!LycansUtility.GameActuallyInPlay)
							{
								break;
							}
							if (UIManager.GenericChoicePanel.Active)
							{
								UIManager.GenericChoicePanel.Hide();
								GameManager.Instance.gameUI.UpdateCursor(false);
							}
							else
							{
								if (playerCustom.PrimaryRolePowerRemainingUses <= 0)
								{
									break;
								}
								if (__instance.MovementAction == 1)
								{
									__instance.MovementAction = 0;
								}
								__instance.UpdateIsMoving(false);
								List<GameObject> buttonsForRitualist = UIGenericChoicePanel.GetButtonsForRitualist();
								foreach (GameObject effectButton in buttonsForRitualist)
								{
									effectButton.GetComponent<UIGenericChoiceButtonRitualistEffect>().SetAction((UnityAction)delegate
									{
										UIManager.GenericChoicePanel.OnClickRitualistEffect(effectButton.GetComponent<UIGenericChoiceButtonRitualistEffect>().Effect);
									});
								}
								UIManager.GenericChoicePanel.Show(buttonsForRitualist, "NALES_UI_CHOICE_PANEL_HEADER_RITUAL");
								GameManager.Instance.gameUI.UpdateCursor(true);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Predator:
							if (!LycansUtility.GameActuallyInPlay)
							{
								break;
							}
							if (UIManager.GenericChoicePanel.Active)
							{
								UIManager.GenericChoicePanel.Hide();
								GameManager.Instance.gameUI.UpdateCursor(false);
							}
							else
							{
								if (!(playerCustom.PrimaryRoleTargetRef == PlayerRef.None) || NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) || NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf))
								{
									break;
								}
								if (__instance.MovementAction == 1)
								{
									__instance.MovementAction = 0;
								}
								__instance.UpdateIsMoving(false);
								List<PlayerRef> players = (from o in PlayerCustomRegistry
									where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Kidnapped) && o.Ref != PlayerController.Local.Ref
									select o.Ref).ToList();
								List<GameObject> buttonsForPlayers = UIGenericChoicePanel.GetButtonsForPlayers(players);
								foreach (GameObject playerButton in buttonsForPlayers)
								{
									playerButton.GetComponent<UIGenericChoiceButtonPlayer>().SetAction((UnityAction)delegate
									{
										//IL_0010: Unknown result type (might be due to invalid IL or missing references)
										UIManager.GenericChoicePanel.OnClickPredatorTarget(playerButton.GetComponent<UIGenericChoiceButtonPlayer>().PlayerRef);
									});
								}
								UIManager.GenericChoicePanel.Show(buttonsForPlayers, "NALES_UI_CHOICE_PANEL_HEADER_CHOOSE_PREY");
								GameManager.Instance.gameUI.UpdateCursor(true);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Sneak:
							if (LycansUtility.GameActuallyInPlay)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Host:
							if (LycansUtility.GameActuallyInPlay && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
							{
								if (PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsDead) && Vector3.Distance(((Component)o).transform.position, ((Component)playerCustom.PlayerController).transform.position) <= 3f)))
								{
									UIManager.ShowRedCenterMessage("NALES_UI_ACTION_CULTIST_CANNOT_PLACE_SKULL_CORPSE", 0.4f, 4f);
									return;
								}
								if (Object.FindObjectsOfType<Teleporter>().Any((Teleporter o) => o.MapID == GameManager.Instance.MapID && Vector3.Distance(((Component)o).transform.position, ((Component)playerCustom.PlayerController).transform.position) <= 2f))
								{
									UIManager.ShowRedCenterMessage("NALES_UI_ACTION_HOST_CANNOT_PLACE_PARASITE_TELEPORTER", 0.4f, 4f);
									return;
								}
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Acrobat:
							if (!LycansUtility.GameActuallyInPlay || !NetworkBool.op_Implicit(__instance.IsWolf))
							{
								break;
							}
							val2 = playerCustom.PrimaryRoleActionTimer;
							if (!((TickTimer)(ref val2)).IsRunning)
							{
								AcrobatSpot[] source = Object.FindObjectsOfType<AcrobatSpot>();
								if (source.Any((AcrobatSpot o) => Vector3.Distance(((Component)o).transform.position, ((Component)__instance).transform.position) <= 1f))
								{
									PlayerCustom.PlaySuccessSound();
									PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								}
							}
							break;
						}
						switch (playerCustom.NewPrimaryRole)
						{
						case PlayerCustom.PlayerNewPrimaryRole.VillageIdiot:
							PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							break;
						case PlayerCustom.PlayerNewPrimaryRole.Scientist:
							if (LycansUtility.GameActuallyInPlay)
							{
								val2 = playerCustom.PrimaryRoleActionTimer;
								if (!((TickTimer)(ref val2)).IsRunning && playerCustom.PrimaryRolePowerRemainingUses > 0)
								{
									PlayerCustom.PlaySuccessSound();
									PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								}
							}
							break;
						case PlayerCustom.PlayerNewPrimaryRole.Lover:
							if (LycansUtility.GameActuallyInPlay)
							{
								if (!NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
								{
									PlayerCustom.PlaySuccessSound();
								}
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerNewPrimaryRole.Kidnapper:
							PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							break;
						}
					}
				}
				catch (Exception ex2)
				{
					Plugin.Logger.LogInfo((object)("PrimaryAction exception: " + ex2));
				}
				try
				{
					if (InputManagerExtra.Instance.SecondaryRoleActionJustPressed && (int)GameManager.LocalGameState == 2 && !NetworkBool.op_Implicit(DraftManager.Instance.Active))
					{
						switch (playerCustom.SecondaryRole)
						{
						case PlayerCustom.PlayerSecondaryRole.BothAlcoholic:
							if (playerCustom.SecondaryRoleFirstRemainingUses <= 0 || !((Object)(object)__instance.Item != (Object)null) || __instance.Item is Potion)
							{
								break;
							}
							val2 = __instance.Item.AnimationTimer;
							if (!((TickTimer)(ref val2)).IsRunning)
							{
								val2 = __instance.Item.TriggerTimer;
								if (!((TickTimer)(ref val2)).IsRunning)
								{
									PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
									PlayerCustom.PlaySuccessSound();
								}
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothEngineer:
							if (playerCustom.SecondaryRoleFirstRemainingUses <= 0)
							{
								break;
							}
							if (!((Object)(object)__instance.Item == (Object)null))
							{
								val2 = __instance.Item.ItemTimer;
								if (((TickTimer)(ref val2)).IsRunning)
								{
									break;
								}
								val2 = __instance.Item.TriggerTimer;
								if (((TickTimer)(ref val2)).IsRunning)
								{
									break;
								}
							}
							PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							PlayerCustom.PlaySuccessSound();
							break;
						case PlayerCustom.PlayerSecondaryRole.BothSherif:
							if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && (int)__instance.Role == 1)
							{
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								PlayerCustom.PlaySuccessSound();
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothGambler:
							if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && NetworkBool.op_Implicit(__instance.IsWolf))
							{
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								PlayerCustom.PlaySuccessSound();
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothIllusionist:
							if (playerCustom.SecondaryRoleFirstRemainingUses > 0)
							{
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								PlayerCustom.PlaySuccessSound();
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothInfected:
							if (playerCustom.SecondaryRoleFirstRemainingUses > 0)
							{
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								if (NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf))
								{
									PlayerCustom.PlaySuccessSound();
								}
								else
								{
									AudioManager.Play("AngelHeal", (MixerTarget)2, 0.4f, 1f);
								}
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothSprinter:
							if (NetworkBool.op_Implicit(__instance.IsWolf))
							{
								if (playerCustom.SecondaryRoleFirstRemainingUses > 0)
								{
									PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
									PlayerCustom.PlaySuccessSound();
								}
							}
							else if (__instance.Hunger >= (float)GameManager.Instance.MaxHunger * 0.5f && !NetworkBool.op_Implicit(playerCustom.Sprinting))
							{
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								PlayerCustom.PlaySuccessSound();
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothTeleporter:
						{
							if (playerCustom.SecondaryRoleFirstRemainingUses <= 0)
							{
								break;
							}
							NetworkTeleportData secondaryRoleTeleportData = playerCustom.SecondaryRoleTeleportData;
							if (((NetworkTeleportData)(ref secondaryRoleTeleportData)).IsNone)
							{
								if (!NetworkBool.op_Implicit(__instance.IsClimbing))
								{
									PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
									PlayerCustom.PlaySuccessSound();
								}
							}
							else
							{
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								PlayerCustom.PlaySuccessSound();
							}
							break;
						}
						case PlayerCustom.PlayerSecondaryRole.BothAstral:
							if (playerCustom.SecondaryRoleFirstRemainingUses > 0)
							{
								if (!NetworkBool.op_Implicit(playerCustom.SecondaryRolePowerActive))
								{
									PlayerCustom.PlaySuccessSound();
								}
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothActor:
							if (playerCustom.SecondaryRoleFirstRemainingUses > 0)
							{
								if (!NetworkBool.op_Implicit(playerCustom.SecondaryRolePowerActive))
								{
									PlayerCustom.PlaySuccessSound();
								}
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothScribe:
							if (playerCustom.SecondaryRoleFirstRemainingUses > 0)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							break;
						case PlayerCustom.PlayerSecondaryRole.BothImitator:
						{
							if (!LycansUtility.GameActuallyInPlay)
							{
								break;
							}
							if (UIManager.GenericChoicePanel.Active)
							{
								UIManager.GenericChoicePanel.Hide();
								GameManager.Instance.gameUI.UpdateCursor(false);
								break;
							}
							if (__instance.MovementAction == 1)
							{
								__instance.MovementAction = 0;
							}
							__instance.UpdateIsMoving(false);
							List<GameObject> buttonsForImitator = UIGenericChoicePanel.GetButtonsForImitator(playerCustom);
							foreach (GameObject effectButton2 in buttonsForImitator)
							{
								effectButton2.GetComponent<UIGenericChoiceButtonImitatorRole>().SetAction((UnityAction)delegate
								{
									UIManager.GenericChoicePanel.OnClickImitatorRole(effectButton2.GetComponent<UIGenericChoiceButtonImitatorRole>().Role);
								});
							}
							UIManager.GenericChoicePanel.Show(buttonsForImitator, "NALES_UI_CHOICE_PANEL_HEADER_IMITATOR");
							GameManager.Instance.gameUI.UpdateCursor(true);
							break;
						}
						case PlayerCustom.PlayerSecondaryRole.BothTelepath:
							if (!NetworkBool.op_Implicit(playerCustom.SecondaryRolePowerActive))
							{
								PlayerCustom.PlaySuccessSound();
							}
							PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							break;
						case PlayerCustom.PlayerSecondaryRole.BothCarabineer:
							PlayerCustom.Rpc_Activate_Secondary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							break;
						case PlayerCustom.PlayerSecondaryRole.BothMerchant:
						{
							if (!LycansUtility.GameActuallyInPlay)
							{
								break;
							}
							val2 = playerCustom.SecondaryRoleActionTimer;
							if (((TickTimer)(ref val2)).IsRunning)
							{
								break;
							}
							if (UIManager.GenericChoicePanel.Active)
							{
								UIManager.GenericChoicePanel.Hide();
								GameManager.Instance.gameUI.UpdateCursor(false);
								break;
							}
							if (__instance.MovementAction == 1)
							{
								__instance.MovementAction = 0;
							}
							__instance.UpdateIsMoving(false);
							List<GameObject> buttonsForMerchant = UIGenericChoicePanel.GetButtonsForMerchant(playerCustom.CurrentMerchantOffers);
							foreach (GameObject playerButton3 in buttonsForMerchant)
							{
								playerButton3.GetComponent<UIGenericChoiceButtonMerchantOffer>().SetAction((UnityAction)delegate
								{
									UIManager.GenericChoicePanel.OnClickMerchantOffer(playerButton3.GetComponent<UIGenericChoiceButtonMerchantOffer>().Offer);
								});
							}
							UIManager.GenericChoicePanel.Show(buttonsForMerchant, "NALES_UI_CHOICE_PANEL_HEADER_MERCHANT");
							GameManager.Instance.gameUI.UpdateCursor(true);
							break;
						}
						case PlayerCustom.PlayerSecondaryRole.BothTinkerer:
							if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && (Object)(object)playerCustom.Accessory != (Object)null && !playerCustom.Accessory.TinkererPowerRequiresPlayerTarget)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, playerCustom.Index);
							}
							break;
						}
						if ((Object)(object)__instance.targetObject != (Object)null)
						{
							PlayerController componentInParent = __instance.targetObject.GetComponentInParent<PlayerController>();
							if ((Object)(object)componentInParent != (Object)null)
							{
								PlayerCustom player = PlayerCustomRegistry.GetPlayer(componentInParent.Ref);
								switch (playerCustom.SecondaryRole)
								{
								case PlayerCustom.PlayerSecondaryRole.BothMetabolic:
									if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && (int)__instance.Role == 1 && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && !NetworkBool.op_Implicit(componentInParent.IsDead))
									{
										PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, componentInParent.Index);
										PlayerCustom.PlaySuccessSound();
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothPolitician:
									if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && !NetworkBool.op_Implicit(player.PoliticianVictimAlltime) && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent.IsWolf) && !NetworkBool.op_Implicit(componentInParent.IsDead) && !NetworkBool.op_Implicit(componentInParent.PlayerEffectManager.Invisible) && !NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
									{
										PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, player.Index);
										PlayerCustom.PlaySuccessSound();
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothSherif:
									if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && !NetworkBool.op_Implicit(componentInParent.IsDead) && (int)__instance.Role != 1 && !NetworkBool.op_Implicit(componentInParent.IsWolf))
									{
										PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, player.Index);
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothGambler:
									if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && !NetworkBool.op_Implicit(__instance.IsWolf) && !NetworkBool.op_Implicit(componentInParent.IsDead))
									{
										PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, player.Index);
										PlayerCustom.PlaySuccessSound();
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothMedium:
									if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && __instance.IsCanMove() && NetworkBool.op_Implicit(componentInParent.IsDead))
									{
										PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, player.Index);
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothScavenger:
									if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && __instance.IsCanMove() && NetworkBool.op_Implicit(componentInParent.IsDead) && (NetworkBool.op_Implicit(__instance.IsWolf) || !NetworkBool.op_Implicit(player.Scavenged)))
									{
										PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, player.Index);
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothBlueMage:
									if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && !NetworkBool.op_Implicit(componentInParent.IsDead))
									{
										PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, componentInParent.Index);
										PlayerCustom.PlaySuccessSound();
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothForger:
									if (playerCustom.SecondaryRoleFirstRemainingUses <= 0 || !((Object)(object)componentInParent.Item != (Object)null) || !PlayerHeldItemComponent.CanSeeItem(playerCustom, componentInParent.Item) || NetworkBool.op_Implicit(componentInParent.IsWolf) || NetworkBool.op_Implicit(componentInParent.IsDead) || NetworkBool.op_Implicit(componentInParent.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.Paranoia))
									{
										break;
									}
									val2 = componentInParent.Item.TriggerTimer;
									if (!((TickTimer)(ref val2)).IsRunning)
									{
										val2 = componentInParent.Item.AnimationTimer;
										if (!((TickTimer)(ref val2)).IsRunning)
										{
											PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, componentInParent.Index);
											PlayerCustom.PlaySuccessSound();
										}
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothTinkerer:
									if (playerCustom.SecondaryRoleFirstRemainingUses > 0 && (Object)(object)playerCustom.Accessory != (Object)null && playerCustom.Accessory.TinkererPowerRequiresPlayerTarget && !NetworkBool.op_Implicit(componentInParent.IsDead))
									{
										PlayerCustom.PlaySuccessSound();
										PlayerCustom.Rpc_Activate_Secondary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, componentInParent.Index);
									}
									break;
								case PlayerCustom.PlayerSecondaryRole.BothIllusionist:
								case PlayerCustom.PlayerSecondaryRole.BothAstral:
								case PlayerCustom.PlayerSecondaryRole.BothActor:
								case PlayerCustom.PlayerSecondaryRole.BothScribe:
								case PlayerCustom.PlayerSecondaryRole.BothCarabineer:
								case PlayerCustom.PlayerSecondaryRole.BothImitator:
								case PlayerCustom.PlayerSecondaryRole.BothMerchant:
									break;
								}
							}
						}
					}
				}
				catch (Exception ex3)
				{
					Plugin.Logger.LogInfo((object)("SecondaryAction exception: " + ex3));
				}
				try
				{
					if (InputManagerExtra.Instance.MayorActionJustPressed && NetworkBool.op_Implicit(Plugin.CustomConfig.AllowMayor) && (int)GameManager.LocalGameState == 4 && NetworkBool.op_Implicit(GameManager.Instance.CanVote) && !NetworkBool.op_Implicit(PlayerController.Local.IsDead))
					{
						PlayerController val3 = null;
						if ((Object)(object)__instance.targetObject != (Object)null)
						{
							val3 = __instance.targetObject.GetComponentInParent<PlayerController>();
						}
						if (GameManagerCustom.Instance.CurrentMayor == PlayerController.Local.Ref)
						{
							val2 = GameManagerCustom.Instance.MayorActionCooldownTimer;
							if (((TickTimer)(ref val2)).IsRunning)
							{
								return;
							}
							switch (GameManagerCustom.Instance.MayorActionIndex)
							{
							case 0:
								if ((Object)(object)val3 != (Object)null)
								{
									GameManagerCustom.Rpc_Mayor_Action(((SimulationBehaviour)__instance).Runner, PlayerController.Local.Index, val3.Index, 0);
								}
								break;
							case 1:
								if ((Object)(object)val3 != (Object)null)
								{
									GameManagerCustom.Rpc_Mayor_Action(((SimulationBehaviour)__instance).Runner, PlayerController.Local.Index, val3.Index, 1);
								}
								break;
							case 2:
								GameManagerCustom.Rpc_Mayor_Action(((SimulationBehaviour)__instance).Runner, PlayerController.Local.Index, PlayerController.Local.Index, 2);
								break;
							case 3:
								if ((Object)(object)val3 != (Object)null)
								{
									GameManagerCustom.Rpc_Mayor_Action(((SimulationBehaviour)__instance).Runner, PlayerController.Local.Index, val3.Index, 3);
								}
								break;
							}
						}
						else
						{
							GameManagerCustom.Rpc_Vote_Mayor(((SimulationBehaviour)__instance).Runner, PlayerController.Local.Index, val3.Index);
						}
					}
				}
				catch (Exception ex4)
				{
					Plugin.Logger.LogInfo((object)("MayorAction exception: " + ex4));
				}
				if (InputManager.Instance.SecondaryActionJustPressed)
				{
					switch (playerCustom.PrimaryRolePower)
					{
					case PlayerCustom.PlayerPrimaryRolePower.Magician:
						if (LycansUtility.GameActuallyInPlay && MagicianBeacon.AssociatedBeacons.Any())
						{
							PlayerCustom.PlaySuccessSound();
							PlayerCustom.Rpc_Activate_Primary_Role_Power_With_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index, playerCustom.Index);
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Runemaster:
						if (LycansUtility.GameActuallyInPlay && LycansUtility.WolvesCanTransform && playerCustom.AssociatedRunes.Any((RunemasterRune o) => o.IsSelected) && playerCustom.PrimaryRolePowerRemainingUses > 0)
						{
							PlayerCustom.PlaySuccessSound();
							RunemasterRune.Rpc_Request_Activate_Rune(((SimulationBehaviour)__instance).Runner, playerCustom.Index, ((SimulationBehaviour)playerCustom.AssociatedRunes.First((RunemasterRune o) => o.IsSelected)).Object.Id);
						}
						break;
					case PlayerCustom.PlayerPrimaryRolePower.Inventor:
						if (LycansUtility.GameActuallyInPlay && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials && !NetworkBool.op_Implicit(__instance.IsWolf))
						{
							PlayerCustom.PlaySuccessSound();
							PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
						}
						break;
					}
					PlayerCustom.PlayerNewPrimaryRole newPrimaryRole = playerCustom.NewPrimaryRole;
					PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
					if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist && LycansUtility.GameActuallyInPlay && playerCustom.PrimaryRolePowerRemainingUses > 0 && !NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
					{
						if (BalancingValues.CultistSkullForbiddenAreasByMapId.ContainsKey(GameManager.Instance.MapID) && BalancingValues.CultistSkullForbiddenAreasByMapId[GameManager.Instance.MapID].Any((Vector3 o) => Vector3.Distance(o, ((Component)playerCustom.PlayerController).transform.position) <= 10f))
						{
							UIManager.ShowRedCenterMessage("NALES_UI_ACTION_CULTIST_CANNOT_PLACE_SKULL_BUSH", 0.4f, 4f);
							return;
						}
						if (PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsDead) && Vector3.Distance(((Component)o).transform.position, ((Component)playerCustom.PlayerController).transform.position) <= 3f)))
						{
							UIManager.ShowRedCenterMessage("NALES_UI_ACTION_CULTIST_CANNOT_PLACE_SKULL_CORPSE", 0.4f, 4f);
							return;
						}
						if (CultistSkull.AllSkulls.Any((CultistSkull o) => Vector3.Distance(((Component)o).transform.position, ((Component)playerCustom.PlayerController).transform.position) <= 10f))
						{
							UIManager.ShowRedCenterMessage("NALES_UI_ACTION_CULTIST_CANNOT_PLACE_SKULL_TOO_CLOSE", 0.4f, 4f);
							return;
						}
						PlayerCustom.PlaySuccessSound();
						PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
					}
					if ((int)GameManager.State.Current == 2 && (int)playerCustom.PlayerController.Role == 1 && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) && !NetworkBool.op_Implicit(GameManager.LightingManager.IsTransition) && NetworkBool.op_Implicit(playerCustom.PlayerController.CanMoveAnimation) && GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Eclipse)
					{
						if (NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf))
						{
							Traverse.Create((object)playerCustom.PlayerController).Method("Rpc_TransformBack", Array.Empty<object>()).GetValue();
						}
						else if (!NetworkBool.op_Implicit(playerCustom.PlayerController.TransformedNight))
						{
							Traverse.Create((object)playerCustom.PlayerController).Method("Rpc_TransformWolf", Array.Empty<object>()).GetValue();
						}
					}
				}
				if (InputManager.Instance.PrimaryInteractJustPressed && GameManager.Instance.MapID == 1)
				{
					Vector3 val4 = default(Vector3);
					((Vector3)(ref val4))._002Ector(214.03f, 30.27f, 180.11f);
					if (Vector3.Distance(((Component)playerCustom.PlayerController).transform.position, val4) <= 0.1f)
					{
						PlayerCustom.Rpc_Request_Unstuck(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index);
					}
				}
				if (InputManager.Instance.ItemJustPressed && (int)GameManager.LocalGameState == 4 && GameManagerCustom.Instance.CurrentMayor == PlayerController.Local.Ref)
				{
					GameManagerCustom.Instance.ChangeMayorAction();
				}
			}
			try
			{
				AdditionalInputFunctions(__instance, playerCustom);
			}
			catch (Exception ex5)
			{
				Plugin.Logger.LogInfo((object)("AdditionalInputFunctions exception: " + ex5));
			}
		}
		catch (Exception ex6)
		{
			Plugin.Logger.LogError((object)("LocalInputPatches error: " + ex6?.ToString() + ", InputManager.Instance.PrimaryActionJustPressed: " + InputManager.Instance.PrimaryActionJustPressed + ", InputManager.Instance.PrimaryInteractJustPressed: " + InputManager.Instance.PrimaryInteractJustPressed + ", InputManager.Instance.ItemJustPressed: " + InputManager.Instance.ItemJustPressed + ", InputManager.Instance.SecondaryActionJustPressed: " + InputManager.Instance.SecondaryActionJustPressed + ", InputManagerExtra.Instance.SecondaryRoleActionJustPressed: " + InputManagerExtra.Instance.SecondaryRoleActionJustPressed + ", power: " + playerCustom.PrimaryRolePower.ToString() + ", secondary role: " + playerCustom.SecondaryRole));
		}
	}
}
