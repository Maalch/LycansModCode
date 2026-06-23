using UnityEngine;

namespace LycansNewRoles;

public class PlayerBombIconComponent : MonoBehaviour
{
	public static GameObject BombIconPrefab;

	private GameObject _bombIcon;

	private void Awake()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		_bombIcon = Object.Instantiate<GameObject>(BombIconPrefab, ((Component)((Component)this).GetComponent<PlayerController>()).transform);
		_bombIcon.SetActive(true);
		_bombIcon.transform.position = new Vector3(_bombIcon.transform.position.x, _bombIcon.transform.position.y + 2f, _bombIcon.transform.position.z);
		_bombIcon.SetActive(false);
	}

	public void SetVisible(bool visible)
	{
		GameObject bombIcon = _bombIcon;
		if (bombIcon != null)
		{
			bombIcon.SetActive(visible);
		}
	}
}
