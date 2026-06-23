using System.Diagnostics;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerHeartSeethroughComponent : MonoBehaviour
{
	public static GameObject SeethroughPrefab;

	private GameObject _seethroughObject;

	private Stopwatch _effectWatch = new Stopwatch();

	private bool _effectActive = false;

	private bool _effectGrowing = false;

	private void Awake()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		_seethroughObject = Object.Instantiate<GameObject>(SeethroughPrefab, ((Component)((Component)this).GetComponent<PlayerController>()).transform);
		_seethroughObject.SetActive(true);
		_seethroughObject.transform.position = new Vector3(_seethroughObject.transform.position.x, _seethroughObject.transform.position.y + 1f, _seethroughObject.transform.position.z);
		_seethroughObject.SetActive(false);
		_effectWatch.Start();
	}

	private void Update()
	{
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		if (!_seethroughObject.activeSelf)
		{
			return;
		}
		_seethroughObject.transform.LookAt(((Component)PlayerController.Local.LocalCameraHandler.PovPlayer).transform);
		if (_effectActive)
		{
			if (_effectWatch.ElapsedMilliseconds >= 250)
			{
				if (_effectGrowing)
				{
					_effectGrowing = false;
				}
				else
				{
					_effectActive = false;
				}
				_effectWatch.Restart();
			}
			else
			{
				float num = Time.deltaTime * 0.5f;
				if (_effectGrowing)
				{
					_seethroughObject.transform.localScale = new Vector3(_seethroughObject.transform.localScale.x + num, _seethroughObject.transform.localScale.y + num, _seethroughObject.transform.localScale.z + num);
				}
				else
				{
					_seethroughObject.transform.localScale = new Vector3(_seethroughObject.transform.localScale.x - num, _seethroughObject.transform.localScale.y - num, _seethroughObject.transform.localScale.z - num);
				}
			}
		}
		else if (_effectWatch.ElapsedMilliseconds >= 1000)
		{
			_effectActive = true;
			_effectGrowing = true;
			_effectWatch.Restart();
		}
	}

	public void SetVisible(bool visible, bool wolf)
	{
		GameObject seethroughObject = _seethroughObject;
		if (seethroughObject != null)
		{
			seethroughObject.SetActive(visible);
		}
		if (visible)
		{
			if (wolf)
			{
				((Renderer)_seethroughObject.GetComponent<MeshRenderer>()).material.shader = PlayerCustom.SeeThroughShaderWolf;
			}
			else
			{
				((Renderer)_seethroughObject.GetComponent<MeshRenderer>()).material.shader = PlayerCustom.SeeThroughShaderHuman;
			}
		}
	}
}
