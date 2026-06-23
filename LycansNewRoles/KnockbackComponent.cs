using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class KnockbackComponent : MonoBehaviour
{
	private Vector3? _knockback;

	private Vector3 _knockbackReductionPerSecond;

	public Vector3? Knockback => _knockback;

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
		_knockback = val;
		float num = Mathf.Abs(val.x) + Mathf.Abs(val.y) + Mathf.Abs(val.z);
		float num2 = Mathf.Abs(val.x) / num;
		float num3 = Mathf.Abs(val.y) / num;
		float num4 = Mathf.Abs(val.z) / num;
		_knockbackReductionPerSecond = new Vector3(reductionPerSecond * num2, reductionPerSecond * num3, reductionPerSecond * num4);
		PlayerController component = ((Component)this).GetComponent<PlayerController>();
		PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)component).Runner, component.Index, 9);
	}

	public void Stop()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		_knockback = Vector3.zero;
	}

	public void Update()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		if (!_knockback.HasValue)
		{
			return;
		}
		if (!LycansUtility.GameActuallyInPlay)
		{
			_knockback = null;
			return;
		}
		float num = 0f;
		if (_knockback.Value.x > 0f)
		{
			num = Mathf.Max(0f, _knockback.Value.x - _knockbackReductionPerSecond.x * Time.deltaTime);
		}
		else if (_knockback.Value.x < 0f)
		{
			num = Mathf.Min(0f, _knockback.Value.x + _knockbackReductionPerSecond.x * Time.deltaTime);
		}
		float num2 = _knockback.Value.y;
		if (num2 > 0f && _knockbackReductionPerSecond.y > 0f)
		{
			num2 = Mathf.Max(0f, _knockback.Value.y - _knockbackReductionPerSecond.y * Time.deltaTime);
		}
		float num3 = 0f;
		if (_knockback.Value.z > 0f)
		{
			num3 = Mathf.Max(0f, _knockback.Value.z - _knockbackReductionPerSecond.z * Time.deltaTime);
		}
		else if (_knockback.Value.z < 0f)
		{
			num3 = Mathf.Min(0f, _knockback.Value.z + _knockbackReductionPerSecond.z * Time.deltaTime);
		}
		if (num == 0f && num2 == 0f && num3 == 0f)
		{
			_knockback = null;
		}
		else
		{
			_knockback = new Vector3(num, num2, num3);
		}
	}
}
