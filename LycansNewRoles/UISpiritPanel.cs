using Fusion;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UISpiritPanel : MonoBehaviour
{
	private static Color ColorCooldown = new Color(1f, 1f, 0f, 0.8f);

	private static Color ColorReady = new Color(0f, 1f, 0f, 0.8f);

	private GameObject _panel;

	private GameObject _attack;

	private GameObject _spell;

	private Image _attackFill;

	private Image _spellFill;

	public bool Active = false;

	private void Start()
	{
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_attack = ((Component)_panel.transform.Find("Attack")).gameObject;
		((TMP_Text)((Component)_attack.transform.Find("Text")).GetComponent<TextMeshProUGUI>()).text = LycansUtility.GetInputDisplayCustom((InputActionName)3).Replace(" -", "");
		_attackFill = ((Component)_attack.transform.Find("Fill")).GetComponent<Image>();
		_spell = ((Component)_panel.transform.Find("Spell")).gameObject;
		((TMP_Text)((Component)_spell.transform.Find("Text")).GetComponent<TextMeshProUGUI>()).text = LycansUtility.GetInputDisplayCustom((InputActionName)5).Replace(" -", "");
		_spellFill = ((Component)_spell.transform.Find("Fill")).GetComponent<Image>();
		_panel.SetActive(false);
	}

	private void Update()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Invalid comparison between Unknown and I4
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)PlayerCustom.Local == (Object)null || (Object)(object)PlayerCustom.Local.SummonedSpirit == (Object)null)
		{
			if (Active)
			{
				Hide();
			}
			return;
		}
		if (!Active)
		{
			Show();
		}
		PlayerSummonedSpiritComponent summonedSpirit = PlayerCustom.Local.SummonedSpirit;
		TickTimer val = summonedSpirit.AttackCooldown;
		if (((TickTimer)(ref val)).IsRunning)
		{
			float num = 30f;
			switch (PlayerCustom.Local.PrimaryRolePower)
			{
			case PlayerCustom.PlayerPrimaryRolePower.Ghost:
				num = 5f;
				break;
			case PlayerCustom.PlayerPrimaryRolePower.Specter:
				num = (((int)GameManager.LocalGameState == 4) ? 40f : 25f);
				break;
			}
			PlayerCustom.PlayerNewPrimaryRole newPrimaryRole = PlayerCustom.Local.NewPrimaryRole;
			PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole = newPrimaryRole;
			if (playerNewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Cultist)
			{
				num = 5f;
			}
			((Graphic)_attackFill).color = ColorCooldown;
			Image attackFill = _attackFill;
			float num2 = num;
			val = summonedSpirit.AttackCooldown;
			attackFill.fillAmount = (num2 - ((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)PlayerCustom.Local).Runner).Value) / num;
		}
		else
		{
			((Graphic)_attackFill).color = ColorReady;
			_attackFill.fillAmount = 1f;
		}
		switch (PlayerCustom.Local.PrimaryRolePower)
		{
		case PlayerCustom.PlayerPrimaryRolePower.Ghost:
			_spell.gameObject.SetActive(true);
			val = summonedSpirit.SpellCooldown;
			if (((TickTimer)(ref val)).IsRunning)
			{
				((Graphic)_spellFill).color = ColorCooldown;
				Image spellFill2 = _spellFill;
				val = summonedSpirit.SpellCooldown;
				spellFill2.fillAmount = (20f - ((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)PlayerCustom.Local).Runner).Value) / 20f;
			}
			else
			{
				((Graphic)_spellFill).color = ColorReady;
				_spellFill.fillAmount = 1f;
			}
			break;
		case PlayerCustom.PlayerPrimaryRolePower.Specter:
			_spell.gameObject.SetActive(true);
			val = summonedSpirit.SpellCooldown;
			if (((TickTimer)(ref val)).IsRunning)
			{
				((Graphic)_spellFill).color = ColorCooldown;
				Image spellFill = _spellFill;
				val = summonedSpirit.SpellCooldown;
				spellFill.fillAmount = (120f - ((TickTimer)(ref val)).RemainingTime(((SimulationBehaviour)PlayerCustom.Local).Runner).Value) / 120f;
			}
			else
			{
				((Graphic)_spellFill).color = ColorReady;
				_spellFill.fillAmount = 1f;
			}
			break;
		}
		PlayerCustom.PlayerNewPrimaryRole newPrimaryRole2 = PlayerCustom.Local.NewPrimaryRole;
		PlayerCustom.PlayerNewPrimaryRole playerNewPrimaryRole2 = newPrimaryRole2;
		if (playerNewPrimaryRole2 == PlayerCustom.PlayerNewPrimaryRole.Cultist)
		{
			_spell.gameObject.SetActive(false);
		}
	}

	public void Show()
	{
		_panel.SetActive(true);
		Active = true;
	}

	public void Hide()
	{
		_panel.SetActive(false);
		Active = false;
	}
}
