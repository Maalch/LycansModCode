using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.Sabotages;

[NetworkBehaviourWeaved(20)]
public class SabotageObject : NetworkBehaviour
{
	public GameObject TargetObject { get; private set; }

	public float ActivationDuration { get; private set; }

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe int SabotageId
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageId. Networked properties can only be accessed when Spawned() has been called.");
			}
			return *base.Ptr;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageId. Networked properties can only be accessed when Spawned() has been called.");
			}
			*base.Ptr = value;
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe int SabotageObjectIndex
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageObjectIndex. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageObjectIndex. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe NetworkBool Completed
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageObject.Completed. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[2];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageObject.Completed. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 2, value);
		}
	}

	public void Init(int index, SabotageManager.SabotageIds sabotageId, float activationDuration)
	{
		SabotageObjectIndex = index;
		SabotageId = (int)sabotageId;
		ActivationDuration = activationDuration;
	}

	public override void Spawned()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Spawned();
		SabotageManager.SabotageIds sabotageId = (SabotageManager.SabotageIds)SabotageId;
		SabotageManager.Instance.UpdateCachedObjectsIfNeeded();
		float minDistance = SabotageManager.CachedObjects.Min((GameObject o) => Vector3.Distance(o.transform.position, ((Component)this).transform.position));
		TargetObject = SabotageManager.CachedObjects.First((GameObject o) => Vector3.Distance(o.transform.position, ((Component)this).transform.position) == minDistance);
		TargetObject.gameObject.AddComponent<SabotageComponent>().Init(sabotageId, this);
		Plugin.Minimap.AddSabotageIcon(TargetObject, SabotageManager.GetSabotageColor(sabotageId));
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		if ((Object)(object)TargetObject != (Object)null)
		{
			SabotageComponent component = TargetObject.gameObject.GetComponent<SabotageComponent>();
			if ((Object)(object)component != (Object)null)
			{
				Object.Destroy((Object)(object)component);
			}
			Plugin.Minimap.RemoveSabotageIcon(TargetObject);
		}
		((NetworkBehaviour)this).Despawned(runner, hasState);
	}
}
