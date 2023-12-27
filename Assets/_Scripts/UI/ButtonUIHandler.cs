using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonUIHandler : UIHandler
{
    private Button btn = null;
    public Button Btn
    {
        get
        {
            if (btn == null)
                btn = GetComponent<Button>();

            return btn;
        }
    }

    public void AddListener(UnityAction action)
    {
        Btn.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        Btn.onClick.RemoveListener(action);
    }

    public void RemoveAllListener()
    {
        Btn.onClick.RemoveAllListeners();
    }
}
