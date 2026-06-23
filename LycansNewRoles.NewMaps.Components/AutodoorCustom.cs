using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

[NetworkBehaviourWeaved(2)]
public class AutodoorCustom : NetworkBehaviour
{
	public Collider DetectionCollider;

	public string SoundOpen;

	public string SoundClose;

	public int MapID;

	private Animator _animator;

	private List<PlayerRef> _playersInDetection = new List<PlayerRef>();

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe NetworkBool IsOpen
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing Door.IsOpen. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing Door.IsOpen. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool IsMoving
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing Door.IsMoving. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing Door.IsMoving. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	private void Start()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Expected O, but got Unknown
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		try
		{
			if ((Object)(object)GameManager.Instance == (Object)null)
			{
				return;
			}
			float? num = null;
			IEnumerable<GameObject> enumerable = from o in Object.FindObjectsOfType<GameObject>()
				where ((Object)o).name == "Autodoors"
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
			_animator = ((Component)this).GetComponent<Animator>();
			AnimationClip val2 = _animator.runtimeAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == "Opening");
			if (val2.events.Length == 0)
			{
				val2.AddEvent(new AnimationEvent
				{
					functionName = "PlayOpenSoundCustom"
				});
				val2.AddEvent(new AnimationEvent
				{
					time = val2.length,
					functionName = "AnimationEndedCustom"
				});
			}
			AnimationClip val3 = _animator.runtimeAnimatorController.animationClips.First((AnimationClip o) => ((Object)o).name == "Closing");
			if (val3.events.Length == 0)
			{
				val3.AddEvent(new AnimationEvent
				{
					functionName = "PlayCloseSoundCustom"
				});
				val3.AddEvent(new AnimationEvent
				{
					time = val3.length,
					functionName = "AnimationEndedCustom"
				});
			}
			IsOpen = NetworkBool.op_Implicit(false);
			IsMoving = NetworkBool.op_Implicit(false);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("DoorCustom start error: " + ex));
		}
	}

	private void PlayOpenSoundCustom()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (!string.IsNullOrEmpty(SoundOpen))
		{
			AudioManager.PlayPosition(SoundOpen.ToLower(), ((Component)this).transform.position, (MixerTarget)2, 15f, 0.5f);
		}
	}

	private void PlayCloseSoundCustom()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (!string.IsNullOrEmpty(SoundClose) && (int)GameManager.State.Current == 2)
		{
			AudioManager.PlayPosition(SoundClose.ToLower(), ((Component)this).transform.position, (MixerTarget)2, 15f, 0.5f);
		}
	}

	private void AnimationEndedCustom()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		IsMoving = NetworkBool.op_Implicit(false);
	}

	public void AddPlayerInDetection(PlayerRef playerRef)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_playersInDetection.Add(playerRef);
	}

	public void RemovePlayerFromDetection(PlayerRef playerRef)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_playersInDetection.Remove(playerRef);
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (!NetworkBool.op_Implicit(IsMoving))
		{
			if (NetworkBool.op_Implicit(IsOpen) && !_playersInDetection.Any())
			{
				IsMoving = NetworkBool.op_Implicit(true);
				IsOpen = NetworkBool.op_Implicit(false);
				_animator.SetBool(Animator.StringToHash("Open"), false);
			}
			else if (!NetworkBool.op_Implicit(IsOpen) && _playersInDetection.Any())
			{
				IsMoving = NetworkBool.op_Implicit(true);
				IsOpen = NetworkBool.op_Implicit(true);
				_animator.SetBool(Animator.StringToHash("Open"), true);
			}
		}
	}
}
