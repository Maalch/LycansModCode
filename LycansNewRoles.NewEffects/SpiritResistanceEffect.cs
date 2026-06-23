using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class SpiritResistanceEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectSpiritResistance";

	public override string TranslateKey => "NALES_EFFECT_SPIRIT_RESISTANCE";

	public override Color Color => Color.green;

	public override EffectType CustomEffectType => (EffectType)0;

	public override DisplayPerRoleType DisplayType => DisplayPerRoleType.Nobody;

	public override bool DurationAffectedByModifiers => false;

	public override bool KeepOnWolfTransformation => true;

	public override bool CanBeDispelled => false;
}
