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
public class InvestigatorHint : NetworkBehaviour
{
	public static List<InvestigatorHint> AllHints = new List<InvestigatorHint>();

	private GameObject _visual;

	private GameObject _hint;

	private PlayerCustom _creatorCustom;

	public Teleporter AssociatedTeleporter;

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
				throw new InvalidOperationException("Error when accessing InvestigatorTarget.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing InvestigatorTarget.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
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
				throw new InvalidOperationException("Error when accessing InvestigatorTarget.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing InvestigatorTarget.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
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

	public void Init(PlayerRef playerRef, Teleporter teleporter, int duration)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
		AssociatedTeleporter = teleporter;
		RemainingDuration = duration;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<InvestigatorHint> changed)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			AllHints.Add(changed.Behaviour);
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			if (changed.Behaviour._creatorCustom.IsCurrentlyPlayedOrObserved)
			{
				changed.Behaviour._creatorCustom.UpdateTargetArrowComponent();
				Plugin.Minimap.AddInvestigatorTargetIcon(changed.Behaviour);
				AudioManager.Play("PowerAvailable", (MixerTarget)2, 0.35f, 1f);
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(CreatorRef);
		_visual.SetActive(player.IsCurrentlyPlayedOrObserved);
	}

	public static void UpdateVisibilityForAllHints()
	{
		foreach (InvestigatorHint allHint in AllHints)
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
				player.PrimaryRolePowerCurrentMaterials += 25;
				PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, player.Index, 12);
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	public static Teleporter CreateNewHint(NetworkRunner runner, PlayerCustom playerCustom, List<Teleporter> teleportersToExclude)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		teleportersToExclude.AddRange(from o in AllHints
			where o.CreatorRef == playerCustom.Ref
			select o.AssociatedTeleporter);
		try
		{
			float minimumDistanceFromPlayer = 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
			float minimumDistanceFromOtherHints = 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectInvestigatorHint");
			Vector3 playerPosition = playerCustom.ActualPositionIncludingTeleport();
			List<Teleporter> list = (from o in Object.FindObjectsOfType<Teleporter>()
				where o.MapID == GameManager.Instance.MapID && !teleportersToExclude.Any((Teleporter j) => (Object)(object)j == (Object)(object)o || Vector3.Distance(((Component)j).transform.position, ((Component)o).transform.position) < minimumDistanceFromOtherHints) && Vector3.Distance(playerPosition, ((Component)o).transform.position) >= minimumDistanceFromPlayer
				select o).ToList();
			if (list.Count > 0)
			{
				Teleporter val = CollectionsUtil.Grab<Teleporter>(list, 1).First();
				Vector3 position = new Vector3(((Component)val).transform.position.x, ((Component)val).transform.position.y + 0.25f, ((Component)val).transform.position.z);
				NetworkObject val2 = runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					//IL_0008: Unknown result type (might be due to invalid IL or missing references)
					((Component)no).transform.position = position;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				((Component)val2).transform.position = position;
				float num = Vector3.Distance(position, playerPosition);
				float num2 = Mathf.InverseLerp(30f, 100f, num);
				float num3 = Mathf.Lerp(30f, 100f, num2);
				float value = Random.value;
				float num4 = Mathf.Lerp(0.7f, 1.3f, value);
				num3 *= num4;
				((Component)val2).GetComponent<InvestigatorHint>().Init(playerCustom.Ref, val, Mathf.RoundToInt(num3));
				return val;
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Exception in CreateNewHint: " + ex));
			if (teleportersToExclude.Any((Teleporter o) => (Object)(object)o == (Object)null))
			{
				Plugin.Logger.LogError((object)"-> Some teleporters are null");
			}
			if (teleportersToExclude.Any((Teleporter o) => (Object)(object)((Component)o).transform == (Object)null))
			{
				Plugin.Logger.LogError((object)"-> Some teleporter transforms are null");
				bool flag = teleportersToExclude.Any((Teleporter o) => (Object)(object)((Component)o).transform == (Object)null && AllHints.Any((InvestigatorHint j) => (Object)(object)j == (Object)(object)o));
				Plugin.Logger.LogError((object)("-> From hints: " + flag));
			}
		}
		return null;
	}

	public static void CreateHintsOnNewDay(NetworkRunner runner, PlayerCustom playerCustom)
	{
		List<Teleporter> list = new List<Teleporter>();
		for (int i = 0; i < 3; i++)
		{
			Teleporter val = CreateNewHint(runner, playerCustom, list);
			if ((Object)(object)val != (Object)null)
			{
				list.Add(val);
			}
		}
		playerCustom.TriggerPrimaryRolePowerCooldown(runner);
	}
}
