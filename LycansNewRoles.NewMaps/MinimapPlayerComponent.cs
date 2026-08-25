using System.Linq;
using Fusion;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.PowerObjects;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles.NewMaps;

public class MinimapPlayerComponent : MonoBehaviour
{
	private static Color PlayerColorSelf = Color.green;

	private static Color PlayerColorHumanForm = Color.blue;

	private static Color PlayerColorWolfForm = Color.red;

	private static Color PlayerColorPlayer = new Color(1f, 0f, 1f);

	private static Color PlayerColorTarget = new Color(1f, 0.5f, 0f);

	private static Color PlayerColorLover = new Color(1f, 0.5f, 1f);

	private static Color PlayerColorCorpse = new Color(0.65f, 0.15f, 1f, 1f);

	public static GameObject MinimapPlayerPrefab;

	public PlayerCustom AssociatedPlayerCustom;

	private bool _active;

	private Image _imagePlayer;

	private Image _imageBomb;

	private GameObject _objectDirection;

	private Image _imageUp;

	private Image _imageDown;

	private MinimapClairvoyanceRadiusComponent _clairvoyanceRadius;

	private MinimapShadowAuraRadiusComponent _shadowRadius;

	private MinimapTrackerRadiusComponent _trackerRadius;

	public void Init(PlayerCustom associatedPlayerCustom)
	{
		AssociatedPlayerCustom = associatedPlayerCustom;
		_imagePlayer = ((Component)this).GetComponent<Image>();
		((Behaviour)_imagePlayer).enabled = false;
		_objectDirection = ((Component)((Component)this).transform.Find("MinimapPlayerDirection")).gameObject;
		_objectDirection.SetActive(false);
		_imageUp = ((Component)((Component)this).transform.Find("MinimapUp")).gameObject.GetComponent<Image>();
		((Behaviour)_imageUp).enabled = false;
		_imageDown = ((Component)((Component)this).transform.Find("MinimapDown")).gameObject.GetComponent<Image>();
		((Behaviour)_imageDown).enabled = false;
		_imageBomb = ((Component)((Component)this).transform.Find("MinimapBomb")).gameObject.GetComponent<Image>();
		((Behaviour)_imageBomb).enabled = false;
		_clairvoyanceRadius = ((Component)((Component)this).transform.Find("MinimapClairvoyanceRadius")).gameObject.AddComponent<MinimapClairvoyanceRadiusComponent>();
		((Component)_clairvoyanceRadius).gameObject.SetActive(false);
		_shadowRadius = ((Component)((Component)this).transform.Find("MinimapShadowAuraRadius")).gameObject.AddComponent<MinimapShadowAuraRadiusComponent>();
		((Component)_shadowRadius).gameObject.SetActive(false);
		_trackerRadius = ((Component)((Component)this).transform.Find("MinimapTrackerRadius")).gameObject.AddComponent<MinimapTrackerRadiusComponent>();
		((Component)_trackerRadius).gameObject.SetActive(false);
		_active = false;
	}

