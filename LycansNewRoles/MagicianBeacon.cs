using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(4)]
public class MagicianBeacon : NetworkBehaviour
{
	private PlayerCustom _creatorCustom;

	private GameObject _effect;

	public static List<MagicianBeacon> AssociatedBeacons = new List<MagicianBeacon>();

	[Networked(OnChanged = "CreatorRefChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe PlayerRef CreatorRef
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianBeacon.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianBeacon.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	public void Awake()
	{
		_effect = ((Component)((Component)this).transform.Find("MagicianBeaconEffect")).gameObject;
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<MagicianBeacon> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		MagicianBeacon behaviour = changed.Behaviour;
		behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(behaviour.CreatorRef);
		if (behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
		{
			AssociatedBeacons.Add(behaviour);
			behaviour._creatorCustom.UpdateDescriptionStatusIfNeeded();
		}
		behaviour.UpdateVisibility();
	}

	private void UpdateVisibility()
	{
		if (!((Object)(object)((SimulationBehaviour)this).Runner == (Object)null))
		{
			_effect.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved);
		}
	}

	public static void UpdateVisibilityForAllBeacons()
	{
		MagicianBeacon[] array = Object.FindObjectsOfType<MagicianBeacon>();
		MagicianBeacon[] array2 = array;
		foreach (MagicianBeacon magicianBeacon in array2)
		{
			magicianBeacon.UpdateVisibility();
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority && (Object)(object)_creatorCustom != (Object)null)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || (int)val == 5)
			{
				flag = true;
			}
			if (!PlayerRegistry.HasPlayer(CreatorRef) || NetworkBool.op_Implicit(_creatorCustom.PlayerController.IsDead))
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		if (_creatorCustom.IsCurrentlyPlayedOrObserved)
		{
			_creatorCustom.UpdateDescriptionStatusIfNeeded();
		}
		((NetworkBehaviour)this).Despawned(runner, hasState);
		AssociatedBeacons.Remove(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void CreateIllusion()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectMagicianIllusionName");
		NetworkObject val = ((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)((Component)this).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			((Component)no).transform.position = ((Component)this).transform.position;
		}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		((Component)val).transform.position = ((Component)this).transform.position;
		((Component)val).transform.forward = ((Component)this).transform.forward;
		((Component)val).GetComponent<MagicianIllusion>().Init(7);
		((Component)val).GetComponent<MagicianIllusion>().SetCreatorRef(CreatorRef);
		((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
	}
}
