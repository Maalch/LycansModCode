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
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Invalid comparison between Unknown and I4
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Invalid comparison between Unknown and I4
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Invalid comparison between Unknown and I4
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Invalid comparison between Unknown and I4
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
				if (povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Host || (povPlayerCustom.PlayerController.Item is SpyglassItem && NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsZooming)))
				{
					foreach (PlayerCustom item4 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != povPlayerCustom.Ref))
					{
						list.Add(item4.Ref);
					}
				}
				if (povPlayerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Spotter && NetworkBool.op_Implicit(povPlayerCustom.Spotter) && NetworkBool.op_Implicit(povPlayerCustom.PlayerController.IsWolf) && (int)povPlayerCustom.PlayerController.Role == 1)
				{
					foreach (PlayerCustom item5 in PlayerCustomRegistry.Where((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && o.Ref != povPlayerCustom.Ref))
					{
						list.Add(item5.Ref);
					}
				}
				list = list.Distinct().ToList();
				foreach (PlayerRef item6 in list)
				{
					PlayerCustom player = PlayerCustomRegistry.GetPlayer(item6);
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
				if ((int)povPlayerCustom.PlayerController.Role == 1 && povPlayerCustom.WolfDaysWithoutTransformation >= 3 && !povPlayerCustom.WolfForcedTransformationWarningIssued && GameManager.LightingManager.TimeOfDay >= 10f)
				{
					povPlayerCustom.WolfForcedTransformationWarningIssued = true;
					UIManager.ShowRedCenterMessage("NALES_UI_FORCED_TRANSFORMATION_TODAY", 0.5f, 5f);
				}
			}
			_eachSecondWatch.Restart();
		}
		if (_fiveTimesPerSecondWatch.ElapsedMilliseconds < 200)
		{
			return;
		}
		PlayerCustom player2 = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (player2.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Runemaster && player2.Ref == PlayerController.Local.Ref && player2.AssociatedRunes.Any() && PlayerCustomRegistry.Any((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf)))
		{
			RunemasterRune runemasterRune = null;
			float num = 1000f;
			List<PlayerController> list2 = PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => NetworkBool.op_Implicit(o.IsWolf) && !NetworkBool.op_Implicit(o.IsDead))).ToList();
			foreach (RunemasterRune associatedRune in player2.AssociatedRunes)
			{
				foreach (PlayerController item7 in list2)
				{
					float num2 = Vector3.Distance(((Component)associatedRune).transform.position, ((Component)item7).transform.position);
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
			foreach (PlayerCustom item8 in list3)
			{
				item8.UpdateVisibility();
			}
		}
		if (_playerCustom.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Inventor && (int)GameManager.LocalGameState == 2)
		{
			_playerCustom.InventorDeviceInfo.CheckAlert();
		}
		if (NetworkBool.op_Implicit(_playerCustom.PlayerController.IsWolf) && !NetworkBool.op_Implicit(_playerCustom.PlayerController.IsDead))
		{
			foreach (PlayerCustom item9 in PlayerCustomRegistry.Where((PlayerCustom o) => o.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Ghost))
			{
				if ((Object)(object)item9.SummonedSpirit != (Object)null && !NetworkBool.op_Implicit(item9.NewPrimaryRoleUniqueBool) && Vector3.Distance(((Component)_playerCustom.PlayerController).transform.position, ((Component)item9.SummonedSpirit).transform.position) <= 2f)
				{
					PlayerCustom.Rpc_Spirit_Attack(((SimulationBehaviour)item9).Runner, _playerCustom.Index, item9.Index);
				}
			}
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
