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
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Image recharge = null;

    public Slot slotOnPanel = null;
    public Slot slotOnHold = null;

    public System.Action<UnitButton> OnUnitButtonClick;
    public Data.UnitData unitData { get; private set; }

    private void Awake()
    {
        button.onClick.AddListener(() => { OnUnitButtonClick?.Invoke(this); });
    }

    public void Initialize(Data.UnitData unitData)
    {
        this.unitData = unitData;
        icon.sprite = Resources.Load<Sprite>(string.Format(GameConstant.CARDS_PATH, unitData.unitName));
        costText.text = unitData.cost.ToString();
    }

    public void Recharge(float time)
    {
        recharge.fillAmount = 1;
        button.interactable = false;

        recharge.DOFillAmount(0f, time)
            .OnComplete(() =>
            {
                button.interactable = true;
            })
            .SetAutoKill();
    }
}
