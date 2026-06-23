using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class ParalyzedEffect : CustomEffect
{
	public static Color EffectColor = new Color(1f, 0.4f, 0.4f);

	public override string CustomEffectName => "LycansNewRoles.EffectParalyzed";

	public override string TranslateKey => "NALES_EFFECT_PARALYZED";

	public override Color Color => EffectColor;

	public override EffectType CustomEffectType => (EffectType)2;

	public override bool CanBeDispelled => true;

	public override void Spawned()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		((Effect)this).Spawned();
		if (((Effect)this).EffectPlayer == PlayerController.Local.LocalCameraHandler.PovPlayer.Ref)
		{
			ColorAdjustmentManager.FlashScreen(Color.red);
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Effect)this).EffectPlayer);
		player.FlashPlayer(Color.red);
	}

	protected override void ApplyEffectToPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				player.Paralyzed = NetworkBool.op_Implicit(true);
				player.UpdateCanMoveAnimation();
			}
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				player.Paralyzed = NetworkBool.op_Implicit(false);
				player.UpdateCanMoveAnimation();
			}
		}
	}
}
