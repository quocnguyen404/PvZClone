using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelInGame : SettingPanel
{
    [SerializeField] protected ButtonUIHandler backToMenuBtn = null;
    [SerializeField] protected ButtonUIHandler restartBtn = null;

    protected override void Awake()
    {
        base.Awake();

        backToMenuBtn.AddListener(() => 
        {
            SceneDispatcher.Instance.LoadScene(Scene.MenuScene);
            TurnOff();
        });

        restartBtn.AddListener(() => 
        { 
            SceneDispatcher.Instance.LoadScene(Scene.GamePlayScene);
            TurnOff();
        });
    }
}
