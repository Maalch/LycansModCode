using Fusion;
using HarmonyLib;
using LycansNewRoles.NewItems.Accessories;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(Item), "DestroyItem")]
internal class DestroyItemPatch
{
	private static bool Prefix(Item __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		PlayerController player = PlayerRegistry.GetPlayer(__instance.Owner);
		((NetworkBehaviour)__instance).CopyStateToBackingFields();
		Traverse.Create((object)__instance).Field("_Owner").SetValue((object)PlayerRef.None);
		((NetworkBehaviour)__instance).CopyBackingFieldsToState(true);
		if ((Object)(object)player != (Object)null && !(__instance is Accessory))
		{
			player.Item = null;
		}
		((SimulationBehaviour)__instance).Runner.Despawn(((SimulationBehaviour)__instance).Object, false);
		return false;
	}
}
