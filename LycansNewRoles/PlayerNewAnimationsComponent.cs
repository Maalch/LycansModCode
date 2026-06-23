using System;
using System.Collections;
using System.Linq;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerNewAnimationsComponent : MonoBehaviour
{
	private PlayerController _playerController;

	private Animator _villagerAnimator;

	private Animator _wolfAnimator;

	private AnimatorOverrideController _villagerAOC;

	private AnimatorOverrideController _wolfAOC;

	private string _currentLoopAnimation = null;

	private bool _currentAnimationIsEmote = false;

	private Coroutine _stopNonLoopAnimationCoroutine;

	public static AnimationClip AnimationClipNonLoop;

	public static AnimationClip AnimationClipLoop;

	public static RuntimeAnimatorController CustomAnimatorController;

	private static bool FirstClipReplaced;

	public const string ClipToOverrideNonLoop = "Drinking";

	public const string AnimationToOverrideNonLoop = "Drinking";

	public const string ClipToOverrideLoop = "RifleIdle";

	public const string AnimationToOverrideLoop = "Aiming";

	public const string AnimationGroundAction = "HumanM@Gathering02";

	public const string AnimationSpellcastInstantWithTarget = "HumanM@MagicAttackDirect1H01_R";

	public const string AnimationSpellcastInstantWithoutTarget = "HumanM@MagicAttackCall1H01_L";

	public const string AnimationSpellcastBuff = "Buff";

	public const string AnimationHurt = "GetHit";

	public const string AnimationKnockback = "Unarmed-Knockback-Back1";

	public const string AnimationSpellcastLoop = "CastingLoop";

	public const string AnimationSabotage = "HumanM@Gathering01";

	public const string AnimationSkipVote = "HumanM@Question01";

	public const string AnimationStunned = "HumanM@Stun01";

	public const string AnimationEmote1 = "6-Threatening to kill-NR";

	public const string AnimationEmote2 = "MCU_am_Stand_Emotion_ThisGuy_01";

	public const string AnimationEmote3 = "HumanM@HeadShake02";

	public const string AnimationEmote4 = "HumanM@Cheer02";

	public const string AnimationEmote5 = "HumanM@HandClap01";

	public const string AnimationEmote6 = "HumanM@Dance01 - Loop";

	public const string AnimationEmote7 = "HumanM@Dance05 - Loop";

	public const string AnimationEmote8 = "HumanM@Dance06 - Loop";

	public string CurrentLoopAnimation => _currentLoopAnimation;

	private void Awake()
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		try
		{
			_playerController = ((Component)this).GetComponent<PlayerController>();
			_villagerAnimator = Traverse.Create((object)_playerController).Field<Animator>("villagerAnimator").Value;
			_wolfAnimator = Traverse.Create((object)_playerController).Field<Animator>("wolfAnimator").Value;
			if (!FirstClipReplaced)
			{
				AnimationClipNonLoop = _villagerAnimator.runtimeAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == "Drinking");
				AnimationClipLoop = _villagerAnimator.runtimeAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == "RifleIdle");
				FirstClipReplaced = true;
			}
			_villagerAOC = new AnimatorOverrideController(_villagerAnimator.runtimeAnimatorController);
			_villagerAnimator.runtimeAnimatorController = (RuntimeAnimatorController)(object)_villagerAOC;
			_wolfAOC = new AnimatorOverrideController(_wolfAnimator.runtimeAnimatorController);
			_wolfAnimator.runtimeAnimatorController = (RuntimeAnimatorController)(object)_wolfAOC;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PlayerAnimationsComponent error: " + ex));
		}
	}

	private void Update()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(_playerController.IsMoving))
		{
			if (_currentAnimationIsEmote)
			{
				StopNonLoopAnimation();
				StopNonLoopCoroutineIfExists();
			}
			if (_currentLoopAnimation != null)
			{
				CancelLoopAnimation();
			}
		}
	}

	public void PlayNonLoopEmote(string animationName)
	{
		StopNonLoopCoroutineIfExists();
		PlayNonLoopAnimation(animationName);
		_currentAnimationIsEmote = true;
	}

	public void PlayNonLoopAnimation(string animationName)
	{
		if (_currentLoopAnimation != null)
		{
			CancelLoopAnimation();
		}
		_villagerAOC["Drinking"] = CustomAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == animationName);
		_villagerAnimator.SetBool("Drinking", true);
		_wolfAOC["Drinking"] = CustomAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == animationName);
		_wolfAnimator.SetBool("Drinking", true);
		StopNonLoopCoroutineIfExists();
		_stopNonLoopAnimationCoroutine = ((MonoBehaviour)this).StartCoroutine(WaitAndStopNonLoopAnimation(((Motion)_villagerAOC["Drinking"]).averageDuration));
	}

	private void StopNonLoopCoroutineIfExists()
	{
		if (_stopNonLoopAnimationCoroutine != null)
		{
			((MonoBehaviour)this).StopCoroutine(_stopNonLoopAnimationCoroutine);
			_stopNonLoopAnimationCoroutine = null;
		}
	}

	private IEnumerator WaitAndStopNonLoopAnimation(float delay)
	{
		yield return (object)new WaitForSeconds(delay);
		StopNonLoopAnimation();
		_stopNonLoopAnimationCoroutine = null;
	}

	private void StopNonLoopAnimation()
	{
		_villagerAnimator.SetBool("Drinking", false);
		_villagerAnimator.speed = 1f;
		_wolfAnimator.SetBool("Drinking", false);
		_wolfAnimator.speed = 1f;
		_currentAnimationIsEmote = false;
	}

	public void ResetNonLoopAnimation()
	{
		_villagerAnimator.SetBool("Drinking", false);
		_wolfAnimator.SetBool("Drinking", false);
		_villagerAOC["Drinking"] = AnimationClipNonLoop;
		_wolfAOC["Drinking"] = AnimationClipNonLoop;
		_currentAnimationIsEmote = false;
	}

	public void SetLoopAnimation(string animationName, bool active)
	{
		if (active)
		{
			if (_currentLoopAnimation != animationName)
			{
				_villagerAOC["RifleIdle"] = CustomAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == animationName);
				_villagerAnimator.SetBool("Aiming", true);
				_wolfAOC["RifleIdle"] = CustomAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == animationName);
				_wolfAnimator.SetBool("Aiming", true);
				_currentLoopAnimation = animationName;
			}
		}
		else if (_currentLoopAnimation == animationName)
		{
			CancelLoopAnimation();
		}
	}

	public bool CanDoEmote(string animationName)
	{
		if (_currentAnimationIsEmote)
		{
			return true;
		}
		if (_currentLoopAnimation != null)
		{
			return false;
		}
		AnimatorControllerParameter[] parameters = _villagerAnimator.parameters;
		foreach (AnimatorControllerParameter val in parameters)
		{
			if (_villagerAnimator.GetBool(val.name))
			{
				return false;
			}
		}
		return true;
	}

	public void ToggleLoopEmote(string animationName)
	{
		if (_currentLoopAnimation != animationName)
		{
			StopNonLoopAnimation();
			StopNonLoopCoroutineIfExists();
			_villagerAOC["RifleIdle"] = CustomAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == animationName);
			_villagerAnimator.SetBool("Aiming", true);
			_wolfAOC["RifleIdle"] = CustomAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == animationName);
			_wolfAnimator.SetBool("Aiming", true);
			_currentLoopAnimation = animationName;
			_currentAnimationIsEmote = true;
		}
		else
		{
			CancelLoopAnimation();
		}
	}

	public void CancelLoopAnimation()
	{
		_villagerAnimator.SetBool("Aiming", false);
		_wolfAnimator.SetBool("Aiming", false);
		_currentLoopAnimation = null;
		_currentAnimationIsEmote = false;
	}

	public void ResetLoopAnimation()
	{
		_villagerAOC["RifleIdle"] = AnimationClipLoop;
		_wolfAOC["RifleIdle"] = AnimationClipLoop;
		_currentLoopAnimation = null;
		_currentAnimationIsEmote = false;
	}
}
