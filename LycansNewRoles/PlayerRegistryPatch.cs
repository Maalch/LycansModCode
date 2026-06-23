using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerRegistry), "GetAvailable")]
internal class PlayerRegistryPatch
{
	private static bool Prefix(PlayerRegistry __instance, ref bool __result, out byte index)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			NetworkDictionary<PlayerRef, PlayerController> value = Traverse.Create((object)__instance).Property<NetworkDictionary<PlayerRef, PlayerController>>("ObjectByRef", (object[])null).Value;
			if (value.Count == 0)
			{
				index = 0;
				__result = true;
				return false;
			}
			if (value.Count == 15)
			{
				index = 0;
				__result = false;
				return false;
			}
			byte[] array = ((IEnumerable<KeyValuePair<PlayerRef, PlayerController>>)(object)value).OrderBy(delegate(KeyValuePair<PlayerRef, PlayerController> kvp)
			{
				KeyValuePair<PlayerRef, PlayerController> keyValuePair = kvp;
				return keyValuePair.Value.Index;
			}).Select(delegate(KeyValuePair<PlayerRef, PlayerController> kvp)
			{
				KeyValuePair<PlayerRef, PlayerController> keyValuePair = kvp;
				return keyValuePair.Value.Index;
			}).ToArray();
			for (int num = 0; num < array.Length - 1; num++)
			{
				if (array[num + 1] > array[num] + 1)
				{
					index = (byte)(array[num] + 1);
					__result = true;
					return false;
				}
			}
			index = (byte)(array[^1] + 1);
			__result = true;
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PlayerRegistryPatch error: " + ex));
			index = 0;
			return true;
		}
	}
}
