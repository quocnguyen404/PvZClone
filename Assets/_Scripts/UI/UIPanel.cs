using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : UIHandler
{
    [SerializeField] private ButtonUIHandler exitBtn = null;

    protected virtual void Awake()
    {
        exitBtn.AddListener(TurnOff);
    }
}
