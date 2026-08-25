using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "SpawnRandomItems")]
internal class SpawnRandomItemsPatch
{
	private static bool Prefix(GameManager __instance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Expected O, but got Unknown
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Expected O, but got Unknown
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Expected O, but got Unknown
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Expected O, but got Unknown
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBool.op_Implicit(DraftManager.Instance.Active))
			{
				return false;
			}
			Item[] value = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
			List<Item> list = value.Where((Item o) => Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)]).ToList();
			BulletItem value2 = Traverse.Create((object)__instance).Field<BulletItem>("bulletPrefab").Value;
			Traverse val = Traverse.Create((object)__instance).Method("GetAndLockRandomItemSpawn", Array.Empty<object>());
			int num = PlayerCustomRegistry.CountWhere((PlayerCustom o) => PlayerCustom.IsPrimaryRolePowerForEliteVillagers(o.PrimaryRolePower));
			if (num > 0)
			{
				int num2 = 0;
				num2 = ((GameManagerCustom.Instance.EventsManager.CurrentEvent != EventsManager.EventType.Harvest) ? Mathf.RoundToInt((float)(__instance.ItemsSpawnRate * 3) * (0.25f + (float)num * 0.5f)) : Mathf.RoundToInt((float)(__instance.ItemsSpawnRate * 3) * (0.25f + (float)num * 0.5f * 1.75f)));
				for (int num3 = 0; num3 < num2; num3++)
				{
					ItemSpawner val2 = (ItemSpawner)val.GetValue();
					if ((Object)(object)val2 != (Object)null)
					{
						((SimulationBehaviour)__instance).Runner.Spawn<BulletItem>(value2, (Vector3?)((Component)val2).transform.position, (Quaternion?)((Component)val2).transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true);
					}
				}
			}
			if (list.Count > 0)
			{
				List<Item> list2 = new List<Item>();
				foreach (Item item in list)
				{
					Item val3 = item;
					Item val4 = val3;
					if (!(val4 is TrapItem))
					{
						if (!(val4 is SmokeItem))
						{
							if (!(val4 is SpyglassItem))
							{
								if (!(val4 is LockItem))
								{
									if (!(val4 is MagicScrollItem))
									{
										if (!(val4 is PhasingDiamondItem))
										{
											if (!(val4 is GrenadeItem))
											{
												if (!(val4 is SleepingGasItem))
												{
													if (!(val4 is MolotovItem))
													{
														if (val4 is RadarItem)
														{
															for (int num4 = 0; num4 < 7; num4++)
															{
																list2.Add(item);
															}
														}
													}
													else
													{
														for (int num5 = 0; num5 < 8; num5++)
														{
															list2.Add(item);
														}
													}
												}
												else
												{
													for (int num6 = 0; num6 < 6; num6++)
													{
														list2.Add(item);
													}
												}
											}
											else
											{
												for (int num7 = 0; num7 < 8; num7++)
												{
													list2.Add(item);
												}
											}
										}
										else
										{
											for (int num8 = 0; num8 < 7; num8++)
											{
												list2.Add(item);
											}
										}
									}
									else
									{
										for (int num9 = 0; num9 < 14; num9++)
										{
											list2.Add(item);
										}
									}
								}
								else
								{
									for (int num10 = 0; num10 < 10; num10++)
									{
										list2.Add(item);
									}
								}
							}
							else
							{
								for (int num11 = 0; num11 < 10; num11++)
								{
									list2.Add(item);
								}
							}
						}
						else
						{
							for (int num12 = 0; num12 < 10; num12++)
							{
								list2.Add(item);
							}
						}
					}
					else
					{
						for (int num13 = 0; num13 < 10; num13++)
						{
							list2.Add(item);
						}
					}
				}
				int num14 = __instance.ItemsSpawnRate * 5;
				if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Harvest)
				{
					num14 = Mathf.RoundToInt((float)num14 * 1.75f);
				}
				for (int num15 = 0; num15 < num14; num15++)
				{
					Item prefab = CollectionsUtil.Grab<Item>(list2, 1).First();
					ItemSpawner val5 = (ItemSpawner)val.GetValue();
					if ((Object)(object)val5 != (Object)null)
					{
						ItemUtility.SpawnItem(prefab, ((Component)val5).transform.position, ((Component)val5).transform.rotation, ((SimulationBehaviour)__instance).Runner);
					}
				}
			}
			List<Accessory> list3 = GameManagerCustom.SpawnableAccessories.Where((Accessory o) => Plugin.CustomConfig.AccessoriesAvailability[ItemUtility.ItemToTranslateKey((Item)(object)o)]).ToList();
			if (list3.Count > 0)
			{
				List<Accessory> list4 = new List<Accessory>();
				foreach (Accessory item2 in list3)
				{
					Accessory accessory = item2;
					Accessory accessory2 = accessory;
					if (!(accessory2 is AccessoryBoots))
					{
						if (!(accessory2 is AccessoryHorn))
						{
							if (!(accessory2 is AccessoryRing))
							{
								if (!(accessory2 is AccessoryMagnifier))
								{
									if (!(accessory2 is AccessoryCrystalBall))
									{
										if (!(accessory2 is AccessoryBackpack))
										{
											if (accessory2 is AccessorySpellbook)
											{
												for (int num16 = 0; num16 < 2; num16++)
												{
													list4.Add(item2);
												}
											}
										}
										else
										{
											for (int num17 = 0; num17 < 1; num17++)
											{
												list4.Add(item2);
											}
										}
									}
									else
									{
										for (int num18 = 0; num18 < 1; num18++)
										{
											list4.Add(item2);
										}
									}
								}
								else
								{
									for (int num19 = 0; num19 < 1; num19++)
									{
										list4.Add(item2);
									}
								}
							}
							else
							{
								for (int num20 = 0; num20 < 1; num20++)
								{
									list4.Add(item2);
								}
							}
						}
						else
						{
							for (int num21 = 0; num21 < 1; num21++)
							{
								list4.Add(item2);
							}
						}
					}
					else
					{
						for (int num22 = 0; num22 < 1; num22++)
						{
							list4.Add(item2);
						}
					}
				}
				int num23 = BalancingValues.AccessoriesAmountToSpawn(__instance.ItemsSpawnRate, PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (Object)(object)o.Accessory == (Object)null));
				if (GameManagerCustom.Instance.EventsManager.CurrentEvent == EventsManager.EventType.Harvest)
				{
					num23 = Mathf.RoundToInt((float)num23 * 1.75f);
				}
				int num24 = 0;
				for (int num25 = 0; num25 < num23; num25++)
				{
					Item prefab2 = ((IEnumerable<Item>)CollectionsUtil.Grab<Accessory>(list4, 1)).First();
					ItemSpawner val6 = (ItemSpawner)val.GetValue();
					if ((Object)(object)val6 != (Object)null)
					{
						Item val7 = ItemUtility.SpawnItem(prefab2, ((Component)val6).transform.position, ((Component)val6).transform.rotation, ((SimulationBehaviour)__instance).Runner);
						if (val7 is AccessoryCrystalBall)
						{
							num24++;
						}
					}
				}
				if (num24 == 0 && !PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead) && (Object)(object)o.Item != (Object)null && o.Item is AccessoryCrystalBall)))
				{
					Plugin.Logger.LogInfo((object)"Adding extra crystal ball because no living player has one");
					Accessory accessory3 = list3.FirstOrDefault((Accessory o) => o is AccessoryCrystalBall);
					if ((Object)(object)accessory3 != (Object)null)
					{
						ItemSpawner val8 = (ItemSpawner)val.GetValue();
						if ((Object)(object)val8 != (Object)null)
						{
							ItemUtility.SpawnItem((Item)(object)accessory3, ((Component)val8).transform.position, ((Component)val8).transform.rotation, ((SimulationBehaviour)__instance).Runner);
						}
					}
				}
			}
			if (NetworkBool.op_Implicit(__instance.BattleRoyale))
			{
				int num26 = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => !NetworkBool.op_Implicit(p.IsDead))).Count();
				for (int num27 = 0; num27 < num26 * 10; num27++)
				{
					ItemSpawner val9 = (ItemSpawner)val.GetValue();
					if ((Object)(object)val9 != (Object)null)
					{
						((SimulationBehaviour)__instance).Runner.Spawn<BulletItem>(value2, (Vector3?)((Component)val9).transform.position, (Quaternion?)((Component)val9).transform.rotation, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true);
					}
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("SpawnRandomItems error: " + ex));
			return true;
		}
	}
}
