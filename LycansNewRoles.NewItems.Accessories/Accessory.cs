using UnityEngine;

namespace LycansNewRoles.NewItems.Accessories;

public abstract class Accessory : CustomItem
{
	public Light Light;

	public virtual string DescriptionTranslateKey => "";

	public virtual float CooldownAfterUse => 0f;

	public virtual string TinkererDescriptionTranslateKey => "";

	public virtual int TinkererPowerCooldown => 90;

	public virtual bool TinkererPowerRequiresPlayerTarget => false;
}
