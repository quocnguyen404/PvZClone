using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingHandler : MonoBehaviour
{
    [SerializeField] private Slider loadingBar = null;
    [SerializeField] private RectTransform handle = null;

    [SerializeField] [Range(0, 1)] private float progressAnimationMultiplier = 0.25f;
    [SerializeField] [Range(0, 1)] private float evolutionTime = 0.5f;
    private AsyncOperation loadOperation;
    private float currentValue;
    private float targetValue;


    private Tween handleTween = null;
    private void Start()
    {
        if (handleTween != null)
            handleTween.Kill();

        loadingBar.value = currentValue = targetValue = 0;
        loadOperation = SceneManager.LoadSceneAsync(1);
        loadOperation.allowSceneActivation = false;
        
        Vector3 rotate = new Vector3(0, 0, -180);
        handleTween = handle.DORotate(rotate, evolutionTime)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Incremental);
    }


    private void Update()
    {
        targetValue = loadOperation.progress / 0.9f;

        currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime * 2);
        loadingBar.value = currentValue;

        if (Mathf.Approximately(currentValue, 1))
        {
            handleTween.Kill();
            loadOperation.allowSceneActivation = true;
        }
    }
}
