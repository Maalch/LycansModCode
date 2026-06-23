using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

public class LadderCustom : MonoBehaviour
{
	public int MapID;

	private void Start()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)GameManager.Instance == (Object)null)
			{
				return;
			}
			float? num = null;
			IEnumerable<GameObject> enumerable = from o in Object.FindObjectsOfType<GameObject>()
				where ((Object)o).name == "Ladders"
				select o;
			Vector3 val = default(Vector3);
			((Vector3)(ref val))._002Ector(((Component)this).transform.position.x, ((Component)this).transform.position.y, ((Component)this).transform.position.z);
			Transform parent = null;
			foreach (GameObject item in enumerable)
			{
				for (int num2 = 0; num2 < item.transform.childCount; num2++)
				{
					Transform child = item.transform.GetChild(num2);
					float num3 = Vector3.Distance(val, ((Component)child).transform.position);
					if (!num.HasValue || num3 < num.Value)
					{
						num = num3;
						parent = child;
					}
				}
			}
			((Component)this).gameObject.transform.parent = parent;
			MapManager.RescaleSpawnedObject(((Component)this).gameObject, ((Component)((Component)this).transform.parent).gameObject, MapManager.NewMapsByIdInfo[GameManager.Instance.MapID]);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("LadderCustom start error: " + ex));
		}
	}
}
