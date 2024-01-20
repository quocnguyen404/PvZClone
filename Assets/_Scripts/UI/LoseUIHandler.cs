using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUIHandler : UIHandler
{
    [SerializeField] UIHandler losePanel = null;
    [SerializeField] ButtonUIHandler playAgainBtn = null;
    [SerializeField] ButtonUIHandler menuBtn = null;

    private void Awake()
    {
        
    }

    public void Initialize()
    {
        losePanel.SetVisuability(false);
        playAgainBtn.AddListener(() => { SceneDispatcher.Instance.LoadScene(Scene.GamePlayScene); });
        menuBtn.AddListener(() => { SceneDispatcher.Instance.LoadScene(Scene.MenuScene); });
    }

    private Tween panelTween = null;
    public override void TurnOn()
    {
        base.TurnOn();

        if (panelTween != null)
            panelTween.Kill();

        panelTween = DOVirtual.DelayedCall(2f, () => { losePanel.TurnOn(); });
    }

    public override void TurnOff()
    {
        losePanel.TurnOff();
        base.TurnOff();
    }

}
