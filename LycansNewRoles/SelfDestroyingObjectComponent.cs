using System.Collections;
using UnityEngine;

namespace LycansNewRoles;

public class SelfDestroyingObjectComponent : MonoBehaviour
{
	public void Init(float secondsToDestroy)
	{
		((MonoBehaviour)this).StartCoroutine(SelfDestroyCoroutine(secondsToDestroy));
	}

	private IEnumerator SelfDestroyCoroutine(float time)
	{
		yield return (object)new WaitForSeconds(time - 0.1f);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
