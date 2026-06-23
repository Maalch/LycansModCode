using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class SpotterEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectSpotter";

	public override string TranslateKey => "NALES_EFFECT_SPOTTER";

	public override Color Color => Color.magenta;

	public override EffectType CustomEffectType => (EffectType)0;

	public override bool CanBeDispelled => false;

	protected override void ApplyEffectToPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustomRegistry.GetPlayer(targetPlayer).Spotter = NetworkBool.op_Implicit(true);
			}
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustomRegistry.GetPlayer(targetPlayer).Spotter = NetworkBool.op_Implicit(false);
			}
		}
	}
}
