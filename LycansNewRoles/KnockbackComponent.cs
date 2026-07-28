using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class KnockbackComponent : MonoBehaviour
{
	private Vector3? _knockback;

	private Vector3 _knockbackReductionPerSecond;

	public Vector3? Knockback => _knockback;

	public void Init(Vector3 direction, float power, float reductionPerSecond, int animationIndex = 9)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Init(direction, power, new Vector3(reductionPerSecond, reductionPerSecond, reductionPerSecond));
	}

	public void Init(Vector3 direction, float power, Vector3 reductionPerSecond, int animationIndex = 9)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = direction * power;
		_knockback = val;
		float num = Mathf.Abs(val.x) + Mathf.Abs(val.y) + Mathf.Abs(val.z);
		float num2 = Mathf.Abs(val.x) / num;
		float num3 = Mathf.Abs(val.y) / num;
		float num4 = Mathf.Abs(val.z) / num;
		_knockbackReductionPerSecond = new Vector3(reductionPerSecond.x * num2, reductionPerSecond.y * num3, reductionPerSecond.z * num4);
		PlayerController component = ((Component)this).GetComponent<PlayerController>();
		PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)component).Runner, component.Index, animationIndex);
	}

	public void StopKnockback()
	{
		_knockback = null;
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
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
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
		if (Mathf.Abs(num) <= 0.1f && Mathf.Abs(num2) <= 0.1f && Mathf.Abs(num3) <= 0.1f)
		{
			StopKnockback();
		}
		else
		{
			_knockback = new Vector3(num, num2, num3);
		}
	}
}
