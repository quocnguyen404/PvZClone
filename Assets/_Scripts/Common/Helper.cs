using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helper
{
    //main cam reference
    private static Camera cam;
    public static Camera Cam
    {
        get
        {
            if (cam == null)
                cam = Camera.main;

            return cam;
        }
    }

    //smart wait for second dictionary
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out WaitForSeconds wait))
            return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }


    // point over ui with mouse
    private static PointerEventData eventDataCurrentPosition;
    private static List<RaycastResult> results;

    public static bool IsOverUI()
    {
        eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };

        results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        object p = RectTransformUtility.ScreenPointToLocalPointInRectangle(element, element.position, cam, out Vector2 result);
        return result;
    }

    public static void DeleteChildren(this Transform parent)
    {
        foreach (Transform child in parent) Object.Destroy(child.gameObject);
    }
}
