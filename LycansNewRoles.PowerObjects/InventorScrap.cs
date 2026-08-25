using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using Helpers.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(2)]
public class InventorScrap : NetworkBehaviour
{
	private GameObject _visual;

	private GameObject _scrap;

	private PlayerCustom _creatorCustom;

	private Stopwatch _nextCheckWatch = new Stopwatch();

	private int _chargeValue;

	public static List<InventorScrap> AllScraps = new List<InventorScrap>();

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
				throw new InvalidOperationException("Error when accessing InventorScrap.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing InventorScrap.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
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
				throw new InvalidOperationException("Error when accessing InventorScrap.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing InventorScrap.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	private void Awake()
	{
		_visual = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		_scrap = ((Component)_visual.transform.Find("Scrap")).gameObject;
	}

	private void Update()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Invalid comparison between Unknown and I4
		if (_visual.gameObject.activeSelf)
		{
			_scrap.transform.Rotate(0f, 0f, Time.deltaTime * 128f);
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
				return;
			}
		}
		_nextCheckWatch.Restart();
	}

	public void Init(PlayerRef playerRef, int duration, int chargeValue)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
		RemainingDuration = duration;
		_chargeValue = chargeValue;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<InventorScrap> changed)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			AllScraps.Add(changed.Behaviour);
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

	public static void UpdateVisibilityForAllScrap()
	{
		foreach (InventorScrap allScrap in AllScraps)
		{
			allScrap.UpdateVisibility();
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
		AllScraps.Remove(this);
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
				player.AddMaterials(_chargeValue);
				PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, player.Index, 12);
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	public static void CreateNewScrapForRandomInventor(NetworkRunner runner, Vector3 position)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Inventor && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld).ToList();
		if (list.Count > 0)
		{
			PlayerCustom playerCustom = CollectionsUtil.Grab<PlayerCustom>(list, 1).First();
			CreateNewScrap(runner, playerCustom, position, list.Count * 25);
		}
	}

	public static void CreateNewScrap(NetworkRunner runner, PlayerCustom playerCustom, Vector3 position, int chargePower)
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
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectInventorScrap");
		position = new Vector3(position.x, position.y + 0.25f, position.z);
		NetworkObject val = runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			((Component)no).transform.position = position;
		}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		((Component)val).transform.position = position;
		((Component)val).GetComponent<InventorScrap>().Init(playerCustom.Ref, 120, chargePower);
	}
}
