using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtilities
{
    public static Date GetDate()
    {
        if (ConfigHelper.GetCurrentLevelConfig().levelIndex < 10)
            return Date.Day;
        else
            return Date.Night;
    }

    public static void RandomBehaviour(System.Action action)
    {
        int ran = Random.Range(0, 10);

        if (ran % 2 == 0)
            action?.Invoke();
        else
            return;
    }

    public static Sound RandomSound(Sound s1, Sound s2)
    {
        int ran = Random.Range(0, 99);

        if (ran % 2 == 0)
            return s1;
        else
            return s2;
    }

    public static float TimeToDestination(Vector3 start, Vector3 end, float speedPerNode)
    {
        return Vector3.Distance(start, end) * speedPerNode;
    }

    public static void DelayCall(this MonoBehaviour unit, float time, System.Action Callback)
    {
        unit.StartCoroutine(IEDelayCall(time, Callback));
    }

    private static IEnumerator IEDelayCall(float time, System.Action Callback)
    {
        yield return Helper.GetWait(time);
        Callback?.Invoke();
    }
}
