using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.Stats;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewItems.Accessories;

[NetworkBehaviourWeaved(6)]
public class AccessorySpellbook : Accessory
{
	public enum PossibleEffects
	{
		Invisibility,
		Swiftness,
		Petrified,
		Giant,
		FlatulencesWithMegaFart,
		Blind,
		Illusion,
		Asleep,
		Bomb,
		Burning,
		Detected,
		Teleportation,
		Resilience,
		ConfusionAndForcedRotation,
		Clairvoyance,
		Regeneration,
		Satiated,
		Camouflage,
		ForcedRotation
	}

	public class SpellbookEffectDetails
	{
		public int Ponderation;

		public float Duration;
	}

	private static Changed<AccessorySpellbook> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<AccessorySpellbook> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<AccessorySpellbook> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	public override string PrefabName => "LycansNewRoles.AccessorySpellbook";

	public override string DescriptionTranslateKey => "NALES_ACCESSORY_SPELLBOOK_DESCRIPTION";

	public override float CooldownAfterUse => 30f;

	public override string TinkererDescriptionTranslateKey => "NALES_ACCESSORY_SPELLBOOK_TINKERER";

	public override int TinkererPowerCooldown => 90;

	public override bool TinkererPowerRequiresPlayerTarget => false;

	protected override void ItemCollected()
	{
	}

	protected override bool CanUseItem()
	{
		return (Object)(object)GetTarget() != (Object)null;
	}

