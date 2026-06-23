using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

public class MechanismObjectTeleportOnDeactivateComponent : MonoBehaviour
{
	private MechanismObject _mechanismObject;

	private GameObject _destinationObject;

	private void Awake()
	{
		_mechanismObject = ((Component)this).GetComponentInParent<MechanismObject>();
		_destinationObject = ((Component)((Component)this).transform.Find("Destination")).gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
		if ((Object)(object)component != (Object)null)
		{
			TickTimer cooldownTimer = _mechanismObject.CooldownTimer;
			if (((TickTimer)(ref cooldownTimer)).IsRunning)
			{
				component.CharacterMovementHandler.TeleportData = new NetworkTeleportData(_destinationObject.transform.position, ((Component)component).transform.rotation, false);
			}
		}
	}
}
