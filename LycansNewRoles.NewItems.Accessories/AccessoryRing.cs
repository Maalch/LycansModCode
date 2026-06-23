using Fusion;

namespace LycansNewRoles.NewItems.Accessories;

[NetworkBehaviourWeaved(6)]
public class AccessoryRing : Accessory
{
	private static Changed<AccessoryRing> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<AccessoryRing> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<AccessoryRing> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	public bool EffectActive = false;

	public override string PrefabName => "LycansNewRoles.AccessoryRing";

	public override string DescriptionTranslateKey => "NALES_ACCESSORY_RING_DESCRIPTION";

	public override string TinkererDescriptionTranslateKey => "NALES_ACCESSORY_RING_TINKERER";

	public override int TinkererPowerCooldown => 90;

	public override bool TinkererPowerRequiresPlayerTarget => true;

	protected override void ItemCollected()
	{
		EffectActive = false;
	}

	protected override bool CanUseItem()
	{
		return true;
	}

	public override void CopyBackingFieldsToState(bool A_1)
	{
		((Item)this).CopyBackingFieldsToState(A_1);
	}

	public override void CopyStateToBackingFields()
	{
		((Item)this).CopyStateToBackingFields();
	}
}
