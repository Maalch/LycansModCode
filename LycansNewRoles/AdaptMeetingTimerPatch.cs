using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "StartVoteTimer")]
internal class AdaptMeetingTimerPatch
{
	private static bool Prefix(GameManager __instance)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(Plugin.CustomConfig.AdaptMeetingTimer) && !NetworkBool.op_Implicit(__instance.BattleRoyale))
		{
			float num = Traverse.Create((object)__instance).Property<int>("VoteTime", (object[])null).Value;
			float num2 = BalancingValues.AdaptMeetingTimerMultiplier(PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld), PlayerRegistry.Count);
			LycansUtility.AddLogOnlyForMe("Meeting timer multiplier: " + num2);
			num *= num2;
			__instance.VoteTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, num);
			return false;
		}
		return true;
	}
}
