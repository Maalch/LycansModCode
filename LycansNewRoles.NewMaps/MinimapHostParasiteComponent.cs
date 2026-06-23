using Fusion;
using LycansNewRoles.PowerObjects;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapHostParasiteComponent : MonoBehaviour
{
	public static GameObject MinimapHostParasitePrefab;

	public HostParasite Parasite;

	private Image _imageUp;

	private Image _imageDown;

	private Image _icon;

	public void Init(HostParasite parasite)
	{
		Parasite = parasite;
		_icon = ((Component)this).GetComponent<Image>();
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
	}

	private void Update()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Parasite == (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else
		{
			if (Plugin.Minimap.State == MinimapComponent.MinimapState.Inactive)
			{
				return;
			}
			((Graphic)_icon).color = (NetworkBool.op_Implicit(Parasite.Appeared) ? Color.green : Color.red);
			if (BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				if (((Component)Parasite).transform.position.y > ((Component)player.PlayerController).transform.position.y + 3.5f)
				{
					((Behaviour)_imageUp).enabled = true;
					((Behaviour)_imageDown).enabled = false;
				}
				else if (((Component)Parasite).transform.position.y < ((Component)player.PlayerController).transform.position.y - 3.5f)
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
}
