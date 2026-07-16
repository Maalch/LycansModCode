using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerEffectsManager), "CheckTeleportation")]
internal class RemovePlayersFromPlayerPositions
{
	private static bool Prefix(PlayerEffectsManager __instance)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		PlayerController ownPlayerController = Traverse.Create((object)__instance).Field<PlayerController>("_playerController").Value;
		if (((SimulationBehaviour)__instance).HasStateAuthority)
		{
			TickTimer teleportationTimer = __instance.TeleportationTimer;
			if (((TickTimer)(ref teleportationTimer)).Expired(((SimulationBehaviour)__instance).Runner) && !NetworkBool.op_Implicit(ownPlayerController.IsDead))
			{
				float num;
				if (((IEnumerable<KeyValuePair<PlayerRef, Vector3>>)(object)__instance.PlayerPositions).Any())
				{
					Dictionary<PlayerController, Vector3> dictionary = new Dictionary<PlayerController, Vector3>();
					Enumerator<PlayerRef, Vector3> enumerator = __instance.PlayerPositions.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<PlayerRef, Vector3> current = enumerator.Current;
							PlayerController player = PlayerRegistry.GetPlayer(current.Key);
							if ((Object)(object)player != (Object)null)
							{
								Vector3 position = ((Component)player).transform.position;
								Vector3 oldPosition = current.Value;
								if (!PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController p) => Vector3.Distance(((Component)p).transform.position, oldPosition) < 0.5f)) && Vector3.Distance(position, oldPosition) >= 0.5f)
								{
									dictionary.Add(player, oldPosition);
								}
							}
						}
					}
					finally
					{
						((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
					}
					if (dictionary.Any())
					{
						PlayerController val = CollectionsUtil.Grab<PlayerController>(dictionary.Keys.ToList(), 1).First();
						Vector3 val2 = dictionary[val];
						Quaternion val3 = ((Component)ownPlayerController).transform.rotation;
						if ((Object)(object)val != (Object)null && (Object)(object)((Component)val).transform != (Object)null)
						{
							Vector3 val4 = ((Component)val).transform.position - val2;
							val4.y = 0f;
							val3 = Quaternion.LookRotation(val4);
						}
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("TELEPORT_START"), ((Component)ownPlayerController).transform.position, 20f, 1f);
						GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)__instance).Runner, NetworkString<_16>.op_Implicit("TELEPORT_END"), val2, 20f, 1f);
						ownPlayerController.CharacterMovementHandler.TeleportData = new NetworkTeleportData(val2, val3, true);
						ownPlayerController.IsClimbing = NetworkBool.op_Implicit(false);
						num = Random.Range(5f, 20f);
					}
					else
					{
						num = 0.5f;
					}
					__instance.PlayerPositions.Clear();
				}
				else
				{
					IEnumerable<PlayerCustom> enumerable = PlayerCustomRegistry.Where((PlayerCustom o) => o.Ref != ownPlayerController.Ref && !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.PlayerController.IsClimbing) && !o.IsOutOfTheWorld && Vector3.Distance(((Component)ownPlayerController).transform.position, ((Component)o.PlayerController).transform.position) >= 35f);
					foreach (PlayerCustom item in enumerable)
					{
						__instance.PlayerPositions.Add(item.Ref, ((Component)item.PlayerController).transform.position);
					}
					num = Random.Range(0.1f, 1f);
				}
				__instance.TeleportationTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)__instance).Runner, num);
			}
		}
		return false;
	}
}
