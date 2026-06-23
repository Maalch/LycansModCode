using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(2)]
public class SurvivalistHint : NetworkBehaviour
{
	private GameObject _visual;

	private GameObject _hint;

	private PlayerCustom _creatorCustom;

	private Stopwatch _nextCheckWatch = new Stopwatch();

	public static List<SurvivalistHint> AllHints = new List<SurvivalistHint>();

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
				throw new InvalidOperationException("Error when accessing SurvivalistHint.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SurvivalistHint.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
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
				throw new InvalidOperationException("Error when accessing SurvivalistHint.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SurvivalistHint.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	private void Awake()
	{
		_visual = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		_hint = ((Component)_visual.transform.Find("Hint")).gameObject;
	}

	private void Update()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Invalid comparison between Unknown and I4
		if (_visual.gameObject.activeSelf)
		{
			_hint.transform.Rotate(0f, Time.deltaTime * 128f, 0f);
		}
		if (_nextCheckWatch.ElapsedMilliseconds < 1000)
		{
			return;
		}
		if (((SimulationBehaviour)this).Runner.IsServer && (int)GameManager.LocalGameState == 2)
		{
			RemainingDuration--;
			if (RemainingDuration <= 0)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
		_nextCheckWatch.Restart();
	}

	public void Init(PlayerRef playerRef, int duration)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
		RemainingDuration = duration;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<SurvivalistHint> changed)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			AllHints.Add(changed.Behaviour);
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
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
		_visual.SetActive(_creatorCustom.IsCurrentlyPlayedOrObserved);
	}

	public static void UpdateVisibilityForAllHints()
	{
		foreach (SurvivalistHint allHint in AllHints)
		{
			allHint.UpdateVisibility();
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
			if (!PlayerRegistry.HasPlayer(CreatorRef) || NetworkBool.op_Implicit(_creatorCustom.PlayerController.IsDead))
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
		AllHints.Remove(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)((SimulationBehaviour)this).Runner == (Object)null) && ((SimulationBehaviour)this).Runner.IsServer)
		{
			PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
			if ((Object)(object)component != (Object)null && component.Ref == CreatorRef)
			{
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(component.Ref);
				player.AddMaterials(2000);
				PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, player.Index, 12);
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	public static void CreateNewHint(NetworkRunner runner, PlayerCustom playerCustom, Vector3 position)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectSurvivalistHint");
		position = new Vector3(position.x, position.y + 0.25f, position.z);
		NetworkObject val = runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			((Component)no).transform.position = position;
		}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		((Component)val).transform.position = position;
		((Component)val).GetComponent<SurvivalistHint>().Init(playerCustom.Ref, 90);
	}
}
