using System.Collections;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerPoacherMarkComponent : MonoBehaviour
{
	public static GameObject PoacherMarkPrefab;

	private GameObject _poacherMarkObject;

	private void Awake()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		_poacherMarkObject = Object.Instantiate<GameObject>(PoacherMarkPrefab, ((Component)((Component)this).GetComponent<PlayerController>()).transform);
		_poacherMarkObject.SetActive(true);
		_poacherMarkObject.transform.position = new Vector3(_poacherMarkObject.transform.position.x, _poacherMarkObject.transform.position.y, _poacherMarkObject.transform.position.z);
		_poacherMarkObject.SetActive(false);
	}

	public void SetPoacherMarkActive(bool active)
	{
		if (active)
		{
			if (!_poacherMarkObject.activeSelf)
			{
				AudioManager.PlayAndFollow("PoacherMark", ((Component)this).transform, (MixerTarget)2, 30f, 0.75f);
				((MonoBehaviour)this).StartCoroutine(RepeatSound(TimeBeforeRepeatSound()));
			}
		}
		else
		{
			((MonoBehaviour)this).StopCoroutine("RepeatSound");
		}
		_poacherMarkObject.SetActive(active);
	}

	private IEnumerator RepeatSound(float delay)
	{
		yield return (object)new WaitForSeconds(delay);
		if (_poacherMarkObject.activeSelf)
		{
			AudioManager.PlayAndFollow("PoacherMark", ((Component)this).transform, (MixerTarget)2, 30f, 0.75f);
			((MonoBehaviour)this).StartCoroutine(RepeatSound(TimeBeforeRepeatSound()));
		}
	}

	private float TimeBeforeRepeatSound()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
		float num = Vector3.Distance(((Component)povPlayer).transform.position, ((Component)this).transform.position);
		float num2 = 0.5f;
		float num3 = 1.5f;
		float num4 = Mathf.InverseLerp(3f, 30f, num);
		return Mathf.Lerp(num2, num3, num4);
	}
}
