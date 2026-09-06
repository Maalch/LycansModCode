using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewPrimaryRoles;
using LycansNewRoles.PowerObjects;
using LycansNewRoles.Stats;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerController), "Rpc_TransformBack")]
internal class TransformChangesPatch
{
	private static bool Prefix(PlayerController __instance)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Invalid comparison between Unknown and I4
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(__instance.Ref);
			if (NetworkBool.op_Implicit(player.ResurrectedByNecromancer))
			{
				return false;
			}
			if (player.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Possessor)
			{
				PlayerRef primaryRoleTargetRef = player.PrimaryRoleTargetRef;
				if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone && NetworkBool.op_Implicit(PlayerCustomRegistry.GetPlayer(player.PrimaryRoleTargetRef).Possessed))
				{
					return false;
				}
			}
			if ((int)__instance.Role != 1)
			{
				return false;
			}
			if ((Object)(object)__instance == (Object)(object)PlayerController.Local && ((SimulationBehaviour)__instance).Object.HasInputAuthority)
			{
				ShowRoleDescriptionPatch.NeedsUpdate = true;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("NewPrimaryRoleShapeshifterTransformBackChangesPatch error: " + ex));
			return true;
		}
	}

	private static void Postfix(PlayerController __instance)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)__instance).Object.HasStateAuthority)
		{
			return;
		}
		PlayerCustom playerCustom = PlayerCustomRegistry.GetPlayer(__instance.Ref);
		if (playerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Sneak)
		{
			__instance.PlayerEffectManager.ClearEffects();
			PlayerCustom.ApplyEffectToPlayer(__instance, "LycansNewRoles.EffectSneaky", ((SimulationBehaviour)__instance).Runner);
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.GameObjectDeceiverIllusion");
			NetworkObject val = ((SimulationBehaviour)__instance).Runner.Spawn(networkObject, (Vector3?)((Component)__instance).transform.position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
			{
				//IL_0012: Unknown result type (might be due to invalid IL or missing references)
				((Component)no).transform.position = ((Component)__instance).transform.position;
			}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			((Component)val).GetComponent<DeceiverIllusionComponent>().SetCreatorRef(__instance.Ref);
			DiscipleAnchor discipleAnchor = Object.FindObjectsOfType<DiscipleAnchor>().FirstOrDefault((DiscipleAnchor o) => o.CreatorRef == playerCustom.Ref);
			__instance.CharacterMovementHandler.TeleportData = new NetworkTeleportData(((Component)discipleAnchor).transform.position, ((Component)discipleAnchor).transform.rotation, false);
			__instance.IsClimbing = NetworkBool.op_Implicit(false);
		}
		if (!NetworkBool.op_Implicit(__instance.IsWolf))
		{
			string text = DateTime.UtcNow.ToString();
			NetworkString<_32> username = __instance.PlayerData.Username;
			LycansUtility.AddLogOnlyForMe("Adding detransformation from regular wolf at date: " + text + ", player: " + ((object)username/*cast due to constrained. prefix*/).ToString());
			GameManagerCustom.Instance.AddDetransformation();
			Effect val2 = __instance.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is DetectedEffect);
			if ((Object)(object)val2 != (Object)null)
			{
				__instance.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val2).Object.Id);
			}
			playerCustom.Stats.AddAction(new PlayerStats.PlayerAction
			{
				ActionType = "Untransform"
			}, ((Component)playerCustom.PlayerController).transform.position);
		}
	}
}
