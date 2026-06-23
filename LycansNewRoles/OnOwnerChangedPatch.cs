using System;
using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Item), "OnOwnerChanged")]
internal class OnOwnerChangedPatch
{
	private static void Postfix(Changed<Item> changed)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (changed.Behaviour.Owner == PlayerRef.None)
			{
				((Component)changed.Behaviour).transform.SetParent((Transform)null);
				Traverse.Create((object)changed.Behaviour).Field<Collider>("itemCollider").Value.enabled = true;
				MeshRenderer[] value = Traverse.Create((object)changed.Behaviour).Field<MeshRenderer[]>("meshRenderers").Value;
				for (int i = 0; i < value.Length; i++)
				{
					((Renderer)value[i]).enabled = true;
				}
			}
			if (changed.Behaviour is Accessory)
			{
				Light component = ((Component)((Component)changed.Behaviour).transform.Find("Light")).GetComponent<Light>();
				((Behaviour)component).enabled = changed.Behaviour.Owner == PlayerRef.None;
				if (changed.Behaviour is AccessoryMagnifier)
				{
					PlayerRef owner = changed.Behaviour.Owner;
					changed.LoadOld();
					PlayerRef owner2 = changed.Behaviour.Owner;
					PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
					if (owner == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref || owner2 == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref)
					{
						FootstepComponent[] array = Object.FindObjectsOfType<FootstepComponent>();
						foreach (FootstepComponent footstepComponent in array)
						{
							footstepComponent.UpdateVisibility(owner == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
						}
					}
					changed.LoadNew();
				}
				if (changed.Behaviour is AccessoryCrystalBall)
				{
					PlayerRef owner3 = changed.Behaviour.Owner;
					changed.LoadOld();
					PlayerRef owner4 = changed.Behaviour.Owner;
					PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
					if (owner3 == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref || owner4 == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref)
					{
						foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf)))
						{
							item.UpdateVisibility();
						}
					}
					changed.LoadNew();
				}
			}
			Item behaviour = changed.Behaviour;
			Item val = behaviour;
			if (!(val is MagicScrollItem))
			{
				if (!(val is PhasingDiamondItem))
				{
					if (!(val is GrenadeItem))
					{
						if (!(val is SleepingGasItem))
						{
							if (!(val is MolotovItem))
							{
								if (!(val is RadarItem))
								{
									if (val is Accessory && changed.Behaviour.Owner == PlayerController.Local.Ref && !PlayerPrefs.HasKey("NALES_TUTORIAL_ACCESSORIES"))
									{
										UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_ACCESSORIES", 0.4f, 5f);
										PlayerPrefs.SetInt("NALES_TUTORIAL_ACCESSORIES", 1);
									}
								}
								else if (changed.Behaviour.Owner == PlayerController.Local.Ref && !PlayerPrefs.HasKey("NALES_TUTORIAL_RADAR"))
								{
									UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_RADAR", 0.4f, 5f);
									PlayerPrefs.SetInt("NALES_TUTORIAL_RADAR", 1);
								}
							}
							else
							{
								Light component2 = ((Component)((Component)changed.Behaviour).transform.Find("MolotovLight")).GetComponent<Light>();
								((Behaviour)component2).enabled = changed.Behaviour.Owner == PlayerRef.None;
								if (changed.Behaviour.Owner == PlayerController.Local.Ref && !PlayerPrefs.HasKey("NALES_TUTORIAL_MOLOTOV"))
								{
									UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_MOLOTOV", 0.4f, 5f);
									PlayerPrefs.SetInt("NALES_TUTORIAL_MOLOTOV", 1);
								}
							}
						}
						else if (changed.Behaviour.Owner == PlayerController.Local.Ref && !PlayerPrefs.HasKey("NALES_TUTORIAL_SLEEPING_GAS"))
						{
							UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_SLEEPING_GAS", 0.4f, 5f);
							PlayerPrefs.SetInt("NALES_TUTORIAL_SLEEPING_GAS", 1);
						}
					}
					else if (changed.Behaviour.Owner == PlayerController.Local.Ref && !PlayerPrefs.HasKey("NALES_TUTORIAL_GRENADE"))
					{
						UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_GRENADE", 0.4f, 5f);
						PlayerPrefs.SetInt("NALES_TUTORIAL_GRENADE", 1);
					}
				}
				else
				{
					Light component3 = ((Component)((Component)changed.Behaviour).transform.Find("PhasingDiamondLight")).GetComponent<Light>();
					((Behaviour)component3).enabled = changed.Behaviour.Owner == PlayerRef.None;
					if (changed.Behaviour.Owner == PlayerController.Local.Ref && !PlayerPrefs.HasKey("NALES_TUTORIAL_PHASING_DIAMOND"))
					{
						UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_PHASING_DIAMOND", 0.4f, 6f);
						PlayerPrefs.SetInt("NALES_TUTORIAL_PHASING_DIAMOND", 1);
					}
				}
			}
			else
			{
				Light component4 = ((Component)((Component)changed.Behaviour).transform.Find("MagicScrollLight")).GetComponent<Light>();
				((Behaviour)component4).enabled = changed.Behaviour.Owner == PlayerRef.None;
				if (changed.Behaviour.Owner == PlayerController.Local.Ref && !PlayerPrefs.HasKey("NALES_TUTORIAL_MAGIC_SCROLL"))
				{
					UIManager.ShowRedCenterMessage("NALES_UI_TUTORIAL_MAGIC_SCROLL", 0.4f, 6f, new List<object> { LycansUtility.GetInputDisplayCustom((InputActionName)11) });
					PlayerPrefs.SetInt("NALES_TUTORIAL_MAGIC_SCROLL", 1);
				}
			}
			if (changed.Behaviour is AccessoryBackpack accessoryBackpack && (Object)(object)accessoryBackpack.ItemInside != (Object)null)
			{
				((NetworkBehaviour)accessoryBackpack.ItemInside).CopyStateToBackingFields();
				Traverse.Create((object)accessoryBackpack.ItemInside).Field("_Owner").SetValue((object)((Item)accessoryBackpack).Owner);
				((NetworkBehaviour)accessoryBackpack.ItemInside).CopyBackingFieldsToState(true);
				((SimulationBehaviour)accessoryBackpack.ItemInside).Object.AssignInputAuthority(accessoryBackpack.ItemInside.Owner);
			}
			if ((Object)(object)PlayerController.Local != (Object)null)
			{
				ItemCustom componentInChildren = ((Component)changed.Behaviour).GetComponentInChildren<ItemCustom>();
				if ((Object)(object)componentInChildren != (Object)null)
				{
					componentInChildren.UpdateLight();
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("OnOwnerChangedPatch error: " + ex));
		}
	}
}
