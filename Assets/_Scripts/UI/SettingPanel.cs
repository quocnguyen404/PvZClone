using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : UIPanel
{
    [SerializeField] protected ToggleUIHandler soundTogg = null;
    [SerializeField] protected ToggleUIHandler musicTogg = null;

    [Space]
    [SerializeField] protected Button rayBlock = null;

    [SerializeField] private Slider soundSlider = null;
    [SerializeField] private Slider musicSlider = null;


    protected override void Awake()
    {
        base.Awake();

        fadeTime = 0.5f;

        UpdateToggleValue();

        soundTogg.AddListener(AudioManager.Instance.SetSoundMute);
        musicTogg.AddListener(AudioManager.Instance.SetMusicMute);
        rayBlock.onClick.AddListener(TurnOff);

        soundSlider.onValueChanged.AddListener(AudioManager.Instance.SoundVolumeChange);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.MusicVolumeChange);
    }

    public override void TurnOn()
    {
        base.TurnOn();
        UpdateToggleValue();
    }

    protected void UpdateToggleValue()
    {
        soundSlider.value = ConfigHelper.UserData.soundVolume;
        musicSlider.value = ConfigHelper.UserData.musicVolume;
        soundTogg.SetValue(ConfigHelper.UserData.isSoundOn);
        musicTogg.SetValue(ConfigHelper.UserData.isMusicOn);
    }
}
