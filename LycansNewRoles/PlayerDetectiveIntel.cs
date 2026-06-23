using System.Collections.Generic;
using Fusion;

namespace LycansNewRoles;

public class PlayerDetectiveIntel
{
	public enum PlayerDetectiveIntelType
	{
		OneIsEvil,
		DifferentSides,
		NotWolf,
		TransformationsAndDetransformations,
		WolvesAndSoloRolesRemaining
	}

	public int DayObtained;

	public bool IsNew = true;

	public virtual PlayerDetectiveIntelType Type => PlayerDetectiveIntelType.OneIsEvil;

	public static List<PlayerRef> GetUnreferencedPlayersIfPossible(List<PlayerRef> playerRefs, List<PlayerDetectiveIntel> intels, int requiredPlayersCount)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		List<PlayerRef> list = new List<PlayerRef>();
		list.AddRange(playerRefs);
		foreach (PlayerDetectiveIntel intel in intels)
		{
			if (intel is PlayerDetectiveIntelOneIsEvil playerDetectiveIntelOneIsEvil)
			{
				foreach (PlayerRef playerRef in playerDetectiveIntelOneIsEvil.PlayerRefs)
				{
					list.Remove(playerRef);
				}
			}
			else
			{
				if (!(intel is PlayerDetectiveIntelDifferentSides playerDetectiveIntelDifferentSides))
				{
					continue;
				}
				foreach (PlayerRef playerRef2 in playerDetectiveIntelDifferentSides.PlayerRefs)
				{
					list.Remove(playerRef2);
				}
			}
		}
		if (list.Count >= requiredPlayersCount)
		{
			return list;
		}
		return playerRefs;
	}
}
