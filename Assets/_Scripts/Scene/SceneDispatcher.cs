using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneDispatcher : MonoBehaviour
{
    public static SceneDispatcher Instance = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void ButtonNavigateScene(Button btn, Scene sceneName)
    {
        btn.AddListener(() => { LoadScene(sceneName); });
    }

    public void LoadScene(Scene scene)
    {
        StartCoroutine(LoadGamePlaySceneAsync(scene));
    }

    protected IEnumerator LoadGamePlaySceneAsync(Scene sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return null;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
