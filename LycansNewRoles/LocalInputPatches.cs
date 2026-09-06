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
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Invalid comparison between Unknown and I4
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Invalid comparison between Unknown and I4
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Invalid comparison between Unknown and I4
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Invalid comparison between Unknown and I4
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Invalid comparison between Unknown and I4
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Invalid comparison between Unknown and I4
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Invalid comparison between Unknown and I4
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Expected O, but got Unknown
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
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
		if ((InputManager.Instance.PrimaryInteractJustPressed || InputManager.Instance.SecondaryInteractJustPressed) && (Object)(object)playerCustom.SummonedSpirit != (Object)null && (Object)(object)playerCustom.SummonedSpirit.TooltipTarget != (Object)null && LycansUtility.GameActuallyInPlay && (playerCustom.PrimaryRolePower != PlayerCustom.PlayerPrimaryRolePower.Specter || !playerCustom.SummonedSpirit.TooltipTarget.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is SpiritResistanceEffect)))
		{
			val = playerCustom.SummonedSpirit.AttackCooldown;
			if (!((TickTimer)(ref val)).IsRunning)
			{
				PlayerCustom.Rpc_Spirit_Attack(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index, playerCustom.SummonedSpirit.TooltipTarget.Index);
			}
		}
		if (InputManager.Instance.PrimaryActionJustPressed && (Object)(object)playerCustom.SummonedSpirit != (Object)null)
		{
			val = playerCustom.SummonedSpirit.SpellCooldown;
			if (!((TickTimer)(ref val)).IsRunning && LycansUtility.GameActuallyInPlay)
			{
				PlayerCustom.Rpc_Spirit_Spell(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index);
			}
		}
		if (LycansUtility.GameActuallyInPlay)
		{
			if (NetworkBool.op_Implicit(Plugin.CustomConfig.DropItemsAvailable) && (Object)(object)playerController.Item != (Object)null && !(playerController.Item is BulletItem) && !NetworkBool.op_Implicit(playerCustom.Tiny) && !NetworkBool.op_Implicit(playerCustom.PlayerController.PlayerEffectManager.Giant) && !NetworkBool.op_Implicit(playerCustom.PlayerController.IsClimbing))
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
		if (NetworkBool.op_Implicit(PlayerController.Local.IsDead) && (Object)(object)PlayerCustom.Local.SummonedSpirit == (Object)null && InputManagerExtra.Instance.MayorActionJustPressed)
		{
			if (UIManager.SpectatorChoicePanel.Active)
			{
				UIManager.SpectatorChoicePanel.Hide();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
			else
			{
				List<PlayerRef> players = (from o in PlayerCustomRegistry
					where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld
					select o.Ref).ToList();
				UIManager.SpectatorChoicePanel.Show(players);
				GameManager.Instance.gameUI.UpdateCursor(true);
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
			if ((int)GameManager.LocalGameState == 4 && !playerCustom.AskForSpeechActive && !playerCustom.AskForSpeechUsedThisMeeting && !NetworkBool.op_Implicit(playerCustom.PlayerController.IsTalking))
			{
				GameManagerCustom.Rpc_Ask_For_Speech(((SimulationBehaviour)playerController).Runner, playerCustom.Index);
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
		//IL_1238: Unknown result type (might be due to invalid IL or missing references)
		//IL_123e: Invalid comparison between Unknown and I4
		//IL_2018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1245: Unknown result type (might be due to invalid IL or missing references)
		//IL_2024: Unknown result type (might be due to invalid IL or missing references)
		//IL_202a: Invalid comparison between Unknown and I4
		//IL_26d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_26d6: Invalid comparison between Unknown and I4
		//IL_2683: Unknown result type (might be due to invalid IL or missing references)
		//IL_2688: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected I4, but got Unknown
		//IL_2031: Unknown result type (might be due to invalid IL or missing references)
		//IL_26dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_26e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_253a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2540: Invalid comparison between Unknown and I4
		//IL_239c: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa1: Invalid comparison between Unknown and I4
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_2042: Unknown result type (might be due to invalid IL or missing references)
		//IL_242b: Unknown result type (might be due to invalid IL or missing references)
		//IL_254d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2553: Invalid comparison between Unknown and I4
		//IL_23b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0887: Unknown result type (might be due to invalid IL or missing references)
		//IL_1135: Unknown result type (might be due to invalid IL or missing references)
		//IL_113a: Unknown result type (might be due to invalid IL or missing references)
		//IL_119d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f07: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1557: Unknown result type (might be due to invalid IL or missing references)
		//IL_189c: Unknown result type (might be due to invalid IL or missing references)
		//IL_208f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2099: Unknown result type (might be due to invalid IL or missing references)
		//IL_255a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2361: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_1519: Unknown result type (might be due to invalid IL or missing references)
		//IL_1624: Unknown result type (might be due to invalid IL or missing references)
		//IL_1629: Unknown result type (might be due to invalid IL or missing references)
		//IL_1416: Unknown result type (might be due to invalid IL or missing references)
		//IL_141c: Invalid comparison between Unknown and I4
		//IL_1464: Unknown result type (might be due to invalid IL or missing references)
		//IL_16be: Unknown result type (might be due to invalid IL or missing references)
		//IL_1716: Unknown result type (might be due to invalid IL or missing references)
		//IL_1906: Unknown result type (might be due to invalid IL or missing references)
		//IL_190b: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_256b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_163f: Unknown result type (might be due to invalid IL or missing references)
		//IL_139e: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acc: Unknown result type (might be due to invalid IL or missing references)
		//IL_25fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1314: Unknown result type (might be due to invalid IL or missing references)
		//IL_1319: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13be: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_132f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1334: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b51: Invalid comparison between Unknown and I4
		//IL_1c73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c05: Expected O, but got Unknown
		//IL_1bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c85: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8b: Invalid comparison between Unknown and I4
		//IL_1cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Expected O, but got Unknown
		//IL_1bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1846: Unknown result type (might be due to invalid IL or missing references)
		//IL_1850: Expected O, but got Unknown
		//IL_1bea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_107c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1086: Expected O, but got Unknown
		//IL_1bfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec4: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19da: Expected O, but got Unknown
		//IL_0d98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da2: Expected O, but got Unknown
		//IL_1c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f13: Unknown result type (might be due to invalid IL or missing references)
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
							foreach (GameObject playerButton in buttonsForPlayers2)
							{
								playerButton.GetComponent<UIGenericChoiceButtonPlayer>().SetAction((UnityAction)delegate
								{
									//IL_0010: Unknown result type (might be due to invalid IL or missing references)
									UIManager.GenericChoicePanel.OnClickShapeshiftingTarget(playerButton.GetComponent<UIGenericChoiceButtonPlayer>().PlayerRef);
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
								List<GameObject> buttonsForPlayers3 = UIGenericChoicePanel.GetButtonsForPlayers(players);
								foreach (GameObject playerButton2 in buttonsForPlayers3)
								{
									playerButton2.GetComponent<UIGenericChoiceButtonPlayer>().SetAction((UnityAction)delegate
									{
										//IL_0010: Unknown result type (might be due to invalid IL or missing references)
										UIManager.GenericChoicePanel.OnClickPredatorTarget(playerButton2.GetComponent<UIGenericChoiceButtonPlayer>().PlayerRef);
									});
								}
								UIManager.GenericChoicePanel.Show(buttonsForPlayers3, "NALES_UI_CHOICE_PANEL_HEADER_CHOOSE_PREY");
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
						case PlayerCustom.PlayerPrimaryRolePower.Tracker:
							if (LycansUtility.GameActuallyInPlay)
							{
								PlayerCustom.PlaySuccessSound();
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
							}
							else
							{
								if ((int)GameManager.LocalGameState != 4 || playerCustom.PrimaryRolePowerRemainingUses <= 0)
								{
									break;
								}
								if (UIManager.GenericChoicePanel.Active)
								{
									UIManager.GenericChoicePanel.Hide();
									GameManager.Instance.gameUI.UpdateCursor(false);
									break;
								}
								List<GameObject> buttonsForPlayers = UIGenericChoicePanel.GetButtonsForPlayers((from o in PlayerCustomRegistry
									where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Kidnapped) && o.Ref != PlayerController.Local.Ref
									select o.Ref).ToList());
								foreach (GameObject trackerGuessPlayerButton in buttonsForPlayers)
								{
									trackerGuessPlayerButton.GetComponent<UIGenericChoiceButtonPlayer>().SetAction((UnityAction)delegate
									{
										//IL_0010: Unknown result type (might be due to invalid IL or missing references)
										UIManager.GenericChoicePanel.OnClickCrystalBallGuessTarget(trackerGuessPlayerButton.GetComponent<UIGenericChoiceButtonPlayer>().PlayerRef);
									});
								}
								UIManager.GenericChoicePanel.Show(buttonsForPlayers, "NALES_UI_CHOICE_PANEL_HEADER_SEER_PLAYER");
								GameManager.Instance.gameUI.UpdateCursor(true);
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
						else if ((Object)(object)val3 != (Object)null)
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
						if (LycansUtility.GameActuallyInPlay && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials && playerCustom.InventorDeviceRef != playerCustom.Ref && !NetworkBool.op_Implicit(__instance.IsWolf))
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
