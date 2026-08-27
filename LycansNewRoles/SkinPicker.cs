using Fusion;

namespace LycansNewRoles;

public static class SkinPicker
{
	public static int SkinsLength = Plugin.Skins.Count;

	public const string FavoriteSkinPlayerPref = "FavoriteSkin";

	public static void NextSkin()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		if (player.SkinIndex >= SkinsLength - 1)
		{
			PlayerCustom.Rpc_Change_Skin(((SimulationBehaviour)player).Runner, player.Index, 0);
		}
		else
		{
			PlayerCustom.Rpc_Change_Skin(((SimulationBehaviour)player).Runner, player.Index, player.SkinIndex + 1);
		}
	}

	public static void PreviousSkin()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		if (player.SkinIndex == 0)
		{
			PlayerCustom.Rpc_Change_Skin(((SimulationBehaviour)player).Runner, player.Index, SkinsLength - 1);
		}
		else
		{
			PlayerCustom.Rpc_Change_Skin(((SimulationBehaviour)player).Runner, player.Index, player.SkinIndex - 1);
		}
	}
}
