using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using HarmonyLib;
using Helpers.Collections;
using LycansNewRoles.PowerObjects;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(3)]
public class MagicianIllusion : NetworkBehaviour
{
	private PlayerCustom _creatorCustom;

	private SkinnedMeshRenderer _villagerMeshRenderer;

	private Transform _hats;

	private Stopwatch _nextCheckWatch = new Stopwatch();

	private float _footstepTimer;

	private LayerMask _layerMask;

	private int _footstepSound;

	private bool _isPlayingFootstep;

	private bool _footstepRhythm;

	[Networked(OnChanged = "CreatorRefChanged")]
	[NetworkedWeaved(0, 1)]
	public unsafe PlayerRef CreatorRef
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)(*base.Ptr);
		}
		private set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.CreatorRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	[Networked]
	[NetworkedWeaved(1, 1)]
	public unsafe int RemainingDuration
	{
		get
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			return base.Ptr[1];
		}
		set
		{
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.RemainingDuration. Networked properties can only be accessed when Spawned() has been called.");
			}
			base.Ptr[1] = value;
		}
	}

	[Networked(OnChanged = "TargetRefChanged")]
	[NetworkedWeaved(2, 1)]
	public unsafe PlayerRef TargetRef
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.TargetRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (PlayerRef)base.Ptr[2];
		}
		private set
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing MagicianIllusion.TargetRef. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr + 2, value);
		}
	}

	private void Awake()
	{
		_villagerMeshRenderer = ((Component)((Component)this).transform.Find("Body").Find("Villager").Find("VillagerModel")).GetComponent<SkinnedMeshRenderer>();
		_hats = ((Component)this).transform.Find("Body").Find("Villager").Find("metarig")
			.Find("spine")
			.Find("spine.001")
			.Find("spine.002")
			.Find("spine.003")
			.Find("spine.004")
			.Find("spine.005")
			.Find("spine.006")
			.Find("HatsContainer")
			.Find("Hats");
	}

	private void Update()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		if (_nextCheckWatch.ElapsedMilliseconds >= 1000)
		{
			if (((SimulationBehaviour)this).Runner.IsServer)
			{
				RemainingDuration--;
				if (RemainingDuration <= 0)
				{
					((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
				}
			}
			_nextCheckWatch.Restart();
		}
		_footstepTimer += Time.deltaTime;
		if (_footstepTimer >= 0.1f)
		{
			_footstepTimer = 0f;
			Vector3 val = ((Component)this).transform.position + Vector3.up * 0.5f;
			RaycastHit val2 = default(RaycastHit);
			if (Physics.Raycast(new Ray(val, Vector3.down), ref val2, 1f, LayerMask.op_Implicit(_layerMask)))
			{
				if (((Component)((RaycastHit)(ref val2)).collider).CompareTag("FootstepStone"))
				{
					_footstepSound = 2;
					return;
				}
				if (((Component)((RaycastHit)(ref val2)).collider).CompareTag("FootstepWood"))
				{
					_footstepSound = 1;
					return;
				}
				_footstepSound = 0;
			}
		}
		PlayFootstepSound();
	}

	private void PlayFootstepSound()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		if (_isPlayingFootstep || !((Object)(object)PlayerController.Local != (Object)null))
		{
			return;
		}
		PlayerController povPlayer = PlayerController.Local.LocalCameraHandler.PovPlayer;
		float num = Vector3.Distance(((Component)povPlayer).transform.position, ((Component)this).transform.position);
		float num2 = 24f;
		float num3 = 0.4f;
		float time = 0.3f;
		if (num < num2 && (NetworkBool.op_Implicit(povPlayer.IsWolf) || povPlayer.Ref == CreatorRef))
		{
			string text = "FOOTSTEP";
			if (_footstepSound == 2)
			{
				text += "_STONE";
			}
			else if (_footstepSound == 1)
			{
				text += "_WOOD";
			}
			text = ((!_footstepRhythm) ? (text + "_2") : (text + "_1"));
			AudioManager.PlayAndFollow(text, ((Component)this).transform, (MixerTarget)2, num2, num3);
			_isPlayingFootstep = true;
			_footstepRhythm = !_footstepRhythm;
			((MonoBehaviour)this).StartCoroutine(ResetFootstepStatus(time));
		}
	}

	private IEnumerator ResetFootstepStatus(float time)
	{
		yield return (object)new WaitForSeconds(time);
		_isPlayingFootstep = false;
	}

	public void SetCreatorRef(PlayerRef playerRef)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CreatorRef = playerRef;
	}

	public void Init(int duration)
	{
		RemainingDuration = duration;
	}

	[Preserve]
	public static void CreatorRefChanged(Changed<MagicianIllusion> changed)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			changed.Behaviour._creatorCustom = PlayerCustomRegistry.GetPlayer(changed.Behaviour.CreatorRef);
			((Component)changed.Behaviour).gameObject.layer = 25;
			((Component)((Component)changed.Behaviour).transform.Find("Body")).gameObject.layer = 25;
			((Component)((Component)changed.Behaviour).transform.Find("Body").Find("Villager")).gameObject.layer = 25;
			changed.Behaviour._layerMask = Traverse.Create((object)((Component)changed.Behaviour._creatorCustom.PlayerController).GetComponent<PlayerGroundDetection>()).Field<LayerMask>("layerMask").Value;
			PlayerController val = CollectionsUtil.Grab<PlayerController>(PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead) && (int)o.Role != 1)).ToList(), 1).First();
			changed.Behaviour.TargetRef = val.Ref;
			changed.Behaviour._nextCheckWatch.Restart();
			changed.Behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("CreatorRefChanged error: " + ex));
		}
	}

	[Preserve]
	public static void TargetRefChanged(Changed<MagicianIllusion> changed)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(changed.Behaviour.TargetRef);
			((Renderer)changed.Behaviour._villagerMeshRenderer).material.mainTexture = ColorManager.GetTexture(player.ColorIndex);
			int childCount = changed.Behaviour._hats.childCount;
			foreach (object item in ((Component)changed.Behaviour._hats).transform)
			{
				((Component)(Transform)item).gameObject.SetActive(false);
			}
			int num = (int)Traverse.Create((object)player.PlayerController).Property("HatIndex", (object[])null).GetValue();
			if (num >= 0 && num < childCount)
			{
				((Component)((Component)changed.Behaviour._hats).transform.GetChild(num)).gameObject.SetActive(true);
				((Component)changed.Behaviour._hats).gameObject.SetActive(true);
				((Component)changed.Behaviour._hats.parent).gameObject.SetActive(true);
			}
			changed.Behaviour.UpdateVisibility();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TargetRefChanged error: " + ex));
		}
	}

	private void UpdateVisibility()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		((Renderer)_villagerMeshRenderer).enabled = _creatorCustom.IsCurrentlyPlayedOrObserved || (NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.IsWolf) && !NetworkBool.op_Implicit(PlayerController.Local.LocalCameraHandler.PovPlayer.PlayerEffectManager.NightVision));
	}

	public static void UpdateVisibilityForAllMagicianIllusions()
	{
		MagicianIllusion[] array = Object.FindObjectsOfType<MagicianIllusion>();
		MagicianIllusion[] array2 = array;
		foreach (MagicianIllusion magicianIllusion in array2)
		{
			magicianIllusion.UpdateVisibility();
		}
	}

	public override void FixedUpdateNetwork()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Invalid comparison between Unknown and I4
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Invalid comparison between Unknown and I4
		if (((SimulationBehaviour)this).HasStateAuthority)
		{
			bool flag = false;
			EGameState localGameState = GameManager.LocalGameState;
			EGameState val = localGameState;
			if ((int)val <= 1 || val - 3 <= 2)
			{
				flag = true;
			}
			if (flag)
			{
				((SimulationBehaviour)this).Runner.Despawn(((Component)this).GetComponent<NetworkObject>(), false);
			}
		}
		((NetworkCharacterControllerPrototypeCustom)((Component)this).GetComponent<PlayerIllusionNetworkCharacterController>()).Move(((Component)this).transform.forward, 3f);
		((Component)((Component)this).transform.Find("Body").Find("Villager")).GetComponent<Animator>().SetFloat(Animator.StringToHash("X_Velocity"), 0f);
		((Component)((Component)this).transform.Find("Body").Find("Villager")).GetComponent<Animator>().SetFloat(Animator.StringToHash("Y_Velocity"), 5f);
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (((Renderer)_villagerMeshRenderer).enabled)
		{
			GameObject val = Object.Instantiate<GameObject>(DiscipleAnchor.ActivationParticleSystemPrefab, ((Component)this).transform.position, Quaternion.identity);
			val.SetActive(true);
			SelfDestroyingObjectComponent selfDestroyingObjectComponent = val.AddComponent<SelfDestroyingObjectComponent>();
			selfDestroyingObjectComponent.Init(2f);
		}
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
