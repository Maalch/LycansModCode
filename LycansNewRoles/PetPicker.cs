using Fusion;

namespace LycansNewRoles;

public static class PetPicker
{
	public static int PetsLength = Plugin.PetNames.Count + 1;

	public const string FavoritePetPlayerPref = "FavoritePet";

	public static void NextPet()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		if (player.PetIndex >= PetsLength - 1)
		{
			PlayerCustom.Rpc_Change_Pet(((SimulationBehaviour)player).Runner, player.Index, 0);
		}
		else
		{
			PlayerCustom.Rpc_Change_Pet(((SimulationBehaviour)player).Runner, player.Index, player.PetIndex + 1);
		}
	}

	public static void PreviousPet()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		if (player.PetIndex == 0)
		{
			PlayerCustom.Rpc_Change_Pet(((SimulationBehaviour)player).Runner, player.Index, PetsLength - 1);
		}
		else
		{
			PlayerCustom.Rpc_Change_Pet(((SimulationBehaviour)player).Runner, player.Index, player.PetIndex - 1);
		}
	}
}
