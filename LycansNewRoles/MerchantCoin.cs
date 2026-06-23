using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using Helpers.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(3)]
public class MerchantCoin : NetworkBehaviour
{
	public static List<MerchantCoin> AllCoins = new List<MerchantCoin>();

	private GameObject _visual;

	private GameObject _coin;

	private PlayerCustom _creatorCustom;

	public Teleporter AssociatedTeleporter;

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
				throw new InvalidOperationException("Error when accessing MerchantCoin.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MerchantCoin.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	private void Awake()
	{
		_visual = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		_coin = ((Component)_visual.transform.Find("Coin")).gameObject;
	}

	public void Initialize(PlayerRef playerRef, Teleporter teleporter)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
		AssociatedTeleporter = teleporter;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<MerchantCoin> changed)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			AllCoins.Add(changed.Behaviour);
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			if (changed.Behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
			{
				Plugin.Minimap.AddMerchantCoinIcon(changed.Behaviour);
				changed.Behaviour._visual.gameObject.SetActive(true);
				changed.Behaviour._creatorCustom.UpdateTargetArrowComponent();
			}
			else
			{
				changed.Behaviour._visual.gameObject.SetActive(false);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreatorRefChanged error: " + ex));
		}
	}

	private void Update()
	{
		if (_visual.gameObject.activeSelf)
		{
			_coin.transform.Rotate(0f, Time.deltaTime * 128f, 0f);
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
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Invalid comparison between Unknown and I4
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
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
			}
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		AllCoins.Remove(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Invalid comparison between Unknown and I4
		if ((Object)(object)((SimulationBehaviour)this).Runner == (Object)null || !((SimulationBehaviour)this).Runner.IsServer)
		{
			return;
		}
		PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
		if ((Object)(object)component != (Object)null && component.Ref == CreatorRef)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(component.Ref);
			if ((int)component.Role == 1)
			{
				player.SecondaryRoleUniqueInt += 2;
			}
			else
			{
				player.SecondaryRoleUniqueInt += 3;
			}
			AllCoins.Remove(this);
			CreateNewCoin(((SimulationBehaviour)this).Runner, player, new List<Teleporter>());
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
	}

	public static Teleporter CreateNewCoin(NetworkRunner runner, PlayerCustom playerCustom, List<Teleporter> teleportersToExclude)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		float minimumDistanceToCoin = 40f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectMerchantCoin");
		NetworkTeleportData teleportData = playerCustom.PlayerController.CharacterMovementHandler.TeleportData;
		Vector3 position;
		if (!((NetworkTeleportData)(ref teleportData)).IsNone)
		{
			teleportData = playerCustom.PlayerController.CharacterMovementHandler.TeleportData;
			position = ((NetworkTeleportData)(ref teleportData)).Position;
		}
		else
		{
			position = ((Component)playerCustom.PlayerController).transform.position;
		}
		Vector3 playerPosition = position;
		teleportersToExclude.AddRange(from o in AllCoins
			where o.CreatorRef == playerCustom.Ref
			select o.AssociatedTeleporter);
		List<Teleporter> list = (from o in Object.FindObjectsOfType<Teleporter>()
			where o.MapID == GameManager.Instance.MapID && !teleportersToExclude.Any((Teleporter j) => (Object)(object)j == (Object)(object)o) && Vector3.Distance(playerPosition, ((Component)o).transform.position) >= minimumDistanceToCoin
			select o).ToList();
		if (list.Count > 0)
		{
			Teleporter val = CollectionsUtil.Grab<Teleporter>(list, 1).First();
			Vector3 position2 = new Vector3(((Component)val).transform.position.x, ((Component)val).transform.position.y + 0.25f, ((Component)val).transform.position.z);
			NetworkObject val2 = runner.Spawn(networkObject, (Vector3?)position2, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
			{
				//IL_0008: Unknown result type (might be due to invalid IL or missing references)
				((Component)no).transform.position = position2;
			}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			((Component)val2).transform.position = position2;
			((Component)val2).GetComponent<MerchantCoin>().Initialize(playerCustom.Ref, val);
			return val;
		}
		return null;
	}

	public static void CreateCoinsOnNewDay(NetworkRunner runner, PlayerCustom playerCustom)
	{
		List<Teleporter> list = new List<Teleporter>();
		int num = BalancingValues.MerchantTotalCoinsOnMap(GameManager.Instance.MapID);
		for (int i = 0; i < num; i++)
		{
			Teleporter val = CreateNewCoin(runner, playerCustom, list);
			if ((Object)(object)val != (Object)null)
			{
				list.Add(val);
			}
		}
	}
}
