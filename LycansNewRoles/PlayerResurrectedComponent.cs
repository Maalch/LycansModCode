using System.Diagnostics;
using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerResurrectedComponent : MonoBehaviour
{
	public static GameObject ResurrectedEffectPrefab;

	private GameObject _resurrectedObject;

	private PlayerController _playerController;

	private bool _active = false;

	private Stopwatch _stopwatch = new Stopwatch();

	private float IntervalMilliseconds = 4500f;

	private void Awake()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		_playerController = ((Component)this).GetComponent<PlayerController>();
		_resurrectedObject = Object.Instantiate<GameObject>(ResurrectedEffectPrefab, ((Component)_playerController).transform);
		_resurrectedObject.SetActive(true);
		_resurrectedObject.transform.position = new Vector3(_resurrectedObject.transform.position.x, _resurrectedObject.transform.position.y, _resurrectedObject.transform.position.z);
		_resurrectedObject.GetComponent<Light>().renderingLayerMask = LayerMask.NameToLayer("NoInteract");
		_resurrectedObject.SetActive(false);
	}

	public void UpdateState()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(_playerController.Ref);
		player.UpdateSkinColor();
		_active = !NetworkBool.op_Implicit(_playerController.IsDead) && (NetworkBool.op_Implicit(player.Resurrected) || player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie);
		_resurrectedObject.SetActive(_active);
		if (_active && player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
		{
			_stopwatch.Restart();
		}
		else
		{
			_stopwatch.Stop();
		}
	}

	private void Update()
	{
		if ((float)_stopwatch.ElapsedMilliseconds >= IntervalMilliseconds && _active)
		{
			AudioManager.PlayAndFollow("Zombie", ((Component)_playerController).transform, (MixerTarget)2, 22f, 0.2f);
			_stopwatch.Restart();
		}
	}
}
