using System.Globalization;
using UnityEngine;

namespace LycansNewRoles.NewMaps;

public class CustomMap
{
	public float MinimapOffsetMultiplier;

	public Vector3 MinimapCameraOffset;

	public float MinimapRotation;

	public Vector3 MapResize;

	public string MapName;

	public CustomMap(int mapId, string mapName, Transform minimapOffsetMultiplierTransform, Transform minimapCameraOffsetTransform, Transform minimapRotationTransform, Vector3 localScale)
	{
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		float minimapOffsetMultiplier = 1f;
		if ((Object)(object)minimapOffsetMultiplierTransform != (Object)null)
		{
			minimapOffsetMultiplier = float.Parse(((Object)minimapOffsetMultiplierTransform.GetChild(0)).name.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture);
		}
		Vector3 minimapCameraOffset = default(Vector3);
		((Vector3)(ref minimapCameraOffset))._002Ector(0f, 0f, 0f);
		if ((Object)(object)minimapCameraOffsetTransform != (Object)null)
		{
			string[] array = ((Object)minimapCameraOffsetTransform.GetChild(0)).name.Split(';');
			((Vector3)(ref minimapCameraOffset))._002Ector(float.Parse(array[0].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture), float.Parse(array[1].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture), float.Parse(array[2].Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture));
		}
		float minimapRotation = 0f;
		if ((Object)(object)minimapRotationTransform != (Object)null)
		{
			minimapRotation = float.Parse(((Object)minimapRotationTransform.GetChild(0)).name);
		}
		MinimapOffsetMultiplier = minimapOffsetMultiplier;
		MinimapCameraOffset = minimapCameraOffset;
		MinimapRotation = minimapRotation;
		MapResize = localScale;
		MapName = mapName;
	}
}
