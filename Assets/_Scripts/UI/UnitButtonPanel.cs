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

    public UnitButton currentButtonSelected = null;

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
            slots[count].Initialize(data);
            newUnitButton.transform.parent = slots[count].transform;
            unitButtons.Add(newUnitButton);
        }
    }

    public void AddToPickPanel(UnitButton unitButton)
    {
        Slot emptySlot = GetEmptySlot();

        if (emptySlot != null)
            return;

        emptySlot.Initialize(unitButton.unitData);
    }

    public Slot GetEmptySlot()
    {
        Slot emptySlot = slots.Find(s => s.unitData == null);

        return emptySlot;
    }

    private void UnitButtonClick(UnitButton unitButton)
    {
        currentButtonSelected = unitButton;
        OnUnitButtonClick?.Invoke(unitButton);
    }
}
