using UnityEngine;

namespace LycansNewRoles;

public class SingleInteraction
{
	public enum SingleInteractionType
	{
		NormalInteraction,
		SecondaryInteraction,
		SecondaryRoleInteraction,
		ItemInteraction,
		SecondaryItemInteraction,
		AccessoryInteraction
	}

	public SingleInteractionType ActionType;

	public float ActionRange;

	public Color ActionColor;

	public string TranslationKey;

	public object[] TextArguments;

	public bool ActionAvailable;

	public SingleInteraction(SingleInteractionType actionType, float actionRange, Color actionColor, string translationKey, object[] textArguments, bool actionAvailable = true)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		ActionType = actionType;
		ActionRange = actionRange;
		ActionColor = actionColor;
		TranslationKey = translationKey;
		TextArguments = textArguments;
		ActionAvailable = actionAvailable;
	}
}
