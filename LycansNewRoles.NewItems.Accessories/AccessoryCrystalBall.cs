using Fusion;

namespace LycansNewRoles.NewItems.Accessories;

[NetworkBehaviourWeaved(6)]
public class AccessoryCrystalBall : Accessory
{
	private static Changed<AccessoryCrystalBall> _0024IL2CPP_CHANGED;

	private static ChangedDelegate<AccessoryCrystalBall> _0024IL2CPP_CHANGED_DELEGATE;

	private static NetworkBehaviourCallbacks<AccessoryCrystalBall> _0024IL2CPP_NETWORK_BEHAVIOUR_CALLBACKS;

	public bool Available = true;

	public override string PrefabName => "LycansNewRoles.AccessoryCrystalBall";

	public override string DescriptionTranslateKey => "NALES_ACCESSORY_CRYSTAL_BALL_DESCRIPTION";

	public override string TinkererDescriptionTranslateKey => "NALES_ACCESSORY_CRYSTAL_BALL_TINKERER";

	public override int TinkererPowerCooldown => 90;

	public override bool TinkererPowerRequiresPlayerTarget => false;

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
