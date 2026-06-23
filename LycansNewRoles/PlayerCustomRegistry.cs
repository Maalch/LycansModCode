using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BepInEx.Logging;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(88)]
public class PlayerCustomRegistry : NetworkBehaviour, INetworkRunnerCallbacks, IElementReaderWriter<PlayerCustom>
{
	[DefaultForProperty("PlayersCustom", 0, 88)]
	private SerializableDictionary<PlayerRef, PlayerCustom> _PlayersCustom;

	public static PlayerCustomRegistry Instance { get; private set; }

	[Networked]
	[Capacity(15)]
	[NetworkedWeaved(0, 88)]
	private unsafe NetworkDictionary<PlayerRef, PlayerCustom> PlayersCustom
	{
		get
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustomManager.PlayersCustom. Networked properties can only be accessed when Spawned() has been called.");
			}
			return new NetworkDictionary<PlayerRef, PlayerCustom>(base.Ptr, 25, PlayerCustomReaderWriter.GetInstance(), (IElementReaderWriter<PlayerCustom>)this);
		}
	}

	public static List<PlayerCustom> AllPlayers => ((IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)Instance.PlayersCustom).Select((KeyValuePair<PlayerRef, PlayerCustom> o) => o.Value).ToList();

	public override void Spawned()
	{
		((NetworkBehaviour)this).Spawned();
		LycansUtility.DebugLog("BUG2: PlayerCustomRegistry instantiated");
		Instance = this;
		((SimulationBehaviour)this).Runner.AddCallbacks((INetworkRunnerCallbacks[])(object)new INetworkRunnerCallbacks[1] { this });
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		LycansUtility.DebugLog("BUG2: PlayerCustomRegistry removed");
		Instance = null;
		runner.RemoveCallbacks((INetworkRunnerCallbacks[])(object)new INetworkRunnerCallbacks[1] { this });
	}

	private bool GetAvailable(out byte index)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		if (PlayersCustom.Count == 0)
		{
			index = 0;
			return true;
		}
		if (PlayersCustom.Count == 15)
		{
			index = 0;
			return false;
		}
		byte[] array = ((IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)PlayersCustom).OrderBy(delegate(KeyValuePair<PlayerRef, PlayerCustom> kvp)
		{
			KeyValuePair<PlayerRef, PlayerCustom> keyValuePair = kvp;
			return keyValuePair.Value.Index;
		}).Select(delegate(KeyValuePair<PlayerRef, PlayerCustom> kvp)
		{
			KeyValuePair<PlayerRef, PlayerCustom> keyValuePair = kvp;
			return keyValuePair.Value.Index;
		}).ToArray();
		for (int num = 0; num < array.Length - 1; num++)
		{
			if (array[num + 1] > array[num] + 1)
			{
				index = (byte)(array[num] + 1);
				return true;
			}
		}
		index = (byte)(array[^1] + 1);
		return true;
	}

	public unsafe static void Server_Add(NetworkRunner runner, PlayerRef pRef, PlayerCustom pObj)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (Instance.GetAvailable(out var index))
		{
			Instance.PlayersCustom.Add(pRef, pObj);
			pObj.Server_Init(pRef, index);
			PlayerRef val = pRef;
			LycansUtility.DebugLog("BUG2: added PlayerCustom with pRef " + ((object)(*(PlayerRef*)(&val))/*cast due to constrained. prefix*/).ToString() + " and index " + index);
		}
		else
		{
			Plugin.Logger.LogError((object)$"Unable to register player {pRef}");
		}
	}

	public unsafe static void Server_Remove(NetworkRunner runner, PlayerRef pRef)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (!Instance.PlayersCustom.Remove(pRef))
		{
			ManualLogSource logger = Plugin.Logger;
			PlayerRef val = pRef;
			logger.LogError((object)("Could not remove player from registry: " + ((object)(*(PlayerRef*)(&val))/*cast due to constrained. prefix*/).ToString()));
		}
		else
		{
			PlayerRef val = pRef;
			LycansUtility.DebugLog("BUG2: removed PlayerCustom with pRef " + ((object)(*(PlayerRef*)(&val))/*cast due to constrained. prefix*/).ToString());
		}
	}

	public static bool HasPlayer(PlayerRef pRef)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (((PlayerRef)(ref pRef)).IsNone)
		{
			Plugin.Logger.LogError((object)("BUG2: HasPlayer with pRef at None, stacktrace: " + new StackTrace()));
		}
		if ((Object)(object)Instance == (Object)null)
		{
			Plugin.Logger.LogError((object)("BUG2: PlayerCustomRegistry.Instance is null, stacktrace: " + new StackTrace()));
		}
		return Instance.PlayersCustom.ContainsKey(pRef);
	}

	public static bool HasPlayerWithIndex(int index)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return ((IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)Instance.PlayersCustom).Any((KeyValuePair<PlayerRef, PlayerCustom> o) => o.Value.Index == index);
	}

	public static PlayerCustom GetPlayer(int index)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return ((IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)Instance.PlayersCustom).First((KeyValuePair<PlayerRef, PlayerCustom> o) => o.Value.Index == index).Value;
	}

	public static PlayerCustom GetPlayer(PlayerRef pRef)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (HasPlayer(pRef))
		{
			return Instance.PlayersCustom.Get(pRef);
		}
		return null;
	}

	public static PlayerCustom GetAnyVillager()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return ((IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)Instance.PlayersCustom).FirstOrDefault((KeyValuePair<PlayerRef, PlayerCustom> o) => (int)o.Value.PlayerController.Role != 1 && o.Value.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.None).Value;
	}

	public static PlayerCustom GetSpecificNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole newPrimaryRole)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return ((IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)Instance.PlayersCustom).FirstOrDefault((KeyValuePair<PlayerRef, PlayerCustom> o) => o.Value.NewPrimaryRole == newPrimaryRole).Value;
	}

	public static PlayerCustom GetSpecificPrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower primaryRolePower)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return ((IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)Instance.PlayersCustom).FirstOrDefault((KeyValuePair<PlayerRef, PlayerCustom> o) => o.Value.PrimaryRolePower == primaryRolePower).Value;
	}

	public static List<PlayerCustom> GetSpecificSecondaryRoles(PlayerCustom.PlayerSecondaryRole secondaryRole)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return (from o in (IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)Instance.PlayersCustom
			where o.Value.SecondaryRole == secondaryRole
			select o.Value).ToList();
	}

	public static IEnumerable<PlayerCustom> Where(Predicate<PlayerCustom> match)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return ((IEnumerable<KeyValuePair<PlayerRef, PlayerCustom>>)(object)Instance.PlayersCustom).Where(delegate(KeyValuePair<PlayerRef, PlayerCustom> kvp)
		{
			Predicate<PlayerCustom> predicate = match;
			KeyValuePair<PlayerRef, PlayerCustom> keyValuePair = kvp;
			return predicate(keyValuePair.Value);
		}).Select(delegate(KeyValuePair<PlayerRef, PlayerCustom> kvp)
		{
			KeyValuePair<PlayerRef, PlayerCustom> keyValuePair = kvp;
			return keyValuePair.Value;
		});
	}

	public static void ForEach(Action<PlayerCustom> action)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Enumerator<PlayerRef, PlayerCustom> enumerator = Instance.PlayersCustom.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				action(enumerator.Current.Value);
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
	}

	public static void ForEachWhere(Predicate<PlayerCustom> match, Action<PlayerCustom> action)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Enumerator<PlayerRef, PlayerCustom> enumerator = Instance.PlayersCustom.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<PlayerRef, PlayerCustom> current = enumerator.Current;
				if (match(current.Value))
				{
					action(current.Value);
				}
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
	}

	public static int CountWhere(Predicate<PlayerCustom> match)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		Enumerator<PlayerRef, PlayerCustom> enumerator = Instance.PlayersCustom.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				if (match(enumerator.Current.Value))
				{
					num++;
				}
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
		return num;
	}

	public static bool Any(Predicate<PlayerCustom> match)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Enumerator<PlayerRef, PlayerCustom> enumerator = Instance.PlayersCustom.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				if (match(enumerator.Current.Value))
				{
					return true;
				}
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
		return false;
	}

	void INetworkRunnerCallbacks.OnPlayerLeft(NetworkRunner runner, PlayerRef player)
	{
	}

	void INetworkRunnerCallbacks.OnPlayerJoined(NetworkRunner runner, PlayerRef player)
	{
	}

	void INetworkRunnerCallbacks.OnInput(NetworkRunner runner, NetworkInput input)
	{
	}

	void INetworkRunnerCallbacks.OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
	{
	}

	void INetworkRunnerCallbacks.OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
	{
	}

	void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner)
	{
	}

	void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner)
	{
	}

	void INetworkRunnerCallbacks.OnConnectRequest(NetworkRunner runner, ConnectRequest request, byte[] token)
	{
	}

	void INetworkRunnerCallbacks.OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
	{
	}

	void INetworkRunnerCallbacks.OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
	{
	}

	void INetworkRunnerCallbacks.OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
	{
	}

	void INetworkRunnerCallbacks.OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
	{
	}

	void INetworkRunnerCallbacks.OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
	{
	}

	void INetworkRunnerCallbacks.OnSceneLoadDone(NetworkRunner runner)
	{
	}

	void INetworkRunnerCallbacks.OnSceneLoadStart(NetworkRunner runner)
	{
	}

	void INetworkRunnerCallbacks.OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
	{
	}

	unsafe PlayerCustom IElementReaderWriter<PlayerCustom>.Read(byte* data, int index)
	{
		NetworkBehaviour val = null;
		ReadWriteUtilsForWeaver.VerifyRawNetworkUnwrap<PlayerCustom>(NetworkBehaviour.NetworkDeserialize(((SimulationBehaviour)this).Runner, data + index * 8, ref val), 8);
		return (PlayerCustom)(object)val;
	}

	public unsafe ref PlayerCustom ReadRef(byte* data, int index)
	{
		throw new NotSupportedException("Only supported for trivially copyable types. PlayerCustom is not trivially copyable.");
	}

	unsafe void IElementReaderWriter<PlayerCustom>.Write(byte* data, int index, PlayerCustom val)
	{
		ReadWriteUtilsForWeaver.VerifyRawNetworkWrap<PlayerCustom>(NetworkBehaviour.NetworkSerialize(((SimulationBehaviour)this).Runner, (NetworkBehaviour)(object)val, data + index * 8), 8);
	}

	public int GetElementWordCount()
	{
		return 2;
	}

	public override void CopyBackingFieldsToState(bool A_1)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		int num = _PlayersCustom?.Count ?? 0;
		if (num == 0)
		{
			return;
		}
		if (num > PlayersCustom.Capacity)
		{
			Log.Error((object)$"Source dictionary is too long for {((Object)this).name} with capacity of {PlayersCustom.Capacity}: {num}. Ignoring extra elements.");
			num = PlayersCustom.Capacity;
		}
		PlayersCustom.Clear();
		foreach (KeyValuePair<PlayerRef, PlayerCustom> item in _PlayersCustom)
		{
			if (--num < 0)
			{
				break;
			}
			PlayersCustom.Add(item.Key, item.Value);
		}
	}

	public override void CopyStateToBackingFields()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (_PlayersCustom == null)
		{
			_PlayersCustom = new SerializableDictionary<PlayerRef, PlayerCustom>();
		}
		else
		{
			_PlayersCustom.Clear();
		}
		Enumerator<PlayerRef, PlayerCustom> enumerator = PlayersCustom.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<PlayerRef, PlayerCustom> current = enumerator.Current;
				_PlayersCustom.Add(current.Key, current.Value);
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
	}
}
