using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUnitManager : MonoBehaviour
{
    [SerializeField] private Button playButton = null;
    [SerializeField] private PlantButtonHold buttonHold = null;
    [SerializeField] private UnitButtonPanel buttonPanel = null;

    public Data.UnitData pickedUnit = null;

    private void Awake()
    {
        buttonPanel.OnUnitButtonClick = buttonHold.AddToHoldPanel;
        buttonHold.OnUnitButtonClick = buttonPanel.AddToPanel;
        buttonHold.Initialize();
        buttonPanel.Initialize();

        playButton.onClick.AddListener(() => { StartGame(); });
    }

    private void StartGame()
    {
        if (!PickFull())
            return;

        buttonPanel.Hide();
        buttonHold.OnUnitButtonClick = SeletedUnit;
        buttonHold.InitializeUnitData();
    }

    private bool PickFull()
    {
        return !buttonHold.slots.Find(s => s.unitData == null);
    }

    private void SeletedUnit(UnitButton unitButton)
    {
        pickedUnit = unitButton.unitData;
    }

}
