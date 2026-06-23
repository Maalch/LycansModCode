namespace LycansNewRoles.Sabotages;

public class SabotageInfo
{
	public string DescriptionKey { get; private set; }

	public string InteractionKey { get; private set; }

	public float Range { get; private set; }

	public SabotageInfo(string descriptionKey, string interactionKey, float range)
	{
		DescriptionKey = descriptionKey;
		InteractionKey = interactionKey;
		Range = range;
	}
}
