using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using LycansNewRoles.NewItems.Accessories;
using UnityEngine;

namespace LycansNewRoles;

public class FootstepComponent : MonoBehaviour
{
	public enum FootstepType
	{
		Villager,
		Wolf
	}

	public static GameObject FootstepVillagerPrefab;

	public static GameObject FootstepWolfPrefab;

	private GameObject _activator;

	private List<MeshRenderer> _meshRenderers;

	private PlayerRef _owner;

	private FootstepType _type;

	private int _lifetimeMilliseconds;

	private Stopwatch _stopwatchToDisappear = new Stopwatch();

	private Stopwatch _stopwatchToUpdate = new Stopwatch();

	public PlayerRef Owner => _owner;

	public void Init(PlayerRef owner, FootstepType type)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			_owner = owner;
			_activator = ((Component)((Component)this).transform.GetChild(0)).gameObject;
			_meshRenderers = _activator.GetComponentsInChildren<MeshRenderer>().ToList();
			_type = type;
			_lifetimeMilliseconds = ((_type == FootstepType.Wolf) ? 90000 : 10000);
			_stopwatchToDisappear.Start();
			_stopwatchToUpdate.Start();
			PlayerRef val = PlayerController.Local.Ref;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(val);
			UpdateVisibility(_owner != val && !NetworkBool.op_Implicit(PlayerController.Local.IsDead) && player.Accessory is AccessoryMagnifier);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("FootstepComponent Init error: " + ex));
		}
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		if ((int)GameManager.LocalGameState != 2)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else if (_stopwatchToUpdate.ElapsedMilliseconds >= 500)
		{
			UpdateFootstep();
			_stopwatchToUpdate.Restart();
		}
	}

	private void UpdateFootstep()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		long elapsedMilliseconds = _stopwatchToDisappear.ElapsedMilliseconds;
		if (elapsedMilliseconds >= _lifetimeMilliseconds)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
			return;
		}
		float a = 1f - (float)elapsedMilliseconds / (float)_lifetimeMilliseconds;
		foreach (MeshRenderer meshRenderer in _meshRenderers)
		{
			Color color = ((Renderer)meshRenderer).material.color;
			color.a = a;
			((Renderer)meshRenderer).material.color = color;
		}
	}

	public void UpdateVisibility(bool visible)
	{
		try
		{
			_activator.SetActive(visible);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("FootstepComponent UpdateVisibility error: " + ex));
		}
	}
}
