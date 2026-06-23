using LycansNewRoles.PowerObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapExorcistDetectorComponent : MonoBehaviour
{
	public static GameObject MinimapExorcistDetectorPrefab;

	public ExorcistDetector Detector;

	private Image _imageUp;

	private Image _imageDown;

	private TextMeshProUGUI _textRemainingDuration;

	public void Init(ExorcistDetector detector)
	{
		Detector = detector;
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).gameObject.GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).gameObject.GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
		_textRemainingDuration = ((Component)((Component)this).transform.Find("RemainingDuration")).gameObject.GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Detector == (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else
		{
			if (Plugin.Minimap.State == MinimapComponent.MinimapState.Inactive)
			{
				return;
			}
			((TMP_Text)_textRemainingDuration).text = Detector.RemainingDuration + "s";
			if (BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				if (((Component)Detector).transform.position.y > ((Component)player.PlayerController).transform.position.y + 3.5f)
				{
					((Behaviour)_imageUp).enabled = true;
					((Behaviour)_imageDown).enabled = false;
				}
				else if (((Component)Detector).transform.position.y < ((Component)player.PlayerController).transform.position.y - 3.5f)
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
