using LycansNewRoles.PowerObjects;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapAcrobatSpotComponent : MonoBehaviour
{
	public static GameObject MinimapAcrobatSpotPrefab;

	public AcrobatSpot Spot;

	private Image _visual;

	public void Init(AcrobatSpot spot)
	{
		Spot = spot;
		_visual = ((Component)this).GetComponent<Image>();
	}

	private void Update()
	{
		if ((Object)(object)Spot == (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else if (Plugin.Minimap.State != MinimapComponent.MinimapState.Inactive)
		{
			((Component)_visual).gameObject.SetActive(PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Acrobat);
		}
	}
}
