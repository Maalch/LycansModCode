using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(3)]
public class ExorcistDetector : NetworkBehaviour
{
	public static GameObject ActivationParticleSystemPrefab;

	private PlayerCustom _creatorCustom;

	private GameObject _effect;

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
				throw new InvalidOperationException("Error when accessing ExorcistDetector.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing ExorcistDetector.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
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
				throw new InvalidOperationException("Error when accessing ExorcistDetector.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing ExorcistDetector.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	public void Awake()
	{
		_effect = ((Component)((Component)this).transform.Find("ExorcistDetectorEffect")).gameObject;
	}

	private void Update()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Invalid comparison between Unknown and I4
		if (_nextCheckWatch.ElapsedMilliseconds < 1000)
		{
			return;
		}
		if (((SimulationBehaviour)this).Runner.IsServer && LycansUtility.WolvesCanTransform && (int)GameManager.LocalGameState == 2)
		{
			RemainingDuration--;
			if (RemainingDuration <= 0)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
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
	public static void CreatorRefChanged(Changed<ExorcistDetector> changed)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			if (changed.Behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
			{
				Plugin.Minimap.AddExorcistDetectorIcon(changed.Behaviour);
			}
			changed.Behaviour._nextCheckWatch.Restart();
			changed.Behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreatorRefChanged error: " + ex));
		}
	}

	private void UpdateVisibility()
	{
		_effect.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved);
	}

	public static void UpdateVisibilityForAllDetectors()
	{
		ExorcistDetector[] array = Object.FindObjectsOfType<ExorcistDetector>();
		ExorcistDetector[] array2 = array;
		foreach (ExorcistDetector exorcistDetector in array2)
		{
			exorcistDetector.UpdateVisibility();
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
		if (((SimulationBehaviour)this).HasStateAuthority && (Object)(object)_creatorCustom != (Object)null)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || (int)val == 5)
			{
				flag = true;
			}
			if (!PlayerRegistry.HasPlayer(CreatorRef))
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
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)((SimulationBehaviour)this).Runner == (Object)null || !((SimulationBehaviour)this).Runner.IsServer)
		{
			return;
		}
		PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
		if ((Object)(object)component != (Object)null && NetworkBool.op_Implicit(component.IsWolf) && !NetworkBool.op_Implicit(component.IsDead))
		{
			PlayerCustom.Rpc_Exorcism(((SimulationBehaviour)this).Runner, _creatorCustom.PlayerController.Index, component.Index);
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(component.Ref);
			Vector3 val = ((Component)this).transform.position - ((Component)component).transform.position;
			Vector3 val2 = -((Vector3)(ref val)).normalized;
			if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Beast && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive))
			{
				val2 *= 0.6f;
			}
			((Component)component).GetComponent<KnockbackComponent>().Init(new Vector3(val2.x, 0f, val2.z), 5f, 5f);
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
	}
}
