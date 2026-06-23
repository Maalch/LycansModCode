using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using Helpers.Collections;
using LycansNewRoles.PowerObjects;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(20)]
public class CultistManager : NetworkBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static OnBeforeSpawned _003C_003E9__17_0;

		public static Func<Teleporter, bool> _003C_003E9__17_1;

		public static Predicate<PlayerCustom> _003C_003E9__17_2;

		public static Predicate<PlayerController> _003C_003E9__19_0;

		internal void _003CFixedUpdateNetwork_003Eb__17_0(NetworkRunner _, NetworkObject no)
		{
		}

		internal bool _003CFixedUpdateNetwork_003Eb__17_1(Teleporter o)
		{
			return o.MapID == GameManager.Instance.MapID;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__17_2(PlayerCustom o)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Cultist && !o.IsOutOfTheWorld && !NetworkBool.op_Implicit(o.CapturedByCultist);
		}

		internal bool _003CCultistActiveChanged_003Eb__19_0(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.IsDead);
		}
	}

	private Stopwatch _skullCreationStopwatch = new Stopwatch();

	[Networked(OnChanged = "CultistActiveChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe NetworkBool CultistActive
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CultistActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CultistActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "CultistCapturesChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe int CultistCaptures
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CultistCaptures. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CultistCaptures. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe int CultistCapturesObjective
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CultistCapturesObjective. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[2];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CultistCapturesObjective. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[2] = value;
		}
	}

	public static CultistManager Instance { get; private set; }

	public void Reset()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		CultistActive = NetworkBool.op_Implicit(false);
		CultistCaptures = 0;
		CultistCapturesObjective = 0;
	}

	public override void Spawned()
	{
		((NetworkBehaviour)this).Spawned();
		Instance = this;
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Instance = null;
	}

	public override void FixedUpdateNetwork()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).Runner.IsServer || !NetworkBool.op_Implicit(CultistActive) || !((float)_skullCreationStopwatch.ElapsedMilliseconds >= 6000f) || !LycansUtility.GameActuallyInPlay)
		{
			return;
		}
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectCultistSkullSpirit");
		NetworkRunner runner = ((SimulationBehaviour)GameManager.Instance).Runner;
		Vector3? val = ((Component)this).transform.position;
		Quaternion? val2 = Quaternion.identity;
		object obj = _003C_003Ec._003C_003E9__17_0;
		if (obj == null)
		{
			OnBeforeSpawned val3 = delegate
			{
			};
			_003C_003Ec._003C_003E9__17_0 = val3;
			obj = (object)val3;
		}
		NetworkObject val4 = runner.Spawn(networkObject, val, val2, (PlayerRef?)null, (OnBeforeSpawned)obj, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		((Component)val4).GetComponent<CultistSkullSpirit>().SetCreatorRef(PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Cultist).Ref);
		Vector3 position;
		if (Random.value <= 0.25f)
		{
			List<Teleporter> source = (from o in Object.FindObjectsOfType<Teleporter>()
				where o.MapID == GameManager.Instance.MapID
				select o).ToList();
			Teleporter val5 = source.First();
			position = ((Component)val5).transform.position;
		}
		else
		{
			List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Cultist && !o.IsOutOfTheWorld && !NetworkBool.op_Implicit(o.CapturedByCultist)).ToList();
			PlayerCustom playerCustom = CollectionsUtil.Grab<PlayerCustom>(list, 1).First();
			position = ((Component)playerCustom.PlayerController).transform.position;
		}
		((Component)val4).transform.position = position;
		GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("VoodooRez"), position, 20f, 0.6f);
		_skullCreationStopwatch.Restart();
	}

	public void ActivateCultist()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		CultistActive = NetworkBool.op_Implicit(true);
		_skullCreationStopwatch.Restart();
	}

	[Preserve]
	public static void CultistActiveChanged(Changed<CultistManager> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			CultistManager behaviour = changed.Behaviour;
			if (NetworkBool.op_Implicit(behaviour.CultistActive))
			{
				int cultistCapturesObjective = BalancingValues.CultistTargetAmount(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) - 1);
				PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Cultist);
				if (((SimulationBehaviour)behaviour).Runner.IsServer)
				{
					behaviour.CultistCapturesObjective = cultistCapturesObjective;
					specificNewPrimaryRole.PlayerController.Hunger = GameManager.Instance.MaxHunger;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ColorIndexChanged error: " + ex));
		}
	}

	[Preserve]
	public static void CultistCapturesChanged(Changed<CultistManager> changed)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist)
			{
				player.UpdateDescriptionStatusIfNeeded();
			}
			CultistManager behaviour = changed.Behaviour;
			if (NetworkBool.op_Implicit(behaviour.CultistActive) && ((SimulationBehaviour)behaviour).Runner.IsServer && behaviour.CultistCaptures >= behaviour.CultistCapturesObjective)
			{
				PlayerCustom.Rpc_End_Game(((SimulationBehaviour)behaviour).Runner, PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Cultist).Index);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CultistCapturesChanged error: " + ex));
		}
	}
}
