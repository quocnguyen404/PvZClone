using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUnitManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private PlantButtonHold buttonHold = null;
    [SerializeField] private UnitButtonPanel buttonPanel = null;

    public System.Action<IUnit, UnitButton> OnPickedUnit = null;
    public System.Func<Data.UnitData, IUnit> OnGetPlant = null;

    private void Awake()
    {
        buttonPanel.OnUnitButtonClick = buttonHold.AddToHoldPanel;
        buttonHold.OnUnitButtonClick = buttonPanel.AddToPanel;
        buttonHold.Initialize();
        buttonPanel.Initialize();
    }

    public void InitializeUnitData()
    {
        buttonPanel.Hide();
        buttonHold.InitializeUnitData();
        buttonHold.OnUnitButtonClick = PickUnit;
    }

    public void InitializeUnitDataLimitPlant()
    {

    }

    public bool PickFull()
    {
        return !buttonHold.slots.Find(s => s.unitData == null);
    }

    public List<Data.UnitData> PlantDatas()
    {
        return buttonHold.unitDatas;
    }

    private void PickUnit(UnitButton unitButton)
    {
        IUnit unit = OnGetPlant?.Invoke(unitButton.unitData);
        OnPickedUnit?.Invoke(unit, unitButton);
    }

}
