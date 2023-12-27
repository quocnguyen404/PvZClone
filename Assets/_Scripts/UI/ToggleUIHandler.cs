using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleUIHandler : UIHandler
{
    private Toggle togg = null;

    public Toggle Togg
    {
        get
        {
            if (togg == null)
                togg = GetComponent<Toggle>();

            return togg;
        }
    }

    public void SetInteracable(bool value)
    {
        Togg.interactable = value;
    }

    public void AddListener(UnityAction<bool> action)
    {
        Togg.onValueChanged.AddListener(action);
    }

    public void RemoveListener(UnityAction<bool> action)
    {
        Togg.onValueChanged.RemoveListener(action);
    }

    public void RemoveAllListener()
    {
        Togg.onValueChanged.RemoveAllListeners();
    }
}
