using UnityEngine;

namespace LycansNewRoles;

public class PlayerMercenaryTargetIconComponent : MonoBehaviour
{
	public static GameObject TargetIconPrefab;

	private GameObject _targetIcon;

	private void Awake()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		_targetIcon = Object.Instantiate<GameObject>(TargetIconPrefab, ((Component)((Component)this).GetComponent<PlayerController>()).transform);
		_targetIcon.SetActive(true);
		_targetIcon.transform.position = new Vector3(_targetIcon.transform.position.x, _targetIcon.transform.position.y + 2f, _targetIcon.transform.position.z);
		_targetIcon.SetActive(false);
	}

	private void Update()
	{
		if (_targetIcon.activeSelf)
		{
			_targetIcon.transform.LookAt(((Component)PlayerController.Local.LocalCameraHandler.PovPlayer).transform);
		}
	}

	public void SetVisible(bool visible)
	{
		GameObject targetIcon = _targetIcon;
		if (targetIcon != null)
		{
			targetIcon.SetActive(visible);
		}
	}
}
