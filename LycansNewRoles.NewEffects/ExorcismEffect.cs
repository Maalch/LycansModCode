using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class ExorcismEffect : CustomEffect
{
	public static Color EffectColor = new Color(0.5f, 0.5f, 0f);

	public override string CustomEffectName => "LycansNewRoles.EffectExorcismActive";

	public override string TranslateKey => "NALES_EFFECT_EXORCISM";

	public override Color Color => EffectColor;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool KeepOnWolfTransformation => true;

	public override bool CanBeDispelled => true;

	public override void Spawned()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		((Effect)this).Spawned();
		if (((Effect)this).EffectPlayer == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref)
		{
			ColorAdjustmentManager.FlashScreen(Color.red);
		}
	}

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
				PlayerCustomRegistry.GetPlayer(targetPlayer).Exorcised = NetworkBool.op_Implicit(true);
			}
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustomRegistry.GetPlayer(targetPlayer).Exorcised = NetworkBool.op_Implicit(false);
				PlayerCustomRegistry.GetPlayer(targetPlayer).Exorciser = PlayerRef.None;
			}
		}
	}
}
