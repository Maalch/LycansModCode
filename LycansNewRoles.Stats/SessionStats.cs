using System;
using System.Collections.Generic;
using Fusion;
using LycansNewRoles.NewMaps;

namespace LycansNewRoles.Stats;

public class SessionStats
{
	public static SessionStats Stats = new SessionStats
	{
		ModVersion = "0.312",
		Filename = ((object)PlayerController.Local.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "-" + LycansUtility.GetCurrentDateTimeUtcForId,
		Key = "N8W0_QJ7Z5"
	};

	[NonSerialized]
	public GameStats CurrentGame = new GameStats();

	public string ModVersion;

	public string Filename;

	public string Key;

	public List<GameStats> GameStats = new List<GameStats>();

	public void NewGame()
	{
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			string mapName = "???";
			switch (MapManager.FindMapNameById(GameManager.Instance.MapID))
			{
			case "map_1":
				mapName = "Village";
				break;
			case "map_2":
				mapName = "Château";
				break;
			case "map_dungeon":
				mapName = "Le Donjon";
				break;
			case "map_haddoncans":
				mapName = "Haddoncans";
				break;
			case "map_apartcan":
				mapName = "Ashfang Woods";
				break;
			case "map_laboratory":
				mapName = "Lupus Labs";
				break;
			case "map_got":
				mapName = "Wisterio Lane";
				break;
			}
			CurrentGame = new GameStats
			{
				Id = ((object)PlayerController.Local.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "-" + LycansUtility.GetCurrentDateTimeUtcForId,
				StartDate = LycansUtility.GetFormattedCurrentDateTimeUtc,
				MapName = mapName,
				HarvestGoal = GameManager.Instance.MaxScore
			};
			if (!NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
			{
				GameStats.Add(CurrentGame);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewGame exception: " + ex));
		}
	}
}
