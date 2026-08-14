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
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Invalid comparison between Unknown and I4
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f9: Invalid comparison between Unknown and I4
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_0900: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_092b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Invalid comparison between Unknown and I4
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_0963: Unknown result type (might be due to invalid IL or missing references)
		//IL_0967: Unknown result type (might be due to invalid IL or missing references)
		//IL_096c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0991: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0e: Unknown result type (might be due to invalid IL or missing references)
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
					goto IL_073b;
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
		}
		goto IL_073b;
		IL_073b:
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
		CustomMap customMap3 = MapManager.NewMapsByIdInfo[GameManager.Instance.MapID];
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
		val2 -= customMap3.MinimapCameraOffset;
		Vector2 val5 = default(Vector2);
		((Vector2)(ref val5))._002Ector(0f - val2.z, val2.x);
		Quaternion val6 = default(Quaternion);
		((Quaternion)(ref val6))._002Ector(((Component)this).transform.localRotation.x, ((Component)this).transform.localRotation.y, val4.y, 0f - val4.w);
		float minimapRotation = customMap3.MinimapRotation;
		float num = minimapRotation;
		if (num == 270f)
		{
			((Vector2)(ref val5))._002Ector(val2.x, val2.z);
			val6 *= Quaternion.Euler(0f, 0f, 270f);
		}
		val5 *= customMap3.MinimapOffsetMultiplier;
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
