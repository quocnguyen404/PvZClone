using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinPanel : UIHandler
{
    [SerializeField] private Image plantIcon = null;
    [SerializeField] private TextMeshProUGUI title = null;
    [SerializeField] private TextMeshProUGUI description = null;

    public void Initialize()
    {
        GiftData giftData = GameUtilities.GetCurrentGiftData();

        //Data.UnitData unitData = ConfigHelper.GameConfig.plants[giftData.Value];

        title.text = giftData.Value;
        plantIcon.sprite = Resources.Load<Sprite>(string.Format(GameConstant.PLANT_SPRITES_PATH, giftData.Value));
    }
}
