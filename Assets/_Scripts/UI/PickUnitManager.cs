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
    [SerializeField] private PlantObjectPool plantObjectPool = null;

    public System.Action<IUnit> OnPickedUnit = null;

    private void Awake()
    {
        buttonPanel.OnUnitButtonClick = buttonHold.AddToHoldPanel;
        buttonHold.OnUnitButtonClick = buttonPanel.AddToPanel;
        buttonHold.Initialize();
        buttonPanel.Initialize();
    }

    public void InitializeStartGame()
    {
        buttonPanel.Hide();
        buttonHold.InitializeUnitData();
        buttonHold.OnUnitButtonClick = SeletedUnit;

        plantObjectPool.InitilizePool(buttonHold.unitDatas);
    }


    public bool PickFull()
    {
        return !buttonHold.slots.Find(s => s.unitData == null);
    }

    private void SeletedUnit(UnitButton unitButton)
    {
        IUnit unit = plantObjectPool.GetPlant(unitButton.unitData);
        OnPickedUnit?.Invoke(unit);
    }
}
