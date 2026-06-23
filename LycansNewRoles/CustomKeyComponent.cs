using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

namespace LycansNewRoles;

public class CustomKeyComponent : MonoBehaviour
{
	public string TranslateKey;

	public void UpdateActionText()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			((LocalizedReference)((Component)((Component)this).transform.Find("ActionNameText")).gameObject.GetComponent<LocalizeStringEvent>().StringReference).SetReference(TableReference.op_Implicit("UI Text"), TableEntryReference.op_Implicit(TranslateKey));
			((Component)((Component)this).transform.Find("ActionNameText")).gameObject.GetComponent<LocalizeStringEvent>().StringReference.RefreshString();
			((TMP_Text)((Component)((Component)this).transform.Find("ActionNameText")).gameObject.GetComponent<TextMeshProUGUI>()).text = TranslationManager.Instance.GetTranslation(TranslateKey);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UpdateActionText error: " + ex));
		}
	}
}
