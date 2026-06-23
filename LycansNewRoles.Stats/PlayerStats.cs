using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.Stats;

public class PlayerStats
{
	public class PositionStat
	{
		public float x;

		public float y;

		public float z;
	}

	public class VoteStat
	{
		public string Target;

		public string Date;

		public int Day;

		public VoteStat(string target, string date)
		{
			Target = target;
			Date = date;
			Day = GameManagerCustom.Instance.CurrentDay;
		}
	}

	public class MainRoleChangeEvent
	{
		public string NewMainRole;

		public string RoleChangeDateIrl;

		public MainRoleChangeEvent(string newMainRole)
		{
			NewMainRole = newMainRole;
			RoleChangeDateIrl = LycansUtility.GetFormattedCurrentDateTimeUtc;
		}
	}

	public class PlayerAction
	{
		public const string ActionDrinkPotion = "DrinkPotion";

		public const string ActionUseGadget = "UseGadget";

		public const string ActionSabotage = "Sabotage";

		public const string ActionShoot = "Shoot";

		public const string ActionTransform = "Transform";

		public const string ActionUntransform = "Untransform";

		public const string ActionHunterShoot = "HunterShoot";

		public const string ActionTakeAccessory = "TakeAccessory";

		public string Date;

		public string Timing;

		public PositionStat Position;

		public string ActionType;

		public string ActionName;

		public string ActionTarget;
	}

	public const string DeathTypeStarvation = "STARVATION";

	public const string DeathTypeWolf = "BY_WOLF";

	public const string DeathTypeZombie = "BY_ZOMBIE";

	public const string DeathTypeBeast = "BY_BEAST";

	public const string DeathTypeCrushed = "CRUSHED";

	public const string DeathTypeStarvationBeast = "STARVATION_AS_BEAST";

	public const string DeathTypeFall = "FALL";

	public const string DeathTypeLover = "LOVER_DEATH";

	public const string DeathTypeBulletHumanForm = "BULLET_HUMAN";

	public const string DeathTypeBulletWolfForm = "BULLET_WOLF";

	public const string DeathTypeSherifSuccess = "SHERIF_SUCCESS";

	public const string DeathTypeSherifMistake = "SHERIF_MISTAKE";

	public const string DeathTypeAgent = "OTHER_AGENT";

	public const string DeathTypeAvenger = "AVENGER";

	public const string DeathTypeCrystalBallGuess = "SEER";

	public const string DeathTypeAssassin = "ASSASSIN";

	public const string DeathTypeBomb = "BOMB";

	public const string DeathTypeVote = "VOTED";

	public const string DeathTypeSurvivalistNotSaved = "SURVIVALIST_NOT_SAVED";

	public const string DeathTypeKilledVillageIdiot = "KILLED_VILLAGE_IDIOT";

	public const string DeathTypeMercenaryKilledInHunt = "MERCENARY_HUNT_KILL";

	public const string DeathTypeInquisitorBurn = "INQUISITOR_GUESS";

	public const string DeathTypeCultistFailed = "CULTIST_FAILED";

	public const string DeathTypeMole = "MOLE";

	public string ID;

	public string Username;

	public string Color;

	public string Hat;

	public string MainRoleInitial;

	public List<MainRoleChangeEvent> MainRoleChanges = new List<MainRoleChangeEvent>();

	public string Power;

	public string SecondaryRole;

	public string DeathDateIrl;

	[NonSerialized]
	public int DeathDay;

	public string DeathTiming;

	public PositionStat DeathPosition;

	public string DeathType = null;

	[NonSerialized]
	public PlayerRef KillerRef;

	public string KillerName;

	public bool Victorious;

	public List<VoteStat> Votes = new List<VoteStat>();

	public int SecondsTalkedOutsideMeeting = 0;

	public int SecondsTalkedDuringMeeting = 0;

	public int SecondsSpentImmobileStanding = 0;

	public int SecondsSpentImmobileCrouched = 0;

	public int SecondsSpentWalkingStanding = 0;

	public int SecondsSpentWalkingCrouched = 0;

	public int SecondsSpentRunning = 0;

	public int TotalCollectedLoot = 0;

	public List<PlayerAction> Actions = new List<PlayerAction>();

	[NonSerialized]
	public PlayerRef PlayerRef;

	[NonSerialized]
	private Stopwatch _stopwatchTalkOutsideMeeting = new Stopwatch();

	[NonSerialized]
	private Stopwatch _stopwatchTalkDuringMeeting = new Stopwatch();

	public static int DeathTypeStringToInt(string deathType)
	{
		return deathType switch
		{
			"STARVATION" => 1, 
			"BY_WOLF" => 2, 
			"BY_ZOMBIE" => 3, 
			"BY_BEAST" => 4, 
			"CRUSHED" => 5, 
			"STARVATION_AS_BEAST" => 6, 
			"FALL" => 7, 
			"LOVER_DEATH" => 8, 
			"BULLET_HUMAN" => 9, 
			"BULLET_WOLF" => 10, 
			"SHERIF_SUCCESS" => 11, 
			"SHERIF_MISTAKE" => 12, 
			"OTHER_AGENT" => 13, 
			"AVENGER" => 14, 
			"SEER" => 15, 
			"ASSASSIN" => 16, 
			"BOMB" => 17, 
			"VOTED" => 18, 
			"SURVIVALIST_NOT_SAVED" => 19, 
			"KILLED_VILLAGE_IDIOT" => 20, 
			"MERCENARY_HUNT_KILL" => 21, 
			"INQUISITOR_GUESS" => 22, 
			"CULTIST_FAILED" => 23, 
			"MOLE" => 24, 
			_ => 0, 
		};
	}

