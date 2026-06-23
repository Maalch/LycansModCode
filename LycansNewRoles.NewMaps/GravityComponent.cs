using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewMaps;

public class GravityComponent : MonoBehaviour
{
	private CharacterMovementHandler _characterMovementHandler;

	private NetworkCharacterControllerPrototypeCustom _networkCharacterControllerPrototypeCustom;

	private PlayerController _playerController;

	private float? _initialY = null;

	private void Awake()
	{
		_characterMovementHandler = ((Component)this).GetComponent<CharacterMovementHandler>();
		_networkCharacterControllerPrototypeCustom = Traverse.Create((object)_characterMovementHandler).Field<NetworkCharacterControllerPrototypeCustom>("_networkCharacterControllerPrototypeCustom").Value;
		_playerController = ((Component)this).GetComponent<PlayerController>();
	}

	public void ResetGrounded()
	{
		_initialY = null;
	}

	private void Update()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Invalid comparison between Unknown and I4
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		bool isGrounded = _networkCharacterControllerPrototypeCustom.IsGrounded;
		if (!isGrounded && !_initialY.HasValue)
		{
			_initialY = ((Component)_playerController).transform.position.y;
		}
		if (!isGrounded || !_initialY.HasValue)
		{
			return;
		}
		float num = _initialY.Value - ((Component)_playerController).transform.position.y;
		num *= BalancingValues.FallingHeightMultiplierForDamage(GameManager.Instance.MapID);
		if ((int)GameManager.LocalGameState == 2 && num > 3f)
		{
			float num2 = (float)GameManager.Instance.MaxHunger * (num * 0.05f);
			PlayerController value = Traverse.Create((object)_characterMovementHandler).Field<PlayerController>("_playerController").Value;
			value.Hunger = Math.Max(value.Hunger - num2, 0f);
			if (value.Hunger <= 0f)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(value.Ref);
				player.Stats.UpdateDeathType("FALL");
				value.Rpc_Kill(PlayerRef.None);
			}
		}
		_initialY = null;
	}
}
