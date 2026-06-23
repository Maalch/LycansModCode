using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class MoleClockEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectResistance";

	public override string TranslateKey => "NALES_EFFECT_MOLE_CLOCK";

	public override Color Color => Color.red;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool DurationAffectedByModifiers => false;

	public override bool CanBeDispelled => false;
}
