using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class CursedEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectCursed";

	public override string TranslateKey => "NALES_EFFECT_CURSED";

	public override Color Color => Color.red;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool DurationAffectedByModifiers => false;

	public override bool CanBeDispelled => false;
}
