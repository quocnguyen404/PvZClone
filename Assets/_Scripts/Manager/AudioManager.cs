using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] private AudioSource musicSource = null;
    [SerializeField] private AudioSource soundSource = null;
    public float SoundVolume => soundSource.volume;

    private Dictionary<Sound, AudioClip> soundClips = new Dictionary<Sound, AudioClip>();
    private Dictionary<Music, AudioClip> musicClips = new Dictionary<Music, AudioClip>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        soundSource.mute = !ConfigHelper.UserData.isSoundOn;
        musicSource.mute = !ConfigHelper.UserData.isMusicOn;
    }

    public void PlaySound(Sound soundType)
    {
        if (soundType is Sound.None)
            return;

        AudioClip soundClip = GetSound(soundType);
        soundSource.PlayOneShot(soundClip, SoundVolume);
    }

    public void PlayMusic(Music musicType)
    {
        if (musicType is Music.None)
            return;

        AudioClip musicClip = GetMusic(musicType);
        SetLoop(true);
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Pause();
        musicSource.clip = null;
        SetLoop(false);
    }

    public void SetLoop(bool value)
    {
        musicSource.loop = value;
    }

    public void SetSoundMute(bool value)
    {
        soundSource.mute = !value;
        ConfigHelper.UserData.isSoundOn = value;
    }

    public void SetMusicMute(bool value)
    {
        musicSource.mute = !value;
        ConfigHelper.UserData.isMusicOn = value;
    }

    public void SoundVolumeChange(float volumeScale)
    {
        soundSource.volume = volumeScale;
        ConfigHelper.UserData.soundVolume = volumeScale;
    }

    public void MusicVolumeChange(float volumeScale)
    {
        musicSource.volume = volumeScale;
        ConfigHelper.UserData.musicVolume = volumeScale;
    }

    public AudioClip GetSound(Sound soundType)
    {
        if (!soundClips.ContainsKey(soundType))
        {
            AudioClip clip = LoadSoundClip(soundType.ToString());
            soundClips.Add(soundType, clip);
        }

        return soundClips[soundType];
    }

    public AudioClip GetMusic(Music musicType)
    {
        if (!musicClips.ContainsKey(musicType))
        {
            AudioClip clip = LoadMusicClip(musicType.ToString());
            musicClips.Add(musicType, clip);    
        }

        return musicClips[musicType];
    }

    private AudioClip LoadMusicClip(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>(string.Format(GameConstant.MUSIC_PATH, clipName));
        return clip;
    }

    private AudioClip LoadSoundClip(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>(string.Format(GameConstant.SOUND_PATH, clipName)); ;
        return clip;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}


