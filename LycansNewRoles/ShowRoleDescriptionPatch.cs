using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using Managers;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "Update")]
internal class ShowRoleDescriptionPatch
{
	public static bool ShowingExplanation;

	public static bool NeedsUpdate;

	public static int? PrimaryRolePowerCooldownSecondsToShow;

	public static int? SecondaryRolePowerCooldownSecondsToShow;

	public static int? LoverPartnerCurrentHealthPercentageToShow;

	private static Shortcut traverseActionShortcut;

	public static void UpdatePrimaryRolePowerCooldownSeconds(PlayerCustom playerCustom)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		TickTimer primaryRolePowerCooldownTimer = playerCustom.PrimaryRolePowerCooldownTimer;
		int num = Mathf.CeilToInt(((TickTimer)(ref primaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value);
		if (!PrimaryRolePowerCooldownSecondsToShow.HasValue || PrimaryRolePowerCooldownSecondsToShow.Value != num)
		{
			playerCustom.UpdateDescriptionStatusIfNeeded();
			PrimaryRolePowerCooldownSecondsToShow = num;
		}
	}

	public static void UpdateSecondaryRolePowerCooldownSeconds(PlayerCustom playerCustom)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		TickTimer secondaryRolePowerCooldownTimer = playerCustom.SecondaryRolePowerCooldownTimer;
		int num = Mathf.CeilToInt(((TickTimer)(ref secondaryRolePowerCooldownTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value);
		if (!SecondaryRolePowerCooldownSecondsToShow.HasValue || SecondaryRolePowerCooldownSecondsToShow.Value != num)
		{
			playerCustom.UpdateDescriptionStatusIfNeeded();
			SecondaryRolePowerCooldownSecondsToShow = num;
		}
	}

	private static void Postfix(GameUI __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)GameManager.LocalGameState == 2 || (int)GameManager.LocalGameState == 4)
			{
				if (LocalInputPatches.CrouchHoldTimer.ElapsedMilliseconds >= 500)
				{
					if (!ShowingExplanation)
					{
						PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
						UIManager.RoleDescription.Show(player);
					}
				}
				else if (ShowingExplanation && !NetworkBool.op_Implicit(DraftManager.Instance.Active))
				{
					UIManager.RoleDescription.Hide();
				}
				if (!NeedsUpdate)
				{
					return;
				}
				PlayerController player2 = PlayerRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				PlayerCustom playerCustom = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				playerCustom.UpdatePrimaryRole();
				if ((Object)(object)traverseActionShortcut == (Object)null)
				{
					traverseActionShortcut = Traverse.Create((object)__instance).Field<Shortcut>("actionShortcut").Value;
				}
				((Component)traverseActionShortcut).gameObject.SetActive(true);
				if (NetworkBool.op_Implicit(PlayerCustom.Local.Possessed))
				{
					((TMP_Text)((Component)traverseActionShortcut).GetComponent<TextMeshProUGUI>()).text = "???";
					NeedsUpdate = false;
					return;
				}
				int? powerCharges = GetPowerCharges(player2, playerCustom);
				TickTimer val;
				string text;
				switch (playerCustom.SecondaryRole)
				{
				case PlayerCustom.PlayerSecondaryRole.BothTeleporter:
					if (powerCharges.Value > 0)
					{
						NetworkTeleportData secondaryRoleTeleportData = playerCustom.SecondaryRoleTeleportData;
						text = ((!((NetworkTeleportData)(ref secondaryRoleTeleportData)).IsNone) ? TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_TELEPORTER_BEACON_AVAILABLE") : TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_TELEPORTER_NO_BEACON"));
						break;
					}
					val = playerCustom.SecondaryRolePowerCooldownTimer;
					if (((TickTimer)(ref val)).IsRunning)
					{
						string translation2 = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_DEFAULT_AVAILABLE_IN");
						val = playerCustom.SecondaryRolePowerCooldownTimer;
						text = translation2.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
					}
					else
					{
						text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_DEFAULT_UNAVAILABLE");
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothBlueMage:
					if (powerCharges.Value > 0)
					{
						text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_BLUE_MAGE_AVAILABLE").Replace("{0}", TranslationManager.Instance.GetTranslation(EffectManager.GetEffect(playerCustom.SecondaryRoleUniqueInt).GetTranslateKey()));
						break;
					}
					val = playerCustom.SecondaryRolePowerCooldownTimer;
					if (((TickTimer)(ref val)).IsRunning)
					{
						string translation4 = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_BLUE_MAGE_AVAILABLE_IN");
						val = playerCustom.SecondaryRolePowerCooldownTimer;
						text = translation4.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString()).Replace("{1}", TranslationManager.Instance.GetTranslation(EffectManager.GetEffect(playerCustom.SecondaryRoleUniqueInt).GetTranslateKey()));
					}
					else
					{
						text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_BLUE_MAGE_UNAVAILABLE").Replace("{0}", TranslationManager.Instance.GetTranslation(EffectManager.GetEffect(playerCustom.SecondaryRoleUniqueInt).GetTranslateKey()));
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothCarabineer:
					if (NetworkBool.op_Implicit(player2.IsGunLoaded))
					{
						text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_CARABINEER_LOADED");
						break;
					}
					val = playerCustom.SecondaryRolePowerCooldownTimer;
					if (((TickTimer)(ref val)).IsRunning)
					{
						string translation3 = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_CARABINEER_LOADED_IN");
						val = playerCustom.SecondaryRolePowerCooldownTimer;
						text = translation3.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
					}
					else
					{
						text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_CARABINEER_UNLOADED");
					}
					break;
				case PlayerCustom.PlayerSecondaryRole.BothMerchant:
					val = playerCustom.SecondaryRoleActionTimer;
					text = ((!((TickTimer)(ref val)).IsRunning) ? TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_MERCHANT_NO_DELIVERY").Replace("{0}", playerCustom.SecondaryRoleUniqueInt.ToString()) : TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_MERCHANT_DELIVERY"));
					break;
				case PlayerCustom.PlayerSecondaryRole.BothTelepath:
				{
					PlayerCustom playerCustom2 = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != playerCustom.Ref && o.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothTelepath).FirstOrDefault();
					if ((Object)(object)playerCustom2 != (Object)null)
					{
						Color color = ColorManager.GetColor(playerCustom2.ColorIndex);
						text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_TELEPATH_DETAILS").Replace("{0}", "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + ((object)playerCustom2.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</color>");
					}
					else
					{
						text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_DEFAULT");
					}
					break;
				}
				default:
					if (powerCharges.HasValue)
					{
						if (powerCharges.Value > 0)
						{
							text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_DEFAULT_AVAILABLE");
							break;
						}
						val = playerCustom.SecondaryRolePowerCooldownTimer;
						if (((TickTimer)(ref val)).IsRunning)
						{
							string translation = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_DEFAULT_AVAILABLE_IN");
							val = playerCustom.SecondaryRolePowerCooldownTimer;
							text = translation.Replace("{0}", Mathf.CeilToInt(((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value).ToString());
						}
						else
						{
							text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_DEFAULT_UNAVAILABLE");
						}
					}
					else
					{
						text = TranslationManager.Instance.GetTranslation("NALES_ROLE_DESCRIPTION_DEFAULT");
					}
					break;
				}
				text = LycansUtility.ReplaceWithActionNameText(text, "#CROUCH", (InputActionName)8);
				text = LycansUtility.ReplaceWithActionNameText(text, "#PRIMARYACTION", (InputActionName)5);
				text = LycansUtility.ReplaceWithActionNameText(text, "#SECONDARYROLEPOWER", InputManagerExtra.Instance.Actions["SECONDARYROLEPOWER"]);
				text = LycansUtility.ReplaceWithActionNameText(text, "#ITEMSECONDARY", InputManagerExtra.Instance.Actions["ITEMSECONDARY"]);
				((TMP_Text)((Component)traverseActionShortcut).GetComponent<TextMeshProUGUI>()).text = text;
				NeedsUpdate = false;
			}
			else if (ShowingExplanation)
			{
				UIManager.RoleDescription.Hide();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ShowRoleDescriptionPatch error: " + ex));
		}
	}

	private static int? GetPowerCharges(PlayerController player, PlayerCustom playerCustom)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Invalid comparison between Unknown and I4
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		switch (playerCustom.SecondaryRole)
		{
		case PlayerCustom.PlayerSecondaryRole.BothAlcoholic:
		case PlayerCustom.PlayerSecondaryRole.BothInfected:
		case PlayerCustom.PlayerSecondaryRole.BothTeleporter:
		case PlayerCustom.PlayerSecondaryRole.BothEngineer:
		case PlayerCustom.PlayerSecondaryRole.BothPolitician:
		case PlayerCustom.PlayerSecondaryRole.BothIllusionist:
		case PlayerCustom.PlayerSecondaryRole.BothSherif:
		case PlayerCustom.PlayerSecondaryRole.BothGambler:
		case PlayerCustom.PlayerSecondaryRole.BothMedium:
		case PlayerCustom.PlayerSecondaryRole.BothAstral:
		case PlayerCustom.PlayerSecondaryRole.BothScavenger:
		case PlayerCustom.PlayerSecondaryRole.BothBlueMage:
		case PlayerCustom.PlayerSecondaryRole.BothActor:
		case PlayerCustom.PlayerSecondaryRole.BothScribe:
		case PlayerCustom.PlayerSecondaryRole.BothForger:
		case PlayerCustom.PlayerSecondaryRole.BothTinkerer:
			return playerCustom.SecondaryRoleFirstRemainingUses;
		case PlayerCustom.PlayerSecondaryRole.BothMetabolic:
			if ((int)player.Role == 1 && !NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
			{
				return playerCustom.SecondaryRoleFirstRemainingUses;
			}
			return null;
		case PlayerCustom.PlayerSecondaryRole.BothSprinter:
			if (NetworkBool.op_Implicit(player.IsWolf))
			{
				return playerCustom.SecondaryRoleFirstRemainingUses;
			}
			return 1;
		default:
			return null;
		}
	}
}
