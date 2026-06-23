using System.Linq;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class TinyEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectTiny";

	public override string TranslateKey => "NALES_EFFECT_TINY";

	public override Color Color => Color.black;

	public override EffectType CustomEffectType => (EffectType)1;

	public override bool CanBeDispelled => true;

	protected override void ApplyEffectToPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
		if ((Object)(object)player != (Object)null)
		{
			Effect val = player.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is GiantEffect);
			if ((Object)(object)val != (Object)null)
			{
				player.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
			}
			PlayerCustomRegistry.GetPlayer(targetPlayer).Tiny = NetworkBool.op_Implicit(true);
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
		if ((Object)(object)player != (Object)null)
		{
			Effect val = player.PlayerEffectManager.GetActiveEffects().FirstOrDefault((Effect o) => o is GiantEffect);
			if ((Object)(object)val != (Object)null)
			{
				player.PlayerEffectManager.RemoveEffect(((SimulationBehaviour)val).Object.Id);
			}
			PlayerCustomRegistry.GetPlayer(targetPlayer).Tiny = NetworkBool.op_Implicit(false);
		}
	}
}
