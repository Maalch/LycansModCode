using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class ExtraSettings : MonoBehaviour
{
	private static ExtraSettings _instance;

	public static string PlayerPrefReduceWolfRef = "EXTRA_SETTINGS_REDUCE_WOLF_RED";

	public static string PlayerPrefDisableLivingVoicesWhenInDeadChannel = "EXTRA_SETTINGS_DISABLE_LIVING_VOICES_WHEN_IN_DEAD_CHANNEL";

	public static string PlayerPrefNoDeadRoleOnDeath = "EXTRA_SETTINGS_NO_DEAD_ROLE_ON_DEATH";

	public static string PlayerPrefHidePets = "EXTRA_SETTINGS_HIDE_PETS";

	public bool ReduceWolfRed;

	public bool DisableLivingVoicesWhenInDeadChannel;

	public bool NoDeadRoleOnDeath;

	public bool HidePets;

	public static ExtraSettings Instance
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			if ((Object)(object)_instance == (Object)null)
			{
				GameObject val = new GameObject("ExtraSettings");
				val.AddComponent<ExtraSettings>();
				GameObject val2 = Object.Instantiate<GameObject>(val);
				_instance = val2.GetComponent<ExtraSettings>();
			}
			return _instance;
		}
	}

	private void Start()
	{
		ReduceWolfRed = PlayerPrefs.GetInt(PlayerPrefReduceWolfRef) == 1;
		DisableLivingVoicesWhenInDeadChannel = PlayerPrefs.GetInt(PlayerPrefDisableLivingVoicesWhenInDeadChannel) == 1;
		NoDeadRoleOnDeath = PlayerPrefs.GetInt(PlayerPrefNoDeadRoleOnDeath) == 1;
		HidePets = PlayerPrefs.GetInt(PlayerPrefHidePets) == 1;
	}

	public static void OnReduceWolfRedChanged(bool value)
	{
		PlayerPrefs.SetInt(PlayerPrefReduceWolfRef, value ? 1 : 0);
		Instance.ReduceWolfRed = value;
	}

	public static void OnDisableLivingVoicesWhenInDeadChannelChanged(bool value)
	{
		PlayerPrefs.SetInt(PlayerPrefDisableLivingVoicesWhenInDeadChannel, value ? 1 : 0);
		Instance.DisableLivingVoicesWhenInDeadChannel = value;
	}

	public static void OnNoDeadRoleOnDeathChanged(bool value)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		PlayerPrefs.SetInt(PlayerPrefNoDeadRoleOnDeath, value ? 1 : 0);
		Instance.NoDeadRoleOnDeath = value;
		if ((int)GameManager.LocalGameState != 0 && (Object)(object)PlayerController.Local != (Object)null)
		{
			PlayerCustom.Rpc_Set_No_Dead_Role(((SimulationBehaviour)PlayerController.Local).Runner, PlayerController.Local.Index, Instance.NoDeadRoleOnDeath ? 1 : 0);
		}
	}

	public static void OnHidePetsChanged(bool value)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		PlayerPrefs.SetInt(PlayerPrefHidePets, value ? 1 : 0);
		Instance.HidePets = value;
		if ((int)GameManager.LocalGameState == 0 || (int)GameManager.LocalGameState == 1)
		{
			return;
		}
		foreach (PlayerCustom allPlayer in PlayerCustomRegistry.AllPlayers)
		{
			allPlayer.UpdateVisibility();
		}
	}
}
