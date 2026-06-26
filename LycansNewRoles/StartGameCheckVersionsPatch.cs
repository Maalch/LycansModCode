using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(GameManager), "StartGame")]
internal class StartGameCheckVersionsPatch
{
	private static bool Prefix(GameManager __instance)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (((SimulationBehaviour)__instance).Runner.IsServer && (int)GameManager.State.Current == 1)
			{
				List<PlayerCustom> list = PlayerCustomRegistry.Where((PlayerCustom o) => PlayerRef.op_Implicit(o.Ref) < 1000).ToList();
				foreach (PlayerCustom item in list)
				{
					if (!item.ModVersion.HasValue)
					{
						if (string.IsNullOrEmpty(((object)item.PlayerController.PlayerData.Username/*cast due to constrained. prefix*/).ToString()))
						{
							UIManager.ShowRedCenterMessage("NALES_UI_VERSION_CHECK_MOD_WAITING", 0.5f, 5f, new List<object> { item.PlayerController.PlayerData.Username });
						}
						else
						{
							UIManager.ShowRedCenterMessage("NALES_UI_VERSION_CHECK_MOD_MISSING", 0.5f, 5f, new List<object> { item.PlayerController.PlayerData.Username });
						}
						return false;
					}
					if (item.ModVersion.Value != float.Parse("0.318", CultureInfo.InvariantCulture.NumberFormat))
					{
						UIManager.ShowRedCenterMessage("NALES_UI_VERSION_CHECK_WRONG_VERSION", 0.5f, 5f, new List<object> { item.PlayerController.PlayerData.Username });
						return false;
					}
				}
				GameManager.Instance.UpdateHuntersCountSetting(0);
				GameManager.Instance.UpdateAlchemistsCountSetting(0);
				if (NetworkBool.op_Implicit(Plugin.CustomConfig.DraftMode) && !NetworkBool.op_Implicit(GameManager.Instance.BattleRoyale))
				{
					GameManager.Instance.IsStarted = NetworkBool.op_Implicit(true);
					((SimulationBehaviour)__instance).Runner.SessionInfo.IsOpen = false;
					DraftManager.Instance.Init();
					DraftManager.Instance.Active = NetworkBool.op_Implicit(true);
					GameManager.State.Server_SetState((EGameState)2);
					PlayerRegistry.ForEach((Action<PlayerController>)delegate(PlayerController pObj)
					{
						//IL_0003: Unknown result type (might be due to invalid IL or missing references)
						pObj.CanMove = NetworkBool.op_Implicit(false);
					});
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("StartGameCheckVersionsPatch error: " + ex));
			return true;
		}
	}
}
