using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "OnMovementActionChanged")]
internal class OnMovementActionChangedPatch
{
	private static void Postfix(Changed<PlayerController> changed)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.Ref);
		if ((Object)(object)player.CurrentPet != (Object)null)
		{
			player.CurrentPet.UpdateVisibility();
		}
	}
}
