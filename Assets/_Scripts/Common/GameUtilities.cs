using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class GameUtilities
{
    public static void AddListener(this Button btn, UnityAction action)
    {
        if (btn == null)
            return;

        btn.onClick.AddListener(action);
    }

    public static void AddButtonSound(this Button btn)
    {
        if (btn == null)
            return;

        btn.onClick.AddListener(() => { AudioManager.Instance.PlaySound(Sound.ButtonClick); });
    }
    
    public static void GetGiftValue(Gift gift)
    {
        if (gift.Data.GiftType is GiftType.Currency)
            ConfigHelper.AddGold(System.Int32.Parse(gift.Data.Value));
        else if (gift.Data.GiftType is GiftType.Plant)
            ConfigHelper.AddNewPlant(gift.Data.Value);
    }

    public static GiftData GetCurrentGiftData()
    {
        GiftData giftData = ConfigHelper.GetCurrentLevelConfig().gift;

        if (giftData.GiftType is GiftType.Plant && ConfigHelper.UserData.ownPlants.ContainsKey(giftData.Value))
        {
            giftData.GiftType = GiftType.Currency;
            giftData.Value = "1000";
        }

        return giftData;
    }

    public static Date GetDate()
    {
        if (ConfigHelper.GetCurrentLevelConfig().levelIndex < 10)
            return Date.Day;
        else
            return Date.Night;
    }

    public static void RandomBehaviour(System.Action action)
    {
        int ran = Random.Range(0, 100000000);

        if (ran == 0)
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
