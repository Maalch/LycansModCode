using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Spawned")]
internal class AddPlayerAddCustomPlayerPatch
{
	private static void Postfix(PlayerController __instance)
	{
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).HasStateAuthority)
			{
				string[] obj = new string[8]
				{
					"Player spawned: ref ",
					((object)__instance.Ref/*cast due to constrained. prefix*/).ToString(),
					", index ",
					__instance.Index.ToString(),
					", username: ",
					null,
					null,
					null
				};
				NetworkString<_32> username = __instance.PlayerData.Username;
				obj[5] = ((object)username/*cast due to constrained. prefix*/).ToString();
				obj[6] = ", id: ";
				NetworkString<_64> iD = __instance.PlayerData.ID;
				obj[7] = ((object)iD/*cast due to constrained. prefix*/).ToString();
				LycansUtility.DebugLog(string.Concat(obj));
				NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.PlayerCustom");
				NetworkObject val = ((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)new Vector3(((Component)__instance).transform.position.x, ((Component)__instance).transform.position.y, ((Component)__instance).transform.position.z), (Quaternion?)null, (PlayerRef?)((SimulationBehaviour)__instance).Object.InputAuthority, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).transform.parent = ((Component)__instance).transform;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				((Component)val).transform.parent = ((Component)__instance).transform;
				PlayerCustomRegistry.Server_Add(((SimulationBehaviour)__instance).Runner, ((SimulationBehaviour)__instance).Object.InputAuthority, ((Component)val).GetComponent<PlayerCustom>());
			}
			((Component)__instance).gameObject.AddComponent<PlayerFootstepsComponent>();
			((Component)__instance).gameObject.AddComponent<PlayerPhasingComponent>();
			CharacterController component = ((Component)__instance).GetComponent<CharacterController>();
			component.center = new Vector3(0f, 0.8f, 0f);
			component.height = 1.5f;
			foreach (GameObject newHat in Plugin.NewHats)
			{
				GameObject val2 = Object.Instantiate<GameObject>(newHat, __instance.hats.transform);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("AddPlayerAddCustomPlayerPatch error: " + ex));
		}
	}
}
