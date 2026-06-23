using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapClairvoyanceRadiusComponent : MonoBehaviour
{
	private Image _radius;

	private bool ColorsUp = false;

	private void Awake()
	{
		_radius = ((Component)((Component)this).transform.Find("Radius")).GetComponent<Image>();
	}

	public void Init(float radiusScale)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		((Component)_radius).transform.localScale = new Vector3(radiusScale, radiusScale, radiusScale);
	}

	private void Update()
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		if (Plugin.Minimap.State == MinimapComponent.MinimapState.Inactive)
		{
			return;
		}
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
	}
}