	protected override void ItemTriggered()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		PlayerController target = GetTarget();
		if ((Object)(object)target != (Object)null)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Item)this).Owner);
			Rpc_Use_Spellbook(((SimulationBehaviour)this).Runner, player.Index, target.Index);
		}
	}

	protected override void ItemCancelled()
	{
	}

	protected override void AnimationStarted()
	{
	}

	protected override void AnimationCancelled()
	{
	}

	protected override void AnimationEnded()
	{
	}

	[Rpc]
	public unsafe static void Rpc_Use_Spellbook(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 24;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Use_Spellbook(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = targetPlayerIndex;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 8;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			PlayerController playerController = player.PlayerController;
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayerIndex);
			PlayerController playerController2 = player2.PlayerController;
			if (player.Accessory is AccessorySpellbook accessorySpellbook)
			{
				TickTimer itemTimer = ((Item)accessorySpellbook).ItemTimer;
				if (!((TickTimer)(ref itemTimer)).IsRunning && !NetworkBool.op_Implicit(playerController2.IsDead))
				{
					if (runner.IsServer)
					{
						CastEffectOnPlayer(player2, playerController2, runner);
						if (player.Stats != null)
						{
							player.Stats.AddAction(new PlayerStats.PlayerAction
							{
								ActionType = "UseGadget",
								ActionName = TranslationManager.Instance.GetTranslation("NALES_ACCESSORY_SPELLBOOK"),
								ActionTarget = ((object)playerController2.PlayerData.Username/*cast due to constrained. prefix*/).ToString()
							}, ((Component)player.PlayerController).transform.position);
						}
					}
					((Item)accessorySpellbook).StartDelayTimer(30f);
				}
			}
			player2.FlashPlayer(Color.blue);
			player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackDirect1H01_R");
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Use_Spellbook error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Use_Spellbook(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Use_Spellbook_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Use_Spellbook(runner, playerIndex, targetPlayerIndex);
	}

	public static void CastEffectOnPlayer(PlayerCustom targetPlayerCustom, PlayerController targetPlayer, NetworkRunner runner)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<PossibleEffects, SpellbookEffectDetails> dictionary = (NetworkBool.op_Implicit(targetPlayer.IsWolf) ? BalancingValues.SpellbookPossibleEffectsAndDurationsOnWolves : BalancingValues.SpellbookPossibleEffectsAndDurationsOnHumans);
		List<PossibleEffects> list = new List<PossibleEffects>();
		foreach (KeyValuePair<PossibleEffects, SpellbookEffectDetails> item in dictionary)
		{
			for (int i = 0; i < item.Value.Ponderation; i++)
			{
				list.Add(item.Key);
			}
		}
		PossibleEffects possibleEffects = CollectionsUtil.Grab<PossibleEffects>(list, 1).FirstOrDefault();
		SpellbookEffectDetails spellbookEffectDetails = dictionary[possibleEffects];
		switch (possibleEffects)
		{
		case PossibleEffects.Invisibility:
		{
			Effect effect2 = EffectManager.GetEffects().First((Effect o) => o is InvisibilityEffect);
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, effect2, runner, 1f, spellbookEffectDetails.Duration);
			break;
		}
		case PossibleEffects.Swiftness:
		{
			Effect effect5 = EffectManager.GetEffects().First((Effect o) => o is SpeedEffect);
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, effect5, runner, 1f, spellbookEffectDetails.Duration);
			break;
		}
		case PossibleEffects.Petrified:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectPetrified", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Giant:
		{
			Effect effect3 = EffectManager.GetEffects().First((Effect o) => o is GiantEffect);
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, effect3, runner, 1f, spellbookEffectDetails.Duration);
			break;
		}
		case PossibleEffects.FlatulencesWithMegaFart:
		{
			PlayerCustom.Rpc_Effect_On_Player(runner, targetPlayer.Index, 4);
			Effect effect4 = EffectManager.GetEffects().First((Effect o) => o is FlatulenceEffect);
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, effect4, runner, 1f, spellbookEffectDetails.Duration);
			break;
		}
		case PossibleEffects.Blind:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectBlind", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Illusion:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectIllusion", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Asleep:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectAsleep", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Bomb:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectBomb", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Burning:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectBurning", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Detected:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectTracked", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Teleportation:
		{
			Effect effect = EffectManager.GetEffects().First((Effect o) => o is TeleportationEffect);
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, effect, runner, 1f, spellbookEffectDetails.Duration);
			break;
		}
		case PossibleEffects.Resilience:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectResilience", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.ConfusionAndForcedRotation:
			((Component)targetPlayerCustom.PlayerController).GetComponent<ForcedRotationComponent>().Init(new Vector3(0f, 1f, 0f), 3000f, 2000f);
			GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("PUNCH"), ((Component)targetPlayerCustom.PlayerController).transform.position, BalancingValues.WolfKillSoundRangeByMap(GameManager.Instance.MapID) * 0.5f, 1f);
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectConfused", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Clairvoyance:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectClairvoyance", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Regeneration:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectRecuperating", runner, 1f, spellbookEffectDetails.Duration);
			break;
		case PossibleEffects.Camouflage:
			PlayerCustom.ApplyEffectToPlayer(targetPlayer, "LycansNewRoles.EffectStealthing", runner, 1f, spellbookEffectDetails.Duration);
			break;
		}
		PlayerCustom.Rpc_Effect_On_Player(runner, targetPlayerCustom.Index, 1);
		GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)targetPlayer).transform.position, 15f, 0.5f);
	}

	public PlayerController GetTarget()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerController player = PlayerRegistry.GetPlayer(((Item)this).Owner);
			PlayerInteract component = ((Component)player).GetComponent<PlayerInteract>();
			Transform transform = ((Component)Traverse.Create((object)component).Field<Camera>("_camera").Value).transform;
			Vector3 position = transform.position;
			Vector3 forward = transform.forward;
			Ray val = default(Ray);
			((Ray)(ref val))._002Ector(position, forward);
			RaycastHit val2 = default(RaycastHit);
			if (Physics.Raycast(val, ref val2, 10f, LayerMask.op_Implicit(Traverse.Create((object)component).Field<LayerMask>("layerMask").Value)))
			{
				PlayerController componentInParent = ((Component)((RaycastHit)(ref val2)).collider).gameObject.GetComponentInParent<PlayerController>();
				if ((Object)(object)componentInParent != (Object)null && !NetworkBool.op_Implicit(componentInParent.IsDead))
				{
					return componentInParent;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GetTarget error: " + ex));
		}
		return null;
	}

	public override void CopyBackingFieldsToState(bool A_1)
	{
		((Item)this).CopyBackingFieldsToState(A_1);
	}

	public override void CopyStateToBackingFields()
	{
		((Item)this).CopyStateToBackingFields();
	}
}
