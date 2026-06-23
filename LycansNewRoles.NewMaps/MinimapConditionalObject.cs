using UnityEngine;

namespace LycansNewRoles.NewMaps;

public class MinimapConditionalObject
{
	public enum MinimapConditionType
	{
		PlayerAtOrAboveVerticalPosition,
		PlayerBelowVerticalPosition
	}

	public MinimapConditionType ConditionType;

	public float ConditionValue;

	public GameObject ConditionObject;

	public bool Active;

	public void SetActive(bool active)
	{
		Active = active;
		ConditionObject.SetActive(active);
	}

	public bool IsConditionMet(Vector3 position)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return ConditionType switch
		{
			MinimapConditionType.PlayerBelowVerticalPosition => position.y < ConditionValue, 
			MinimapConditionType.PlayerAtOrAboveVerticalPosition => position.y >= ConditionValue, 
			_ => false, 
		};
	}
}
