using Fusion;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerCustomAudioComponent : MonoBehaviour
{
	private AudioSource _audioSource;

	private AudioReverbFilter _reverbFilter;

	private PlayerCustom _playerCustom;

	public const AudioReverbPreset ReverbWolf = (AudioReverbPreset)1;

	public const AudioReverbPreset ReverbGiant = (AudioReverbPreset)19;

	public const AudioReverbPreset ReverbTelepathy = (AudioReverbPreset)23;

	private void Start()
	{
		_audioSource = ((Component)this).GetComponent<AudioSource>();
		_reverbFilter = ((Component)_audioSource).gameObject.AddComponent<AudioReverbFilter>();
		_reverbFilter.reverbPreset = (AudioReverbPreset)0;
		_audioSource.bypassEffects = false;
	}

	public void Init(PlayerCustom playerCustom)
	{
		_playerCustom = playerCustom;
	}

	public void UpdateReverbIfNeeded(AudioReverbPreset reverb)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_reverbFilter != (Object)null && _reverbFilter.reverbPreset != reverb)
		{
			_reverbFilter.reverbPreset = reverb;
		}
	}

	public void UpdateVolume()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		string text = "SETTINGS_CUSTOM_VOLUME_PLAYER_SAVED_";
		NetworkString<_64> iD = _playerCustom.PlayerController.PlayerData.ID;
		string text2 = text + ((object)iD/*cast due to constrained. prefix*/).ToString();
		float volume = (PlayerPrefs.HasKey(text2) ? PlayerPrefs.GetFloat(text2) : 0.75f);
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		if (NetworkBool.op_Implicit(_playerCustom.Kidnapped) && player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Kidnapper)
		{
			volume = 0.4f;
			if (NetworkBool.op_Implicit(player.NewPrimaryRoleUniqueBool))
			{
				volume = 0.18f;
			}
		}
		_audioSource.volume = volume;
	}
}
