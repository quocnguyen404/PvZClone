using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitButtonPanel : UIHandler
{
    [SerializeField] private Transform grid = null;

    [Header("Prefab reference")]
    [SerializeField] private UnitButton unitButtonPrefab = null;
    [SerializeField] private Slot slotPrefab = null;

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

        for (int i = 0; i < GameConstant.PLANT_AMOUNT; i++)
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
            slots[count].NotEmptySlot();
            newUnitButton.slotOnPanel = slots[count];

            newUnitButton.transform.SetParent(slots[count].transform);
            newUnitButton.transform.localPosition = Vector3.zero;

            unitButtons.Add(newUnitButton);

            count++;
        }
    }

    public void AddToPanel(UnitButton unitButton)
    {
        Slot slotOnPanel = unitButton.slotOnPanel;

        unitButton.transform.DOKill();
        unitButton.transform.SetParent(slotOnPanel.transform);
        unitButton.transform.DOLocalMove(Vector3.zero, GameConstant.TIME_BUTTON_MOVE / 2).SetAutoKill();

        unitButton.slotOnHold.EmptySlotData();
        unitButton.slotOnHold = null;

        unitButton.OnUnitButtonClick = UnitButtonClick;
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

    //public void Hide()
    //{
    //    gameObject.SetActive(false);
    //}
}
