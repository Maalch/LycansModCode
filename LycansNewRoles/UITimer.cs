using System;
using System.Diagnostics;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LycansNewRoles;

public class UITimer : MonoBehaviour
{
	private static Color ColorBackground = Color.white;

	private static Color ColorFill = Color.green;

	private static Color ColorText = Color.black;

	private TickTimer Timer = TickTimer.None;

	private NetworkRunner Runner;

	private float InitialSeconds;

	private Image ImageBorder;

	private Image ImageBackground;

	private Image ImageFill;

	private TextMeshProUGUI TextAction;

	private bool Active = false;

	public bool IsActive => Active;

	private void Awake()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			ImageBorder = ((Component)((Component)this).transform.Find("Border")).GetComponent<Image>();
			ImageBackground = ((Component)((Component)this).transform.Find("Background")).GetComponent<Image>();
			ImageFill = ((Component)((Component)this).transform.Find("Fill")).GetComponent<Image>();
			TextAction = ((Component)((Component)this).transform.Find("ActionText")).GetComponent<TextMeshProUGUI>();
			((Graphic)ImageBackground).color = ColorBackground;
			((Graphic)ImageFill).color = ColorFill;
			((Graphic)TextAction).color = ColorText;
			Show(active: false);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UITimer Awake error: " + ex));
		}
	}

	public void UpdateTimer(TickTimer timer, NetworkRunner runner, string translateKey)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((TickTimer)(ref timer)).ExpiredOrNotRunning(runner))
			{
				Active = false;
				Timer = TickTimer.None;
				Show(active: false);
				return;
			}
			Active = true;
			Timer = timer;
			Runner = runner;
			InitialSeconds = ((TickTimer)(ref Timer)).RemainingTime(runner).Value;
			((TMP_Text)TextAction).text = TranslationManager.Instance.GetTranslation(translateKey).Replace("{0} ", "");
			Show(active: true);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UITimer UpdateTimer error: " + ex));
		}
	}

	public void HideTimer()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Active = false;
			Timer = TickTimer.None;
			Show(active: false);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UITimer HideTimer error: " + ex));
		}
	}

	public void Update()
	{
		try
		{
			if (Active)
			{
				if (((TickTimer)(ref Timer)).ExpiredOrNotRunning(Runner))
				{
					HideTimer();
				}
				else
				{
					ImageFill.fillAmount = 1f - ((TickTimer)(ref Timer)).RemainingTime(Runner).Value / InitialSeconds;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("UITimer update error: " + ex));
			Active = false;
			Show(active: false);
		}
	}

	private void Show(bool active)
	{
		try
		{
			if ((Object)(object)ImageBorder != (Object)null && (Object)(object)((Component)ImageBorder).gameObject != (Object)null)
			{
				((Component)ImageBorder).gameObject.SetActive(active);
			}
			if ((Object)(object)ImageBackground != (Object)null && (Object)(object)((Component)ImageBackground).gameObject != (Object)null)
			{
				((Component)ImageBackground).gameObject.SetActive(active);
			}
			if ((Object)(object)ImageFill != (Object)null && (Object)(object)((Component)ImageFill).gameObject != (Object)null)
			{
				((Component)ImageFill).gameObject.SetActive(active);
			}
			if ((Object)(object)TextAction != (Object)null && (Object)(object)((Component)TextAction).gameObject != (Object)null)
			{
				((Component)TextAction).gameObject.SetActive(active);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Show error: " + ex));
			StackTrace stackTrace = new StackTrace();
			Plugin.Logger.LogError((object)("StackTrace: " + stackTrace));
		}
	}
}
