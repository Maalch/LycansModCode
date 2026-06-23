using LycansNewRoles.PowerObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapInvestigatorTargetComponent : MonoBehaviour
{
	public static GameObject MinimapInvestigatorTargetPrefab;

	public InvestigatorHint Hint;

	private Image _imageUp;

	private Image _imageDown;

	private TextMeshProUGUI _textRemainingDuration;

	public void Init(InvestigatorHint hint)
	{
		Hint = hint;
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).gameObject.GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).gameObject.GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
		_textRemainingDuration = ((Component)((Component)this).transform.Find("RemainingDuration")).gameObject.GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Hint == (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else
		{
			if (Plugin.Minimap.State == MinimapComponent.MinimapState.Inactive)
			{
				return;
			}
			((TMP_Text)_textRemainingDuration).text = Hint.RemainingDuration + "s";
			Image component = ((Component)this).GetComponent<Image>();
			if ((float)Hint.RemainingDuration >= 30f)
			{
				((Graphic)component).color = Color.green;
			}
			else
			{
				((Graphic)component).color = Color.red;
			}
			if (BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				if (((Component)Hint).transform.position.y > ((Component)player.PlayerController).transform.position.y + 3.5f)
				{
					((Behaviour)_imageUp).enabled = true;
					((Behaviour)_imageDown).enabled = false;
				}
				else if (((Component)Hint).transform.position.y < ((Component)player.PlayerController).transform.position.y - 3.5f)
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
