using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public System.Action OnSoundMute = null;
    public System.Action OnMusicMute = null;
    public System.Action<float> OnVolumeChange = null;

    [SerializeField] private AudioSource audioSource = null;
    public float VolumeScale => audioSource.volume;

    private Dictionary<Sound, AudioClip> soundClips = new Dictionary<Sound, AudioClip>();
    private Dictionary<Music, AudioClip> musicClips = new Dictionary<Music, AudioClip>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        //play background audio
    }

    public void PlaySound(Sound soundType, float volumeScale)
    {
        AudioClip soundClip = GetSound(soundType);
        audioSource.PlayOneShot(soundClip, volumeScale);
    }

    public void PlayMusic(Music musicType, float volumeScale)
    {
        AudioClip musicClip = GetMusic(musicType);
        audioSource.PlayOneShot(musicClip, volumeScale);
    }

    public void VolumeChange(float volumeScale)
    {
        audioSource.volume = volumeScale;
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


