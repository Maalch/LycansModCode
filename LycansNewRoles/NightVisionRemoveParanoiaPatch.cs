using System;
using System.Linq;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerEffectsManager), "NightVisionChanged")]
internal class NightVisionRemoveParanoiaPatch
{
	private static void Postfix(Changed<PlayerEffectsManager> changed)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerEffectsManager behaviour = changed.Behaviour;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(Traverse.Create((object)behaviour).Field<PlayerController>("_playerController").Value.Ref);
			behaviour.Paranoia = NetworkBool.op_Implicit(behaviour.GetActiveEffects().Any((Effect o) => o is ParanoiaEffect) && !NetworkBool.op_Implicit(behaviour.NightVision));
			if (!player.IsCurrentlyPlayedOrObserved)
			{
				return;
			}
			foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
			{
				allPlayer.UpdateVisibility();
				allPlayer.UpdateIllusion();
			}
			ItemCustom.UpdateAllItems();
			MagicianIllusion.UpdateVisibilityForAllMagicianIllusions();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NightVisionChanged error: " + ex));
		}
	}
}
