namespace LycansNewRoles.Stats;

public class GameEvent
{
	public enum GameEventType
	{
		NewMayor,
		SabotageActive,
		NewPhase,
		DailyEventStart
	}

	public string Date;

	public string Timing;

	public string Type;

	public string Name;
}
