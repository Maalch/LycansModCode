using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(LightingManager), "FixedUpdateNetwork")]
internal class LightingManagerFixedUpdateNetworkPatch
{
	public static float TimeOfDayAccurate;

	private static bool Prefix(LightingManager __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) || NetworkBool.op_Implicit(DraftManager.Instance.Active))
			{
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("BeastNoCyclePatch error: " + ex));
			return true;
		}
	}

	private static void Postfix(LightingManager __instance)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Invalid comparison between Unknown and I4
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		if (!NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && !NetworkBool.op_Implicit(DraftManager.Instance.Active) && ((SimulationBehaviour)__instance).Runner.IsServer && !NetworkBool.op_Implicit(GameManager.Instance.IsFinished) && (int)GameManager.State.Current == 2 && !NetworkBool.op_Implicit(__instance.IsTransition))
		{
			float num = (NetworkBool.op_Implicit(__instance.IsNight) ? GameManager.Instance.NightDuration : GameManager.Instance.DayDuration);
			float num2 = ((SimulationBehaviour)__instance).Runner.DeltaTime / num;
			if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Eclipse)
			{
				num2 *= 1.5f;
			}
			if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.FullMoon && NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
			{
				num2 *= 1.65f;
			}
			float num3 = TimeOfDayAccurate + num2;
			TimeOfDayAccurate = num3 % 24f;
			__instance.TimeOfDay = TimeOfDayAccurate;
			Traverse.Create((object)__instance).Field<float>("_TimeOfDay").Value = TimeOfDayAccurate;
			Traverse.Create((object)__instance).Field<float>("_localTimeOfDay").Value = TimeOfDayAccurate;
		}
	}
}
