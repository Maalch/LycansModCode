using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class NearsightedEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectNearsighted";

	public override string TranslateKey => "NALES_EFFECT_NEARSIGHTED";

	public override Color Color => Color.blue;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool CanBeDispelled => true;

	protected override void ApplyEffectToPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayer);
				player2.Nearsighted = NetworkBool.op_Implicit(true);
			}
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayer);
				player2.Nearsighted = NetworkBool.op_Implicit(false);
			}
		}
	}
}
