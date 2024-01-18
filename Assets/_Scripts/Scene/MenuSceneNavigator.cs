using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneNavigator : MonoBehaviour
{
    [SerializeField] private Button playNextLevelBtn = null;
    [SerializeField] private Button returnMenuBtn = null;
    [SerializeField] private Button exitGameBtn = null;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic(Music.MenuSceneBG);

        playNextLevelBtn.AddListener(LoadGamePlayScene);
        returnMenuBtn.AddListener(LoadMenuScene);
        exitGameBtn.AddListener(ExitGame);

        playNextLevelBtn.AddButtonSound();
        returnMenuBtn.AddButtonSound();
        exitGameBtn.AddButtonSound();
    }

    public void LoadGamePlayScene()
    {
        SceneDispatcher.Instance.LoadScene(Scene.GamePlayScene);
    }

    public void LoadMenuScene()
    {
        SceneDispatcher.Instance.LoadScene(Scene.MenuScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

