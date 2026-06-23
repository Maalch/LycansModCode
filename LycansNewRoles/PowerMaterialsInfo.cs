namespace LycansNewRoles;

public class PowerMaterialsInfo
{
	public int RequiredMaterials;

	public float MaximumMaterialsPercentage;

	public float StartingMaterialsPercentage;

	public bool GainsMaterialOnCollect;

	public float BonusMultiplierAtFullHealth;

	public PowerMaterialsInfo(int requiredMaterials, float maximumMaterialsPercentage, float startingMaterialsPercentage, bool gainsMaterialsOnCollect, float bonusMultiplierAtFullHealth)
	{
		RequiredMaterials = requiredMaterials;
		MaximumMaterialsPercentage = maximumMaterialsPercentage;
		StartingMaterialsPercentage = startingMaterialsPercentage;
		GainsMaterialOnCollect = gainsMaterialsOnCollect;
		BonusMultiplierAtFullHealth = bonusMultiplierAtFullHealth;
	}
}
