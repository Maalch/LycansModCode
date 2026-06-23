using Managers;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerAstralSpiritCameraHandler : MonoBehaviour
{
	private Vector2 _viewInput;

	private float _cameraRotationX;

	private float _cameraRotationY;

	private Transform localCameraAnchorPoint;

	private float _rotationSpeed = 120f;

	public Camera LocalCamera { get; private set; }

	private void Awake()
	{
		LocalCamera = ((Component)this).GetComponent<Camera>();
		localCameraAnchorPoint = ((Component)this).transform.parent.Find("AnchorPoint");
	}

	private void Start()
	{
		if (((Behaviour)LocalCamera).enabled)
		{
			((Component)LocalCamera).transform.parent = null;
			if (PlayerPrefs.HasKey("SETTINGS_ROTATION_SPEED"))
			{
				_rotationSpeed = PlayerPrefs.GetFloat("SETTINGS_ROTATION_SPEED");
			}
		}
	}

	public void InitRotation(float x, float y)
	{
		_cameraRotationX = x;
		_cameraRotationY = y;
	}

	public void SetRotationSpeed(float rotationSpeed)
	{
		_rotationSpeed = rotationSpeed;
	}

	private void LateUpdate()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)LocalCamera).enabled)
		{
			((Component)LocalCamera).transform.position = localCameraAnchorPoint.position;
			if (!GameManager.Instance.gameUI.IsSettingMenuOpen && !GameManager.Instance.gameUI.IsGameSettingMenuOpen)
			{
				float num = (InputManager.Instance.IsGamepad() ? 2f : 1f);
				_cameraRotationX += _viewInput.y * _rotationSpeed * num * Time.smoothDeltaTime;
				_cameraRotationX = Mathf.Clamp(_cameraRotationX, -70f, 70f);
				_cameraRotationY += _viewInput.x * _rotationSpeed * num * Time.smoothDeltaTime;
				((Component)LocalCamera).transform.rotation = Quaternion.Euler(_cameraRotationX, _cameraRotationY, 0f);
			}
		}
	}

	public void UpdateRotation()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 eulerAngles = localCameraAnchorPoint.eulerAngles;
		_cameraRotationX = eulerAngles.x;
		_cameraRotationY = eulerAngles.y;
	}

	public void SetViewInputVector(Vector2 viewInput)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		_viewInput = viewInput;
	}
}
