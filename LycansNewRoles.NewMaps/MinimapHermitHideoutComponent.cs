using LycansNewRoles.PowerObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapHermitHideoutComponent : MonoBehaviour
{
	public static GameObject MinimapHermitHideoutPrefab;

	public HermitHideout Hideout;

	private Image _visual;

	private Image _imageUp;

	private Image _imageDown;

	private TextMeshProUGUI _textRemainingDuration;

	public void Init(HermitHideout hideout, float radiusScale)
	{
		Hideout = hideout;
		_visual = ((Component)this).GetComponent<Image>();
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).gameObject.GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).gameObject.GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
		_textRemainingDuration = ((Component)((Component)this).transform.Find("RemainingDuration")).gameObject.GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Hideout == (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else
		{
			if (Plugin.Minimap.State == MinimapComponent.MinimapState.Inactive)
			{
				return;
			}
			if (Hideout.RemainingDuration < 30)
			{
				((Graphic)_visual).color = Color.red;
			}
			else if (Hideout.RemainingDuration < 60)
			{
				((Graphic)_visual).color = Color.yellow;
			}
			else
			{
				((Graphic)_visual).color = Color.green;
			}
			((TMP_Text)_textRemainingDuration).text = Hideout.RemainingDuration + "s";
			if (BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				if (((Component)Hideout).transform.position.y > ((Component)player.PlayerController).transform.position.y + 3.5f)
				{
					((Behaviour)_imageUp).enabled = true;
					((Behaviour)_imageDown).enabled = false;
				}
				else if (((Component)Hideout).transform.position.y < ((Component)player.PlayerController).transform.position.y - 3.5f)
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
