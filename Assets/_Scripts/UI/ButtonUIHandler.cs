using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
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

    public Sound ButtonSound;


    private void OnEnable()
    {
        AddButtonHandlerSound(ButtonSound);
    }

    public void AddButtonHandlerSound(Sound buttonSound)
    {
        btn.onClick.AddListener(() => { AudioManager.Instance.PlaySound(buttonSound, 1f); });
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
