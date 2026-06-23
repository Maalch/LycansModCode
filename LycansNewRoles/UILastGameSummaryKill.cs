using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UILastGameSummaryKill : MonoBehaviour
{
	public static Sprite DaySprite;

	public static Sprite NightSprite;

	public static Sprite MeetingSprite;

	public static Sprite WinnerSprite;

	public static Sprite DeathTypeSpriteBomb;

	public static Sprite DeathTypeSpriteCrushed;

	public static Sprite DeathTypeSpriteFalling;

	public static Sprite DeathTypeSpriteGun;

	public static Sprite DeathTypeSpriteHunterGun;

	public static Sprite DeathTypeSpriteLover;

	public static Sprite DeathTypeSpriteCrystalBallGuess;

	public static Sprite DeathTypeSpriteStarvation;

	public static Sprite DeathTypeSpriteSurvivalistNotSaved;

	public static Sprite DeathTypeSpriteVote;

	public static Sprite DeathTypeSpriteWolfKill;

	public static Sprite DeathTypeSpriteZombie;

	public static Sprite DeathTypeSpriteInquisitorBurn;

	public static Sprite DeathTypeSpriteAssassin;

	private Image _timingIcon;

	private TextMeshProUGUI _timingText;

	private TextMeshProUGUI _killerText;

	private Image _deathTypeIcon;

	private TextMeshProUGUI _victimText;

	public void Awake()
	{
		_timingIcon = ((Component)((Component)this).gameObject.transform.Find("Timing").Find("Icon")).GetComponent<Image>();
		_timingText = ((Component)((Component)this).gameObject.transform.Find("Timing").Find("Day")).GetComponent<TextMeshProUGUI>();
		_killerText = ((Component)((Component)this).gameObject.transform.Find("Killer")).GetComponent<TextMeshProUGUI>();
		_deathTypeIcon = ((Component)((Component)this).gameObject.transform.Find("Icon")).GetComponent<Image>();
		_victimText = ((Component)((Component)this).gameObject.transform.Find("Victim")).GetComponent<TextMeshProUGUI>();
	}

	public void UpdateData(LastGameSummaryKill data)
	{
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		switch (data.Timing)
		{
		case LastGameSummaryKill.LastGameSummaryKillTiming.Day:
			_timingIcon.sprite = DaySprite;
			break;
		case LastGameSummaryKill.LastGameSummaryKillTiming.Night:
			_timingIcon.sprite = NightSprite;
			break;
		case LastGameSummaryKill.LastGameSummaryKillTiming.Meeting:
			_timingIcon.sprite = MeetingSprite;
			break;
		}
		((TMP_Text)_timingText).text = data.Day.ToString();
		switch (data.DeathType)
		{
		case "BOMB":
			_deathTypeIcon.sprite = DeathTypeSpriteBomb;
			break;
		case "CRUSHED":
			_deathTypeIcon.sprite = DeathTypeSpriteCrushed;
			break;
		case "FALL":
			_deathTypeIcon.sprite = DeathTypeSpriteFalling;
			break;
		case "AVENGER":
		case "SHERIF_MISTAKE":
		case "SHERIF_SUCCESS":
		case "KILLED_VILLAGE_IDIOT":
		case "MERCENARY_HUNT_KILL":
			_deathTypeIcon.sprite = DeathTypeSpriteGun;
			break;
		case "BULLET_HUMAN":
		case "BULLET_WOLF":
			_deathTypeIcon.sprite = DeathTypeSpriteHunterGun;
			break;
		case "LOVER_DEATH":
			_deathTypeIcon.sprite = DeathTypeSpriteLover;
			break;
		case "SEER":
		case "CULTIST_FAILED":
			_deathTypeIcon.sprite = DeathTypeSpriteCrystalBallGuess;
			break;
		case "STARVATION":
		case "STARVATION_AS_BEAST":
			_deathTypeIcon.sprite = DeathTypeSpriteStarvation;
			break;
		case "VOTED":
			_deathTypeIcon.sprite = DeathTypeSpriteVote;
			break;
		case "BY_WOLF":
		case "BY_BEAST":
		case "SURVIVALIST_NOT_SAVED":
		case "MOLE":
			_deathTypeIcon.sprite = DeathTypeSpriteWolfKill;
			break;
		case "BY_ZOMBIE":
			_deathTypeIcon.sprite = DeathTypeSpriteZombie;
			break;
		case "INQUISITOR_GUESS":
			_deathTypeIcon.sprite = DeathTypeSpriteInquisitorBurn;
			break;
		case "OTHER_AGENT":
		case "ASSASSIN":
			_deathTypeIcon.sprite = DeathTypeSpriteAssassin;
			break;
		}
		((TMP_Text)_killerText).text = data.Killer;
		((Graphic)_killerText).color = data.KillerColor;
		((TMP_Text)_victimText).text = data.Victim;
		((Graphic)_victimText).color = data.VictimColor;
	}

	public static Color GetColorForPlayer(PlayerCustom playerCustom)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Invalid comparison between Unknown and I4
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Color black = Color.black;
		if (playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.None)
		{
			return (Color)(playerCustom.NewPrimaryRole switch
			{
				PlayerCustom.PlayerNewPrimaryRole.Lover => PlayerCustom.NewPrimaryRoleLoverColor, 
				PlayerCustom.PlayerNewPrimaryRole.Traitor => PlayerCustom.NewPrimaryRoleTraitorColor, 
				_ => PlayerCustom.PlayerColorInListForGenericSoloRole, 
			});
		}
		if ((int)playerCustom.PlayerController.Role == 1)
		{
			return GameUI.WolfColor;
		}
		return Color.cyan;
	}
}
