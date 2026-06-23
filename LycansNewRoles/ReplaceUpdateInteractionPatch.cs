using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Managers;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "UpdateInteraction", new Type[]
{
	typeof(string),
	typeof(Color),
	typeof(List<InputActionName>),
	typeof(object[])
})]
internal class ReplaceUpdateInteractionPatch
{
	private unsafe static bool Prefix(string key, Color color, List<InputActionName> actions, params object[] items)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			InteractionsManager.UpdateInteraction(key, color, actions.Select((InputActionName o) => ((object)(*(InputActionName*)(&o))/*cast due to constrained. prefix*/).ToString()).ToList(), items);
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("ReplaceUpdateInteractionPatch error: " + ex));
			return true;
		}
	}
}
