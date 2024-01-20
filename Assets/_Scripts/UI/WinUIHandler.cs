using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUIHandler : UIHandler
{
    [SerializeField] private UIHandler whitePanel = null;
    [SerializeField] private WinPanel winPanel = null;
    [SerializeField] private ButtonUIHandler nextLevelBtn = null;
    [SerializeField] private ButtonUIHandler menuBtn = null;

    private void Awake()
    {
        winPanel.SetVisuability(false);
        whitePanel.SetVisuability(false);
    }

    public void Initialize()
    {
        nextLevelBtn.AddListener(() => { SceneDispatcher.Instance.LoadScene(Scene.GamePlayScene); });
        menuBtn.AddListener(() => { SceneDispatcher.Instance.LoadScene(Scene.MenuScene); });
        winPanel.Initialize();
    }

    public void TurnOnWhitePanel()
    {
        whitePanel.TurnOn();
    }

    public void TurnOffWhitePanel()
    {
        whitePanel.TurnOff();
    }

    public void TurnOnWinPanel()
    {
        winPanel.TurnOn();
    }
}
