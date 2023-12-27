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
    [Header("Toggle and Button")]
    public ButtonUIHandler PlayButton = null;
    [SerializeField] private ToggleUIHandler viewZombieTg = null;

    [Header("Panel")]
    [SerializeField] private UnitButtonPanel unitButtonPanel = null;
    [SerializeField] private PlantButtonHold plantButtonHold = null;


    [Header("UI Handler")]
    [SerializeField] private WinUIHandler winUIHandler = null;
    [SerializeField] private LoseUIHandler loseUIHandler = null;


    #region Event
    public System.Action<GameObject> OnTossGift = null;

    #endregion


    //cam when play x = 0.3
    //cam when pick plant x = 2.5
    //cam when view zombie x = 6

    private Camera cam = null;

    public void Awake()
    {
        cam = Helper.Cam;
        
        viewZombieTg.AddListener(ViewZombieCamera);

        viewZombieTg.SetVisuability(false);

        PlayButton.SetVisuability(false);
        unitButtonPanel.SetVisuability(false);
        plantButtonHold.SetVisuability(false);


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
                viewZombieTg.SetVisuability(true);
                PlayButton.SetVisuability(true);
                unitButtonPanel.TurnOn();
                plantButtonHold.TurnOn();
            });

        }).SetAutoKill(true);
    }

    public void StartGameTransition()
    {
        unitButtonPanel.TurnOff();
        viewZombieTg.SetVisuability(false);
        MoveCamera(0.3f, null);
    }

    public void WinTransition()
    {

    }

    public void LoseTransition()
    {

    }

    private void ViewZombieCamera(bool isOn)
    {
        if (isOn)
        {
            unitButtonPanel.TurnOff();
            plantButtonHold.TurnOff();
            PlayButton.TurnOff();

            MoveCamera(6f, null);
        }
        else
        {
            MoveCamera(2.5f, () =>
            {
                unitButtonPanel.TurnOn();
                plantButtonHold.TurnOn();
                PlayButton.TurnOn();
            });
        }
    }

    private Tween cameraTwene = null;
    public void MoveCamera(float x, System.Action callback)
    {
        viewZombieTg.SetInteracable(false);

        cameraTwene = cam.transform.DOMoveX(x, 2.5f)
            .OnComplete(() =>
            {
                viewZombieTg.SetInteracable(true);
                callback?.Invoke();
            })
            .SetAutoKill(true);
    }
}
