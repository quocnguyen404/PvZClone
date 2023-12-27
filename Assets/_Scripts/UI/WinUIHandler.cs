using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUIHandler : UIHandler
{
    [SerializeField] private Button nextLevelBtn = null;
    [SerializeField] private Button menuBtn = null;

    public void Initialize()
    {
        SceneDispatcher.Instance.ButtonNavigateScene(nextLevelBtn, Scene.GamePlayScene);
        SceneDispatcher.Instance.ButtonNavigateScene(menuBtn, Scene.MenuScene);
    }

    public void WinTransition()
    {

    }
}
