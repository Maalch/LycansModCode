using UnityEngine;

namespace LycansNewRoles;

public class PlayerAskForSpeechIconComponent : MonoBehaviour
{
	public static GameObject AskForSpeechIconPrefab;

	private GameObject _askForSpeechIcon;

	private Transform _visual;

	private void Awake()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		_askForSpeechIcon = Object.Instantiate<GameObject>(AskForSpeechIconPrefab, ((Component)((Component)this).GetComponent<PlayerController>()).transform);
		_askForSpeechIcon.SetActive(true);
		_visual = _askForSpeechIcon.transform.Find("Visual");
		_askForSpeechIcon.transform.position = new Vector3(_askForSpeechIcon.transform.position.x, _askForSpeechIcon.transform.position.y + 2.3f, _askForSpeechIcon.transform.position.z);
		_askForSpeechIcon.SetActive(false);
	}

	private void Update()
	{
		if (_askForSpeechIcon.gameObject.activeSelf)
		{
			_visual.Rotate(0f, Time.deltaTime * 256f, 0f);
		}
	}

	public void SetVisible(bool visible)
	{
		GameObject askForSpeechIcon = _askForSpeechIcon;
		if (askForSpeechIcon != null)
		{
			askForSpeechIcon.SetActive(visible);
		}
	}
}
