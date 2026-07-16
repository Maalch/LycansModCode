using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using Helpers.Collections;
using UnityEngine;

namespace LycansNewRoles;

public class EventsManager
{
	public enum EventType
	{
		None,
		Harvest,
		Haste,
		Fog,
		Spellstorm,
		Eclipse,
		Plague,
		Tournament,
		FullMoon,
		Rage,
		Vengeance
	}

	public EventType CurrentEvent = EventType.None;

	public bool CurrentEventUniqueBool = false;

	private Stopwatch _currentEventStopwatch = new Stopwatch();

	private int _currentEventUniqueInt = 0;

	public List<EventType> GameEventsHistory = new List<EventType>();

	public void NewEvent(EventType eventType)
	{
		LycansUtility.AddLogOnlyForMe("NewEvent: " + eventType);
		CurrentEvent = eventType;
		EventType currentEvent = CurrentEvent;
		EventType eventType2 = currentEvent;
		if (eventType2 == EventType.Fog)
		{
			_currentEventStopwatch.Restart();
			_currentEventUniqueInt = Mathf.RoundToInt(Random.Range(50f, 90f) / 2f);
		}
		GameEventsHistory.Add(eventType);
	}

	public static bool IsEventDisabled(EventType eventType)
	{
		return false;
	}

	public void ClearEvent()
	{
		if (CurrentEvent == EventType.None)
		{
			return;
		}
		LycansUtility.AddLogOnlyForMe("ClearEvent");
		CurrentEvent = EventType.None;
		CurrentEventUniqueBool = false;
		_currentEventStopwatch.Reset();
		_currentEventUniqueInt = 0;
		foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
		{
			allPlayer.UpdateVisibility();
			allPlayer.UpdateMoveSpeed();
		}
	}

	public void RollEvent()
	{
		if (CanGenerateEvent() && Random.value * 100f < (float)Plugin.CustomConfig.EventChance && Plugin.CustomConfig.EventsActive.Any((KeyValuePair<EventType, bool> o) => o.Value))
		{
			List<EventType> list = (from o in Plugin.CustomConfig.EventsActive
				where o.Value && o.Key != EventType.None && !GameEventsHistory.Contains(o.Key) && !IsEventDisabled(o.Key)
				select o.Key).ToList();
			if (GameManagerCustom.Instance.CurrentDay == 0)
			{
				list.Remove(EventType.Eclipse);
			}
			if (list.Any())
			{
				EventType eventIndex = CollectionsUtil.Grab<EventType>(list, 1).First();
				GameManagerCustom.Rpc_New_Event(((SimulationBehaviour)GameManagerCustom.Instance).Runner, (int)eventIndex);
			}
		}
	}

	private bool CanGenerateEvent()
	{
		if (PlayerCustomRegistry.Any((PlayerCustom o) => (o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary && NetworkBool.op_Implicit(o.NewPrimaryRoleUniqueBool)) || (o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist && NetworkBool.op_Implicit(o.NewPrimaryRoleUniqueBool))))
		{
			return false;
		}
		return true;
	}

	public void OnNightStarted()
	{
		EventType currentEvent = CurrentEvent;
		EventType eventType = currentEvent;
		if (eventType == EventType.Rage)
		{
			_currentEventStopwatch.Restart();
		}
	}

	public void ServerUpdate(NetworkRunner runner)
	{
		switch (CurrentEvent)
		{
		case EventType.Fog:
			if (_currentEventStopwatch.ElapsedMilliseconds < _currentEventUniqueInt * 1000)
			{
				break;
			}
			foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead)))
			{
				PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectStealthing", runner, 1f, 30f);
			}
			_currentEventStopwatch.Restart();
			_currentEventUniqueInt = Mathf.RoundToInt(Random.Range(50f, 90f));
			break;
		case EventType.Rage:
			if (!CurrentEventUniqueBool && (float)_currentEventStopwatch.ElapsedMilliseconds >= 20000f)
			{
				List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => (int)o.PlayerController.Role == 1 && !NetworkBool.op_Implicit(o.IsWolfPup) && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !o.IsOutOfTheWorld).ToList();
				if (list.Any())
				{
					PlayerCustom playerCustom = CollectionsUtil.Grab<PlayerCustom>(list, 1).First();
					PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)GameManager.Instance).Runner, playerCustom.Index, 16);
				}
				_currentEventStopwatch.Reset();
			}
			break;
		}
	}

	public void EventUniqueMethod()
	{
		EventType currentEvent = CurrentEvent;
		EventType eventType = currentEvent;
		if (eventType == EventType.Rage)
		{
			CurrentEventUniqueBool = true;
			_currentEventStopwatch.Reset();
		}
	}
}
