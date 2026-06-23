using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "Rpc_BroadcastFollowSound")]
internal class TeleportEffectOnTeleportSound
{
	private static void Postfix(NetworkString<_16> sound, Vector3 position)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		string text = ((object)sound/*cast due to constrained. prefix*/).ToString();
		if (text == "TELEPORT_START" || text == "TELEPORT_END")
		{
			GameObject val = Object.Instantiate<GameObject>(PlayerCustom.TeleportParticleSystemPrefab, position, Quaternion.identity);
			val.SetActive(true);
			SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
			MainModule main = val.GetComponent<ParticleSystem>().main;
			selfDestroyingObjectComponent.Init(((MainModule)(ref main)).duration);
		}
	}
}
