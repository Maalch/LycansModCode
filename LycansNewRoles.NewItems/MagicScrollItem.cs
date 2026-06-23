using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using LycansNewRoles.Stats;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(6)]
public class MagicScrollItem : CustomItem
{
	public Effect Effect;

	private static Changed<MagicScrollItem> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<MagicScrollItem> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<MagicScrollItem> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	[Networked(OnChanged = "EffectIndexChanged")]
	[NetworkedWeaved(5, 1)]
	public unsafe int EffectIndex
	{
		get
		{
			if (((NetworkBehaviour)this).Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicScrollItem.EffectIndex. Networked properties can only be accessed when Spawned() has been called.");
			}
			return ((NetworkBehaviour)this).Ptr[5];
		}
		set
		{
			if (((NetworkBehaviour)this).Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicScrollItem.EffectIndex. Networked properties can only be accessed when Spawned() has been called.");
			}
			((NetworkBehaviour)this).Ptr[5] = value;
		}
	}

	public override string PrefabName => "LycansNewRoles.ItemMagicScroll";

	public override void Spawned()
	{
		((Item)this).Spawned();
		UpdateEffect();
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			RandomizeEffect();
		}
	}

	[Preserve]
	public static void EffectIndexChanged(Changed<MagicScrollItem> changed)
	{
		try
		{
			MagicScrollItem behaviour = changed.Behaviour;
			behaviour.UpdateEffect();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("EffectIndexChanged error: " + ex));
		}
	}

	private void UpdateEffect()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		Effect = EffectManager.GetEffect(EffectIndex);
		((Item)this).ItemQuantity = BalancingValues.GetScrollCharges(Effect);
		Color white = Color.white;
		white = ((!(Effect is CustomEffect customEffect)) ? Effect.GetColor() : customEffect.Color);
		Light component = ((Component)((Component)this).transform.Find("MagicScrollLight")).GetComponent<Light>();
		component.color = white;
		component.intensity = 0.1f;
		component.range = 0.25f;
	}

	public void RandomizeEffect()
	{
		Effect randomEffectWithPonderation = GetRandomEffectWithPonderation();
		EffectIndex = EffectManager.GetEffectIndex(randomEffectWithPonderation);
	}

	public static Effect GetRandomEffectWithPonderation()
	{
		List<Effect> list = new List<Effect>();
		foreach (Effect effect in EffectManager.GetEffects())
		{
			for (int i = 0; i < BalancingValues.ScrollsEffectPonderation(effect); i++)
			{
				list.Add(effect);
			}
		}
		return CollectionsUtil.Grab<Effect>(list, 1).First();
	}

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
			Rpc_Use_Magic_Scroll(((SimulationBehaviour)this).Runner, player.Index, target.Index);
		}
	}

	[Rpc]
	public unsafe static void Rpc_Use_Magic_Scroll(NetworkRunner runner, int playerIndex, int targetPlayerIndex)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
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
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Use_Magic_Scroll(Fusion.NetworkRunner,System.Int32,System.Int32)")), data);
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
			if ((Object)(object)playerController.Item != (Object)null && playerController.Item is MagicScrollItem magicScrollItem)
			{
				if (runner.IsServer)
				{
					Effect effect = EffectManager.GetEffect(magicScrollItem.EffectIndex);
					float value = Traverse.Create((object)effect).Field<float>("duration").Value;
					PlayerCustom.ApplyEffectToPlayer(playerController2, effect, runner, 0.75f);
					PlayerCustom.Rpc_Effect_On_Player(runner, player2.Index, 1);
					GameManager.Rpc_BroadcastFollowSound(runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)playerController2).transform.position, 15f, 0.5f);
					if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothBlueMage)
					{
						player.SecondaryRoleUniqueInt = magicScrollItem.EffectIndex;
					}
					if (player2.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothBlueMage)
					{
						player2.SecondaryRoleUniqueInt = magicScrollItem.EffectIndex;
					}
					if (player.Stats != null)
					{
						player.Stats.AddAction(new PlayerStats.PlayerAction
						{
							ActionType = "UseGadget",
							ActionName = TranslationManager.Instance.GetTranslation("NALES_ITEM_SCROLL").Replace("#EFFECT", TranslationManager.Instance.GetTranslation(magicScrollItem.Effect.GetTranslateKey())),
							ActionTarget = ((object)playerController2.PlayerData.Username/*cast due to constrained. prefix*/).ToString()
						}, ((Component)player.PlayerController).transform.position);
					}
				}
				((Item)magicScrollItem).ItemQuantity = ((Item)magicScrollItem).ItemQuantity - 1;
				if (((Item)magicScrollItem).ItemQuantity > 0)
				{
					((Item)magicScrollItem).StartDelayTimer(Traverse.Create((object)magicScrollItem).Field<float>("resetDelay").Value);
				}
				else
				{
					((Item)magicScrollItem).DestroyItem();
				}
			}
			player2.FlashPlayer(Color.blue);
			player.PlayerAnimations.PlayNonLoopAnimation("HumanM@MagicAttackDirect1H01_R");
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Use_Magic_Scroll error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Use_Magic_Scroll(Fusion.NetworkRunner,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Use_Magic_Scroll_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int targetPlayerIndex = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Use_Magic_Scroll(runner, playerIndex, targetPlayerIndex);
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
