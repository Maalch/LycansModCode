using System.Linq;
using Fusion;

namespace LycansNewRoles;

public class PlayerDetectiveIntelWolvesAndSoloRolesRemaining : PlayerDetectiveIntel
{
	public int Wolves;

	public int SoloRoles;

	public static int Cost => 1;

	public override PlayerDetectiveIntelType Type => PlayerDetectiveIntelType.WolvesAndSoloRolesRemaining;

	public static bool CanGet(PlayerCustom playerCustom)
	{
		return PlayerCustomRegistry.CountWhere((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsDead)) > 0 && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j.Type == PlayerDetectiveIntelType.WolvesAndSoloRolesRemaining && j.DayObtained == GameManagerCustom.Instance.CurrentDay);
	}
}
