using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(4)]
public class CultistSkullSpirit : NetworkBehaviour
{
	private BoxCollider _boxCollider;

	private PlayerCustom _creatorCustom;

	private Stopwatch _appearStopwatch = new Stopwatch();

	private Stopwatch _soundStopwatch = new Stopwatch();

	private Stopwatch _checkCollisionsStopwatch = new Stopwatch();

	private Stopwatch _changeTargetPlayerStopwatch = new Stopwatch();

	private Stopwatch _disappearStopwatch = new Stopwatch();

	private GameObject _portal;

	private GameObject _visual;

	private bool _slowedByWall = false;

	private Stopwatch _slowStopwatch = new Stopwatch();

	[Networked(OnChanged = "CreatorRefChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe PlayerRef CreatorRef
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing CultistSkull.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing CultistSkull.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "AppearedChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool Appeared
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing CultistSkull.Appeared. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing CultistSkull.Appeared. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe PlayerRef TargetPlayerRef
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing CultistSkull.TargetPlayerRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[2];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing CultistSkull.TargetPlayerRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 2, value);
		}
	}

	private void Awake()
	{
		_boxCollider = ((Component)this).GetComponent<BoxCollider>();
		_portal = ((Component)((Component)this).transform.Find("Portal")).gameObject;
		_visual = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		_portal.SetActive(true);
		_visual.SetActive(false);
		_slowStopwatch.Reset();
	}

	public override void Spawned()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		Appeared = NetworkBool.op_Implicit(false);
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			_appearStopwatch.Start();
		}
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
		_creatorCustom = PlayerCustomRegistry.GetPlayer(playerRef);
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<CultistSkullSpirit> changed)
	{
	}

	[Preserve]
	public static void AppearedChanged(Changed<CultistSkullSpirit> changed)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(changed.Behaviour.Appeared))
		{
			changed.Behaviour._portal.SetActive(false);
			changed.Behaviour._visual.SetActive(true);
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Invalid comparison between Unknown and I4
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority && (Object)(object)_creatorCustom != (Object)null)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || val - 4 <= 1)
			{
				flag = true;
			}
			if (!PlayerRegistry.HasPlayer(CreatorRef) || NetworkBool.op_Implicit(_creatorCustom.PlayerController.IsDead))
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				return;
			}
		}
		if (!NetworkBool.op_Implicit(Appeared) && ((SimulationBehaviour)this).Runner.IsServer && (float)_appearStopwatch.ElapsedMilliseconds >= 4000f)
		{
			Appeared = NetworkBool.op_Implicit(true);
			_appearStopwatch.Stop();
			_soundStopwatch.Restart();
			_changeTargetPlayerStopwatch.Restart();
			_disappearStopwatch.Restart();
			_checkCollisionsStopwatch.Restart();
		}
		if (!LycansUtility.GameActuallyInPlay || !NetworkBool.op_Implicit(Appeared))
		{
			return;
		}
		if (_soundStopwatch.ElapsedMilliseconds >= 2000)
		{
			AudioManager.PlayAndFollow("CultistSkull", ((Component)this).transform, (MixerTarget)2, 12f, 0.7f);
			_soundStopwatch.Restart();
		}
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			if ((float)_disappearStopwatch.ElapsedMilliseconds >= 45000f)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				return;
			}
			if (_changeTargetPlayerStopwatch.ElapsedMilliseconds >= 1000)
			{
				TargetPlayerRef = (from o in PlayerCustomRegistry
					where !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Cultist && !NetworkBool.op_Implicit(o.CapturedByCultist) && !NetworkBool.op_Implicit(o.Resurrected) && !o.IsOutOfTheWorld
					orderby Vector3.Distance(((Component)o.PlayerController).transform.position, ((Component)this).transform.position)
					select o).FirstOrDefault().Ref;
			}
			if (_checkCollisionsStopwatch.ElapsedMilliseconds >= 250)
			{
				IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.CapturedByCultist) && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Cultist && !NetworkBool.op_Implicit(o.Resurrected) && !NetworkBool.op_Implicit(o.PlayerController.IsDead));
				foreach (PlayerCustom item in enumerable)
				{
					if (Vector3.Distance(((Component)this).transform.position, ((Component)item.PlayerController).transform.position) <= 1f)
					{
						item.PlayerController.PlayerEffectManager.ClearEffects();
						item.BombDormant = NetworkBool.op_Implicit(false);
						item.CurseDormant = NetworkBool.op_Implicit(false);
						PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectCaptured", ((SimulationBehaviour)this).Runner, 1f, 3600f);
						CultistManager.Instance.CultistCaptures++;
					}
				}
				RaycastHit val2 = default(RaycastHit);
				if (Physics.Raycast(_visual.transform.position, _visual.transform.forward, ref val2, 0.75f) && ((Component)((RaycastHit)(ref val2)).transform).gameObject.layer == 0)
				{
					_slowedByWall = true;
					_slowStopwatch.Restart();
				}
				_checkCollisionsStopwatch.Restart();
			}
			if (_slowStopwatch.IsRunning && (float)_slowStopwatch.ElapsedMilliseconds >= 2000f)
			{
				_slowedByWall = false;
				_slowStopwatch.Reset();
			}
		}
		if (!(TargetPlayerRef != PlayerRef.None))
		{
			return;
		}
		if (!PlayerRegistry.HasPlayer(TargetPlayerRef))
		{
			TargetPlayerRef = PlayerRef.None;
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(TargetPlayerRef);
		if (NetworkBool.op_Implicit(player.CapturedByCultist) || player.IsOutOfTheWorld || NetworkBool.op_Implicit(player.PlayerController.IsDead))
		{
			TargetPlayerRef = PlayerRef.None;
			return;
		}
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			float num = (_slowedByWall ? 1.25f : 3.5f);
			((Component)this).transform.position = Vector3.MoveTowards(((Component)this).transform.position, ((Component)player.PlayerController).transform.position, num * ((SimulationBehaviour)this).Runner.DeltaTime);
		}
		((Component)this).transform.LookAt(((Component)player.PlayerController).transform.position);
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
