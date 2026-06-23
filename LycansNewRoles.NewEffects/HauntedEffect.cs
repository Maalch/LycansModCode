using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class HauntedEffect : CustomEffect
{
	public enum HauntedPossibleEffect
	{
		HealthGain,
		HealthLoss,
		OpenDoors,
		CloseDoors,
		Teleport,
		GainItem,
		RandomEffect,
		Sound,
		FlickerLanterns,
		ForcedRotation
	}

	public override string CustomEffectName => "LycansNewRoles.EffectHaunted";

	public override string TranslateKey => "NALES_EFFECT_HAUNTED";

	public override Color Color => Color.black;

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
			ColorAdjustmentManager.FlashScreen(Color.black);
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(((Effect)this).EffectPlayer);
		player.FlashPlayer(Color.black);
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
				player2.HauntedTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, 0.5f);
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
				player2.HauntedTimer = TickTimer.None;
			}
		}
	}
}
