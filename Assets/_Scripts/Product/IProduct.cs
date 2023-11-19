using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProduct : MonoBehaviour
{
    public enum ProductState
    {
        Idle,
        Projectile,
        Hit,
    }


    [SerializeField] protected Animator ator = null;

    public string productName = "";
    public float existTime = 0;

    protected virtual void Update()
    {
        transform.eulerAngles = Helper.Cam.transform.eulerAngles;
    }

    protected virtual void ReturnPool()
    {
        gameObject.SetActive(false);
    }


    protected virtual void ReturnPool(float time)
    {
        StartCoroutine(ICountdown(time, () => { ReturnPool(); }));
    }

    private IEnumerator ICountdown(float time, System.Action Callback)
    {
        yield return Helper.GetWait(time);
        Callback?.Invoke();
    }
}
