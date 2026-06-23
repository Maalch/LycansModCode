using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewMaps.Components;
using Managers;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(6)]
public class PlayerSummonedSpiritComponent : NetworkBehaviour
{
	public static GameObject SpiritCreationPrefab;

	private PlayerSummonedSpiritCameraHandler _cameraHandler;

	public bool Movable = false;

	private Stopwatch _movableWatch = new Stopwatch();

	public bool CanShift = false;

	public PlayerController TooltipTarget = null;

	private GameObject _visual;

	private PlayerCustom _playerCustom;

	private bool _hasFocus = false;

	[Networked(OnChanged = "RefChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe PlayerRef Ref
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Ref. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Ref. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[NetworkedWeaved(1, 1)]
	public unsafe TickTimer AttackCooldown
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.AttackCooldown. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.AttackCooldown. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	[NetworkedWeaved(2, 1)]
	public unsafe TickTimer SpellCooldown
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SpellCooldown. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[2];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SpellCooldown. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 2, value);
		}
	}

	[Networked(OnChanged = "HasFocusChanged")]
	[NetworkedWeaved(3, 1)]
	public unsafe NetworkBool HasFocus
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.HasFocus. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[3];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.HasFocus. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 3, value);
		}
	}

	private void Awake()
	{
		_visual = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		_cameraHandler = ((Component)this).GetComponentInChildren<PlayerSummonedSpiritCameraHandler>();
	}

	public override void Spawned()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Spawned();
		((Behaviour)((Component)this).GetComponentInChildren<Camera>()).enabled = false;
		((Behaviour)((Component)this).GetComponentInChildren<AudioListener>()).enabled = false;
		((Behaviour)_cameraHandler).enabled = false;
		((Behaviour)((Component)this).GetComponent<PlayerSummonedSpiritInputHandler>()).enabled = false;
		_movableWatch.Start();
		AttackCooldown = TickTimer.None;
		SpellCooldown = TickTimer.None;
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		if ((Object)(object)_cameraHandler != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)_cameraHandler).gameObject);
		}
		SetFocus(focus: false);
		if (Ref == PlayerController.Local.Ref)
		{
			OnFocusLost();
		}
		if ((Object)(object)_playerCustom != (Object)null)
		{
			_playerCustom.SummonedSpirit = null;
		}
	}

	public void SetVisible(bool visible)
	{
		_visual.SetActive(visible);
	}

	public void Init(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Ref = playerRef;
	}

	[Preserve]
	public static void RefChanged(Changed<PlayerSummonedSpiritComponent> changed)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (changed.Behaviour.Ref == PlayerRef.None)
		{
			changed.Behaviour._playerCustom = null;
			return;
		}
		changed.Behaviour._playerCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
		switch (changed.Behaviour._playerCustom.PrimaryRolePower)
		{
		case PlayerCustom.PlayerPrimaryRolePower.Ghost:
			((NetworkCharacterControllerPrototypeCustom)((Component)changed.Behaviour).GetComponent<PlayerSummonedSpiritNetworkCharacterController>()).maxSpeed = 1.3f;
			break;
		case PlayerCustom.PlayerPrimaryRolePower.Specter:
			((NetworkCharacterControllerPrototypeCustom)((Component)changed.Behaviour).GetComponent<PlayerSummonedSpiritNetworkCharacterController>()).maxSpeed = 1.8f;
			break;
		}
		PlayerCustom.PlayerNewPrimaryRole newPrimaryRole = changed.Behaviour._playerCustom.NewPrimaryRole;
		PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
		if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist)
		{
			((NetworkCharacterControllerPrototypeCustom)((Component)changed.Behaviour).GetComponent<PlayerSummonedSpiritNetworkCharacterController>()).maxSpeed = 1.4f;
		}
		changed.Behaviour._playerCustom.SummonedSpirit = changed.Behaviour;
		changed.Behaviour.SetFocus(focus: true);
		((Component)changed.Behaviour).gameObject.layer = 25;
		changed.Behaviour._playerCustom.UpdateVisibility();
	}

	public void SetFocus(bool focus)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		HasFocus = NetworkBool.op_Implicit(focus);
	}

	[Preserve]
	public static void HasFocusChanged(Changed<PlayerSummonedSpiritComponent> changed)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		if (changed.Behaviour._hasFocus == NetworkBool.op_Implicit(changed.Behaviour.HasFocus))
		{
			return;
		}
		changed.Behaviour._hasFocus = NetworkBool.op_Implicit(changed.Behaviour.HasFocus);
		if (changed.Behaviour.Ref != PlayerController.Local.Ref)
		{
			return;
		}
		if (NetworkBool.op_Implicit(changed.Behaviour.HasFocus))
		{
			PlayerController.Local.LocalCameraHandler.SwitchPov(PlayerController.Local);
			Quaternion rotation = ((Component)PlayerController.Local.LocalCameraHandler.LocalCamera).transform.rotation;
			float value = Traverse.Create((object)PlayerController.Local.LocalCameraHandler).Field<float>("_cameraRotationX").Value;
			float value2 = Traverse.Create((object)PlayerController.Local.LocalCameraHandler).Field<float>("_cameraRotationY").Value;
			((Behaviour)PlayerController.Local.LocalCameraHandler.LocalCamera).enabled = false;
			((Behaviour)PlayerController.Local.LocalCameraHandler).enabled = false;
			((Behaviour)PlayerController.Local.AudioListener).enabled = false;
			((Behaviour)((Component)PlayerController.Local).GetComponent<CharacterInputHandler>()).enabled = false;
			((Behaviour)((Component)changed.Behaviour).GetComponent<PlayerSummonedSpiritInputHandler>()).enabled = true;
			((Behaviour)changed.Behaviour._cameraHandler).enabled = true;
			((Behaviour)((Component)changed.Behaviour._cameraHandler).GetComponent<Camera>()).enabled = true;
			((Behaviour)((Component)changed.Behaviour._cameraHandler).GetComponent<AudioListener>()).enabled = true;
			((Component)changed.Behaviour._cameraHandler.LocalCamera).transform.rotation = rotation;
			changed.Behaviour._cameraHandler.InitRotation(value, value2);
			PlayerController.Local.ClearRayCast();
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.UpdateVisibility();
			}
		}
		else
		{
			changed.Behaviour.OnFocusLost();
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
		player.UpdateVisibility();
	}

	private void OnFocusLost()
	{
		((Behaviour)PlayerController.Local.LocalCameraHandler.LocalCamera).enabled = true;
		((Behaviour)PlayerController.Local.LocalCameraHandler).enabled = true;
		((Behaviour)PlayerController.Local.AudioListener).enabled = true;
		((Behaviour)((Component)PlayerController.Local).GetComponent<CharacterInputHandler>()).enabled = true;
		PlayerController.Local.LocalCameraHandler.NextPov(true);
	}

	public void Shift()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = default(Vector3);
		((Vector3)(ref position))._002Ector(((Component)this).transform.position.x + ((Component)this).transform.forward.x * 2f, ((Component)this).transform.position.y, ((Component)this).transform.position.z + ((Component)this).transform.forward.z * 2f);
		((Component)this).transform.position = position;
		CanShift = false;
		Movable = false;
		_movableWatch.Restart();
		if (Ref == PlayerController.Local.Ref)
		{
			GameManager.Instance.gameUI.HideInteraction();
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Invalid comparison between Unknown and I4
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Invalid comparison between Unknown and I4
		TickTimer val = AttackCooldown;
		if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
		{
			AttackCooldown = TickTimer.None;
		}
		val = SpellCooldown;
		if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
		{
			SpellCooldown = TickTimer.None;
		}
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val2 = localGameState;
			if ((int)val2 <= 1 || (int)val2 == 5)
			{
				flag = true;
			}
			if (!PlayerRegistry.HasPlayer(Ref))
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	private void LateUpdate()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)((SimulationBehaviour)this).Runner == (Object)null)
		{
			return;
		}
		if (!Movable && _movableWatch.ElapsedMilliseconds >= 200)
		{
			Movable = true;
			_movableWatch.Stop();
		}
		if (!Movable || !(_playerCustom.Ref == PlayerController.Local.Ref))
		{
			return;
		}
		Transform transform = ((Component)_cameraHandler.LocalCamera).transform;
		Vector3 position = transform.position;
		Vector3 forward = transform.forward;
		Ray val = default(Ray);
		((Ray)(ref val))._002Ector(position, forward);
		Ray val2 = default(Ray);
		((Ray)(ref val2))._002Ector(position, forward);
		float num = 2.5f;
		PlayerController local = PlayerController.Local;
		LayerMask value = Traverse.Create((object)((Component)local).GetComponent<PlayerInteract>()).Field<LayerMask>("layerMask").Value;
		RaycastHit val3 = default(RaycastHit);
		if (Physics.Raycast(val, ref val3, num, LayerMask.op_Implicit(value)) && LycansUtility.GameActuallyInPlay)
		{
			GameObject gameObject = ((Component)((RaycastHit)(ref val3)).collider).gameObject;
			if ((Object)(object)gameObject.GetComponent<Door>() != (Object)null || (Object)(object)gameObject.GetComponent<AutodoorCustom>() != (Object)null || (Object)(object)gameObject.GetComponentInParent<AutodoorCustom>() != (Object)null)
			{
				GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_ASTRAL_SHIFT", Color.white, (InputActionName)3, Array.Empty<object>());
				CanShift = true;
				return;
			}
			TickTimer attackCooldown = AttackCooldown;
			if (((TickTimer)(ref attackCooldown)).IsRunning || !((Object)(object)gameObject.GetComponentInParent<PlayerController>() != (Object)null))
			{
				return;
			}
			PlayerController componentInParent = gameObject.GetComponentInParent<PlayerController>();
			switch (_playerCustom.PrimaryRolePower)
			{
			case PlayerCustom.PlayerPrimaryRolePower.Ghost:
				if (NetworkBool.op_Implicit(componentInParent.IsWolf) && !NetworkBool.op_Implicit(componentInParent.IsDead) && !componentInParent.IsStarving())
				{
					GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_SPIRIT_ATTACK", Color.red, (InputActionName)3, Array.Empty<object>());
					TooltipTarget = componentInParent;
				}
				break;
			case PlayerCustom.PlayerPrimaryRolePower.Specter:
				if (!NetworkBool.op_Implicit(componentInParent.IsDead))
				{
					GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_SPECTER_CURSE", Color.red, (InputActionName)3, Array.Empty<object>());
					TooltipTarget = componentInParent;
				}
				break;
			}
			PlayerCustom.PlayerNewPrimaryRole newPrimaryRole = _playerCustom.NewPrimaryRole;
			PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
			if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(componentInParent.Ref);
				if (!NetworkBool.op_Implicit(componentInParent.IsDead) && !NetworkBool.op_Implicit(player.CapturedByCultist))
				{
					GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_CULTIST_CAPTURE", Color.red, (InputActionName)3, Array.Empty<object>());
					TooltipTarget = componentInParent;
				}
			}
		}
		else
		{
			GameManager.Instance.gameUI.HideInteraction();
			CanShift = false;
			TooltipTarget = null;
		}
	}
}
