using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerPhasingComponent : MonoBehaviour
{
	private PlayerController _player;

	private GameObject _phasingObject;

	private Light _phasingObjectLight;

	private void Awake()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		_player = ((Component)this).GetComponent<PlayerController>();
		Portal[] array = Object.FindObjectsOfType<Portal>(true);
		Portal[] array2 = array;
		foreach (Portal val in array2)
		{
			Light value = Traverse.Create((object)val).Field<Light>("teleportLight").Value;
			if ((Object)(object)value != (Object)null)
			{
				_phasingObject = Object.Instantiate<GameObject>(((Component)value).gameObject, ((Component)_player).transform);
				break;
			}
		}
		_phasingObjectLight = _phasingObject.GetComponent<Light>();
		_phasingObjectLight.color = new Color(1f, 0f, 1f, 1f);
		((Behaviour)_phasingObjectLight).enabled = true;
		_phasingObject.gameObject.SetActive(false);
	}

	public void Activate()
	{
		_phasingObject.gameObject.SetActive(true);
		((MonoBehaviour)this).StartCoroutine(WaitAndDisableLight(0.2f));
	}

	private IEnumerator WaitAndDisableLight(float waitTime)
	{
		yield return (object)new WaitForSeconds(waitTime);
		_phasingObject.SetActive(false);
	}
}
