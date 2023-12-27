using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour
{
    [SerializeField] private Slider slider = null;

    [SerializeField] [Range(0, 1)] private float progressAnimationMultiplier = 0.25f;

    private AsyncOperation loadOperation;
    private float currentValue;
    private float targetValue;

    private void Start()
    {
        slider.value = currentValue = targetValue = 0;
        loadOperation = SceneManager.LoadSceneAsync(1);
        loadOperation.allowSceneActivation = false;
    }


    private void Update()
    {
        targetValue = loadOperation.progress / 0.9f;

        currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime * 2);
        slider.value = currentValue;

        if (Mathf.Approximately(currentValue, 1))
            loadOperation.allowSceneActivation = true;
    }
}
