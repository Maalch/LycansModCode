using System;
using System.Runtime.CompilerServices;
using BepInEx.Logging;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(6)]
public class PlayerPetComponent : NetworkBehaviour
{
	private GameObject _object;

	private GameObject _visual;

	private PlayerCustom _ownerCustom;

	private NetworkCharacterControllerPrototype _networkCharacterController;

	private Shader _defaultShader;

	public int PetIndex;

	public const float MaximumMovementSpeed = 5f;

	public const float DistanceToPlayer = 0.8f;

	public const float DistanceForMaxMovementSpeed = 6f;

	public const float DistanceToPlayerToTeleport = 5f;

	public const float MinimumSpeedRatioForMoveAnimation = 0.075f;

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
				throw new InvalidOperationException("Error when accessing PlayerPetComponent.Ref. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerPetComponent.Ref. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "MovingChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool Moving
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerPetComponent.Moving. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerPetComponent.Moving. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	private void Awake()
	{
		_object = ((Component)((Component)this).transform.Find("Pet")).gameObject;
		_visual = ((Component)_object.transform.Find("Visual")).gameObject;
		_networkCharacterController = ((Component)this).GetComponent<NetworkCharacterControllerPrototype>();
		_defaultShader = ((Renderer)_visual.GetComponent<SkinnedMeshRenderer>()).material.shader;
		_visual.SetActive(false);
	}

	public override void Spawned()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Moving = NetworkBool.op_Implicit(false);
		Ref = PlayerRef.None;
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		if ((Object)(object)_ownerCustom != (Object)null)
		{
			_ownerCustom.CurrentPet = null;
		}
		((NetworkBehaviour)this).Despawned(runner, hasState);
	}

	public void UpdateVisibility()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Invalid comparison between Unknown and I4
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if ((Object)(object)_ownerCustom == (Object)null || NetworkBool.op_Implicit(_ownerCustom.PlayerController.IsDead) || NetworkBool.op_Implicit(_ownerCustom.PlayerController.IsWolf) || NetworkBool.op_Implicit(_ownerCustom.Dying) || LycansUtility.WolvesCanTransform || (!_ownerCustom.Visible && !_ownerCustom.IsCurrentlyPlayedOrObserved) || NetworkBool.op_Implicit(player.PlayerController.PlayerEffectManager.Paranoia) || (ExtraSettings.Instance.HidePets && (int)GameManager.LocalGameState != 1))
		{
			_visual.SetActive(false);
		}
		else if (_ownerCustom.CamouflageLevelForPovPlayer > 0 || _ownerCustom.PlayerController.MovementAction == 1)
		{
			_visual.SetActive(true);
			((Renderer)_visual.GetComponent<SkinnedMeshRenderer>()).material.shader = PlayerCustom.CamouflageLevel2Shader;
		}
		else
		{
			_visual.SetActive(true);
			((Renderer)_visual.GetComponent<SkinnedMeshRenderer>()).material.shader = _defaultShader;
		}
	}

	public void Init(PlayerRef playerRef, int petIndex)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Ref = playerRef;
		PetIndex = petIndex;
	}

	[Preserve]
	public static void RefChanged(Changed<PlayerPetComponent> changed)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (changed.Behaviour.Ref == PlayerRef.None)
		{
			changed.Behaviour._ownerCustom = null;
			return;
		}
		changed.Behaviour._ownerCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
		((Component)changed.Behaviour).gameObject.layer = 27;
		changed.Behaviour._ownerCustom.CurrentPet = changed.Behaviour;
		changed.Behaviour.UpdateVisibility();
	}

	[Preserve]
	public static void MovingChanged(Changed<PlayerPetComponent> changed)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)changed.Behaviour._object == (Object)null)
		{
			Plugin.Logger.LogError((object)("Pet has no object!!! Owner: " + ((object)changed.Behaviour.Ref/*cast due to constrained. prefix*/).ToString()));
		}
		else if ((Object)(object)changed.Behaviour._object.GetComponent<Animator>() == (Object)null)
		{
			Plugin.Logger.LogError((object)("Pet has no animator!!! Owner: " + ((object)changed.Behaviour.Ref/*cast due to constrained. prefix*/).ToString()));
			if (PlayerCustomRegistry.HasPlayer(changed.Behaviour.Ref))
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
				ManualLogSource logger = Plugin.Logger;
				NetworkString<_32> username = player.PlayerController.PlayerData.Username;
				logger.LogError((object)("Player name: " + ((object)username/*cast due to constrained. prefix*/).ToString() + ", pet index: " + player.PetIndex));
			}
		}
		else
		{
			changed.Behaviour._object.GetComponent<Animator>().SetBool("moving", NetworkBool.op_Implicit(changed.Behaviour.Moving));
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).Runner.IsServer)
		{
			return;
		}
		if (Ref != PlayerRef.None && (Object)(object)_ownerCustom == (Object)null)
		{
			if (!PlayerCustomRegistry.HasPlayer(Ref))
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				return;
			}
			_ownerCustom = PlayerCustomRegistry.GetPlayer(Ref);
		}
		Vector3 val = default(Vector3);
		((Vector3)(ref val))._002Ector(((Component)_ownerCustom.PlayerController).transform.forward.x, 0f, ((Component)_ownerCustom.PlayerController).transform.forward.z);
		Vector3 val2 = ((Component)_ownerCustom.PlayerController).transform.position - val * 0.8f;
		float num = Vector3.Distance(((Component)this).transform.position, val2);
		if (num > 5f)
		{
			((NetworkTransform)_networkCharacterController).TeleportToPosition(val2, (Vector3?)null, true);
			_networkCharacterController.Velocity = Vector3.zero;
		}
		else if (num > 0.5f)
		{
			Vector3 val3 = val2 - ((Component)this).transform.position;
			val3.y = 0f;
			((Vector3)(ref val3)).Normalize();
			float num2 = Mathf.InverseLerp(0f, 6f, num);
			float num3 = Mathf.Lerp(0.1f, 1f, num2);
			float num4 = 5f * num3;
			Vector3 val4 = val3 * num4;
			_networkCharacterController.Move(val4);
			Moving = NetworkBool.op_Implicit(num3 >= 0.075f);
		}
		else
		{
			Moving = NetworkBool.op_Implicit(false);
		}
		Vector3 val5 = ((Component)_ownerCustom.PlayerController).transform.position - ((Component)this).transform.position;
		val5.y = 0f;
		if (((Vector3)(ref val5)).sqrMagnitude > 0.001f)
		{
			Quaternion val6 = Quaternion.LookRotation(val5);
			((Component)this).transform.rotation = Quaternion.Slerp(((Component)this).transform.rotation, val6, 100f * ((SimulationBehaviour)this).Runner.DeltaTime);
		}
	}
}
