using System;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameUI), "Awake")]
public class GameUIAwakePatch
{
	private static void Postfix(GameUI __instance)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			TextMeshProUGUI value = Traverse.Create((object)__instance).Field<TextMeshProUGUI>("mmVersionText").Value;
			((TMP_Text)value).text = ((TMP_Text)value).text + Environment.NewLine + "New Roles 0.318";
			GameObject value2 = Traverse.Create((object)__instance).Field<GameObject>("playersContainer").Value;
			value2.transform.position = new Vector3(value2.transform.position.x, value2.transform.position.y + 250f, value2.transform.position.z);
			GameObject val = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("Timer");
			GameObject val2 = Object.Instantiate<GameObject>(val, ((Component)__instance).transform.Find("Canvas").Find("Game"));
			val2.transform.position = new Vector3(val2.transform.position.x, val2.transform.position.y + 100f, val2.transform.position.z);
			val2.transform.SetAsLastSibling();
			UITimer timer = val2.AddComponent<UITimer>();
			UIManager.Timer = timer;
			GameObject val3 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("GenericChoicePanel");
			GameObject val4 = Object.Instantiate<GameObject>(val3, ((Component)__instance).transform.Find("Canvas"));
			val4.transform.SetAsLastSibling();
			UIGenericChoicePanel genericChoicePanel = val4.AddComponent<UIGenericChoicePanel>();
			UIManager.GenericChoicePanel = genericChoicePanel;
			GameObject val5 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("DraftPanel");
			GameObject val6 = Object.Instantiate<GameObject>(val5, ((Component)__instance).transform.Find("Canvas"));
			val6.transform.SetAsLastSibling();
			UIDraftPanel draftPanel = val6.AddComponent<UIDraftPanel>();
			UIManager.DraftPanel = draftPanel;
			GameObject val7 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("Customization");
			GameObject val8 = Object.Instantiate<GameObject>(val7, ((Component)__instance).transform.Find("Canvas"));
			val8.transform.SetAsLastSibling();
			UICustomizationComponent customizationComponent = val8.AddComponent<UICustomizationComponent>();
			UIManager.CustomizationComponent = customizationComponent;
			GameObject val9 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("PlayerStatePanel");
			GameObject val10 = Object.Instantiate<GameObject>(val9, ((Component)__instance).transform.Find("Canvas"));
			val10.transform.SetAsLastSibling();
			UIModInstallationPanel modInstallationPanel = val10.AddComponent<UIModInstallationPanel>();
			UIManager.ModInstallationPanel = modInstallationPanel;
			GameObject val11 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("SpiritUI");
			GameObject val12 = Object.Instantiate<GameObject>(val11, ((Component)__instance).transform.Find("Canvas"));
			val12.transform.SetAsFirstSibling();
			UISpiritPanel spiritPanel = val12.AddComponent<UISpiritPanel>();
			UIManager.SpiritPanel = spiritPanel;
			GameObject val13 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("OptionsDisplayPanel");
			GameObject val14 = Object.Instantiate<GameObject>(val13, ((Component)__instance).transform.Find("Canvas"));
			val14.transform.SetAsLastSibling();
			UIOptionsDisplayPanel optionsDisplayPanel = val14.AddComponent<UIOptionsDisplayPanel>();
			UIManager.OptionsDisplayPanel = optionsDisplayPanel;
			GameObject val15 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("ItemDetailsText");
			Transform transform = ((Component)Traverse.Create((object)GameManager.Instance.gameUI).Field<Image>("itemImage").Value).transform;
			Transform parent = transform.parent;
			GameObject val16 = Object.Instantiate<GameObject>(val15, parent);
			val16.SetActive(true);
			val16.transform.position = new Vector3(transform.position.x, transform.position.y + 96f, transform.position.z);
			val16.transform.SetAsLastSibling();
			UIItemDetailsPanel itemDetailsPanel = val16.AddComponent<UIItemDetailsPanel>();
			UIManager.ItemDetailsPanel = itemDetailsPanel;
			GameObject val17 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("ItemSecondary");
			GameObject val18 = Object.Instantiate<GameObject>(val17, parent);
			val18.SetActive(true);
			val18.transform.position = new Vector3(transform.position.x, transform.position.y - 112f, transform.position.z);
			val18.transform.SetAsLastSibling();
			UIItemSecondaryPanel itemSecondaryPanel = val18.AddComponent<UIItemSecondaryPanel>();
			UIManager.ItemSecondaryPanel = itemSecondaryPanel;
			GameObject val19 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("AccessoryPanel");
			GameObject val20 = Object.Instantiate<GameObject>(val19, parent.parent);
			val20.SetActive(true);
			val20.transform.position = new Vector3(parent.position.x + ((Component)parent).GetComponent<RectTransform>().sizeDelta.x * 0.667f, parent.position.y + 256f, parent.position.z);
			val20.transform.SetAsLastSibling();
			UIAccessoryPanel accessoryPanel = val20.AddComponent<UIAccessoryPanel>();
			UIManager.AccessoryPanel = accessoryPanel;
			GameObject val21 = Object.Instantiate<GameObject>(((Component)parent).gameObject, parent.parent);
			val21.transform.position = new Vector3(parent.position.x + 192f, parent.position.y, parent.position.z);
			val21.SetActive(true);
			val21.transform.SetAsLastSibling();
			UISecondItemPanel secondItemPanel = val21.AddComponent<UISecondItemPanel>();
			UIManager.SecondItemPanel = secondItemPanel;
			GameObject val22 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("DetectiveNotesPanel");
			GameObject val23 = Object.Instantiate<GameObject>(val22, ((Component)__instance).transform.Find("Canvas"));
			val23.transform.SetAsLastSibling();
			UIDetectivePanel detectivePanel = val23.AddComponent<UIDetectivePanel>();
			UIManager.DetectivePanel = detectivePanel;
			GameObject val24 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("MayorPanelForMayor");
			GameObject val25 = Object.Instantiate<GameObject>(val24, ((Component)__instance).transform.Find("Canvas"));
			val25.transform.SetAsLastSibling();
			UIMayorPanelForMayor mayorPanelForMayor = val25.AddComponent<UIMayorPanelForMayor>();
			UIManager.MayorPanelForMayor = mayorPanelForMayor;
			GameObject val26 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("MayorPanelForOthers");
			GameObject val27 = Object.Instantiate<GameObject>(val26, ((Component)__instance).transform.Find("Canvas"));
			val27.transform.SetAsLastSibling();
			UIMayorPanelForOthers mayorPanelForOthers = val27.AddComponent<UIMayorPanelForOthers>();
			UIManager.MayorPanelForOthers = mayorPanelForOthers;
			GameObject val28 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("RoleDescriptionPanel");
			GameObject val29 = Object.Instantiate<GameObject>(val28, ((Component)__instance).transform.Find("Canvas"));
			val29.transform.SetAsLastSibling();
			UIRoleDescription roleDescription = val29.AddComponent<UIRoleDescription>();
			UIManager.RoleDescription = roleDescription;
			GameObject val30 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("DeadRolePanel");
			GameObject val31 = Object.Instantiate<GameObject>(val30, ((Component)__instance).transform.Find("Canvas"));
			val31.transform.SetAsLastSibling();
			UIDeadRolePanel deadRolePanel = val31.AddComponent<UIDeadRolePanel>();
			UIManager.DeadRolePanel = deadRolePanel;
			GameObject val32 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("LastGamePanel");
			GameObject val33 = Object.Instantiate<GameObject>(val32, ((Component)__instance).transform.Find("Canvas"));
			val33.transform.SetAsLastSibling();
			UILastGameSummaryPanel lastGameSummaryPanel = val33.AddComponent<UILastGameSummaryPanel>();
			UIManager.LastGameSummaryPanel = lastGameSummaryPanel;
			GameObject val34 = Plugin.NewRolesCoreBundle.LoadAsset<GameObject>("SoloRolesProgressPanel");
			GameObject val35 = Object.Instantiate<GameObject>(val34, ((Component)__instance).transform.Find("Canvas"));
			val35.transform.SetAsLastSibling();
			UISoloRolesProgressPanel soloRolesProgressPanel = val35.AddComponent<UISoloRolesProgressPanel>();
			UIManager.SoloRolesProgressPanel = soloRolesProgressPanel;
			UILastGameSummaryKill.DaySprite = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("TimingDay");
			UILastGameSummaryKill.NightSprite = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("TimingNight");
			UILastGameSummaryKill.MeetingSprite = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("TimingMeeting");
			UILastGameSummaryKill.WinnerSprite = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("Winner");
			UILastGameSummaryKill.DeathTypeSpriteBomb = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathBomb");
			UILastGameSummaryKill.DeathTypeSpriteCrushed = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathCrushed");
			UILastGameSummaryKill.DeathTypeSpriteFalling = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathFalling");
			UILastGameSummaryKill.DeathTypeSpriteGun = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathGun");
			UILastGameSummaryKill.DeathTypeSpriteHunterGun = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathHunterGun");
			UILastGameSummaryKill.DeathTypeSpriteLover = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathLover");
			UILastGameSummaryKill.DeathTypeSpriteCrystalBallGuess = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathSeerGuess");
			UILastGameSummaryKill.DeathTypeSpriteStarvation = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathStarvation");
			UILastGameSummaryKill.DeathTypeSpriteSurvivalistNotSaved = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathSurvivalistNotSaved");
			UILastGameSummaryKill.DeathTypeSpriteVote = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathVote");
			UILastGameSummaryKill.DeathTypeSpriteWolfKill = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathWolfKill");
			UILastGameSummaryKill.DeathTypeSpriteZombie = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathZombie");
			UILastGameSummaryKill.DeathTypeSpriteInquisitorBurn = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathInquisitorGuess");
			UILastGameSummaryKill.DeathTypeSpriteAssassin = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("DeathAssassin");
			UIManager.KidnappedPlayerIcon = Plugin.NewRolesCoreBundle.LoadAsset<Sprite>("KidnapperIcon");
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GameUI Awake postfix error: " + ex));
		}
	}
}
