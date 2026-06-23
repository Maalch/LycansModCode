using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class WeakenedEffect : CustomEffect
{
	public static Color EffectColor = new Color(1f, 0.4f, 0.4f);

	public override string CustomEffectName => "LycansNewRoles.EffectWeakened";

	public override string TranslateKey => "NALES_EFFECT_WEAKENED";

	public override Color Color => EffectColor;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool KeepOnWolfTransformation => true;

	public override bool CanBeDispelled => true;

	public override bool DurationAffectedByModifiers => false;

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
				PlayerCustomRegistry.GetPlayer(targetPlayer).Weakened = NetworkBool.op_Implicit(true);
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
				PlayerCustomRegistry.GetPlayer(targetPlayer).Weakened = NetworkBool.op_Implicit(false);
			}
		}
	}
}
