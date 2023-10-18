using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private Button button = null;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text costText;

    public System.Action<UnitButton> OnUnitButtonClick;
    public Data.UnitData unitData { get; private set; }

    private void Awake()
    {
        button.onClick.AddListener(() => { OnUnitButtonClick?.Invoke(this); });
    }

    public void Initialize(Data.UnitData unitData)
    {
        this.unitData = unitData;
        icon.sprite = Resources.Load<Sprite>(string.Format(GameConstant.SPRITE_PATH, unitData.unitName));
        costText.text = unitData.cost.ToString();
    }
}
