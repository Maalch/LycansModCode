using System.Diagnostics;
using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerPredatorComponent : MonoBehaviour
{
	private PlayerCustom _playerCustom;

	private Stopwatch _millisecondsSinceNextCheck = new Stopwatch();

	private void Start()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		_playerCustom = PlayerCustomRegistry.GetPlayer(((Component)this).GetComponent<PlayerController>().Ref);
		_millisecondsSinceNextCheck.Restart();
	}

	private void Update()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		if (_millisecondsSinceNextCheck.ElapsedMilliseconds < 1500)
		{
			return;
		}
		if (_playerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Predator)
		{
			PlayerRef primaryRoleTargetRef = _playerCustom.PrimaryRoleTargetRef;
			if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && NetworkBool.op_Implicit(_playerCustom.PlayerController.IsWolf))
			{
				PlayerController player = PlayerRegistry.GetPlayer(_playerCustom.PrimaryRoleTargetRef);
				float num = Vector3.Distance(((Component)_playerCustom.PlayerController).transform.position, ((Component)player).transform.position);
				if (num <= 20f && !NetworkBool.op_Implicit(player.IsDead))
				{
					AudioManager.PlayAndFollow("BeastHeartBeatMid", ((Component)player).transform, (MixerTarget)2, 20f, 1f);
				}
			}
		}
		_millisecondsSinceNextCheck.Restart();
	}
}
