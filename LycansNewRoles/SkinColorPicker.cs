using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public static class SkinColorPicker
{
	public const string FavoriteSkinColorPlayerPref = "FavoriteSkinColor";

	public static int SkinColorsLength => Plugin.Skins[PlayerCustom.Local.SkinIndex].SkinTextures.Count;

	public static void NextSkinColor()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		if (player.SkinColorIndex >= SkinColorsLength - 1)
		{
			PlayerCustom.Rpc_Change_Skin_Color(((SimulationBehaviour)player).Runner, player.Index, 0);
		}
		else
		{
			PlayerCustom.Rpc_Change_Skin_Color(((SimulationBehaviour)player).Runner, player.Index, player.SkinColorIndex + 1);
		}
	}

	public static void PreviousSkinColor()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.Ref);
		if (player.SkinColorIndex == 0)
		{
			PlayerCustom.Rpc_Change_Skin_Color(((SimulationBehaviour)player).Runner, player.Index, Mathf.Max(0, SkinColorsLength - 1));
		}
		else
		{
			PlayerCustom.Rpc_Change_Skin_Color(((SimulationBehaviour)player).Runner, player.Index, player.SkinColorIndex - 1);
		}
	}
}
