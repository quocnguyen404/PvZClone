using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public Button DeleteUserDataBtn = null;
    [SerializeField] [Range(1, 10)] int timeScale;

    private void Awake()
    {
        DeleteUserDataBtn.onClick.AddListener(() => 
        { 
            ES3.DeleteKey("UserData123");
        });
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }
}
