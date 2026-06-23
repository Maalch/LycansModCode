using LycansNewRoles.PowerObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapScoutRadarComponent : MonoBehaviour
{
	public static GameObject MinimapScoutRadarPrefab;

	public ScoutRadar Radar;

	private Image _imageUp;

	private Image _imageDown;

	private Image _radius;

	private TextMeshProUGUI _textRemainingDuration;

	private bool ColorsUp = false;

	public void Init(ScoutRadar radar, float radiusScale)
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		Radar = radar;
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).gameObject.GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).gameObject.GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
		_textRemainingDuration = ((Component)((Component)this).transform.Find("RemainingDuration")).gameObject.GetComponent<TextMeshProUGUI>();
		_radius = ((Component)((Component)this).transform.Find("Radius")).GetComponent<Image>();
		((Component)_radius).transform.localScale = new Vector3(radiusScale, radiusScale, radiusScale);
	}

	private void Update()
	{
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Radar == (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else
		{
			if (Plugin.Minimap.State == MinimapComponent.MinimapState.Inactive)
			{
				return;
			}
			((TMP_Text)_textRemainingDuration).text = Radar.RemainingDuration + "s";
			float deltaTime = Time.deltaTime;
			if (ColorsUp)
			{
				((Graphic)_radius).color = new Color(((Graphic)_radius).color.r, ((Graphic)_radius).color.g, ((Graphic)_radius).color.b, ((Graphic)_radius).color.a + deltaTime / 3f);
				if ((double)((Graphic)_radius).color.a >= 0.3)
				{
					ColorsUp = false;
				}
			}
			else
			{
				((Graphic)_radius).color = new Color(((Graphic)_radius).color.r, ((Graphic)_radius).color.g, ((Graphic)_radius).color.b, ((Graphic)_radius).color.a - deltaTime / 3f);
				if (((Graphic)_radius).color.a <= 0f)
				{
					ColorsUp = true;
				}
			}
			if (BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				if (((Component)Radar).transform.position.y > ((Component)player.PlayerController).transform.position.y + 3.5f)
				{
					((Behaviour)_imageUp).enabled = true;
					((Behaviour)_imageDown).enabled = false;
				}
				else if (((Component)Radar).transform.position.y < ((Component)player.PlayerController).transform.position.y - 3.5f)
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
