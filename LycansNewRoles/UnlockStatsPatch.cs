using Fusion;
using HarmonyLib;
using LycansNewRoles.Stats;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Door), "Rpc_Unlock")]
public class UnlockStatsPatch
{
	private static void Prefix(Door __instance, PlayerRef actor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(actor);
		if (NetworkBool.op_Implicit(__instance.IsLocked))
		{
			ItemCustom.DisableInventorScrap(player.PlayerController.Item);
			player.PlayerController.Item.DestroyItem();
		}
		if (NetworkBool.op_Implicit(__instance.IsLocked) && player.Stats != null)
		{
			player.Stats.AddAction(new PlayerStats.PlayerAction
			{
				ActionType = "UseGadget",
				ActionName = TranslationManager.Instance.GetTranslation("NALES_GADGET_KEY")
			}, ((Component)player.PlayerController).transform.position);
		}
	}
}
