using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image img = null;

    private Color emptyColor = new Color(1, 1, 1, 10f/255f);


    public Data.UnitData unitData
    {
        get; private set;
    }

    public void NotEmptySlot()
    {
        img.color = Color.clear;
    }

    public void GetUnitButton(Data.UnitData unitData)
    {
        this.unitData = unitData;
    }

    public void EmptySlotData()
    {
        unitData = null;
        img.color = emptyColor;
    }
}
