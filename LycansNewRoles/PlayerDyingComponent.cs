using UnityEngine;

namespace LycansNewRoles;

public class PlayerDyingComponent : MonoBehaviour
{
	public static GameObject DyingPlayerInfoPrefab;

	private GameObject _dyingPlayerInfo;

	private void Awake()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		_dyingPlayerInfo = Object.Instantiate<GameObject>(DyingPlayerInfoPrefab, ((Component)((Component)this).GetComponent<PlayerController>()).transform);
		_dyingPlayerInfo.SetActive(true);
		_dyingPlayerInfo.transform.position = new Vector3(_dyingPlayerInfo.transform.position.x, _dyingPlayerInfo.transform.position.y + 1.5f, _dyingPlayerInfo.transform.position.z);
		_dyingPlayerInfo.SetActive(false);
	}

	public void PlayDyingEffect()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		ColorAdjustmentManager.FlashScreen(Color.red);
		AudioManager.Play("SurvivalistDying", (MixerTarget)2, 1f, 1f);
	}

	public void SetVisible(bool visible)
	{
		GameObject dyingPlayerInfo = _dyingPlayerInfo;
		if (dyingPlayerInfo != null)
		{
			dyingPlayerInfo.SetActive(visible);
		}
	}

	private void Update()
	{
		if (_dyingPlayerInfo.activeSelf)
		{
			_dyingPlayerInfo.transform.Rotate(0f, Time.deltaTime * 128f, 0f);
		}
	}
}
