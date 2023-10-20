using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButtonPanel : MonoBehaviour
{
    [SerializeField] private Transform grid = null;

    [Header("Prefab reference")]
    [SerializeField] private UnitButton unitButtonPrefab = null;
    [SerializeField] private Slot slotPrefab = null;

    [Space]
    [SerializeField] private int plantAmount = 44;

    private List<Slot> slots;
    private List<UnitButton> unitButtons;

    public System.Action<UnitButton> OnUnitButtonClick;

    public void Initialize()
    {
        SpawnUnitButton();
    }

    private void SpawnUnitButton()
    {
        unitButtons = new List<UnitButton>();
        slots = new List<Slot>();
        int count = 0;

        for (int i = 0; i < plantAmount; i++)
        {
            Slot newSlot = Instantiate(slotPrefab, grid);
            slots.Add(newSlot);
        }

        foreach (Data.UnitData data in ConfigHelper.UserData.ownPlants.Values)
        {
            UnitButton newUnitButton = Instantiate(unitButtonPrefab, grid);
            newUnitButton.OnUnitButtonClick = UnitButtonClick;

            newUnitButton.Initialize(data);
            slots[count].GetUnitButton(data);
            newUnitButton.slotOnPanel = slots[count];
            
            newUnitButton.transform.parent = slots[count].transform;
            
            newUnitButton.transform.localPosition = Vector3.zero;
            
            unitButtons.Add(newUnitButton);

            count++;
        }
    }

    public void AddToPanel(UnitButton unitButton)
    {
        Slot slotOnPanel = unitButton.slotOnPanel;

        unitButton.transform.parent = slotOnPanel.transform;
        unitButton.transform.localPosition = Vector3.zero;

        unitButton.slotOnHold.EmptySlotData();
        unitButton.slotOnHold = null;

        unitButton.OnUnitButtonClick = OnUnitButtonClick;
    }

    public Slot GetEmptySlot()
    {
        Slot emptySlot = slots.Find(s => s.unitData == null);

        return emptySlot;
    }

    private void UnitButtonClick(UnitButton unitButton)
    {
        OnUnitButtonClick?.Invoke(unitButton);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
