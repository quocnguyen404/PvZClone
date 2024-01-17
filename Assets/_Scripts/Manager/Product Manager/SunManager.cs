using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SunManager : ProductManager
{
    public System.Action<Sun> OnSunClick = null;

    public override void AddProduct(IProduct product)
    {
        base.AddProduct(product);
        Sun sun = product as Sun;

        if (sun == null)
            return;

        sun.OnClick = SunClick;
    }

    public void SunClick(Sun sun)
    {
        OnSunClick?.Invoke(sun);
        AudioManager.Instance.PlaySound(Sound.PickSun);
    }
}
