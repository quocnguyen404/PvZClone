using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] int timeScale;
    public bool ResetUserData = false;
    private void Awake()
    {
        if (ResetUserData)
            ES3.DeleteKey("UserData123");

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }
}
