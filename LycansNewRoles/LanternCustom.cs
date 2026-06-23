using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using UnityEngine;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(5)]
public class LanternCustom : MonoBehaviour
{
	public static Dictionary<CustomLight, LanternCustom> LanternCustomsByLight = new Dictionary<CustomLight, LanternCustom>();

	private List<Lantern> _lanterns;

	private Stopwatch _flickeringStopwatch = new Stopwatch();

	private int _remainingFlickers = 0;

	public bool On = false;

	public List<Lantern> Lanterns => _lanterns;

	private void Awake()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		_lanterns = (from o in Object.FindObjectsOfType<Lantern>()
			where Vector3.Distance(((Component)o).transform.position, ((Component)this).transform.position) < 0.5f
			select o).ToList();
		((Component)this).transform.parent = ((Component)_lanterns[0]).transform;
		CustomLight[] array = Object.FindObjectsOfType<CustomLight>();
		float? num = null;
		Vector3 val = default(Vector3);
		((Vector3)(ref val))._002Ector(((Component)_lanterns[0]).transform.position.x, ((Component)_lanterns[0]).transform.position.y + 2f, ((Component)_lanterns[0]).transform.position.z);
		CustomLight key = null;
		CustomLight[] array2 = array;
		foreach (CustomLight val2 in array2)
		{
			float num3 = Vector3.Distance(val, ((Component)val2).transform.position);
			if (!num.HasValue || num3 < num.Value)
			{
				num = num3;
				key = val2;
			}
		}
		LanternCustomsByLight[key] = this;
	}

	private void Update()
	{
		if (_flickeringStopwatch.ElapsedMilliseconds >= 500)
		{
			_remainingFlickers--;
			ToggleLight(_remainingFlickers > 0);
			if (_remainingFlickers > 0)
			{
				_flickeringStopwatch.Restart();
				return;
			}
			_flickeringStopwatch.Stop();
			_flickeringStopwatch.Reset();
		}
	}

	public void StartFlicker(int amount)
	{
		_remainingFlickers = amount;
		ToggleLight(random: true);
		_flickeringStopwatch.Restart();
	}

	private void ToggleLight(bool random)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		foreach (Lantern lantern in Lanterns)
		{
			bool flag = (random ? (Random.value < 0.5f) : NetworkBool.op_Implicit(GameManager.LightingManager.IsNight));
			lantern.Switch(flag);
			On = flag;
		}
	}
}
