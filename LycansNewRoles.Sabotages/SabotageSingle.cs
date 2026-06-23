using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewMaps.Components;
using LycansNewRoles.Stats;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.Sabotages;

[NetworkBehaviourWeaved(20)]
public class SabotageSingle : NetworkBehaviour
{
	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe int SabotageId
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageId. Networked properties can only be accessed when Spawned() has been called.");
			}
			return *base.Ptr;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.SabotageId. Networked properties can only be accessed when Spawned() has been called.");
			}
			*base.Ptr = value;
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe int AmountRequired
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageSingle.AmountRequired. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageSingle.AmountRequired. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	[Networked]
	[NetworkedWeaved(2, 1)]
	public unsafe int AmountCurrent
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageSingle.AmountCurrent. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[2];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageSingle.AmountCurrent. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[2] = value;
		}
	}

	[Networked(OnChanged = "ActiveChanged")]
	[NetworkedWeaved(3, 1)]
	public unsafe NetworkBool Active
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageSingle.Active. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)base.Ptr[3];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageSingle.Active. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 3, value);
		}
	}

	public bool Completed => AmountCurrent >= AmountRequired;

	public override void Spawned()
	{
		((NetworkBehaviour)this).Spawned();
		try
		{
			SabotageManager.Instance.Sabotages[(SabotageManager.SabotageIds)SabotageId] = this;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SabotageSingle spawned error: " + ex));
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		RemoveSabotageEffects();
		((NetworkBehaviour)this).Despawned(runner, hasState);
	}

	public void Init(SabotageManager.SabotageIds sabotageId, int amountRequired)
	{
		SabotageId = (int)sabotageId;
		AmountRequired = amountRequired;
		AmountCurrent = 0;
	}

	public void AddStep()
	{
		AmountCurrent++;
	}

	public void RemoveStep()
	{
		AmountCurrent--;
	}

	[Preserve]
	public static void ActiveChanged(Changed<SabotageSingle> changed)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(changed.Behaviour.Active))
			{
				string key;
				switch ((SabotageManager.SabotageIds)changed.Behaviour.SabotageId)
				{
				case SabotageManager.SabotageIds.WellPoison:
					key = "NALES_UI_SABOTAGE_WELL_POISON_MESSAGE";
					if (!((SimulationBehaviour)changed.Behaviour).Runner.IsServer)
					{
						break;
					}
					foreach (PlayerController item in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))))
					{
						PlayerCustom.ApplyEffectToPlayer(item, "LycansNewRoles.EffectDiseased", ((SimulationBehaviour)item).Runner);
					}
					break;
				case SabotageManager.SabotageIds.WolvesRitual:
					key = "NALES_UI_SABOTAGE_RITUAL_MESSAGE";
					break;
				case SabotageManager.SabotageIds.Portals:
					key = "NALES_UI_SABOTAGE_PORTALS_PORTAL_MESSAGE";
					CollectionsUtil.ForEach<Portal>(Object.FindObjectsOfType<Portal>(), (Action<Portal>)delegate(Portal portal)
					{
						//IL_0018: Unknown result type (might be due to invalid IL or missing references)
						Light value = Traverse.Create((object)portal).Field<Light>("teleportLight").Value;
						value.color = Color.red;
						portal.ResetPortal();
					});
					break;
				case SabotageManager.SabotageIds.CursedNight:
					key = "NALES_UI_SABOTAGE_CURSED_NIGHT_MESSAGE";
					if (((SimulationBehaviour)changed.Behaviour).Runner.IsServer)
					{
						SabotageManager.Instance.StartCursedNightTimer();
					}
					break;
				case SabotageManager.SabotageIds.LaboratoryAutodoors:
				{
					key = "NALES_UI_SABOTAGE_LABORATORY_AUTODOORS_MESSAGE";
					AutodoorCustom[] array = Object.FindObjectsOfType<AutodoorCustom>(false);
					AutodoorCustom[] array2 = array;
					foreach (AutodoorCustom autodoorCustom in array2)
					{
						((Component)((Component)autodoorCustom).transform.Find("Light")).GetComponent<Light>().color = Color.red;
					}
					break;
				}
				case SabotageManager.SabotageIds.LaboratoryLights:
					key = "NALES_UI_SABOTAGE_LABORATORY_BLACKOUT_MESSAGE";
					break;
				default:
					throw new NotImplementedException();
				}
				UIManager.ShowRedCenterMessage(key, 0.35f, 5f);
				if (((SimulationBehaviour)changed.Behaviour).Runner.IsServer)
				{
					SessionStats.Stats.CurrentGame.AddEvent(GameEvent.GameEventType.SabotageActive, ((SabotageManager.SabotageIds)changed.Behaviour.SabotageId/*cast due to constrained. prefix*/).ToString());
				}
				return;
			}
			changed.Behaviour.AmountCurrent = 0;
			foreach (SabotageObject item2 in SabotageManager.Instance.SabotageObjectsByIndex.Values.Where((SabotageObject o) => o.SabotageId == changed.Behaviour.SabotageId))
			{
				item2.Completed = NetworkBool.op_Implicit(false);
			}
			changed.Behaviour.RemoveSabotageEffects();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ActiveChanged error: " + ex));
		}
	}

	private void RemoveSabotageEffects()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		switch ((SabotageManager.SabotageIds)SabotageId)
		{
		case SabotageManager.SabotageIds.Portals:
			CollectionsUtil.ForEach<Portal>(Object.FindObjectsOfType<Portal>(), (Action<Portal>)delegate(Portal portal)
			{
				//IL_002c: Unknown result type (might be due to invalid IL or missing references)
				Light value = Traverse.Create((object)portal).Field<Light>("teleportLight").Value;
				value.color = new Color(0f, 0.744f, 1f, 1f);
			});
			break;
		case SabotageManager.SabotageIds.LaboratoryAutodoors:
		{
			AutodoorCustom[] array = Object.FindObjectsOfType<AutodoorCustom>(false);
			AutodoorCustom[] array2 = array;
			foreach (AutodoorCustom autodoorCustom in array2)
			{
				((Component)((Component)autodoorCustom).transform.Find("Light")).GetComponent<Light>().color = Color.green;
			}
			break;
		}
		}
	}
}
