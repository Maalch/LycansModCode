using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class TournamentLoser : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectTournamentLoser";

	public override string TranslateKey => "NALES_EFFECT_TOURNAMENT_LOSER";

	public override Color Color => Color.red;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool KeepOnWolfTransformation => true;

	public override bool CanBeDispelled => false;
}