	public static string DeathTypeIntToString(int deathType)
	{
		return deathType switch
		{
			1 => "STARVATION", 
			2 => "BY_WOLF", 
			3 => "BY_ZOMBIE", 
			4 => "BY_BEAST", 
			5 => "CRUSHED", 
			6 => "STARVATION_AS_BEAST", 
			7 => "FALL", 
			8 => "LOVER_DEATH", 
			9 => "BULLET_HUMAN", 
			10 => "BULLET_WOLF", 
			11 => "SHERIF_SUCCESS", 
			12 => "SHERIF_MISTAKE", 
			13 => "OTHER_AGENT", 
			14 => "AVENGER", 
			15 => "SEER", 
			16 => "ASSASSIN", 
			17 => "BOMB", 
			18 => "VOTED", 
			19 => "SURVIVALIST_NOT_SAVED", 
			20 => "KILLED_VILLAGE_IDIOT", 
			21 => "MERCENARY_HUNT_KILL", 
			22 => "INQUISITOR_GUESS", 
			23 => "CULTIST_FAILED", 
			24 => "MOLE", 
			_ => "", 
		};
	}

	public void UpdateDeathType(string deathType)
	{
		LycansUtility.AddLogOnlyForMe("UpdateDeathType for " + Username + ", current type: " + DeathType);
		if (string.IsNullOrEmpty(DeathType))
		{
			LycansUtility.AddLogOnlyForMe("-> Update with " + deathType);
			DeathType = deathType;
		}
	}

	public void OnKilled(PlayerRef killerRef, Vector3 position)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Invalid comparison between Unknown and I4
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Invalid comparison between Unknown and I4
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		LycansUtility.AddLogOnlyForMe("OnKilled for " + Username + ", current death date irl: " + DeathDateIrl);
		if (!string.IsNullOrEmpty(DeathDateIrl))
		{
			return;
		}
		DeathDateIrl = LycansUtility.GetFormattedCurrentDateTimeUtc;
		EGameState localGameState = GameManager.LocalGameState;
		EGameState val = localGameState;
		if (val - 2 > 1)
		{
			if ((int)val == 4)
			{
				DeathTiming = "M";
			}
			else
			{
				DeathTiming = "?";
			}
		}
		else
		{
			DeathTiming = (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) ? "N" : "J");
		}
		DeathTiming += GameManagerCustom.Instance.CurrentDay;
		DeathDay = GameManagerCustom.Instance.CurrentDay;
		DeathPosition = new PositionStat
		{
			x = position.x,
			y = position.y,
			z = position.z
		};
		if (killerRef != PlayerRef.None && PlayerRegistry.HasPlayer(killerRef))
		{
			PlayerController player = PlayerRegistry.GetPlayer(killerRef);
			KillerRef = killerRef;
			KillerName = ((object)player.PlayerData.Username/*cast due to constrained. prefix*/).ToString();
		}
		LycansUtility.AddLogOnlyForMe("-> Update with killer " + KillerName);
		if (!string.IsNullOrEmpty(DeathType))
		{
			LycansUtility.AddLogOnlyForMe("-> Add to summary");
			PlayerCustom.Rpc_Add_Game_Summary_Kill(((SimulationBehaviour)GameManager.Instance).Runner, (killerRef != PlayerRef.None && PlayerRegistry.HasPlayer(killerRef)) ? PlayerRegistry.GetPlayer(killerRef).Index : (-1), PlayerRegistry.GetPlayer(PlayerRef).Index, DeathTypeStringToInt(DeathType));
		}
	}

	public void OnTalkingChanged(bool talking)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Invalid comparison between Unknown and I4
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Invalid comparison between Unknown and I4
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Invalid comparison between Unknown and I4
		if (!talking)
		{
			_stopwatchTalkOutsideMeeting.Stop();
			_stopwatchTalkDuringMeeting.Stop();
			SecondsTalkedOutsideMeeting = Mathf.FloorToInt((float)(_stopwatchTalkOutsideMeeting.ElapsedMilliseconds / 1000));
			SecondsTalkedDuringMeeting = Mathf.FloorToInt((float)(_stopwatchTalkDuringMeeting.ElapsedMilliseconds / 1000));
		}
		else if ((int)GameManager.LocalGameState == 4)
		{
			_stopwatchTalkDuringMeeting.Start();
		}
		else if ((int)GameManager.LocalGameState == 2 || (int)GameManager.LocalGameState == 3)
		{
			_stopwatchTalkOutsideMeeting.Start();
		}
	}

	public void AddAction(PlayerAction action, Vector3 position)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		string text = "";
		EGameState localGameState = GameManager.LocalGameState;
		EGameState val = localGameState;
		text = ((val - 2 <= 1) ? (NetworkBool.op_Implicit(GameManager.LightingManager.IsNight) ? "N" : "J") : (((int)val != 4) ? "?" : "M"));
		text += GameManagerCustom.Instance.CurrentDay;
		action.Date = LycansUtility.GetFormattedCurrentDateTimeUtc;
		action.Timing = text;
		action.Position = new PositionStat
		{
			x = position.x,
			y = position.y,
			z = position.z
		};
		Actions.Add(action);
	}
}
