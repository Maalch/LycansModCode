using System;
using HarmonyLib;
using Managers;

namespace LycansNewRoles;

[HarmonyPatch(typeof(InputManager), "Init")]
internal class MoreKeysInputManagerInitPatch
{
	private static void Prefix(InputManager __instance)
	{
		try
		{
			InputManagerExtra.CreateAction(__instance, "SECONDARYROLEPOWER", "<Keyboard>/q", "<Gamepad>/buttonSouth", "ed232440-9c66-4a8d-8de9-62e293701b62", "9ed81a8b-b408-4657-a43b-82bca504f722");
			InputManagerExtra.CreateAction(__instance, "SHOWMINIMAP", "<Keyboard>/TAB", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b63", "9ed81a8b-b408-4657-a43b-82bca504f723");
			InputManagerExtra.CreateAction(__instance, "ITEMSECONDARY", "<Keyboard>/f", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b72", "9ed81a8b-b408-4657-a43b-82bca504f732");
			InputManagerExtra.CreateAction(__instance, "MAYORACTION", "<Keyboard>/x", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b74", "9ed81a8b-b408-4657-a43b-82bca504f734");
			InputManagerExtra.CreateAction(__instance, "ACCESSORYACTION", "<Keyboard>/c", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b75", "9ed81a8b-b408-4657-a43b-82bca504f735");
			InputManagerExtra.CreateAction(__instance, "EMOTE1", "<Keyboard>/1", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b64", "9ed81a8b-b408-4657-a43b-82bca504f724");
			InputManagerExtra.CreateAction(__instance, "EMOTE2", "<Keyboard>/2", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b65", "9ed81a8b-b408-4657-a43b-82bca504f725");
			InputManagerExtra.CreateAction(__instance, "EMOTE3", "<Keyboard>/3", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b66", "9ed81a8b-b408-4657-a43b-82bca504f726");
			InputManagerExtra.CreateAction(__instance, "EMOTE4", "<Keyboard>/4", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b67", "9ed81a8b-b408-4657-a43b-82bca504f727");
			InputManagerExtra.CreateAction(__instance, "EMOTE5", "<Keyboard>/5", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b68", "9ed81a8b-b408-4657-a43b-82bca504f728");
			InputManagerExtra.CreateAction(__instance, "EMOTE6", "<Keyboard>/6", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b69", "9ed81a8b-b408-4657-a43b-82bca504f729");
			InputManagerExtra.CreateAction(__instance, "EMOTE7", "<Keyboard>/7", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b70", "9ed81a8b-b408-4657-a43b-82bca504f730");
			InputManagerExtra.CreateAction(__instance, "EMOTE8", "<Keyboard>/8", "<Gamepad>/select", "ed232440-9c66-4a8d-8de9-62e293701b71", "9ed81a8b-b408-4657-a43b-82bca504f731");
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MoreKeysInputManagerInitPatch error: " + ex));
		}
	}
}
