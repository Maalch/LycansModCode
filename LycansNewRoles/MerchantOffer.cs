using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewItems;

namespace LycansNewRoles;

public class MerchantOffer
{
	public enum MerchantOfferType
	{
		Scroll,
		OtherGadget,
		Potion,
		ImmediateEffect,
		PriestProtection,
		Heal
	}

	public int Index;

	public int Price;

	public MerchantOfferType Type;

	public int? TypeIndex;

	public static List<MerchantOffer> GenerateOffers(PlayerCustom merchant)
	{
		List<MerchantOffer> list = new List<MerchantOffer>();
		while (list.Count < 4)
		{
			MerchantOffer offer = GenerateOffer(merchant);
			if (offer != null)
			{
				offer.Index = list.Count;
				if (!list.Any((MerchantOffer o) => o.Type == offer.Type && o.TypeIndex == offer.TypeIndex))
				{
					list.Add(offer);
				}
			}
		}
		return list;
	}

	private static MerchantOffer GenerateOffer(PlayerCustom merchant)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Invalid comparison between Unknown and I4
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		MerchantOffer merchantOffer = new MerchantOffer();
		List<MerchantOfferType> list = new List<MerchantOfferType>();
		foreach (MerchantOfferType item in Enum.GetValues(typeof(MerchantOfferType)).Cast<MerchantOfferType>())
		{
			int merchantOfferTypePonderation = BalancingValues.GetMerchantOfferTypePonderation(item);
			for (int i = 0; i < merchantOfferTypePonderation; i++)
			{
				list.Add(item);
			}
		}
		if ((int)merchant.PlayerController.Role == 1)
		{
			list.RemoveAll((MerchantOfferType o) => o == MerchantOfferType.PriestProtection);
		}
		merchantOffer.Type = CollectionsUtil.Grab<MerchantOfferType>(list, 1).First();
		switch (merchantOffer.Type)
		{
		case MerchantOfferType.Scroll:
		{
			Effect randomEffectWithPonderation = MagicScrollItem.GetRandomEffectWithPonderation();
			merchantOffer.TypeIndex = EffectManager.GetEffectIndex(randomEffectWithPonderation);
			merchantOffer.Price = BalancingValues.GetMerchantCostForScrollEffect(randomEffectWithPonderation);
			break;
		}
		case MerchantOfferType.OtherGadget:
		{
			Item[] value = Traverse.Create((object)GameManager.Instance).Field<Item[]>("spawnableItemPrefabs").Value;
			List<Item> list2 = value.Where((Item o) => Plugin.CustomConfig.GadgetsAvailability[ItemUtility.ItemToTranslateKey(o)] && !(o is MagicScrollItem)).ToList();
			if (list2.Count == 0)
			{
				return null;
			}
			Item val2 = CollectionsUtil.Grab<Item>(list2.Where((Item o) => o is LockItem || o is TrapItem || o is SmokeItem || o is SpyglassItem || o is PhasingDiamondItem || o is GrenadeItem || o is SleepingGasItem || o is MolotovItem || o is RadarItem).ToList(), 1).First();
			if (val2 is LockItem)
			{
				merchantOffer.TypeIndex = 0;
				merchantOffer.Price = 20;
			}
			if (val2 is TrapItem)
			{
				merchantOffer.TypeIndex = 1;
				merchantOffer.Price = (NetworkBool.op_Implicit(Plugin.CustomConfig.TrapsModified) ? 10 : 20);
			}
			if (val2 is SmokeItem)
			{
				merchantOffer.TypeIndex = 2;
				merchantOffer.Price = (NetworkBool.op_Implicit(Plugin.CustomConfig.SmokeBoosted) ? 15 : 5);
			}
			if (val2 is SpyglassItem)
			{
				merchantOffer.TypeIndex = 3;
				merchantOffer.Price = 5;
			}
			if (val2 is PhasingDiamondItem)
			{
				merchantOffer.TypeIndex = 4;
				merchantOffer.Price = 20;
			}
			if (val2 is GrenadeItem)
			{
				merchantOffer.TypeIndex = 5;
				merchantOffer.Price = 15;
			}
			if (val2 is SleepingGasItem)
			{
				merchantOffer.TypeIndex = 6;
				merchantOffer.Price = 15;
			}
			if (val2 is MolotovItem)
			{
				merchantOffer.TypeIndex = 7;
				merchantOffer.Price = 15;
			}
			if (val2 is RadarItem)
			{
				merchantOffer.TypeIndex = 8;
				merchantOffer.Price = 15;
			}
			break;
		}
		case MerchantOfferType.Potion:
		{
			Potion value2 = Traverse.Create((object)GameManager.Instance).Field<Potion>("potionPrefab").Value;
			List<Effect> value3 = Traverse.Create((object)GameManager.Instance).Field<List<Effect>>("_potionEffects").Value;
			List<Effect> list3 = value3.Where((Effect o) => BalancingValues.GetMerchantCostForPotionEffect(o).HasValue).ToList();
			if (list3.Count == 0)
			{
				return null;
			}
			Effect effect = CollectionsUtil.Grab<Effect>(list3, 1).First();
			merchantOffer.TypeIndex = BalancingValues.GetModifiedEffectData(effect).RealIndex;
			merchantOffer.Price = BalancingValues.GetMerchantCostForPotionEffect(effect).Value;
			break;
		}
		case MerchantOfferType.ImmediateEffect:
		{
			Effect val = CollectionsUtil.Grab<Effect>((from o in EffectManager.GetEffects()
				where BalancingValues.GetMerchantCostForImmediateEffect(o).HasValue
				select o).ToList(), 1).First();
			merchantOffer.TypeIndex = EffectManager.GetEffectIndex(val);
			merchantOffer.Price = BalancingValues.GetMerchantCostForImmediateEffect(val).Value;
			break;
		}
		case MerchantOfferType.PriestProtection:
			merchantOffer.TypeIndex = null;
			merchantOffer.Price = 20;
			break;
		case MerchantOfferType.Heal:
			merchantOffer.TypeIndex = null;
			merchantOffer.Price = 20;
			break;
		}
		return merchantOffer;
	}
}
