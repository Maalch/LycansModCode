using UnityEngine;

namespace LycansNewRoles;

public class ForcedRotationComponent : MonoBehaviour
{
	private PlayerCustom _playerCustom;

	private Vector3? _forcedRotation;

	private Vector3 _forcedRotationReductionPerSecond;

	public Vector3? ForcedRotation => _forcedRotation;

	public void SetPlayer(PlayerCustom playerCustom)
	{
		_playerCustom = playerCustom;
	}

	public void Init(Vector3 direction, float power, float reductionPerSecond)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = new Vector3(direction.x, direction.y, direction.z) * power;
		_forcedRotation = val;
		float num = Mathf.Abs(val.x) + Mathf.Abs(val.y) + Mathf.Abs(val.z);
		float num2 = Mathf.Abs(val.x) / num;
		float num3 = Mathf.Abs(val.y) / num;
		float num4 = Mathf.Abs(val.z) / num;
		_forcedRotationReductionPerSecond = new Vector3(reductionPerSecond * num2, reductionPerSecond * num3, reductionPerSecond * num4);
	}

	public void Stop()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		_forcedRotation = Vector3.zero;
	}

	public void Update()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		if (!_forcedRotation.HasValue)
		{
			return;
		}
		if (!LycansUtility.GameActuallyInPlay)
		{
			_forcedRotation = null;
			_playerCustom.ForcedRotationY = 0f;
			return;
		}
		float num = 0f;
		if (_forcedRotation.Value.x > 0f)
		{
			num = Mathf.Max(0f, _forcedRotation.Value.x - _forcedRotationReductionPerSecond.x * Time.deltaTime);
		}
		else if (_forcedRotation.Value.x < 0f)
		{
			num = Mathf.Min(0f, _forcedRotation.Value.x + _forcedRotationReductionPerSecond.x * Time.deltaTime);
		}
		float num2 = 0f;
		if (_forcedRotation.Value.y > 0f)
		{
			num2 = Mathf.Max(0f, _forcedRotation.Value.y - _forcedRotationReductionPerSecond.y * Time.deltaTime);
		}
		else if (_forcedRotation.Value.y < 0f)
		{
			num2 = Mathf.Min(0f, _forcedRotation.Value.y + _forcedRotationReductionPerSecond.y * Time.deltaTime);
		}
		float num3 = 0f;
		if (_forcedRotation.Value.z > 0f)
		{
			num3 = Mathf.Max(0f, _forcedRotation.Value.z - _forcedRotationReductionPerSecond.z * Time.deltaTime);
		}
		else if (_forcedRotation.Value.z < 0f)
		{
			num3 = Mathf.Min(0f, _forcedRotation.Value.z + _forcedRotationReductionPerSecond.z * Time.deltaTime);
		}
		if (num == 0f && num2 == 0f && num3 == 0f)
		{
			_forcedRotation = null;
			_playerCustom.ForcedRotationY = 0f;
		}
		else
		{
			_forcedRotation = new Vector3(num, num2, num3);
			_playerCustom.ForcedRotationY = _forcedRotation.Value.y;
		}
	}
}
