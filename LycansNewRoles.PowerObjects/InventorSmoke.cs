using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(3)]
public class InventorSmoke : NetworkBehaviour
{
	public static GameObject InventorSmokeForInventorPrefab;

	public static GameObject InventorSmokeForOthersPrefab;

	private PlayerCustom _creatorCustom;

	private ParticleSystem _visual;

	private Stopwatch _disappearStopwatch = new Stopwatch();

	private Stopwatch _deleteStopwatch = new Stopwatch();

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
				throw new InvalidOperationException("Error when accessing InventorSmoke.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing InventorSmoke.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "DisappearingChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool Disappearing
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing InventorSmoke.Disappearing. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing InventorSmoke.Disappearing. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	public void Awake()
	{
		_visual = ((Component)((Component)this).transform.Find("Visual")).GetComponent<ParticleSystem>();
		_visual.Stop();
	}

	public override void Spawned()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		Disappearing = NetworkBool.op_Implicit(false);
		UpdateVisibility();
		if (((SimulationBehaviour)this).Runner.IsServer)
		{
			_disappearStopwatch.Start();
			_deleteStopwatch.Stop();
		}
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<InventorSmoke> changed)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			changed.Behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreatorRefChanged error: " + ex));
		}
	}

	[Preserve]
	public static void DisappearingChanged(Changed<InventorSmoke> changed)
	{
		changed.Behaviour.UpdateVisibility();
	}

	private void UpdateVisibility()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(Disappearing))
		{
			_visual.Stop();
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (NetworkBool.op_Implicit(player.PlayerController.IsWolf) || player.Ref == CreatorRef)
		{
			_visual.Play();
			ColorOverLifetimeModule colorOverLifetime = _visual.colorOverLifetime;
			MainModule main = _visual.main;
			ColorOverLifetimeModule colorOverLifetime2;
			if (player.Ref == CreatorRef)
			{
				colorOverLifetime2 = ((Component)InventorSmokeForInventorPrefab.transform.Find("Visual")).GetComponent<ParticleSystem>().colorOverLifetime;
				((ColorOverLifetimeModule)(ref colorOverLifetime)).color = ((ColorOverLifetimeModule)(ref colorOverLifetime2)).color;
			}
			else
			{
				colorOverLifetime2 = ((Component)InventorSmokeForOthersPrefab.transform.Find("Visual")).GetComponent<ParticleSystem>().colorOverLifetime;
				((ColorOverLifetimeModule)(ref colorOverLifetime)).color = ((ColorOverLifetimeModule)(ref colorOverLifetime2)).color;
			}
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Invalid comparison between Unknown and I4
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Invalid comparison between Unknown and I4
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
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
			if (NetworkBool.op_Implicit(Disappearing) && _deleteStopwatch.ElapsedMilliseconds >= 3000)
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				return;
			}
		}
		if (((SimulationBehaviour)this).Runner.IsServer && !NetworkBool.op_Implicit(Disappearing) && (float)_disappearStopwatch.ElapsedMilliseconds >= 4000f)
		{
			Disappearing = NetworkBool.op_Implicit(true);
			_disappearStopwatch.Stop();
			_deleteStopwatch.Restart();
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
