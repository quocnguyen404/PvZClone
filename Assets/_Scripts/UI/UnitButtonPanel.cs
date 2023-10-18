using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButtonPanel : MonoBehaviour
{
    [SerializeField] private Transform grid = null;
    [SerializeField] private UnitButton unitButtonPrefab = null;
    [SerializeField] private Slot slotPrefab = null;

    private List<Slot> slots;
    private List<UnitButton> unitButtons;
    public void Initialize()
    {
        SpawnSlot();
        SpawnUnitButton();
    }

    private void SpawnSlot()
    {
        slots = new List<Slot>();

        for (int i = 0; i < ConfigHelper.plantAmount; i++)
        {
            Slot newSlot = Instantiate(slotPrefab, grid);
            newSlot.isEmpty = true;

            slots.Add(newSlot);
        }
    }

    private void SpawnUnitButton()
    {
        int count = 0;
        unitButtons = new List<UnitButton>();

        foreach (Data.UnitData data in ConfigHelper.UserData.ownPlants.Values)
        {
            UnitButton newUnitButton = Instantiate(unitButtonPrefab, grid);
            newUnitButton.unitData = data;
            unitButtons.Add(newUnitButton);

            newUnitButton.transform.parent = slots[count].transform;
            //newUnitButton.transform.position = Vector3.zero;
        }
    }


}
