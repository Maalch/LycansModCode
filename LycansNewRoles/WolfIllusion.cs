using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using LycansNewRoles.PowerObjects;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(3)]
public class WolfIllusion : NetworkBehaviour
{
	private PlayerCustom _creatorCustom;

	private SkinnedMeshRenderer _wolfMeshRenderer;

	private Stopwatch _nextCheckWatch = new Stopwatch();

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
				throw new InvalidOperationException("Error when accessing MagicianIllusion.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe int RemainingDuration
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	private void Awake()
	{
		_wolfMeshRenderer = ((Component)((Component)this).transform.Find("Body").Find("Werewolf").Find("WerewolfModel")).GetComponent<SkinnedMeshRenderer>();
		UpdateVisible(visible: false);
	}

	private void Update()
	{
		if (_nextCheckWatch.ElapsedMilliseconds < 1000)
		{
			return;
		}
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			RemainingDuration--;
			if (RemainingDuration <= 0)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				return;
			}
		}
		_nextCheckWatch.Restart();
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	public void Init(int duration)
	{
		RemainingDuration = duration;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<WolfIllusion> changed)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			((Component)changed.Behaviour).gameObject.layer = 25;
			((Component)((Component)changed.Behaviour).transform.Find("Body")).gameObject.layer = 25;
			((Component)((Component)changed.Behaviour).transform.Find("Body").Find("Werewolf")).gameObject.layer = 25;
			((Component)changed.Behaviour).transform.LookAt(((Component)changed.Behaviour._creatorCustom.PlayerController).transform);
			changed.Behaviour._nextCheckWatch.Restart();
			if (changed.Behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
			{
				GameObject val = Object.Instantiate<GameObject>(PlayerCustom.TeleportParticleSystemPrefab, ((Component)changed.Behaviour).transform.position, Quaternion.identity);
				val.SetActive(true);
				SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
				MainModule main = val.GetComponent<ParticleSystem>().main;
				selfDestroyingObjectComponent.Init(((MainModule)(ref main)).duration);
				AudioManager.PlayPosition("TELEPORT_END", ((Component)changed.Behaviour).transform.position, (MixerTarget)2, 10f, 1f);
			}
			changed.Behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreatorRefChanged error: " + ex));
		}
	}

	private void UpdateVisibility()
	{
		if (!((Object)(object)_creatorCustom == (Object)null))
		{
			bool isCurrentlyPlayedOrObserved = _creatorCustom.IsCurrentlyPlayedOrObserved;
			UpdateVisible(isCurrentlyPlayedOrObserved);
		}
	}

	private void UpdateVisible(bool visible)
	{
		((Renderer)_wolfMeshRenderer).enabled = visible;
	}

	public override void FixedUpdateNetwork()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Invalid comparison between Unknown and I4
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Invalid comparison between Unknown and I4
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || val - 3 <= 2)
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (_creatorCustom.IsCurrentlyPlayedOrObserved)
		{
			GameObject val = Object.Instantiate<GameObject>(DiscipleAnchor.ActivationParticleSystemPrefab, ((Component)this).transform.position, Quaternion.identity);
			val.SetActive(true);
			SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
			selfDestroyingObjectComponent.Init(2f);
		}
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
