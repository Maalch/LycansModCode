using UnityEngine;

namespace LycansNewRoles.Sabotages;

public class SabotageComponent : MonoBehaviour
{
	public SabotageManager.SabotageIds SabotageId;

	public SabotageObject SabotageObject;

	public void Init(SabotageManager.SabotageIds sabotageId, SabotageObject sabotageObject)
	{
		SabotageId = sabotageId;
		SabotageObject = sabotageObject;
	}
}
