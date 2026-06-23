using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class TrapResistanceEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectTrapResistance";

	public override string TranslateKey => "NALES_EFFECT_TRAP_RESISTANCE";

	public override Color Color => Color.green;

	public override EffectType CustomEffectType => (EffectType)0;

	public override DisplayPerRoleType DisplayType => DisplayPerRoleType.Nobody;

	public override bool DurationAffectedByModifiers => false;
}
