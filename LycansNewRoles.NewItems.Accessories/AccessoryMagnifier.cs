using Fusion;

namespace LycansNewRoles.NewItems.Accessories;

[NetworkBehaviourWeaved(6)]
public class AccessoryMagnifier : Accessory
{
	private static Changed<AccessoryMagnifier> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<AccessoryMagnifier> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<AccessoryMagnifier> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	public override string PrefabName => "LycansNewRoles.AccessoryMagnifier";

	public override string DescriptionTranslateKey => "NALES_ACCESSORY_MAGNIFIER_DESCRIPTION";

	public override string TinkererDescriptionTranslateKey => "NALES_ACCESSORY_MAGNIFIER_TINKERER";

	public override int TinkererPowerCooldown => 90;

	public override bool TinkererPowerRequiresPlayerTarget => true;

	protected override void ItemCollected()
	{
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
