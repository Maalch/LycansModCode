using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles;

public class KnockbackComponent : MonoBehaviour
{
	private Vector3? _knockback;

	private Vector3 _knockbackReductionPerSecond;

	private PlayerController _playerController;

	public Vector3? Knockback => _knockback;

	private void Awake()
	{
		_playerController = ((Component)this).GetComponent<PlayerController>();
	}

	public void Init(Vector3 direction, float power, float reductionPerSecond, int animationIndex = 9, bool heavyGravity = true)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Init(direction, power, new Vector3(reductionPerSecond, reductionPerSecond, reductionPerSecond), animationIndex, heavyGravity);
	}

	public void Init(Vector3 direction, float power, Vector3 reductionPerSecond, int animationIndex = 9, bool heavyGravity = true)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (!_knockback.HasValue)
		{
			Vector3 val = direction * power;
			_knockback = val;
			float num = Mathf.Abs(val.x) + Mathf.Abs(val.y) + Mathf.Abs(val.z);
			float num2 = Mathf.Abs(val.x) / num;
			float num3 = Mathf.Abs(val.y) / num;
			float num4 = Mathf.Abs(val.z) / num;
			_knockbackReductionPerSecond = new Vector3(reductionPerSecond.x * num2, reductionPerSecond.y * num3, reductionPerSecond.z * num4);
			PlayerCustom.Rpc_Play_Animation(((SimulationBehaviour)_playerController).Runner, _playerController.Index, animationIndex);
			if (heavyGravity)
			{
				Traverse.Create((object)_playerController.CharacterMovementHandler).Field<NetworkCharacterControllerPrototypeCustom>("_networkCharacterControllerPrototypeCustom").Value.gravity = -400f * BalancingValues.GravityMultiplier(GameManager.Instance.MapID);
			}
		}
	}

	public void StopKnockback()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		_knockback = null;
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(_playerController.Ref);
		player.ResetGravity();
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
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
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
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(_playerController.Ref);
		if (!NetworkBool.op_Implicit(player.Jump) || !Traverse.Create((object)_playerController.CharacterMovementHandler).Field<NetworkCharacterControllerPrototypeCustom>("_networkCharacterControllerPrototypeCustom").Value.Controller.isGrounded)
		{
			return;
		}
		Effect val = _playerController.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is JumpEffect);
		if ((Object)(object)val != (Object)null)
		{
			CustomTickTimer effectTimer = val.EffectTimer;
			if (((CustomTickTimer)(ref effectTimer)).NormalizedValue(((SimulationBehaviour)_playerController).Runner) >= 0.25f)
			{
				_playerController.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
			}
		}
	}
}
