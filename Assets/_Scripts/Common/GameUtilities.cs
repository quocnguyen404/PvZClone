using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtilities
{
    public static void DelayCall(this IUnit unit, float time, System.Action Callback)
    {
        unit.StartCoroutine(IEDelayCall(time, Callback));
    }

    private static IEnumerator IEDelayCall(float time, System.Action Callback)
    {
        yield return Helper.GetWait(time);
        Callback?.Invoke();
    }
}
