using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtilities
{
    public static Vector3 RandomPositionOnPlane(Transform parent)
    {
        float rowMax = parent.position.x - (5 * 1) / 2 + 1 / 2;
        float columnMax = parent.position.y - (5 * 1) / 2 + 1 / 2;

        float x = Random.Range(-5, 5);
        float z = Random.Range(-5, 5);

        return new Vector3(x, parent.transform.position.y, z);
    }

    public static void DelayCall(this Plant plant, float time, System.Action Callback)
    {
        plant.StartCoroutine(IEDelayCall(time, Callback));
    }

    private static IEnumerator IEDelayCall(float time, System.Action Callback)
    {
        yield return Helper.GetWait(time);
        Callback?.Invoke();
    }
}
