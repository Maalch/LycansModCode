using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fusion;
using LycansNewRoles.NewItems;
using UnityEngine;

namespace LycansNewRoles;

public class MolotovEntity : MonoBehaviour
{
	public enum FireType
	{
		RegularMolotov,
		PurifierStandard,
		PurifierBoosted
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static OnBeforeSpawned _003C_003E9__16_0;

		public static OnBeforeSpawned _003C_003E9__16_1;

		public static OnBeforeSpawned _003C_003E9__16_2;

		internal void _003CUpdate_003Eb__16_0(NetworkRunner _, NetworkObject no)
		{
		}

		internal void _003CUpdate_003Eb__16_1(NetworkRunner _, NetworkObject no)
		{
		}

		internal void _003CUpdate_003Eb__16_2(NetworkRunner _, NetworkObject no)
		{
		}
	}

	public static GameObject MolotovEntityPrefab;

	private FireType _type;

	private Stopwatch _disappearWatch = new Stopwatch();

	private Stopwatch _createFireWatch = new Stopwatch();

	private const float _molotovMoveSpeed = 1.25f;

	private const float _purifierStandardMoveSpeed = 1.75f;

	private const float _purifierBoostedMoveSpeed = 2.5f;

	private const int _molotovCreateFireDelayMilliseconds = 1000;

	private const int _purifierStandardCreateFireDelayMilliseconds = 750;

	private const int _purifierBoostedCreateFireDelayMilliseconds = 500;

	private const int _molotovEntityDurationMilliseconds = 6000;

	private const int _purifierStandardEntityDurationMilliseconds = 4000;

	private const int _purifierBoostedEntityDurationMilliseconds = 4500;

	private void Awake()
	{
		((Component)this).gameObject.layer = 25;
		_disappearWatch.Start();
		_createFireWatch.Start();
	}

	public void Init(FireType type)
	{
		_type = type;
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected O, but got Unknown
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Expected O, but got Unknown
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
		switch (_type)
		{
		case FireType.RegularMolotov:
			((Component)this).GetComponent<CharacterController>().Move(((Component)this).transform.forward * 1.25f * Time.deltaTime);
			if (!((Component)this).GetComponent<CharacterController>().isGrounded)
			{
				((Component)this).GetComponent<CharacterController>().Move(new Vector3(0f, -1f, 0f) * Time.deltaTime);
			}
			if (_createFireWatch.ElapsedMilliseconds >= 1000)
			{
				NetworkPrefabId networkObject3 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemMolotovFire");
				NetworkRunner runner3 = ((SimulationBehaviour)GameManager.Instance).Runner;
				Vector3? val10 = ((Component)this).transform.position;
				Quaternion? val11 = Quaternion.identity;
				object obj3 = _003C_003Ec._003C_003E9__16_0;
				if (obj3 == null)
				{
					OnBeforeSpawned val12 = delegate
					{
					};
					_003C_003Ec._003C_003E9__16_0 = val12;
					obj3 = (object)val12;
				}
				NetworkObject val13 = runner3.Spawn(networkObject3, val10, val11, (PlayerRef?)null, (OnBeforeSpawned)obj3, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				float num3 = Mathf.InverseLerp(0f, 6f, (float)_disappearWatch.ElapsedMilliseconds);
				float burnDuration3 = Mathf.Lerp(2f, 8f, num3);
				((Component)val13).GetComponent<MolotovFire>().Init(12000, burnDuration3);
				_createFireWatch.Restart();
			}
			if (_disappearWatch.ElapsedMilliseconds >= 6000)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
			break;
		case FireType.PurifierStandard:
			((Component)this).GetComponent<CharacterController>().Move(((Component)this).transform.forward * 1.75f * Time.deltaTime);
			if (!((Component)this).GetComponent<CharacterController>().isGrounded)
			{
				((Component)this).GetComponent<CharacterController>().Move(new Vector3(0f, -1f, 0f) * Time.deltaTime);
			}
			if (_createFireWatch.ElapsedMilliseconds >= 750)
			{
				NetworkPrefabId networkObject2 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemPurifierFire");
				NetworkRunner runner2 = ((SimulationBehaviour)GameManager.Instance).Runner;
				Vector3? val6 = ((Component)this).transform.position;
				Quaternion? val7 = Quaternion.identity;
				object obj2 = _003C_003Ec._003C_003E9__16_1;
				if (obj2 == null)
				{
					OnBeforeSpawned val8 = delegate
					{
					};
					_003C_003Ec._003C_003E9__16_1 = val8;
					obj2 = (object)val8;
				}
				NetworkObject val9 = runner2.Spawn(networkObject2, val6, val7, (PlayerRef?)null, (OnBeforeSpawned)obj2, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				float num2 = Mathf.InverseLerp(0f, 5f, (float)_disappearWatch.ElapsedMilliseconds);
				float burnDuration2 = Mathf.Lerp(3f, 7f, num2);
				((Component)val9).GetComponent<PurifierFire>().Init(8000, burnDuration2);
				_createFireWatch.Restart();
			}
			if (_disappearWatch.ElapsedMilliseconds >= 4000)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
			break;
		case FireType.PurifierBoosted:
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
				object obj = _003C_003Ec._003C_003E9__16_2;
				if (obj == null)
				{
					OnBeforeSpawned val4 = delegate
					{
					};
					_003C_003Ec._003C_003E9__16_2 = val4;
					obj = (object)val4;
				}
				NetworkObject val5 = runner.Spawn(networkObject, val2, val3, (PlayerRef?)null, (OnBeforeSpawned)obj, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				float num = Mathf.InverseLerp(0f, 5f, (float)_disappearWatch.ElapsedMilliseconds);
				float burnDuration = Mathf.Lerp(3f, 7f, num);
				((Component)val5).GetComponent<PurifierFire>().Init(12000, burnDuration);
				_createFireWatch.Restart();
			}
			if (_disappearWatch.ElapsedMilliseconds >= 4500)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
			break;
		}
	}
}
