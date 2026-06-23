using UnityEngine;

namespace LycansNewRoles;

public class LastGameSummaryKill
{
	public enum LastGameSummaryKillTiming
	{
		Day,
		Night,
		Meeting
	}

	private LastGameSummaryKillTiming _timing;

	private int _day;

	private string _killer;

	private Color _killerColor;

	private string _deathType;

	private string _victim;

	private Color _victimColor;

	public LastGameSummaryKillTiming Timing => _timing;

	public int Day => _day;

	public string Killer => _killer;

	public Color KillerColor => _killerColor;

	public string DeathType => _deathType;

	public string Victim => _victim;

	public Color VictimColor => _victimColor;

	public LastGameSummaryKill(LastGameSummaryKillTiming timing, int day, string killer, Color killerColor, string deathType, string victim, Color victimColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		_timing = timing;
		_day = day;
		_killer = killer;
		_killerColor = killerColor;
		_deathType = deathType;
		_victim = victim;
		_victimColor = victimColor;
	}
}
