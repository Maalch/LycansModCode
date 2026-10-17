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
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Invalid comparison between Unknown and I4
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Invalid comparison between Unknown and I4
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Invalid comparison between Unknown and I4
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Invalid comparison between Unknown and I4
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Invalid comparison between Unknown and I4
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Invalid comparison between Unknown and I4
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Invalid comparison between Unknown and I4
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Expected O, but got Unknown
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		if (InputManagerExtra.Instance.AccessoryActionJustPressed && (int)GameManager.LocalGameState == 1)
		{
			PlayerCustom.Rpc_Switch_Spectator(((SimulationBehaviour)playerCustom).Runner, playerCustom.Index);
			return;
		}
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
		if ((NetworkBool.op_Implicit(PlayerController.Local.IsDead) || NetworkBool.op_Implicit(PlayerCustom.Local.IsSpectator)) && (Object)(object)PlayerCustom.Local.SummonedSpirit == (Object)null && InputManagerExtra.Instance.MayorActionJustPressed)
		{
			if (UIManager.SpectatorChoicePanel.Active)
			{
				UIManager.SpectatorChoicePanel.Hide();
				GameManager.Instance.gameUI.UpdateCursor(false);
			}
			else
			{
				List<PlayerRef> players = PlayerCustomRegistry.AllPlayers.Select((PlayerCustom o) => o.Ref).ToList();
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
			if ((int)GameManager.LocalGameState == 4 && !playerCustom.AskForSpeechTimer.IsRunning && !playerCustom.AskForSpeechUsedThisMeeting && !PlayerCustomRegistry.Any((PlayerCustom o) => o.AskForSpeechTimer.IsRunning))
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
		if (InputManagerExtra.Instance.MayorActionJustPressed && playerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Host && !NetworkBool.op_Implicit(playerController.IsDead) && LycansUtility.GameActuallyInPlay && PlayerCustomRegistry.Any((PlayerCustom o) => NetworkBool.op_Implicit(o.Parasite)))
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
		//IL_1390: Unknown result type (might be due to invalid IL or missing references)
		//IL_1396: Invalid comparison between Unknown and I4
		//IL_2170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_139d: Unknown result type (might be due to invalid IL or missing references)
		//IL_217c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2182: Invalid comparison between Unknown and I4
		//IL_2828: Unknown result type (might be due to invalid IL or missing references)
		//IL_282e: Invalid comparison between Unknown and I4
		//IL_27db: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected I4, but got Unknown
		//IL_10e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2189: Unknown result type (might be due to invalid IL or missing references)
		//IL_2835: Unknown result type (might be due to invalid IL or missing references)
		//IL_283f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2692: Unknown result type (might be due to invalid IL or missing references)
		//IL_2698: Invalid comparison between Unknown and I4
		//IL_24f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb4: Invalid comparison between Unknown and I4
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_219a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2583: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ab: Invalid comparison between Unknown and I4
		//IL_2511: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_129a: Unknown result type (might be due to invalid IL or missing references)
		//IL_129f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16af: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1671: Unknown result type (might be due to invalid IL or missing references)
		//IL_177c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1781: Unknown result type (might be due to invalid IL or missing references)
		//IL_156e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1574: Invalid comparison between Unknown and I4
		//IL_15bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1816: Unknown result type (might be due to invalid IL or missing references)
		//IL_186e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_220a: Unknown result type (might be due to invalid IL or missing references)
		//IL_220f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2710: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_171f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1797: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c24: Unknown result type (might be due to invalid IL or missing references)
		//IL_2754: Unknown result type (might be due to invalid IL or missing references)
		//IL_26da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_146c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1471: Unknown result type (might be due to invalid IL or missing references)
		//IL_1511: Unknown result type (might be due to invalid IL or missing references)
		//IL_1516: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11eb: Expected O, but got Unknown
		//IL_1487: Unknown result type (might be due to invalid IL or missing references)
		//IL_148c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d14: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca9: Invalid comparison between Unknown and I4
		//IL_1dcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c18: Expected O, but got Unknown
		//IL_1d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ddd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de3: Invalid comparison between Unknown and I4
		//IL_1e49: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Expected O, but got Unknown
		//IL_1d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ffb: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_199e: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a8: Expected O, but got Unknown
		//IL_1d42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_2009: Unknown result type (might be due to invalid IL or missing references)
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1099: Expected O, but got Unknown
		//IL_1d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_201c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b32: Expected O, but got Unknown
		//IL_0dab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db5: Expected O, but got Unknown
		//IL_1d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_203c: Unknown result type (might be due to invalid IL or missing references)
		//IL_204f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2054: Unknown result type (might be due to invalid IL or missing references)
		//IL_2066: Unknown result type (might be due to invalid IL or missing references)
		//IL_206b: Unknown result type (might be due to invalid IL or missing references)
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
							if (LycansUtility.GameActuallyInPlay && NetworkBool.op_Implicit(__instance.CanMove) && NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool) && NetworkBool.op_Implicit(PlayerRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef).IsDead) && playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials && LycansUtility.WolvesCanTransform && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
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
								foreach (GameObject effectButton2 in buttonsForRitualist)
								{
									effectButton2.GetComponent<UIGenericChoiceButtonRitualistEffect>().SetAction((UnityAction)delegate
									{
										UIManager.GenericChoicePanel.OnClickRitualistEffect(effectButton2.GetComponent<UIGenericChoiceButtonRitualistEffect>().Effect);
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
						case PlayerCustom.PlayerPrimaryRolePower.Fartmaster:
							if (NetworkBool.op_Implicit(__instance.IsWolf) && playerCustom.PrimaryRolePowerRemainingUses > 0)
							{
								PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
								PlayerCustom.PlaySuccessSound();
							}
							break;
						case PlayerCustom.PlayerPrimaryRolePower.Impostor:
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
							List<GameObject> buttonsForImpostor = UIGenericChoicePanel.GetButtonsForImpostor();
							foreach (GameObject effectButton in buttonsForImpostor)
							{
								effectButton.GetComponent<UIGenericChoiceButtonImpostorRole>().SetAction((UnityAction)delegate
								{
									UIManager.GenericChoicePanel.OnClickImpostorRole(effectButton.GetComponent<UIGenericChoiceButtonImpostorRole>().Role);
								});
							}
							UIManager.GenericChoicePanel.Show(buttonsForImpostor, "NALES_UI_CHOICE_PANEL_HEADER_IMPOSTOR");
							GameManager.Instance.gameUI.UpdateCursor(true);
							break;
						}
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
							if (!NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
							{
								PlayerCustom.PlaySuccessSound();
							}
							PlayerCustom.Rpc_Activate_Primary_Role_Power_Without_Target(((SimulationBehaviour)__instance).Runner, playerCustom.Index);
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
							foreach (GameObject effectButton3 in buttonsForImitator)
							{
								effectButton3.GetComponent<UIGenericChoiceButtonImitatorRole>().SetAction((UnityAction)delegate
								{
									UIManager.GenericChoicePanel.OnClickImitatorRole(effectButton3.GetComponent<UIGenericChoiceButtonImitatorRole>().Role);
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
