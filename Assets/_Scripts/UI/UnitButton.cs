using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private Button button = null;
    [SerializeField] private Image icon = null;
    [SerializeField] private Image recharge = null;

    private Sprite plantSprite = null;

    private bool isRecharge = false;
    public Slot slotOnPanel = null;
    public Slot slotOnHold = null;

    public System.Action<UnitButton> OnUnitButtonClick;
    public Data.UnitData unitData { get; private set; }

    private void Awake()
    {
        button.onClick.AddListener(() => { OnUnitButtonClick?.Invoke(this); });
    }

    private void FixedUpdate()
    {
        if (!GameManager.IsStartGame)
            return;

        button.interactable = (unitData.cost <= SunInGameManager.CurrentSun) && !isRecharge;
    }

    public void Initialize(Data.UnitData unitData)
    {
        this.unitData = unitData;
        icon.sprite = Resources.Load<Sprite>(string.Format(GameConstant.CARDS_SPRITES_PATH, unitData.unitName));
        plantSprite = Resources.Load<Sprite>(string.Format(GameConstant.PLANT_SPRITES_PATH, unitData.unitName));
    }

    public void Recharge(float time)
    {
        recharge.fillAmount = 1;
        isRecharge = true;

        recharge.DOKill();
        recharge.DOFillAmount(0f, time)
            .OnComplete(() =>
            {
                isRecharge = false;
            })
            .SetAutoKill();
    }

    public Sprite GetUnitSprite()
    {
        if (plantSprite == null)
        {
            Debug.Log("Sprite of button is null");
            return null;
        }

        return plantSprite;
    }
}
