using System.Linq;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "UpdateTimer")]
internal class MeetingTimerPositionPatch
{
	private static void Postfix(GameUI __instance)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		TextMeshProUGUI value = Traverse.Create((object)__instance).Field<TextMeshProUGUI>("timer").Value;
		PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
		if (povPlayer.PlayerEffectManager.GetActiveEffects().Any())
		{
			((TMP_Text)value).transform.localPosition = new Vector3(0f, 480f, 0f);
		}
		else
		{
			((TMP_Text)value).transform.localPosition = new Vector3(0f, 540f, 0f);
		}
	}
}
