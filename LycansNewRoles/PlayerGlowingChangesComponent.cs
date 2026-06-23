using System.Diagnostics;
using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerGlowingChangesComponent : MonoBehaviour
{
	private const float GlowIntervalMilliseconds = 500f;

	private PlayerCustom _playerCustom;

	private PlayerEffectsManager _playerEffects;

	private Light _playerGlowing;

	private Stopwatch _watch = new Stopwatch();

	public void Init(PlayerCustom playerCustom, PlayerEffectsManager playerEffects, Light playerGlowing)
	{
		_playerCustom = playerCustom;
		_playerEffects = playerEffects;
		_playerGlowing = playerGlowing;
	}

	public void Update()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (NetworkBool.op_Implicit(_playerEffects.Glowing) && !NetworkBool.op_Implicit(_playerCustom.PlayerController.IsWolf))
		{
			PlayerController player = PlayerRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
			if (NetworkBool.op_Implicit(player.IsWolf))
			{
				if (_watch.IsRunning)
				{
					if ((float)_watch.ElapsedMilliseconds >= 500f)
					{
						((Behaviour)_playerGlowing).enabled = !((Behaviour)_playerGlowing).enabled;
						_watch.Restart();
					}
				}
				else
				{
					_watch.Restart();
				}
			}
			else if (!((Behaviour)_playerGlowing).enabled)
			{
				((Behaviour)_playerGlowing).enabled = true;
			}
		}
		else if (_watch.IsRunning)
		{
			_watch.Stop();
		}
	}
}
