using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

namespace LycansNewRoles;

public class UIDetectivePanel : MonoBehaviour
{
	public static GameObject DetectiveNotePrefab;

	private GameObject _headerPanel;

	private GameObject _panel;

	private List<GameObject> _notes = new List<GameObject>();

	public bool Active = false;

	private void Start()
	{
		_headerPanel = ((Component)((Component)this).transform.Find("HeaderPanel")).gameObject;
		((TMP_Text)((Component)_headerPanel.transform.Find("Text")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_UI_DETECTIVE_HEADER");
		_panel = ((Component)((Component)this).transform.Find("Panel")).gameObject;
		_headerPanel.SetActive(false);
		_panel.SetActive(false);
	}

	public void Show()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		_headerPanel.SetActive(true);
		_panel.SetActive(true);
		DestroyNotes();
		List<PlayerDetectiveIntel> detectiveIntelList = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref).DetectiveIntelList;
		foreach (PlayerDetectiveIntel item in detectiveIntelList)
		{
			GameObject val = Object.Instantiate<GameObject>(DetectiveNotePrefab);
			val.SetActive(true);
			((TMP_Text)((Component)val.transform.Find("DayPanel").Find("Text")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_UI_DETECTIVE_DAY").Replace("{0}", item.DayObtained.ToString());
			string text = "";
			switch (item.Type)
			{
			case PlayerDetectiveIntel.PlayerDetectiveIntelType.OneIsEvil:
			{
				string text2 = "";
				foreach (PlayerRef playerRef in (item as PlayerDetectiveIntelOneIsEvil).PlayerRefs)
				{
					if (text2 != "")
					{
						text2 += ", ";
					}
					if (PlayerCustomRegistry.HasPlayer(playerRef))
					{
						PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(playerRef);
						Color color2 = ColorManager.GetColor(player2.ColorIndex);
						text2 = text2 + "<color=#" + ColorUtility.ToHtmlStringRGB(color2) + ">" + ((object)player2.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</color>";
					}
					else
					{
						text2 += "???";
					}
				}
				text = TranslationManager.Instance.GetTranslation("NALES_UI_DETECTIVE_ONE_IS_EVIL").Replace("{0}", text2);
				break;
			}
			case PlayerDetectiveIntel.PlayerDetectiveIntelType.DifferentSides:
			{
				PlayerRef pRef = (item as PlayerDetectiveIntelDifferentSides).PlayerRefs[0];
				PlayerRef pRef2 = (item as PlayerDetectiveIntelDifferentSides).PlayerRefs[1];
				if (!PlayerCustomRegistry.HasPlayer(pRef) || !PlayerCustomRegistry.HasPlayer(pRef2))
				{
					continue;
				}
				PlayerCustom player3 = PlayerCustomRegistry.GetPlayer(pRef);
				PlayerCustom player4 = PlayerCustomRegistry.GetPlayer(pRef2);
				Color color3 = ColorManager.GetColor(player3.ColorIndex);
				Color color4 = ColorManager.GetColor(player4.ColorIndex);
				text = TranslationManager.Instance.GetTranslation("NALES_UI_DETECTIVE_DIFFERENT_SIDES").Replace("{0}", "<color=#" + ColorUtility.ToHtmlStringRGB(color3) + ">" + ((object)player3.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</color>").Replace("{1}", "<color=#" + ColorUtility.ToHtmlStringRGB(color4) + ">" + ((object)player4.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</color>");
				break;
			}
			case PlayerDetectiveIntel.PlayerDetectiveIntelType.NotWolf:
			{
				PlayerRef target = (item as PlayerDetectiveIntelNotWolf).Target;
				if (!PlayerCustomRegistry.HasPlayer(target))
				{
					continue;
				}
				PlayerCustom player = PlayerCustomRegistry.GetPlayer(target);
				Color color = ColorManager.GetColor(player.ColorIndex);
				text = TranslationManager.Instance.GetTranslation("NALES_UI_DETECTIVE_NOT_WOLF").Replace("{0}", "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + ((object)player.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString() + "</color>");
				break;
			}
			case PlayerDetectiveIntel.PlayerDetectiveIntelType.TransformationsAndDetransformations:
			{
				int transformations = (item as PlayerDetectiveIntelTransformationsAndDetransformations).Transformations;
				int detransformations = (item as PlayerDetectiveIntelTransformationsAndDetransformations).Detransformations;
				text = TranslationManager.Instance.GetTranslation("NALES_UI_DETECTIVE_TRANSFORMATIONS_AND_DETRANSFORMATIONS").Replace("{0}", transformations.ToString()).Replace("{1}", detransformations.ToString());
				break;
			}
			case PlayerDetectiveIntel.PlayerDetectiveIntelType.WolvesAndSoloRolesRemaining:
			{
				int wolves = (item as PlayerDetectiveIntelWolvesAndSoloRolesRemaining).Wolves;
				int soloRoles = (item as PlayerDetectiveIntelWolvesAndSoloRolesRemaining).SoloRoles;
				text = TranslationManager.Instance.GetTranslation("NALES_UI_DETECTIVE_WOLVES_AND_SOLO_ROLES_REMAINING").Replace("{0}", wolves.ToString()).Replace("{1}", soloRoles.ToString());
				break;
			}
			}
			((TMP_Text)((Component)val.transform.Find("DescriptionPanel").Find("Text")).GetComponent<TextMeshProUGUI>()).text = text;
			val.transform.SetParent(_panel.transform);
			((TMP_Text)((Component)val.transform.Find("DescriptionPanel").Find("New")).GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation("NALES_UI_DETECTIVE_NEW");
			((Component)val.transform.Find("DescriptionPanel").Find("New")).gameObject.SetActive(item.IsNew);
			_notes.Add(val);
		}
		Active = true;
	}

	public void Hide()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		DestroyNotes();
		_headerPanel.SetActive(false);
		_panel.SetActive(false);
		Active = false;
		if ((Object)(object)PlayerController.Local == (Object)null || (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer == (Object)null)
		{
			return;
		}
		List<PlayerDetectiveIntel> detectiveIntelList = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref).DetectiveIntelList;
		foreach (PlayerDetectiveIntel item in detectiveIntelList)
		{
			item.IsNew = false;
		}
	}

	private void DestroyNotes()
	{
		foreach (GameObject note in _notes)
		{
			Object.Destroy((Object)(object)note);
		}
		_notes.Clear();
	}
}
