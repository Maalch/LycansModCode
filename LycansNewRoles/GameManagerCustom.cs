using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.NewPrimaryRoles;
using LycansNewRoles.PowerObjects;
using LycansNewRoles.Stats;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(5)]
public class GameManagerCustom : NetworkBehaviour
{
	public int MayorActionIndex;

	public int CollectedLoot;

	public float SoloRoleDifficulty = 1f;

	public float? IsolationTime = null;

	public Dictionary<int, int> TransformationsAmountByDay = new Dictionary<int, int>();

	public Dictionary<int, int> DetransformationsAmountByDay = new Dictionary<int, int>();

	public EventsManager EventsManager = new EventsManager();

	public static List<Accessory> SpawnableAccessories = new List<Accessory>();

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe int CurrentDay
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameManagerCustom.CurrentDay. Networked properties can only be accessed when Spawned() has been called.");
			}
			return *base.Ptr;
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameManagerCustom.CurrentDay. Networked properties can only be accessed when Spawned() has been called.");
			}
			*base.Ptr = value;
		}
	}

	[Networked(OnChanged = "CurrentMayorChanged")]
	[NetworkedWeaved(1, 1)]
	public unsafe PlayerRef CurrentMayor
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameManagerCustom.CurrentMayor. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[1];
		}
		set
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameManagerCustom.CurrentMayor. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 1, value);
		}
	}

	[Networked(OnChanged = "MayorActionCooldownTimerChanged")]
	[NetworkedWeaved(2, 1)]
	public unsafe TickTimer MayorActionCooldownTimer
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameManagerCustom.MayorActionCooldownTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[2];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameManagerCustom.MayorActionCooldownTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 2, value);
		}
	}

	[Networked]
	[NetworkedWeaved(3, 1)]
	public unsafe TickTimer EachSecondGlobalTimer
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameManagerCustom.EachSecondGlobalTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)base.Ptr[3];
		}
		set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing GameManagerCustom.EachSecondGlobalTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 3, value);
		}
	}

	public static GameManagerCustom Instance { get; private set; }

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

	public void NewGame()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		CurrentDay = 0;
		SoloRoleDifficulty = 1f;
		CurrentMayor = PlayerRef.None;
		MayorActionCooldownTimer = TickTimer.None;
		CollectedLoot = 0;
		EachSecondGlobalTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 1f);
		TransformationsAmountByDay.Clear();
		DetransformationsAmountByDay.Clear();
		if (((SimulationBehaviour)GameManager.Instance).Runner.IsServer)
		{
			foreach (DeceiverIllusionComponent illusion in DeceiverIllusionComponent.Illusions)
			{
				((SimulationBehaviour)GameManager.Instance).Runner.Despawn(((Component)illusion).GetComponent<NetworkObject>(), false);
			}
			foreach (MerchantCoin allCoin in MerchantCoin.AllCoins)
			{
				((SimulationBehaviour)GameManager.Instance).Runner.Despawn(((Component)allCoin).GetComponent<NetworkObject>(), false);
			}
			foreach (InvestigatorHint allHint in InvestigatorHint.AllHints)
			{
				((SimulationBehaviour)GameManager.Instance).Runner.Despawn(((Component)allHint).GetComponent<NetworkObject>(), false);
			}
			foreach (SurvivalistHint allHint2 in SurvivalistHint.AllHints)
			{
				((SimulationBehaviour)GameManager.Instance).Runner.Despawn(((Component)allHint2).GetComponent<NetworkObject>(), false);
			}
			foreach (HermitHideout allHideout in HermitHideout.AllHideouts)
			{
				((SimulationBehaviour)GameManager.Instance).Runner.Despawn(((Component)allHideout).GetComponent<NetworkObject>(), false);
			}
		}
		DeceiverIllusionComponent.Illusions.Clear();
		MerchantCoin.AllCoins.Clear();
		InvestigatorHint.AllHints.Clear();
		SurvivalistHint.AllHints.Clear();
		HermitHideout.AllHideouts.Clear();
	}

	public void NewDay()
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		CurrentDay++;
		if (CurrentDay > 1)
		{
			UpdateSoloRoleDifficulty();
		}
		foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead)))
		{
			PlayerCustom.PlayerPrimaryRolePower primaryRolePower = item.PrimaryRolePower;
			PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
			if (playerPrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Purifier && (Object)(object)item.PlayerController.Item == (Object)null)
			{
				Item[] value = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
				Item val = value.FirstOrDefault((Item o) => o is MolotovItem);
				if ((Object)(object)val == (Object)null)
				{
					break;
				}
				Item val2 = ItemUtility.SpawnItem(val, Vector3.zero, Quaternion.identity, ((SimulationBehaviour)this).Runner);
				val2.Rpc_ClaimItem(item.Ref);
			}
		}
	}

	public void UpdateSoloRoleDifficulty()
	{
		int num = PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsDead)));
		float num2 = 1f + (float)(CurrentDay - 1) * 0.2f - 1.5f * (float)num / (float)PlayerRegistry.Count;
		SoloRoleDifficulty = Mathf.Clamp(num2, 0.5f, 1.75f);
		Plugin.Logger.LogInfo((object)("Difficulty is now " + SoloRoleDifficulty));
	}

	public void AddTransformation()
	{
		if (!TransformationsAmountByDay.ContainsKey(CurrentDay))
		{
			TransformationsAmountByDay[CurrentDay] = 0;
		}
		TransformationsAmountByDay[CurrentDay]++;
		Plugin.Logger.LogInfo((object)("Added transformation, now: " + TransformationsAmountByDay[CurrentDay] + ", date: " + DateTime.UtcNow));
	}

	public void AddDetransformation()
	{
		if (!DetransformationsAmountByDay.ContainsKey(CurrentDay))
		{
			DetransformationsAmountByDay[CurrentDay] = 0;
		}
		DetransformationsAmountByDay[CurrentDay]++;
		Plugin.Logger.LogInfo((object)("Added detransformation, now: " + DetransformationsAmountByDay[CurrentDay] + ", date: " + DateTime.UtcNow));
	}

	public void PickRandomMayor()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom playerCustom = CollectionsUtil.Grab<PlayerCustom>(PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Kidnapped) && o.Ref != CurrentMayor).ToList(), 1).FirstOrDefault();
		CurrentMayor = playerCustom.Ref;
	}

	public void SetMayorAction(int actionIndex)
	{
		MayorActionIndex = actionIndex;
		UIManager.MayorPanelForMayor.SetActiveAction(actionIndex);
	}

	public void ChangeMayorAction()
	{
		if (MayorActionIndex == 3)
		{
			SetMayorAction(0);
		}
		else
		{
			SetMayorAction(MayorActionIndex + 1);
		}
	}

	[Preserve]
	public static void CurrentMayorChanged(Changed<GameManagerCustom> changed)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (changed.Behaviour.CurrentMayor == PlayerRef.None)
			{
				return;
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CurrentMayor);
			if (changed.Behaviour.CurrentMayor != PlayerRef.None)
			{
				foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
				{
					allPlayer.MayorVoteTarget = PlayerRef.None;
				}
			}
			if (((SimulationBehaviour)changed.Behaviour).Runner.IsServer)
			{
				SessionStats.Stats.CurrentGame.AddEvent(GameEvent.GameEventType.NewMayor, ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString());
			}
			GameObject val = Object.Instantiate<GameObject>(PlayerCustom.MayorParticleSystemPrefab, ((Component)player.PlayerController).transform.position, Quaternion.identity);
			val.SetActive(true);
			SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
			selfDestroyingObjectComponent.Init(6f);
			AudioManager.PlayPosition("Mayor", ((Component)player.PlayerController).transform.position, (MixerTarget)2, 100f, 1f);
			UIManager.MayorPanelForMayor.UpdateMayor(player.Ref);
			UIManager.MayorPanelForOthers.UpdateMayor(player.Ref);
			int required = Mathf.CeilToInt((float)(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) / 2));
			UIManager.MayorPanelForOthers.UpdateCurrentVote();
			UIManager.MayorPanelForOthers.UpdateDestitutionCount(0, required);
			UIManager.MayorPanelForOthers.UpdateDifferentCount(0, required, PlayerRef.None);
			Instance.SetMayorAction(0);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CurrentMayorChanged error: " + ex));
		}
	}

	[Preserve]
	public static void MayorActionCooldownTimerChanged(Changed<GameManagerCustom> changed)
	{
		try
		{
			UIManager.MayorPanelForMayor.UpdateCooldown();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MayorActionCooldownTimerChanged error: " + ex));
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_153c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_186c: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1805: Invalid comparison between Unknown and I4
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e36: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Invalid comparison between Unknown and I4
		//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_164a: Unknown result type (might be due to invalid IL or missing references)
		//IL_164f: Unknown result type (might be due to invalid IL or missing references)
		//IL_166c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1191: Unknown result type (might be due to invalid IL or missing references)
		//IL_1126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ead: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebe: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1252: Unknown result type (might be due to invalid IL or missing references)
		//IL_1258: Invalid comparison between Unknown and I4
		//IL_128d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1452: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_130b: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).Runner.IsServer)
		{
			return;
		}
		EventsManager.ServerUpdate(((SimulationBehaviour)this).Runner);
		TickTimer val = MayorActionCooldownTimer;
		if (((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
		{
			MayorActionCooldownTimer = TickTimer.None;
		}
		val = EachSecondGlobalTimer;
		if (!((TickTimer)(ref val)).Expired(((SimulationBehaviour)this).Runner))
		{
			return;
		}
		EffectManager effectManager = Traverse.Create(typeof(EffectManager)).Field<EffectManager>("_instance").Value;
		IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead));
		Vector3 val5 = default(Vector3);
		foreach (PlayerCustom playerCustom in enumerable)
		{
			PlayerController player = playerCustom.PlayerController;
			if (LycansUtility.GameActuallyInPlay)
			{
				if (NetworkBool.op_Implicit(playerCustom.Vampire))
				{
					IEnumerable<PlayerCustom> enumerable2 = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != playerCustom.Ref && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(player.IsDead) && (!NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) || !NetworkBool.op_Implicit(player.IsWolf)) && !NetworkBool.op_Implicit(o.Dying));
					foreach (PlayerCustom item in enumerable2)
					{
						float num = Vector3.Distance(((Component)player).transform.position, ((Component)item.PlayerController).transform.position);
						if (num < 25f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
						{
							int num2 = Mathf.RoundToInt(item.PlayerController.Hunger * 0.03f);
							if (item.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover || (int)item.PlayerController.Role != 0)
							{
								PlayerController playerController = item.PlayerController;
								playerController.Hunger -= (float)num2;
							}
							playerCustom.IncreaseHealth(num2);
							PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectVampireTarget", ((SimulationBehaviour)player).Runner);
						}
					}
				}
				if (NetworkBool.op_Implicit(playerCustom.Stinking))
				{
					IEnumerable<PlayerCustom> enumerable3 = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != playerCustom.Ref && !NetworkBool.op_Implicit(o.PlayerController.IsDead));
					foreach (PlayerCustom item2 in enumerable3)
					{
						float num3 = Vector3.Distance(((Component)player).transform.position, ((Component)item2.PlayerController).transform.position);
						float num4 = (NetworkBool.op_Implicit(item2.PlayerController.IsWolf) ? 15f : 5f);
						if (num3 < num4)
						{
							PlayerCustom.ApplyEffectToPlayer(item2.PlayerController, "LycansNewRoles.EffectNauseated", ((SimulationBehaviour)player).Runner);
						}
					}
				}
				switch (playerCustom.NewPrimaryRole)
				{
				case PlayerCustom.PlayerNewPrimaryRole.VillageIdiot:
				{
					if (!NetworkBool.op_Implicit(GameManager.LightingManager.IsNight))
					{
						break;
					}
					float num12 = 200f;
					foreach (PlayerCustom item3 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != player.Ref))
					{
						float num13 = Vector3.Distance(((Component)player).transform.position, ((Component)item3.PlayerController).transform.position);
						float num14 = (NetworkBool.op_Implicit(item3.PlayerController.IsWolf) ? 40f : 30f) * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
						if (num13 <= num14)
						{
							float num15 = (0f + 3000f * (1f - num13 / num14)) / (float)(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) - 1);
							if (NetworkBool.op_Implicit(item3.PlayerController.IsWolf))
							{
								num15 *= 2f;
							}
							num12 -= num15;
						}
					}
					if (NetworkBool.op_Implicit(player.IsWolf))
					{
						num12 -= 400f;
					}
					float num16 = (float)playerCustom.PrimaryRolePowerCurrentMaterials + num12;
					if (num16 > 10000f)
					{
						num16 = 10000f;
					}
					if (num16 < 0f)
					{
						num16 = 0f;
					}
					playerCustom.PrimaryRolePowerCurrentMaterials = Mathf.RoundToInt(num16);
					break;
				}
				case PlayerCustom.PlayerNewPrimaryRole.Spy:
				{
					if (!(playerCustom.PrimaryRoleTargetRef != PlayerRef.None))
					{
						break;
					}
					PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef);
					float num10 = BalancingValues.SpyMaximumRange(GameManager.Instance.MapID);
					if (Vector3.Distance(((Component)player).transform.position, ((Component)player2.PlayerController).transform.position) <= num10)
					{
						playerCustom.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
						bool flag = LycansUtility.CanPlayerSeeOtherPlayer(playerCustom, player2, num10);
						bool flag2 = !NetworkBool.op_Implicit(player.IsMoving);
						float num11 = 10f;
						if (flag && flag2)
						{
							num11 = 63f;
						}
						else if (flag)
						{
							num11 = 32f;
						}
						else if (flag2)
						{
							num11 = 18f;
						}
						num11 *= (float)Plugin.CustomConfig.SpyPercentage * 0.01f;
						num11 /= Instance.SoloRoleDifficulty;
						playerCustom.SoloRoleObjectiveCount += Mathf.RoundToInt(num11);
					}
					else
					{
						playerCustom.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
					}
					break;
				}
				case PlayerCustom.PlayerNewPrimaryRole.Scientist:
				{
					int num6 = 0;
					foreach (PlayerCustom item4 in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf) && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != player.Ref))
					{
						float num7 = Vector3.Distance(((Component)player).transform.position, ((Component)item4).transform.position);
						float num8 = 40f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
						if (!(num7 <= num8))
						{
							continue;
						}
						float num9 = ScientistUtility.GetBasePower(playerCustom, item4.PlayerController, num7, num8);
						if (LycansUtility.CanPlayerSeeOtherPlayer(playerCustom, item4, num8))
						{
							num9 *= 4f;
							if (item4.PlayerController.IsStarving())
							{
								num9 *= 0.4f;
							}
						}
						if (player.MovementAction == 1)
						{
							num9 *= 0.8f;
						}
						if (NetworkBool.op_Implicit(player.PlayerEffectManager.Invisible))
						{
							num9 *= 0.4f;
						}
						else if (NetworkBool.op_Implicit(player.PlayerEffectManager.BonusSpeed))
						{
							num9 *= 0.3f;
						}
						int val2 = Mathf.RoundToInt(num9);
						num6 = Math.Max(num6, val2);
					}
					if (num6 > 0)
					{
						playerCustom.SoloRoleObjectiveCount += num6;
					}
					break;
				}
				case PlayerCustom.PlayerNewPrimaryRole.Cultist:
				{
					int count = CultistSkull.AllSkulls.Count;
					if (count > 0)
					{
						float num5 = (float)count * 15f * ((float)Plugin.CustomConfig.CultistSpeed * 0.01f) * BalancingValues.CultistChargeGainMultiplierForPlayersAmount(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))));
						num5 /= SoloRoleDifficulty;
						playerCustom.SoloRoleObjectiveCount += Mathf.RoundToInt(num5);
					}
					break;
				}
				}
				switch (playerCustom.PrimaryRolePower)
				{
				case PlayerCustom.PlayerPrimaryRolePower.Peasant:
				{
					if (NetworkBool.op_Implicit(playerCustom.NewPrimaryRoleUniqueBool))
					{
						break;
					}
					float num21 = 0f;
					foreach (PlayerCustom item5 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != player.Ref))
					{
						float num22 = Vector3.Distance(((Component)player).transform.position, ((Component)item5.PlayerController).transform.position);
						float num23 = 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
						if (num22 <= num23)
						{
							float num24 = (70f + 260f * (1f - num22 / num23)) / (float)(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) - 1);
							num21 += num24;
						}
					}
					if (num21 > 0f)
					{
						playerCustom.AddMaterials(Mathf.RoundToInt(num21));
					}
					break;
				}
				case PlayerCustom.PlayerPrimaryRolePower.Avenger:
				{
					float num26 = 0f;
					if (PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead) && o.Ref != player.Ref && NetworkBool.op_Implicit(o.IsWolf) && Vector3.Distance(((Component)player).transform.position, ((Component)o).transform.position) <= 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))))
					{
						num26 += 100f;
					}
					if (num26 > 0f)
					{
						playerCustom.AddMaterials(Mathf.RoundToInt(num26));
					}
					break;
				}
				case PlayerCustom.PlayerPrimaryRolePower.Survivalist:
					if (Random.value < 0.035f)
					{
						List<PlayerController> list = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsMoving) && !NetworkBool.op_Implicit(o.IsDead) && o.Ref != playerCustom.Ref)).ToList();
						if (list.Any())
						{
							PlayerController val4 = CollectionsUtil.Grab<PlayerController>(list, 1).First();
							((Vector3)(ref val5))._002Ector(((Component)val4).transform.position.x, ((Component)val4).transform.position.y + 0.5f, ((Component)val4).transform.position.z);
							SurvivalistHint.CreateNewHint(((SimulationBehaviour)this).Runner, playerCustom, ((Component)val4).transform.position);
						}
					}
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Mystic:
				{
					float num28 = Mathf.InverseLerp(0f, (float)GameManager.Instance.MaxHunger, playerCustom.PlayerController.Hunger);
					int amount = Mathf.RoundToInt(Mathf.Lerp(150f, 25f, num28));
					playerCustom.AddMaterials(amount);
					break;
				}
				case PlayerCustom.PlayerPrimaryRolePower.Shadow:
				{
					float num25 = 35f;
					if (PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead) && o.Ref != player.Ref && NetworkBool.op_Implicit(o.IsWolf) && Vector3.Distance(((Component)player).transform.position, ((Component)o).transform.position) <= 30f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))))
					{
						num25 += 215f;
					}
					playerCustom.AddMaterials(Mathf.RoundToInt(num25));
					break;
				}
				case PlayerCustom.PlayerPrimaryRolePower.Necromancer:
				{
					PlayerRef primaryRoleTargetRef = playerCustom.PrimaryRoleTargetRef;
					if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && !NetworkBool.op_Implicit(PlayerRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef).IsDead))
					{
						playerCustom.PrimaryRolePowerCurrentMaterials = Mathf.Max(0, playerCustom.PrimaryRolePowerCurrentMaterials - 300);
						if (playerCustom.PrimaryRolePowerCurrentMaterials == 0)
						{
							PlayerController player3 = PlayerRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef);
							Effect val3 = player3.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is ResurrectedEffect);
							if ((Object)(object)val3 != (Object)null)
							{
								player3.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val3).Object.Id);
							}
						}
					}
					else if (NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf))
					{
						playerCustom.AddMaterials(150);
					}
					break;
				}
				case PlayerCustom.PlayerPrimaryRolePower.Possessor:
				{
					if (!(playerCustom.PrimaryRoleTargetRef != PlayerRef.None) || playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
					{
						break;
					}
					PlayerCustom player4 = PlayerCustomRegistry.GetPlayer(playerCustom.PrimaryRoleTargetRef);
					float num27 = BalancingValues.PossessorMaximumRangeByMap(GameManager.Instance.MapID);
					if (Vector3.Distance(((Component)player).transform.position, ((Component)player4.PlayerController).transform.position) <= num27)
					{
						playerCustom.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(true);
						playerCustom.PrimaryRolePowerCurrentMaterials += 400;
						if (playerCustom.PrimaryRolePowerCurrentMaterials >= playerCustom.PowerMaterialsInfo.RequiredMaterials)
						{
							playerCustom.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
						}
					}
					else
					{
						playerCustom.NewPrimaryRoleUniqueBool = NetworkBool.op_Implicit(false);
					}
					break;
				}
				case PlayerCustom.PlayerPrimaryRolePower.Ritualist:
				{
					float num17 = 0f;
					foreach (PlayerCustom item6 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != player.Ref))
					{
						float num18 = Vector3.Distance(((Component)player).transform.position, ((Component)item6.PlayerController).transform.position);
						float num19 = 25f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
						if (num18 <= num19)
						{
							float num20 = (100f + 500f * (1f - num18 / num19)) / (float)(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) - 1);
							num17 += num20;
						}
					}
					if (num17 > 0f)
					{
						playerCustom.AddMaterials(Mathf.RoundToInt(num17));
					}
					break;
				}
				case PlayerCustom.PlayerPrimaryRolePower.Saboteur:
					playerCustom.AddMaterials(160);
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Tracker:
					playerCustom.AddMaterials(100);
					break;
				}
				if ((Object)(object)playerCustom.Accessory != (Object)null && playerCustom.Accessory is AccessoryRing accessoryRing)
				{
					bool flag3 = NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf) || PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && Vector3.Distance(((Component)playerCustom.PlayerController).transform.position, ((Component)o).transform.position) < Traverse.Create((object)effectManager).Method("WolfMusicDistance", new List<Type> { typeof(PlayerController) }.ToArray(), (object[])null).GetValue<float>(new object[1] { o })));
					if (accessoryRing.EffectActive != flag3)
					{
						accessoryRing.EffectActive = flag3;
						playerCustom.UpdateMoveSpeed();
					}
				}
				if (NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf) || PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && Vector3.Distance(((Component)playerCustom.PlayerController).transform.position, ((Component)o).transform.position) < Traverse.Create((object)effectManager).Method("WolfMusicDistance", new List<Type> { typeof(PlayerController) }.ToArray(), (object[])null).GetValue<float>(new object[1] { o }))))
				{
					playerCustom.SecondsTransformedOrNearTransformedWolfToday++;
				}
				if (Instance.EventsManager.CurrentEvent == EventsManager.EventType.Spellstorm && Random.value < 0.01f)
				{
					AccessorySpellbook.CastEffectOnPlayer(playerCustom, playerCustom.PlayerController, ((SimulationBehaviour)this).Runner);
				}
				if (Instance.EventsManager.CurrentEvent == EventsManager.EventType.Plague && (int)playerCustom.PlayerController.Role != 1 && playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover && !playerCustom.IsOutOfTheWorld && !NetworkBool.op_Implicit(playerCustom.Dying))
				{
					float num29 = 0.4f;
					foreach (PlayerCustom item7 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != player.Ref))
					{
						float num30 = Vector3.Distance(((Component)player).transform.position, ((Component)item7.PlayerController).transform.position);
						float num31 = 10f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
						if (num30 <= num31)
						{
							float num32 = (5f + 20f * (1f - num30 / num31)) / (8f + (float)(PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead))) - 1));
							num29 -= num32;
						}
					}
					playerCustom.PlayerController.Hunger = Mathf.Clamp(playerCustom.PlayerController.Hunger + num29, 0f, (float)GameManager.Instance.MaxHunger);
				}
				if (playerCustom.Stats != null)
				{
					switch (playerCustom.PlayerController.MovementAction)
					{
					case 0:
						if (NetworkBool.op_Implicit(playerCustom.PlayerController.IsMoving))
						{
							playerCustom.Stats.SecondsSpentWalkingStanding++;
						}
						else
						{
							playerCustom.Stats.SecondsSpentImmobileStanding++;
						}
						break;
					case 1:
						if (NetworkBool.op_Implicit(playerCustom.PlayerController.IsMoving))
						{
							playerCustom.Stats.SecondsSpentWalkingCrouched++;
						}
						else
						{
							playerCustom.Stats.SecondsSpentImmobileCrouched++;
						}
						break;
					case 2:
						playerCustom.Stats.SecondsSpentRunning++;
						break;
					}
				}
			}
			if (NetworkBool.op_Implicit(playerCustom.Energized))
			{
				bool flag4 = false;
				switch (playerCustom.NewPrimaryRole)
				{
				case PlayerCustom.PlayerNewPrimaryRole.Scientist:
				case PlayerCustom.PlayerNewPrimaryRole.Beast:
				case PlayerCustom.PlayerNewPrimaryRole.Voodoo:
				case PlayerCustom.PlayerNewPrimaryRole.Kidnapper:
				case PlayerCustom.PlayerNewPrimaryRole.Cultist:
					flag4 = true;
					break;
				}
				PlayerCustom.PlayerPrimaryRolePower primaryRolePower = playerCustom.PrimaryRolePower;
				PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower = primaryRolePower;
				if (playerPrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Deceiver || playerPrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Warlock)
				{
					flag4 = true;
				}
				if (flag4)
				{
					val = playerCustom.PrimaryRolePowerCooldownTimer;
					if (((TickTimer)(ref val)).IsRunning)
					{
						val = playerCustom.PrimaryRolePowerCooldownTimer;
						if (((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)this).Runner) > 1f)
						{
							PlayerCustom playerCustom2 = playerCustom;
							NetworkRunner runner = ((SimulationBehaviour)this).Runner;
							val = playerCustom.PrimaryRolePowerCooldownTimer;
							playerCustom2.PrimaryRolePowerCooldownTimer = TickTimer.CreateFromSeconds(runner, ((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)this).Runner).Value - 1f);
						}
					}
				}
				else
				{
					bool flag5 = false;
					switch (playerCustom.PrimaryRolePower)
					{
					case PlayerCustom.PlayerPrimaryRolePower.Saboteur:
					case PlayerCustom.PlayerPrimaryRolePower.Tracker:
					case PlayerCustom.PlayerPrimaryRolePower.Bomber:
					case PlayerCustom.PlayerPrimaryRolePower.Ritualist:
					case PlayerCustom.PlayerPrimaryRolePower.Host:
					case PlayerCustom.PlayerPrimaryRolePower.Peasant:
					case PlayerCustom.PlayerPrimaryRolePower.Exorcist:
					case PlayerCustom.PlayerPrimaryRolePower.Avenger:
					case PlayerCustom.PlayerPrimaryRolePower.Investigator:
					case PlayerCustom.PlayerPrimaryRolePower.Survivalist:
					case PlayerCustom.PlayerPrimaryRolePower.Priest:
					case PlayerCustom.PlayerPrimaryRolePower.Scout:
					case PlayerCustom.PlayerPrimaryRolePower.Magician:
					case PlayerCustom.PlayerPrimaryRolePower.Mystic:
					case PlayerCustom.PlayerPrimaryRolePower.Shadow:
					case PlayerCustom.PlayerPrimaryRolePower.Hermit:
					case PlayerCustom.PlayerPrimaryRolePower.Runemaster:
					case PlayerCustom.PlayerPrimaryRolePower.Alchemist:
					case PlayerCustom.PlayerPrimaryRolePower.Spotter:
					case PlayerCustom.PlayerPrimaryRolePower.Purifier:
						flag5 = true;
						break;
					}
					if (flag5)
					{
						playerCustom.AddMaterials(BalancingValues.EnergizedMaterialsGainByPower(playerCustom.PrimaryRolePower));
					}
					else
					{
						bool flag6 = false;
						PlayerCustom.PlayerPrimaryRolePower primaryRolePower2 = playerCustom.PrimaryRolePower;
						PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower2 = primaryRolePower2;
						if (playerPrimaryRolePower2 == PlayerCustom.PlayerPrimaryRolePower.Hunter)
						{
							flag6 = true;
						}
						if (!flag6)
						{
							playerCustom.IncreaseHealth(0.6f);
						}
					}
				}
			}
			if (NetworkBool.op_Implicit(playerCustom.Confused) && !NetworkBool.op_Implicit(playerCustom.Downed) && NetworkBool.op_Implicit(playerCustom.PlayerController.IsTalking) && Random.value < 0.15f)
			{
				float value = (((int)GameManager.LocalGameState == 4) ? 8f : 2f);
				PlayerCustom.ApplyEffectToPlayer(playerCustom.PlayerController, "LycansNewRoles.EffectDowned", ((SimulationBehaviour)this).Runner, 1f, value);
			}
		}
		EachSecondGlobalTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 1f);
	}

	[Rpc]
	public unsafe static void Rpc_Vote_Mayor(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Invalid comparison between Unknown and I4
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
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
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.GameManagerCustom::Rpc_Vote_Mayor(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
				*(int*)(data + num2) = playerIndex;
				num2 += 4;
				*(int*)(data + num2) = targetPlayerIndex;
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 8;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
		PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
		if (player.Ref == Instance.CurrentMayor || NetworkBool.op_Implicit(player.PlayerController.PlayerEffectManager.Paranoia))
		{
			return;
		}
		player.MayorVoteTarget = player2.Ref;
		int num3 = Mathf.CeilToInt((float)(PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Kidnapped)) / 2));
		if (num3 < 3)
		{
			num3 = 3;
		}
		int num4 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.MayorVoteTarget == Instance.CurrentMayor);
		PlayerRef val = PlayerRef.None;
		int num5 = 0;
		foreach (PlayerCustom differentPlayerCustom in PlayerCustomRegistry.AllPlayers.Where((PlayerCustom o) => o.Ref != Instance.CurrentMayor))
		{
			int num6 = PlayerCustomRegistry.CountWhere((PlayerCustom o) => o.MayorVoteTarget == differentPlayerCustom.Ref);
			if (num6 > num5)
			{
				val = differentPlayerCustom.Ref;
				num5 = num6;
			}
		}
		UIManager.MayorPanelForOthers.UpdateDestitutionCount(num4, num3);
		UIManager.MayorPanelForOthers.UpdateDifferentCount(num5, num3, val);
		if (runner.IsServer)
		{
			if (num4 >= num3)
			{
				Instance.PickRandomMayor();
			}
			else if (num5 >= num3)
			{
				Instance.CurrentMayor = val;
			}
		}
		if (player.Ref == PlayerController.Local.Ref)
		{
			UIManager.MayorPanelForOthers.UpdateCurrentVote();
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.GameManagerCustom::Rpc_Vote_Mayor(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Vote_Mayor_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Vote_Mayor(runner, playerIndex, targetPlayerIndex);
	}

	[Rpc]
	public unsafe static void Rpc_Mayor_Action(NetworkRunner runner, int playerIndex, int targetPlayerIndex, int actionIndex)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Invalid comparison between Unknown and I4
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
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
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.GameManagerCustom::Rpc_Mayor_Action(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")), data);
				*(int*)(data + num2) = playerIndex;
				num2 += 4;
				*(int*)(data + num2) = targetPlayerIndex;
				num2 += 4;
				*(int*)(data + num2) = actionIndex;
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 8;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
		PlayerCustom targetPlayerCustom = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
		if (Instance.CurrentMayor != player.Ref)
		{
			return;
		}
		TickTimer mayorActionCooldownTimer = Instance.MayorActionCooldownTimer;
		if (((TickTimer)(ref mayorActionCooldownTimer)).IsRunning || NetworkBool.op_Implicit(targetPlayerCustom.Downed))
		{
			return;
		}
		switch (actionIndex)
		{
		case 0:
			if (runner.IsServer)
			{
				PlayerCustom.ApplyEffectToPlayer(targetPlayerCustom.PlayerController, "LycansNewRoles.EffectDowned", runner, 1f, 7f);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)targetPlayerCustom.PlayerController).transform.position, 100f, 1f);
			}
			player.PlayerController.UpdateAnimation(Animator.StringToHash("Attacking"), true);
			((MonoBehaviour)player.PlayerController).StartCoroutine("WaitAndResetAttackAnimation");
			Instance.MayorActionCooldownTimer = TickTimer.CreateFromSeconds(runner, 15f);
			break;
		case 1:
			if (runner.IsServer)
			{
				IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != Instance.CurrentMayor && o.Ref != targetPlayerCustom.Ref);
				foreach (PlayerCustom item in enumerable)
				{
					if (!NetworkBool.op_Implicit(item.Mute))
					{
						PlayerCustom.ApplyEffectToPlayer(item.PlayerController, "LycansNewRoles.EffectMute", runner, 1f, 8f);
					}
				}
				PlayerCustom.Rpc_Effect_On_Player(runner, targetPlayerCustom.Index, 6);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("PossessorDance"), ((Component)targetPlayerCustom.PlayerController).transform.position, 100f, 1f);
			}
			player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackCall1H01_L");
			if (targetPlayerCustom.IsCurrentlyPlayedOrObserved)
			{
				UIManager.ShowRedCenterMessage("NALES_UI_MAYOR_SPEECH", 0.5f, 4f);
			}
			Instance.MayorActionCooldownTimer = TickTimer.CreateFromSeconds(runner, 25f);
			break;
		case 2:
			if (runner.IsServer)
			{
				IEnumerable<PlayerCustom> enumerable2 = PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != Instance.CurrentMayor);
				foreach (PlayerCustom item2 in enumerable2)
				{
					if (!NetworkBool.op_Implicit(item2.Mute))
					{
						PlayerCustom.ApplyEffectToPlayer(item2.PlayerController, "LycansNewRoles.EffectMute", runner, 1f, 8f);
					}
				}
				PlayerCustom.Rpc_Effect_On_Player(runner, targetPlayerCustom.Index, 6);
				GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("PossessorDance"), ((Component)targetPlayerCustom.PlayerController).transform.position, 100f, 1f);
			}
			player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackCall1H01_L");
			Instance.MayorActionCooldownTimer = TickTimer.CreateFromSeconds(runner, 25f);
			break;
		case 3:
			if (runner.IsServer)
			{
				Instance.CurrentMayor = targetPlayerCustom.Ref;
				Instance.MayorActionCooldownTimer = TickTimer.CreateFromSeconds(runner, 5f);
			}
			player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackCall1H01_L");
			break;
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.GameManagerCustom::Rpc_Mayor_Action(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Mayor_Action_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		int actionIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Mayor_Action(runner, playerIndex, targetPlayerIndex, actionIndex);
	}

	[Rpc]
	public unsafe static void Rpc_New_Event(NetworkRunner runner, int eventIndex)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Invalid comparison between Unknown and I4
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
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
				int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.GameManagerCustom::Rpc_New_Event(Fusion.NetworkRunner,System.Int32)")), data);
				*(int*)(data + num2) = eventIndex;
				num2 += 4;
				((SimulationMessage)ptr).Offset = num2 * 8;
				((SimulationMessage)ptr).SetStatic();
				runner.SendRpc(ptr);
			}
		}
		EventsManager.EventType eventType = (EventsManager.EventType)eventIndex;
		Instance.EventsManager.NewEvent(eventType);
		UIManager.ShowRedCenterMessage("NALES_EVENT_ANNOUNCEMENT", 0.5f, 4f, new List<object> { TranslationManager.Instance.GetTranslation("NALES_EVENT_" + eventType.ToString().ToUpper()) });
		switch (eventType)
		{
		case EventsManager.EventType.Haste:
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.UpdateMoveSpeed();
			}
			break;
		}
		if (runner.IsServer)
		{
			SessionStats.Stats.CurrentGame.AddEvent(GameEvent.GameEventType.DailyEventStart, eventType.ToString());
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.GameManagerCustom::Rpc_New_Event(Fusion.NetworkRunner,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_New_Event_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int eventIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_New_Event(runner, eventIndex);
	}
}
