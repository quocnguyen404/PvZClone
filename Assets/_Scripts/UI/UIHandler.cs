using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIHandler : MonoBehaviour
{
    protected CanvasGroup cv = null;
    public CanvasGroup CV
    {
        get
        {
            if (cv == null)
                cv = GetComponent<CanvasGroup>();

            return cv;
        }
    }

    protected float time = 0.7f;

    public virtual void SetVisuability(bool isActive)
    {
        if (isActive)
        {
            CV.alpha = 1.0f;
            CV.interactable = true;
            CV.blocksRaycasts = true;
        }
        else
        {
            CV.alpha = 0f;
            CV.interactable = false;
            CV.blocksRaycasts = false;
        }
    }


    protected Tween cvTween = null;
    public virtual void TurnOn()
    {
        cvTween.Kill();

        cvTween = CV.DOFade(1f, time)
            .OnComplete(() =>
            {
                CV.interactable = true;
                CV.blocksRaycasts = true;
            }).SetAutoKill(true);
    }

    public virtual void TurnOff()
    {
        cvTween.Kill();

        CV.interactable = false;
        CV.blocksRaycasts = false;

        cvTween = CV.DOFade(0f, time).SetAutoKill(true);
    }
}
