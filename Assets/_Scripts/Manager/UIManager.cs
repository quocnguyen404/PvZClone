using DG.Tweening;
using DG.Tweening.CustomPlugins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<float> camPos;

    [Space]
    [Header("UI Element Reference")]
    [SerializeField] private Toggle viewZombieTg = null;
    [SerializeField] private UnitButtonPanel unitButtonPanel = null;
    [SerializeField] private PlantButtonHold plantButtonHold = null;

    //cam when play x = 0.3
    //cam when pick plant x = 2.5
    //cam when view zombie x = 6

    private Camera cam = null;

    public void Awake()
    {
        viewZombieTg.onValueChanged.AddListener(ViewZombieCamera);
        viewZombieTg.gameObject.SetActive(false);
        unitButtonPanel.SetVisuability(false);
        plantButtonHold.SetVisuability(false);
        cam = Helper.Cam;
    }

    public void Initialize()
    {

    }

    public void BeginMatch()
    {
        DOVirtual.DelayedCall(1f, () =>
        {
            Sequence camSeq = DOTween.Sequence();

            foreach (float x in camPos)
                camSeq.Append(cam.transform.DOMoveX(x, 2.5f));

            camSeq.OnComplete(() =>
            {
                viewZombieTg.gameObject.SetActive(true);
                unitButtonPanel.TurnOn();
                plantButtonHold.TurnOn();
            });

        }).SetAutoKill(true);

    }

    public void StartGameTransition()
    {
        unitButtonPanel.TurnOff();
        viewZombieTg.gameObject.SetActive(false);
        MoveCamera(0.3f, null);
    }


    private void ViewZombieCamera(bool isOn)
    {
        if (isOn)
        {
            unitButtonPanel.TurnOff();
            plantButtonHold.TurnOff();

            MoveCamera(6f, null);
        }
        else
        {
            MoveCamera(2.5f, () =>
            {
                unitButtonPanel.TurnOn();
                plantButtonHold.TurnOn();
            });
        }
    }

    private Tween cameraTwene = null;
    public void MoveCamera(float x, System.Action callback)
    {
        viewZombieTg.interactable = false;

        cameraTwene = cam.transform.DOMoveX(x, 2.5f)
            .OnComplete(() =>
            {
                viewZombieTg.interactable = true;
                callback?.Invoke();
            })
            .SetAutoKill(true);
    }
}
