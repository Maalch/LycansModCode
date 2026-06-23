using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class ResistanceEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectResistance";

	public override string TranslateKey => "NALES_EFFECT_RESISTANCE";

	public override Color Color => Color.green;

	public override EffectType CustomEffectType => (EffectType)0;

	public override bool DurationAffectedByModifiers => true;

	public override bool CanBeDispelled => false;
}
