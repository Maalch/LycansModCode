using System.Collections.Generic;
using Fusion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "UpdateDeadPlayer")]
internal class KidnapperUpdateIcon
{
	private static bool Prefix(GameUI __instance, PlayerRef playerRef)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<PlayerRef, PlayerDisplay> value = Traverse.Create((object)__instance).Field<Dictionary<PlayerRef, PlayerDisplay>>("_playerDisplays").Value;
		if (value.TryGetValue(playerRef, out var value2))
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerRef);
			Image value3 = Traverse.Create((object)value2).Field<Image>("deadIcon").Value;
			if ((Object)(object)UIManager.DefaultDeadPlayerIcon == (Object)null)
			{
				UIManager.DefaultDeadPlayerIcon = value3.sprite;
			}
			if (NetworkBool.op_Implicit(player.Kidnapped) && !NetworkBool.op_Implicit(player.PlayerController.IsDead))
			{
				((Behaviour)value3).enabled = true;
				((Graphic)value3).color = UIManager.KidnappedPlayerColor;
				value3.sprite = UIManager.KidnappedPlayerIcon;
				return false;
			}
			if (NetworkBool.op_Implicit(player.DiedFromNotBeingSaved))
			{
				((Graphic)value3).color = Color.red;
			}
			else
			{
				((Graphic)value3).color = Color.white;
			}
		}
		return true;
	}
}
