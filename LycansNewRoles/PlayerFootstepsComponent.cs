using System;
using System.Diagnostics;
using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerFootstepsComponent : MonoBehaviour
{
	private PlayerController _player;

	private Stopwatch Stopwatch = new Stopwatch();

	private void Awake()
	{
		_player = ((Component)this).GetComponent<PlayerController>();
		Stopwatch.Start();
	}

	private void Update()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!NetworkBool.op_Implicit(_player.IsMoving) || (int)GameManager.LocalGameState != 2)
			{
				return;
			}
			int num = 1000;
			switch (_player.MovementAction)
			{
			case 0:
				num = 1000;
				break;
			case 1:
				num = 2000;
				break;
			case 2:
				num = 500;
				break;
			}
			if (Stopwatch.ElapsedMilliseconds >= num)
			{
				Vector3 val = default(Vector3);
				((Vector3)(ref val))._002Ector(((Component)_player).transform.position.x, ((Component)_player).transform.position.y + 0.1f, ((Component)_player).transform.position.z);
				if (NetworkBool.op_Implicit(_player.IsWolf))
				{
					GameObject val2 = Object.Instantiate<GameObject>(FootstepComponent.FootstepWolfPrefab, val, ((Component)_player).transform.rotation);
					val2.SetActive(true);
					FootstepComponent footstepComponent = val2.AddComponent<FootstepComponent>();
					footstepComponent.Init(_player.Ref, FootstepComponent.FootstepType.Wolf);
				}
				else
				{
					GameObject val2 = Object.Instantiate<GameObject>(FootstepComponent.FootstepVillagerPrefab, val, ((Component)_player).transform.rotation);
					val2.SetActive(true);
					FootstepComponent footstepComponent2 = val2.AddComponent<FootstepComponent>();
					footstepComponent2.Init(_player.Ref, FootstepComponent.FootstepType.Villager);
				}
				Stopwatch.Restart();
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PlayerFootstepsComponent error: " + ex));
		}
	}
}
