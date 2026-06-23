using Fusion;

namespace LycansNewRoles.NewPrimaryRoles;

public static class ScientistUtility
{
	public static float GetBasePower(PlayerCustom scientistCustom, PlayerController wolf, float distance, float maxDistance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Invalid comparison between Unknown and I4
		if (NetworkBool.op_Implicit(scientistCustom.Phasing))
		{
			return 0f;
		}
		float num = 20f + 50f * (1f - distance / maxDistance);
		num *= BalancingValues.ScientistPowerMultiplierByMap(GameManager.Instance.MapID);
		if ((int)wolf.Role != 1)
		{
			num *= 0.2f;
		}
		num *= (float)Plugin.CustomConfig.ScientistResearchSpeed * 0.01f;
		return num / GameManagerCustom.Instance.SoloRoleDifficulty;
	}
}
