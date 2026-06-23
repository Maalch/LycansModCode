using Fusion;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerAstralSpiritInputHandler : MonoBehaviour
{
	private NetworkInputData _networkInputData;

	private PlayerAstralSpiritCameraHandler _localCameraHandler;

	private void Awake()
	{
		_localCameraHandler = ((Component)this).GetComponentInChildren<PlayerAstralSpiritCameraHandler>();
	}

	private void Update()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (!GameManager.Instance.gameUI.IsSettingMenuOpen)
		{
			_localCameraHandler.SetViewInputVector(InputManager.Instance.LookInput);
			_networkInputData.movementInput = InputManager.Instance.MoveInput;
			_networkInputData.aimForwardVector = ((Component)_localCameraHandler).transform.forward;
		}
	}

	public NetworkInputData GetNetworkInput()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return _networkInputData;
	}

	public void ClearInputs()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		_networkInputData.buttons = default(NetworkButtons);
	}
}
