using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerSpotterLightComponent : MonoBehaviour
{
	public static GameObject SpotterLightEffectPrefab;

	private GameObject _spotterLightObject;

	private List<Light> _lights;

	private PlayerController _playerController;

	private bool _active = false;

	private void Awake()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		_playerController = ((Component)this).GetComponent<PlayerController>();
		_spotterLightObject = Object.Instantiate<GameObject>(SpotterLightEffectPrefab, ((Component)_playerController).transform);
		_spotterLightObject.SetActive(true);
		_spotterLightObject.transform.position = new Vector3(_spotterLightObject.transform.position.x, _spotterLightObject.transform.position.y, _spotterLightObject.transform.position.z);
		_lights = _spotterLightObject.GetComponentsInChildren<Light>().ToList();
		foreach (Light light in _lights)
		{
			light.renderingLayerMask = LayerMask.NameToLayer("NoInteract");
		}
		_spotterLightObject.SetActive(false);
	}

	public void UpdateState()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer == (Object)null)
		{
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		_active = !NetworkBool.op_Implicit(_playerController.IsDead) && NetworkBool.op_Implicit(_playerController.IsWolf) && player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Spotter;
		if (_active)
		{
			foreach (Light light in _lights)
			{
				light.intensity = 4f;
				light.range = 250f;
			}
		}
		_spotterLightObject.SetActive(_active);
	}
}
