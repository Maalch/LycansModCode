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

[NetworkBehaviourWeaved(3)]
public class PlayerAstralSpiritComponent : NetworkBehaviour
{
	private PlayerAstralSpiritCameraHandler _cameraHandler;

	public bool Movable = false;

	private Stopwatch _movableWatch = new Stopwatch();

	public bool CanShift = false;

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

	public override void Spawned()
	{
		((NetworkBehaviour)this).Spawned();
		((Behaviour)((Component)this).GetComponentInChildren<Camera>()).enabled = false;
		((Behaviour)((Component)this).GetComponentInChildren<AudioListener>()).enabled = false;
		_cameraHandler = ((Component)this).GetComponentInChildren<PlayerAstralSpiritCameraHandler>();
		((Behaviour)_cameraHandler).enabled = false;
		((Behaviour)((Component)this).GetComponent<PlayerAstralSpiritInputHandler>()).enabled = false;
		_movableWatch.Start();
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		if ((Object)(object)_cameraHandler != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)_cameraHandler).gameObject);
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(Ref);
		if ((Object)(object)player != (Object)null)
		{
			player.AstralSpirit = null;
		}
		if (Ref == PlayerController.Local.Ref)
		{
			((Behaviour)PlayerController.Local.LocalCameraHandler.LocalCamera).enabled = true;
			((Behaviour)PlayerController.Local.LocalCameraHandler).enabled = true;
			((Behaviour)((Component)PlayerController.Local).GetComponent<CharacterInputHandler>()).enabled = true;
			PlayerCustom.Local.UpdateVisibility();
		}
	}

	[Preserve]
	public static void RefChanged(Changed<PlayerAstralSpiritComponent> changed)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		if (!(changed.Behaviour.Ref == PlayerRef.None))
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
			player.AstralSpirit = changed.Behaviour;
			((Component)changed.Behaviour).transform.position = ((Component)player.PlayerController).transform.position;
			if (changed.Behaviour.Ref == PlayerController.Local.Ref)
			{
				Quaternion rotation = ((Component)player.PlayerController.LocalCameraHandler.LocalCamera).transform.rotation;
				float value = Traverse.Create((object)player.PlayerController.LocalCameraHandler).Field<float>("_cameraRotationX").Value;
				float value2 = Traverse.Create((object)player.PlayerController.LocalCameraHandler).Field<float>("_cameraRotationY").Value;
				((Behaviour)PlayerController.Local.LocalCameraHandler.LocalCamera).enabled = false;
				((Behaviour)PlayerController.Local.LocalCameraHandler).enabled = false;
				((Behaviour)((Component)PlayerController.Local).GetComponent<CharacterInputHandler>()).enabled = false;
				((Behaviour)((Component)changed.Behaviour).GetComponent<PlayerAstralSpiritInputHandler>()).enabled = true;
				((Behaviour)((Component)changed.Behaviour).GetComponentInChildren<PlayerAstralSpiritCameraHandler>()).enabled = true;
				((Behaviour)((Component)changed.Behaviour).GetComponentInChildren<Camera>()).enabled = true;
				((Behaviour)((Component)changed.Behaviour).GetComponentInChildren<AudioListener>()).enabled = true;
				((Component)((Component)changed.Behaviour).GetComponentInChildren<PlayerAstralSpiritCameraHandler>().LocalCamera).transform.rotation = rotation;
				((Component)changed.Behaviour).GetComponentInChildren<PlayerAstralSpiritCameraHandler>().InitRotation(value, value2);
				PlayerCustom.Local.UpdateVisibility();
			}
			((Component)changed.Behaviour).gameObject.layer = 25;
		}
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

	private void LateUpdate()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		if (!Movable && _movableWatch.ElapsedMilliseconds >= 200)
		{
			Movable = true;
			_movableWatch.Stop();
		}
		if (!Movable || !(Ref == PlayerController.Local.Ref))
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
		float num = 1.25f;
		PlayerController local = PlayerController.Local;
		LayerMask value = Traverse.Create((object)((Component)local).GetComponent<PlayerInteract>()).Field<LayerMask>("layerMask").Value;
		RaycastHit val3 = default(RaycastHit);
		if (Physics.Raycast(val, ref val3, num, LayerMask.op_Implicit(value)))
		{
			GameObject gameObject = ((Component)((RaycastHit)(ref val3)).collider).gameObject;
			if ((Object)(object)gameObject.GetComponent<Door>() != (Object)null || (Object)(object)gameObject.GetComponent<AutodoorCustom>() != (Object)null || (Object)(object)gameObject.GetComponentInParent<AutodoorCustom>() != (Object)null)
			{
				GameManager.Instance.gameUI.UpdateInteraction("NALES_UI_ACTION_ASTRAL_SHIFT", Color.white, (InputActionName)3, Array.Empty<object>());
				CanShift = true;
			}
		}
		else
		{
			GameManager.Instance.gameUI.HideInteraction();
			CanShift = false;
		}
	}
}
