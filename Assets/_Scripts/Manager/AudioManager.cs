using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] private AudioSource music = null;
    [SerializeField] private AudioSource sound = null;

    private Dictionary<Sound, AudioClip> soundClips;
    private Dictionary<Music, AudioClip> musicClips;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    public void Initialize()
    {
        soundClips = new Dictionary<Sound, AudioClip>();
        musicClips = new Dictionary<Music, AudioClip>();
    }

    public void PlaySound(Sound soundType)
    {
        sound.PlayOneShot(GetSound(soundType));
    }

    public void PlayMusic(Music musicType)
    {
        music.PlayOneShot(GetMusic(musicType));
    }

    public AudioClip GetSound(Sound soundType)
    {
        if (!soundClips.ContainsKey(soundType))
        {
            AudioClip clip = LoadAudioClip(soundType.ToString());
            soundClips.Add(soundType, clip);
        }

        return soundClips[soundType];
    }

    public AudioClip GetMusic(Music musicType)
    {
        if (!musicClips.ContainsKey(musicType))
        {
            AudioClip clip = LoadAudioClip(musicType.ToString());
            musicClips.Add(musicType, clip);
        }

        return musicClips[musicType];
    }

    private AudioClip LoadAudioClip(string clipName)
    {
        return Resources.Load<AudioClip>(string.Format(GameConstant.SOUND_PATH, clipName));
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}


