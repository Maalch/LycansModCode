using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class TransformationEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectTransformation";

	public override string TranslateKey => "NALES_EFFECT_TRANSFORMATION";

	public override Color Color => Color.red;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool CanBeDispelled => false;

	public override bool DurationAffectedByModifiers => false;

	public override bool ReducedByResilience => false;
}
