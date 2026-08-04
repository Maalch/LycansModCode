using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.PowerObjects;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Item), "DestroyItem")]
internal class DestroyItemPatch
{
	private static bool Prefix(Item __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Invalid comparison between Unknown and I4
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		PlayerController player = PlayerRegistry.GetPlayer(__instance.Owner);
		((NetworkBehaviour)__instance).CopyStateToBackingFields();
		Traverse.Create((object)__instance).Field("_Owner").SetValue((object)PlayerRef.None);
		((NetworkBehaviour)__instance).CopyBackingFieldsToState(true);
		if ((Object)(object)player != (Object)null && !(__instance is Accessory))
		{
			player.Item = null;
			if (((SimulationBehaviour)__instance).Runner.IsServer && (int)GameManager.LocalGameState == 2 && ((Object)(object)((Component)__instance).GetComponentInChildren<ItemCustom>() == (Object)null || ((Component)__instance).GetComponentInChildren<ItemCustom>().DropsInventorScrap) && !(__instance is LockItem) && !(__instance is KeyItem) && !(__instance is SleepingGasItem))
			{
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(player.Ref);
				if (player2.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Inventor)
				{
					player2.AddMaterials(30);
				}
				else
				{
					InventorScrap.CreateNewScrapForRandomInventor(((SimulationBehaviour)__instance).Runner, ((Component)player).transform.position);
				}
			}
		}
		((SimulationBehaviour)__instance).Runner.Despawn(((SimulationBehaviour)__instance).Object, false);
		return false;
	}
}
