using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] Image adventureBtn = null;
    [SerializeField] Sprite adventureSprite = null;
    [SerializeField] Sprite startAdventureSprite = null;

    [Space]
    [Header("Button")]
    [SerializeField] ButtonUIHandler playBtn = null;
    [SerializeField] ButtonUIHandler optionBtn = null;
    [SerializeField] ButtonUIHandler helpBtn = null;
    [SerializeField] ButtonUIHandler quitBtn = null;

    [Space]
    [Header("Panel")]
    [SerializeField] SettingPanel optionPanel = null;
    [SerializeField] UIPanel helpPanel = null;

    private void Awake()
    {
        if (ConfigHelper.UserData.userLevel <= 1)
            adventureBtn.sprite = startAdventureSprite;

        AudioManager.Instance.PlayMusic(Music.MenuSceneBG);
        optionPanel.SetVisuability(false);

        playBtn.AddListener(() => { SceneDispatcher.Instance.LoadScene(Scene.GamePlayScene); });
        optionBtn.AddListener(optionPanel.TurnOn);
    }

}
