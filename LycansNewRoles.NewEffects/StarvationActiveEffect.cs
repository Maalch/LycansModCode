using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class StarvationActiveEffect : CustomEffect
{
	public static Color EffectColor = new Color(0.6f, 0f, 1f);

	public override string CustomEffectName => "LycansNewRoles.EffectStarvationActive";

	public override string TranslateKey => "NALES_EFFECT_STARVATION";

	public override Color Color => EffectColor;

	public override EffectType CustomEffectType => (EffectType)2;

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
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null)
			{
				PlayerCustomRegistry.GetPlayer(targetPlayer).StarvationActive = true;
			}
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
				PlayerCustomRegistry.GetPlayer(targetPlayer).StarvationActive = false;
			}
		}
	}
}
