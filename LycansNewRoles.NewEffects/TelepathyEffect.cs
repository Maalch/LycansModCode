using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class TelepathyEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectTelepathy";

	public override string TranslateKey => "NALES_EFFECT_TELEPATHY";

	public override Color Color => Color.gray;

	public override EffectType CustomEffectType => (EffectType)0;

	public override bool KeepOnWolfTransformation => true;

	public override bool CanBeDispelled => false;
}
