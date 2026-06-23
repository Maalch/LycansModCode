using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class ChaosEffect : CustomEffect
{
	public enum ChaosPossibleEffect
	{
		CreateTraps,
		CreateGrenade,
		UseScrollOnNearbyPlayer,
		LockNearbyDoor,
		UnlockNearbyDoor,
		CreateSmoke,
		UseDiamondOnNearbyPlayer
	}

	public override string CustomEffectName => "LycansNewRoles.EffectChaos";

	public override string TranslateKey => "NALES_EFFECT_CHAOS";

	public override Color Color => Color.red;

	public override EffectType CustomEffectType => (EffectType)1;

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
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayer);
				player2.ChaosTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 0.5f);
			}
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayer);
				player2.ChaosTimer = TickTimer.None;
			}
		}
	}
}
