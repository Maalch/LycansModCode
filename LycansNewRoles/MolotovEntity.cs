using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using LycansNewRoles.NewItems;
using UnityEngine;

namespace LycansNewRoles;

public class MolotovEntity : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static OnBeforeSpawned _003C_003E9__12_0;

		public static OnBeforeSpawned _003C_003E9__12_1;

		internal void _003CUpdate_003Eb__12_0(NetworkRunner _, NetworkObject no)
		{
		}

		internal void _003CUpdate_003Eb__12_1(NetworkRunner _, NetworkObject no)
		{
		}
	}

	public static GameObject MolotovEntityPrefab;

	private bool _purifierEntity = false;

	private Stopwatch _disappearWatch = new Stopwatch();

	private Stopwatch _createFireWatch = new Stopwatch();

	private const float _molotovMoveSpeed = 1.25f;

	private const float _purifierMoveSpeed = 2.5f;

	private const int _molotovCreateFireDelayMilliseconds = 1000;

	private const int _purifierCreateFireDelayMilliseconds = 500;

	private const int _molotovEntityDurationMilliseconds = 6000;

	private const int _purifierEntityDurationMilliseconds = 3000;

	private void Awake()
	{
		((Component)this).gameObject.layer = 25;
		_disappearWatch.Start();
		_createFireWatch.Start();
	}

	public void Init(bool purifierEntity)
	{
		_purifierEntity = purifierEntity;
	}

	private void Update()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		bool flag = false;
		EGameState localGameState = GameManager.LocalGameState;
		EGameState val = localGameState;
		if ((int)val <= 1 || (int)val == 5)
		{
			flag = true;
		}
		if (flag)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
			return;
		}
		if (_purifierEntity)
		{
			((Component)this).GetComponent<CharacterController>().Move(((Component)this).transform.forward * 2.5f * Time.deltaTime);
			if (!((Component)this).GetComponent<CharacterController>().isGrounded)
			{
				((Component)this).GetComponent<CharacterController>().Move(new Vector3(0f, -1f, 0f) * Time.deltaTime);
			}
			if (_createFireWatch.ElapsedMilliseconds >= 500)
			{
				NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemPurifierFire");
				NetworkRunner runner = ((SimulationBehaviour)GameManager.Instance).Runner;
				Vector3? val2 = ((Component)this).transform.position;
				Quaternion? val3 = Quaternion.identity;
				object obj = _003C_003Ec._003C_003E9__12_0;
				if (obj == null)
				{
					OnBeforeSpawned val4 = delegate
					{
					};
					_003C_003Ec._003C_003E9__12_0 = val4;
					obj = (object)val4;
				}
				NetworkObject val5 = runner.Spawn(networkObject, val2, val3, (PlayerRef?)null, (OnBeforeSpawned)obj, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				float num = Mathf.InverseLerp(0f, 5f, (float)_disappearWatch.ElapsedMilliseconds);
				float burnDuration = Mathf.Lerp(3f, 7f, num);
				((Component)val5).GetComponent<PurifierFire>().Init(9000, burnDuration);
				_createFireWatch.Restart();
			}
			if (_disappearWatch.ElapsedMilliseconds >= 3000)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
			return;
		}
		((Component)this).GetComponent<CharacterController>().Move(((Component)this).transform.forward * 1.25f * Time.deltaTime);
		if (!((Component)this).GetComponent<CharacterController>().isGrounded)
		{
			((Component)this).GetComponent<CharacterController>().Move(new Vector3(0f, -1f, 0f) * Time.deltaTime);
		}
		if (_createFireWatch.ElapsedMilliseconds >= 1000)
		{
			NetworkPrefabId networkObject2 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemMolotovFire");
			NetworkRunner runner2 = ((SimulationBehaviour)GameManager.Instance).Runner;
			Vector3? val6 = ((Component)this).transform.position;
			Quaternion? val7 = Quaternion.identity;
			object obj2 = _003C_003Ec._003C_003E9__12_1;
			if (obj2 == null)
			{
				OnBeforeSpawned val8 = delegate
				{
				};
				_003C_003Ec._003C_003E9__12_1 = val8;
				obj2 = (object)val8;
			}
			NetworkObject val9 = runner2.Spawn(networkObject2, val6, val7, (PlayerRef?)null, (OnBeforeSpawned)obj2, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			float num2 = Mathf.InverseLerp(0f, 6f, (float)_disappearWatch.ElapsedMilliseconds);
			float burnDuration2 = Mathf.Lerp(2f, 8f, num2);
			((Component)val9).GetComponent<MolotovFire>().Init(12000, burnDuration2);
			_createFireWatch.Restart();
		}
		if (_disappearWatch.ElapsedMilliseconds >= 6000)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}
}
