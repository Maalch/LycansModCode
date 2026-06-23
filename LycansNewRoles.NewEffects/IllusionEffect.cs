using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Helpers.Collections;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public class IllusionEffect : CustomEffect
{
	public override string CustomEffectName => "LycansNewRoles.EffectIllusion";

	public override string TranslateKey => "NALES_EFFECT_ILLUSION";

	public override Color Color => Color.grey;

	public override EffectType CustomEffectType => (EffectType)1;

	public override DisplayPerRoleType DisplayType => DisplayPerRoleType.WolvesOnly;

	public override bool CanBeDispelled => false;

	protected override void ApplyEffectToPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
		if ((Object)(object)player != (Object)null)
		{
			PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(targetPlayer);
			List<PlayerController> list = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController p) => (Object)(object)p != (Object)(object)player && !NetworkBool.op_Implicit(p.IsDead))).ToList();
			if (list.Count > 0)
			{
				PlayerController val = CollectionsUtil.Grab<PlayerController>(list, 1).First();
				player2.IllusionTarget = val.Ref;
			}
		}
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			PlayerController player = PlayerRegistry.GetPlayer(targetPlayer);
			if ((Object)(object)player != (Object)null && !player.PlayerEffectManager.GetActiveEffects().Any((Effect o) => o is DisguisedEffect))
			{
				PlayerCustomRegistry.GetPlayer(targetPlayer).IllusionTarget = PlayerRef.None;
			}
		}
	}
}
