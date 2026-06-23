using Fusion;

namespace LycansNewRoles;

public static class ColorPicker
{
	public const int ColorsLength = 12;

	public const string FavoriteColorPlayerPref = "FavoriteColor";

	public static void NextColor()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		if (player.ColorIndex >= 11)
		{
			PlayerCustom.Rpc_Change_Color(((SimulationBehaviour)player).Runner, player.Index, 0);
		}
		else
		{
			PlayerCustom.Rpc_Change_Color(((SimulationBehaviour)player).Runner, player.Index, player.ColorIndex + 1);
		}
	}

	public static void PreviousColor()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		if (player.ColorIndex == 0)
		{
			PlayerCustom.Rpc_Change_Color(((SimulationBehaviour)player).Runner, player.Index, 11);
		}
		else
		{
			PlayerCustom.Rpc_Change_Color(((SimulationBehaviour)player).Runner, player.Index, player.ColorIndex - 1);
		}
	}
}
