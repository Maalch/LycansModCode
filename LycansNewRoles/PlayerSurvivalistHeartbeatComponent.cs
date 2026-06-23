using System.Collections;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerSurvivalistHeartbeatComponent : MonoBehaviour
{
	private bool _active;

	public void ToggleEffect(bool active)
	{
		if (!_active && active)
		{
			AudioManager.PlayAndFollow("BeastHeartBeatMid", ((Component)this).transform, (MixerTarget)2, 15f, 0.75f);
			((MonoBehaviour)this).StartCoroutine(RepeatSoundIfNeeded(1.337f));
		}
		_active = active;
	}

	private IEnumerator RepeatSoundIfNeeded(float delay)
	{
		yield return (object)new WaitForSeconds(delay);
		if (_active)
		{
			AudioManager.PlayAndFollow("BeastHeartBeatMid", ((Component)this).transform, (MixerTarget)2, 15f, 0.75f);
			((MonoBehaviour)this).StartCoroutine(RepeatSoundIfNeeded(delay));
		}
	}
}
