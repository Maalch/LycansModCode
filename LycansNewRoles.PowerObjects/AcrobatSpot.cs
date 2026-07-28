using System;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(8)]
public class AcrobatSpot : NetworkBehaviour
{
	private GameObject _visual;

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe float KnockbackPower
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.KnockbackPower. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)(*base.Ptr) * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.KnockbackPower. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr, 999.99994f, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe float GravityDuringJump
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.GravityDuringJump. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)base.Ptr[1] * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.GravityDuringJump. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr + 1, 999.99994f, value);
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe float DirectionX
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.DirectionX. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)base.Ptr[2] * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.DirectionX. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr + 2, 999.99994f, value);
		}
	}

	[Networked]
	[NetworkedWeaved(3, 1)]
	public unsafe float DirectionY
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.DirectionY. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)base.Ptr[3] * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.DirectionY. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr + 3, 999.99994f, value);
		}
	}

	[Networked]
	[NetworkedWeaved(4, 1)]
	public unsafe float DirectionZ
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.DirectionZ. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)base.Ptr[4] * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.DirectionZ. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr + 4, 999.99994f, value);
		}
	}

	[Networked]
	[NetworkedWeaved(5, 1)]
	public unsafe float JumpDuration
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.JumpDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)base.Ptr[5] * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.JumpDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr + 5, 999.99994f, value);
		}
	}

	[Networked]
	[NetworkedWeaved(6, 1)]
	public unsafe float FallSpeedDuringJump
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.FallSpeedDuringJump. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (float)base.Ptr[6] * 0.001f;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing AcrobatSpot.FallSpeedDuringJump. Networked properties can only be accessed when Spawned() has been called.");
			}
			ReadWriteUtilsForWeaver.WriteFloat(base.Ptr + 6, 999.99994f, value);
		}
	}

	private void Awake()
	{
		_visual = ((Component)((Component)this).transform.Find("Visual")).gameObject;
		UpdateVisibility();
	}

	private void UpdateVisibility()
	{
		_visual.SetActive((Object)(object)PlayerCustom.Local != (Object)null && PlayerCustom.Local.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Acrobat);
	}

	public static void UpdateVisibilityForAllSpots()
	{
		AcrobatSpot[] array = Object.FindObjectsOfType<AcrobatSpot>();
		AcrobatSpot[] array2 = array;
		foreach (AcrobatSpot acrobatSpot in array2)
		{
			acrobatSpot.UpdateVisibility();
		}
	}

	public void Init(float knockbackPower, float gravityDuringJump, Vector3 direction, float jumpDuration, float fallSpeedDuringJump)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		KnockbackPower = knockbackPower;
		GravityDuringJump = gravityDuringJump;
		DirectionX = direction.x;
		DirectionY = direction.y;
		DirectionZ = direction.z;
		JumpDuration = jumpDuration;
		FallSpeedDuringJump = fallSpeedDuringJump;
	}

	public override void Spawned()
	{
		((NetworkBehaviour)this).Spawned();
		Plugin.Minimap.AddAcrobatSpotIcon(this);
	}
}
