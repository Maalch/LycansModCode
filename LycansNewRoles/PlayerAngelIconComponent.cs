using UnityEngine;

namespace LycansNewRoles;

public class PlayerAngelIconComponent : MonoBehaviour
{
	public static GameObject AngelIconPrefab;

	private GameObject _angelIcon;

	private void Awake()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		_angelIcon = Object.Instantiate<GameObject>(AngelIconPrefab, ((Component)((Component)this).GetComponent<PlayerController>()).transform);
		_angelIcon.SetActive(true);
		_angelIcon.transform.position = new Vector3(_angelIcon.transform.position.x, _angelIcon.transform.position.y + 2f, _angelIcon.transform.position.z);
		_angelIcon.SetActive(false);
	}

	private void Update()
	{
		if (_angelIcon.activeSelf)
		{
			_angelIcon.transform.Rotate(0f, Time.deltaTime * 128f, 0f);
		}
	}

	public void SetVisible(bool visible)
	{
		GameObject angelIcon = _angelIcon;
		if (angelIcon != null)
		{
			angelIcon.SetActive(visible);
		}
	}
}
