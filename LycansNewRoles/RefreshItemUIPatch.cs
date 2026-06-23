using HarmonyLib;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "RefreshItemUI")]
internal class RefreshItemUIPatch
{
	private static void Postfix(PlayerController __instance)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if ((int)GameManager.LocalGameState == 0 || !((Object)(object)PlayerController.Local != (Object)null) || !((Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer == (Object)(object)__instance))
		{
			return;
		}
		if ((Object)(object)__instance.Item != (Object)null)
		{
			string itemDetails = GetItemDetails(__instance.Item);
			if (itemDetails != null)
			{
				UIManager.ItemDetailsPanel.Show(itemDetails);
			}
			else
			{
				UIManager.ItemDetailsPanel.Hide();
			}
		}
		else
		{
			UIManager.ItemDetailsPanel.Hide();
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
		if ((Object)(object)player.PlacedSleepingGas != (Object)null && !player.PlacedSleepingGas.Detonated)
		{
			UIManager.ItemSecondaryPanel.Show(LycansUtility.GetInputDisplayCustom(InputManagerExtra.Instance.Actions["ITEMSECONDARY"]));
		}
		else
		{
			UIManager.ItemSecondaryPanel.Hide();
		}
		UIManager.AccessoryPanel.UpdateAccessory(player);
		if (player.Accessory is AccessoryBackpack accessoryBackpack)
		{
			((Component)UIManager.SecondItemPanel).gameObject.SetActive(true);
			if ((Object)(object)accessoryBackpack.ItemInside != (Object)null)
			{
				((Behaviour)UIManager.SecondItemPanel.Image).enabled = true;
				UIManager.SecondItemPanel.Image.sprite = accessoryBackpack.ItemInside.Sprite();
				if (accessoryBackpack.ItemInside.ItemQuantity > 1)
				{
					((TMP_Text)UIManager.SecondItemPanel.Quantity).text = "x" + accessoryBackpack.ItemInside.ItemQuantity;
					((Behaviour)UIManager.SecondItemPanel.Quantity).enabled = true;
				}
				else
				{
					((Behaviour)UIManager.SecondItemPanel.Quantity).enabled = false;
				}
				string itemDetails2 = GetItemDetails(accessoryBackpack.ItemInside);
				if (itemDetails2 != null)
				{
					((Behaviour)UIManager.SecondItemPanel.DetailsText).enabled = true;
					((TMP_Text)UIManager.SecondItemPanel.DetailsText).text = itemDetails2;
				}
				else
				{
					((Behaviour)UIManager.SecondItemPanel.DetailsText).enabled = false;
				}
			}
			else
			{
				((Behaviour)UIManager.SecondItemPanel.Image).enabled = false;
				((Behaviour)UIManager.SecondItemPanel.Quantity).enabled = false;
				((Behaviour)UIManager.SecondItemPanel.DetailsText).enabled = false;
			}
		}
		else
		{
			((Component)UIManager.SecondItemPanel).gameObject.SetActive(false);
		}
	}

	public static string GetItemDetails(Item item)
	{
		if (item is MagicScrollItem magicScrollItem && (Object)(object)magicScrollItem.Effect != (Object)null)
		{
			return TranslationManager.Instance.GetTranslation(magicScrollItem.Effect.GetTranslateKey());
		}
		if (item is SleepingGasItem sleepingGasItem)
		{
			switch (sleepingGasItem.Type)
			{
			case 0:
				return TranslationManager.Instance.GetTranslation("NALES_ITEM_GAS_SLEEPING");
			case 1:
				return TranslationManager.Instance.GetTranslation("NALES_ITEM_GAS_DEAFENER");
			case 2:
				return TranslationManager.Instance.GetTranslation("NALES_ITEM_GAS_POISON");
			}
		}
		else
		{
			Potion val = (Potion)(object)((item is Potion) ? item : null);
			if (val != null)
			{
				Color[] value = Traverse.Create(typeof(Potion)).Field<Color[]>("PotionColors").Value;
				if (val.ColorIndex == value.Length - 1)
				{
					Effect effect = EffectManager.GetEffect(val.EffectIndex);
					return TranslationManager.Instance.GetTranslation(effect.GetTranslateKey());
				}
			}
		}
		return null;
	}
}
