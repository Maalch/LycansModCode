using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles.NewEffects;

[NetworkBehaviourWeaved(3)]
public abstract class CustomEffect : Effect
{
	public enum DisplayPerRoleType
	{
		Everyone,
		WolvesOnly,
		Nobody
	}

	public virtual string CustomEffectName => null;

	public virtual string TranslateKey => string.Empty;

	public virtual Color Color => Color.white;

	public virtual EffectType CustomEffectType => (EffectType)1;

	public virtual DisplayPerRoleType DisplayType => DisplayPerRoleType.Everyone;

	public virtual bool KeepOnWolfTransformation => false;

	public virtual bool CanBeDispelled => false;

	public virtual bool DurationAffectedByModifiers => true;

	public void InitWithSpecificDuration(PlayerController targetPlayer, float duration)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Invalid comparison between Unknown and I4
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		targetPlayer.PlayerEffectManager.ApplyEffect((Effect)(object)this);
		Traverse.Create((object)this).Field<PlayerRef>("_EffectPlayer").Value = targetPlayer.Ref;
		if ((int)((Effect)this).GetEffectType() == 2 && NetworkBool.op_Implicit(BeastManager.Instance.BeastActive) && NetworkBool.op_Implicit(targetPlayer.IsWolf))
		{
			duration *= 0.5f;
		}
		int num = (int)(duration * (float)((SimulationBehaviour)targetPlayer).Runner.Config.Simulation.TickRate);
		Traverse.Create((object)this).Field<CustomTickTimer>("_EffectTimer").Value = CustomTickTimer.CreateFromTicks(((SimulationBehaviour)targetPlayer).Runner, num);
		((NetworkBehaviour)this).CopyBackingFieldsToState(true);
		((Effect)this).ApplyEffectToPlayerSpecific(targetPlayer.Ref);
	}

	protected override void ApplyEffectToPlayerSpecific(PlayerRef targetPlayer)
	{
	}

	protected override void RemoveEffectFromPlayerSpecific(PlayerRef targetPlayer)
	{
	}
}
