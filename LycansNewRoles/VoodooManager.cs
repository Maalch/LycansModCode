using System;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(20)]
public class VoodooManager : NetworkBehaviour
{
	[Networked(OnChanged = "VoodooActiveChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe NetworkBool VoodooActive
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing VoodooManager.VoodooActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing VoodooManager.VoodooActive. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkBool VoodooTriggered
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing VoodooManager.VoodooTriggered. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing VoodooManager.VoodooTriggered. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	public static VoodooManager Instance { get; private set; }

	public void Reset()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		VoodooTriggered = NetworkBool.op_Implicit(false);
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

	public void ActivateVoodoo()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		VoodooActive = NetworkBool.op_Implicit(true);
		VoodooTriggered = NetworkBool.op_Implicit(true);
		foreach (PlayerController item in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead))))
		{
			item.IsWolf = NetworkBool.op_Implicit(false);
			item.Hunger = GameManager.Instance.MaxHunger;
		}
		foreach (PlayerCustom item2 in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.Kidnapped)))
		{
			item2.Kidnapped = NetworkBool.op_Implicit(false);
		}
	}

	[Preserve]
	public static void VoodooActiveChanged(Changed<VoodooManager> changed)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			VoodooManager behaviour = changed.Behaviour;
			if (!NetworkBool.op_Implicit(behaviour.VoodooActive))
			{
				return;
			}
			AudioManager.Play("VoodooRez", (MixerTarget)2, 1f, 1f);
			PlayerCustom specificNewPrimaryRole = PlayerCustomRegistry.GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Voodoo);
			if (((SimulationBehaviour)behaviour).Runner.IsServer)
			{
				specificNewPrimaryRole.PlayerController.Hunger = GameManager.Instance.MaxHunger;
				specificNewPrimaryRole.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)behaviour).Runner, 3f);
			}
			PlayerCustom local = PlayerCustom.Local;
			if (!NetworkBool.op_Implicit(local.PlayerController.IsDead) || local.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
			{
				if (local.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Voodoo)
				{
					UIManager.ShowRedCenterMessage("NALES_UI_VOODOO_HUNT_VOODOO", 0.5f, 4f);
				}
				else if (local.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
				{
					UIManager.ShowRedCenterMessage("NALES_UI_VOODOO_HUNT_ZOMBIE", 0.5f, 4f);
				}
				else
				{
					UIManager.ShowRedCenterMessage("NALES_UI_VOODOO_HUNT_OTHERS", 0.5f, 4f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ColorIndexChanged error: " + ex));
		}
	}
}
