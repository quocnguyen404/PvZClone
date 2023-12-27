using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUIHandler : UIHandler
{
    [SerializeField] UIHandler panel = null;
    [SerializeField] Button playAgainBtn = null;
    [SerializeField] Button menuBtn = null;

    public void Initialize()
    {
        panel.SetVisuability(false);
        SceneDispatcher.Instance.ButtonNavigateScene(playAgainBtn, Scene.GamePlayScene);
        SceneDispatcher.Instance.ButtonNavigateScene(menuBtn, Scene.MenuScene);
    }

    private Tween panelTween = null;
    public override void TurnOn()
    {
        base.TurnOn();

        if (panelTween != null)
            panelTween.Kill();

        panelTween = DOVirtual.DelayedCall(1f, () => { panel.TurnOn(); });
    }

    public override void TurnOff()
    {
        panel.TurnOff();
        base.TurnOff();
    }

}
