using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(BulletItem), "ItemTriggered")]
public class BulletUsePatch
{
	private static bool Prefix(BulletItem __instance)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)__instance).Owner);
		switch (player.PrimaryRolePower)
		{
		case PlayerCustom.PlayerPrimaryRolePower.Alchemist:
			player.AddMaterials(100);
			return false;
		case PlayerCustom.PlayerPrimaryRolePower.Spotter:
			player.AddMaterials(150);
			return false;
		case PlayerCustom.PlayerPrimaryRolePower.Purifier:
			player.AddMaterials(150);
			return false;
		default:
			return true;
		}
	}
}
