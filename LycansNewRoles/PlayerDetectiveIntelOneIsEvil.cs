using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;

namespace LycansNewRoles;

public class PlayerDetectiveIntelOneIsEvil : PlayerDetectiveIntel
{
	public List<PlayerRef> PlayerRefs = new List<PlayerRef>();

	public static int Cost => 1;

	public override PlayerDetectiveIntelType Type => PlayerDetectiveIntelType.OneIsEvil;

	public static bool CanGet(PlayerCustom playerCustom)
	{
		return PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.Ref != playerCustom.Ref && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j.Type == PlayerDetectiveIntelType.OneIsEvil && (j as PlayerDetectiveIntelOneIsEvil).ContainsSpecificPlayer(o.Ref))) >= BalancingValues.DetectiveOneIsEvilPlayersToAdd(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController j) => !NetworkBool.op_Implicit(j.IsDead))));
	}

	public bool ContainsSpecificPlayer(PlayerRef player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return PlayerRefs.Contains(player);
	}
}
