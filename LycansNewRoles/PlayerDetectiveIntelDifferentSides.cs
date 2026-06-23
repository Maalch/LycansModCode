using System.Collections.Generic;
using System.Linq;
using Fusion;

namespace LycansNewRoles;

public class PlayerDetectiveIntelDifferentSides : PlayerDetectiveIntel
{
	public List<PlayerRef> PlayerRefs = new List<PlayerRef>();

	public static int Cost => 2;

	public override PlayerDetectiveIntelType Type => PlayerDetectiveIntelType.DifferentSides;

	public static bool CanGet(PlayerCustom playerCustom)
	{
		return GameManagerCustom.Instance.CurrentDay >= 2 && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel o) => o.Type == PlayerDetectiveIntelType.DifferentSides);
	}
}
