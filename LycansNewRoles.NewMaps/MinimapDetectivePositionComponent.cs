using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapDetectivePositionComponent : MonoBehaviour
{
	public static GameObject MinimapDetectivePositionPrefab;

	public Vector3 Position;

	private Image _imageUp;

	private Image _imageDown;

	public void Init(Vector3 position)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		Position = position;
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).gameObject.GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).gameObject.GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Invalid comparison between Unknown and I4
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState == 5 || (int)GameManager.LocalGameState == 1 || (int)GameManager.LocalGameState == 0)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else if (Plugin.Minimap.State != MinimapComponent.MinimapState.Inactive && BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (Position.y > ((Component)player.PlayerController).transform.position.y + 3.5f)
			{
				((Behaviour)_imageUp).enabled = true;
				((Behaviour)_imageDown).enabled = false;
			}
			else if (Position.y < ((Component)player.PlayerController).transform.position.y - 3.5f)
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
