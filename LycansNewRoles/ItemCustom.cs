using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(5)]
public class ItemCustom : NetworkBehaviour
{
	private Light _light = null;

	private Item _item = null;

	[Networked(OnChanged = "SabotagedChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe NetworkBool Sabotaged
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing ItemCustom.Sabotaged. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing ItemCustom.Sabotaged. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked(OnChanged = "ItemNetworkIdChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe NetworkId ItemNetworkId
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing ItemCustom.ItemNetworkId. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkId)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing ItemCustom.ItemNetworkId. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	[Preserve]
	public static void ItemNetworkIdChanged(Changed<ItemCustom> changed)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			NetworkObject val = ((SimulationBehaviour)changed.Behaviour).Runner.FindObject(changed.Behaviour.ItemNetworkId);
			if ((Object)(object)val == (Object)null)
			{
				((SimulationBehaviour)changed.Behaviour).Runner.Despawn(((Component)changed.Behaviour).GetComponent<NetworkObject>(), false);
				return;
			}
			((Component)changed.Behaviour).transform.parent = ((Component)val).transform;
			changed.Behaviour.Initialize(((Component)val).GetComponent<Item>());
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ItemNetworkIdChanged error: " + ex));
		}
	}

	[Preserve]
	public static void SabotagedChanged(Changed<ItemCustom> changed)
	{
		try
		{
			changed.Behaviour.UpdateLight();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ItemNetworkIdChanged error: " + ex));
		}
	}

	public void Initialize(Item item)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		_item = item;
		if ((Object)(object)_light == (Object)null)
		{
			_light = ((Component)item).gameObject.AddComponent<Light>();
			_light.renderingLayerMask = LayerMask.NameToLayer("NoInteract");
			_light.color = Color.green;
			((Behaviour)_light).enabled = false;
			UpdateLight();
		}
	}

	public void UpdateLight()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_light == (Object)null)
		{
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		PlayerRef owner;
		if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Saboteur)
		{
			owner = _item.Owner;
			if (((PlayerRef)(ref owner)).IsNone)
			{
				_light.intensity = 2f;
				_light.range = 2f;
			}
			else
			{
				_light.intensity = 0.5f;
				_light.range = 0.15f;
			}
			_light.color = (NetworkBool.op_Implicit(Sabotaged) ? Color.red : Color.green);
			((Behaviour)_light).enabled = true;
			return;
		}
		if (NetworkBool.op_Implicit(player.PlayerController.PlayerEffectManager.NightVision))
		{
			owner = _item.Owner;
			if (((PlayerRef)(ref owner)).IsNone)
			{
				_light.intensity = 2f;
				_light.range = 2f;
				_light.color = Color.blue;
				((Behaviour)_light).enabled = true;
				return;
			}
		}
		((Behaviour)_light).enabled = false;
	}

	public static void UpdateAllItems()
	{
		List<ItemCustom> list = Object.FindObjectsOfType<ItemCustom>().ToList();
		foreach (ItemCustom item in list)
		{
			item.UpdateLight();
		}
	}
}
