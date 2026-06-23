using System;
using Fusion;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using UnityEngine;

namespace LycansNewRoles;

public static class ItemUtility
{
	public static string ItemToTranslateKey(Item item)
	{
		if (!(item is TrapItem))
		{
			if (!(item is SmokeItem))
			{
				if (!(item is SpyglassItem))
				{
					if (!(item is LockItem))
					{
						if (!(item is KeyItem))
						{
							if (!(item is BulletItem))
							{
								if (!(item is MagicScrollItem))
								{
									if (!(item is PhasingDiamondItem))
									{
										if (!(item is GrenadeItem))
										{
											if (!(item is SleepingGasItem))
											{
												if (!(item is MolotovItem))
												{
													if (!(item is RadarItem))
													{
														if (!(item is Potion))
														{
															if (!(item is AccessoryBoots))
															{
																if (!(item is AccessoryHorn))
																{
																	if (!(item is AccessoryRing))
																	{
																		if (!(item is AccessoryMagnifier))
																		{
																			if (!(item is AccessoryCrystalBall))
																			{
																				if (!(item is AccessoryBackpack))
																				{
																					if (item is AccessorySpellbook)
																					{
																						return "NALES_ACCESSORY_SPELLBOOK";
																					}
																					Plugin.Logger.LogError((object)("Couldn't find item: " + ((object)item).GetType()));
																					throw new NotImplementedException();
																				}
																				return "NALES_ACCESSORY_BACKPACK";
																			}
																			return "NALES_ACCESSORY_CRYSTAL_BALL";
																		}
																		return "NALES_ACCESSORY_MAGNIFIER";
																	}
																	return "NALES_ACCESSORY_RING";
																}
																return "NALES_ACCESSORY_HORN";
															}
															return "NALES_ACCESSORY_BOOTS";
														}
														return "NALES_POTION";
													}
													return "NALES_GADGET_RADAR";
												}
												return "NALES_GADGET_MOLOTOV";
											}
											return "NALES_GADGET_SLEEPING_GAS";
										}
										return "NALES_GADGET_GRENADE";
									}
									return "NALES_GADGET_DIAMOND";
								}
								return "NALES_GADGET_SCROLL";
							}
							return "NALES_BULLET";
						}
						return "NALES_GADGET_KEY";
					}
					return "NALES_GADGET_LOCK";
				}
				return "NALES_GADGET_SPYGLASS";
			}
			return "NALES_GADGET_SMOKE";
		}
		return "NALES_GADGET_TRAP";
	}

	public static Item SpawnItem(Item prefab, Vector3 position, Quaternion rotation, NetworkRunner runner)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (prefab is CustomItem customItem)
		{
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject(customItem.PrefabName);
			NetworkObject val = runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)rotation, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			return ((Component)val).GetComponent<Item>();
		}
		return runner.Spawn<Item>(prefab, (Vector3?)position, (Quaternion?)rotation, (PlayerRef?)null, (OnBeforeSpawned)null, (NetworkObjectPredictionKey?)null, true);
	}
}
