using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musics = null;
    [SerializeField] private AudioSource sounds = null;

    private Dictionary<string, AudioClip> soundClips;
    private Dictionary<string, AudioClip> musicClips;

    public void InitializeAudioClip()
    {
        //soundClips = Resources.LoadAll(GameConstant.SOUND_PATH, typeof(AudioClip))
    }

}
