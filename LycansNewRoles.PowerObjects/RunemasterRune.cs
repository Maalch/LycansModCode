using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(3)]
public class RunemasterRune : NetworkBehaviour
{
	public static GameObject ActivationParticleSystemPrefab;

	private PlayerCustom _creatorCustom;

	private GameObject _effect;

	private GameObject _seethrough;

	private int _triggerDelayMilliseconds = 1000;

	private Stopwatch _checkWatch = new Stopwatch();

	private Stopwatch _explosionWatch = new Stopwatch();

	private int _runeIndex;

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
				throw new InvalidOperationException("Error when accessing RunemasterRune.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing RunemasterRune.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "TriggeredChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool Triggered
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing RunemasterRune.Triggered. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing RunemasterRune.Triggered. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	public void Awake()
	{
		_effect = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		_seethrough = ((Component)((Component)this).transform.Find("Icon")).gameObject;
		_checkWatch.Start();
		_explosionWatch.Stop();
	}

	private void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(Triggered))
		{
			if (_explosionWatch.ElapsedMilliseconds < _triggerDelayMilliseconds)
			{
				return;
			}
			_explosionWatch.Reset();
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (_creatorCustom.IsCurrentlyPlayedOrObserved || NetworkBool.op_Implicit(player.PlayerController.IsWolf))
			{
				GameObject val = Object.Instantiate<GameObject>(ActivationParticleSystemPrefab, new Vector3(((Component)this).transform.position.x, ((Component)this).transform.position.y + 0.5f, ((Component)this).transform.position.z), Quaternion.identity);
				val.SetActive(true);
				float num = 4.5f;
				val.transform.localScale = new Vector3(num, num, num);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
				selfDestroyingObjectComponent.Init(2.5f);
				if (_creatorCustom.IsCurrentlyPlayedOrObserved)
				{
					AudioManager.PlayPosition("RuneExplosion", ((Component)this).transform.position, (MixerTarget)2, 500f, 1f);
				}
				else
				{
					AudioManager.PlayPosition("RuneExplosion", ((Component)this).transform.position, (MixerTarget)2, 15f, 1f);
				}
			}
			float num2 = 8f;
			foreach (PlayerController item in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead))))
			{
				float num3 = Vector3.Distance(((Component)item).transform.position, ((Component)this).transform.position);
				if (!(num3 > num2))
				{
					if (item.MovementAction == 1)
					{
						PlayerCustom.ApplyEffectToPlayer(item, "LycansNewRoles.EffectDisoriented", ((SimulationBehaviour)this).Runner, 1f, 2f);
						continue;
					}
					((Component)item).GetComponent<ForcedRotationComponent>().Init(new Vector3(0f, 1f, 0f), 4000f, 2000f);
					PlayerCustom.ApplyEffectToPlayer(item, "LycansNewRoles.EffectDisoriented", ((SimulationBehaviour)this).Runner, 1f, 8f);
					PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(item.Ref);
					player2.GrantResilience(15f);
				}
			}
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
		else if (((SimulationBehaviour)this).Runner.IsServer && _checkWatch.ElapsedMilliseconds >= 250 && _creatorCustom.PrimaryRolePowerCurrentMaterials >= _creatorCustom.PowerMaterialsInfo.RequiredMaterials)
		{
			PlayerCustom playerCustom = PlayerCustomRegistry.AllPlayers.FirstOrDefault((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf) && Vector3.Distance(((Component)o.PlayerController).transform.position, ((Component)this).transform.position) <= 4f);
			if ((Object)(object)playerCustom != (Object)null)
			{
				PlayerCustom.ApplyEffectToPlayer(playerCustom.PlayerController, "LycansNewRoles.EffectDetected", ((SimulationBehaviour)this).Runner, 1f, 5f);
				Triggered = NetworkBool.op_Implicit(true);
				_creatorCustom.ReduceMaterialAfterPowerUse();
				_checkWatch.Stop();
			}
			else
			{
				_checkWatch.Restart();
			}
		}
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<RunemasterRune> changed)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)changed.Behaviour).Runner.IsServer)
			{
				List<RunemasterRune> list = (from o in Object.FindObjectsOfType<RunemasterRune>()
					where o.CreatorRef == changed.Behaviour.CreatorRef
					select o).ToList();
				changed.Behaviour._runeIndex = ((!list.Any()) ? 1 : (list.Max((RunemasterRune o) => o._runeIndex) + 1));
				if (list.Count > 8)
				{
					int minRuneIndex = list.Min((RunemasterRune o) => o._runeIndex);
					RunemasterRune runemasterRune = list.First((RunemasterRune o) => o._runeIndex == minRuneIndex);
					((SimulationBehaviour)changed.Behaviour).Runner.Despawn(((Component)runemasterRune).GetComponent<NetworkObject>(), false);
				}
			}
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			if (changed.Behaviour.CreatorRef == PlayerController.Local.Ref)
			{
				Plugin.Minimap.AddRunemasterRuneIcon(changed.Behaviour);
			}
			changed.Behaviour._creatorCustom.AssociatedRunes.Add(changed.Behaviour);
			changed.Behaviour._creatorCustom.UpdateDescriptionStatusIfNeeded();
			changed.Behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreatorRefChanged error: " + ex));
		}
	}

	[Preserve]
	public static void TriggeredChanged(Changed<RunemasterRune> changed)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		changed.Behaviour.UpdateVisibility();
		if (!NetworkBool.op_Implicit(changed.Behaviour.Triggered))
		{
			return;
		}
		changed.Behaviour._triggerDelayMilliseconds = Random.RandomRangeInt(800, 1400);
		ParticleSystem[] componentsInChildren = changed.Behaviour._effect.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem val in componentsInChildren)
		{
			RotationOverLifetimeModule rotationOverLifetime = val.rotationOverLifetime;
			if (((RotationOverLifetimeModule)(ref rotationOverLifetime)).enabled)
			{
				RotationOverLifetimeModule rotationOverLifetime2 = val.rotationOverLifetime;
				((RotationOverLifetimeModule)(ref rotationOverLifetime2)).x = MinMaxCurve.op_Implicit(100f);
				((RotationOverLifetimeModule)(ref rotationOverLifetime2)).y = MinMaxCurve.op_Implicit(100f);
				((RotationOverLifetimeModule)(ref rotationOverLifetime2)).z = MinMaxCurve.op_Implicit(100f);
			}
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (changed.Behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
		{
			AudioManager.PlayPosition("RuneTrigger", ((Component)changed.Behaviour).transform.position, (MixerTarget)2, 500f, 0.8f);
		}
		else if (NetworkBool.op_Implicit(player.PlayerController.IsWolf))
		{
			AudioManager.PlayPosition("RuneTrigger", ((Component)changed.Behaviour).transform.position, (MixerTarget)2, 15f, 0.8f);
		}
		changed.Behaviour._explosionWatch.Restart();
	}

	private void UpdateVisibility()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		_effect.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved || (NetworkBool.op_Implicit(Triggered) && NetworkBool.op_Implicit(player.PlayerController.IsWolf)));
		_seethrough.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved);
	}

	public static void UpdateVisibilityForAllRunes()
	{
		RunemasterRune[] array = Object.FindObjectsOfType<RunemasterRune>();
		RunemasterRune[] array2 = array;
		foreach (RunemasterRune runemasterRune in array2)
		{
			runemasterRune.UpdateVisibility();
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority && (Object)(object)_creatorCustom != (Object)null)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || (int)val == 5)
			{
				flag = true;
			}
			if (!PlayerRegistry.HasPlayer(CreatorRef) || NetworkBool.op_Implicit(PlayerRegistry.GetPlayer(CreatorRef).IsDead))
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		if ((Object)(object)_creatorCustom != (Object)null)
		{
			_creatorCustom.AssociatedRunes.Remove(this);
		}
		if ((Object)(object)_creatorCustom != (Object)null && _creatorCustom.Ref == PlayerController.Local.Ref)
		{
			PlayerCustom.Local.UpdateDescriptionStatusIfNeeded();
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
