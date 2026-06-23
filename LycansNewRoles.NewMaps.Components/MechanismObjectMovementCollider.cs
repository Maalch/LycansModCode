using LycansNewRoles.NewItems;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

public class MechanismObjectMovementCollider : MonoBehaviour
{
	private MechanismObject _mechanismObject;

	private void Awake()
	{
		_mechanismObject = ((Component)this).GetComponentInParent<MechanismObject>();
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
		if ((Object)(object)component != (Object)null)
		{
			_mechanismObject.AddPlayerInDetection(component.Ref);
		}
		else if ((Object)(object)((Component)other).gameObject.GetComponent<Item>() != (Object)null || (Object)(object)((Component)other).gameObject.GetComponent<Trap>() != (Object)null || (Object)(object)((Component)other).gameObject.GetComponent<GrenadeActive>() != (Object)null)
		{
			_mechanismObject.AddObjectInDetection(((Component)other).gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
		if ((Object)(object)component != (Object)null)
		{
			_mechanismObject.RemovePlayerFromDetection(component.Ref);
		}
		else if ((Object)(object)((Component)other).gameObject.GetComponent<Item>() != (Object)null || (Object)(object)((Component)other).gameObject.GetComponent<Trap>() != (Object)null || (Object)(object)((Component)other).gameObject.GetComponent<GrenadeActive>() != (Object)null)
		{
			_mechanismObject.RemoveObjectFromDetection(((Component)other).gameObject);
		}
	}
}
