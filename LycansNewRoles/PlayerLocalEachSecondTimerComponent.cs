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
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Invalid comparison between Unknown and I4
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_playerCustom != (Object)null && NetworkBool.op_Implicit(_playerCustom.Confused))
		{
			if (!_confusedWatch.IsRunning)
			{
				_confusedMillisecondsToNextChange = 1500;
				_confusedWatch.Restart();
			}
			else if (_confusedWatch.ElapsedMilliseconds >= _confusedMillisecondsToNextChange)
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
		if (player2.PrimaryRolePower == PlayerCustom.PlayerPrimaryRolePower.Runemaster && player2.Ref == PlayerController.Local.Ref && RunemasterRune.AssociatedRunes.Any())
		{
			RunemasterRune runemasterRune = null;
			float num = -1f;
			foreach (RunemasterRune associatedRune in RunemasterRune.AssociatedRunes)
			{
				Vector3 val = ((Component)associatedRune).transform.position - ((Component)player2.PlayerController).transform.position;
				Vector3 normalized = ((Vector3)(ref val)).normalized;
				float num2 = Vector3.Dot(((Component)player2.PlayerController).transform.forward, normalized);
				float num3 = Vector3.Distance(((Component)player2.PlayerController).transform.position, ((Component)associatedRune).transform.position);
				float num4 = num2 / (1000f + num3);
				if (num4 > num)
				{
					num = num4;
					runemasterRune = associatedRune;
				}
			}
			foreach (RunemasterRune associatedRune2 in RunemasterRune.AssociatedRunes)
			{
				bool flag = (Object)(object)associatedRune2 == (Object)(object)runemasterRune;
				if (associatedRune2.IsSelected != flag)
				{
					associatedRune2.SetSelected(flag);
				}
			}
			List<PlayerCustom> list2 = PlayerCustomRegistry.Where((PlayerCustom o) => NetworkBool.op_Implicit(o.PlayerController.IsWolf) && !NetworkBool.op_Implicit(o.PlayerController.IsDead)).ToList();
			foreach (PlayerCustom item6 in list2)
			{
				item6.UpdateVisibility();
			}
		}
		_fiveTimesPerSecondWatch.Restart();
	}

	private void RandomizeConfusedEffect()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		ConfusedForwardInverted = Random.value < 0.5f;
		ConfusedSidesInverted = Random.value < 0.5f;
		ConfusedRotationHorizontalInverted = Random.value < 0.5f;
		ConfusedRotationVerticalInverted = Random.value < 0.5f;
		_confusedMillisecondsToNextChange = Random.Range(5000, 8000);
		_confusedWatch.Restart();
		if (_playerCustom.IsCurrentlyPlayedOrObserved)
		{
			ColorAdjustmentManager.FlashScreen(Color.gray);
		}
	}
}
