using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnZoomingChanged")]
public class ZoomingPatch
{
	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom playerCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
		if (!playerCustom.IsCurrentlyPlayedOrObserved)
		{
			return;
		}
		foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != playerCustom.Ref))
		{
			item.UpdateVisibility();
		}
	}
}
