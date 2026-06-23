using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "CheckPlayerRayCast")]
internal class LongerPlayerReachPatch
{
	private static bool Prefix(PlayerController targetPlayer, float distance, ref bool __result, PlayerController __instance)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Invalid comparison between Unknown and I4
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Invalid comparison between Unknown and I4
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Invalid comparison between Unknown and I4
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			__result = false;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(targetPlayer.Ref);
			if ((Object)(object)player != (Object)null && (NetworkBool.op_Implicit(player.Disappeared) || NetworkBool.op_Implicit(player.Phasing)))
			{
				return false;
			}
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			if (NetworkBool.op_Implicit(player2.Tiny) || NetworkBool.op_Implicit(player2.Phasing) || (NetworkBool.op_Implicit(player.Isolation) && !NetworkBool.op_Implicit(__instance.IsWolf)) || (NetworkBool.op_Implicit(player2.Isolation) && !NetworkBool.op_Implicit(targetPlayer.IsWolf)))
			{
				return false;
			}
			if (player2.Ref == player.Ref)
			{
				return false;
			}
			if (PlayerCustom.Local.IsOutOfTheWorld)
			{
				return false;
			}
			if (distance < 10f)
			{
				PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
				GameUI gameUI = GameManager.Instance.gameUI;
				Color color = Color.white;
				float? num = null;
				if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Peasant && NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool) && NetworkBool.op_Implicit(__instance.IsWolf))
				{
					num = 2.25f;
				}
				else if (NetworkBool.op_Implicit(player.Hidden) && NetworkBool.op_Implicit(__instance.IsWolf))
				{
					num = 2f;
				}
				else if (NetworkBool.op_Implicit(targetPlayer.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(player.Sneaky) || player.CamouflageLevelForPovPlayer > 0)
				{
					num = 3.5f;
				}
				string text;
				if (num.HasValue)
				{
					text = ((!(distance < num.Value)) ? "" : "???");
				}
				else if (NetworkBool.op_Implicit(targetPlayer.IsWolf) || NetworkBool.op_Implicit(povPlayer.PlayerEffectManager.Paranoia))
				{
					text = "???";
				}
				else
				{
					PlayerRef illusionTarget = player.IllusionTarget;
					if (!((PlayerRef)(ref illusionTarget)).IsNone && !NetworkBool.op_Implicit(__instance.PlayerEffectManager.NightVision))
					{
						PlayerController player3 = PlayerRegistry.GetPlayer(player.IllusionTarget);
						text = ((object)player3.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
					}
					else
					{
						text = ((object)targetPlayer.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
						if ((int)GameManager.LocalGameState == 2 && player2.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent)
						{
							if ((int)targetPlayer.Role == 1)
							{
								text += TranslationManager.Instance.GetTranslation("NALES_ROLE_TOOLTIP_WOLF");
								color = Color.red;
							}
							else if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Agent)
							{
								text += TranslationManager.Instance.GetTranslation("NALES_ROLE_TOOLTIP_AGENT");
								color = Color.magenta;
							}
							else
							{
								text += TranslationManager.Instance.GetTranslation("NALES_ROLE_TOOLTIP_VILLAGER");
							}
						}
					}
				}
				gameUI.UpdateUsername(text);
				GameManager.Instance.gameUI.ShowUsername(true);
				((Graphic)Traverse.Create((object)gameUI).Field<TextMeshProUGUI>("usernameText").Value).color = color;
			}
			if ((int)GameManager.LocalGameState == 2)
			{
				if ((Object)(object)PlayerCustom.Local.AstralSpirit != (Object)null || (Object.op_Implicit((Object)(object)PlayerCustom.Local.SummonedSpirit) && NetworkBool.op_Implicit(PlayerCustom.Local.SummonedSpirit.HasFocus)))
				{
					return false;
				}
				SingleInteraction interaction = InteractionsManager.GetInteraction(SingleInteraction.SingleInteractionType.NormalInteraction, __instance, player2, targetPlayer, player, distance);
				SingleInteraction interaction2 = InteractionsManager.GetInteraction(SingleInteraction.SingleInteractionType.SecondaryRoleInteraction, __instance, player2, targetPlayer, player, distance);
				SingleInteraction interaction3 = InteractionsManager.GetInteraction(SingleInteraction.SingleInteractionType.ItemInteraction, __instance, player2, targetPlayer, player, distance);
				SingleInteraction interaction4 = InteractionsManager.GetInteraction(SingleInteraction.SingleInteractionType.SecondaryItemInteraction, __instance, player2, targetPlayer, player, distance);
				SingleInteraction interaction5 = InteractionsManager.GetInteraction(SingleInteraction.SingleInteractionType.AccessoryInteraction, __instance, player2, targetPlayer, player, distance);
				List<SingleInteraction> list = new List<SingleInteraction>();
				if (interaction != null)
				{
					list.Add(interaction);
				}
				if (interaction2 != null)
				{
					list.Add(interaction2);
				}
				if (interaction3 != null)
				{
					list.Add(interaction3);
				}
				if (interaction4 != null)
				{
					list.Add(interaction4);
				}
				if (interaction5 != null)
				{
					list.Add(interaction5);
				}
				if (list.Any())
				{
					InteractionsManager.UpdateWithInteractions(list);
					__result = true;
					return false;
				}
				GameManager.Instance.gameUI.HideInteraction();
				__result = false;
				return false;
			}
			if (!NetworkBool.op_Implicit(__instance.IsWolf) && NetworkBool.op_Implicit(GameManager.Instance.CanVote) && !NetworkBool.op_Implicit(targetPlayer.IsDead))
			{
				NetworkPlayerData playerData = targetPlayer.PlayerData;
				string item = (NetworkBool.op_Implicit(__instance.PlayerEffectManager.Paranoia) ? "???" : ((object)playerData.Username/*cast due to constrained. prefix*/).ToString());
				bool flag = __instance.IdVoted == -1;
				int num2;
				if (NetworkBool.op_Implicit(Plugin.CustomConfig.AllowMayor) && !NetworkBool.op_Implicit(__instance.PlayerEffectManager.Paranoia))
				{
					if (!(GameManagerCustom.Instance.CurrentMayor != PlayerController.Local.Ref))
					{
						TickTimer mayorActionCooldownTimer = GameManagerCustom.Instance.MayorActionCooldownTimer;
						num2 = ((!((TickTimer)(ref mayorActionCooldownTimer)).IsRunning) ? 1 : 0);
					}
					else
					{
						num2 = 1;
					}
				}
				else
				{
					num2 = 0;
				}
				bool flag2 = (byte)num2 != 0;
				List<object> list2 = new List<object>();
				if (flag && !flag2)
				{
					string text2 = "NALES_MAYOR_VOTE_FOR_ELIMINATION";
					list2.Add(item);
					GameManager.Instance.gameUI.UpdateInteraction(text2, Color.white, (InputActionName)4, list2.ToArray());
					__result = true;
				}
				else if (!flag && flag2)
				{
					string text2;
					if (GameManagerCustom.Instance.CurrentMayor == PlayerController.Local.Ref)
					{
						switch (GameManagerCustom.Instance.MayorActionIndex)
						{
						case 0:
							text2 = "NALES_MAYOR_ACTION_STUN_WITHOUT_ELIMINATION";
							list2.Add(item);
							break;
						case 1:
							text2 = "NALES_MAYOR_ACTION_LISTEN_WITHOUT_ELIMINATION";
							list2.Add(item);
							break;
						case 2:
							text2 = "NALES_MAYOR_ACTION_SPEECH_WITHOUT_ELIMINATION";
							break;
						case 3:
							text2 = "NALES_MAYOR_ACTION_NEW_MAYOR_WITHOUT_ELIMINATION";
							list2.Add(item);
							break;
						default:
							text2 = "";
							break;
						}
					}
					else if (targetPlayer.Ref == GameManagerCustom.Instance.CurrentMayor)
					{
						text2 = "NALES_MAYOR_VOTE_FOR_DESTITUTION_WITHOUT_ELIMINATION";
						list2.Add(item);
					}
					else
					{
						text2 = "NALES_MAYOR_VOTE_FOR_NEW_MAYOR_WITHOUT_ELIMINATION";
						list2.Add(item);
					}
					InteractionsManager.UpdateInteraction(text2, Color.white, new List<string> { "MAYORACTION" }, list2.ToArray());
					__result = true;
				}
				else if (flag && flag2)
				{
					string text2;
					if (GameManagerCustom.Instance.CurrentMayor == PlayerController.Local.Ref)
					{
						switch (GameManagerCustom.Instance.MayorActionIndex)
						{
						case 0:
							text2 = "NALES_MAYOR_ACTION_STUN_WITH_ELIMINATION";
							list2.Add(item);
							list2.Add(item);
							break;
						case 1:
							text2 = "NALES_MAYOR_ACTION_LISTEN_WITH_ELIMINATION";
							list2.Add(item);
							list2.Add(item);
							break;
						case 2:
							text2 = "NALES_MAYOR_ACTION_SPEECH_WITH_ELIMINATION";
							list2.Add(item);
							break;
						case 3:
							text2 = "NALES_MAYOR_ACTION_NEW_MAYOR_WITH_ELIMINATION";
							list2.Add(item);
							list2.Add(item);
							break;
						default:
							text2 = "";
							break;
						}
					}
					else if (targetPlayer.Ref == GameManagerCustom.Instance.CurrentMayor)
					{
						text2 = "NALES_MAYOR_VOTE_FOR_DESTITUTION_WITH_ELIMINATION";
						list2.Add(item);
						list2.Add(item);
					}
					else
					{
						text2 = "NALES_MAYOR_VOTE_FOR_NEW_MAYOR_WITH_ELIMINATION";
						list2.Add(item);
						list2.Add(item);
					}
					InteractionsManager.UpdateInteraction(text2, Color.white, new List<string>
					{
						((object)(InputActionName)4/*cast due to constrained. prefix*/).ToString(),
						"MAYORACTION"
					}, list2.ToArray());
					__result = true;
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("LongerPlayerReachPatch error: " + ex));
			return true;
		}
	}
}
