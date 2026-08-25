using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class EnduranceEffect : CustomEffect
{
	public static Color EffectColor = new Color(0f, 0.5f, 0f);

	public override string CustomEffectName => "LycansNewRoles.EffectEndurance";

	public override string TranslateKey => "NALES_EFFECT_ENDURANCE";

	public override Color Color => EffectColor;

	public override EffectType CustomEffectType => (EffectType)0;

	public override bool CanBeDispelled => false;

	public override DisplayPerRoleType DisplayType => DisplayPerRoleType.Nobody;

	public override bool DurationAffectedByModifiers => false;

	protected override void ApplyEffectToPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustomRegistry.GetPlayer(targetPlayer).Endurance = NetworkBool.op_Implicit(true);
			}
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustomRegistry.GetPlayer(targetPlayer).Endurance = NetworkBool.op_Implicit(false);
			}
		}
	}
}
