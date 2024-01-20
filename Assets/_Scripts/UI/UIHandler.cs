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

    protected float fadeTime = 0.7f;
    public System.Action OnTurnOn = null;
    public System.Action OnTurnOff = null;

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
        if (cvTween != null)
            cvTween.Kill();

        cvTween = CV.DOFade(1f, fadeTime)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                CV.interactable = true;
                CV.blocksRaycasts = true;
                OnTurnOn?.Invoke();
            }).SetAutoKill(true);
    }

    public virtual void TurnOff()
    {
        if (cvTween != null)
            cvTween.Kill();

        OnTurnOff?.Invoke();
        CV.interactable = false;
        CV.blocksRaycasts = false;

        cvTween = CV.DOFade(0f, fadeTime).SetAutoKill(true);
    }
}
