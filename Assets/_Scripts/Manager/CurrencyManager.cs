using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private GameObject sunbar = null;
    [SerializeField] private TextMeshProUGUI sunTxt = null;

    private int currentSun = 0;

    public void Initialize()
    {
        OnSunChange(25);
        ShowSunBar();
    }

    public void BuyPlant(Data.UnitData unit)
    {
        OnSunChange(-unit.cost);
    }

    public bool CanBuy(Data.UnitData unit)
    {
        return currentSun - unit.cost > 0;
    }

    private void OnSunChange(int value)
    {
        currentSun += value;

        UpdateSunText();
    }

    public void ShowSunBar()
    {
        sunbar.SetActive(true);
    }

    private void UpdateSunText()
    {
        sunTxt.text = currentSun.ToString();
    }
}
