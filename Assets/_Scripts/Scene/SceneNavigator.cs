using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    public enum Scene
    {
        LoadingScene,
        MenuScene,
        GamePlayScene,
    }

    [SerializeField] private Button playNextLevelBtn = null;
    [SerializeField] private Button returnMenuBtn = null;
    [SerializeField] private Button exitGameBtn = null;

    private void Awake()
    {
        playNextLevelBtn.AddListener(LoadGamePlayScene);
        returnMenuBtn.AddListener(LoadMenuScene);
        exitGameBtn.AddListener(ExitGame);
    }

    public void LoadGamePlayScene()
    {
        StartCoroutine(LoadGamePlaySceneAsync(Scene.GamePlayScene));
    }

    public void LoadMenuScene()
    {
        StartCoroutine(LoadGamePlaySceneAsync(Scene.MenuScene));
    }

    public void LoadScene(Scene scene)
    {
        StartCoroutine(LoadGamePlaySceneAsync(scene));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadGamePlaySceneAsync(Scene sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return null;
    }
}

