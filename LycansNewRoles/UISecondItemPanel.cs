using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UISecondItemPanel : MonoBehaviour
{
	private Image _image;

	private TextMeshProUGUI _quantity;

	private TextMeshProUGUI _detailsText;

	public Image Image => _image;

	public TextMeshProUGUI Quantity => _quantity;

	public TextMeshProUGUI DetailsText => _detailsText;

	private void Start()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		_image = ((Component)((Component)this).transform.Find("ItemImage")).GetComponent<Image>();
		_quantity = ((Component)((Component)this).transform.Find("ItemQuantity")).GetComponent<TextMeshProUGUI>();
		_detailsText = ((Component)((Component)this).transform.Find("ItemDetailsText(Clone)")).GetComponent<TextMeshProUGUI>();
		((TMP_Text)_detailsText).transform.position = new Vector3(((Component)_image).transform.position.x, ((Component)_image).transform.position.y + 96f, ((Component)_image).transform.position.z);
	}
}
