using System;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LycansNewRoles;

public class DraftChoiceRoleComponent : MonoBehaviour
{
	public enum DraftChoiceRoleType
	{
		SoloRole,
		Power,
		SecondaryRole
	}

	public bool Available = true;

	public bool Selected = false;

	private GameObject _button;

	private TextMeshProUGUI _description;

	private DraftChoiceRoleType _type;

	private int _roleIndex;

	public GameObject Button => _button;

	public TextMeshProUGUI Description => _description;

	public DraftChoiceRoleType Type => _type;

	public int RoleIndex => _roleIndex;

	public void Init(DraftChoiceRoleType type, int index, string suffix)
	{
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		_button = ((Component)((Component)this).transform.Find("RoleChoiceButton")).gameObject;
		_description = ((Component)((Component)this).transform.Find("RoleChoiceDescription")).GetComponent<TextMeshProUGUI>();
		switch (type)
		{
		case DraftChoiceRoleType.SoloRole:
			((TMP_Text)_button.GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetNewPrimaryRoleString((PlayerCustom.PlayerNewPrimaryRole)index));
			break;
		case DraftChoiceRoleType.Power:
			((TMP_Text)_button.GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetPrimaryRolePowerString((PlayerCustom.PlayerPrimaryRolePower)index));
			break;
		case DraftChoiceRoleType.SecondaryRole:
			((TMP_Text)_button.GetComponentInChildren<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_ROLE_" + PlayerCustom.GetSecondaryRoleString((PlayerCustom.PlayerSecondaryRole)index));
			break;
		}
		((TMP_Text)_description).text = ((suffix != null) ? TranslationManager.Instance.GetTranslation("NALES_DRAFT_ROLE_DESCRIPTION_" + suffix) : "");
		_type = type;
		_roleIndex = index;
		((UnityEvent)_button.GetComponent<Button>().onClick).AddListener(new UnityAction(Choose));
	}

	public void SetAvailable(bool available)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		Available = available;
		((Behaviour)_button.GetComponent<Button>()).enabled = available;
		if (Selected)
		{
			((Graphic)_button.GetComponent<Image>()).color = Color.green;
		}
		else
		{
			((Graphic)_button.GetComponent<Image>()).color = (Available ? Color.white : Color.red);
		}
	}

	public void Choose()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		foreach (DraftChoiceRoleComponent item in UIManager.DraftPanel.CurrentRoles.Where((DraftChoiceRoleComponent o) => o.Type == Type))
		{
			((Graphic)item.Button.GetComponent<Image>()).color = (item.Available ? Color.white : Color.red);
			item.Selected = false;
		}
		((Graphic)_button.GetComponent<Image>()).color = Color.green;
		Selected = true;
		switch (_type)
		{
		case DraftChoiceRoleType.SoloRole:
		case DraftChoiceRoleType.Power:
		{
			if (_type == DraftChoiceRoleType.SoloRole)
			{
				DraftManager.Instance.MyData.SelectedSoloRole = (PlayerCustom.PlayerNewPrimaryRole)_roleIndex;
			}
			else
			{
				DraftManager.Instance.MyData.SelectedPower = (PlayerCustom.PlayerPrimaryRolePower)_roleIndex;
			}
			PlayerRole basePrimaryRole;
			PlayerCustom.PlayerNewPrimaryRole newPrimaryRole;
			PlayerCustom.PlayerPrimaryRolePower playerPrimaryRolePower;
			switch (DraftManager.Instance.MyData.MainRole)
			{
			case DraftManager.DraftPlayerMainRole.NormalVillager:
			case DraftManager.DraftPlayerMainRole.EliteVillager:
				basePrimaryRole = (PlayerRole)0;
				newPrimaryRole = PlayerCustom.PlayerNewPrimaryRole.None;
				playerPrimaryRolePower = (PlayerCustom.PlayerPrimaryRolePower)RoleIndex;
				switch (playerPrimaryRolePower)
				{
				case PlayerCustom.PlayerPrimaryRolePower.Hunter:
					basePrimaryRole = (PlayerRole)2;
					break;
				case PlayerCustom.PlayerPrimaryRolePower.Alchemist:
					basePrimaryRole = (PlayerRole)3;
					break;
				}
				break;
			case DraftManager.DraftPlayerMainRole.Solo:
				basePrimaryRole = (PlayerRole)0;
				newPrimaryRole = (PlayerCustom.PlayerNewPrimaryRole)RoleIndex;
				playerPrimaryRolePower = PlayerCustom.PlayerPrimaryRolePower.None;
				break;
			case DraftManager.DraftPlayerMainRole.Traitor:
				basePrimaryRole = (PlayerRole)0;
				newPrimaryRole = PlayerCustom.PlayerNewPrimaryRole.Traitor;
				playerPrimaryRolePower = (PlayerCustom.PlayerPrimaryRolePower)RoleIndex;
				break;
			case DraftManager.DraftPlayerMainRole.WolfPup:
				basePrimaryRole = (PlayerRole)1;
				newPrimaryRole = PlayerCustom.PlayerNewPrimaryRole.None;
				playerPrimaryRolePower = (PlayerCustom.PlayerPrimaryRolePower)RoleIndex;
				break;
			case DraftManager.DraftPlayerMainRole.Wolf:
				basePrimaryRole = (PlayerRole)1;
				newPrimaryRole = PlayerCustom.PlayerNewPrimaryRole.None;
				playerPrimaryRolePower = (PlayerCustom.PlayerPrimaryRolePower)RoleIndex;
				break;
			default:
				throw new IndexOutOfRangeException();
			}
			foreach (DraftChoiceRoleComponent item2 in UIManager.DraftPanel.CurrentRoles.Where((DraftChoiceRoleComponent o) => o.Type == DraftChoiceRoleType.SecondaryRole))
			{
				PlayerCustom.PlayerSecondaryRole roleIndex = (PlayerCustom.PlayerSecondaryRole)item2.RoleIndex;
				item2.SetAvailable(PlayerCustom.GetAvailableSecondaryRoles(basePrimaryRole, newPrimaryRole, playerPrimaryRolePower).Contains(roleIndex));
			}
			UIManager.DraftPanel.ChooseDefaultSecondaryRoleIfNeeded();
			break;
		}
		case DraftChoiceRoleType.SecondaryRole:
			DraftManager.Instance.MyData.SelectedSecondaryRole = (PlayerCustom.PlayerSecondaryRole)_roleIndex;
			break;
		}
		DraftManager.Rpc_Select_Roles(((SimulationBehaviour)GameManager.Instance).Runner, PlayerController.Local.Index, (DraftManager.Instance.MyData.MainRole == DraftManager.DraftPlayerMainRole.Solo) ? ((int)DraftManager.Instance.MyData.SelectedSoloRole) : ((int)DraftManager.Instance.MyData.SelectedPower), (int)DraftManager.Instance.MyData.SelectedSecondaryRole);
	}
}
