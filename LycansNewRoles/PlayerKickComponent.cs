using System;
using System.Linq;
using Fusion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace LycansNewRoles;

public class PlayerKickComponent : MonoBehaviour
{
	public void Awake()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		try
		{
			PlayerVolume component = ((Component)this).GetComponent<PlayerVolume>();
			Button val = (Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[]).First((Button o) => ((Object)((Component)o).gameObject).name == "ResetButton");
			Transform transform = ((Component)val).transform;
			Transform parent = ((Component)val).transform.parent;
			Transform val2 = ((Component)component).transform.Find("LayoutGroup");
			((HorizontalOrVerticalLayoutGroup)((Component)val2).GetComponent<HorizontalLayoutGroup>()).spacing = 25f;
			Transform val3 = Object.Instantiate<Transform>(transform, val2);
			((Component)val3).GetComponent<RectTransform>().sizeDelta = new Vector2(120f, 70f);
			LocalizeStringEvent componentInChildren = ((Component)val3).GetComponentInChildren<LocalizeStringEvent>();
			((LocalizedReference)componentInChildren.StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit("NALES_BUTTON_KICK"));
			ColorBlock colors = ((Selectable)((Component)val3).GetComponent<Button>()).colors;
			Object.DestroyImmediate((Object)(object)((Component)val3).GetComponent<Button>());
			Button val4 = ((Component)val3).gameObject.AddComponent<Button>();
			((Selectable)val4).colors = colors;
			VoiceSpeaker value = Traverse.Create((object)component).Field<VoiceSpeaker>("_voiceSpeaker").Value;
			byte playerIndex = Traverse.Create((object)value).Field<PlayerController>("_playerController").Value.Index;
			((UnityEvent)val4.onClick).AddListener((UnityAction)delegate
			{
				OnClickKick(playerIndex);
			});
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("PlayerKickComponent Awake error: " + ex));
		}
	}

	public void OnClickKick(int playerIndex)
	{
		if (GameManager.Instance.IsHost)
		{
			PlayerCustom.Rpc_Kick(((SimulationBehaviour)GameManager.Instance).Runner, playerIndex);
		}
	}
}
