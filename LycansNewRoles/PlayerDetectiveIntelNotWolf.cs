using System.Linq;
using Fusion;

namespace LycansNewRoles;

public class PlayerDetectiveIntelNotWolf : PlayerDetectiveIntel
{
	public PlayerRef Target;

	public static int Cost => 2;

	public override PlayerDetectiveIntelType Type => PlayerDetectiveIntelType.NotWolf;

	public static bool CanGet(PlayerCustom playerCustom)
	{
		return PlayerCustomRegistry.Any((PlayerCustom o) => (int)o.PlayerController.Role != 1 && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.VillageIdiot && o.Ref != playerCustom.Ref && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j.Type == PlayerDetectiveIntelType.NotWolf && (j as PlayerDetectiveIntelNotWolf).Target == o.Ref));
	}
}
