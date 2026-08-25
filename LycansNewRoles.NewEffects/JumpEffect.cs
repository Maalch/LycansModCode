using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class JumpEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectJump";

	public override string TranslateKey => "NALES_EFFECT_JUMP";

	public override Color Color => Color.green;

	public override EffectType CustomEffectType => (EffectType)1;

	public override bool CanBeDispelled => false;

	public override bool DurationAffectedByModifiers => false;

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
				player2.Jump = NetworkBool.op_Implicit(true);
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
				player2.Jump = NetworkBool.op_Implicit(false);
			}
		}
	}
}
