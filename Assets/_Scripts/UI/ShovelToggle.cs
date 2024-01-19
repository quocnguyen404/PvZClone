using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelToggle : ToggleUIHandler
{
    [SerializeField] private Image shovelIcon = null;

    public System.Action<bool> OnPickShovel = null;

    public bool IsPickShovel => Togg.isOn;

    public void Awake()
    {
        AddListener(PickShovel);
    }

    private void PickShovel(bool isOn)
    {
        shovelIcon.gameObject.SetActive(!isOn);
        OnPickShovel?.Invoke(isOn);
    }

    public void UnPickShovel()
    {
        shovelIcon.gameObject.SetActive(true);
        Togg.isOn = false;
    }

    public Sprite GetShovelSprite()
    {
        return shovelIcon.sprite;
    }
}
