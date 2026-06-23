using LycansNewRoles.NewItems;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapSleepingGasComponent : MonoBehaviour
{
	public static GameObject MinimapSleepingGasPrefab;

	public SleepingGasPlaced SleepingGasPlaced;

	private Image _imageUp;

	private Image _imageDown;

	public void Init(SleepingGasPlaced sleepingGas)
	{
		SleepingGasPlaced = sleepingGas;
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).gameObject.GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).gameObject.GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
	}

	private void Update()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom local = PlayerCustom.Local;
		if ((Object)(object)SleepingGasPlaced == (Object)null || SleepingGasPlaced.CreatorRef != local.Ref)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else if (Plugin.Minimap.State != MinimapComponent.MinimapState.Inactive && BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
		{
			if (((Component)SleepingGasPlaced).transform.position.y > ((Component)local.PlayerController).transform.position.y + 3.5f)
			{
				((Behaviour)_imageUp).enabled = true;
				((Behaviour)_imageDown).enabled = false;
			}
			else if (((Component)SleepingGasPlaced).transform.position.y < ((Component)local.PlayerController).transform.position.y - 3.5f)
			{
				((Behaviour)_imageUp).enabled = false;
				((Behaviour)_imageDown).enabled = true;
			}
			else
			{
				((Behaviour)_imageUp).enabled = false;
				((Behaviour)_imageDown).enabled = false;
			}
		}
	}
}
