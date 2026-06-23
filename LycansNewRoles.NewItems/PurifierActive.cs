using System;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;

namespace LycansNewRoles.NewItems;

[NetworkBehaviourWeaved(1)]
public class PurifierActive : NetworkBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static OnBeforeSpawned _003C_003E9__7_0;

		internal void _003COnCollisionEnter_003Eb__7_0(NetworkRunner _, NetworkObject no)
		{
		}
	}

	public static GameObject PurifierExplosionParticleSystemPrefab;

	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = ((Component)this).GetComponent<Rigidbody>();
	}

	public void Init(Vector3 velocity)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).GetComponent<Rigidbody>().velocity = velocity;
	}

	public override void Spawned()
	{
		((NetworkBehaviour)this).Spawned();
		((Component)this).gameObject.layer = 26;
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		((NetworkBehaviour)this).Despawned(runner, hasState);
		GameObject val = Object.Instantiate<GameObject>(PlayerCustom.BombExplosionParticleSystemPrefab, ((Component)this).transform.position, Quaternion.identity);
		val.SetActive(true);
		SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
		MainModule main = val.GetComponent<ParticleSystem>().main;
		selfDestroyingObjectComponent.Init(((MainModule)(ref main)).duration + 2f);
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).HasStateAuthority)
		{
			return;
		}
		if ((int)GameManager.LocalGameState != 2)
		{
			if (((SimulationBehaviour)this).HasStateAuthority)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
			return;
		}
		Vector3 velocity = ((Component)this).GetComponent<Rigidbody>().velocity;
		velocity.x *= 1f - 0.5f * ((SimulationBehaviour)this).Runner.DeltaTime;
		velocity.z *= 1f - 0.5f * ((SimulationBehaviour)this).Runner.DeltaTime;
		_rigidbody.velocity = velocity;
		float num = Mathf.Abs(_rigidbody.velocity.x) + Mathf.Abs(_rigidbody.velocity.z);
		if (num >= 1f)
		{
			((Component)this).transform.Rotate(0f, 0f, Time.deltaTime * num * 10f);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("SleepingGasBreak"), ((Component)this).transform.position, 20f, 0.4f);
			GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("InquisitorFire"), ((Component)this).transform.position, 20f, 0.4f);
			for (int i = 0; i < 16; i++)
			{
				GameObject val = Object.Instantiate<GameObject>(MolotovEntity.MolotovEntityPrefab, ((Component)this).transform.position, Quaternion.Euler(0f, (float)i * 22.5f, 0f), (Transform)null);
				val.SetActive(true);
				val.GetComponent<MolotovEntity>().Init(purifierEntity: true);
			}
			NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.ItemPurifierFire");
			NetworkRunner runner = ((SimulationBehaviour)GameManager.Instance).Runner;
			Vector3? val2 = ((Component)this).transform.position;
			Quaternion? val3 = Quaternion.identity;
			object obj = _003C_003Ec._003C_003E9__7_0;
			if (obj == null)
			{
				OnBeforeSpawned val4 = delegate
				{
				};
				_003C_003Ec._003C_003E9__7_0 = val4;
				obj = (object)val4;
			}
			NetworkObject val5 = runner.Spawn(networkObject, val2, val3, (PlayerRef?)null, (OnBeforeSpawned)obj, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			((Component)val5).GetComponent<PurifierFire>().Init(9000, 3f);
			((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
		}
	}
}
