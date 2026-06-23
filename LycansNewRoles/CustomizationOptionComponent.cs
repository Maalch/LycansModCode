using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class CustomizationOptionComponent : MonoBehaviour
{
	public enum CustomizationOptionType
	{
		Hat,
		Color,
		Pet
	}

	private static Color ColorInactive = new Color(0.8f, 0.8f, 0.8f, 1f);

	private static Color ColorActive = new Color(0f, 0.8f, 0.8f, 1f);

	private CustomizationOptionType _type;

	public void Init(string translationKey, CustomizationOptionType type)
	{
		((TMP_Text)((Component)this).GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation(translationKey);
		_type = type;
	}

	public void Toggle(CustomizationOptionType typeToShow)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)((Component)this).GetComponent<Image>()).color = ((_type == typeToShow) ? ColorActive : ColorInactive);
	}
}
