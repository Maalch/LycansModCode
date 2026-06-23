using System;

namespace LycansNewRoles.NewItems;

public abstract class CustomItem : Item
{
	public virtual string PrefabName => "";

	protected override void AnimationCancelled()
	{
		throw new NotImplementedException();
	}

	protected override void AnimationEnded()
	{
		throw new NotImplementedException();
	}

	protected override void AnimationStarted()
	{
		throw new NotImplementedException();
	}

	protected override bool CanUseItem()
	{
		throw new NotImplementedException();
	}

	protected override void ItemCancelled()
	{
		throw new NotImplementedException();
	}

	protected override void ItemCollected()
	{
		throw new NotImplementedException();
	}

	protected override void ItemTriggered()
	{
		throw new NotImplementedException();
	}
}
