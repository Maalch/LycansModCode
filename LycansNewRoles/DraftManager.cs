using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using Helpers.Collections;
using LycansNewRoles.Sabotages;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(20)]
public class DraftManager : NetworkBehaviour
{
	public enum DraftPlayerMainRole
	{
		NormalVillager,
		Wolf,
		Solo,
		Traitor,
		WolfPup,
		EliteVillager
	}

	public class PlayerDraftData
	{
		public DraftPlayerMainRole MainRole;

		public List<PlayerCustom.PlayerNewPrimaryRole> OfferedSoloRoles = new List<PlayerCustom.PlayerNewPrimaryRole>();

		public PlayerCustom.PlayerNewPrimaryRole SelectedSoloRole = PlayerCustom.PlayerNewPrimaryRole.None;

		public bool GetsPrimaryRolePower = false;

		public List<PlayerCustom.PlayerPrimaryRolePower> OfferedPowers = new List<PlayerCustom.PlayerPrimaryRolePower>();

		public PlayerCustom.PlayerPrimaryRolePower SelectedPower = PlayerCustom.PlayerPrimaryRolePower.None;

		public bool GetsSecondaryRole = false;

		public List<PlayerCustom.PlayerSecondaryRole> OfferedSecondaryRoles = new List<PlayerCustom.PlayerSecondaryRole>();

		public PlayerCustom.PlayerSecondaryRole SelectedSecondaryRole = PlayerCustom.PlayerSecondaryRole.None;

		public bool SelectionDone = false;

		public void Reset()
		{
			Instance.MyData.SelectionDone = false;
			SelectedSoloRole = PlayerCustom.PlayerNewPrimaryRole.None;
			SelectedPower = PlayerCustom.PlayerPrimaryRolePower.None;
			SelectedSecondaryRole = PlayerCustom.PlayerSecondaryRole.None;
		}
	}

	public const float TimeToChooseRolesSeconds = 30f;

	public Dictionary<int, PlayerDraftData> PlayersDraftDataByPlayerIndex = new Dictionary<int, PlayerDraftData>();

	public PlayerDraftData MyData = new PlayerDraftData();

