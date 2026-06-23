using System.Collections.Generic;
using System.Linq;
using Fusion;

namespace LycansNewRoles.Stats;

public class GameStats
{
	public string Id;

	public string StartDate;

	public string EndDate;

	public string MapName;

	public int HarvestGoal;

	public int HarvestDone;

	public string EndTiming;

	public List<PlayerStats> PlayerStats = new List<PlayerStats>();

	public List<GameEvent> GameEvents = new List<GameEvent>();

	public void AddPlayerStats(PlayerStats playerStats)
	{
		if (PlayerStats.Any((PlayerStats o) => o.Username == playerStats.Username))
		{
			Plugin.Logger.LogError((object)("Player already existed for stats: " + playerStats.Username));
			PlayerStats.RemoveAll((PlayerStats o) => o.Username == playerStats.Username);
		}
		PlayerStats.Add(playerStats);
	}

	public void AddEvent(GameEvent.GameEventType type, string name)
	{
		GameEvents.Add(new GameEvent
		{
			Date = LycansUtility.GetFormattedCurrentDateTimeUtc,
			Timing = GetCurrentTiming(),
			Type = type.ToString(),
			Name = name
		});
	}

	public static string GetCurrentTiming()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		EGameState localGameState = GameManager.LocalGameState;
		EGameState val = localGameState;
		if (val - 2 > 1)
		{
			if ((int)val == 4)
			{
				return "M" + GameManagerCustom.Instance.CurrentDay;
			}
			return "?" + GameManagerCustom.Instance.CurrentDay;
		}
		if (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
		{
			return "N" + GameManagerCustom.Instance.CurrentDay;
		}
		return "J" + GameManagerCustom.Instance.CurrentDay;
	}
}
