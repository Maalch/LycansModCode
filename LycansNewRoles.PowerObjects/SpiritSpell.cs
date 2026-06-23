using System.Linq;
using Fusion;
using LycansNewRoles.NewEffects;
using UnityEngine;

namespace LycansNewRoles.PowerObjects;

[NetworkBehaviourWeaved(3)]
public class SpiritSpell : NetworkBehaviour
{
	public override void FixedUpdateNetwork()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Invalid comparison between Unknown and I4
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Invalid comparison between Unknown and I4
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || (int)val == 5)
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)((SimulationBehaviour)this).Runner != (Object)null && ((SimulationBehaviour)this).Runner.IsServer)
		{
			PlayerController component = ((Component)other).gameObject.GetComponent<PlayerController>();
			if ((Object)(object)component != (Object)null && NetworkBool.op_Implicit(component.IsWolf) && !component.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is SpiritResistanceEffect))
			{
				component.Hunger = Mathf.Max((float)GameManager.Instance.MaxHunger * 0.25f, component.Hunger - (float)GameManager.Instance.MaxHunger * 0.05f);
				PlayerCustom.ApplyEffectToPlayer(component, "LycansNewRoles.EffectWounded", ((SimulationBehaviour)this).Runner, 1f, 6f);
				GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("Exorcism"), ((Component)component).transform.position, 20f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID), 1f);
				PlayerCustom.ApplyEffectToPlayer(component, "LycansNewRoles.EffectResilience", ((SimulationBehaviour)this).Runner, 1f, 5f);
				PlayerCustom.ApplyEffectToPlayer(component, "LycansNewRoles.EffectSpiritResistance", ((SimulationBehaviour)this).Runner, 1f, 20f);
			}
		}
	}
}
