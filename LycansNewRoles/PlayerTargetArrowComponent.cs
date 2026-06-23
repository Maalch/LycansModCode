using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using LycansNewRoles.PowerObjects;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerTargetArrowComponent : MonoBehaviour
{
	public static GameObject PlayerTargetArrowPrefab;

	private GameObject _arrowObject;

	private PlayerController _playerController;

	private PlayerCustom _playerCustom;

	private bool _active = false;

	private GameObject _visual;

	private GameObject _red;

	private GameObject _blue;

	private GameObject _yellow;

	public void Init(PlayerController playerController, PlayerCustom playerCustom)
	{
		_playerController = playerController;
		_playerCustom = playerCustom;
		_arrowObject = Object.Instantiate<GameObject>(PlayerTargetArrowPrefab, ((Component)_playerController).transform);
		_visual = ((Component)_arrowObject.transform.Find("Visual")).gameObject;
		_red = ((Component)_visual.transform.Find("Red")).gameObject;
		_blue = ((Component)_visual.transform.Find("Blue")).gameObject;
		_yellow = ((Component)_visual.transform.Find("Yellow")).gameObject;
	}

	public void UpdateState(bool isObservedOrPlayed)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		PlayerCustom playerCustom = PlayerCustomRegistry.GetPlayer(_playerController.Ref);
		bool flag = false;
		GameObject colorToShow = null;
		if (isObservedOrPlayed)
		{
			switch (playerCustom.PrimaryRolePower)
			{
			case PlayerCustom.PlayerPrimaryRolePower.Investigator:
				flag = InvestigatorHint.AllHints.Any((InvestigatorHint o) => o.CreatorRef == playerCustom.Ref);
				colorToShow = _blue;
				break;
			case PlayerCustom.PlayerPrimaryRolePower.Scout:
				flag = ScoutRadar.AssociatedRadars.Any((ScoutRadar o) => o.WolvesInRange.Count > 0);
				colorToShow = _red;
				break;
			}
			switch (playerCustom.NewPrimaryRole)
			{
			case PlayerCustom.PlayerNewPrimaryRole.Spy:
			case PlayerCustom.PlayerNewPrimaryRole.Mercenary:
			{
				PlayerRef primaryRoleTargetRef = playerCustom.PrimaryRoleTargetRef;
				flag = !((PlayerRef)(ref primaryRoleTargetRef)).IsNone;
				colorToShow = _red;
				break;
			}
			case PlayerCustom.PlayerNewPrimaryRole.Scientist:
				flag = PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead)));
				colorToShow = _red;
				break;
			}
			PlayerCustom.PlayerSecondaryRole secondaryRole = playerCustom.SecondaryRole;
			PlayerCustom.PlayerSecondaryRole playerSecondaryRole = secondaryRole;
			if (playerSecondaryRole == PlayerCustom.PlayerSecondaryRole.BothMerchant)
			{
				flag = MerchantCoin.AllCoins.Any((MerchantCoin o) => o.CreatorRef == playerCustom.Ref) && !NetworkBool.op_Implicit(playerCustom.PlayerController.IsWolf);
				colorToShow = _yellow;
			}
			if (NetworkBool.op_Implicit(playerCustom.Repulsion))
			{
				flag = true;
				colorToShow = _red;
			}
		}
		if (flag != _active)
		{
			SetActive(flag, colorToShow);
		}
	}

	public void SetActive(bool active, GameObject _colorToShow)
	{
		_active = active;
		_arrowObject.SetActive(_active);
		if (active)
		{
			_arrowObject.transform.SetParent(_playerController.GetCameraAnchorPoint().parent, false);
			_red.SetActive(false);
			_blue.SetActive(false);
			_yellow.SetActive(false);
			_colorToShow.SetActive(true);
		}
	}

	private void Update()
	{
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		if (!_active)
		{
			return;
		}
		bool flag = false;
		switch (_playerCustom.PrimaryRolePower)
		{
		case PlayerCustom.PlayerPrimaryRolePower.Investigator:
		{
			flag = true;
			InvestigatorHint investigatorHint = (from o in InvestigatorHint.AllHints
				where o.CreatorRef == _playerCustom.Ref
				orderby Vector3.Distance(((Component)o).transform.position, ((Component)_playerCustom.PlayerController).transform.position)
				select o).FirstOrDefault();
			if ((Object)(object)investigatorHint != (Object)null)
			{
				_visual.transform.LookAt(((Component)investigatorHint).transform);
			}
			else
			{
				SetActive(active: false, null);
			}
			break;
		}
		case PlayerCustom.PlayerPrimaryRolePower.Scout:
		{
			flag = true;
			List<PlayerRef> source = ScoutRadar.AssociatedRadars.SelectMany((ScoutRadar o) => o.WolvesInRange).ToList();
			if (source.Any())
			{
				PlayerController val = (from o in ((IEnumerable<PlayerRef>)source).Select((Func<PlayerRef, PlayerController>)PlayerRegistry.GetPlayer)
					orderby Vector3.Distance(((Component)_playerController).transform.position, ((Component)o).transform.position)
					select o).First();
				_visual.transform.LookAt(((Component)val).transform);
			}
			else
			{
				SetActive(active: false, null);
			}
			break;
		}
		}
		switch (_playerCustom.NewPrimaryRole)
		{
		case PlayerCustom.PlayerNewPrimaryRole.Spy:
		case PlayerCustom.PlayerNewPrimaryRole.Mercenary:
		{
			flag = true;
			PlayerRef primaryRoleTargetRef = _playerCustom.PrimaryRoleTargetRef;
			if (!((PlayerRef)(ref primaryRoleTargetRef)).IsNone)
			{
				PlayerController player = PlayerRegistry.GetPlayer(_playerCustom.PrimaryRoleTargetRef);
				_visual.transform.LookAt(((Component)player).transform.position);
			}
			else
			{
				SetActive(active: false, null);
			}
			break;
		}
		case PlayerCustom.PlayerNewPrimaryRole.Scientist:
		{
			flag = true;
			PlayerController val2 = (from o in PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead)))
				orderby Vector3.Distance(((Component)o).transform.position, ((Component)this).transform.position)
				select o).FirstOrDefault();
			if ((Object)(object)val2 != (Object)null)
			{
				_visual.transform.LookAt(((Component)val2).transform);
			}
			else
			{
				SetActive(active: false, null);
			}
			break;
		}
		}
		PlayerCustom.PlayerSecondaryRole secondaryRole = _playerCustom.SecondaryRole;
		PlayerCustom.PlayerSecondaryRole playerSecondaryRole = secondaryRole;
		if (playerSecondaryRole == PlayerCustom.PlayerSecondaryRole.BothMerchant)
		{
			flag = true;
			MerchantCoin merchantCoin = (from o in MerchantCoin.AllCoins
				where o.CreatorRef == _playerCustom.Ref
				orderby Vector3.Distance(((Component)o).transform.position, ((Component)_playerCustom.PlayerController).transform.position)
				select o).FirstOrDefault();
			if ((Object)(object)merchantCoin != (Object)null)
			{
				_visual.transform.LookAt(((Component)merchantCoin).transform);
			}
			else
			{
				SetActive(active: false, null);
			}
		}
		if (NetworkBool.op_Implicit(_playerCustom.Repulsion))
		{
			flag = true;
			MysticRepulsor mysticRepulsor = MysticRepulsor.AllRepulsors.OrderBy((MysticRepulsor o) => Vector3.Distance(((Component)o).transform.position, ((Component)_playerCustom.PlayerController).transform.position)).FirstOrDefault();
			if ((Object)(object)mysticRepulsor != (Object)null)
			{
				_visual.transform.LookAt(((Component)mysticRepulsor).transform);
			}
			else
			{
				SetActive(active: false, null);
			}
		}
		if (!flag)
		{
			UpdateState(isObservedOrPlayed: false);
		}
	}
}
