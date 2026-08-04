using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fusion;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.PowerObjects;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerLocalEachSecondTimerComponent : MonoBehaviour
{
	private PlayerCustom _playerCustom;

	private Stopwatch _eachSecondWatch = new Stopwatch();

	private Stopwatch _fiveTimesPerSecondWatch = new Stopwatch();

	private int _confusedMillisecondsToNextChange;

	private Stopwatch _confusedWatch = new Stopwatch();

	public bool ConfusedForwardInverted = false;

	public bool ConfusedSidesInverted = false;

	public bool ConfusedRotationHorizontalInverted = false;

	public bool ConfusedRotationVerticalInverted = false;

	public void Init(PlayerCustom playerCustom)
	{
		_playerCustom = playerCustom;
		_eachSecondWatch.Start();
		_fiveTimesPerSecondWatch.Start();
		_confusedWatch.Stop();
	}

	private void Update()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Invalid comparison between Unknown and I4
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Invalid comparison between Unknown and I4
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_playerCustom != (Object)null && NetworkBool.op_Implicit(_playerCustom.Confused))
		{
			if (!_confusedWatch.IsRunning)
			{
				_confusedMillisecondsToNextChange = 1500;
				_confusedWatch.Restart();
			}
			else if (_confusedWatch.ElapsedMilliseconds >= _confusedMillisecondsToNextChange && LycansUtility.GameActuallyInPlay)
			{
				RandomizeConfusedEffect();
			}
		}
		else if (_confusedWatch.IsRunning)
		{
			_confusedWatch.Reset();
		}
		if (_eachSecondWatch.ElapsedMilliseconds >= 1000)
		{
			if (LycansUtility.GameActuallyInPlay && (Object)(object)PlayerController.Local.LocalCameraHandler.PovPlayer != (Object)null)
			{
				PlayerCustom povPlayerCustom = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
				List<PlayerRef> list = new List<PlayerRef>();
				if (povPlayerCustom.Accessory is AccessoryCrystalBall)
				{
					foreach (PlayerCustom item in PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf) && o.Ref != povPlayerCustom.Ref))
					{
						list.Add(item.Ref);
					}
				}
				if (NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf) && PlayerCustomRegistry.Any((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Shadow && NetworkBool.op_Implicit(o.NewPrimaryRoleUniqueBool)))
				{
					foreach (PlayerCustom item2 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != povPlayerCustom.Ref))
					{
						list.Add(item2.Ref);
					}
				}
				if (NetworkBool.op_Implicit(povPlayerCustom.Clairvoyance))
				{
					foreach (PlayerCustom item3 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != povPlayerCustom.Ref))
					{
						list.Add(item3.Ref);
					}
				}
				if (povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Host)
				{
					foreach (PlayerCustom item4 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != povPlayerCustom.Ref))
					{
						list.Add(item4.Ref);
					}
				}
				list = list.Distinct().ToList();
				foreach (PlayerRef item5 in list)
				{
					PlayerCustom player = PlayerCustomRegistry.GetPlayer(item5);
					player.UpdateVisibility();
				}
				if (((int)povPlayerCustom.PlayerController.Role == 1 || povPlayerCustom.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor) && povPlayerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Lover && Plugin.CustomConfig.MoleChance > 0 && !povPlayerCustom.MoleWarningIssued && GameManagerCustom.Instance.CurrentDay == 1 && GameManager.LightingManager.TimeOfDay >= 10f)
				{
					if (PlayerCustomRegistry.Any((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Mole))
					{
						UIManager.ShowRedCenterMessage("NALES_MOLE_WARNING_YES", 0.5f, 5f);
					}
					else
					{
						UIManager.ShowRedCenterMessage("NALES_MOLE_WARNING_NO", 0.5f, 5f);
					}
					povPlayerCustom.MoleWarningIssued = true;
				}
			}
			_eachSecondWatch.Restart();
		}
		if (_fiveTimesPerSecondWatch.ElapsedMilliseconds < 200)
		{
			return;
		}
		PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (player2.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Runemaster && player2.Ref == PlayerController.Local.Ref && player2.AssociatedRunes.Any() && PlayerRegistry.Any((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf))))
		{
			RunemasterRune runemasterRune = null;
			float num = 1000f;
			List<PlayerController> list2 = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead))).ToList();
			foreach (RunemasterRune associatedRune in player2.AssociatedRunes)
			{
				foreach (PlayerController item6 in list2)
				{
					float num2 = Vector3.Distance(((Component)associatedRune).transform.position, ((Component)item6).transform.position);
					float num3 = Vector3.Distance(((Component)associatedRune).transform.position, ((Component)player2.PlayerController).transform.position);
					if (num2 <= 10f && num3 < num)
					{
						runemasterRune = associatedRune;
						num = num3;
					}
				}
			}
			foreach (RunemasterRune associatedRune2 in player2.AssociatedRunes)
			{
				bool flag = (Object)(object)associatedRune2 == (Object)(object)runemasterRune;
				if (associatedRune2.IsSelected != flag)
				{
					associatedRune2.SetSelected(flag);
				}
			}
			List<PlayerCustom> list3 = PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
			foreach (PlayerCustom item7 in list3)
			{
				item7.UpdateVisibility();
			}
		}
		if (_playerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Inventor && (int)GameManager.LocalGameState == 2)
		{
			_playerCustom.InventorDeviceInfo.CheckAlert();
		}
		_fiveTimesPerSecondWatch.Restart();
	}

	private void RandomizeConfusedEffect()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		ConfusedForwardInverted = Random.value < 0.5f;
		ConfusedSidesInverted = Random.value < 0.5f;
		ConfusedRotationHorizontalInverted = false;
		ConfusedRotationVerticalInverted = false;
		_confusedMillisecondsToNextChange = Random.Range(4000, 7000);
		_confusedWatch.Restart();
		if (_playerCustom.IsCurrentlyPlayedOrObserved)
		{
			ColorAdjustmentManager.FlashScreen(Color.gray);
		}
	}
}
