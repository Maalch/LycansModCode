using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UICustomizationComponent : MonoBehaviour
{
	public bool Active = false;

	private GameObject _parent;

	private TextMeshProUGUI _description;

	private CustomizationOptionComponent _choiceHat;

	private CustomizationOptionComponent _choiceColor;

	private CustomizationOptionComponent _choicePet;

	private List<CustomizationOptionComponent.CustomizationOptionType> _types = new List<CustomizationOptionComponent.CustomizationOptionType>
	{
		CustomizationOptionComponent.CustomizationOptionType.Hat,
		CustomizationOptionComponent.CustomizationOptionType.Color,
		CustomizationOptionComponent.CustomizationOptionType.Pet
	};

	private int _currentTypeIndex;

	private void Start()
	{
		_parent = ((Component)((Component)this).transform.Find("Parent")).gameObject;
		_description = ((Component)_parent.transform.Find("Description")).GetComponentInChildren<TextMeshProUGUI>();
		((TMP_Text)_description).text = TranslationManager.Instance.GetTranslation("NALES_UI_CUSTOMIZATION_DESCRIPTION").Replace("{0}", LycansUtility.GetInputDisplayCustom((InputActionName)11).Replace("-", ""));
		_choiceHat = ((Component)_parent.transform.Find("Choices").Find("ChoiceHat")).gameObject.AddComponent<CustomizationOptionComponent>();
		_choiceColor = ((Component)_parent.transform.Find("Choices").Find("ChoiceColor")).gameObject.AddComponent<CustomizationOptionComponent>();
		_choicePet = ((Component)_parent.transform.Find("Choices").Find("ChoicePet")).gameObject.AddComponent<CustomizationOptionComponent>();
		_choiceHat.Init("NALES_UI_CUSTOMIZATION_HAT", CustomizationOptionComponent.CustomizationOptionType.Hat);
		_choiceColor.Init("NALES_UI_CUSTOMIZATION_COLOR", CustomizationOptionComponent.CustomizationOptionType.Color);
		_choicePet.Init("NALES_UI_CUSTOMIZATION_PET", CustomizationOptionComponent.CustomizationOptionType.Pet);
		Hide();
	}

	public void Show()
	{
		Active = true;
		_parent.SetActive(true);
		SetType(0);
	}

	public void Hide()
	{
		Active = false;
		_parent.SetActive(false);
	}

	public void CycleType()
	{
		if (_currentTypeIndex == _types.Count - 1)
		{
			_currentTypeIndex = 0;
		}
		else
		{
			_currentTypeIndex++;
		}
		SetType(_currentTypeIndex);
	}

	private void SetType(int typeIndex)
	{
		_currentTypeIndex = typeIndex;
		CustomizationOptionComponent.CustomizationOptionType typeToShow = _types[typeIndex];
		_choiceHat.Toggle(typeToShow);
		_choiceColor.Toggle(typeToShow);
		_choicePet.Toggle(typeToShow);
	}

	public void ChooseNext()
	{
		switch (_types[_currentTypeIndex])
		{
		case CustomizationOptionComponent.CustomizationOptionType.Hat:
			PlayerController.Local.UpdateHatLocal(true);
			break;
		case CustomizationOptionComponent.CustomizationOptionType.Color:
			ColorPicker.NextColor();
			break;
		case CustomizationOptionComponent.CustomizationOptionType.Pet:
			PetPicker.NextPet();
			break;
		}
	}

	public void ChoosePrevious()
	{
		switch (_types[_currentTypeIndex])
		{
		case CustomizationOptionComponent.CustomizationOptionType.Hat:
			PlayerController.Local.UpdateHatLocal(false);
			break;
		case CustomizationOptionComponent.CustomizationOptionType.Color:
			ColorPicker.PreviousColor();
			break;
		case CustomizationOptionComponent.CustomizationOptionType.Pet:
			PetPicker.PreviousPet();
			break;
		}
	}
}
