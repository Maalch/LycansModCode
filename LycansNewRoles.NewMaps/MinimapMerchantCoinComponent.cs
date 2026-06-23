using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapMerchantCoinComponent : MonoBehaviour
{
	public static GameObject MinimapMerchantCoinPrefab;

	public MerchantCoin Coin;

	private Image _imageUp;

	private Image _imageDown;

	public void Init(MerchantCoin coin)
	{
		Coin = coin;
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).gameObject.GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).gameObject.GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
	}

	private void Update()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Coin == (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else if (Plugin.Minimap.State != MinimapComponent.MinimapState.Inactive && BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (((Component)Coin).transform.position.y > ((Component)player.PlayerController).transform.position.y + 3.5f)
			{
				((Behaviour)_imageUp).enabled = true;
				((Behaviour)_imageDown).enabled = false;
			}
			else if (((Component)Coin).transform.position.y < ((Component)player.PlayerController).transform.position.y - 3.5f)
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
