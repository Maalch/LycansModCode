using System;
using Fusion;
using HarmonyLib;
using LycansNewRoles.Sabotages;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LightingManager), "UpdateLighting")]
public class FogChangesPatch
{
	public static float FogDensityDaytime = 0.015f;

	public static float FogEndDistanceDaytime { get; set; }

	public static float FogEndDistanceNight { get; set; }

	public static float FogDensityNight { get; set; }

	private static void Postfix(LightingManager __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Invalid comparison between Unknown and I4
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if (val - 2 <= 2)
			{
				PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(povPlayer.Ref);
				float num = 0f;
				float num2 = 300f;
				float value = Traverse.Create((object)__instance).Field<float>("_localTimeOfDay").Value;
				if (value >= 6f && value <= 17f)
				{
					num = FogDensityDaytime;
					num2 = FogEndDistanceDaytime;
				}
				else if (value <= 5f || value >= 18f)
				{
					num = FogDensityNight;
					num2 = FogEndDistanceNight;
				}
				else if (value > 5f && value < 6f)
				{
					num = Mathf.Lerp(FogDensityDaytime, FogDensityNight, 6f - value);
					num2 = Mathf.Lerp(FogEndDistanceDaytime, FogEndDistanceNight, 6f - value);
				}
				else if (value > 17f && value < 18f)
				{
					num = Mathf.Lerp(FogDensityNight, FogDensityDaytime, 18f - value);
					num2 = Mathf.Lerp(FogEndDistanceNight, FogEndDistanceDaytime, 18f - value);
				}
				if (SabotageManager.Instance.IsSabotageActive(SabotageManager.SabotageIds.CursedNight))
				{
					num2 *= 0.65f;
				}
				switch (GameManagerCustom.Instance.EventsManager.CurrentEvent)
				{
				case EventsManager.EventType.Fog:
					num2 = 40f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.5f;
					RenderSettings.fogColor = BalancingValues.EventFogFogColor;
					break;
				case EventsManager.EventType.Spellstorm:
					num2 = 70f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.25f;
					RenderSettings.fogColor = BalancingValues.EventSpellstormFogColor;
					break;
				case EventsManager.EventType.Eclipse:
					num2 = 60f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.4f;
					RenderSettings.fogColor = BalancingValues.EventEclipseFogColor;
					break;
				case EventsManager.EventType.Plague:
					num2 = 65f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0f;
					RenderSettings.fogColor = BalancingValues.EventPlagueFogColor;
					break;
				}
				if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
				{
					num2 = 65f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.25f;
					RenderSettings.fogColor = BalancingValues.BeastActiveFogColor;
				}
				if (NetworkBool.op_Implicit(CultistManager.Instance.CultistActive))
				{
					num2 = 65f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.25f;
					RenderSettings.fogColor = BalancingValues.CultistActiveFogColor;
				}
				if (NetworkBool.op_Implicit(povPlayer.IsWolf))
				{
					if (NetworkBool.op_Implicit(player.Repulsion))
					{
						float num3 = Mathf.InverseLerp(0f, 1000f, (float)player.RepulsionStacks);
						num2 = Mathf.Lerp(BalancingValues.MysticRepulsorFogEndDistanceAtZeroStack, BalancingValues.MysticRepulsorFogEndDistanceAtMaxStacks, num3) * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
						num = 0.5f;
						RenderSettings.fogColor = BalancingValues.MysticRepulsorFogColor;
					}
					else if (PlayerCustomRegistry.Any((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && !NetworkBool.op_Implicit(o.PlayerController.IsDead)) && !NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
					{
						num2 = 55f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
						num = 0.5f;
						RenderSettings.fogColor = BalancingValues.LoverFogColor;
					}
					else
					{
						num2 += 25f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					}
				}
				if (SabotageManager.Instance.IsSabotageActive(SabotageManager.SabotageIds.LaboratoryLights) && !NetworkBool.op_Implicit(player.PlayerController.IsWolf))
				{
					num2 = 17.5f;
					num = 1f;
					RenderSettings.fogColor = Color.black;
				}
				else if (NetworkBool.op_Implicit(player.Nearsighted))
				{
					num2 = 12f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 1f;
					RenderSettings.fogColor = BalancingValues.NearsightedFogColor;
				}
				else if (NetworkBool.op_Implicit(player.Poison))
				{
					num2 = 20f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.75f;
					RenderSettings.fogColor = BalancingValues.SaboteurPoisonFogColor;
				}
				else if (NetworkBool.op_Implicit(player.Exorcised))
				{
					num2 = 20f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 1f;
					RenderSettings.fogColor = BalancingValues.ExorcistFogColor;
				}
				else if (NetworkBool.op_Implicit(player.Nauseated))
				{
					num2 = 30f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.75f;
					RenderSettings.fogColor = BalancingValues.NauseatedFogColor;
				}
				else if (NetworkBool.op_Implicit(player.Burning))
				{
					num2 = 30f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.5f;
					RenderSettings.fogColor = BalancingValues.MolotovBurnFogColor;
				}
				else if (NetworkBool.op_Implicit(player.PurifierBurn))
				{
					num2 = 25f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.5f;
					RenderSettings.fogColor = BalancingValues.PurifierBurnFogColor;
				}
				else if (NetworkBool.op_Implicit(player.Sleepy))
				{
					float num4 = Mathf.InverseLerp(0f, 1000f, (float)player.SleepStacks);
					num2 = Mathf.Lerp(BalancingValues.SleepingGasSleepyFogEndDistanceAtZeroStack, BalancingValues.SleepingGasSleepyFogEndDistanceAtMaxStacks, num4) * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.5f;
					RenderSettings.fogColor = BalancingValues.SleepingGasSleepyFogColor;
				}
				else if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
				{
					RenderSettings.fogColor = BalancingValues.ZombieFogColor;
				}
				else if (NetworkBool.op_Implicit(player.Isolation))
				{
					num2 = 40f * BalancingValues.NightFogEndDistanceMultiplierByMap(GameManager.Instance.MapID);
					num = 0.6f;
					RenderSettings.fogColor = BalancingValues.IsolationFogColor;
				}
				if (NetworkBool.op_Implicit(povPlayer.PlayerEffectManager.NightVision))
				{
					num2 += 200f;
					num /= 2f;
				}
				if (NetworkBool.op_Implicit(player.Petrified))
				{
					RenderSettings.fogColor = BalancingValues.PetrifiedFogColor;
				}
				if (NetworkBool.op_Implicit(povPlayer.IsZooming) || (NetworkBool.op_Implicit(povPlayer.IsAiming) && (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary || player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Poacher)))
				{
					num /= 4f;
					num2 *= 4f;
				}
				RenderSettings.fogDensity = num;
				RenderSettings.fogEndDistance = num2;
			}
			else
			{
				RenderSettings.fogDensity = 0f;
				RenderSettings.fogEndDistance = 300f;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("FogChangesPatch error: " + ex));
		}
	}
}
