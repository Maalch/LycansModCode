using System.Linq;

namespace LycansNewRoles;

public class PlayerDetectiveIntelTransformationsAndDetransformations : PlayerDetectiveIntel
{
	public int Transformations;

	public int Detransformations;

	public static int Cost => 1;

	public override PlayerDetectiveIntelType Type => PlayerDetectiveIntelType.TransformationsAndDetransformations;

	public static bool CanGet(PlayerCustom playerCustom)
	{
		return GameManagerCustom.Instance.CurrentDay >= 2 && !playerCustom.DetectiveIntelList.Any((PlayerDetectiveIntel j) => j.Type == PlayerDetectiveIntelType.TransformationsAndDetransformations && j.DayObtained == GameManagerCustom.Instance.CurrentDay) && (GameManagerCustom.Instance.TransformationsAmountByDay.ContainsKey(GameManagerCustom.Instance.CurrentDay - 1) || GameManagerCustom.Instance.DetransformationsAmountByDay.ContainsKey(GameManagerCustom.Instance.CurrentDay - 1));
	}
}
