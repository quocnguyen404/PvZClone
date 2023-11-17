using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProduct : MonoBehaviour
{
    public string productName = "";
    public float existTime = 0;

    protected virtual void ReturnPool(float time)
    {
        StartCoroutine(ICountdown(time, () => { gameObject.SetActive(false); }));
    }

    private IEnumerator ICountdown(float time, System.Action Callback)
    {
        yield return Helper.GetWait(time);
        Callback?.Invoke();
    }
}
