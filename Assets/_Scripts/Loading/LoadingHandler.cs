using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar = null;
    [SerializeField] private Image filledBG = null;

    private bool isLoadDone;

    private void Awake()
    {
        isLoadDone = false;
        StartCoroutine(LoadMenuScene());
    }

    private void Start()
    {
        
    }

    private IEnumerator LoadMenuScene()
    {
        float countTime = 0f;
        float duration = 1f;
        while (!isLoadDone)
        {
            countTime += Time.deltaTime;

            scrollbar.value += 0.9f * (countTime/ duration);
            filledBG.fillAmount += 0.9f * (countTime/ duration);

            if (countTime >= duration)
            {
                if (scrollbar.value >= 0.9)
                    isLoadDone = true;

                break;
            }

            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)SceneNavigator.Scene.MenuScene);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
                asyncLoad.allowSceneActivation = true;

            yield return null;
        }

        yield return null;
    }
}
