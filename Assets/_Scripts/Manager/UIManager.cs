using DG.Tweening;
using DG.Tweening.CustomPlugins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<float> camPos;
    [SerializeField] private Transform giftCenterPos = null;

    [Space]
    [Header("Toggle and Button")]
    public ButtonUIHandler PlayButton = null;
    [SerializeField] private ToggleUIHandler viewZombieTg = null;
    [SerializeField] private ToggleUIHandler shovelToggle = null;

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

    public void Initialize()
    {
        cam = Helper.Cam;

        viewZombieTg.AddListener(ViewZombieCamera);

        viewZombieTg.SetVisuability(false);
        PlayButton.SetVisuability(false);
        unitButtonPanel.SetVisuability(false);
        plantButtonHold.SetVisuability(false);
        shovelToggle.SetVisuability(false);

        loseUIHandler.SetVisuability(false);
        winUIHandler.SetVisuability(false);

        loseUIHandler.Initialize();
        winUIHandler.Initialize();
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
        plantButtonHold.TurnOff();
        MoveCamera(0.3f, () => 
        {
            shovelToggle.TurnOn();
            plantButtonHold.TurnOn();
            AudioManager.Instance.PlayMusic(Music.GamePlayBG); 
        });
    }

    private Tween giftTween = null;
    public void WinTransition(Gift gift)
    {
        if (giftTween != null)
            giftTween.Kill();

        winUIHandler.TurnOn();
        plantButtonHold.TurnOff();
        shovelToggle.TurnOff();

        AudioManager.Instance.PlaySound(Sound.WinMusic);

        DOVirtual.DelayedCall(GameConstant.TIME_GIFT_MOVE / 1.2f, () => { winUIHandler.TurnOnWhitePanel(); })
            .SetAutoKill();

        giftTween = gift.transform.DOMove(giftCenterPos.position, GameConstant.TIME_GIFT_MOVE)
            .OnComplete(() => 
            {
                winUIHandler.TurnOnWinPanel();
            });

    }

    public void LoseTransition()
    {
        AudioManager.Instance.PlaySound(Sound.Scream);
        AudioManager.Instance.PlaySound(Sound.LoseMusic);
        loseUIHandler.TurnOn();
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

    private Tween cameraTween = null;
    public void MoveCamera(float xPos, System.Action callback)
    {
        if (cameraTween != null)
            cameraTween.Kill();

        viewZombieTg.SetInteracable(false);

        cameraTween = cam.transform.DOMoveX(xPos, 2.5f)
            .OnComplete(() =>
            {
                viewZombieTg.SetInteracable(true);
                callback?.Invoke();
            })
            .SetAutoKill(true);
    }
}
