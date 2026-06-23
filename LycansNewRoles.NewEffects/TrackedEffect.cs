using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class TrackedEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectTracked";

	public override string TranslateKey => "NALES_EFFECT_TRACKED";

	public override Color Color => Color.red;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool CanBeDispelled => true;

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
				PlayerCustomRegistry.GetPlayer(targetPlayer).Tracked = NetworkBool.op_Implicit(true);
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
				PlayerCustomRegistry.GetPlayer(targetPlayer).Tracked = NetworkBool.op_Implicit(false);
			}
		}
	}
}
