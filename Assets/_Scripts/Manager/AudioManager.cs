using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public System.Action OnSoundMute = null;
    public System.Action OnMusicMute = null;
    public System.Action<float> OnVolumeChange = null;

    [SerializeField] private AudioSource musicSource = null;
    [SerializeField] private AudioSource soundSource = null;
    public float VolumeScale => soundSource.volume;

    private Dictionary<Sound, AudioClip> soundClips = new Dictionary<Sound, AudioClip>();
    private Dictionary<Music, AudioClip> musicClips = new Dictionary<Music, AudioClip>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        //play loading audio
        //PlayMusic(Music.GamePlayBG);
    }

    public void PlaySound(Sound soundType)
    {
        AudioClip soundClip = GetSound(soundType);
        soundSource.PlayOneShot(soundClip, VolumeScale);
    }

    public void PlayMusic(Music musicType)
    {
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

    public void VolumeChange(float volumeScale)
    {
        musicSource.volume = volumeScale;
        soundSource.volume = volumeScale;
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


