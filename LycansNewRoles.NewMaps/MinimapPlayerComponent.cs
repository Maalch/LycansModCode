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
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Invalid comparison between Unknown and I4
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Invalid comparison between Unknown and I4
		//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Invalid comparison between Unknown and I4
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0925: Invalid comparison between Unknown and I4
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0960: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b27: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
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
		else if (povPlayerCustom.Accessory is AccessoryCrystalBall && NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) && Vector3.Distance(((Component)povPlayerCustom.PlayerController).transform.position, ((Component)AssociatedPlayerCustom.PlayerController).transform.position) <= 25f * BalancingValues.DistanceMultiplierByMap(GameManager.Instance.MapID))
		{
			flag = true;
		}
		else if ((int)povPlayerCustom.PlayerController.Role == 1 && NetworkBool.op_Implicit(AssociatedPlayerCustom.PoacherMark))
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
					goto IL_0770;
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
		goto IL_0770;
		IL_0770:
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
		Color val7 = (((Object)(object)AssociatedPlayerCustom == (Object)(object)povPlayerCustom) ? PlayerColorSelf : (NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsDead) ? PlayerColorCorpse : ((povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && AssociatedPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Lover && !NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf)) ? PlayerColorLover : ((Plugin.Minimap.State == MinimapComponent.MinimapState.Admin) ? PlayerColorHumanForm : ((povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Spy && povPlayerCustom.PrimaryRoleTargetRef == AssociatedPlayerCustom.Ref) ? PlayerColorTarget : ((povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Mercenary && povPlayerCustom.PrimaryRoleTargetRef == AssociatedPlayerCustom.Ref) ? PlayerColorTarget : (NetworkBool.op_Implicit(povPlayerCustom.Clairvoyance) ? (NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) ? PlayerColorWolfForm : PlayerColorHumanForm) : ((!NetworkBool.op_Implicit(AssociatedPlayerCustom.PlayerController.IsWolf) && !NetworkBool.op_Implicit(povPlayerCustom.PlayerController.PlayerEffectManager.Paranoia)) ? PlayerColorHumanForm : PlayerColorWolfForm))))))));
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
