using Fusion;
using LycansNewRoles.Sabotages;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

public class AutodoorDetection : MonoBehaviour
{
	private AutodoorCustom _autodoor;

	private void Awake()
	{
		_autodoor = ((Component)this).GetComponentInParent<AutodoorCustom>();
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
		if ((Object)(object)component != (Object)null && (!SabotageManager.Instance.IsSabotageActive(SabotageManager.SabotageIds.LaboratoryAutodoors) || NetworkBool.op_Implicit(component.IsWolf)))
		{
			_autodoor.AddPlayerInDetection(component.Ref);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
		if ((Object)(object)component != (Object)null)
		{
			_autodoor.RemovePlayerFromDetection(component.Ref);
		}
	}
}
