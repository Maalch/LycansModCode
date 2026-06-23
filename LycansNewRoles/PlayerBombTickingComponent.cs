using System.Collections;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerBombTickingComponent : MonoBehaviour
{
	private bool _active;

	public void ToggleTickEffect(bool active)
	{
		_active = active;
		if (active)
		{
			AudioManager.PlayAndFollow("BombTicking", ((Component)this).transform, (MixerTarget)2, 20f, 0.75f);
			((MonoBehaviour)this).StartCoroutine(RepeatSoundIfNeeded(1f));
		}
	}

	private IEnumerator RepeatSoundIfNeeded(float delay)
	{
		yield return (object)new WaitForSeconds(delay);
		if (_active)
		{
			AudioManager.PlayAndFollow("BombTicking", ((Component)this).transform, (MixerTarget)2, 20f, 0.75f);
			((MonoBehaviour)this).StartCoroutine(RepeatSoundIfNeeded(delay));
		}
	}
}
