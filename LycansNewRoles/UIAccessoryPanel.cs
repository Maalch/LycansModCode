using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UIAccessoryPanel : MonoBehaviour
{
	private Image _accessoryImage;

	private TextMeshProUGUI _textInfo;

	private void Start()
	{
		_accessoryImage = ((Component)((Component)this).transform.Find("Accessory")).GetComponent<Image>();
		_textInfo = ((Component)((Component)this).transform.Find("TextInfo")).GetComponent<TextMeshProUGUI>();
		UpdateAccessory(null);
	}

	public void UpdateAccessory(PlayerCustom playerCustom)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		((Behaviour)_textInfo).enabled = false;
		if ((Object)(object)playerCustom != (Object)null)
		{
			if ((Object)(object)playerCustom.Accessory != (Object)null)
			{
				((Behaviour)_accessoryImage).enabled = true;
				_accessoryImage.sprite = ((Item)playerCustom.Accessory).Sprite();
				TickTimer itemTimer = ((Item)playerCustom.Accessory).ItemTimer;
				if (((TickTimer)(ref itemTimer)).IsRunning)
				{
					((Behaviour)_textInfo).enabled = true;
					TextMeshProUGUI textInfo = _textInfo;
					itemTimer = ((Item)playerCustom.Accessory).ItemTimer;
					((TMP_Text)textInfo).text = Mathf.CeilToInt(((TickTimer)(ref itemTimer)).RemainingTime(((SimulationBehaviour)playerCustom).Runner).Value) + "s";
				}
			}
			else
			{
				((Behaviour)_accessoryImage).enabled = false;
			}
		}
		else
		{
			((Behaviour)_accessoryImage).enabled = false;
		}
	}
}