	public void UpdateIcon()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Invalid comparison between Unknown and I4
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0c: Invalid comparison between Unknown and I4
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Invalid comparison between Unknown and I4
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerController.Local == (Object)null || (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer == (Object)null || (Object)(object)PlayerController.Local.LocalCameraHandler == (Object)null || (Object)(object)Plugin.Minimap == (Object)null)
		{
			return;
		}
		if (Plugin.Minimap.State == MinimapComponent.MinimapState.Inactive)
		{
			if (_active)
			{
				SetActive(active: false, showDirection: false);
			}
			return;
		}
		bool flag = false;
		PlayerCustom povPlayerCustom = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (AssociatedPlayerCustom.Ref == povPlayerCustom.Ref && NetworkBool.op_Implicit(povPlayerCustom.Clairvoyance))
		{
			if (!((Component)_clairvoyanceRadius).gameObject.activeSelf)
			{
				((Component)_clairvoyanceRadius).gameObject.SetActive(true);
				CustomMap customMap = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
				float radiusScale = customMap.MinimapOffsetMultiplier / 5.45f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID);
				_clairvoyanceRadius.Init(radiusScale);
			}
		}
		else
		{
			((Component)_clairvoyanceRadius).gameObject.SetActive(false);
		}
		if (AssociatedPlayerCustom.Ref == povPlayerCustom.Ref && povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Shadow)
		{
			if (!((Component)_shadowRadius).gameObject.activeSelf)
			{
				((Component)_shadowRadius).gameObject.SetActive(true);
				CustomMap customMap2 = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
				float radiusScale2 = customMap2.MinimapOffsetMultiplier / 5.45f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
				_shadowRadius.Init(radiusScale2);
			}
		}
		else
		{
			((Component)_shadowRadius).gameObject.SetActive(false);
		}
		if (NetworkBool.op_Implicit(AssociatedPlayerCustom.Tracked) && povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Tracker && !NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf))
		{
			if (!((Component)_trackerRadius).gameObject.activeSelf)
			{
				((Component)_trackerRadius).gameObject.SetActive(true);
				CustomMap customMap3 = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
				float radiusScale3 = customMap3.MinimapOffsetMultiplier / 5.45f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID);
				_trackerRadius.Init(radiusScale3);
			}
		}
		else
		{
			((Component)_trackerRadius).gameObject.SetActive(false);
		}
		if (NetworkBool.op_Implicit(povPlayerCustom.Clairvoyance) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)AssociatedPlayerCustom.PlayerController).transform.position) <= 40f * BalancingValues.ScoutRadarRadiusMultiplierByMap(GameManager.Instance.MapID))
		{
			flag = true;
		}
		else if ((Object)(object)AssociatedPlayerCustom == (Object)(object)povPlayerCustom)
		{
			flag = true;
		}
		else if (Plugin.Minimap.State == MinimapComponent.MinimapState.Admin && !NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf))
		{
			flag = true;
		}
		else if (povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Avatar && NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)AssociatedPlayerCustom.PlayerController).transform.position) <= 40f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
		{
			flag = true;
		}
		else if (NetworkBool.op_Implicit(povPlayerCustom.Angel) && NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)AssociatedPlayerCustom.PlayerController).transform.position) <= 40f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
		{
			flag = true;
		}
		else if (povPlayerCustom.Accessory is AccessoryCrystalBall && NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)AssociatedPlayerCustom.PlayerController).transform.position) <= 20f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
		{
			flag = true;
		}
		else if (povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Scientist && NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf))
		{
			flag = true;
		}
		else if (povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary && povPlayerCustom.PrimaryRoleTargetRef == AssociatedPlayerCustom.Ref)
		{
			flag = true;
		}
		else if (povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Spy && AssociatedPlayerCustom.Ref == povPlayerCustom.PrimaryRoleTargetRef)
		{
			flag = true;
		}
		else if (povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && AssociatedPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && !NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf))
		{
			flag = true;
		}
		else if (NetworkBool.op_Implicit(AssociatedPlayerCustom.Exorcised) && AssociatedPlayerCustom.Exorciser == povPlayerCustom.Ref)
		{
			flag = true;
		}
		else if (NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) && povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Scout && ScoutRadar.AssociatedRadars.Any((ScoutRadar o) => o.CreatorRef == povPlayerCustom.Ref && o.WolvesInRange.Any((PlayerRef j) => j == AssociatedPlayerCustom.Ref)))
		{
			flag = true;
		}
		else
		{
			if (NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf))
			{
				TickTimer curseTimer = AssociatedPlayerCustom.CurseTimer;
				if (((TickTimer)(ref curseTimer)).IsRunning && povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Warlock)
				{
					flag = true;
					goto IL_084e;
				}
			}
			if (povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Spotter && NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)AssociatedPlayerCustom.PlayerController).transform.position) <= (NetworkBool.op_Implicit(povPlayerCustom.Spotter) ? 45f : 25f) * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
			{
				flag = true;
			}
			else if (povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Host && NetworkBool.op_Implicit(AssociatedPlayerCustom.Parasite) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)AssociatedPlayerCustom.PlayerController).transform.position) <= 20f)
			{
				flag = true;
			}
			else if (povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Tracker && !NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf) && PlayerCustomRegistry.Any((PlayerCustom o) => NetworkBool.op_Implicit(o.Tracked) && Vector3.Distance(((Component)o.PlayerController).transform.position, ((Component)AssociatedPlayerCustom.PlayerController).transform.position) <= 15f))
			{
				flag = true;
			}
		}
		goto IL_084e;
		IL_084e:
		if (NetworkBool.op_Implicit(AssociatedPlayerCustom.BombDormant) && !NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsDead) && ((int)povPlayerCustom.PlayerController.Role == 1 || povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor))
		{
			flag = true;
			((Behaviour)_imageBomb).enabled = true;
		}
		else
		{
			((Behaviour)_imageBomb).enabled = false;
		}
		if (flag && (Object)(object)AssociatedPlayerCustom != (Object)(object)povPlayerCustom && (NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsDead) || NetworkBool.op_Implicit(AssociatedPlayerCustom.Phasing) || NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.PlayerEffectManager.Invisible) || NetworkBool.op_Implicit(AssociatedPlayerCustom.Sneaky)))
		{
			flag = false;
		}
		if (povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Voodoo && !NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsDead) && AssociatedPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Zombie)
		{
			flag = true;
		}
		if (flag && !_active)
		{
			SetActive(active: true, AssociatedPlayerCustom.Ref == povPlayerCustom.Ref);
		}
		else if (_active && !flag)
		{
			SetActive(active: false, showDirection: false);
		}
		if (!flag)
		{
			return;
		}
		CustomMap customMap4 = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
		Vector3 val;
		if ((int)GameManager.LocalGameState == 4)
		{
			_ = Plugin.Minimap.PlayerPositionBeforeMeeting;
			val = Plugin.Minimap.PlayerPositionBeforeMeeting;
		}
		else
		{
			val = ((Component)AssociatedPlayerCustom.PlayerController).transform.position;
		}
		Vector3 val2 = val;
		Quaternion val3;
		if ((int)GameManager.LocalGameState == 4)
		{
			_ = Plugin.Minimap.PlayerRotationBeforeMeeting;
			val3 = Plugin.Minimap.PlayerRotationBeforeMeeting;
		}
		else
		{
			val3 = ((Component)AssociatedPlayerCustom.PlayerController).transform.rotation;
		}
		Quaternion val4 = val3;
		val2 -= customMap4.MinimapCameraOffset;
		Vector2 val5 = default(Vector2);
		((Vector2)(ref val5))._002Ector(0f - val2.z, val2.x);
		Quaternion val6 = default(Quaternion);
		((Quaternion)(ref val6))._002Ector(((Component)this).transform.localRotation.x, ((Component)this).transform.localRotation.y, val4.y, 0f - val4.w);
		float minimapRotation = customMap4.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val5))._002Ector(val2.x, val2.z);
			val6 *= Quaternion.Euler(0f, 0f, 270f);
		}
		val5 *= customMap4.MinimapOffsetMultiplier;
		((Component)this).transform.localPosition = Vector2.op_Implicit(val5);
		_objectDirection.transform.localRotation = val6;
		Color val7 = (((Object)(object)AssociatedPlayerCustom == (Object)(object)povPlayerCustom) ? PlayerColorSelf : ((!NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsDead) && AssociatedPlayerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Zombie) ? ((povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && AssociatedPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && !NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf)) ? PlayerColorLover : ((Plugin.Minimap.State == MinimapComponent.MinimapState.Admin) ? PlayerColorHumanForm : ((povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Spy && povPlayerCustom.PrimaryRoleTargetRef == AssociatedPlayerCustom.Ref) ? PlayerColorTarget : ((povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary && povPlayerCustom.PrimaryRoleTargetRef == AssociatedPlayerCustom.Ref) ? PlayerColorTarget : (NetworkBool.op_Implicit(povPlayerCustom.Clairvoyance) ? (NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) ? PlayerColorWolfForm : PlayerColorHumanForm) : ((!NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) && !NetworkBool.op_Implicit(povPlayerCustom.PlayerController.PlayerEffectManager.Paranoia)) ? PlayerColorHumanForm : PlayerColorWolfForm)))))) : PlayerColorCorpse));
		if (((Graphic)_imagePlayer).color != val7)
		{
			((Graphic)_imagePlayer).color = val7;
		}
		if (BalancingValues.ShowMinimapArrowsOnMap(GameManager.Instance.MapID))
		{
			if (((Component)AssociatedPlayerCustom.PlayerController).transform.position.y > ((Component)povPlayerCustom.PlayerController).transform.position.y + 3.5f)
			{
				((Behaviour)_imageUp).enabled = true;
				((Behaviour)_imageDown).enabled = false;
			}
			else if (((Component)AssociatedPlayerCustom.PlayerController).transform.position.y < ((Component)povPlayerCustom.PlayerController).transform.position.y - 3.5f)
			{
				((Behaviour)_imageUp).enabled = false;
				((Behaviour)_imageDown).enabled = true;
			}
			else
			{
				((Behaviour)_imageUp).enabled = false;
				((Behaviour)_imageDown).enabled = false;
			}
		}
	}

	public void SetActive(bool active, bool showDirection)
	{
		if (!_active && active)
		{
			((Component)this).transform.SetParent(((Component)Plugin.Minimap).transform.Find("Panel"));
			((Component)this).transform.SetAsLastSibling();
		}
		_active = active;
		((Behaviour)_imagePlayer).enabled = active;
		_objectDirection.SetActive(showDirection);
		if (!active)
		{
			((Behaviour)_imageDown).enabled = false;
			((Behaviour)_imageUp).enabled = false;
		}
	}
}