	[Networked(OnChanged = "ActiveChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe NetworkBool Active
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Active. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (NetworkBool)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.Active. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe TickTimer DraftTimer
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CurrentPlayerTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing PlayerCustom.CurrentPlayerTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	public static DraftManager Instance { get; private set; }

	public void Init()
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0d: Unknown result type (might be due to invalid IL or missing references)
		PlayersDraftDataByPlayerIndex.Clear();
		List<PlayerCustom> allPlayers = PlayerCustomRegistry.AllPlayers;
		allPlayers.Shuffle();
		foreach (PlayerCustom item in allPlayers)
		{
			item.PlayerController.Role = (PlayerRole)0;
		}
		List<PlayerCustom.PlayerNewPrimaryRole> list = (from o in Plugin.CustomConfig.SoloRoleActive
			where o.Key != PlayerCustom.PlayerNewPrimaryRole.None && o.Key != PlayerCustom.PlayerNewPrimaryRole.Traitor && o.Value && !PlayerCustom.IsNewPrimaryRoleDisabled(o.Key)
			select o.Key).ToList();
		if (GameManager.Instance.WolvesCount < 2 && !NetworkBool.op_Implicit(Plugin.CustomConfig.LoverWolfReplacesVillager))
		{
			list.Remove(PlayerCustom.PlayerNewPrimaryRole.Lover);
		}
		int num = Plugin.CustomConfig.WolfPowersCount;
		List<PlayerCustom.PlayerPrimaryRolePower> list2 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
			where o.Value && PlayerCustom.IsPrimaryRolePowerForWolves(o.Key) && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key)
			select o.Key).ToList();
		List<PlayerCustom.PlayerPrimaryRolePower> list3 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
			where o.Value && PlayerCustom.IsPrimaryRolePowerForEliteVillagers(o.Key) && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key)
			select o.Key).ToList();
		List<PlayerCustom.PlayerPrimaryRolePower> list4 = (from o in Plugin.CustomConfig.PrimaryRolePowerActive
			where o.Value && PlayerCustom.IsPrimaryRolePowerForNormalVillagers(o.Key) && !PlayerCustom.IsPrimaryRolePowerDisabled(o.Key)
			select o.Key).ToList();
		List<PlayerCustom.PlayerPrimaryRolePower> list5 = new List<PlayerCustom.PlayerPrimaryRolePower>();
		foreach (PlayerCustom.PlayerPrimaryRolePower item2 in list4)
		{
			int villagerJobChancePonderation = BalancingValues.GetVillagerJobChancePonderation(item2);
			for (int num2 = 0; num2 < villagerJobChancePonderation; num2++)
			{
				list5.Add(item2);
			}
		}
		if (Random.value * 100f < (float)Plugin.CustomConfig.AvatarChance)
		{
			list5.Add(PlayerCustom.PlayerPrimaryRolePower.Avatar);
		}
		if (Random.value * 100f < (float)Plugin.CustomConfig.MoleChance)
		{
			list5.Add(PlayerCustom.PlayerPrimaryRolePower.Mole);
		}
		int num3 = Plugin.CustomConfig.SecondaryRolesCount;
		List<PlayerCustom.PlayerSecondaryRole> list6 = (from o in Plugin.CustomConfig.SecondaryRoleActive
			where o.Value && o.Key != PlayerCustom.PlayerSecondaryRole.BothTelepath && !PlayerCustom.IsSecondaryRoleDisabled(o.Key)
			select o.Key).ToList();
		foreach (KeyValuePair<PlayerCustom.PlayerSecondaryRole, int> item3 in BalancingValues.SecondaryRoleMaxAmountInDraft)
		{
			if (item3.Value > 1)
			{
				for (int num4 = 1; num4 < item3.Value; num4++)
				{
					list6.Add(item3.Key);
				}
			}
		}
		List<DraftPlayerMainRole> list7 = new List<DraftPlayerMainRole>();
		for (int num5 = 0; num5 < Math.Min(Plugin.CustomConfig.ElitesCount, list3.Count); num5++)
		{
			list7.Add(DraftPlayerMainRole.EliteVillager);
		}
		for (int num6 = 0; num6 < GameManager.Instance.WolvesCount; num6++)
		{
			list7.Add(DraftPlayerMainRole.Wolf);
		}
		for (int num7 = 0; num7 < Plugin.CustomConfig.TraitorsCount; num7++)
		{
			list7.Add(DraftPlayerMainRole.Traitor);
		}
		for (int num8 = 0; num8 < Plugin.CustomConfig.WolfPupsCount; num8++)
		{
			list7.Add(DraftPlayerMainRole.WolfPup);
		}
		for (int num9 = 0; num9 < Math.Min(Plugin.CustomConfig.SoloRolesCount, list.Count); num9++)
		{
			list7.Add(DraftPlayerMainRole.Solo);
		}
		for (int num10 = list7.Count; num10 < PlayerRegistry.Count; num10++)
		{
			list7.Add(DraftPlayerMainRole.NormalVillager);
		}
		LogRemainingMainRoles(list7);
		list7.Shuffle();
		foreach (PlayerCustom item4 in allPlayers)
		{
			PlayerDraftData playerDraftData = new PlayerDraftData();
			playerDraftData.MainRole = list7.First();
			list7.RemoveAt(0);
			switch (playerDraftData.MainRole)
			{
			case DraftPlayerMainRole.NormalVillager:
				playerDraftData.GetsPrimaryRolePower = Random.value * 100f < (float)Plugin.CustomConfig.VillagerPowersChance;
				break;
			case DraftPlayerMainRole.EliteVillager:
				playerDraftData.GetsPrimaryRolePower = true;
				break;
			case DraftPlayerMainRole.Solo:
				playerDraftData.GetsPrimaryRolePower = true;
				break;
			case DraftPlayerMainRole.Wolf:
			case DraftPlayerMainRole.Traitor:
			case DraftPlayerMainRole.WolfPup:
				if (num > 0)
				{
					playerDraftData.GetsPrimaryRolePower = true;
					num--;
				}
				else
				{
					playerDraftData.GetsPrimaryRolePower = false;
				}
				break;
			}
			if (num3 > 0)
			{
				playerDraftData.GetsSecondaryRole = true;
				num3--;
			}
			else
			{
				playerDraftData.GetsSecondaryRole = false;
			}
			PlayersDraftDataByPlayerIndex[item4.Index] = playerDraftData;
		}
		for (int num11 = 0; num11 < 3; num11++)
		{
			foreach (PlayerDraftData playerDraftData2 in PlayersDraftDataByPlayerIndex.Values.Where((PlayerDraftData o) => o.MainRole == DraftPlayerMainRole.Solo))
			{
				List<PlayerCustom.PlayerNewPrimaryRole> list8 = list.Where((PlayerCustom.PlayerNewPrimaryRole o) => !playerDraftData2.OfferedSoloRoles.Contains(o)).ToList();
				if (list8.Count > 0)
				{
					PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = CollectionsUtil.Grab<PlayerCustom.PlayerNewPrimaryRole>(list8, 1).First();
					playerDraftData2.OfferedSoloRoles.Add(playerNewPrimaryRole);
					list.Remove(playerNewPrimaryRole);
					if (playerDraftData2.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.None)
					{
						playerDraftData2.SelectedSoloRole = playerNewPrimaryRole;
					}
				}
			}
		}
		for (int num12 = 0; num12 < 3; num12++)
		{
			foreach (PlayerDraftData playerDraftData3 in PlayersDraftDataByPlayerIndex.Values.Where((PlayerDraftData o) => o.MainRole == DraftPlayerMainRole.NormalVillager && o.GetsPrimaryRolePower))
			{
				List<PlayerCustom.PlayerPrimaryRolePower> list9 = list5.Where((PlayerCustom.PlayerPrimaryRolePower o) => !playerDraftData3.OfferedPowers.Contains(o)).ToList();
				if (list9.Count > 0)
				{
					PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list9, 1).First();
					playerDraftData3.OfferedPowers.Add(playerPrimaryRolePower);
					list5.Remove(playerPrimaryRolePower);
					if (playerDraftData3.SelectedPower == PlayerCustom.PlayerPrimaryRolePower.None)
					{
						playerDraftData3.SelectedPower = playerPrimaryRolePower;
					}
				}
			}
		}
		for (int num13 = 0; num13 < 3; num13++)
		{
			foreach (PlayerDraftData playerDraftData4 in PlayersDraftDataByPlayerIndex.Values.Where((PlayerDraftData o) => o.MainRole == DraftPlayerMainRole.EliteVillager))
			{
				List<PlayerCustom.PlayerPrimaryRolePower> list10 = list3.Where((PlayerCustom.PlayerPrimaryRolePower o) => !playerDraftData4.OfferedPowers.Contains(o)).ToList();
				if (list10.Count > 0)
				{
					PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower2 = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list10, 1).First();
					playerDraftData4.OfferedPowers.Add(playerPrimaryRolePower2);
					list3.Remove(playerPrimaryRolePower2);
					if (playerDraftData4.SelectedPower == PlayerCustom.PlayerPrimaryRolePower.None)
					{
						playerDraftData4.SelectedPower = playerPrimaryRolePower2;
					}
				}
			}
		}
		for (int num14 = 0; num14 < 3; num14++)
		{
			foreach (PlayerDraftData playerDraftData5 in PlayersDraftDataByPlayerIndex.Values.Where((PlayerDraftData o) => (o.MainRole == DraftPlayerMainRole.Wolf || o.MainRole == DraftPlayerMainRole.Traitor || o.MainRole == DraftPlayerMainRole.WolfPup) && o.GetsPrimaryRolePower))
			{
				List<PlayerCustom.PlayerPrimaryRolePower> list11 = list2.Where((PlayerCustom.PlayerPrimaryRolePower o) => !playerDraftData5.OfferedPowers.Contains(o)).ToList();
				if (playerDraftData5.MainRole == DraftPlayerMainRole.Traitor)
				{
					list11.RemoveAll((PlayerCustom.PlayerPrimaryRolePower o) => !PlayerCustom.IsWolfPowerAvailableForTraitor(o));
				}
				if (list11.Count > 0)
				{
					PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower3 = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(list11, 1).First();
					playerDraftData5.OfferedPowers.Add(playerPrimaryRolePower3);
					list2.Remove(playerPrimaryRolePower3);
					if (playerDraftData5.SelectedPower == PlayerCustom.PlayerPrimaryRolePower.None)
					{
						playerDraftData5.SelectedPower = playerPrimaryRolePower3;
					}
				}
			}
		}
		for (int num15 = 0; num15 < 3; num15++)
		{
			foreach (PlayerDraftData playerDraftData6 in PlayersDraftDataByPlayerIndex.Values.Where((PlayerDraftData o) => o.GetsSecondaryRole))
			{
				List<PlayerCustom.PlayerSecondaryRole> list12 = list6.Where((PlayerCustom.PlayerSecondaryRole o) => !playerDraftData6.OfferedSecondaryRoles.Contains(o)).ToList();
				if (playerDraftData6.MainRole == DraftPlayerMainRole.Traitor)
				{
					list12.RemoveAll((PlayerCustom.PlayerSecondaryRole o) => PlayerCustom.IsSecondaryRoleDisabledForTraitor(o));
				}
				if (list12.Count > 0)
				{
					PlayerCustom.PlayerSecondaryRole playerSecondaryRole = CollectionsUtil.Grab<PlayerCustom.PlayerSecondaryRole>(list12, 1).First();
					playerDraftData6.OfferedSecondaryRoles.Add(playerSecondaryRole);
					list6.Remove(playerSecondaryRole);
					if (playerDraftData6.SelectedSecondaryRole == PlayerCustom.PlayerSecondaryRole.None)
					{
						playerDraftData6.SelectedSecondaryRole = playerSecondaryRole;
					}
				}
			}
		}
		foreach (KeyValuePair<int, PlayerDraftData> item5 in PlayersDraftDataByPlayerIndex)
		{
			if (item5.Value.MainRole == DraftPlayerMainRole.Solo)
			{
				Rpc_Send_Player_Info(((SimulationBehaviour)this).Runner, item5.Key, (int)item5.Value.MainRole, (int)((item5.Value.OfferedSoloRoles.Count >= 1) ? item5.Value.OfferedSoloRoles[0] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((item5.Value.OfferedSoloRoles.Count >= 2) ? item5.Value.OfferedSoloRoles[1] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((item5.Value.OfferedSoloRoles.Count >= 3) ? item5.Value.OfferedSoloRoles[2] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((item5.Value.OfferedSecondaryRoles.Count >= 1) ? item5.Value.OfferedSecondaryRoles[0] : ((PlayerCustom.PlayerSecondaryRole)(-1))), (int)((item5.Value.OfferedSecondaryRoles.Count >= 2) ? item5.Value.OfferedSecondaryRoles[1] : ((PlayerCustom.PlayerSecondaryRole)(-1))), (int)((item5.Value.OfferedSecondaryRoles.Count >= 3) ? item5.Value.OfferedSecondaryRoles[2] : ((PlayerCustom.PlayerSecondaryRole)(-1))));
			}
			else
			{
				Rpc_Send_Player_Info(((SimulationBehaviour)this).Runner, item5.Key, (int)item5.Value.MainRole, (int)((item5.Value.OfferedPowers.Count >= 1) ? item5.Value.OfferedPowers[0] : ((PlayerCustom.PlayerPrimaryRolePower)(-1))), (int)((item5.Value.OfferedPowers.Count >= 2) ? item5.Value.OfferedPowers[1] : ((PlayerCustom.PlayerPrimaryRolePower)(-1))), (int)((item5.Value.OfferedPowers.Count >= 3) ? item5.Value.OfferedPowers[2] : ((PlayerCustom.PlayerPrimaryRolePower)(-1))), (int)((item5.Value.OfferedSecondaryRoles.Count >= 1) ? item5.Value.OfferedSecondaryRoles[0] : ((PlayerCustom.PlayerSecondaryRole)(-1))), (int)((item5.Value.OfferedSecondaryRoles.Count >= 2) ? item5.Value.OfferedSecondaryRoles[1] : ((PlayerCustom.PlayerSecondaryRole)(-1))), (int)((item5.Value.OfferedSecondaryRoles.Count >= 3) ? item5.Value.OfferedSecondaryRoles[2] : ((PlayerCustom.PlayerSecondaryRole)(-1))));
			}
		}
		DraftTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 30f);
	}

	private void LogRemainingMainRoles(List<DraftPlayerMainRole> mainRoles)
	{
		LycansUtility.DebugLog("----- Showing remaining roles -----");
		foreach (DraftPlayerMainRole mainRole in mainRoles)
		{
			LycansUtility.DebugLog(mainRole.ToString());
		}
		LycansUtility.DebugLog("----- End showing roles ------");
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(Active) && ((SimulationBehaviour)this).Runner.IsServer)
		{
			TickTimer draftTimer = DraftTimer;
			if (((TickTimer)(ref draftTimer)).Expired(((SimulationBehaviour)this).Runner))
			{
				TimerExpired();
			}
		}
	}

	private void TimerExpired()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		DraftTimer = TickTimer.None;
		foreach (KeyValuePair<int, PlayerDraftData> item in PlayersDraftDataByPlayerIndex.Where((KeyValuePair<int, PlayerDraftData> o) => !o.Value.SelectionDone))
		{
			Instance.FinishChoice(item.Key);
		}
	}

	public void FinishChoice(int playerIndex)
	{
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Invalid comparison between Unknown and I4
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a66: Unknown result type (might be due to invalid IL or missing references)
		PlayerDraftData playerDraftData = PlayersDraftDataByPlayerIndex[playerIndex];
		playerDraftData.SelectionDone = true;
		PlayerCustom playerCustom = PlayerCustomRegistry.GetPlayer(playerIndex);
		switch (playerDraftData.MainRole)
		{
		case DraftPlayerMainRole.NormalVillager:
			playerCustom.PlayerController.Role = (PlayerRole)0;
			playerCustom.GivePrimaryRolePower(playerDraftData.SelectedPower);
			break;
		case DraftPlayerMainRole.EliteVillager:
			playerCustom.PlayerController.Role = (PlayerRole)0;
			playerCustom.GivePrimaryRolePower(playerDraftData.SelectedPower);
			break;
		case DraftPlayerMainRole.Wolf:
			playerCustom.PlayerController.Role = (PlayerRole)1;
			playerCustom.GivePrimaryRolePower(playerDraftData.SelectedPower);
			playerCustom.IsWolfPup = NetworkBool.op_Implicit(false);
			break;
		case DraftPlayerMainRole.Traitor:
			playerCustom.PlayerController.Role = (PlayerRole)0;
			playerCustom.GiveNewPrimaryRole(PlayerCustom.PlayerNewPrimaryRole.Traitor);
			playerCustom.GivePrimaryRolePower(playerDraftData.SelectedPower);
			break;
		case DraftPlayerMainRole.WolfPup:
			playerCustom.PlayerController.Role = (PlayerRole)1;
			playerCustom.GivePrimaryRolePower(playerDraftData.SelectedPower);
			playerCustom.IsWolfPup = NetworkBool.op_Implicit(true);
			break;
		case DraftPlayerMainRole.Solo:
			if (playerDraftData.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
			{
				if (PlayersDraftDataByPlayerIndex.Any((KeyValuePair<int, PlayerDraftData> o) => o.Value.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Lover && o.Key != playerIndex))
				{
					PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayersDraftDataByPlayerIndex.First((KeyValuePair<int, PlayerDraftData> o) => o.Value.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Lover && o.Key != playerIndex).Key);
					playerCustom.PlayerController.Role = (PlayerRole)((int)player.PlayerController.Role != 1);
				}
				else
				{
					playerCustom.PlayerController.Role = (PlayerRole)(!(Random.value < 0.5f));
				}
			}
			else
			{
				playerCustom.PlayerController.Role = (PlayerRole)0;
			}
			playerCustom.GiveNewPrimaryRole(playerDraftData.SelectedSoloRole);
			break;
		}
		if (playerDraftData.SelectedSecondaryRole != PlayerCustom.PlayerSecondaryRole.None && !PlayerCustom.GetAvailableSecondaryRoles(playerCustom.PlayerController.Role, playerCustom.NewPrimaryRole, playerCustom.PrimaryRolePower).Contains(playerDraftData.SelectedSecondaryRole))
		{
			List<PlayerCustom.PlayerSecondaryRole> list = playerDraftData.OfferedSecondaryRoles.Where((PlayerCustom.PlayerSecondaryRole o) => PlayerCustom.GetAvailableSecondaryRoles(playerCustom.PlayerController.Role, playerCustom.NewPrimaryRole, playerCustom.PrimaryRolePower).Contains(o)).ToList();
			if (list.Any())
			{
				playerDraftData.SelectedSecondaryRole = CollectionsUtil.Grab<PlayerCustom.PlayerSecondaryRole>(list, 1).First();
			}
			else
			{
				playerDraftData.SelectedSecondaryRole = PlayerCustom.PlayerSecondaryRole.None;
			}
		}
		playerCustom.GiveSecondaryRole(playerDraftData.SelectedSecondaryRole);
		NetworkString<_32> username = playerCustom.PlayerController.PlayerData.Username;
		LycansUtility.AddLogOnlyForMe("--- Player " + ((object)username/*cast due to constrained. prefix*/).ToString() + " --- finished choice");
		LycansUtility.AddLogOnlyForMe("Main role: " + playerDraftData.MainRole);
		LycansUtility.AddLogOnlyForMe("Player role: " + ((object)playerCustom.PlayerController.Role/*cast due to constrained. prefix*/).ToString());
		LycansUtility.AddLogOnlyForMe("Selected solo roles: " + playerDraftData.SelectedSoloRole);
		LycansUtility.AddLogOnlyForMe("Selected power: " + playerDraftData.SelectedPower);
		LycansUtility.AddLogOnlyForMe("Selected secondary role: " + playerDraftData.SelectedSecondaryRole);
		if (playerDraftData.MainRole == DraftPlayerMainRole.Solo && ((playerDraftData.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Agent && PlayersDraftDataByPlayerIndex.Count((KeyValuePair<int, PlayerDraftData> o) => o.Value.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Agent) == 1) || (playerDraftData.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Lover && PlayersDraftDataByPlayerIndex.Count((KeyValuePair<int, PlayerDraftData> o) => o.Value.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Lover) == 1)))
		{
			LycansUtility.AddLogOnlyForMe("-> Add second agent or lover");
			if (playerDraftData.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Agent)
			{
				KeyValuePair<int, PlayerDraftData> keyValuePair = (PlayersDraftDataByPlayerIndex.Any((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == DraftPlayerMainRole.NormalVillager && PlayerCustomRegistry.GetPlayer(o.Key).DraftWantsSecondAgent && !o.Value.SelectionDone) ? PlayersDraftDataByPlayerIndex.First((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == DraftPlayerMainRole.NormalVillager && PlayerCustomRegistry.GetPlayer(o.Key).DraftWantsSecondAgent && !o.Value.SelectionDone) : ((!PlayersDraftDataByPlayerIndex.Any((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == DraftPlayerMainRole.NormalVillager && PlayerCustomRegistry.GetPlayer(o.Key).DraftWantsSecondAgent)) ? PlayersDraftDataByPlayerIndex.First((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == DraftPlayerMainRole.NormalVillager) : PlayersDraftDataByPlayerIndex.First((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == DraftPlayerMainRole.NormalVillager && PlayerCustomRegistry.GetPlayer(o.Key).DraftWantsSecondAgent)));
				PlayerCustomRegistry.GetPlayer(keyValuePair.Key).Reset();
				keyValuePair.Value.SelectionDone = false;
				keyValuePair.Value.MainRole = DraftPlayerMainRole.Solo;
				keyValuePair.Value.OfferedSoloRoles = new List<PlayerCustom.PlayerNewPrimaryRole> { PlayerCustom.PlayerNewPrimaryRole.Agent };
				keyValuePair.Value.SelectedSoloRole = PlayerCustom.PlayerNewPrimaryRole.Agent;
				keyValuePair.Value.OfferedPowers.Clear();
				keyValuePair.Value.SelectedPower = PlayerCustom.PlayerPrimaryRolePower.None;
				keyValuePair.Value.OfferedSecondaryRoles.RemoveAll((PlayerCustom.PlayerSecondaryRole o) => !PlayerCustom.GetAvailableSecondaryRoles((PlayerRole)0, PlayerCustom.PlayerNewPrimaryRole.Agent, PlayerCustom.PlayerPrimaryRolePower.None).Contains(o));
				keyValuePair.Value.SelectedSecondaryRole = (keyValuePair.Value.OfferedSecondaryRoles.Any() ? keyValuePair.Value.OfferedSecondaryRoles[0] : PlayerCustom.PlayerSecondaryRole.None);
				Rpc_Send_Player_Info(((SimulationBehaviour)this).Runner, keyValuePair.Key, (int)keyValuePair.Value.MainRole, (int)((keyValuePair.Value.OfferedSoloRoles.Count >= 1) ? keyValuePair.Value.OfferedSoloRoles[0] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((keyValuePair.Value.OfferedSoloRoles.Count >= 2) ? keyValuePair.Value.OfferedSoloRoles[1] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((keyValuePair.Value.OfferedSoloRoles.Count >= 3) ? keyValuePair.Value.OfferedSoloRoles[2] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((keyValuePair.Value.OfferedSecondaryRoles.Count >= 1) ? keyValuePair.Value.OfferedSecondaryRoles[0] : ((PlayerCustom.PlayerSecondaryRole)(-1))), (int)((keyValuePair.Value.OfferedSecondaryRoles.Count >= 2) ? keyValuePair.Value.OfferedSecondaryRoles[1] : ((PlayerCustom.PlayerSecondaryRole)(-1))), (int)((keyValuePair.Value.OfferedSecondaryRoles.Count >= 3) ? keyValuePair.Value.OfferedSecondaryRoles[2] : ((PlayerCustom.PlayerSecondaryRole)(-1))));
			}
			else if (playerDraftData.SelectedSoloRole == PlayerCustom.PlayerNewPrimaryRole.Lover)
			{
				DraftPlayerMainRole targetMainRole = ((!NetworkBool.op_Implicit(Plugin.CustomConfig.LoverWolfReplacesVillager)) ? DraftPlayerMainRole.Wolf : DraftPlayerMainRole.NormalVillager);
				KeyValuePair<int, PlayerDraftData> keyValuePair2 = (PlayersDraftDataByPlayerIndex.Any((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == targetMainRole && PlayerCustomRegistry.GetPlayer(o.Key).DraftWantsSecondLover && !o.Value.SelectionDone) ? PlayersDraftDataByPlayerIndex.First((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == targetMainRole && PlayerCustomRegistry.GetPlayer(o.Key).DraftWantsSecondLover && !o.Value.SelectionDone) : ((!PlayersDraftDataByPlayerIndex.Any((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == targetMainRole && PlayerCustomRegistry.GetPlayer(o.Key).DraftWantsSecondLover)) ? PlayersDraftDataByPlayerIndex.First((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == targetMainRole) : PlayersDraftDataByPlayerIndex.First((KeyValuePair<int, PlayerDraftData> o) => o.Value.MainRole == targetMainRole && PlayerCustomRegistry.GetPlayer(o.Key).DraftWantsSecondLover)));
				PlayerCustomRegistry.GetPlayer(keyValuePair2.Key).Reset();
				keyValuePair2.Value.SelectionDone = false;
				keyValuePair2.Value.MainRole = DraftPlayerMainRole.Solo;
				keyValuePair2.Value.OfferedSoloRoles = new List<PlayerCustom.PlayerNewPrimaryRole> { PlayerCustom.PlayerNewPrimaryRole.Lover };
				keyValuePair2.Value.SelectedSoloRole = PlayerCustom.PlayerNewPrimaryRole.Lover;
				keyValuePair2.Value.OfferedPowers.Clear();
				keyValuePair2.Value.SelectedPower = PlayerCustom.PlayerPrimaryRolePower.None;
				keyValuePair2.Value.OfferedSecondaryRoles.RemoveAll((PlayerCustom.PlayerSecondaryRole o) => !PlayerCustom.GetAvailableSecondaryRoles((PlayerRole)0, PlayerCustom.PlayerNewPrimaryRole.Lover, PlayerCustom.PlayerPrimaryRolePower.None).Contains(o));
				keyValuePair2.Value.SelectedSecondaryRole = (keyValuePair2.Value.OfferedSecondaryRoles.Any() ? keyValuePair2.Value.OfferedSecondaryRoles[0] : PlayerCustom.PlayerSecondaryRole.None);
				Rpc_Send_Player_Info(((SimulationBehaviour)this).Runner, keyValuePair2.Key, (int)keyValuePair2.Value.MainRole, (int)((keyValuePair2.Value.OfferedSoloRoles.Count >= 1) ? keyValuePair2.Value.OfferedSoloRoles[0] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((keyValuePair2.Value.OfferedSoloRoles.Count >= 2) ? keyValuePair2.Value.OfferedSoloRoles[1] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((keyValuePair2.Value.OfferedSoloRoles.Count >= 3) ? keyValuePair2.Value.OfferedSoloRoles[2] : ((PlayerCustom.PlayerNewPrimaryRole)(-1))), (int)((keyValuePair2.Value.OfferedSecondaryRoles.Count >= 1) ? keyValuePair2.Value.OfferedSecondaryRoles[0] : ((PlayerCustom.PlayerSecondaryRole)(-1))), (int)((keyValuePair2.Value.OfferedSecondaryRoles.Count >= 2) ? keyValuePair2.Value.OfferedSecondaryRoles[1] : ((PlayerCustom.PlayerSecondaryRole)(-1))), (int)((keyValuePair2.Value.OfferedSecondaryRoles.Count >= 3) ? keyValuePair2.Value.OfferedSecondaryRoles[2] : ((PlayerCustom.PlayerSecondaryRole)(-1))));
			}
			TickTimer draftTimer = DraftTimer;
			if (((TickTimer)(ref draftTimer)).IsRunning)
			{
				draftTimer = DraftTimer;
				if (!(((TickTimer)(ref draftTimer)).RemainingTime(((SimulationBehaviour)this).Runner) < 2f))
				{
					return;
				}
			}
			LycansUtility.AddLogOnlyForMe("-> Change draft timer");
			DraftTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 2f);
		}
		else if (PlayersDraftDataByPlayerIndex.All((KeyValuePair<int, PlayerDraftData> o) => o.Value.SelectionDone))
		{
			StartGame();
		}
	}

	private void StartGame()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		DraftTimer = TickTimer.None;
		Active = NetworkBool.op_Implicit(false);
		GameManagerCustom.Instance.EventsManager.RollEvent();
		GameManager.Instance.ClearSpawnedItems();
		GameManager.Instance.UpdatePotions();
		GameManager.Instance.SpawnRandomItems();
		PlayerRegistry.ForEach((Action<PlayerController>)delegate(PlayerController pObj)
		{
			pObj.Hunger = GameManager.Instance.MaxHunger;
		});
		CollectionsUtil.ForEach<Portal>(Object.FindObjectsOfType<Portal>(), (Action<Portal>)delegate(Portal p)
		{
			p.ResetPortal();
		});
		GameManager.Instance.TeleportAllPlayers();
		foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
		{
			allPlayer.PlayerController.CanMove = NetworkBool.op_Implicit(true);
			allPlayer.InitForGameStart();
			if (((SimulationBehaviour)allPlayer).Runner.IsServer)
			{
				allPlayer.InitStats();
			}
		}
		SabotageManager.Instance.Init();
		BeastManager.Instance.Reset();
		CultistManager.Instance.Reset();
		GameManager.Instance.UpdateLoot(true);
		GameManager.Instance.GiveAlchemistPotions();
		GameManagerCustom.Instance.NewDay();
		Plugin.CreatePlayerIllusionIfNeeded();
		foreach (PlayerCustom allPlayer2 in PlayerCustomRegistry.AllPlayers)
		{
			PlayerController playerController = allPlayer2.PlayerController;
			string[] obj = new string[14]
			{
				"Player ", null, null, null, null, null, null, null, null, null,
				null, null, null, null
			};
			NetworkString<_32> username = playerController.PlayerData.Username;
			obj[1] = ((object)username/*cast due to constrained. prefix*/).ToString();
			obj[2] = " with ref ";
			obj[3] = ((object)playerController.Ref/*cast due to constrained. prefix*/).ToString();
			obj[4] = " has base role ";
			obj[5] = ((object)playerController.Role/*cast due to constrained. prefix*/).ToString();
			obj[6] = ", primary role ";
			obj[7] = allPlayer2.NewPrimaryRole.ToString();
			obj[8] = ", secondary role ";
			obj[9] = allPlayer2.SecondaryRole.ToString();
			obj[10] = ", power ";
			obj[11] = allPlayer2.PrimaryRolePower.ToString();
			obj[12] = ", is wolf pup: ";
			obj[13] = ((object)allPlayer2.IsWolfPup/*cast due to constrained. prefix*/).ToString();
			LycansUtility.DebugLog(string.Concat(obj));
		}
	}

	[Preserve]
	public static void ActiveChanged(Changed<DraftManager> changed)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Invalid comparison between Unknown and I4
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Invalid comparison between Unknown and I4
		try
		{
			if (NetworkBool.op_Implicit(changed.Behaviour.Active))
			{
				UIManager.DraftPanel.Show();
				return;
			}
			UIManager.DraftPanel.Hide();
			if ((int)GameManager.LocalGameState != 2)
			{
				return;
			}
			GameManager.Instance.gameUI.UpdateCursor(false);
			GameManager.Instance.gameUI.ShowGameMenu(true);
			GameManager.Instance.gameUI.ShowRole(true);
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.PlayerController.EnablePlayerInteraction(true);
				allPlayer.InitialPower = allPlayer.PrimaryRolePower;
				allPlayer.AlreadyPossessed = false;
			}
			PlayerRegistry.ForEach((Action<PlayerController>)delegate(PlayerController p)
			{
				p.EnablePlayerInteraction(true);
			});
			PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref).UpdatePrimaryRole();
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
			string text;
			float num;
			switch (player.NewPrimaryRole)
			{
			case PlayerCustom.PlayerNewPrimaryRole.Traitor:
				text = "WOLF";
				num = 0.5f;
				break;
			case PlayerCustom.PlayerNewPrimaryRole.None:
			{
				PlayerRole role = player.PlayerController.Role;
				PlayerRole val = role;
				if ((int)val == 1)
				{
					text = "WOLF";
					num = 0.5f;
				}
				else
				{
					text = "VILLAGER";
					num = 0.25f;
				}
				break;
			}
			default:
				text = "AngelHeal";
				num = 0.5f;
				break;
			}
			AudioManager.Play(text, (MixerTarget)2, num, 1f);
			UIManager.RoleDescription.Hide();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ActiveChanged error: " + ex));
		}
	}

	[Rpc]
	public unsafe static void Rpc_Send_Player_Info(NetworkRunner runner, int playerIndex, int mainRoleIndex, int powerOrSoloRoleIndex1, int powerOrSoloRoleIndex2, int powerOrSoloRoleIndex3, int secondaryRoleIndex1, int secondaryRoleIndex2, int secondaryRoleIndex3)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 48;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.DraftManager::Rpc_Send_Player_Info(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = mainRoleIndex;
					num2 += 4;
					*(int*)(data + num2) = powerOrSoloRoleIndex1;
					num2 += 4;
					*(int*)(data + num2) = powerOrSoloRoleIndex2;
					num2 += 4;
					*(int*)(data + num2) = powerOrSoloRoleIndex3;
					num2 += 4;
					*(int*)(data + num2) = secondaryRoleIndex1;
					num2 += 4;
					*(int*)(data + num2) = secondaryRoleIndex2;
					num2 += 4;
					*(int*)(data + num2) = secondaryRoleIndex3;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			DraftPlayerMainRole draftPlayerMainRole = (DraftPlayerMainRole)mainRoleIndex;
			List<PlayerCustom.PlayerNewPrimaryRole> list = new List<PlayerCustom.PlayerNewPrimaryRole>();
			List<PlayerCustom.PlayerPrimaryRolePower> list2 = new List<PlayerCustom.PlayerPrimaryRolePower>();
			List<PlayerCustom.PlayerSecondaryRole> list3 = new List<PlayerCustom.PlayerSecondaryRole>();
			DraftPlayerMainRole draftPlayerMainRole2 = draftPlayerMainRole;
			DraftPlayerMainRole draftPlayerMainRole3 = draftPlayerMainRole2;
			if (draftPlayerMainRole3 == DraftPlayerMainRole.Solo)
			{
				if (powerOrSoloRoleIndex1 != -1)
				{
					list.Add((PlayerCustom.PlayerNewPrimaryRole)powerOrSoloRoleIndex1);
				}
				if (powerOrSoloRoleIndex2 != -1)
				{
					list.Add((PlayerCustom.PlayerNewPrimaryRole)powerOrSoloRoleIndex2);
				}
				if (powerOrSoloRoleIndex3 != -1)
				{
					list.Add((PlayerCustom.PlayerNewPrimaryRole)powerOrSoloRoleIndex3);
				}
			}
			else
			{
				if (powerOrSoloRoleIndex1 != -1)
				{
					list2.Add((PlayerCustom.PlayerPrimaryRolePower)powerOrSoloRoleIndex1);
				}
				if (powerOrSoloRoleIndex2 != -1)
				{
					list2.Add((PlayerCustom.PlayerPrimaryRolePower)powerOrSoloRoleIndex2);
				}
				if (powerOrSoloRoleIndex3 != -1)
				{
					list2.Add((PlayerCustom.PlayerPrimaryRolePower)powerOrSoloRoleIndex3);
				}
			}
			if (secondaryRoleIndex1 != -1)
			{
				list3.Add((PlayerCustom.PlayerSecondaryRole)secondaryRoleIndex1);
			}
			if (secondaryRoleIndex2 != -1)
			{
				list3.Add((PlayerCustom.PlayerSecondaryRole)secondaryRoleIndex2);
			}
			if (secondaryRoleIndex3 != -1)
			{
				list3.Add((PlayerCustom.PlayerSecondaryRole)secondaryRoleIndex3);
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			if (player.Ref == PlayerController.Local.Ref)
			{
				Instance.MyData.Reset();
				Instance.MyData.MainRole = draftPlayerMainRole;
				Instance.MyData.OfferedSoloRoles = list;
				Instance.MyData.OfferedPowers = list2;
				Instance.MyData.OfferedSecondaryRoles = list3;
				UIManager.DraftPanel.UpdateInfo();
			}
			NetworkString<_32> username = player.PlayerController.PlayerData.Username;
			LycansUtility.AddLogOnlyForMe("--- Player " + ((object)username/*cast due to constrained. prefix*/).ToString() + " ---");
			LycansUtility.AddLogOnlyForMe("Main role: " + draftPlayerMainRole);
			LycansUtility.AddLogOnlyForMe("Offered solo roles: ");
			foreach (PlayerCustom.PlayerNewPrimaryRole item in list)
			{
				LycansUtility.AddLogOnlyForMe(item.ToString());
			}
			LycansUtility.AddLogOnlyForMe("Offered powers: ");
			foreach (PlayerCustom.PlayerPrimaryRolePower item2 in list2)
			{
				LycansUtility.AddLogOnlyForMe(item2.ToString());
			}
			LycansUtility.AddLogOnlyForMe("Offered secondary roles:");
			foreach (PlayerCustom.PlayerSecondaryRole item3 in list3)
			{
				LycansUtility.AddLogOnlyForMe(item3.ToString());
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Send_Player_Info error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.DraftManager::Rpc_Send_Player_Info(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Send_Player_Info_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int mainRoleIndex = *(int*)(data + num);
		num += 4;
		int powerOrSoloRoleIndex = *(int*)(data + num);
		num += 4;
		int powerOrSoloRoleIndex2 = *(int*)(data + num);
		num += 4;
		int powerOrSoloRoleIndex3 = *(int*)(data + num);
		num += 4;
		int secondaryRoleIndex = *(int*)(data + num);
		num += 4;
		int secondaryRoleIndex2 = *(int*)(data + num);
		num += 4;
		int secondaryRoleIndex3 = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Send_Player_Info(runner, playerIndex, mainRoleIndex, powerOrSoloRoleIndex, powerOrSoloRoleIndex2, powerOrSoloRoleIndex3, secondaryRoleIndex, secondaryRoleIndex2, secondaryRoleIndex3);
	}

	[Rpc]
	public unsafe static void Rpc_Select_Roles(NetworkRunner runner, int playerIndex, int powerOrSoloRoleIndex, int secondaryRoleIndex)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Invalid comparison between Unknown and I4
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBehaviourUtils.InvokeRpc)
		{
			NetworkBehaviourUtils.InvokeRpc = false;
		}
		else
		{
			if ((Object)(object)runner == (Object)null)
			{
				throw new ArgumentNullException("runner");
			}
			if ((int)runner.Stage == 4)
			{
				return;
			}
			if (runner.HasAnyActiveConnections())
			{
				int num = 60;
				SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
				byte* data = SimulationMessage.GetData(ptr);
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.DraftManager::Rpc_Select_Roles(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")), data);
				*(int*)(data + num2) = playerIndex;
				num2 += 4;
				*(int*)(data + num2) = powerOrSoloRoleIndex;
				num2 += 4;
				*(int*)(data + num2) = secondaryRoleIndex;
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 8;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		if (runner.IsServer)
		{
			PlayerDraftData playerDraftData = Instance.PlayersDraftDataByPlayerIndex[playerIndex];
			if (playerDraftData.MainRole == DraftPlayerMainRole.Solo)
			{
				playerDraftData.SelectedSoloRole = (PlayerCustom.PlayerNewPrimaryRole)powerOrSoloRoleIndex;
			}
			else
			{
				playerDraftData.SelectedPower = ((powerOrSoloRoleIndex > -1) ? ((PlayerCustom.PlayerPrimaryRolePower)powerOrSoloRoleIndex) : PlayerCustom.PlayerPrimaryRolePower.None);
			}
			playerDraftData.SelectedSecondaryRole = ((secondaryRoleIndex > -1) ? ((PlayerCustom.PlayerSecondaryRole)secondaryRoleIndex) : PlayerCustom.PlayerSecondaryRole.None);
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.DraftManager::Rpc_Select_Roles(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Select_Roles_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int powerOrSoloRoleIndex = *(int*)(data + num);
		num += 4;
		int secondaryRoleIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Select_Roles(runner, playerIndex, powerOrSoloRoleIndex, secondaryRoleIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Confirm(NetworkRunner runner, int playerIndex)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Invalid comparison between Unknown and I4
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBehaviourUtils.InvokeRpc)
		{
			NetworkBehaviourUtils.InvokeRpc = false;
		}
		else
		{
			if ((Object)(object)runner == (Object)null)
			{
				throw new ArgumentNullException("runner");
			}
			if ((int)runner.Stage == 4)
			{
				return;
			}
			if (runner.HasAnyActiveConnections())
			{
				int num = 60;
				SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
				byte* data = SimulationMessage.GetData(ptr);
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.DraftManager::Rpc_Confirm(Fusion.NetworkRunner,System.Int32)")), data);
				*(int*)(data + num2) = playerIndex;
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 8;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		if (runner.IsServer)
		{
			Instance.FinishChoice(playerIndex);
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.DraftManager::Rpc_Confirm(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Confirm_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Confirm(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Random(NetworkRunner runner, int playerIndex)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Invalid comparison between Unknown and I4
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBehaviourUtils.InvokeRpc)
		{
			NetworkBehaviourUtils.InvokeRpc = false;
		}
		else
		{
			if ((Object)(object)runner == (Object)null)
			{
				throw new ArgumentNullException("runner");
			}
			if ((int)runner.Stage == 4)
			{
				return;
			}
			if (runner.HasAnyActiveConnections())
			{
				int num = 12;
				SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
				byte* data = SimulationMessage.GetData(ptr);
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.DraftManager::Rpc_Random(Fusion.NetworkRunner,System.Int32)")), data);
				*(int*)(data + num2) = playerIndex;
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 8;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		if (runner.IsServer)
		{
			PlayerDraftData playerDraftData = Instance.PlayersDraftDataByPlayerIndex[playerIndex];
			if (playerDraftData.MainRole == DraftPlayerMainRole.Solo)
			{
				playerDraftData.SelectedSoloRole = CollectionsUtil.Grab<PlayerCustom.PlayerNewPrimaryRole>(playerDraftData.OfferedSoloRoles, 1).First();
			}
			else if (playerDraftData.OfferedPowers.Any())
			{
				playerDraftData.SelectedPower = CollectionsUtil.Grab<PlayerCustom.PlayerPrimaryRolePower>(playerDraftData.OfferedPowers, 1).First();
			}
			if (playerDraftData.OfferedSecondaryRoles.Any())
			{
				playerDraftData.SelectedSecondaryRole = CollectionsUtil.Grab<PlayerCustom.PlayerSecondaryRole>(playerDraftData.OfferedSecondaryRoles, 1).First();
			}
			Instance.FinishChoice(playerIndex);
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.DraftManager::Rpc_Random(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Random_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Random(runner, playerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Role_Option(NetworkRunner runner, int playerIndex, int roleOptionIndex, bool active)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Invalid comparison between Unknown and I4
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBehaviourUtils.InvokeRpc)
		{
			NetworkBehaviourUtils.InvokeRpc = false;
		}
		else
		{
			if ((Object)(object)runner == (Object)null)
			{
				throw new ArgumentNullException("runner");
			}
			if ((int)runner.Stage == 4)
			{
				return;
			}
			if (runner.HasAnyActiveConnections())
			{
				int num = 60;
				SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
				byte* data = SimulationMessage.GetData(ptr);
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.DraftManager::Rpc_Role_Option(Fusion.NetworkRunner,System.Int32,System.Int32,System.Boolean)")), data);
				*(int*)(data + num2) = playerIndex;
				num2 += 4;
				*(int*)(data + num2) = roleOptionIndex;
				num2 += 4;
				ReadWriteUtilsForWeaver.WriteBoolean((int*)(data + num2), active);
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 12;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		if (runner.IsServer)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			switch (roleOptionIndex)
			{
			case 0:
				player.DraftWantsSecondAgent = active;
				break;
			case 1:
				player.DraftWantsSecondLover = active;
				break;
			}
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.DraftManager::Rpc_Role_Option(Fusion.NetworkRunner,System.Int32,System.Int32,System.Boolean)")]
	[Preserve]
	protected unsafe static void Rpc_Role_Option_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int roleOptionIndex = *(int*)(data + num);
		num += 4;
		bool flag = ReadWriteUtilsForWeaver.ReadBoolean((int*)(data + num));
		num += 4;
		bool active = flag;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Role_Option(runner, playerIndex, roleOptionIndex, active);
	}
}
