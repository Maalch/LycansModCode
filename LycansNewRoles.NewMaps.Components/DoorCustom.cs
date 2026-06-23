using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

public class DoorCustom : MonoBehaviour
{
	public int MapID;

	private void Start()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		try
		{
			if ((Object)(object)GameManager.Instance == (Object)null)
			{
				return;
			}
			float? num = null;
			IEnumerable<GameObject> enumerable = from o in Object.FindObjectsOfType<GameObject>()
				where ((Object)o).name == "Doors"
				select o;
			Transform parent = null;
			foreach (GameObject item in enumerable)
			{
				for (int num2 = 0; num2 < item.transform.childCount; num2++)
				{
					Transform child = item.transform.GetChild(num2);
					float num3 = Vector3.Distance(((Component)this).transform.position, ((Component)child).transform.position);
					if (!num.HasValue || num3 < num.Value)
					{
						num = num3;
						parent = child;
					}
				}
			}
			((Component)this).gameObject.transform.SetParent(parent);
			MapManager.RescaleSpawnedObject(((Component)this).gameObject, ((Component)((Component)this).transform.parent).gameObject, MapManager.NewMapsByIdInfo[GameManager.Instance.MapID]);
			Animator component = ((Component)this).GetComponent<Animator>();
			if (component.runtimeAnimatorController.animationClips[1].events.Length == 0)
			{
				component.runtimeAnimatorController.animationClips[1].AddEvent(new AnimationEvent
				{
					functionName = "PlayOpenSoundCustom"
				});
				component.runtimeAnimatorController.animationClips[1].AddEvent(new AnimationEvent
				{
					time = component.runtimeAnimatorController.animationClips[1].length,
					functionName = "AnimationEndedCustom"
				});
			}
			if (component.runtimeAnimatorController.animationClips[3].events.Length == 0)
			{
				component.runtimeAnimatorController.animationClips[3].AddEvent(new AnimationEvent
				{
					functionName = "PlayCloseSoundCustom"
				});
				component.runtimeAnimatorController.animationClips[3].AddEvent(new AnimationEvent
				{
					time = component.runtimeAnimatorController.animationClips[3].length,
					functionName = "AnimationEndedCustom"
				});
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DoorCustom start error: " + ex));
		}
	}

	private void PlayOpenSoundCustom()
	{
		((Component)this).GetComponent<Door>().PlayOpenSound();
	}

	private void PlayCloseSoundCustom()
	{
		((Component)this).GetComponent<Door>().PlayCloseSound();
	}

	private void AnimationEndedCustom()
	{
		((Component)this).GetComponent<Door>().AnimationEnded();
	}
}
