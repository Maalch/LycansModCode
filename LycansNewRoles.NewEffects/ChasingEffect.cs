using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class ChasingEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectChasing";

	public override string TranslateKey => "NALES_EFFECT_CHASE";

	public override Color Color => Color.blue;

	public override EffectType CustomEffectType => (EffectType)0;

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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
		if ((Object)(object)player != (Object)null)
		{
			PlayerCustomRegistry.GetPlayer(targetPlayer).UpdateMoveSpeed();
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustomRegistry.GetPlayer(targetPlayer).UpdateMoveSpeed();
			}
		}
	}
}
