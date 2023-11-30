using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunInGameManager : MonoBehaviour
{
    [SerializeField] private GameObject sunbar = null;
    [SerializeField] private TextMeshProUGUI sunTxt = null;
    
    public static int CurrentSun { get; private set; }


    public void Initialize()
    {
        CurrentSun = 0;
        OnSunChange(GameConstant.SUN_START_COST);
        ShowSunBar();
    }

    public void PickSunUp(int value)
    {
        OnSunChange(value);
    }

    public void BuyPlant(Data.UnitData unit)
    {
        OnSunChange(-unit.cost);
    }

    public bool CanBuy(Data.UnitData unit)
    {
        return CurrentSun - unit.cost >= 0;
    }

    private void OnSunChange(int value)
    {
        CurrentSun += value;

        UpdateSunText();
    }

    public void ShowSunBar()
    {
        sunbar.gameObject.SetActive(true);
    }

    public Vector3 GetSunBarPosition()
    {
        return sunbar.transform.position;
    }

    private void UpdateSunText()
    {
        sunTxt.text = CurrentSun.ToString();
    }
}
