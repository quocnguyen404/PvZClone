using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUIHandler : UIHandler
{
    [SerializeField] private WinPanel winPanel = null;
    [SerializeField] private Button nextLevelBtn = null;
    [SerializeField] private Button menuBtn = null;

    private void Awake()
    {
        SetVisuability(false);
    }

    public void Initialize()
    {
        //SceneDispatcher.Instance.ButtonNavigateScene(nextLevelBtn, Scene.GamePlayScene);
        //SceneDispatcher.Instance.ButtonNavigateScene(menuBtn, Scene.MenuScene);
        winPanel.Initialize();
    }

    public void WinPanelOpen()
    {
        TurnOn();
    }
}
